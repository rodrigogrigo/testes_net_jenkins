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
using RgCidadao.Api.Helpers;
using RgCidadao.Api.ViewModels.Endemias;
using RgCidadao.Domain.Entities.Endemias;
using RgCidadao.Domain.Repositories.Endemias;

namespace RgCidadao.Api.Areas.Endemias.Controllers
{
    [Route("api/Endemias/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("Endemias")]
    public class EstabelecimentoController : ControllerBase
    {
        private IEstabelecimentoRepository _repository;
        private IConfiguration _config;
        public EstabelecimentoController(IEstabelecimentoRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _config = configuration;
        }

        [HttpGet("GetAllPagination")]
        public ActionResult<List<Estabelecimento>> GetAllPagination([FromHeader]string ibge, int page,
                                                         int pagesize, string bairro, string logradouro, string quarteirao_logradouro, string sequencia_quarteirao,
                                                         string numero_logradouro, string sequencia_numero)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                string filtro = string.Empty;

                if (!string.IsNullOrWhiteSpace(bairro))
                {
                    filtro += !string.IsNullOrWhiteSpace(filtro) ? " AND " : " WHERE "; 
                    filtro += $@"BAI.CSI_CODBAI = '{bairro}'";
                }

                if (!string.IsNullOrWhiteSpace(logradouro))
                {
                    filtro += !string.IsNullOrWhiteSpace(filtro) ? " AND " : " WHERE ";
                    filtro += $@"LOG.CSI_CODEND = '{logradouro}'";
                }

                if (!string.IsNullOrWhiteSpace(quarteirao_logradouro))
                {
                    filtro += !string.IsNullOrWhiteSpace(filtro) ? " AND " : " WHERE ";
                    filtro += $@"VS.QUARTEIRAO_LOGRADOURO  = '{quarteirao_logradouro}'";
                }

                if (!string.IsNullOrWhiteSpace(sequencia_quarteirao))
                {
                    filtro += !string.IsNullOrWhiteSpace(filtro) ? " AND " : " WHERE ";
                    filtro += $@"VS.QUARTEIRAO_LOGRADOURO  = '{sequencia_quarteirao}'";
                }

                if (!string.IsNullOrWhiteSpace(numero_logradouro))
                {
                    filtro += !string.IsNullOrWhiteSpace(filtro) ? " AND " : " WHERE ";
                    filtro += $@"VS.NUMERO_LOGRADOURO = '{numero_logradouro}'";
                }

                if (!string.IsNullOrWhiteSpace(sequencia_numero))
                {
                    filtro += !string.IsNullOrWhiteSpace(filtro) ? " AND " : " WHERE ";
                    filtro += $@"VS.SEQUENCIA_NUMERO = '{sequencia_numero}'";
                }

                if (!string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" AND COALESCE(VS.EXCLUIDO, 'F') = 'F' ";
                else
                    filtro += $@" WHERE COALESCE(VS.EXCLUIDO, 'F') = 'F' ";

                int count = _repository.GetCountAll(ibge, filtro);
                if (count == 1)
                    page = 0;
                else
                    page = page * pagesize;

                Response.Headers.Add("X-Total-Count", count.ToString());
                List<Estabelecimento> lista = _repository.GetAllPagination(ibge, filtro, page, pagesize);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetAll")]
        public ActionResult<List<Estabelecimento>> GetAll([FromHeader] string ibge)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<Estabelecimento> itens = _repository.GetAll(ibge);
                return Ok(itens);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetEstabelecimentoByCiclo/{id_ciclo}")]
        public ActionResult<List<Estabelecimento>> GetEstabelecimentoByCiclo([FromHeader] string ibge, [FromRoute] int? id_ciclo)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<Estabelecimento> itens = _repository.GetEstabelecimentoByCiclo(ibge, (int)id_ciclo);
                return Ok(itens);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetFiltroAvancado")]
        public ActionResult<List<Estabelecimento>> GetFiltroAvancado([FromHeader]string ibge, [FromQuery] ParamFiltroEstabelecimentoViewModel model)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                string filtro = string.Empty;
                
                if (model.id != null)
                    filtro += $@" VS.ID = {model.id}";

                if (model.tipoImovel != null && !string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" AND VS.TIPO_IMOVEL = {model.tipoImovel}";
                else if (model.tipoImovel != null && string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" VS.TIPO_IMOVEL = {model.tipoImovel} ";

                if (!string.IsNullOrWhiteSpace(model.bairro) && !string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" AND BAI.CSI_CODBAI = {model.bairro}";
                else if (!string.IsNullOrWhiteSpace(model.bairro) && string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" BAI.CSI_CODBAI = {model.bairro}";

                if (!string.IsNullOrWhiteSpace(model.logradouro) && !string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" AND LOG.CSI_CODEND = {model.logradouro}";
                else if (!string.IsNullOrWhiteSpace(model.logradouro) && string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" LOG.CSI_CODEND = {model.logradouro}";

                if (!string.IsNullOrWhiteSpace(model.quarteirao) && !string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" AND VS.QUARTEIRAO_LOGRADOURO CONTAINING '{model.quarteirao}'";
                else if (!string.IsNullOrWhiteSpace(model.quarteirao) && string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" VS.QUARTEIRAO_LOGRADOURO CONTAINING '{model.quarteirao}'";

                if (model.sequencia_numero != null && !string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" AND VS.SEQUENCIA_NUMERO = {model.sequencia_numero}";
                else if (model.sequencia_numero != null && string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" VS.SEQUENCIA_NUMERO = {model.sequencia_numero}";

                if (model.sequencia_quarteirao != null && !string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" AND VS.SEQUENCIA_QUARTEIRAO = {model.sequencia_quarteirao}";
                else if (model.sequencia_quarteirao != null && string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" VS.SEQUENCIA_QUARTEIRAO = {model.sequencia_quarteirao}";

                if (model.numero != null && !string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" AND VS.NUMERO_LOGRADOURO = '{model.numero}'";
                else if (model.numero != null && string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" VS.NUMERO_LOGRADOURO = '{model.numero}'";

                if (!string.IsNullOrWhiteSpace(model.razao_social_nome) && !string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" AND VS.RAZAO_SOCIAL_NOME CONTAINING '{model.razao_social_nome}'";
                else if (!string.IsNullOrWhiteSpace(model.razao_social_nome) && string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" VS.RAZAO_SOCIAL_NOME CONTAINING '{model.razao_social_nome}'";

                if (!string.IsNullOrWhiteSpace(filtro))
                    filtro = $@" WHERE {filtro}";

                int count = _repository.GetCountAll(ibge, filtro);
                if (count == 1)
                    model.page = 0;
                else
                    model.page = model.page * model.pagesize;

                Response.Headers.Add("X-Total-Count", count.ToString());
                List<Estabelecimento> lista = _repository.GetAllPagination(ibge, filtro, model.page, model.pagesize);

                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetEstabelecimentoById/{id}")]
        public ActionResult<Estabelecimento> GetEstabelecimentoById([FromHeader] string ibge, [FromRoute]int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                Estabelecimento item = _repository.GetEstabelecimentoById(ibge, id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPost("Inserir")]
        public ActionResult Inserir([FromHeader] string ibge, [FromBody]Estabelecimento model)
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
        public ActionResult Editar([FromHeader] string ibge, [FromBody]Estabelecimento model, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                model.id = id;
                _repository.Update(ibge, model);
                return Ok(model);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpDelete("Excluir/{id}")]
        public ActionResult Excluir([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                _repository.Delete(ibge, id);
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