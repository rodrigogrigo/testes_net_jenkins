using System;
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
using Microsoft.Extensions.Configuration;
using RgCidadao.Api.Filters;
using System.Collections.Generic;

namespace RgCidadao.Api.Controllers
{
    [Route("api/Cadastro/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("Cadastro")]
    public class PaisController : ControllerBase
    {
        private readonly IPaisRepository _paisRepository;
        private IConfiguration _config;
        public PaisController(IPaisRepository paisrepository, IConfiguration configuration)
        {
            _paisRepository = paisrepository;
            _config = configuration;
        }

        [HttpGet("GetAll")]
        [Route("{ibge}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<Pais>> GetAll([FromHeader] string ibge)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<Pais> itens = _paisRepository.GetAll(ibge);
                return Ok(itens);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }
    }
}