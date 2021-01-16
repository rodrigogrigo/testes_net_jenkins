using System;
using System.Collections.Generic;
using System.Net;
using RgCidadao.Api.Helpers;
using RgCidadao.Domain.Entities;
using RgCidadao.Domain.Repositories;
using RgCidadao.Domain.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RgCidadao.Domain.Repositories.Imunizacao;
using RgCidadao.Domain.ViewModels.Imunizacao;
using Microsoft.Extensions.Configuration;
using RgCidadao.Api.Filters;

namespace RgCidadao.Api.Controllers
{
    [Route("api/Imunizacao/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("Imunizacao")]
    public class DashboardController : ControllerBase
    {
        public IDashboardRepository _repository;
        private IConfiguration _config;
        public DashboardController(IDashboardRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _config = configuration;
        }

        [HttpGet("GetImunizadasByMes")]
        [TypeFilter(typeof(PermissaoModuloTelaFilter))]
        public ActionResult<List<DashboardViewModel>> GetImunizadasByMes([FromHeader] string ibge)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<DashboardViewModel> itens = _repository.TotalImunizadasMes(ibge);
                return Ok(itens);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetCountVacinasDia/{id}")]
        [TypeFilter(typeof(PermissaoModuloTelaFilter))]
        public ActionResult<DashboardViewModel> GetCountVacinasDia([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                DashboardViewModel count = _repository.TotalVacinasDia(ibge, id);
                return Ok(count);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetCountVacinasVencidas")]
        [TypeFilter(typeof(PermissaoModuloTelaFilter))]
        public ActionResult<DashboardViewModel> GetCountVacinasVencidas([FromHeader] string ibge)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                DashboardViewModel count = _repository.TotalVacinaVencida(ibge);
                return Ok(count);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetVacinasByUnidade/{id}")]
        [TypeFilter(typeof(PermissaoModuloTelaFilter))]
        public ActionResult<DashboardViewModel> GetVacinasByUnidade([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<DashboardViewModel> vacinas = _repository.GetVacinas(ibge, id);
                return Ok(vacinas);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetPercentualPolioPenta/{id}")]
        [TypeFilter(typeof(PermissaoModuloTelaFilter))]
        public ActionResult<DashboardViewModel> GetPercentualPolioPenta([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                DashboardViewModel percentual = _repository.GetPercentualPolioPenta(ibge, id);
                return Ok(percentual);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }
    }
}