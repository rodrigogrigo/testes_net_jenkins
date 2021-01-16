using System;
using System.Collections.Generic;
using System.Net;
using RgCidadao.Api.Helpers;
using RgCidadao.Domain.Entities;
using RgCidadao.Domain.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RgCidadao.Domain.Repositories.Cadastro;
using RgCidadao.Domain.Entities.Cadastro;
using RgCidadao.Domain.Entities.Imunizacao;
using RgCidadao.Domain.ViewModels.Cadastros;
using Microsoft.Extensions.Configuration;
using RgCidadao.Api.Filters;
using RgCidadao.Api.ViewModels.Cadastro;
using System.Linq;

namespace RgCidadao.Api.Controllers
{
    [Route("api/Cadastro/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("Cadastro")]
    public class ProfissionalController : ControllerBase
    {
        private readonly IProfissionalRepository _profissionalRepository;
        private IConfiguration _config;
        public ProfissionalController(IProfissionalRepository profissionalrepository, IConfiguration configuration)
        {
            _profissionalRepository = profissionalrepository;
            _config = configuration;
        }

        [HttpGet("{ibge, filtro}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<ProfissionalViewModel>> GetAll([FromHeader]string ibge)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                string filtro = string.Empty;
                List<ProfissionalViewModel> lista = _profissionalRepository.GetAll(ibge, filtro);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetProfissionalByUnidade/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<ProfissionalViewModel>> GetProfissionalByUnidade([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<ProfissionalViewModel> lista = _profissionalRepository.GetProfissionalByUnidade(ibge, id);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetProfissionalAtivoByUnidade/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<ProfissionalViewModel>> GetProfissionalAtivoByUnidade([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<ProfissionalViewModel> lista = _profissionalRepository.GetProfissionalAtivoByUnidade(ibge, id);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetCboByProfissional/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<CBO>> GetCboByProfissional([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<CBO> lista = _profissionalRepository.GetCboProfissional(ibge, id);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetProfissionalCboByUnidade/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<ProfissionalViewModel>> GetProfissionalCboByUnidade([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<ProfissionalViewModel> lista = _profissionalRepository.GetProfissionalCBOByUnidade(ibge, id);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetProfissionalByUnidades")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<ProfissionalViewModel> GetProfissionalByUnidades([FromHeader] string ibge, string id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<ProfissionalViewModel> lista = new List<ProfissionalViewModel>();
                if (!string.IsNullOrWhiteSpace(id))
                    lista = _profissionalRepository.GetProfissionalByUnidades(ibge, id);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetFiltroAvancado")]
        public ActionResult<List<ProfissionalViewModel>> GetFiltroAvancado([FromHeader]string ibge, [FromQuery] ParametersProfissional model)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                string filtro = string.Empty;
                if (model.codigo != null)
                    filtro += $@" MED.CSI_CODMED = {model.codigo}";

                if (!string.IsNullOrWhiteSpace(model.nome) && !string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" AND MED.CSI_NOMMED CONTAINING '{model.nome}'";
                else if (!string.IsNullOrWhiteSpace(model.nome) && string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" MED.CSI_NOMMED CONTAINING '{model.nome}'";

                if (!string.IsNullOrWhiteSpace(model.cbo) && !string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" AND MED.CSI_CBO CONTAINING '{model.cbo}'";
                else if (!string.IsNullOrWhiteSpace(model.cbo) && string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" MED.CSI_CBO CONTAINING '{model.cbo}'";

                if (model.unidade != null && !string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" AND MED_UNI.CSI_CODUNI = {model.unidade}";
                else if (model.unidade != null && string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" MED_UNI.CSI_CODUNI = {model.unidade}";

                if (model.cpf != null && !string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" AND MED.CSI_CPF CONTAINING '{model.cpf}'";
                else if (model.cpf != null && string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" MED.CSI_CPF CONTAINING '{model.cpf}'";

                if (!string.IsNullOrWhiteSpace(filtro))
                    filtro = $@" WHERE {filtro}";

                int count = _profissionalRepository.GetCountAll(ibge, filtro);
                if (count == 1)
                    model.page = 0;
                else
                    model.page = model.page * model.pagesize;

                Response.Headers.Add("X-Total-Count", count.ToString());
                List<ProfissionalViewModel> lista = _profissionalRepository.GetAllPagination(ibge, (int)model.page, (int)model.pagesize, filtro);

                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetProfissionalByIdAndUnidade")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<ProfissionalViewModel>> GetProfissionalByIdAndUnidade([FromHeader] string ibge, int profissional, int unidade)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<ProfissionalViewModel> lista = _profissionalRepository.GetProfissionalByIdAndUnidade(ibge, unidade, profissional);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetProfissionalByUnidadeWithCBO/{unidade}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<ProfissionalViewModel> GetProfissionalByUnidadeWithCBO([FromHeader] string ibge, [FromRoute]int unidade)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<ProfissionalViewModel> lista = _profissionalRepository.GetProfissionalByUnidadeWithCBO(ibge, unidade);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetAllPaginationProfissionalWithCBO")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<ProfissionalViewModel>> GetAllPaginationProfissionalWithCBO([FromHeader]string ibge, int page, int pagesize, string search, string fields)
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
                        string filtronum = string.Empty;
                        if (Helper.soContemNumeros(search))
                            filtronum = $@" CAST(MED.CSI_CODMED AS VARCHAR(20)) CONTAINING '{search}' OR";

                        filtro += $@" AND( {filtronum}
                                          MED.CSI_NOMMED CONTAINING '{search}')";
                    }
                }

                int count = _profissionalRepository.GetCountAllProfissionalWithCBO(ibge, filtro);
                if (count == 1)
                    page = 0;
                else
                    page = page * pagesize;

                List<ProfissionalViewModel> lista = _profissionalRepository.GetAllPaginationProfissionalWithCBO(ibge, page, pagesize, filtro);
                Response.Headers.Add("X-Total-Count", count.ToString());
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }


        [HttpGet("GetProfissionalById/{profissional}")] /*6485/005*/
        [ParameterTypeFilter("visualizar")]
        public ActionResult<Profissional> GetProfissionalById([FromHeader] string ibge, [FromRoute]int profissional)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                Profissional lista = _profissionalRepository.GetProfissionalById(ibge, profissional);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetProfissionalByEquipe/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<ProfissionalViewModel> GetProfissionalByEquipe([FromHeader] string ibge, [FromRoute] string id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<ProfissionalViewModel> lista = new List<ProfissionalViewModel>();
                if (!string.IsNullOrWhiteSpace(id))
                    lista = _profissionalRepository.GetProfissionalByEquipe(ibge, id);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetACSByEstabelecimentoSaude/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<ACSViewModel>> GetACSByEstabelecimentoSaude([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<ACSViewModel> agentes_saude = _profissionalRepository.GetACSByEstabelecimentoSaude(ibge, id);
                return Ok(agentes_saude);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        #region Agendamento de Consultas
        [HttpGet("GetCBOByMedicoUnidade")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<ProfissionalViewModel>> GetCBOByMedicoUnidade([FromHeader] string ibge, int codmed, int coduni, string cbo = "")
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<ProfissionalViewModel> cbos = _profissionalRepository.GetCBOByMedicoUnidade(ibge, codmed, coduni, cbo);
                return Ok(cbos);
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