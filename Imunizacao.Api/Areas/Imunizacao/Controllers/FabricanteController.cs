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
using RgCidadao.Domain.Repositories.Imunizacao;
using RgCidadao.Domain.Entities.Imunizacao;
using RgCidadao.Domain.Entities.Cadastro;
using Microsoft.Extensions.Configuration;
using RgCidadao.Api.Filters;

namespace Imunizacao.Api.Controllers
{
    [Route("api/Imunizacao/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("Imunizacao")]
    public class FabricanteController : ControllerBase
    {
        private readonly IFabricanteRepository _fabricanteRepository;
        private IConfiguration _config;
        public FabricanteController(IFabricanteRepository fornecedorrepository, IConfiguration configuration)
        {
            _fabricanteRepository = fornecedorrepository;
            _config = configuration;
        }

        [HttpGet("{ibge}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<Fabricante>> GetAll([FromHeader]string ibge)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                string filtro = string.Empty;
                List<Fabricante> lista = _fabricanteRepository.GetAll(ibge, filtro);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPost("Inserir")]
        [Route("{ibge}")]
        [ParameterTypeFilter("inserir")]
        public ActionResult<Fornecedor> Inserir([FromHeader]string ibge, [FromBody] Fabricante model)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                var id = _fabricanteRepository.GetNewId(ibge);
                model.id = id;
                _fabricanteRepository.Inserir(ibge, model);

                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPut("Atualizar")]
        [Route("{ibge}")]
        [ParameterTypeFilter("editar")]
        public ActionResult<Fornecedor> Atualizar([FromHeader]string ibge, [FromBody] Fabricante model)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                _fabricanteRepository.Atualizar(ibge, model);
                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpDelete("Excluir/{id}")]
        [Route("{ibge}")]
        [ParameterTypeFilter("excluir")]
        public ActionResult<Fornecedor> Excluir([FromHeader]string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                _fabricanteRepository.Delete(ibge, id);
                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetProdutorByImunobiologico/{produto}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<Fabricante>> GetProdutorByImunobiologico([FromHeader]string ibge, [FromRoute] int produto)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<Fabricante> itens = _fabricanteRepository.GetProdutorByImunobiologico(ibge, produto);

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