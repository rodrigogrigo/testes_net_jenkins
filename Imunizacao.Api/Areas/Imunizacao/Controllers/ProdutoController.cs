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
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICalendarioBasicoRepository _cbRepository;
        private readonly ICartaoVacinaRepository _cvRepository;
        private readonly ILoteRepository _loteRepository;
        private IConfiguration _config;
        public ProdutoController(IProdutoRepository vacinarepository, IConfiguration configuration, ICalendarioBasicoRepository cbRepository,
                                    ICartaoVacinaRepository cvRepository, ILoteRepository loteRepository)
        {
            _produtoRepository = vacinarepository;
            _config = configuration;
            _cbRepository = cbRepository;
            _cvRepository = cvRepository;
            _loteRepository = loteRepository;
        }

        [HttpGet("GetAllPagination")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<Produto>> GetAllPagination([FromHeader]string ibge, int page,
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
                        filtro += Helper.GetFiltroInicial(fields, search);
                    }
                    else
                    {
                        var stringcod = string.Empty;
                        if (Helper.soContemNumeros(search))
                            stringcod = $" PP.id CONTAINING '{search}' OR ";

                        filtro += $@"  {stringcod}
                                     PP.nome CONTAINING '{search}' OR 
                                     PP.sigla CONTAINING '{search}' OR
                                     PP.ABREVIATURA CONTAINING '{search}' OR 
                                     PU.NOME CONTAINING '{search}' OR
                                     PC.NOME CONTAINING '{search}'";
                    }
                }

                if (!string.IsNullOrWhiteSpace(filtro))
                    filtro = $" WHERE " + filtro;

                int count = _produtoRepository.GetCountAll(ibge, filtro);
                if (count == 1)
                    page = 0;
                else
                    page = page * pagesize;

                List<Produto> lista = _produtoRepository.GetAll(ibge, filtro, page, pagesize);

                Response.Headers.Add("X-Total-Count", count.ToString());
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetAll")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<Produto>> GetAll([FromHeader]string ibge, bool orderAbrev = false)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<Produto> lista = _produtoRepository.GetImunobiologico(ibge, orderAbrev);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetProdutoEstoqueByUnidade/{unidade}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<Produto>> GetProdutoEstoqueByUnidade([FromHeader]string ibge, [FromRoute] int unidade)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<Produto> lista = _produtoRepository.GetImunobiologicoEstoqueByUnidade(ibge, unidade);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetProdutoById/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<Produto> GetProdutoById([FromHeader]string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                Produto item = _produtoRepository.GetProdutoById(ibge, id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetEstoqueProduto")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<EstoqueProduto> GetEstoqueProduto([FromHeader]string ibge, int produto, int unidade, int produtor, string lote)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                EstoqueProduto item = _produtoRepository.GetEstoqueImunobiologicoByParams(ibge, produto, unidade, produtor, lote);

                return Ok(item);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetProdutoByEstrategia/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<Produto>> GetProdutoByEstrategia([FromHeader]string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<Produto> lista = _produtoRepository.GetProdutoByEstrategia(ibge, id);
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
        public ActionResult Inserir([FromHeader] string ibge, [FromBody]Produto model)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                model.id = _produtoRepository.GetNewId(ibge);
                _produtoRepository.Insert(ibge, model);
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
        public ActionResult Editar([FromHeader] string ibge, [FromBody]Produto model, [FromRoute] int? id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                model.id = id;
                _produtoRepository.Update(ibge, model);
                return Ok(model);
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
                string unidade = HttpContext.Request.Headers["unidade"];
                //valida lote
                var lote = _loteRepository.GetLoteByImunobiologico(ibge, (int)id, Convert.ToInt32(unidade));

                if (lote.Count > 0)
                    return BadRequest(TrataErro.GetResponse("Este Produto não pode ser excluído pois está vinculado a um lote.", true));
                //valida calendario basico
                var cb = _cbRepository.GetCalendarioByProduto(ibge, (int)id);
                if (cb.Count > 0)
                    return BadRequest(TrataErro.GetResponse("Este Produto não pode ser excluído ppois está vinculado a um calendário básico.", true));
                //valida cartao de vacina
                var cv = _cvRepository.GetCartaoVacinaByProduto(ibge, (int)id);
                if (cv.Count > 0)
                    return BadRequest(TrataErro.GetResponse("Este Produto não pode ser excluído ppois está vinculado a um cartão de vacina.", true));

                _produtoRepository.Delete(ibge, (int)id);
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