using System;
using System.Collections.Generic;
using System.Net;
using RgCidadao.Api.Helpers;
using RgCidadao.Domain.Repositories;
using RgCidadao.Domain.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RgCidadao.Domain.Repositories.Cadastro;
using RgCidadao.Domain.ViewModels.Imunizacao;
using Microsoft.Extensions.Configuration;
using RgCidadao.Api.ViewModels;
using RgCidadao.Api.ViewModels.Cadastro;
using RgCidadao.Api.Filters;
using RgCidadao.Domain.Entities.Cadastro;
using RgCidadao.Domain.ViewModels.AtencaoBasica;

namespace RgCidadao.Api.Controllers
{
    [Route("api/Cadastro/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("Cadastro")]
    public class FornecedorController : ControllerBase
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private IConfiguration _config;
        public FornecedorController(IFornecedorRepository fornecedorrepository, IConfiguration configuration)
        {
            _fornecedorRepository = fornecedorrepository;
            _config = configuration;
        }

        [HttpGet("GetAllPagination")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<Domain.Entities.Cadastro.Fornecedor>> GetAllPagination([FromHeader]string ibge, int page,
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
                        filtro += Helper.GetFiltro(fields, search);
                    }
                    else
                    {
                        var stringcod = string.Empty;
                        if (Helper.soContemNumeros(search))
                            stringcod = $" CSI_CODFOR CONTAINING '{search}' OR ";

                        filtro += $@" AND ( {stringcod}
                                            CSI_NOMFOR CONTAINING '{search}' OR
                                            CSI_TIPFOR CONTAINING '{search}')";
                    }
                }

                int count = _fornecedorRepository.GetCountAll(ibge, filtro);
                if (count == 1)
                    page = 0;
                else
                    page = page * pagesize;

                Response.Headers.Add("X-Total-Count", count.ToString());
                List<Fornecedor> lista = _fornecedorRepository.GetAllPagination(ibge, filtro, page, pagesize);

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
        public ActionResult<List<Domain.Entities.Cadastro.Fornecedor>> GetAll([FromHeader]string ibge)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<Fornecedor> lista = _fornecedorRepository.GetAll(ibge);

                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                throw ex;
            }
        }

        //getbyid
        [HttpGet("GetFornecedorById/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<Domain.Entities.Cadastro.Fornecedor> GetFornecedorById([FromHeader]string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                Fornecedor item = _fornecedorRepository.GetById(ibge, id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPost("Inserir")]
        [Route("{ibge}")]
        [ParameterTypeFilter("inserir")]
        public ActionResult<Domain.Entities.Cadastro.Fornecedor> Inserir([FromHeader]string ibge, [FromBody] FornecedorViewModel model)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                var id = _fornecedorRepository.GetNewId(ibge);
                model.csi_codfor = id;
                model.csi_datainc = DateTime.Now;
                model.csi_datcad = DateTime.Now;
                _fornecedorRepository.Insert(model, ibge);
                model.csi_codfor = id;

                return Ok(model);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPut("Editar/{id}")]
        [Route("{ibge}")]
        [ParameterTypeFilter("editar")]
        public ActionResult<Domain.Entities.Cadastro.Fornecedor> Editar([FromHeader]string ibge, [FromBody] FornecedorViewModel model, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                model.csi_dataalt = DateTime.Now;
                model.csi_codfor = id;
                _fornecedorRepository.Update(model, ibge);
                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpDelete("Excluir/{id}")]
        [Route("{ibge}")]
        [ParameterTypeFilter("excluir")]
        public ActionResult<Domain.Entities.Cadastro.Fornecedor> Excluir([FromHeader]string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                _fornecedorRepository.Delete(DateTime.Now, id, ibge);
                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("ExisteCPFCNPJ")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult ExisteCPFCNPJ([FromHeader]string ibge, string cpfcnpj)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                var existe = _fornecedorRepository.ExisteCPFCNPJ(ibge, cpfcnpj);
                var response = new ReponseBoolViewModel();
                response.possui = existe;

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        #region Agendamento de Consultas
        [HttpGet("GetPrestadoresVigencia")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<PrestadorAgendaViewModel>> GetPrestadoresVigencia([FromHeader]string ibge, int codmed, int coduni, string data_ini, string data_fim)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<PrestadorAgendaViewModel> itens = _fornecedorRepository.GetPrestadoresVigencia(ibge, codmed, coduni, data_ini, data_fim);

                return Ok(itens);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }
        #endregion
    }
}