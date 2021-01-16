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
using RgCidadao.Domain.ViewModels.Endemias;

namespace RgCidadao.Api.Areas.Endemias.Controllers
{
    [Route("api/Endemias/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("Endemias")]
    public class VisitaController : ControllerBase
    {
        public IVisitaRepository _repository;
        private IConfiguration _config;
        public VisitaController(IVisitaRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _config = configuration;
        }

        [HttpGet("GetAllPagination")]
        public ActionResult<List<Visita>> GetAllPagination([FromHeader] string ibge, int page,
                                                          int pagesize, string search, string fields, DateTime? datainicial, DateTime? datafinal, string num_ciclo)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                string filtro = string.Empty;
                if (datainicial != null)
                    filtro += $@" AND CAST(DATA_HORA_ENTRADA AS DATE) >= '{datainicial?.ToString("dd.MM.yyyy")}'";

                if (datafinal != null)
                    filtro += $@" AND CAST(DATA_HORA_ENTRADA AS DATE) <= '{datafinal?.ToString("dd.MM.yyyy")}'";

                if (!string.IsNullOrWhiteSpace(num_ciclo))
                    filtro += $@" AND EC.NUM_CICLO = '{num_ciclo}'";

                if (!string.IsNullOrWhiteSpace(search))
                {
                    if (fields != null && fields.Split(",").Length > 0 && fields.Split(",")[0] != null)
                        filtro += Helper.GetFiltro(fields, search);
                    else
                        filtro += $@" AND (I.QUARTEIRAO_LOGRADOURO = '{search}' OR
                                           B.CSI_NOMBAI CONTAINING '{search}' OR
                                           I.NOME_FANTASIA_APELIDO CONTAINING '{search}' OR
                                           I.RAZAO_SOCIAL_NOME CONTAINING '{search}')";
                }

                int count = _repository.GetCountAll(ibge, filtro);
                if (count == 1)
                    page = 0;
                else
                    page = page * pagesize;

                Response.Headers.Add("X-Total-Count", count.ToString());
                List<Visita> lista = _repository.GetAllPagination(ibge, page, pagesize, filtro).ToList();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetFiltroAvancado")]
        public ActionResult<List<Visita>> GetFiltroAvancado([FromHeader] string ibge, [FromQuery] FiltroAvancVisitaViewModel model)
        {
            try
            {
                string filtro = string.Empty;
                #region filtros

                if (model.bairro != null)
                    filtro += $@"AND B.CSI_CODBAI = {model.bairro} ";

                if (model.logradouro != null)
                    filtro += $@" AND L.CSI_CODEND = {model.logradouro} ";

                if (!string.IsNullOrWhiteSpace(model.quarteirao_logradouro))
                    filtro += $@" AND I.QUARTEIRAO_LOGRADOURO = {model.quarteirao_logradouro}";

                if (model.datainicial != null)
                    filtro += $@" AND CAST(DATA_HORA_ENTRADA AS DATE) >= '{model.datainicial?.ToString("dd.MM.yyyy")}'";

                if (model.datafinal != null)
                    filtro += $@" AND CAST(DATA_HORA_ENTRADA AS DATE) <= '{model.datafinal?.ToString("dd.MM.yyyy")}'";

                if (model.agente != null)
                    filtro += $@" AND M.CSI_CODMED = {model.agente}";

                if (model.atividade != null)
                    filtro += $@" AND VI.ATIVIDADE = {model.atividade}";

                if (model.tipoImovel != null)
                    filtro += $@" AND I.TIPO_DOMICILIO = {model.tipoImovel}";

                if (model.tipo_visita != null)
                    filtro += $@" AND VI.TIPO_VISITA = {model.tipo_visita}";

                if (model.desfecho != null)
                    filtro += $@" AND VI.DESFECHO = {model.desfecho}";

                if (model.encontroufoco != null)
                    filtro += $@" AND VI.ENCONTROU_FOCO = {model.encontroufoco}";

                if (!string.IsNullOrWhiteSpace(model.nomefantasia))
                    filtro += $@" AND I.NOME_FANTASIA_APELIDO CONTAINING '{model.nomefantasia}'";

                if (!string.IsNullOrWhiteSpace(model.razaosocial))
                    filtro += $@" AND I.RAZAO_SOCIAL_NOME CONTAINING '{model.razaosocial}'";

                if (!string.IsNullOrWhiteSpace(model.num_ciclo))
                    filtro += $@" AND EC.NUM_CICLO CONTAINING '{model.num_ciclo}'";

                #endregion

                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                int count = _repository.GetCountAll(ibge, filtro);
                if (count == 1)
                    model.page = 0;
                else
                    model.page = model.page * model.pagesize;

                Response.Headers.Add("X-Total-Count", count.ToString());
                List<Visita> lista = _repository.GetAllPagination(ibge, (int)model.page, (int)model.pagesize, filtro);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPost("Inserir")]
        public ActionResult Inserir([FromHeader] string ibge, [FromBody] Visita model)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                model.id = _repository.GetNewIdVisita(ibge);
                _repository.InsertVisita(ibge, model);

                foreach (var item in model.itens)
                {
                    item.id_visita = model.id;
                    item.id = _repository.GetNewIdColeta(ibge);
                    _repository.InsertColeta(ibge, item);
                }

                return Ok(model);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPut("Editar/{id}")]
        public ActionResult Editar([FromHeader] string ibge, [FromBody] Visita model, [FromRoute] int? id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                model.id = id;
                _repository.UpdateVisita(ibge, model);

                foreach (var item in model.itens)
                {
                    if (item.id != null)
                        _repository.UpdateColeta(ibge, item);
                    else
                    {
                        item.id_visita = model.id;
                        item.id = _repository.GetNewIdColeta(ibge);
                        _repository.InsertColeta(ibge, item);
                    }
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
        public ActionResult Excluir([FromHeader] string ibge, [FromRoute] int? id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                var itenscoleta = _repository.GetColetaByVisita(ibge, (int)id);
                foreach (var item in itenscoleta)
                {
                    _repository.DeleteColeta(ibge, item.id);
                }

                _repository.DeleteVisita(ibge, (int)id);
                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetVisitaById/{id}")]
        public ActionResult<Visita> GetVisitaById([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                Visita item = _repository.GetVisitaById(ibge, id);
                List<Coleta> itens = _repository.GetColetaByVisita(ibge, id);
                if (itens.Count != 0)
                    item.itens.AddRange(itens);

                return Ok(item);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpDelete("ExcluirItem/{id}")]
        public ActionResult ExcluirItem([FromHeader] string ibge, [FromRoute] int? id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                _repository.DeleteColeta(ibge, id);

                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetVisitaByEstabelecimento")]
        public ActionResult<Visita> GetVisitaByEstabelecimento([FromHeader] string ibge, DateTime? datainicial, DateTime? datafinal, int? estabelecimento, int? id_ciclo)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                Visita item = _repository.GetVisitaByEstabelecimento(ibge, datainicial, datafinal, estabelecimento, id_ciclo);

                return Ok(item);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetQuarteiraoEstabelecimentoByBairro")]
        public ActionResult<List<VisitaBairroViewModel>> GetQuarteiraoEstabelecimentoByBairro([FromHeader] string ibge, int? id_bairro, int? id_ciclo, string quarteirao,
                                                                                            int? sequencia_quarteirao)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));

                var filtro = string.Empty;


                if (!string.IsNullOrWhiteSpace(quarteirao))
                {
                    filtro += $@" AND EST.QUARTEIRAO_LOGRADOURO = '{quarteirao}'";
                }

                if (sequencia_quarteirao != null)
                {
                    filtro += $@" AND EST.SEQUENCIA_QUARTEIRAO = {sequencia_quarteirao}";
                }

                List<VisitaBairroViewModel> lista = _repository.GetQuarteiraoEstabelecimentoByBairro(ibge, (int)id_bairro, (int)id_ciclo, filtro);

                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetVisitasByCiclo/{id}")]
        public ActionResult<List<Visita>> GetVisitasByCiclo([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<Visita> lista = _repository.GetVisitasByCiclo(ibge, id);

                return Ok(lista);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}