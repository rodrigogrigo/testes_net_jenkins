using System;
using System.Collections.Generic;
using System.Net;
using RgCidadao.Api.Helpers;
using RgCidadao.Api.ViewModels;
using RgCidadao.Domain.Entities;
using RgCidadao.Domain.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RgCidadao.Domain.Entities.Imunizacao;
using RgCidadao.Domain.Repositories.Imunizacao;
using Microsoft.Extensions.Configuration;
using RgCidadao.Api.ViewModels.Imunizacao;
using RgCidadao.Api.Filters;

namespace RgCidadao.Api.Controllers
{
    [Route("api/Imunizacao/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("Imunizacao")]
    public class VacinaApresentacaoController : ControllerBase
    {
        private readonly IVacinaApresentacaoRepository _vacinaApresentRepository;
        private IConfiguration _config;
        public VacinaApresentacaoController(IVacinaApresentacaoRepository vacinaApresentrepository, IConfiguration configuration)
        {
            _vacinaApresentRepository = vacinaApresentrepository;
            _config = configuration;
        }

        [HttpGet("{ibge, filtro}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<VacinaApresentacao>> GetAll([FromQuery]ParametersVacinaApresentacao model, [FromHeader] string ibge)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                string filtro = string.Empty;
                List<VacinaApresentacao> lista = _vacinaApresentRepository.GetAll(ibge, filtro);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetById/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<VacinaApresentacao> GetById([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                VacinaApresentacao item = _vacinaApresentRepository.GetById(ibge, id);

                return Ok(item);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPost("Inserir")]
        [Route("{ibge,descricao,quantidade}")]
        [ParameterTypeFilter("inserir")]
        public ActionResult<VacinaApresentacao> Inserir([FromBody]ParametersVacinaApresentacao model, [FromHeader]string ibge)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                var id = _vacinaApresentRepository.GetId(ibge);
                _vacinaApresentRepository.InserirVacinaApresentacao(ibge, id, model.descricao, model.quantidade);
                model.id = id;
                return Ok(model);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPut("Atualizar")]
        [Route("{ibge,id,descricao,quantidade}")]
        [ParameterTypeFilter("editar")]
        public ActionResult<VacinaApresentacao> Atualizar([FromBody]ParametersVacinaApresentacao model, [FromHeader]string ibge)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                _vacinaApresentRepository.AtualizarVacinaApresentacao(ibge, model.id, model.descricao, model.quantidade);
                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpDelete("Excluir")]
        [Route("{ibge,id}")]
        [ParameterTypeFilter("excluir")]
        public ActionResult<VacinaApresentacao> Excluir([FromBody] ParametersVacinaApresentacao model, [FromHeader]string ibge)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                _vacinaApresentRepository.ExcluirVacinaApresentacao(ibge, model.id);
                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }
    }
}