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
    public class ProfissaoController : ControllerBase
    {
        public IProfissaoRepository _repository;
        private IConfiguration _config;
        public ProfissaoController(IProfissaoRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _config = configuration;
        }

        [HttpGet("GetAll")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<Profissao>> GetAll([FromHeader] string ibge)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<Profissao> lista = _repository.GetAll(ibge);
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
        public ActionResult<List<Profissao>> GetAllPagination([FromHeader] string ibge, int page,
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
                            stringcod = $" CSI_CODPRO CONTAINING '{search}' OR ";

                        filtro += $@" AND ( {stringcod}
                                            CSI_NOMPRO CONTAINING '{search}')";
                    }
                }

                int count = _repository.GetCountAll(ibge, filtro);
                if (count == 1)
                    page = 0;
                else
                    page = page * pagesize;

                Response.Headers.Add("X-Total-Count", count.ToString());
                List<Profissao> lista = _repository.GetAllPagination(ibge, page, pagesize, filtro);

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