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
using RgCidadao.Domain.Entities.Seguranca;
using RgCidadao.Domain.Repositories.Cadastro;
using RgCidadao.Domain.Repositories.Seguranca;
using RgCidadao.Domain.ViewModels.Seguranca;

namespace RgCidadao.Api.Areas.Seguranca
{
    [Route("api/Seguranca/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("Seguranca")]
    public class PerfilUsuarioController : ControllerBase
    {
        private readonly IPerfilUsuarioRepository _Repository;
        private readonly ISegUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public PerfilUsuarioController(IPerfilUsuarioRepository perfil, IConfiguration _config, ISegUserRepository userRepository)
        {
            _Repository = perfil;
            _configuration = _config;
            _userRepository = userRepository;
        }

        [HttpGet("GetByIdUsuario/{id}")]
        [TypeFilter(typeof(PermissaoTipoMasterFilter))]
        public ActionResult<List<Seg_Perfil_Usuario>> GetByIdUsuario([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _configuration.GetConnectionString(Connection.GetConnection(ibge));
                var itens = _Repository.GetByIdUsuario(ibge, id);

                return Ok(itens);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPost("Inserir")]
        [TypeFilter(typeof(PermissaoTipoMasterFilter))]
        public ActionResult Inserir([FromHeader] string ibge, [FromBody] List<Seg_Perfil_Usuario> model)
        {
            try
            {
                ibge = _configuration.GetConnectionString(Connection.GetConnection(ibge));
                foreach (var item in model)
                {
                    if (item.id == null)
                        item.id = _Repository.GetNewId(ibge);
                    _Repository.InsertOrUpdate(ibge, item);

                }

                return Ok(model);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpDelete("Excluir/{id}")]
        [TypeFilter(typeof(PermissaoTipoMasterFilter))]
        public ActionResult Excluir([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _configuration.GetConnectionString(Connection.GetConnection(ibge));
                _Repository.Delete(ibge, id);

                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetPermissaoUsuario/{unidade}")]
        public ActionResult GetPermissaoUsuario([FromHeader] string ibge, [FromRoute]int unidade)
        {
            try
            {
                var principal = HttpContext.User;
                int? id_usuario = null;
                if (principal?.Identities?.FirstOrDefault().Claims != null)
                    id_usuario = Convert.ToInt32(principal?.Identities?.FirstOrDefault().Claims.FirstOrDefault()?.Value);

                if (id_usuario == null)
                    throw new Exception("Código de usuário não encontrado! Por favor, faça o login novamente.");
                ibge = _configuration.GetConnectionString(Connection.GetConnection(ibge));

                var itens = new List<SegPermissoesUsuarioViewModel>();

                var usuario = _userRepository.GetSegUsuarioById((int)id_usuario, ibge);
                if (usuario.tipo_usuario == 1 || usuario.tipo_usuario == 2)
                    itens = _Repository.GetPermissaoUsuarioTipo1e2(ibge, unidade);
                else
                    itens = _Repository.GetPermissaoUsuarios(ibge, (int)id_usuario, unidade);

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