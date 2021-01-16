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
using System;
using System.Collections.Generic;
using System.Net;

namespace RgCidadao.Api.Areas.AtencaoBasica.Controllers
{
    [Route("api/AtencaoBasica/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("AtencaoBasica")]
    public class EstabelecimentoController : ControllerBase
    {
        public IEstabelecimentoRepository _repository;
        private IConfiguration _config;
        public EstabelecimentoController(IEstabelecimentoRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _config = configuration;
        }

        [HttpPost("Inserir")]
        [ParameterTypeFilter("inserir")]
        public ActionResult Inserir([FromHeader] string ibge, [FromBody] Estabelecimento model)
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
        public ActionResult Editar([FromHeader] string ibge, [FromBody] Estabelecimento model, [FromRoute] int id)
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

        [HttpGet("GetEstabelecimentoById/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<Estabelecimento> GetEstabelecimentoById([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                Estabelecimento item = _repository.GetEstabelecimentoById(ibge, id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetEstabelecimentosByArea")]
        public ActionResult GetEstabelecimentosByArea([FromHeader] string ibge, int page,
                                                          int pagesize, string search, string fields, int microarea)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                string filtro = string.Empty;

                if (!string.IsNullOrWhiteSpace(search))
                {
                    if (fields != null && fields.Split(",").Length > 0 && fields.Split(",")[0] != null)
                        filtro += Helper.GetFiltro(fields, search);
                    else
                    {
                        var filtronumero = string.Empty;
                        if (Helper.soContemNumeros(search))
                            filtronumero = $@" EST.ID = {search} OR ";

                        filtro += $@" AND( {filtronumero}
                                          EST.NUMERO_LOGRADOURO CONTAINING '{search}' OR 
                                          LOG.CSI_NOMEND CONTAINING '{search}' OR
                                          BAI.CSI_NOMBAI CONTAINING '{search}' OR
                                          CID.CSI_NOMCID CONTAINING '{search}' OR
                                          CID.CSI_SIGEST CONTAINING '{search}')";
                    }
                }

                int count = _repository.GetCountEstabelecimentosByArea(ibge, filtro, microarea);
                if (count == 1)
                    page = 0;
                else
                    page = page * pagesize;

                Response.Headers.Add("X-Total-Count", count.ToString());
                List<EstabelecimentoViewModel> lista = _repository.GetEstabelecimentosByArea(ibge, page, pagesize, filtro, microarea);
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