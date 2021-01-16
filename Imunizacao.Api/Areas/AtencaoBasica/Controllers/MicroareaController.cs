using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RgCidadao.Api.Filters;
using RgCidadao.Api.Helpers;
using RgCidadao.Domain.Repositories.AtencaoBasica;
using RgCidadao.Domain.ViewModels.AtencaoBasica;

namespace RgCidadao.Api.Areas.AtencaoBasica.Controllers
{
    [Route("api/AtencaoBasica/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("AtencaoBasica")]
    public class MicroareaController : Controller
    {
        public IMicroareaRepository _repository;
        private IConfiguration _config;
        public MicroareaController(IMicroareaRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _config = configuration;
        }

        [HttpGet("GetMicroareas")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<MicroareaViewModel>> GetMicroareas([FromHeader] string ibge)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                string filtro = string.Empty;
                List<MicroareaViewModel> lista = _repository.GetMicroareas(ibge);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetMicroareasByUnidade")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<MicroareaViewModel>> GetMicroareasByUnidade([FromHeader] string ibge, int? unidade)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                string filtro = string.Empty;
                List<MicroareaViewModel> lista = _repository.GetMicroareasByUnidade(ibge, (int)unidade);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, response);
            }
        }
    }
}