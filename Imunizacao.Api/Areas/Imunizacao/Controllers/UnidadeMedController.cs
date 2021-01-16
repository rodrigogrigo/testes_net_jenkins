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
using RgCidadao.Domain.Entities.Imunizacao;
using RgCidadao.Domain.Repositories.Imunizacao;

namespace RgCidadao.Api.Areas.Imunizacao.Controllers
{
    [Route("api/Imunizacao/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("Imunizacao")]
    public class UnidadeMedController : ControllerBase
    {
        private readonly IUnidadeMedRepository _unidademedRepository;
        private IConfiguration _config;
        public UnidadeMedController(IUnidadeMedRepository unidademedrepository, IConfiguration configuration)
        {
            _unidademedRepository = unidademedrepository;
            _config = configuration;
        }
        [HttpGet("GetAll")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<UnidadeMedida>> GetAll([FromHeader] string ibge)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<UnidadeMedida> itens = _unidademedRepository.GetAll(ibge);
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