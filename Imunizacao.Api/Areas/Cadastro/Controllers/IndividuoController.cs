using System;
using System.Net;
using RgCidadao.Api.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RgCidadao.Domain.Repositories.Cadastro;
using RgCidadao.Domain.Entities.Cadastro;
using Microsoft.Extensions.Configuration;
using RgCidadao.Api.Filters;

namespace RgCidadao.Api.Controllers
{
    [Route("api/Cadastro/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("Cadastro")]
    public class IndividuoController : ControllerBase
    {
        private readonly IIndividuoRepository _individuoRepository;
        private readonly ICidadaoRepository _cidadaoRepository;
        private IConfiguration _config;
        public IndividuoController(IIndividuoRepository individuoRepository, IConfiguration configuration, ICidadaoRepository cidadaoRepository)
        {
            _individuoRepository = individuoRepository;
            _config = configuration;
            _cidadaoRepository = cidadaoRepository;
        }

        [HttpPost]
        [ParameterTypeFilter("incluir_individuo")]
        public ActionResult Insert([FromHeader] string ibge, [FromBody] Individuo model)
        {
            try
            {
                string response = CheckIndividuoExiste(ibge, model);

                if (!string.IsNullOrWhiteSpace(response))
                {
                    var message = TrataErro.GetResponse(response, true);
                    return BadRequest(message);
                }

                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));

                model.csi_codpac = _individuoRepository.GetNewId(ibge);
                _individuoRepository.Insert(ibge, model);

                return Ok(model);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPut("{id}")]
        [ParameterTypeFilter("editar_individuo")]
        public ActionResult Update([FromHeader] string ibge, [FromBody] Individuo model, [FromRoute] int id)
        {
            try
            {
                string response = CheckIndividuoExiste(ibge, model);

                if (!string.IsNullOrWhiteSpace(response))
                {
                    var message = TrataErro.GetResponse(response, true);
                    return BadRequest(message);
                }

                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));

                model.csi_codpac = id;
                _individuoRepository.Update(ibge, model);

                return Ok(model);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<Individuo> GetById([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                Individuo model = _individuoRepository.GetById(ibge, id);

                return Ok(model);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("CheckVinculoMicroarea/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<bool> CheckVinculoMicroarea([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                string sql_estrutura = string.Empty;
                if (_cidadaoRepository.VerificaExisteEsusFamilia(ibge))
                    sql_estrutura = $@"JOIN ESUS_FAMILIA FAM ON (CP.ID_FAMILIA = FAM.ID)
                                       JOIN VS_ESTABELECIMENTOS EST ON (FAM.ID_DOMICILIO = EST.ID)";
                else
                    sql_estrutura = $@"JOIN ESUS_CADDOMICILIAR EST ON CP.ID_ESUS_CADDOMICILIAR = EST.ID";

                bool possui_vinculo_microarea = _individuoRepository.CheckVinculoMicroarea(ibge, id, sql_estrutura);

                return Ok(possui_vinculo_microarea);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        #region Validacao

        [HttpPost("CheckIndividuoMesmoNome")]
        [ParameterTypeFilter("incluir_individuo")]
        public dynamic CheckIndividuoMesmoNome([FromHeader] string ibge, [FromBody] Individuo model)
        {
            try
            {
                string response = CheckIndividuoExiste(ibge, model);

                if (!string.IsNullOrWhiteSpace(response))
                {
                    var message = TrataErro.GetResponse(response, true);
                    return BadRequest(message);
                }

                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));

                string filtro = string.Empty;

                if (!string.IsNullOrWhiteSpace(model.csi_nompac))
                    filtro += $@" PAC.CSI_NOMPAC CONTAINING '{model.csi_nompac}'";

                if (!string.IsNullOrWhiteSpace(filtro))
                {
                    filtro += !string.IsNullOrWhiteSpace(filtro)
                        ? $@" AND PAC.CSI_DTNASC = '{model.csi_dtnasc:dd.MM.yyyy}'"
                        : $@" PAC.CSI_DTNASC = '{model.csi_dtnasc:dd.MM.yyyy}'";
                }

                filtro = (model.csi_codpac != null)
                    ? $@" WHERE (PAC.CSI_CODPAC <> {model.csi_codpac}) AND ({filtro})"
                    : $@" WHERE ({filtro})";

                var data = _individuoRepository.CheckIndividuoMesmoNome(ibge, filtro);

                return Ok(data);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        private string CheckIndividuoExiste(string ibge, Individuo model)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));

                string filtro = string.Empty;

                if (!string.IsNullOrWhiteSpace(model.csi_cpfpac))
                    filtro += $@" PAC.CSI_CPFPAC CONTAINING '{model.csi_cpfpac}'";

                if (!string.IsNullOrWhiteSpace(model.csi_idepac))
                {
                    filtro += string.IsNullOrWhiteSpace(filtro)
                        ? $@" PAC.CSI_IDEPAC CONTAINING '{model.csi_idepac}'"
                        : $@" OR PAC.CSI_IDEPAC CONTAINING '{model.csi_idepac}'";
                }

                if (!string.IsNullOrWhiteSpace(model.csi_ncartao))
                {
                    filtro += string.IsNullOrWhiteSpace(filtro)
                        ? $@" PAC.CSI_NCARTAO CONTAINING '{model.csi_ncartao}'"
                        : $@" OR PAC.CSI_NCARTAO CONTAINING '{model.csi_ncartao}'";
                }

                filtro = (model.csi_codpac != null)
                    ? $@" WHERE (PAC.CSI_CODPAC <> {model.csi_codpac}) AND ({filtro})"
                    : $@" WHERE ({filtro})";


                var retorno = _individuoRepository.CheckIndividuoExiste(ibge, filtro);

                return retorno.Item1
                    ? $"Indivíduo já se encontra cadastrado. [{retorno.Item2} - {retorno.Item3}]"
                    : null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Log Eventos

        [HttpGet("GetUltimoLog/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<dynamic> GetUltimoLog([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                var log = _individuoRepository.GetUltimoLog(ibge, id);

                return Ok(log);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        #endregion
    }
}