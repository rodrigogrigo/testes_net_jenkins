using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using RgCidadao.Api.Helpers;
using RgCidadao.Domain.Entities;
using RgCidadao.Domain.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RgCidadao.Domain.Entities.Imunizacao;
using RgCidadao.Domain.Repositories.Imunizacao;
using Microsoft.Extensions.Configuration;
using RgCidadao.Api.Filters;

namespace RgCidadao.Api.Controllers
{
    [Route("api/Imunizacao/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("Imunizacao")]
    public class ViaAdmController : ControllerBase
    {
        public IViaAdmRepository _repository;
        private IConfiguration _config;
        public ViaAdmController(IViaAdmRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _config = configuration;
        }

        [HttpGet("GetAllViaAdm")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<ViaAdministracao>> GetAllViaAdm([FromHeader] string ibge, int? id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                string filtro = string.Empty;
                if (id != null)
                {
                    filtro = $" WHERE ID = {id} ";
                }

                List<ViaAdministracao> itens = _repository.GetAllViaAdm(ibge, filtro);

                return Ok(itens);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetLocalAplicacaoByViaAdm")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<LocalAplicacao>> GetLocalAplicacaoByViaAdm([FromHeader] string ibge, int? id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<LocalAplicacao> itens = _repository.GetLocalAplicacaoByViaAdm(ibge, id);

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