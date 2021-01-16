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

namespace RgCidadao.Api.Areas.AtencaoBasica.Controllers
{
    [Route("api/AtencaoBasica/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("AtencaoBasica")]
    public class ProcedimentoController : ControllerBase
    {
        public IProcedimentoRepository _repository;
        private IConfiguration _config;
        public ProcedimentoController(IProcedimentoRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _config = configuration;
        }

        [HttpGet("GetProcedimentosByCompetencia022019")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<Procedimento>> GetProcedimentosByCompetencia022019([FromHeader]string ibge)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<Procedimento> itens = _repository.GetProcedimentosByCompetencia022019(ibge);
                return Ok(itens);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        #region Agendamento de Consulta
        [HttpGet("GetProcedimentoBycbo/{cbo}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<Procedimento>> GetProcedimentoBycbo([FromHeader] string ibge, [FromRoute] string cbo)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<Procedimento> itens = _repository.GetProcedimentoBycbo(ibge, cbo);
                return Ok(itens);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }
        #endregion

    }
}