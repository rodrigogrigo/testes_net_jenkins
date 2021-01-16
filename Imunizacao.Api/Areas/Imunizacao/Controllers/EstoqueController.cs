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
using RgCidadao.Domain.Entities.Imunizacao;
using Microsoft.Extensions.Configuration;
using RgCidadao.Api.Filters;

namespace RgCidadao.Api.Controllers
{
    [Route("api/Imunizacao/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("Imunizacao")]
    public class EstoqueController : ControllerBase
    {
        public IEstoqueRepository _repository;
        private IConfiguration _config;
        public EstoqueController(IEstoqueRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _config = configuration;
        }

        [HttpGet("GetUnidadeEstoque")]
        [ParameterTypeFilter("ver_estoque")]
        public ActionResult<List<UnidadeEstoqueViewModel>> GetUnidadeEstoque([FromHeader]string ibge, int usuario, int produto)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<UnidadeEstoqueViewModel> lista = _repository.GetAllUnidadeWithEstoque(ibge, produto, usuario);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetAuditoria")]
        [ParameterTypeFilter("ver_estoque")]
        public ActionResult<List<AuditoriaEstoque>> GetAuditoria([FromHeader]string ibge, int page, int pagesize, int id_produto,
                                                                            string datainicial, string datafinal, string lote, int? unidade, int? id_produtor)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                int count = _repository.GetCountAuditoria(ibge, id_produto, Convert.ToDateTime(datainicial), Convert.ToDateTime(datafinal), lote, unidade, id_produtor);
                if (count == 1)
                    page = 0;
                else
                    page = page * pagesize;

                Response.Headers.Add("X-Total-Count", count.ToString());
                List<AuditoriaEstoque> lista = _repository.GetAuditoria(ibge, id_produto, Convert.ToDateTime(datainicial), Convert.ToDateTime(datafinal), lote, unidade, page, pagesize, id_produtor);

                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }
    }
}