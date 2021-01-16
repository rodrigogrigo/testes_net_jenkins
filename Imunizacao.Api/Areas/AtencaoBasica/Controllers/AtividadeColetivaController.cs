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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RgCidadao.Api.Areas.AtencaoBasica.Controllers
{
    [Route("api/AtencaoBasica/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("AtencaoBasica")]
    public class AtividadeColetivaController : ControllerBase
    {
        public IAtividadeColetivaRepository _repository;
        private IConfiguration _config;
        public AtividadeColetivaController(IAtividadeColetivaRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _config = configuration;
        }

        [HttpGet("GetAll")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<AtividadeColetivaViewModel>> GetAll([FromHeader]string ibge)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                List<AtividadeColetivaViewModel> itens = _repository.GetAll(ibge);

                return Ok(itens);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetAllPagination")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<AtividadeColetivaViewModel>> GetAllPagination([FromHeader]string ibge, int page,
                                                          int pagesize, string search, string fields, int usuario)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                string filtro = string.Empty;

                if (!string.IsNullOrWhiteSpace(search))
                {
                    if (fields != null && fields.Split(",").Length > 0 && fields.Split(",")[0] != null)
                        filtro += Helper.GetFiltro(fields, search);
                    else
                    {
                        var filtronumero = string.Empty;
                        if (Helper.soContemNumeros(search))
                            filtronumero = $@" AC.ID = {search}";

                        filtro += $@" AND( {filtronumero}
                                          EQ.DESCRICAO_EXIBIR CONTAINING '{search}' OR
                                          M.CSI_NOMMED CONTAINING '{search}' OR
                                          UNI.CSI_NOMUNI = '{search}')";
                    }
                }

                int count = _repository.GetCountAll(ibge, filtro, usuario);
                if (count == 1)
                    page = 0;
                else
                    page = page * pagesize;

                Response.Headers.Add("X-Total-Count", count.ToString());
                List<AtividadeColetivaViewModel> lista = _repository.GetAllPagination(ibge, filtro, page, pagesize, usuario);
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
        public ActionResult Inserir([FromHeader] string ibge, [FromBody] AtividadeColetiva model)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
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
        public ActionResult Editar([FromHeader] string ibge, [FromBody] AtividadeColetiva model, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
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
        [ParameterTypeFilter("excluir")]
        public ActionResult Excluir([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));

                //exclui itens antes de excluir atividade coletiva
                _repository.DeleteParticipanteByAtividade(ibge, id);
                _repository.DeleteProfissionalByAtividade(ibge, id);

                _repository.Excluir(ibge, id);

                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetAtividadeColetivaById/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<AtividadeColetivaEditViewModel> GetAtividadeColetivaById([FromHeader]string ibge, [FromRoute]int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                AtividadeColetivaEditViewModel lista = _repository.GetAtividadeColetivaById(ibge, id);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }      

        [HttpGet("GetProfissionalByAtividadeColetiva/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<ProfissionalAtivColetivaViewModel>> GetProfissionalByAtividadeColetiva([FromHeader]string ibge, [FromRoute]int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                List<ProfissionalAtivColetivaViewModel> item = _repository.GetProfissionalByAtividadeColetiva(ibge, id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetPacienteByAtividadeColetiva/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<PacienteAtivColetivaViewModel>> GetPacienteByAtividadeColetiva([FromHeader]string ibge, [FromRoute]int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                List<PacienteAtivColetivaViewModel> item = _repository.GetPacienteByAtividadeColetiva(ibge, id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpDelete("DeleteProfissional/{id}")]
        [ParameterTypeFilter("excluir")]
        public ActionResult DeleteProfissional([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                _repository.DeleteProfissional(ibge, id);
                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpDelete("DeleteParticipante/{id}")]
        [ParameterTypeFilter("excluir")]
        public ActionResult DeleteParticipante([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                _repository.DeleteParticipante(ibge, id);
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