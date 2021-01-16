using System;
using System.Collections.Generic;
using System.Net;
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
    public class ConsumoAlimentarController : ControllerBase
    {
        public IConsumoAlimentarRepository _repository;
        private IConfiguration _config;
        public ConsumoAlimentarController(IConsumoAlimentarRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _config = configuration;
        }

        [HttpGet("GetAllPagination")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<ConsumoAlimentarViewModel>> GetAllPagination([FromHeader]string ibge, int page, int pagesize, string search, string fields, 
               DateTime? data_inicial, DateTime? data_final)
       {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                string filtro = string.Empty;

                if (!string.IsNullOrWhiteSpace(search))
                {
                    if (fields != null && fields.Split(",").Length > 0 && fields.Split(",")[0] != null)
                        filtro += Helper.GetFiltroInicial(fields, search);
                    else
                    {
                        var filtronumero = string.Empty;
                        if (Helper.soContemNumeros(search))
                            filtronumero = $@" ECA.ID = {search} OR
                                               ECA.ID_CIDADAO = {search} OR";

                        filtro += $@" ( {filtronumero}
                                          M.CSI_CBO CONTAINING '{search}' OR
                                          EE.DESCRICAO CONTAINING '{search}')";
                    }
                }

                if (data_final == null)
                    data_final = DateTime.Now;
                
                if(data_inicial != null)
                {
                    filtro = !string.IsNullOrWhiteSpace(filtro) 
                        ? $@" AND ECA.DATA_ATENDIMENTO BETWEEN '{data_inicial?.ToString("dd.MM.yyyy")}' AND '{data_final?.ToString("dd.MM.yyyy")}'" 
                        : $@" ECA.DATA_ATENDIMENTO BETWEEN '{data_inicial?.ToString("dd.MM.yyyy")}' AND '{data_final?.ToString("dd.MM.yyyy")}'";
                }

                if(data_inicial == null)
                {
                    filtro = !string.IsNullOrWhiteSpace(filtro) 
                        ? $@" AND ECA.DATA_ATENDIMENTO <= '{data_final?.ToString("dd.MM.yyyy")}'"
                        : $@" ECA.DATA_ATENDIMENTO <= '{data_final?.ToString("dd.MM.yyyy")}'";
                }

                if (!string.IsNullOrWhiteSpace(filtro))
                    filtro = " WHERE " + filtro;

                int count = _repository.GetCountAll(ibge, filtro);
                if (count == 1)
                    page = 0;
                else
                    page = page * pagesize;

                Response.Headers.Add("X-Total-Count", count.ToString());
                List<ConsumoAlimentarViewModel> lista = _repository.GetAllPagination(ibge, page, pagesize, filtro);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPost("Inserir")]
        [ParameterTypeFilter("inserir")]
        public ActionResult Inserir([FromHeader] string ibge, [FromBody] ConsumoAlimentar model)
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
        [ParameterTypeFilter("editar")]
        public ActionResult Editar([FromHeader] string ibge, [FromBody] ConsumoAlimentar model, [FromRoute] int id)
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

        [HttpGet("GetConsumoAlimentarById/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<ConsumoAlimentar> GetConsumoAlimentarById([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                ConsumoAlimentar item = _repository.GetConsumoAlimentarById(ibge, id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpDelete("Excluir/{id}")]
        [ParameterTypeFilter("excluir")]
        public ActionResult Excluir([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
               _repository.Delete(ibge, id);
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