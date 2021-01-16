using RgCidadao.Domain.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RgCidadao.Domain.Repositories.Imunizacao;
using Microsoft.Extensions.Configuration;
using RgCidadao.Domain.Entities.Imunizacao;
using RgCidadao.Api.Helpers;
using System.Net;
using System.Collections.Generic;
using RgCidadao.Api.Filters;

namespace RgCidadao.Api.Controllers
{
    [Route("api/Imunizacao/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("Imunizacao")]
    public class ImunobiologicoController : ControllerBase
    {
        private readonly IImunobiologicoRepository _imunobiologicoRepository;
        private IConfiguration _config;

        public ImunobiologicoController(IImunobiologicoRepository imunorepository, IConfiguration configuration)
        {
            _imunobiologicoRepository = imunorepository;
            _config = configuration;
        }

        [HttpGet("GetAll")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<Imunobiologico>> GetAll([FromHeader] string ibge)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<Imunobiologico> itens = _imunobiologicoRepository.GetAllImunobiologico(ibge);
                return Ok(itens);
            }
            catch (System.Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }
    }
}