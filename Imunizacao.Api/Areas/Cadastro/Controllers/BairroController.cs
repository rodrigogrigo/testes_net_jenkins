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
using RgCidadao.Domain.Entities.Cadastro;
using RgCidadao.Domain.Repositories.Cadastro;

namespace RgCidadao.Api.Areas.Cadastro.Controllers
{
    [Route("api/Cadastro/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("Cadastro")]
    public class BairroController : ControllerBase
    {
        public IBairroRepository _repository;
        private IConfiguration _config;
        public BairroController(IBairroRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _config = configuration;
        }

        [HttpGet("GetAllPagination")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<Bairro>> GetAllPagination([FromHeader]string ibge, int page,
                                                                  int pagesize, string search, string fields)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                string filtro = string.Empty;

                if (!string.IsNullOrWhiteSpace(search))
                {
                    if (fields != null && fields.Split(",").Length > 0 && fields.Split(",")[0] != null)
                        filtro += " WHERE " + Helper.GetFiltroInicial(fields, search);

                    else
                        filtro += $@" WHERE CSI_NOMBAI CONTAINING '{search}'";
                }

                int count = _repository.GetCountAll(ibge, filtro);
                if (count == 1)
                    page = 0;
                else
                    page = page * pagesize;

                Response.Headers.Add("X-Total-Count", count.ToString());
                List<Bairro> lista = _repository.GetAllPagination(ibge, filtro, page, pagesize);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetBairroById")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<Bairro> GetBairroById([FromHeader] string ibge, int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                Bairro bairro = _repository.GetById(ibge, id);

                return Ok(bairro);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetAll")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<Bairro>> GetAll([FromHeader] string ibge)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<Bairro> bairro = _repository.GetAll(ibge);

                return Ok(bairro);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetBairroByIbge")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<Bairro>> GetBairroByIbge([FromHeader] string ibge)
        {
            try
            {
                string codibge = ibge;
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<Bairro> bairro = _repository.GetBairroByIbge(ibge, codibge);

                return Ok(bairro);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }
    }
}