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
using RgCidadao.Domain.Entities.AtencaoBasica;
using RgCidadao.Domain.Repositories.AtencaoBasica;
using RgCidadao.Domain.ViewModels.AtencaoBasica;

namespace RgCidadao.Api.Areas.AtencaoBasica.Controllers
{
    [Route("api/AtencaoBasica/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("AtencaoBasica")]
    public class VisitaDomiciliarController : ControllerBase
    {
        private IConfiguration _config;
        private IVisitaDomiciliarRepository _repository;
        public VisitaDomiciliarController(IConfiguration config, IVisitaDomiciliarRepository repository)
        {
            _repository = repository;
            _config = config;
        }

        [HttpGet("GetAllPagination")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<VisitaDomiciliarViewModel>> GetAllPagination([FromHeader]string ibge, int page, int pagesize,
                                                             int? unidade, int? agente, DateTime? datainicial, DateTime? datafinal)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                string filtro = string.Empty;


                if (unidade != null) {
                    filtro += $@" VD.ID_UNIDADE IN ({unidade})";
                }

                if ((agente != null) && !string.IsNullOrWhiteSpace(filtro)) {
                    filtro += $@" AND VD.ID_PROFISSIONAL IN ({agente})";
                }
                else if ((agente != null) && string.IsNullOrWhiteSpace(filtro))
                {
                    filtro += $@" VD.ID_PROFISSIONAL IN ({agente})";
                }

                if (datafinal == null) {
                    datafinal = DateTime.Now;
                }


                if (datainicial != null && !string.IsNullOrWhiteSpace(filtro))
                {
                    filtro += $@" AND CAST(VD.DATA_VISITA AS DATE) BETWEEN '{datainicial?.ToString("dd.MM.yyyy")}' AND '{datafinal?.ToString("dd.MM.yyyy")}' ";
                }
                else if (datainicial != null && string.IsNullOrWhiteSpace(filtro))
                {
                    filtro = $@" CAST(VD.DATA_VISITA AS DATE) BETWEEN '{datainicial?.ToString("dd.MM.yyyy")}' AND '{datafinal?.ToString("dd.MM.yyyy")}' ";
                }

                if (!string.IsNullOrWhiteSpace(filtro)) {
                    filtro = $@" WHERE {filtro}";
                }


                int count = _repository.GetCountAll(ibge, filtro);

                if (count == 1)
                    page = 0;
                else
                    page = page * pagesize;

                Response.Headers.Add("X-Total-Count", count.ToString());
                List<VisitaDomiciliarViewModel> lista = _repository.GetAllPagination(ibge, page, pagesize, filtro);
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
        public ActionResult<List<VisitaDomiciliarViewModel>> GetFiltroAvancado([FromHeader]string ibge, int? page, int? pagesize,
                                                             int? individuo, int? idade_inicial, int? idade_final, int? desfecho,
                                                             string hipertenso, string diabetico, string gestante, string turno)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                string filtro = string.Empty;

                if (individuo != null)
                {
                    filtro = $@" C.CSI_CODPAC = {individuo} ";
                }

                if (idade_inicial != null)
                {
                    if (idade_final == null)
                        idade_final = idade_inicial;

                    
                    if (!string.IsNullOrWhiteSpace(filtro))
                        filtro += $@" AND (SELECT IDADE FROM PRO_CALCULA_IDADE(C.CSI_DTNASC,CURRENT_TIMESTAMP)) BETWEEN {idade_inicial} AND {idade_final}";
                    else
                        filtro = $@" (SELECT IDADE FROM PRO_CALCULA_IDADE(C.CSI_DTNASC,CURRENT_TIMESTAMP)) BETWEEN {idade_inicial} AND {idade_final}";
                }

                if (!string.IsNullOrWhiteSpace(turno))
                {
                    if (!string.IsNullOrWhiteSpace(filtro))
                        filtro += $@" AND VD.TURNO = '{turno}' ";
                    else
                        filtro += $@" VD.TURNO = '{turno}' ";
                }

                if (desfecho != null)
                {
                    if (!string.IsNullOrWhiteSpace(filtro))
                        filtro += $@" AND VD.DESFECHO = {desfecho} ";
                    else
                        filtro += $@" VD.DESFECHO = {desfecho} ";
                }


                if (!string.IsNullOrWhiteSpace(hipertenso))
                {
                    if (!string.IsNullOrWhiteSpace(filtro))
                        filtro += $@" AND VD.MV_HIPERTENCAO = '{hipertenso}' ";
                    else
                        filtro += $@" VD.MV_HIPERTENCAO = '{hipertenso}' ";
                }

                if (!string.IsNullOrWhiteSpace(diabetico))
                {
                    if (!string.IsNullOrWhiteSpace(filtro))
                        filtro += $@" AND VD.MV_DIABETES = '{diabetico}' ";
                    else
                        filtro += $@" VD.MV_DIABETES = '{diabetico}' ";
                }


                if (!string.IsNullOrWhiteSpace(gestante))
                {
                    if (!string.IsNullOrWhiteSpace(filtro))
                        filtro += $@" AND VD.MV_GESTANTE = '{gestante}' ";
                    else
                        filtro += $@" VD.MV_GESTANTE = '{gestante}' ";
                }

                if (!string.IsNullOrWhiteSpace(filtro))
                {
                    filtro = $@" WHERE {filtro}";
                }

                int count = _repository.GetCountAll(ibge, filtro);

                if (count == 1)
                    page = 0;
                else
                    page = page * pagesize;

                Response.Headers.Add("X-Total-Count", count.ToString());


                List<VisitaDomiciliarViewModel> lista = _repository.GetAllPagination(ibge, page, pagesize, filtro);
                return Ok(lista);
            }

            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
    }

        [HttpGet("GetById/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<VisitaDomiciliar> GetById([FromHeader]string ibge, [FromRoute]int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                VisitaDomiciliar Itens = _repository.GetById(ibge, id);
                return Ok(Itens);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }


        [HttpPost("Inserir")]
        [ParameterTypeFilter("Insert")]
        public ActionResult Inserir([FromHeader]string ibge, [FromBody] VisitaDomiciliar model)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                model.id = _repository.GetNewId(ibge);
                _repository.Insert(ibge, model);

                return Ok(model);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPut("Editar/{id}")]
        [ParameterTypeFilter("Editar")]
        public ActionResult Editar([FromHeader]string ibge, [FromBody] VisitaDomiciliar model, [FromRoute]int? id)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                model.id = id;
                _repository.Update(ibge, model);

                return Ok(model);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpDelete("Excluir/{id}")]
        [ParameterTypeFilter("Excluir")]
        public ActionResult Excluir([FromHeader]string ibge, int? Id)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                _repository.Delete(ibge, Id);
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