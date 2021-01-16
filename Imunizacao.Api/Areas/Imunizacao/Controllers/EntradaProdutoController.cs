
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
using RgCidadao.Api.Filters;
using RgCidadao.Domain.ViewModels.Imunizacao;
using System.Collections.Generic;

namespace RgCidadao.Api.Controllers
{
    [Route("api/Imunizacao/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("Imunizacao")]
    public class EntradaProdutoController : ControllerBase
    {
        private readonly IEntradaProdutoRepository _entradaRepository;
        private readonly IEntradaProdutoItemRepository _entradaitemRepository;
        public IMovImunobiologicoRepository _movRepository;
        private IConfiguration _config;
        public EntradaProdutoController(IMovImunobiologicoRepository _movimentacaoIB, IEntradaProdutoRepository repository, IEntradaProdutoItemRepository item, IConfiguration configuration)
        {
            _entradaRepository = repository;
            _movRepository = _movimentacaoIB;
            _entradaitemRepository = item;
            _config = configuration;
        }

        [HttpGet("GetEntradaById/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<EntradaVacina> GetEntradaById([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                EntradaVacina model = _entradaRepository.GetEntradaById(ibge, id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetEntradaVacinaByUnidade/{unidade}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<EntradaVacina> GetEntradaVacinaByUnidade([FromRoute] string unidade, [FromHeader]string ibge, int page,
                                                                                       int pagesize, string search, string fields, string dataEntrada)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                string filtro = string.Empty;

                if (!string.IsNullOrWhiteSpace(dataEntrada))
                    filtro += $" AND E.DATA = '{Convert.ToDateTime(dataEntrada).ToString("dd.MM.yyyy")}'";

                if (!string.IsNullOrWhiteSpace(search))
                {
                    if (fields != null && fields.Split(",").Length > 0 && fields.Split(",")[0] != null)
                    {
                        foreach (var item in fields.Split(","))
                        {
                            if (!string.IsNullOrWhiteSpace(filtro))
                                filtro += " OR ";

                            filtro += $"{item} CONTAINING '{search}'";
                        }
                        filtro = "AND (" + filtro + ")";
                    }
                    else
                    {
                        var stringcod = string.Empty;
                        if (Helper.soContemNumeros(search))
                            stringcod = $" e.id CONTAINING '{search}' OR ";

                        filtro += $@"AND (  {stringcod}
                                           f.csi_nomfor CONTAINING '{search}' OR 
                                           e.numero_nota CONTAINING '{search}')";
                    }
                }

                int count = _entradaRepository.GetCountEntradaVacina(ibge, unidade, filtro);
                if (count == 1)
                    page = 0;
                else
                    page = page * pagesize;

                List<EntradaVacina> lista = _entradaRepository.GetEntradaVacinaByUnidade(ibge, unidade, page, pagesize, filtro);
                Response.Headers.Add("X-Total-Count", count.ToString());

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
        public ActionResult Inserir([FromHeader] string ibge, [FromBody]EntradaVacina model)
        {
            bool novoregistro = false;
            int? idEntrada = null;
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                if (model.EntradaProdutoItem.Count <= 0)
                    throw new Exception("É obrigatória a inclusão de itens!");

                if (model.id == null || model.id == 0)
                {
                    var id = _entradaRepository.GetNewId(ibge);
                    model.id = id;
                    novoregistro = true;
                    idEntrada = id;
                }

                _entradaRepository.InsertOrUpdate(ibge, model);

                foreach (var item in model.EntradaProdutoItem)
                {
                    if (item.id == 0 || item.id == null)
                        item.id = _entradaitemRepository.GetNewId(ibge); //cria novo id para item caso não possua
                    item.id_entrada_produto = model.id;
                    _entradaitemRepository.InsertOrUpdate(ibge, item); //grava item
                }

                return Ok(model);
            }
            catch (Exception ex)
            {
                //excluir pai e itens
                if (novoregistro && idEntrada != null)
                {
                    _entradaitemRepository.DeleteAllByPai(ibge, (int)idEntrada);
                    _entradaRepository.Delete(ibge, (int)idEntrada);
                }

                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPut("Editar/{id}")]
        [ParameterTypeFilter("editar")]
        public ActionResult Editar([FromHeader] string ibge, [FromBody] EntradaVacina model, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                if (model.EntradaProdutoItem.Count <= 0)
                    throw new Exception("É obrigatória a inclusão de itens!");

                model.id = id;
                _entradaRepository.InsertOrUpdate(ibge, model);

                foreach (var item in model.EntradaProdutoItem)
                {
                    if (item.id == 0 || item.id == null)
                        item.id = _entradaitemRepository.GetNewId(ibge); //cria novo id para item caso não possua
                    item.id_entrada_produto = id;
                    _entradaitemRepository.InsertOrUpdate(ibge, item); //grava item
                }

                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpDelete("Excluir/{id}")]
        [ParameterTypeFilter("excluir")]
        public ActionResult Excluir([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));

                var lista = _entradaitemRepository.GetItensEntradaProdutoByIdEntradaProduto(ibge, id);

                foreach (EntradaProdutoItem Item in lista)
                {
                    DateTime DataEntrada = _movRepository.GetDataMovimentoProduto(ibge, $@"PNI_ENTRADA_PRODUTO_ITEM", Item.id);
                    if (_movRepository.GetExisteMovimentoProdutoAposEntrada(ibge, Item.id_produtor, Item.id_produto, Item.lote, DataEntrada))
                    {
                        var response = TrataErro.GetResponse("Esta entrada não pode ser excluída, pois houve movimentação do lote após a entrada.", true);
                        return BadRequest(response);
                    }
                }

                foreach (EntradaProdutoItem Item in lista)
                {
                    _movRepository.ExcluirMovimentoProdutoById(ibge, Item.id, $@"PNI_ENTRADA_PRODUTO_ITEM");
                    _entradaitemRepository.Delete(ibge, Item.id);
                }

                _entradaRepository.Delete(ibge, id);
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