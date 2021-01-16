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
    public class ResultadoAmostraController : ControllerBase
    {
        private IResultadoAmostraRepository _repository;
        private IConfiguration _config;
        public ResultadoAmostraController(IResultadoAmostraRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _config = configuration;
        }

        [HttpGet("GetAllPagination")]
        public ActionResult<List<ResultadoAmostraViewModel>> GetAllPagination([FromHeader]string ibge, [FromQuery] ParamResultAmostraViewModel model)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                string filtro = string.Empty;

                string filtroAmostra = string.Empty;

                if (!string.IsNullOrWhiteSpace(model.agente))
                    filtro += $@" M.CSI_CODMED = {model.agente}";

                if (!string.IsNullOrWhiteSpace(model.unidade))
                {
                    if (this.verificarFiltro(filtro))
                    {
                        filtro += " AND ";
                    }
                    filtro += $@" UNI.CSI_CODUNI = {model.unidade}";

                    filtroAmostra += $@" AND UNI.CSI_CODUNI = {model.unidade}";
                }

                if (model.data_inicial != null && string.IsNullOrWhiteSpace(filtro))
                {
                    if (this.verificarFiltro(filtro))
                    {
                        filtro += " AND ";
                    }
                    filtro += $@" CAST(VI.DATA_HORA_ENTRADA AS DATE) >= '{model.data_inicial?.ToString("dd.MM.yyyy")}'";

                    filtroAmostra += $@" AND CAST(VI.DATA_HORA_ENTRADA AS DATE) >= '{model.data_inicial?.ToString("dd.MM.yyyy")}'";
                }

                if (model.data_final != null && string.IsNullOrWhiteSpace(filtro))
                {
                    if (this.verificarFiltro(filtro))
                    {
                        filtro += " AND ";
                    }
                    filtro += $@" CAST(VI.DATA_HORA_SAIDA AS DATE) <= '{model.data_final?.ToString("dd.MM.yyyy")}'";
           
                    filtroAmostra += $@" AND CAST(VI.DATA_HORA_SAIDA AS DATE) <= '{model.data_final?.ToString("dd.MM.yyyy")}'";

                }

                if (!string.IsNullOrWhiteSpace(model.ciclo) && string.IsNullOrWhiteSpace(filtro))
                {
                    if (this.verificarFiltro(filtro))
                    {
                        filtro += " AND ";
                    }
                    filtro += $@" C.ID = {model.ciclo}";

                    filtroAmostra+= $@" AND CIC.ID = {model.ciclo}";
                }

                if (this.verificarFiltro(filtro))
                {
                    filtro = $@"WHERE " + filtro;
                }

                int count = _repository.GetCountResultadoAmostra(ibge, filtro);
                if (count == 1)
                    model.page = 0;
                else
                    model.page = model.page * model.pagesize;

                Response.Headers.Add("X-Total-Count", count.ToString());
                List<ResultadoAmostraViewModel> lista = _repository.GetAllPagination(ibge, model.page, model.pagesize, filtro, filtroAmostra);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetResultadoColetaByProfissional")]
        public ActionResult<List<ResultadoColetaViewModel>> GetResultadoColetaByProfissional([FromHeader] string ibge, [FromQuery] int? id_profissional)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<ResultadoColetaViewModel> itens = _repository.GetResultadoColetaByProfissional(ibge, id_profissional);

                return Ok(itens);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetResultadoColetaByVisita")]
        public ActionResult<List<ResultadoColetaViewModel>> GetResultadoColetaByVisita([FromHeader] string ibge, [FromQuery] int? id_visita)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<ResultadoColetaViewModel> itens = _repository.GetResultadoColetaByVisita(ibge, id_visita);

                return Ok(itens);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetResultadoAmostraByVisita")]
        public ActionResult<List<Coleta>> GetResultadoAmostraByVisita([FromHeader] string ibge, [FromQuery] int? id_visita)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<Coleta> itens = _repository.GetResultadoAmostraByVisita(ibge, id_visita);

                return Ok(itens);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetResultadoAmostraByProfissional")]
        public ActionResult<List<Coleta>> GetResultadoAmostraByProfissional([FromHeader] string ibge, int id_profissional, int? id_ciclo, DateTime? data_inicial, DateTime? data_final)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<Coleta> itens = _repository.GetResultadoAmostraByProfissional(ibge, id_profissional, id_ciclo, data_inicial, data_final);

                return Ok(itens);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPut("Editar/{id}")]
        public ActionResult Editar([FromHeader] string ibge, [FromBody] ColetaResultado model, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                model.id = id;
                _repository.UpdateColetaResultado(ibge, model);
                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPost("Inserir")]
        public ActionResult Inserir([FromHeader] string ibge, [FromBody] List<ColetaResultado> model)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                foreach (var item in model)
                {
                    item.id = _repository.GetNewIdResultadoAmostra(ibge);
                    _repository.InsertColetaResultado(ibge, item);

                }
                return Ok(model);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpDelete("Excluir/{id}")] //item por item
        public ActionResult Excluir([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                _repository.DeleteColetaResultado(ibge, id);

                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpDelete("ExcluirItemByColeta/{id}")] //itens por coleta
        public ActionResult ExcluirItemByColeta([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                _repository.DeleteColetaResultadoByColeta(ibge, id);

                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetColetaResultadoByColeta/{id}")] //lista itens por coleta
        public ActionResult<List<ColetaResultado>> GetColetaResultadoByColeta([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<ColetaResultado> itens = _repository.GetColetaResultadoByColeta(ibge, id);

                return Ok(itens);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        public Boolean verificarFiltro(string filtro)
        {
            if (!string.IsNullOrWhiteSpace(filtro))
            {
                return true;
            }
            return false;
        }
    }
}