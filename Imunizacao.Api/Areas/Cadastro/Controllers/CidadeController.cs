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
    public class CidadeController : ControllerBase
    {
        private readonly ICidadeRepository _cidadeRepository;
        private IConfiguration _config;
        public CidadeController(ICidadeRepository cidaderepository, IConfiguration configuration)
        {
            _cidadeRepository = cidaderepository;
            _config = configuration;
        }

        [HttpGet("{ibge}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<Cidade>> GetAll([FromHeader] string ibge)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<Cidade> lista = _cidadeRepository.GetAll(ibge);
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
        public ActionResult<List<Cidade>> GetAllPagination([FromHeader] string ibge, int page,
                                                           int pagesize, string search, string fields)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                string filtro = string.Empty;

                if (!string.IsNullOrWhiteSpace(search))
                {
                    var stringcod = string.Empty;
                    if (Helper.soContemNumeros(search))
                        stringcod = $" CSI_CODCID = '{search}' OR ";

                    search = Helper.RemoveAcentos(search);
                    filtro += $@" WHERE {stringcod}
                                        CSI_NOMCID CONTAINING '{search}'";

                }

                List<Cidade> lista = _cidadeRepository.GetAllPagination(ibge, page, pagesize, filtro);

                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetCidadeById")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<Cidade> GetCidadeById([FromHeader] string ibge, int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                Cidade bairro = _cidadeRepository.GetCidadeById(ibge, id);

                return Ok(bairro);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetCidadeByIBGE")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<Cidade> GetCidadeByIBGE([FromHeader] string ibge, string codigo_ibge)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                Cidade cidade = _cidadeRepository.GetCidadeByIBGE(ibge, codigo_ibge);

                return Ok(cidade);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }
    }
}