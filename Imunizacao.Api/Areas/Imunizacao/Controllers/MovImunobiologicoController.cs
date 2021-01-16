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
using RgCidadao.Domain.Entities.Imunizacao;
using RgCidadao.Domain.Repositories.Imunizacao;
using Microsoft.Extensions.Configuration;
using RgCidadao.Api.ViewModels;
using RgCidadao.Api.ViewModels.Cadastro;
using RgCidadao.Api.Filters;

namespace RgCidadao.Api.Controllers
{
    [Route("api/Imunizacao/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("Imunizacao")]
    public class MovImunobiologicoController : ControllerBase
    {
        private readonly IMovImunobiologicoRepository _movRepository;
        private IConfiguration _config;
        public MovImunobiologicoController(IMovImunobiologicoRepository movimentorepository, IConfiguration configuration)
        {
            _movRepository = movimentorepository;
            _config = configuration;
        }

        [HttpGet("GetMovimentoByUnidade")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<MovimentoImunobiologico>> GetMovimentoByUnidade([FromHeader]string ibge, int unidade, int page,
                                                                                            int pagesize, string search, string fields)
        {
            try
            {
                string filtro = string.Empty;
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
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
                            stringcod = $" MP.ID CONTAINING '{search}' OR ";

                        filtro += $@" AND ( {stringcod} 
                                            PDT.ABREVIATURA ||' - '|| PDT.SIGLA CONTAINING '{search}' OR
                                            MP.LOTE CONTAINING '{search}' OR
                                            PDTR.NOME CONTAINING '{search}')";
                    }
                }
                int count = _movRepository.GetCountAll(ibge, filtro, unidade);
                if (count == 1)
                    page = 0;
                else
                    page = page * pagesize;

                List<MovimentoImunobiologico> lista = _movRepository.GetMovimentoByUnidade(ibge, unidade, filtro, page, pagesize);

                Response.Headers.Add("X-Total-Count", count.ToString());
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        //getbyid
        [HttpGet("GetMovimentoById/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<MovimentoImunobiologico> GetMovimentoById([FromHeader]string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                MovimentoImunobiologico item = _movRepository.GetMovimentoById(ibge, id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetMovRetiradaEstoqueByLote")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<ReponseBoolViewModel> GetMovRetiradaEstoqueByLote([FromHeader] string ibge, string lote, DateTime? data)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                bool mov = _movRepository.GetMovRetiradaEstoqueByLote(ibge, lote, data);

                var response = new ReponseBoolViewModel();
                response.possui = mov;
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        //inserir
        [HttpPost("Inserir")]
        [Route("{ibge}")]
        [ParameterTypeFilter("inserir")]
        public ActionResult Inserir([FromHeader]string ibge, [FromBody] MovimentoImunobiologico model)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                var id = _movRepository.GetNewId(ibge);
                model.id = id;
                _movRepository.Inserir(ibge, model);
                return Ok(model);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        //atualizar
        [HttpPut("Editar/{id}")]
        [Route("{ibge}")]
        [ParameterTypeFilter("editar")]
        public ActionResult<MovimentoImunobiologico> Editar([FromHeader]string ibge, [FromBody] MovimentoImunobiologico model, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                model.id = id;
                _movRepository.Atualizar(ibge, model);
                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        //excluir
        [HttpDelete("Excluir/{id}")]
        [ParameterTypeFilter("excluir")]
        public ActionResult<MovimentoImunobiologico> Excluir([FromHeader]string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                _movRepository.Excluir(ibge, id);
                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetMovimentoByLote")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<MovimentoImunobiologico>> GetMovimentoByLote([FromHeader] string ibge, string lote, DateTime? data)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<MovimentoImunobiologico> itens = _movRepository.GetMovimentoByLote(ibge, lote, data);
                return Ok(itens);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}