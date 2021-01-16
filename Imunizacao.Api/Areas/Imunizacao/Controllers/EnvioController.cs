using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RgCidadao.Api.Filters;
using RgCidadao.Api.Helpers;
using RgCidadao.Domain.Commands.Imunizacao;
using RgCidadao.Domain.Entities.Imunizacao;
using RgCidadao.Domain.Repositories.Imunizacao;

namespace RgCidadao.Api.Areas.Imunizacao.Controllers
{
    [Route("api/Imunizacao/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("Imunizacao")]
    public class EnvioController : ControllerBase
    {

        private IEnvioRepository _repository;
        private IConfiguration _config;
        public EnvioController(IEnvioRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _config = configuration;
        }

        [HttpDelete("Delete/{id}")]
        [ParameterTypeFilter("excluir")]
        public ActionResult Delete([FromHeader]string ibge, [FromRoute] int id)
        {
            try
            {
                //verifica se ja aconteceu a transferencia
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                var tranferencia = _repository.GetEnvioById(ibge, id);
                if (tranferencia.status != 0)
                {
                    var response = TrataErro.GetResponse("Esta transferência já foi enviada para outra Unidade de Saúde e não pode ser excluída.", true);
                    return BadRequest(response);
                }

                _repository.Delete(ibge, id);
                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpDelete("DeleteItem/{id}")]
        [ParameterTypeFilter("excluir")]
        public ActionResult DeleteItem([FromHeader]string ibge, [FromRoute] int id)
        {
            try
            {
                //verifica se ja aconteceu a transferencia
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                var idpai = _repository.GetItemById(ibge, id);
                var tranferencia = _repository.GetEnvioById(ibge, (int)idpai.id_envio);
                if (tranferencia.status != 0)
                {
                    var response = TrataErro.GetResponse("Esta transferência já foi enviada para outra Unidade de Saúde e não pode ser excluída.", true);
                    return BadRequest(response);
                }

                _repository.DeleteItem(ibge, id);
                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetAllItensByPai/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<EnvioItem>> GetAllItensByPai([FromHeader]string ibge, [FromRoute]int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<EnvioItem> itens = _repository.GetAllItensByPai(ibge, id);
                return Ok(itens);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetAllPagination/{id_unidade_origem}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<Envio>> GetAllPagination([FromHeader]string ibge, int page,
                                                           int pagesize, int? id, DateTime? datainicial, DateTime? datafinal, int? unidadedestino, [FromRoute] int? id_unidade_origem)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                string filtro = string.Empty;

                //por id
                if (id != null)
                    filtro += $@" E.ID = {id} ";

                // por data inicial
                if (datainicial != null && !string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" AND E.DATA_ENVIO >= '{datainicial?.ToString("dd.MM.yyyy")}'";
                else if (datainicial != null && string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" E.DATA_ENVIO >= '{datainicial?.ToString("dd.MM.yyyy")}'";

                //por data final
                if (datafinal != null && !string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" AND E.DATA_ENVIO <= '{datafinal?.ToString("dd.MM.yyyy")}'";
                else if (datafinal != null && string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" E.DATA_ENVIO <= '{datafinal?.ToString("dd.MM.yyyy")}'";

                //por unidade de destino
                if (unidadedestino != null && !string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" AND UDEST.CSI_CODUNI = {unidadedestino}";
                else if (unidadedestino != null && string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" UDEST.CSI_CODUNI = {unidadedestino}";

                if (id_unidade_origem != null && !string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" AND E.ID_UNIDADE_ORIGEM = {id_unidade_origem}";
                else if (id_unidade_origem != null && string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" E.ID_UNIDADE_ORIGEM = {id_unidade_origem}";

                if (!string.IsNullOrWhiteSpace(filtro))
                    filtro = " WHERE " + filtro;

                int count = _repository.GetCountAll(ibge, filtro);
                if (count == 1)
                    page = 0;
                else
                    page = page * pagesize;

                Response.Headers.Add("X-Total-Count", count.ToString());
                List<Envio> lista = _repository.GetAllPagination(ibge, page, pagesize, filtro);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetEnvioById/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<Envio> GetEnvioById([FromHeader] string ibge, [FromRoute]int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                Envio lista = _repository.GetEnvioById(ibge, id);
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
        public ActionResult Inserir([FromHeader] string ibge, [FromBody]Envio model)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                //INSERIR PAI E ITENS
                model = _repository.InsertOrUpdate(ibge, model);

                return Ok(model);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPut("ConfirmaEnvioTranferencia/{id}")]
        [ParameterTypeFilter("editar")]
        public ActionResult ConfirmaEnvioTranferencia([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                string unidade = HttpContext.Request.Headers["unidade"];
                var EnvioPai = _repository.GetEnvioById(ibge, id);
                var possuiitemindisponivel = _repository.ValidaEstoqueItensEnvio(ibge, id, Convert.ToInt32(unidade));
                if (possuiitemindisponivel > 0)
                {
                    var response = TrataErro.GetResponse("Existem itens com estoque indisponível ou com lote bloqueado.", true);
                    return BadRequest(response);
                }
                else if (EnvioPai.status == 0)
                {
                    _repository.UpdateStatusEnviado(ibge, id);
                }
                else
                {
                    var response = TrataErro.GetResponse("Esta transferência já foi enviada.", true);
                    return BadRequest(response);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetTranferenciaByUnidadeDestino/{id}")]
        [ParameterTypeFilter("receber_envio")]
        public ActionResult<List<Envio>> GetTranferenciaByUnidadeDestino([FromHeader] string ibge, [FromRoute] int? id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<Envio> itens = _repository.GetTranferenciaByUnidadeDestino(ibge, (int)id);
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