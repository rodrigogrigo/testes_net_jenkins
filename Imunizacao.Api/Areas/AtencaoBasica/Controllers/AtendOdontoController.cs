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
using RgCidadao.Domain.Entities.AtencaoBasica;
using RgCidadao.Domain.Repositories.AtencaoBasica;
using RgCidadao.Domain.ViewModels.AtencaoBasica;

namespace RgCidadao.Api.Areas.AtencaoBasica.Controllers
{
    [Route("api/AtencaoBasica/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("AtencaoBasica")]
    public class AtendOdontoController : ControllerBase
    {
        private IConfiguration _config;
        private IAtendOdontoRepository _repository;
        public AtendOdontoController(IConfiguration config, IAtendOdontoRepository repository)
        {
            _repository = repository;
            _config = config;
        }

        [HttpGet("GetAllPagination")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<AtendOdontoViewModel>> GetAllPagination([FromHeader]string ibge, int page,
                                                                                      int pagesize, string search, string fields, DateTime? data_inicial, DateTime? data_final)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                string filtro = string.Empty;

                if (!string.IsNullOrWhiteSpace(search))
                {
                    if (fields != null && fields.Split(",").Length > 0 && fields.Split(",")[0] != null)
                        filtro += Helper.GetFiltroInicial(fields, search);
                    else
                    {

                        filtro += $@" (MED.CSI_NOMMED CONTAINING '{search}' OR
                                       UNI.CSI_NOMUNI CONTAINING '{search}')";
                    }
                }

                if (data_inicial != null && !string.IsNullOrWhiteSpace(filtro))
                    filtro += $" AND  CAST(ATEN.DATA AS DATE) >= '{data_inicial?.ToString("dd.MM.yyyy")}' ";
                else if (data_inicial != null && string.IsNullOrWhiteSpace(filtro))
                    filtro += $" CAST(ATEN.DATA AS DATE) >= '{data_inicial?.ToString("dd.MM.yyyy")}' ";

                if (data_final != null && !string.IsNullOrWhiteSpace(filtro))
                    filtro += $" AND  CAST(ATEN.DATA AS DATE) <= '{data_final?.ToString("dd.MM.yyyy")}' ";
                else if (data_final != null && string.IsNullOrWhiteSpace(filtro))
                    filtro += $" CAST(ATEN.DATA AS DATE) <= '{data_final?.ToString("dd.MM.yyyy")}' ";

                if (!string.IsNullOrWhiteSpace(filtro))
                    filtro = " WHERE " + filtro;

                int count = _repository.GetCountAll(ibge, filtro);
                if (count == 1)
                    page = 0;
                else
                    page = page * pagesize;

                Response.Headers.Add("X-Total-Count", count.ToString());
                List<AtendOdontoViewModel> lista = _repository.GetAllPagination(ibge, page, pagesize, filtro);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetById/{id}")]
        public ActionResult<AtendOdontoIndividual> GetById([FromHeader]string ibge, [FromRoute]int? id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                AtendOdontoIndividual itens = _repository.GetAtendOdontoById(ibge, (int)id);
                return Ok(itens);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPost("Inserir")]
        public ActionResult Inserir([FromHeader] string ibge, [FromBody] AtendOdontoIndividual model)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                if (model.id == null)
                    model.id = _repository.GetNewId(ibge);

                foreach (var item in model.itens)
                {
                    if (item.id == null)
                        item.id = _repository.GetNewIdItem(ibge);
                }
                _repository.UpdateOrInsert(ibge, model);

                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPut("Editar/{id}")]
        public ActionResult Editar([FromHeader] string ibge, [FromBody] AtendOdontoIndividual model, [FromRoute] int? id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                if (id == null)
                    model.id = _repository.GetNewId(ibge);
                else
                    model.id = id;

                foreach (var item in model.itens)
                {
                    if (item.id == null)
                        item.id = _repository.GetNewIdItem(ibge);
                }
                _repository.UpdateOrInsert(ibge, model);

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
                //valida se ficha já não foi exportada
                var itensdopai = _repository.GetAtendOdontoItensByPai(ibge, (int)id);

                if (itensdopai.Any(x => x.id_esus_exportacao_item != null))
                    throw new Exception("Não é possível excluir esse registro porque existem itens exportados!");

                _repository.ExcluirItensByPai(ibge, (int)id);
                _repository.ExcluirItemPai(ibge, (int)id);

                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpDelete("ExcluirItemById/{id}")]
        public ActionResult ExcluirItemById([FromHeader] string ibge, [FromRoute] int? id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));

                //valida se ficha já não foi exportada
                var itensdopai = _repository.GetAtendOdontoItemById(ibge, (int)id);

                if (itensdopai?.id_esus_exportacao_item != null)
                    throw new Exception("Não é possível excluir esse registro porque existem itens exportados!");

                _repository.ExcluirItemById(ibge, (int)id);
                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetProcOdontoIndividualizado")]
        public ActionResult GetProcOdontoIndividualizado([FromHeader]string ibge, int page,
                                                                int pagesize, string search, string cbo)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                string filtro = string.Empty;
                if (!string.IsNullOrWhiteSpace(search))
                {
                    string filtronumero = string.Empty;
                    if (Helper.soContemNumeros(search))
                        filtronumero = $@" CAST(TAB.CSI_CODPROC AS VARCHAR(20)) CONTAINING {search} OR";

                    filtro += $@"WHERE {filtronumero}
                                       TAB.NOME CONTAINING '{search}'";
                }

                int count = _repository.GetCountProcOdontoIndividualizado(ibge, string.Empty, cbo);

                if (count == 1)
                    page = 0;
                else
                    page = page * pagesize;

                Response.Headers.Add("X-Total-Count", count.ToString());
                List<AtendOdontoProcedimentoViewModel> lista = _repository.GetProcOdontoIndividualizado(ibge, filtro, cbo, page, pagesize);
                return Ok(lista);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}