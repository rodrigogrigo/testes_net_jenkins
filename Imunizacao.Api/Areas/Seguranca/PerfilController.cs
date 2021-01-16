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
using RgCidadao.Api.ViewModels.Seguranca;
using RgCidadao.Domain.Entities.Seguranca;
using RgCidadao.Domain.Repositories.Seguranca;
using RgCidadao.Domain.ViewModels.Seguranca;

namespace RgCidadao.Api.Areas.Seguranca
{
    [Route("api/Seguranca/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("Seguranca")]
    public class PerfilController : ControllerBase
    {
        private readonly IPerfilRepository _Repository;
        private readonly IConfiguration _configuration;
        public PerfilController(IPerfilRepository perfil, IConfiguration _config)
        {
            _Repository = perfil;
            _configuration = _config;
        }

        [HttpGet("GetPerfilUsuario")]
        [TypeFilter(typeof(PermissaoTipoMasterFilter))]
        public ActionResult<List<Seg_Perfil_Acesso>> GetPerfilUsuario([FromHeader] string ibge, string descricao)
        {
            try
            {
                ibge = _configuration.GetConnectionString(Connection.GetConnection(ibge));
                string filtro = string.Empty;
                if (!string.IsNullOrWhiteSpace(descricao))
                    filtro = $@" WHERE SPA.DESCRICAO CONTAINING '{descricao}'";
                List<Seg_Perfil_Acesso> lista = _Repository.GetAllPerfis(ibge, filtro);

                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPost("Inserir")]
        [TypeFilter(typeof(PermissaoTipoMasterFilter))]
        public ActionResult Inserir([FromHeader] string ibge, [FromBody] Seg_Perfil_Acesso model)
        {
            try
            {
                ibge = _configuration.GetConnectionString(Connection.GetConnection(ibge));
                var pordescricao = _Repository.GetPerfilByDescricao(ibge, model.descricao);
                if (pordescricao != null)
                    return BadRequest(TrataErro.GetResponse("Já existe um outro perfil cadastrado com a mesma descrição. ", true));

                model.id = _Repository.GetPerfilNewId(ibge);
                _Repository.InsertSegPerfilAcesso(ibge, model);
                return Ok(model);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPut("Editar/{id}")]
        [TypeFilter(typeof(PermissaoTipoMasterFilter))]
        public ActionResult Editar([FromHeader] string ibge, [FromBody] Seg_Perfil_Acesso model, [FromRoute] int? id)
        {
            try
            {
                ibge = _configuration.GetConnectionString(Connection.GetConnection(ibge));
                model.id = id;
                _Repository.UpdateSegPerfilAcesso(ibge, model);
                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetPerfilById/{id}")]
        [TypeFilter(typeof(PermissaoTipoMasterFilter))]
        public ActionResult<Seg_Perfil_Acesso> GetPerfilById([FromHeader] string ibge, [FromRoute] int? id)
        {
            try
            {
                ibge = _configuration.GetConnectionString(Connection.GetConnection(ibge));
                Seg_Perfil_Acesso item = _Repository.GetPerfilById(ibge, (int)id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetPermissoesByPerfil/{id_perfil}")]
        [TypeFilter(typeof(PermissaoTipoMasterFilter))]
        public ActionResult<List<SegModuloViewModel>> GetPermissoesByPerfil([FromHeader] string ibge, [FromRoute]int? id_perfil)
        {
            try
            {
                ibge = _configuration.GetConnectionString(Connection.GetConnection(ibge));
                string filtro = string.Empty;
                List<SegModuloViewModel> lista = _Repository.GetModulosByPerfil(ibge, (int)id_perfil);

                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPut("AtualizaPermissaoPerfil")]
        [TypeFilter(typeof(PermissaoTipoMasterFilter))]
        public ActionResult AtualizaPermissaoPerfil([FromHeader] string ibge, [FromBody] List<ParamPermissaoPerfilViewModel> model)
        {
            try
            {
                ibge = _configuration.GetConnectionString(Connection.GetConnection(ibge));
                foreach (var item in model)
                {
                    _Repository.AtualizaPermissaoPerfil(ibge, (int)item.permissao, (int)item.id);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetAllPagination")]
        [TypeFilter(typeof(PermissaoTipoMasterFilter))]
        public ActionResult<List<Seg_Perfil_Acesso>> GetAllPagination([FromHeader]string ibge, int page,
                                                          int pagesize, string search, string fields)
        {
            try
            {
                ibge = _configuration.GetConnectionString(Connection.GetConnection(ibge));
                string filtro = string.Empty;

                if (!string.IsNullOrWhiteSpace(search))
                {
                    if (fields != null && fields.Split(",").Length > 0 && fields.Split(",")[0] != null)
                        filtro += " WHERE " + Helper.GetFiltroInicial(fields, search);

                    else
                        filtro += $@" WHERE DESCRICAO CONTAINING '{search}'";
                }

                int count = _Repository.GetCountAll(ibge, filtro);
                if (count == 1)
                    page = 0;
                else
                    page = page * pagesize;

                Response.Headers.Add("X-Total-Count", count.ToString());
                List<Seg_Perfil_Acesso> lista = _Repository.GetAllPagination(ibge, page, pagesize, filtro);
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