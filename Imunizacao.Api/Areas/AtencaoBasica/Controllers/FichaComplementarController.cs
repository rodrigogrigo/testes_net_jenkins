using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using RgCidadao.Api.Filters;
using RgCidadao.Api.Helpers;
using RgCidadao.Domain.ViewModels.AtencaoBasica;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using RgCidadao.Domain.Repositories.AtencaoBasica;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace RgCidadao.Api.Areas.AtencaoBasica.Controllers
{
    [Route("api/AtencaoBasica/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("AtencaoBasica")]
    public class FichaComplementarController : ControllerBase
    {
        public IFichaComplementarRepository _repository;
        private IConfiguration _config;
        public FichaComplementarController(IFichaComplementarRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _config = configuration;
        }

        [HttpGet("GetAllPagination")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<FichaComplementarViewModel>> GetAllPagination([FromHeader] string ibge, int page,
                                                          int pagesize, int usuario, int? idPaciente, int? idProfissional, int? idUnidade, DateTime? dataInicial, DateTime? dataFinal)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                string filtro = string.Empty;

                if (idPaciente != null)
                {
                    if (this.verificarFiltro(filtro))
                    {
                        filtro += " AND ";
                    }
                    filtro += $@" PAC.CSI_CODPAC = {idPaciente} ";
                }

                if (idProfissional != null)
                {
                    if (this.verificarFiltro(filtro))
                    {
                        filtro += " AND ";
                    }
                    filtro += $@" MED.CSI_CODMED = {idProfissional} ";
                }

                if (idUnidade != null)
                {
                    if (this.verificarFiltro(filtro))
                    {
                        filtro += " AND ";
                    }
                    filtro += $@" UNI.CSI_CODUNI = {idUnidade} ";
                }

                if ((dataInicial!=null) && (dataFinal!= null))
                {
                    if (this.verificarFiltro(filtro))
                    {
                        filtro += " AND ";
                    }
                    filtro += $@" (CAST(FC.DATA AS DATE) >= '{dataInicial?.ToString("dd.MM.yyyy")}' AND CAST(FC.DATA AS DATE) <= '{dataFinal?.ToString("dd.MM.yyyy")}') ";
                }
                else if (dataInicial != null)
                {
                    if (this.verificarFiltro(filtro))
                    {
                        filtro += " AND ";
                    }
                    filtro += $@"CAST(FC.DATA AS DATE) >= '{dataInicial?.ToString("dd.MM.yyyy")}'";
                }
                else if (dataFinal!= null)
                {
                    if (this.verificarFiltro(filtro))
                    {
                        filtro += " AND ";
                    }
                    filtro += $@"CAST(FC.DATA AS DATE) <= '{dataFinal?.ToString("dd.MM.yyyy")}'";
                }

                if(this.verificarFiltro(filtro))
                {
                    filtro = $@" WHERE ( {filtro} )";
                }

                int count = _repository.GetCountAll(ibge, filtro);
                if (count == 1)
                    page = 0;
                else
                    page = page * pagesize;

                Response.Headers.Add("X-Total-Count", count.ToString());
                List<FichaComplementarViewModel> lista = _repository.GetAllPagination(ibge, filtro, page, pagesize, usuario);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetFichaComplementarById/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<FichaComplementarViewModel> GetFichaComplementarById([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                FichaComplementarViewModel lista = _repository.GetFichaComplementarById(ibge, id);
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
        public ActionResult Inserir([FromHeader] string ibge, [FromBody] FichaComplementarViewModel model)
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
        public ActionResult Editar([FromHeader] string ibge, [FromBody] FichaComplementarViewModel model, [FromRoute] int id)
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

                _repository.Excluir(ibge, id);

                return Ok();
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
