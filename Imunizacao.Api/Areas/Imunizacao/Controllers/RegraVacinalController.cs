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

namespace RgCidadao.Api.Controllers
{
    [Route("api/Imunizacao/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("Imunizacao")]
    public class RegraVacinalController : ControllerBase
    {
        public IRegraVacinalRepository _command;
        private IConfiguration _config;
        public RegraVacinalController(IRegraVacinalRepository command, IConfiguration configuration)
        {
            _command = command;
            _config = configuration;
        }

        [HttpGet("GetRegraVacinalByParams")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<RegraVacinal> GetRegraVacinalByParams([FromHeader] string ibge, int id_imunobiologico, int id_estrategia, int id_dose)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                RegraVacinal item = _command.GetRegraVacinalByParams(ibge, id_imunobiologico, id_estrategia, id_dose);

                if (item != null)
                    return Ok(item);
                else
                    return BadRequest("Regra vacinal não cadastrada");
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }
    }
}