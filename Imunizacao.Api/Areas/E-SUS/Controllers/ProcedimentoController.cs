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
using RgCidadao.Domain.Commands.E_SUS;
using RgCidadao.Domain.Entities.E_SUS;
using RgCidadao.Domain.Repositories.E_SUS;

namespace RgCidadao.Api.Areas.E_SUS.Controllers
{
    [Route("api/E-SUS/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("E-SUS")]
    public class ProcedimentoController : ControllerBase
    {
        public IProcedimentoRepository _repository;
        private IConfiguration _config;
        public ProcedimentoController(IProcedimentoRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _config = configuration;
        }

        [HttpGet("GetAllPagination")]
        public ActionResult<List<ProcedimentoAvulso>> GetAllPagination([FromHeader]string ibge, int page,
                                                                                            int pagesize, string search, string fields)
        {
            try
            {
                string filtro = string.Empty;
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));

                //rever filtros com front
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
                            stringcod = $" CSI_CODPAC CONTAINING '{search}' OR ";

                        filtro += $@"  {stringcod}
                                     CSI_NOMPAC CONTAINING '{search}' OR 
                                     CSI_CODMED CONTAINING '{search}' OR
                                     CSI_NOMMED CONTAINING '{search}' OR 
                                     CSI_DATA CONTAINING '{search}' OR
                                     CSI_NOMUSU CONTAINING '{search}'
                                     CSI_DATAINC CONTAINING '{search}'
                                     CSI_CONTROLE CONTAINING '{search}'";
                    }
                }

                if (!string.IsNullOrWhiteSpace(filtro))
                    filtro = $" WHERE " + filtro;

                int count = _repository.GetCountAll(ibge, filtro);
                if (count == 1)
                    page = 0;
                else
                    page = page * pagesize;

                var lista = _repository.GetAllPagination(ibge, filtro, page, pagesize);
                Response.Headers.Add("X-Total-Count", count.ToString());
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetProcEnfermagemById/{id}")]
        public ActionResult<ProcedimentoAvulso> GetProcEnfermagemById([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                var lista = _repository.GetProcEnfermagemById(ibge, id);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetProcEnfermagemItemByPaiProc")]
        public ActionResult<ProcedimentoAvulso> GetProcEnfermagemItemByPaiProc([FromHeader] string ibge, int id, int proc)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                var lista = _repository.GetProcEnfermagemItemByPaiProc(ibge, id, proc);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPost("Inserir")]
        public ActionResult<ProcedimentoAvulso> Inserir([FromHeader] string ibge, [FromBody] ProcedimentoAvulso model)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                //generator
                var id = _repository.GetNewId(ibge);
                model.csi_controle = id;

                _repository.Inserir(ibge, model);
                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPut("Editar/{id}")]
        public ActionResult<ProcedimentoAvulso> Editar([FromHeader] string ibge, [FromBody] ProcedimentoAvulso model, [FromRoute]int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                model.csi_controle = id;
                _repository.Editar(ibge, model);
                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPost("InserirItem/{id}")]
        public ActionResult<ProcedimentoAvulso> InserirItem([FromHeader] string ibge, [FromBody] ProcedimentoAvulsoItem model)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                model.id_sequencial = _repository.GetNewIdItem(ibge);
                model.uuid = _repository.GetNewUuid(ibge);
                _repository.InserirItem(ibge, model);
                return Ok(model);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPut("EditarItem/{id}")]
        public ActionResult<ProcedimentoAvulso> EditarItem([FromHeader] string ibge, [FromBody] ProcedimentoAvulsoItem model, [FromRoute]int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                model.csi_controle = id;
                _repository.EditarItem(ibge, model);
                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpDelete("Excluir/{id}")]
        public ActionResult<ProcedimentoAvulso> Excluir([FromHeader] string ibge, [FromRoute]int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                _repository.Excluir(ibge, id);
                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpDelete("ExcluirItem")]
        public ActionResult<ProcedimentoAvulso> ExcluirItem([FromHeader] string ibge, int id, int idproc)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                _repository.ExcluirItem(ibge, id, idproc);
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