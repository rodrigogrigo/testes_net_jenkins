using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RgCidadao.Api.Filters;
using RgCidadao.Api.Helpers;
using RgCidadao.Api.ViewModels.AtencaoBasica;
using RgCidadao.Domain.Entities.AtencaoBasica;
using RgCidadao.Domain.Repositories.AtencaoBasica;
using RgCidadao.Domain.Repositories.Cadastro;
using RgCidadao.Domain.ViewModels.AtencaoBasica;

namespace RgCidadao.Api.Areas.AtencaoBasica.Controllers
{
    [Route("api/AtencaoBasica/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("AtencaoBasica")]
    public class FamiliaController : ControllerBase
    {
        public IFamiliaRepository _repository;
        public IEstabelecimentoRepository _estabelecimentorepository;
        public ICidadaoRepository _cidadaorepository;
        private IConfiguration _config;
        public FamiliaController(IFamiliaRepository repository, IConfiguration configuration, IEstabelecimentoRepository estabelecimentorepository, ICidadaoRepository cidadaorepository)
        {
            _repository = repository;
            _config = configuration;
            _estabelecimentorepository = estabelecimentorepository;
            _cidadaorepository = cidadaorepository;
        }

        [HttpPost("Inserir")]
        [ParameterTypeFilter("inserir")]
        public ActionResult Inserir([FromHeader] string ibge, [FromBody] Familia model)
        {
            try
            {
                if (model.id_responsavel == null)
                    return BadRequest(TrataErro.GetResponse("O prenchimento de um responsável para a família é obrigatório!", true));

                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                if (model.id_domicilio != null)
                {
                    var domicio = _estabelecimentorepository.GetEstabelecimentoById(ibge, (int)model.id_domicilio);
                    if (domicio.zona == 0)
                        model.area_prod_rural = null;
                    else if (domicio.zona == 1)
                    {
                        if (domicio.tipo_imovel == 6 || domicio.tipo_imovel == 7 || domicio.tipo_imovel == 8 || domicio.tipo_imovel == 9 || domicio.tipo_imovel == 10)
                            model.area_prod_rural = null;
                    }
                }

                model.id = _repository.GetNewId(ibge);
                _repository.Insert(ibge, model);
                _repository.AtualizaCadPacFamilia(ibge, (int)model.id, (int)model.id_responsavel);

                return Ok(model);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPut("Editar/{id}")]
        [ParameterTypeFilter("editar")]
        public ActionResult Editar([FromHeader] string ibge, [FromBody] Familia model, [FromRoute] int id)
        {
            try
            {
                if (model.id_responsavel == null)
                    return BadRequest(TrataErro.GetResponse("O prenchimento de um responsável para a família é obrigatório!", true));

                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                model.id = id;
                _repository.Update(ibge, model);
                _repository.AtualizaCadPacFamilia(ibge, id, (int)model.id_responsavel);

                return Ok(model);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetFamiliaById/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<Familia> GetFamiliaById([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                Familia item = _repository.GetFamiliaById(ibge, id);
                item.domicilio = _estabelecimentorepository.GetEstabelecimentoById(ibge, (int)item.id_domicilio);
                return Ok(item);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetFamiliaProntuarioQtde/{id_profissional}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<FamiliaProntuarioQtdeViewModel> GetFamiliaProntuarioQtde([FromHeader] string ibge, [FromRoute] int id_profissional)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                FamiliaProntuarioQtdeViewModel itens = _repository.FamiliaProntuarioQtde(ibge, id_profissional);

                return Ok(itens);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPut("AtualizaResponsavelFamilia/{id_responsavel:int}/{id_familia:int}")]
        [ParameterTypeFilter("editar")]
        public ActionResult AtualizaResponsavelFamilia([FromHeader] string ibge, int? id_responsavel, int id_familia)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                var individuo = _repository.GetIndividuoFamilia(ibge, (int)id_responsavel);
                if (id_familia != individuo)
                    return BadRequest(TrataErro.GetResponse("O indivíduo não pertence a família informada.", true));

                string sql_estrutura = string.Empty;
                if (_cidadaorepository.VerificaExisteEsusFamilia(ibge))
                    sql_estrutura = $@"LEFT JOIN ESUS_FAMILIA D ON(D.ID = D.ID_FAMILIA)";
                else
                    sql_estrutura = $@"LEFT JOIN ESUS_CADDOMICILIAR D ON PAC.ID_ESUS_CADDOMICILIAR = D.ID";

                var paciente = _cidadaorepository.GetCidadaoById(ibge, (int)id_responsavel, sql_estrutura);
                if (string.IsNullOrWhiteSpace(paciente.csi_ncartao) && string.IsNullOrWhiteSpace(paciente.csi_cpfpac))
                    return BadRequest(TrataErro.GetResponse("Para ser responsável pela Família, o indivíduo precisa ter CPF e/ou CNS preenchidos.", true));

                _repository.AtualizaResponsavelFamilia(ibge, id_familia, (int)id_responsavel);
                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPut("AtualizaFamiliaOutraArea/{id_familia}")]
        [ParameterTypeFilter("editar")]
        public ActionResult AtualizaFamiliaOutraArea([FromHeader] string ibge, [FromRoute] int? id_familia)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                _repository.AtualizaFamiliaOutraArea(ibge, (int)id_familia);
                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPut("AtualizaFamiliaDomicilio/{id_familia}")]
        [ParameterTypeFilter("editar")]
        public ActionResult AtualizaFamiliaDomicilio([FromHeader] string ibge, [FromRoute]int? id_familia, [FromBody] FamiliaParamViewModel model)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                _repository.AtualizaFamiliaDomicilio(ibge, (int)id_familia, (int)model.id_domicilio);
                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPut("DesvincularIndividuo/{id_individuo}")]
        [ParameterTypeFilter("editar")]
        public ActionResult DesvincularIndividuo([FromHeader] string ibge, [FromRoute] int id_individuo)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                var familia = _repository.GetFamiliaByIndividuoResponsavel(ibge, id_individuo);
                if (familia.Count != 0)
                    return BadRequest(TrataErro.GetResponse("O Responsável pela família não pode ser desvinculado!", true));

                _repository.AtualizaCadPacFamilia(ibge, null, id_individuo);
                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPut("VincularFamiliaIndividuo/{id_individuo}")]
        [ParameterTypeFilter("editar")]
        public ActionResult VincularFamiliaIndividuo([FromHeader] string ibge, [FromRoute] int id_individuo, [FromBody] FamiliaParamViewModel model)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                _repository.AtualizaCadPacFamilia(ibge, model.id_familia, id_individuo);
                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPut("VincularFamiliaDomicilio/{id_familia}")]
        [ParameterTypeFilter("editar")]
        public ActionResult VincularFamiliaDomicilio([FromHeader] string ibge, [FromRoute] int id_familia, [FromBody] FamiliaParamViewModel model)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                _repository.AtualizaFamiliaDomicilio(ibge, id_familia, (int)model.id_domicilio);
                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

    }
}