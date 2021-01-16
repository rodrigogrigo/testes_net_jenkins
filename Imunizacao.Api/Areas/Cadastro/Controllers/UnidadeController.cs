using System;
using System.Collections.Generic;
using System.Net;
using RgCidadao.Api.Helpers;
using RgCidadao.Domain.Entities;
using RgCidadao.Domain.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RgCidadao.Domain.Entities.Cadastro;
using RgCidadao.Domain.Repositories.Cadastro;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using RgCidadao.Api.Filters;
using RgCidadao.Domain.Repositories.Seguranca;
using RgCidadao.Domain.ViewModels.Cadastros;

namespace RgCidadao.Api.Controllers
{
    [Route("api/Cadastro/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("Cadastro")]
    public class UnidadeController : ControllerBase
    {
        private IUnidadeRepository _unidadeRepository;
        private static IPerfilUsuarioRepository _perfilRepository;
        private static ISegUserRepository _userRepository;
        private static IConfiguration _config;
        public UnidadeController(IUnidadeRepository unidaderepository, IConfiguration configuration,
                                                IPerfilUsuarioRepository perfilRepository, ISegUserRepository userRepository)
        {
            _unidadeRepository = unidaderepository;
            _config = configuration;
            _perfilRepository = perfilRepository;
            _userRepository = userRepository;
        }

        [HttpGet("{ibge, filtro}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<Unidade>> GetAll([FromHeader] string ibge)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                string filtro = string.Empty;
                List<Unidade> lista = _unidadeRepository.GetAll(ibge, filtro);

                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetUnidadesByUser/{user}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<Unidade>> GetUnidadesByUser([FromRoute]int user, [FromHeader] string ibge)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<Unidade> lista = new List<Unidade>();

                var usuario = _userRepository.GetSegUsuarioById(user, ibge);
                if (usuario.tipo_usuario == 1 || usuario.tipo_usuario == 2)
                    lista = _unidadeRepository.GetAll(ibge, " WHERE UN.EXCLUIDO = 'F' OR UN.EXCLUIDO IS NULL ");
                else
                    lista = _unidadeRepository.GetUnidadesByUser(ibge, user);

                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        #region Agendamento de Consulta
        [HttpGet("GetLocaisAtendimentoByUnidade/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<LocalAtendimentoViewModel>> GetLocaisAtendimentoByUnidade([FromHeader] string ibge, int unidade)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<LocalAtendimentoViewModel> lista = _unidadeRepository.GetLocaisAtendimentoByUnidade(ibge, unidade);

                return Ok(lista);
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