using System;
using System.Net;
using RgCidadao.Api.Helpers;
using RgCidadao.Api.ViewModels;
using RgCidadao.Domain.Entities;
using RgCidadao.Domain.Repositories;
using RgCidadao.Domain.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RgCidadao.Domain.Repositories.Imunizacao;
using RgCidadao.Domain.Entities.Imunizacao;
using RgCidadao.Domain.ViewModels.Imunizacao;
using Microsoft.Extensions.Configuration;
using RgCidadao.Api.ViewModels.Imunizacao;
using RgCidadao.Api.Filters;
using System.Collections.Generic;

namespace RgCidadao.Api.Controllers
{
    [Route("api/Imunizacao/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("Imunizacao")]
    public class LoteController : ControllerBase
    {
        public ILoteRepository _repository;
        public IMovImunobiologicoRepository _movrepository;
        private IConfiguration _config;
        public LoteController(ILoteRepository repository, IConfiguration configuration, IMovImunobiologicoRepository mov)
        {
            _repository = repository;
            _config = configuration;
            _movrepository = mov;
        }

        [HttpGet("GetLoteByProduto/{produto}")]
        [ParameterTypeFilter("ver_lotes")]
        public ActionResult<List<LoteImunobiologico>> GetLoteByProduto([FromHeader]string ibge, [FromRoute] int produto)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                string unidade = HttpContext.Request.Headers["unidade"];
                List<LoteImunobiologico> lista = _repository.GetLoteByImunobiologico(ibge, produto, Convert.ToInt32(unidade));

                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetLoteByLote")]
        [ParameterTypeFilter("ver_lotes")]
        public ActionResult<LoteImunobiologico> GetLoteByLote([FromHeader]string ibge, string lote, int produto, int produtor)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                LoteImunobiologico item = _repository.GetLoteByLote(ibge, lote, produtor, produto);

                return Ok(item);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetLoteEstoqueByProduto")]
        [ParameterTypeFilter("ver_lotes")]
        public ActionResult<List<LoteImunobiologico>> GetLoteEstoqueByProduto([FromHeader]string ibge, int produto,
                                                                                            int unidade, string lote)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                string filtro = string.Empty;
                if (!string.IsNullOrWhiteSpace(lote))
                    filtro = $@" (LP.ID_PRODUTO = {produto} AND
                                  EP.ID_UNIDADE = {unidade} AND
                                  EP.QTDE > 0) OR                                 
                                  LP.LOTE = '{lote}' ";
                else
                    filtro = $@"LP.ID_PRODUTO = {produto} AND
                                EP.ID_UNIDADE = {unidade} AND 
                                EP.QTDE > 0";

                List<LoteImunobiologico> lista = _repository.GetLoteEstoqueByImunobiologico(ibge, filtro, unidade);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetLoteByUnidade")]
        [ParameterTypeFilter("ver_lotes")]
        public ActionResult<List<LoteImunobiologico>> GetLoteByUnidade([FromHeader] string ibge, int unidade, int produto)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<LoteImunobiologico> itens = _repository.GetLoteByUnidade(ibge, unidade, produto);
                return Ok(itens);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetLoteById/{id}")]
        [ParameterTypeFilter("ver_lotes")]
        public ActionResult<LoteImunobiologico> GetLoteById([FromHeader] string ibge, [FromRoute]int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                LoteImunobiologico item = _repository.GetLoteById(ibge, id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPost("Inserir")]
        [ParameterTypeFilter("inserir")]
        public ActionResult Inserir([FromHeader] string ibge, [FromBody] LoteImunobiologico model)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                model.id = _repository.GetNewId(ibge);
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
        public ActionResult Editar([FromHeader] string ibge, [FromBody] LoteImunobiologico model, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                var item = _repository.GetLoteById(ibge, id);

                var contagemmov = _movrepository.GetMovimentoSaidaLote(ibge, item.lote, (int)item.id_produto, (int)item.id_produtor);
                if (contagemmov > 0)
                    return BadRequest(TrataErro.GetResponse("Existem doses desse Lote que já foram retiradas do Estoque. Não é possível editar o Lote.", true));

                model.id = id;
                _repository.Editar(ibge, model);
                return Ok(model);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        //[HttpPut("AtualizarSituacaoLote/{idlote}")]
        //[Route("{ibge}")]
        //[ParameterTypeFilter("bloquear_desbloquear_lote")]
        //public ActionResult<LoteImunobiologico> AtualizarSituacaoLote([FromHeader] string ibge, [FromBody] ParametersAtualizaLote model, [FromRoute]int idlote)
        //{
        //    try
        //    {
        //        ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
        //        _repository.AtualizarSituacaoLote(ibge, idlote, model.situacao);
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        var response = TrataErro.GetResponse(ex.Message, true);
        //        return StatusCode((int)HttpStatusCode.InternalServerError, response);
        //    }
        //}

        [HttpGet("GetPrimeiroMovimentoLote")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<MovLoteViewModel> GetPrimeiroMovimentoLote([FromHeader] string ibge, int? id_produto, int? id_unidade, string lote)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                MovLoteViewModel data = _repository.GetPrimeiroMovimentoLote(ibge, id_produto, id_unidade, lote);
                return Ok(data);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }


        [HttpPost("AdicionaBloqueioUnidadeLote")]
        [ParameterTypeFilter("inserir")]
        public ActionResult AdicionaBloqueioUnidadeLote([FromHeader] string ibge, [FromBody] BloqueioLoteViewModel model)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                _repository.AdicionaBloqueioUnidadeLote(ibge, (int)model.unidade, (int)model.lote);

                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpDelete("RemoveBloqueioUnidadeLote/{lote:int}/{unidade:int}")]
        [ParameterTypeFilter("excluir")]
        public ActionResult RemoveBloqueioUnidadeLote([FromHeader] string ibge, [FromRoute] BloqueioLoteViewModel model)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                _repository.RemoveBloqueioUnidadeLote(ibge, (int)model.unidade, (int)model.lote);

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