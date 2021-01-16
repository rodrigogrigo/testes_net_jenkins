using System;
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
using System.Collections.Generic;
using RgCidadao.Api.Filters;

namespace RgCidadao.Api.Controllers
{
    [Route("api/Imunizacao/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("Imunizacao")]
    public class ProdutorController : ControllerBase
    {
        public IProdutorRepository _repository;
        public ILoteRepository _loterepository;
        public ICartaoVacinaRepository _cvrepository;
        private IConfiguration _config;
        public ProdutorController(IProdutorRepository repository, IConfiguration configuration, ILoteRepository loterepository, ICartaoVacinaRepository cvrepository)
        {
            _repository = repository;
            _config = configuration;
            _loterepository = loterepository;
            _cvrepository = cvrepository;
        }

        [HttpGet("GetAllPagination")]
        [Route("{ibge}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<Produtor>> GetAllPagination([FromHeader]string ibge, int page,
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
                        filtro += " WHERE " + Helper.GetFiltroInicial(fields, search);
                    }
                    else
                    {
                        var stringcod = string.Empty;

                        filtro += $@" WHERE NOME CONTAINING '{search}' OR
                                           ABREVIATURA CONTAINING '{search}'";
                    }
                }

                int count = _repository.GetCountAll(ibge, filtro);
                if (count == 1)
                    page = 0;
                else
                    page = page * pagesize;

                Response.Headers.Add("X-Total-Count", count.ToString());
                List<Produtor> lista = _repository.GetAllPagination(ibge, pagesize, page, filtro);

                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPost("Inserir")]
        [ParameterTypeFilter("inserir")]
        public ActionResult<Produtor> Inserir([FromHeader]string ibge, [FromBody] Produtor model)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                var id = _repository.GetNewId(ibge);
                model.id = id;
                _repository.Insert(ibge, model);

                return Ok(model);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPut("Editar/{id}")]
        [ParameterTypeFilter("editar")]
        public ActionResult<Produtor> Editar([FromHeader]string ibge, [FromBody] Produtor model, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                model.id = id;
                _repository.Update(ibge, model);

                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetAll")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult GetAll([FromHeader] string ibge)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<Produtor> lista = _repository.GetAll(ibge);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetProdutorById/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<Produtor> GetProdutorById([FromHeader] string ibge, [FromRoute]int? id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                Produtor item = _repository.GetProdutorById(ibge, (int)id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpDelete("Excluir/{id}")]
        [ParameterTypeFilter("excluir")]
        public ActionResult Excluir([FromHeader] string ibge, [FromRoute] int? id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));

                //verifica tabela de lote
                var itenslote = _loterepository.GetLoteByProdutor(ibge, (int)id);
                if (itenslote.Count > 0)
                    return BadRequest(TrataErro.GetResponse("Esse fabricante não pode ser excluído pois já possui um lote vinculado a ele.", true));

                //verifica tabela de cartão de vacina
                var itenscv = _loterepository.GetLoteByProdutor(ibge, (int)id);
                if (itenscv.Count > 0)
                    return BadRequest(TrataErro.GetResponse("Esse fabricante não pode ser excluído pois já possui um cartão de vacina vinculado a ele.", true));

                _repository.Delete(ibge, (int)id);
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