using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using RgCidadao.Api.Helpers;
using RgCidadao.Domain.Entities;
using RgCidadao.Domain.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RgCidadao.Domain.Repositories.Cadastro;
using RgCidadao.Domain.Entities.Cadastro;
using Microsoft.Extensions.Configuration;
using RgCidadao.Api.Filters;

namespace RgCidadao.Api.Controllers
{
    [Route("api/Cadastro/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("Cadastro")]
    public class FeriadoController : ControllerBase
    {
        private readonly IFeriadoRepository _feriadoRepository;
        private IConfiguration _config;
        public FeriadoController(IFeriadoRepository feriadorepository, IConfiguration configuration)
        {
            _feriadoRepository = feriadorepository;
            _config = configuration;
        }

        [HttpGet("GetAllPagination")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<Domain.Entities.Cadastro.Feriado>> GetAllPagination([FromHeader]string ibge, int page,
                                                                                            int pagesize, string search, string fields)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                string filtro = string.Empty;
                if (!string.IsNullOrWhiteSpace(search))
                {
                    if (fields != null && fields.Split(",").Length > 0 && fields.Split(",")[0] != null)
                    {
                        filtro += Helpers.Helper.GetFiltroInicial(fields, search);
                    }
                    else
                    {
                        var stringcod = string.Empty;
                        bool data = false;
                        try
                        {
                            var dt = Convert.ToDateTime(search);
                            data = true;
                        }
                        catch (Exception)
                        {
                            data = false;
                        }

                        if (data)
                            stringcod = $" CSI_DATA = '{search}' OR ";

                        filtro += $@"  {stringcod}
                                            CSI_DESCRICAO CONTAINING '{search}' OR
                                            CSI_OBS CONTAINING '{search}'";
                    }
                }

                if (!string.IsNullOrWhiteSpace(filtro))
                    filtro = $" WHERE " + filtro;

                int count = _feriadoRepository.GetCountAll(ibge, filtro);
                if (count == 1)
                    page = 0;
                else
                    page = page * pagesize;

                Response.Headers.Add("X-Total-Count", count.ToString());
                List<Feriado> lista = _feriadoRepository.GetAllPagination(ibge, page, pagesize, filtro);

                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetAll")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<Feriado>> GetAll([FromHeader]string ibge)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<Feriado> lista = _feriadoRepository.GetAll(ibge);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetFeriadoById")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<Domain.Entities.Cadastro.Feriado> GetFeriadoById([FromHeader]string ibge, [FromQuery] DateTime? date)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                Feriado model = _feriadoRepository.GetFeriadoById(ibge, date);
                return Ok(model);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPost("Inserir")]
        [ParameterTypeFilter("inserir")]
        public ActionResult Inserir([FromHeader]string ibge, [FromBody]Feriado model)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                _feriadoRepository.Inserir(ibge, model);

                return Ok(model);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPut("Editar/{data}")]
        [ParameterTypeFilter("editar")]
        public ActionResult Editar([FromHeader]string ibge, [FromBody]Feriado model, [FromRoute]DateTime? data)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                model.csi_data = data;
                _feriadoRepository.Atualizar(ibge, model);
                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpDelete("Excluir/{data}")]
        [ParameterTypeFilter("excluir")]
        public ActionResult Excluir([FromHeader]string ibge, [FromRoute]DateTime? data)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                _feriadoRepository.Deletar(ibge, data);
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