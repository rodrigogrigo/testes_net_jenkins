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
using Microsoft.Extensions.Configuration;
using RgCidadao.Api.Filters;

namespace RgCidadao.Api.Controllers
{
    [Route("api/Imunizacao/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("Imunizacao")]
    public class CalendarioBasicoController : ControllerBase
    {
        private readonly ICalendarioBasicoRepository _calendarioRepository;
        private readonly IAprazamentoRepository _aprazamentoRepository;
        private IConfiguration _config;
        public CalendarioBasicoController(ICalendarioBasicoRepository calendariorepository, IAprazamentoRepository aprazamentorepositopry, IConfiguration config)
        {
            _calendarioRepository = calendariorepository;
            _aprazamentoRepository = aprazamentorepositopry;
            _config = config;
        }

        [HttpGet("GetAllPagination")]
        [Route("{ibge}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<CalendarioBasico>> GetAllPagination([FromHeader]string ibge, int page,
                                                                                          int pagesize, string search, string fields, int? publico_alvo)
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
                            stringcod = $" CB.ID CONTAINING '{search}' OR ";

                        if (Helper.soContemNumerosDouble(search))
                        {
                            stringcod += $@" CB.IDADE_MINIMA = {search} OR
                                             CB.IDADE_MAXIMA = {search} OR";

                        }

                        filtro += $@" AND( {stringcod}
                                            P.ABREVIATURA ||' - '|| P.SIGLA CONTAINING '{search}' OR
                                            D.DESCRICAO CONTAINING '{search}')";
                    }
                }

                int count = _calendarioRepository.GetCountAll(ibge, filtro, publico_alvo);
                if (count == 1)
                    page = 0;
                else
                    page = page * pagesize;

                Response.Headers.Add("X-Total-Count", count.ToString());
                List<CalendarioBasico> lista = _calendarioRepository.GetAllPagination(ibge, page, pagesize, filtro, publico_alvo);

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
        public ActionResult<Domain.Entities.Cadastro.Fornecedor> Inserir([FromHeader]string ibge, [FromBody] CalendarioBasico model)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                var id = _calendarioRepository.GetNewId(ibge);
                model.id = id;
                _calendarioRepository.Insert(ibge, model);

                return Ok(model);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        //getbyid
        [HttpGet("GetCalendarioById/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<CalendarioBasico> GetCalendarioById([FromHeader]string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                CalendarioBasico item = _calendarioRepository.GetById(ibge, id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPut("Editar/{id}")]
        [Route("{ibge}")]
        [ParameterTypeFilter("editar")]
        public ActionResult Editar([FromHeader]string ibge, [FromBody] CalendarioBasico model, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                model.id = id;
                _calendarioRepository.Update(ibge, model);
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
        public ActionResult<Domain.Entities.Cadastro.Fornecedor> Excluir([FromHeader]string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                var existe = _aprazamentoRepository.GetAprazamentoByCalendarioBasico(ibge, id);
                if (existe != null)
                    _calendarioRepository.UpdateInativo(ibge, id);
                else
                    _calendarioRepository.Delete(ibge, id);

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