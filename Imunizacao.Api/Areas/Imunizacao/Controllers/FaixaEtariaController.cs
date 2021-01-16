using System;
using System.Net;
using RgCidadao.Api.Helpers;
using RgCidadao.Domain.Entities;
using RgCidadao.Domain.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RgCidadao.Domain.Repositories.Imunizacao;
using RgCidadao.Domain.Entities.Imunizacao;
using Microsoft.Extensions.Configuration;
using RgCidadao.Api.Filters;
using System.Collections.Generic;

namespace Imunizacao.Api.Controllers
{
    [Route("api/Imunizacao/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("Imunizacao")]
    public class FaixaEtariaController : ControllerBase
    {
        public IFaixaEtariaRepository _faixaRepository;
        private IConfiguration _config;
        public FaixaEtariaController(IFaixaEtariaRepository _repository, IConfiguration configuration)
        {
            _faixaRepository = _repository;
            _config = configuration;
        }

        [HttpGet("GetAll")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<FaixaEtaria>> GetAll([FromHeader] string ibge)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<FaixaEtaria> lista = _faixaRepository.GetAll(ibge);

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