using System;
using System.Net;
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
using System.Collections.Generic;
using RgCidadao.Api.Filters;
using System.Linq;

namespace RgCidadao.Api.Controllers
{
    [Route("api/Cadastro/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("Cadastro")]
    public class EquipeController : ControllerBase
    {
        private readonly IEquipeRepository _equipeRepository;
        private IConfiguration _config;
        private ISegUserRepository _seguserRepository;
        public EquipeController(IEquipeRepository equiperepository, IConfiguration configuration, ISegUserRepository seguserRepository)
        {
            _equipeRepository = equiperepository;
            _config = configuration;
            _seguserRepository = seguserRepository;
        }

        [HttpGet("GetEquipeByCidadao/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<Equipe> GetEquipeByCidadao([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                var usanovo = _equipeRepository.UsaEstruturaNova(ibge);
                Equipe equipe = new Equipe();
                if (usanovo)
                    equipe = _equipeRepository.GetEquipeByCidadaoEstruturaNova(ibge, id);
                else
                    equipe = _equipeRepository.GetEquipeByCidadaoEstruturaVelha(ibge, id);

                return Ok(equipe);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetEquipeByBairro")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<Equipe>> GetEquipeByBairro([FromHeader] string ibge, int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<Equipe> equipes = _equipeRepository.GetEquipeByBairro(ibge, id);
                return Ok(equipes);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetEquipeByProfissional/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<Equipe>> GetEquipeByProfissional([FromHeader] string ibge, [FromRoute]int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<Equipe> equipes = _equipeRepository.GetEquipeByProfissional(ibge, id);
                return Ok(equipes);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetEquipeByUnidade/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<Equipe>> GetEquipeByUnidade([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<Equipe> equipes = _equipeRepository.GetEquipeByUnidade(ibge, id);
                return Ok(equipes);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetEquipeByPerfil")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<Equipe>> GetEquipeByPerfil([FromHeader] string ibge)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));

                var id_usuario = Convert.ToInt32(HttpContext.User.Identities?.FirstOrDefault().Claims.FirstOrDefault()?.Value);
                int? user = _seguserRepository.GetTipoUsuarioById(ibge, id_usuario);

                string filtro = string.Empty;
                if (user == 3)
                    filtro += $@" WHERE UN.CSI_CODUNI IN (
                                    SELECT PU.ID_UNIDADE
                                    FROM SEG_PERFIL_USUARIO PU
                                    WHERE PU.ID_USUARIO = {id_usuario})";


                List<Equipe> itens = _equipeRepository.GetEquipeByPerfil(ibge, filtro);
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