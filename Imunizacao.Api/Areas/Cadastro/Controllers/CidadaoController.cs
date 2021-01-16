using System;
using System.Collections.Generic;
using System.Net;
using RgCidadao.Api.Helpers;
using RgCidadao.Api.ViewModels;
using RgCidadao.Domain.Entities;
using RgCidadao.Domain.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RgCidadao.Domain.Entities.Imunizacao;
using RgCidadao.Domain.Repositories.Cadastro;
using RgCidadao.Domain.Entities.Cadastro;
using Microsoft.Extensions.Configuration;
using RgCidadao.Api.ViewModels.Cadastro;
using System.Linq;
using RgCidadao.Api.Filters;
using RgCidadao.Domain.ViewModels.Cadastros;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.IO;
using RgCidadao.Services.ViewModels;

namespace RgCidadao.Api.Controllers
{
    [Route("api/Cadastro/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("Cadastro")]
    public class CidadaoController : ControllerBase
    {
        private readonly ICidadaoRepository _cidadaoRepository;
        private IConfiguration _config;
        public CidadaoController(ICidadaoRepository cidadaorepository, IConfiguration configuration)
        {
            _cidadaoRepository = cidadaorepository;
            _config = configuration;
        }

        [HttpGet("GetAll")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<Cidadao>> GetAll([FromHeader]string ibge)//[FromBody] ParametersCidadao model,
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<Cidadao> lista = _cidadaoRepository.GetAll(ibge, string.Empty);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetAllPagination")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<Cidadao>> GetAllPagination([FromHeader]string ibge, int page,
                                                                                            int pagesize, string search, string fields, string datanasc, string situacoes)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                string filtro = string.Empty;

                if (situacoes != null && situacoes.Split(",").Length > 0 && situacoes.Split(",")[0] != null)
                {
                    foreach (var item in situacoes.Split(","))
                    {
                        if (!string.IsNullOrWhiteSpace(filtro))
                            filtro += " OR ";
                        else
                            filtro += " ( ";

                        filtro += $" C.CSI_SITUACAO LIKE '%{item}%'";
                    }
                }

                if (!string.IsNullOrWhiteSpace(filtro))
                    filtro += " ) ";

                if (!string.IsNullOrWhiteSpace(datanasc) && !string.IsNullOrWhiteSpace(filtro))
                    filtro += $" AND C.CSI_DTNASC = '{Convert.ToDateTime(datanasc).ToString("dd/MM/yyyy")}'";
                else if (!string.IsNullOrWhiteSpace(datanasc) && string.IsNullOrWhiteSpace(filtro))
                    filtro += $" C.CSI_DTNASC = '{Convert.ToDateTime(datanasc).ToString("dd/MM/yyyy")}'";

                if (!string.IsNullOrWhiteSpace(search))
                {
                    if (fields != null && fields.Split(",").Length > 0 && fields.Split(",")[0] != null)
                    {
                        if (string.IsNullOrWhiteSpace(filtro))
                            filtro += Helper.GetFiltroInicial(fields, search);
                        else
                            filtro += Helper.GetFiltro(fields, search);
                    }
                    else
                    {
                        var stringcod = string.Empty;
                        if (Helper.soContemNumeros(search))
                            stringcod = $" c.csi_codpac = {search} OR ";

                        if (!string.IsNullOrWhiteSpace(filtro))
                        {
                            filtro += $@" AND ( {stringcod}
                                            (SELECT UPPER(RETORNO) FROM TIRA_ACENTOS(C.CSI_NOMPAC)) LIKE '%{Helper.RemoveAcentos(search.ToUpper())}%' OR
                                            C.csi_sexpac LIKE '%{search}%' OR
                                            C.csi_ncartao LIKE '%{search}%' OR
                                            C.CSI_CPFPAC LIKE '%{search}%')";
                        }
                        else
                        {
                            filtro += $@"  ( {stringcod}
                                            (SELECT UPPER(RETORNO) FROM TIRA_ACENTOS(C.CSI_NOMPAC)) LIKE '%{Helper.RemoveAcentos(search.ToUpper())}%' OR
                                            C.csi_sexpac LIKE '%{search}%' OR
                                            C.csi_ncartao LIKE '%{search}%' OR
                                            C.CSI_CPFPAC LIKE '%{search}%')";
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(filtro))
                    filtro = " WHERE " + filtro;

                string sql_estrutura = string.Empty;
                if (_cidadaoRepository.VerificaExisteEsusFamilia(ibge))
                    sql_estrutura = $@"LEFT JOIN ESUS_FAMILIA FAM ON (C.ID_FAMILIA = FAM.ID)
                                       LEFT JOIN VS_ESTABELECIMENTOS D ON D.ID = FAM.ID_DOMICILIO";
                else
                    sql_estrutura = $@"LEFT JOIN ESUS_CADDOMICILIAR D ON C.ID_ESUS_CADDOMICILIAR = D.ID";

                int count = _cidadaoRepository.GetCountAll(ibge, filtro);
                if (count == 1)
                    page = 0;
                else
                    page = page * pagesize;

                Response.Headers.Add("X-Total-Count", count.ToString());
                List<Cidadao> lista = _cidadaoRepository.GetAllPagination(ibge, filtro, page, pagesize, sql_estrutura);

                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetAllPaginationWithFamilia")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<Cidadao>> GetAllPaginationWithFamilia([FromHeader]string ibge, int page, int pagesize, string search, string fields)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                string filtro = string.Empty;

                if (!string.IsNullOrWhiteSpace(search))
                {
                    if (fields != null && fields.Split(",").Length > 0 && fields.Split(",")[0] != null)
                    {
                        filtro += Helper.GetFiltro(fields, search);
                    }
                    else
                    {
                        var stringcod = string.Empty;
                        if (Helper.soContemNumeros(search))
                            stringcod = $" cp.csi_codpac = {search} OR ";

                        filtro += $@" WHERE ( {stringcod}
                                             (SELECT UPPER(RETORNO) FROM TIRA_ACENTOS(CP.CSI_NOMPAC)) LIKE '%{Helper.RemoveAcentos(search.ToUpper())}%')";
                    }
                }

                int count = _cidadaoRepository.GetCountAllWithFamilia(ibge, filtro);
                if (count == 1)
                    page = 0;
                else
                    page = page * pagesize;

                Response.Headers.Add("X-Total-Count", count.ToString());
                List<CidadaoFamiliaViewModel> lista = _cidadaoRepository.GetAllPaginationWithFamilia(ibge, page, pagesize, filtro);

                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetFiltroAvancado")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<Cidadao>> GetFiltroAvancado([FromHeader]string ibge, [FromQuery] ParametersCidadao model)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                //monta aqui o sql
                string filtro = $@" WHERE 1 = 1 ";

                if (!string.IsNullOrWhiteSpace(model.nome))
                    filtro += $@" AND (SELECT UPPER(RETORNO) FROM TIRA_ACENTOS(C.CSI_NOMPAC)) LIKE '%{Helper.RemoveAcentos(model.nome.ToUpper())}%' ";

                if (!string.IsNullOrWhiteSpace(model.nomeMae))
                    filtro += $@" AND (SELECT UPPER(RETORNO) FROM TIRA_ACENTOS(C.CSI_MAEPAC)) LIKE '%{Helper.RemoveAcentos(model.nomeMae.ToUpper())}%'";

                if (!string.IsNullOrWhiteSpace(model.cpf))
                    filtro += $@" AND C.CSI_CPFPAC LIKE '%{model.cpf}%'";

                if (!string.IsNullOrWhiteSpace(model.dataNascimento))
                    filtro += $@" AND C.CSI_DTNASC = '{Convert.ToDateTime(model.dataNascimento).ToString("dd.MM.yyyy")}'";

                if (model.codigo != null)
                    filtro += $@" AND C.CSI_CODPAC = {model.codigo}";

                if (!string.IsNullOrWhiteSpace(model.cns))
                    filtro += $@" AND C.CSI_NCARTAO LIKE '%{model.cns}%'";

                string sql_estrutura = string.Empty;
                if (_cidadaoRepository.VerificaExisteEsusFamilia(ibge))
                    sql_estrutura = $@"LEFT JOIN ESUS_FAMILIA FAM ON (C.ID_FAMILIA = FAM.ID)
                                       LEFT JOIN VS_ESTABELECIMENTOS D ON D.ID = FAM.ID_DOMICILIO";
                else
                    sql_estrutura = $@"LEFT JOIN ESUS_CADDOMICILIAR D ON C.ID_ESUS_CADDOMICILIAR = D.ID";

                int count = _cidadaoRepository.GetCountAll(ibge, filtro);
                if (count == 1)
                    model.page = 0;
                else
                    model.page = model.page * model.pagesize;

                Response.Headers.Add("X-Total-Count", count.ToString());
                List<Cidadao> lista = _cidadaoRepository.GetAllPagination(ibge, filtro, model.page, model.pagesize, sql_estrutura);

                return Ok(lista);

            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetCidadaoById/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<Cidadao> GetCidadaoById([FromHeader]string ibge, [FromRoute]int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));

                string sql_estrutura = string.Empty;
                if (_cidadaoRepository.VerificaExisteEsusFamilia(ibge))
                    sql_estrutura = $@"LEFT JOIN ESUS_FAMILIA D ON(D.ID = D.ID_FAMILIA)";
                else
                    sql_estrutura = $@"LEFT JOIN ESUS_CADDOMICILIAR D ON PAC.ID_ESUS_CADDOMICILIAR = D.ID";

                Cidadao model = _cidadaoRepository.GetCidadaoById(ibge, id, sql_estrutura);
                return Ok(model);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPost("ValidaExistencia")]
        [ParameterTypeFilter("inserir")]
        public ActionResult<RetornoValidaCidadao> ValidaExistencia([FromHeader] string ibge, [FromBody] Cidadao model)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                #region Valida se cidadão já existe
                string filtro = string.Empty;
                if (!string.IsNullOrWhiteSpace(model.csi_cpfpac))
                    filtro += $@"( PAC.CSI_CPFPAC LIKE '%{model.csi_cpfpac}%'";

                if (!string.IsNullOrWhiteSpace(filtro) && !string.IsNullOrWhiteSpace(model.csi_idepac))
                    filtro += $@" OR PAC.CSI_IDEPAC LIKE '%{model.csi_idepac}%'";
                else if (string.IsNullOrWhiteSpace(filtro) && !string.IsNullOrWhiteSpace(model.csi_idepac))
                    filtro += $@"( PAC.CSI_IDEPAC LIKE '%{model.csi_idepac}%'";

                if (!string.IsNullOrWhiteSpace(filtro) && !string.IsNullOrWhiteSpace(model.csi_ncartao))
                    filtro += $@" OR PAC.CSI_CARTAO LIKE '%{model.csi_ncartao}%'";
                else if (string.IsNullOrWhiteSpace(filtro) && !string.IsNullOrWhiteSpace(model.csi_ncartao))
                    filtro += $@"( PAC.CSI_CARTAO LIKE '%{model.csi_ncartao}%'";

                if (model.csi_dtnasc != null && !string.IsNullOrWhiteSpace(filtro))
                    filtro += $@") OR PAC.CSI_DTNASC = '{model.csi_dtnasc?.ToString("dd.MM.yyyy")}'";
                else if (string.IsNullOrWhiteSpace(filtro) && !string.IsNullOrWhiteSpace(model.csi_ncartao))
                    filtro += $@" PAC.CSI_DTNASC = '{model.csi_dtnasc?.ToString("dd.MM.yyyy")}'";

                if (!string.IsNullOrWhiteSpace(model.csi_nompac) && !string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" (SELECT UPPER(RETORNO) FROM TIRA_ACENTOS(PAC.CSI_NOMPAC)) LIKE '%{Helper.RemoveAcentos(model.csi_nompac.ToUpper())}%'";
                else if (string.IsNullOrWhiteSpace(filtro) && !string.IsNullOrWhiteSpace(model.csi_ncartao))
                    filtro += $@" (SELECT UPPER(RETORNO) FROM TIRA_ACENTOS(PAC.CSI_NOMPAC)) LIKE '%{Helper.RemoveAcentos(model.csi_nompac.ToUpper())}%'";

                filtro = " WHERE " + filtro;

                var retorno = _cidadaoRepository.ValidaExistenciaCidadaoParam(ibge, filtro);

                //caso esteja editando o registro
                if (model.csi_codpac != null)
                    retorno = retorno.Where(x => x.csi_codpac != model.csi_codpac).ToList();

                RetornoValidaCidadao item = new RetornoValidaCidadao();
                item.cidadao = retorno;
                var possuicpfcns = retorno.Where(x => x.csi_ncartao == null ?
                                                      x.csi_cpfpac == model.csi_cpfpac :
                                                      x.csi_ncartao == model.csi_ncartao)
                                          .ToList();
                if (possuicpfcns.Count > 0)
                    item.permiteEnviar = false;
                else
                    item.permiteEnviar = true;

                #endregion

                return Ok(item);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPost("Inserir")]
        [Route("{ibge}")]
        [ParameterTypeFilter("inserir")]
        public ActionResult<Cidadao> Inserir([FromHeader]string ibge, [FromBody] Cidadao model)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));

                #region Valida se cidadão já existe
                string filtro = string.Empty;
                if (!string.IsNullOrWhiteSpace(model.csi_cpfpac))
                    filtro += $@" PAC.CSI_CPFPAC LIKE '%{model.csi_cpfpac}%'";

                if (!string.IsNullOrWhiteSpace(filtro) && !string.IsNullOrWhiteSpace(model.csi_idepac))
                    filtro += $@" OR PAC.CSI_IDEPAC LIKE '%{model.csi_idepac}%'";
                else if (string.IsNullOrWhiteSpace(filtro) && !string.IsNullOrWhiteSpace(model.csi_idepac))
                    filtro += $@" PAC.CSI_IDEPAC LIKE '%{model.csi_idepac}%'";

                if (!string.IsNullOrWhiteSpace(filtro) && !string.IsNullOrWhiteSpace(model.csi_ncartao))
                    filtro += $@" OR PAC.CSI_CARTAO LIKE '%{model.csi_ncartao}%'";
                else if (string.IsNullOrWhiteSpace(filtro) && !string.IsNullOrWhiteSpace(model.csi_ncartao))
                    filtro += $@" PAC.CSI_CARTAO LIKE '%{model.csi_ncartao}%'";

                filtro = " WHERE " + filtro;

                var retorno = _cidadaoRepository.ValidaExistenciaCidadao(ibge, filtro);

                if (retorno.Item1)
                    throw new Exception($"Cidadão já se encontra cadastrado. Cidadão {retorno.Item2} - {retorno.Item3}");
                #endregion

                var id = _cidadaoRepository.GetNewId(ibge);
                model.csi_codpac = id;
                _cidadaoRepository.Insert(model, ibge);

                return Ok(model);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPut("Editar/{id}")]
        [Route("{ibge}")]
        [ParameterTypeFilter("editar")]
        public ActionResult<Cidadao> Editar([FromHeader]string ibge, [FromBody] Cidadao model, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                model.csi_codpac = id;
                _cidadaoRepository.Update(model, ibge);
                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPut("Excluir/{id}")]
        [Route("{ibge}")]
        [ParameterTypeFilter("Excluir")]
        public ActionResult<Cidadao> Excluir([FromHeader]string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                _cidadaoRepository.Excluir(ibge, id);
                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetFotoByCidadao/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult GetFotoByCidadao([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnectionFoto(ibge));
                var foto = _cidadaoRepository.GetFotoByCidadao(ibge, id);
                var fotobase = new
                {
                    fotobase64 = foto != null ? Convert.ToBase64String(foto) : string.Empty
                };

                return Ok(fotobase);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        #region GRAU PARENTESCO
        [HttpGet("GetGrauParentesco")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<GrauParentesco>> GetGrauParentesco([FromHeader] string ibge)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<GrauParentesco> lista = _cidadaoRepository.GetGrauParentesco(ibge);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }
        #endregion

        #region CADASTRO INDIVIDUAL
        [HttpPost("InserirCadIndividual")]
        [ParameterTypeFilter("incluir_individuo")]
        public ActionResult InserirCadIndividual([FromHeader] string ibge, [FromBody] Cidadao model)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                model.csi_codpac = _cidadaoRepository.GetNewId(ibge); //recupera novo id
                _cidadaoRepository.InsertCadIndividual(ibge, model);
                return Ok(model);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPut("AtualizarCadIndividual/{id}")]
        [ParameterTypeFilter("editar_individuo")]
        public ActionResult AtualizarCadIndividual([FromHeader] string ibge, [FromBody] Cidadao model, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                model.csi_codpac = id;
                _cidadaoRepository.UpdateCadIndividual(ibge, model);
                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }
        #endregion

        #region Consulta CadSus
        [HttpGet("ConsultaDadosCidadao")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<PacientesCNSViewModel>> ConsultaDadosCidadao([FromHeader]string ibge, string nome, string dataNascimento, string cns, string sexo, string nomemae)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                var loginCadSus = _cidadaoRepository.GetLoginCadSus(ibge);
                var consulta = new ConsultaCadSus();
                List<PacientesCNSViewModel> retorno = consulta.ConsultaDadosCidadao(nome, cns, sexo, dataNascimento, nomemae, loginCadSus.Item1, loginCadSus.Item2);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }
        #endregion

        [HttpGet("GetIndividuosToFamilia")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<CidadaoFamiliaViewModel>> GetIndividuosToFamilia([FromHeader]string ibge, int page, int pagesize, string search, string fields)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                string filtro = string.Empty;

                if (!string.IsNullOrWhiteSpace(search))
                {
                    if (fields != null && fields.Split(",").Length > 0 && fields.Split(",")[0] != null)
                    {
                        filtro += Helper.GetFiltroInicial(fields, search);
                    }
                    else
                    {
                        var stringcod = string.Empty;
                        if (Helper.soContemNumeros(search))
                            stringcod = $" csi_codpac = {search} OR ";

                        filtro += $@" WHERE ( {stringcod}
                                             (SELECT UPPER(RETORNO) FROM TIRA_ACENTOS(C.CSI_NOMPAC)) LIKE '%{Helper.RemoveAcentos(search.ToUpper())}%')";
                    }
                }

                int count = _cidadaoRepository.GetCountIndividuosToFamilia(ibge, filtro);
                if (count == 1)
                    page = 0;
                else
                    page = page * pagesize;

                Response.Headers.Add("X-Total-Count", count.ToString());
                List<CidadaoFamiliaViewModel> lista = _cidadaoRepository.GetIndividuosToFamilia(ibge, page, pagesize, filtro);

                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }


        [HttpGet("GetCidadaoByIdProntuarioIdAgente")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<CidadaoViewModel>> GetCidadaoByIdProntuarioIdAgente([FromHeader]string ibge, [FromQuery] int? id_prontuario, int id_agente)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<CidadaoViewModel> lista = _cidadaoRepository.GetCidadaoByIdProntuarioIdAgente(ibge, id_prontuario, id_agente);

                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

    }
}