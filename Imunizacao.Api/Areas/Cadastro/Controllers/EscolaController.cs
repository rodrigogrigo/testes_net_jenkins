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
using RgCidadao.Api.ViewModels.Cadastro;
using RgCidadao.Domain.Entities.Cadastro;
using RgCidadao.Domain.Repositories.Cadastro;

namespace RgCidadao.Api.Areas.Cadastro.Controllers
{
    [Route("api/Cadastro/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("Cadastro")]
    public class EscolaController : ControllerBase
    {
        public IEscolaRepository _repository;
        private IConfiguration _config;
        public EscolaController(IEscolaRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _config = configuration;
        }

        [HttpGet("GetAllPagination")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<Escola>> GetAllPagination([FromHeader]string ibge, int page,
                                                                                   int pagesize, string inep, string nome)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                string filtro = string.Empty;

                if (!string.IsNullOrWhiteSpace(nome))
                    filtro = $@" WHERE E.NOME CONTAINING '{nome}' ";

                if (!string.IsNullOrWhiteSpace(inep) && string.IsNullOrWhiteSpace(filtro))
                    filtro = $@" WHERE E.INEP CONTAINING '{inep}' ";
                else if (!string.IsNullOrWhiteSpace(inep) && !string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" AND E.INEP CONTAINING '{inep}' ";

                int count = _repository.GetCountAll(ibge, filtro);
                if (count == 1)
                    page = 0;
                else
                    page = page * pagesize;

                Response.Headers.Add("X-Total-Count", count.ToString());
                List<Escola> lista = _repository.GetAllPagination(ibge, page, pagesize, filtro);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetFiltroAvancado")]
        public ActionResult<List<Escola>> GetFiltroAvancado([FromHeader]string ibge, [FromQuery] ParamEscolaViewModel model)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                string filtro = string.Empty;
                if (model.id != null)
                    filtro += $@" E.ID = {model.id}";

                if (!string.IsNullOrWhiteSpace(model.nome) && !string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" AND E.NOME CONTAINING '{model.nome}'";
                else if (!string.IsNullOrWhiteSpace(model.nome) && string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" E.NOME CONTAINING '{model.nome}'";

                if (!string.IsNullOrWhiteSpace(model.inep) && !string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" AND E.INEP CONTAINING '{model.inep}'";
                else if (!string.IsNullOrWhiteSpace(model.inep) && string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" E.INEP CONTAINING '{model.inep}'";

                if (!string.IsNullOrWhiteSpace(model.uf) && !string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" AND CID.CSI_SIGEST CONTAINING '{model.uf}'";
                else if (!string.IsNullOrWhiteSpace(model.uf) && string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" CID.CSI_SIGEST CONTAINING '{model.uf}'";

                if (!string.IsNullOrWhiteSpace(model.cidade) && !string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" AND CID.CSI_NOMCID CONTAINING '{model.cidade}'";
                else if (!string.IsNullOrWhiteSpace(model.cidade) && string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" CID.CSI_NOMCID CONTAINING '{model.cidade}'";

                if (!string.IsNullOrWhiteSpace(model.logradouro) && !string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" AND L.CSI_NOMEND CONTAINING '{model.logradouro}'";
                else if (!string.IsNullOrWhiteSpace(model.logradouro) && string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" L.CSI_NOMEND CONTAINING '{model.logradouro}'";

                if (!string.IsNullOrWhiteSpace(model.cep) && !string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" AND L.CSI_CODEND CONTAINING '{model.cep}'";
                else if (!string.IsNullOrWhiteSpace(model.cep) && string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" L.CSI_CODEND CONTAINING '{model.cep}'";

                if (!string.IsNullOrWhiteSpace(model.telefone) && !string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" AND E.TELEFONE CONTAINING '{model.telefone}'";
                else if (!string.IsNullOrWhiteSpace(model.telefone) && string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" E.TELEFONE CONTAINING '{model.telefone}'";

                if (!string.IsNullOrWhiteSpace(filtro))
                    filtro = $@" WHERE {filtro}";

                int count = _repository.GetCountAll(ibge, filtro);
                if (count == 1)
                    model.page = 0;
                else
                    model.page = model.page * model.pagesize;

                Response.Headers.Add("X-Total-Count", count.ToString());
                List<Escola> lista = _repository.GetAllPagination(ibge, (int)model.page, (int)model.pagesize, filtro);

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
        public ActionResult Inserir([FromHeader] string ibge, [FromBody] Escola model)
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
        public ActionResult Editar([FromHeader] string ibge, [FromBody] Escola model, [FromRoute] int id)
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

        [HttpGet("GetConsumoAlimentarById/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<Escola> GetEscolaById([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                Escola item = _repository.GetEscolaById(ibge, id);
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
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                _repository.Delete(ibge, id);
                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }


        [HttpGet("GetAll")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<Escola>> GetAll([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                List<Escola> itens = _repository.GetAll(ibge);
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