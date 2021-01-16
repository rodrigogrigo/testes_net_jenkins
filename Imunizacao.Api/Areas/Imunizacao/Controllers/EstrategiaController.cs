using System;
using System.Net;
using RgCidadao.Api.Helpers;
using RgCidadao.Domain.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RgCidadao.Domain.Repositories.Imunizacao;
using Microsoft.Extensions.Configuration;
using RgCidadao.Api.Filters;
using System.Collections.Generic;
using RgCidadao.Domain.Entities.Imunizacao;

namespace Imunizacao.Api.Controllers
{
    [Route("api/Imunizacao/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("Imunizacao")]
    public class EstrategiaController : ControllerBase
    {
        private readonly IEstrategiaRepository _estrategiaRepository;
        private IConfiguration _config;
        public EstrategiaController(IEstrategiaRepository estrategiarepository, IConfiguration configuration)
        {
            _estrategiaRepository = estrategiarepository;
            _config = configuration;
        }

        [HttpGet("GetEstrategiaByProduto/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<Estrategia>> GetEstrategiaByProduto([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<Estrategia> lista = _estrategiaRepository.GetEstrategiasByProduto(ibge, id);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetAll")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult GetAll([FromHeader] string ibge)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<Estrategia> lista = _estrategiaRepository.GetAll(ibge);
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