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
using RgCidadao.Domain.Entities.Cadastro;
using RgCidadao.Domain.Repositories.Cadastro;

namespace RgCidadao.Api.Areas.Cadastro.Controllers
{
    [Route("api/Cadastro/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("Cadastro")]
    public class LogradouroController : Controller
    {
        private readonly ILogradouroRepository _Repository;
        private IConfiguration _config;
        public LogradouroController(ILogradouroRepository repository, IConfiguration configuration)
        {
            _Repository = repository;
            _config = configuration;
        }

        [HttpGet("GetAllPagination")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<Logradouro>> GetAllPagination([FromHeader]string ibge, int page,
                                                                                         int pagesize, string search, string fields)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                string filtro = string.Empty;

                if (!string.IsNullOrWhiteSpace(search))
                {
                    if (fields != null && fields.Split(",").Length > 0 && fields.Split(",")[0] != null)
                    {
                        filtro += Helper.GetFiltroInicial(fields, search);
                    }
                    else
                    {
                        filtro += $@" (
                                        LOGR.CSI_NOMEND CONTAINING '{search}'
                                        OR (BAI.CSI_NOMBAI CONTAINING '{search}')
                                        OR (CID.CSI_NOMCID CONTAINING '{search}')
                                        OR (LOGR.CSI_CEP CONTAINING '{search}')
                                        )";
                    }
                }

                if (!string.IsNullOrWhiteSpace(filtro))
                    filtro = $" WHERE " + filtro;

                int count = _Repository.GetCountAll(ibge, filtro);
                if (count == 1)
                    page = 0;
                else
                    page = page * pagesize;

                List<Logradouro> lista = _Repository.GetAllPagination(ibge, page, pagesize, filtro);

                Response.Headers.Add("X-Total-Count", count.ToString());
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetLogradouroById")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<Logradouro> GetLogradouroById([FromHeader] string ibge, int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                Logradouro logradouro = _Repository.GetLogradouroById(ibge, id);

                return Ok(logradouro);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetLogradouroByBairro/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<Logradouro>> GetLogradouroByBairro([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<Logradouro> logradouro = _Repository.GetLogradouroByBairro(ibge, id);

                return Ok(logradouro);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }
    }
}