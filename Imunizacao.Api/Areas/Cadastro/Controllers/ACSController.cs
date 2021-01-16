using System;
using System.Collections.Generic;
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

namespace RgCidadao.Api.Controllers
{
    [Route("api/Cadastro/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("Cadastro")]
    public class ACSController : ControllerBase
    {
        public IACSRepository _repository;
        private IConfiguration _config;
        public ACSController(IACSRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _config = configuration;
        }

        [HttpGet("GetAll")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<ACS>> GetAll([FromHeader]string ibge)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<ACS> lista = _repository.GetAll(ibge);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetAllPagination")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<ACS>> GetAllPagination([FromHeader]string ibge, int page,
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
                        filtro += Helper.GetFiltro(fields, search);
                    }
                    else
                    {
                        var stringcod = string.Empty;
                        if (Helper.soContemNumeros(search))
                            stringcod = $" CSI_CODMED CONTAINING '{search}' OR ";

                        filtro += $@" AND ( {stringcod}
                                            CSI_NOMMED CONTAINING '{search}' )";
                    }
                }

                int count = _repository.GetCountAll(ibge, filtro);
                if (count == 1)
                    page = 0;
                else
                    page = page * pagesize;

                Response.Headers.Add("X-Total-Count", count.ToString());
                List<ACS> lista = _repository.GetAllPagination(ibge, filtro, page, pagesize);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetACSByEquipe")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<ACS>> GetACSByEquipe([FromHeader] string ibge, int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<ACS> acs = _repository.GetAcsByEquipe(ibge, id);

                return Ok(acs);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

    }
}