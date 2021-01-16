using System;
using System.Collections.Generic;
using System.Linq;
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

namespace RgCidadao.Api.Controllers
{
    [Route("api/Imunizacao/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("Imunizacao")]
    public class EntradaProdutoItemController : ControllerBase
    {
        public IEntradaProdutoItemRepository _repository;
        public IEntradaProdutoRepository _entrada;
        public IMovImunobiologicoRepository _movRepository;
        private IConfiguration _config;

        public EntradaProdutoItemController(IMovImunobiologicoRepository _movimentacaoIB, IEntradaProdutoItemRepository _entradarepository, IEntradaProdutoRepository entrada, IConfiguration configuration)
        {
            _repository = _entradarepository;
            _movRepository = _movimentacaoIB;
            _entrada = entrada;
            _config = configuration;
        }

        [HttpGet("GetEntradaProdutoById/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<EntradaVacinaItem> GetEntradaProdutoById([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                EntradaVacinaItem item = _repository.GetEntradaItemById(ibge, id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetAllProdutosByEntrada/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<EntradaVacinaItem>> GetAllProdutosByEntrada([FromHeader] string ibge, [FromRoute] int id)
         {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<EntradaVacinaItem> itens = _repository.GetAllItensByPai(ibge, id);

                return Ok(itens);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetUltimaEntradaItemByLote/{id_lote}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<EntradaVacinaItem> GetUltimaEntradaItemByLote([FromHeader] string ibge, [FromRoute] int id_lote)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                EntradaVacinaItem item = _repository.GetUltimaEntradaItemByLote(ibge, id_lote);

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
        public ActionResult Excluir([FromHeader] string ibge, [FromRoute] int id)
        {

            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                var Item = _repository.GetEntradaProdutoItemById(ibge, id);

                DateTime DataEntrada = _movRepository.GetDataMovimentoProduto(ibge, $@"PNI_ENTRADA_PRODUTO_ITEM", Item.id);
                if (!_movRepository.GetExisteMovimentoProdutoAposEntrada(ibge, Item.id_produtor, Item.id_produto, Item.lote, DataEntrada))
                {
                    _movRepository.ExcluirMovimentoProdutoById(ibge, Item.id, $@"PNI_ENTRADA_PRODUTO_ITEM");
                    _repository.Delete(ibge, Item.id);
                    return Ok();
                }
                else
                {
                    var response = TrataErro.GetResponse("Este item não pode ser excluído, pois houve movimentação do lote após a entrada.", true);
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }

        }

    }
}