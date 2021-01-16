using System;
using System.Net;
using RgCidadao.Api.Helpers;
using RgCidadao.Domain.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RgCidadao.Domain.Repositories.Cadastro;
using Microsoft.Extensions.Configuration;
using RgCidadao.Api.Filters;
using RgCidadao.Domain.Entities.Cadastro;
using System.Collections.Generic;

namespace RgCidadao.Api.Controllers
{
    [Route("api/Cadastro/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("Cadastro")]
    public class GestacaoController : ControllerBase
    {
        public IGestacaoRepository _repository;
        private IConfiguration _config;
        public GestacaoController(IGestacaoRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _config = configuration;
        }

        [HttpGet("IsGestante")]
        public ActionResult<Gestacao> IsGestante([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                Gestacao gestacao = _repository.IsGestante(ibge, id);
                return Ok(gestacao);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetGestacaoByCidadao/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<Gestacao>> GetGestacaoByCidadao([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<Gestacao> itens = _repository.GetGestacaoByCidadao(ibge, id);
                return Ok(itens);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetGestacaoItemByGestacao/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<Gestacao_Item>> GetGestacaoItemByGestacao([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<Gestacao_Item> itens = _repository.GetGestacaoItensByGestacao(ibge, id);
                return Ok(itens);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetUltimaGestacao/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<Gestacao> GetUltimaGestacao([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                Gestacao gestacao = _repository.GetUltimaGestacao(ibge, id);

                return Ok(gestacao);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }
    }
}