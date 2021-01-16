using RgCidadao.Api.Helpers;
using RgCidadao.Api.ViewModels;
using RgCidadao.Domain.Entities;
using RgCidadao.Domain.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net;
using RgCidadao.Domain.Entities.Cadastro;
using RgCidadao.Domain.Repositories.Cadastro;
using RgCidadao.Domain.Entities.Imunizacao;
using RgCidadao.Api.ViewModels.Cadastro;
using System;
using System.Collections.Generic;
using RgCidadao.Api.Filters;

namespace RgCidadao.Api.Controllers
{
    [Route("api/Cadastro/[controller]")]
    [ApiController]
    [EnableCors("SiteCorsPolicy")]
    [Area("Cadastro")]
    public class UserController : ControllerBase
    {
        private readonly ISegUserRepository _seguserRepository;
        private readonly IVersaoRepository _versaoRepository;
        private readonly IConfiguration _configuration;

        public UserController(ISegUserRepository segUsuariorepository, IConfiguration _config, IVersaoRepository repository)
        {
            _seguserRepository = segUsuariorepository;
            _configuration = _config;
            _versaoRepository = repository;
        }

        [HttpGet("GetById/{id}")]
        [Route("{ibge}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<Seg_Usuario> GetById([FromHeader]string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _configuration.GetConnectionString(Connection.GetConnection(ibge));
                Seg_Usuario user = _seguserRepository.GetSegUsuarioById(id, ibge);
                return Ok(user);
            }
            catch (System.Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPost("GetLogin")]
        [Route("{ibge,login,senha}")]
        public ActionResult<Seg_Usuario> GetLogin([FromBody] UserParameters model, [FromHeader]string ibge)
        {
            try
            {
                ibge = _configuration.GetConnectionString(Connection.GetConnection(ibge));
                Seg_Usuario user = new Seg_Usuario();
                user = _seguserRepository.GetLogin(ibge, model.login, model.senha);

                if (user == null)
                {
                    var response = TrataErro.GetResponse("Login não encontrado! Verifique seu usuário ou senha!", true);
                    return StatusCode((int)HttpStatusCode.BadRequest, response);
                }

                if (user.tipo_usuario != 1 && user.tipo_usuario != 2)
                {
                    var permissao = _seguserRepository.GetPermissaoUser(ibge, (int)user.id);
                    if (permissao == 0)
                    {
                        var response = TrataErro.GetResponse("O usuário informado não possui permissão de acesso.", true);
                        return StatusCode((int)HttpStatusCode.BadRequest, response);
                    }
                }

                var userinfo = new UserInfo
                {
                    Email = user.email_1,
                    Password = user.senha
                };
                var classtoken = new Token(_configuration);
                var token = classtoken.BuildToken(userinfo, (int)user.id);
                //user.Token = token.Token;

                UserReturnViewModel retorno = new UserReturnViewModel();
                retorno.user = user;
                retorno.Token = token.Token;
                retorno.chave_configuracao = _seguserRepository.GetConfigUsuario(ibge, (int)user.id);
                retorno.versao = new Versionamento { versao = Helper.GetVersao };

                return Ok(retorno);
            }
            catch (System.Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPost("RecuperarSenha")]
        [Route("{ibge,telefone}")]
        public ActionResult RecuperarSenha([FromBody] UserParameters model, [FromHeader]string ibge)
        {
            try
            {
                ibge = _configuration.GetConnectionString(Connection.GetConnection(ibge));
                Seg_Usuario user = _seguserRepository.GetTelefoneByUser(ibge, model.login);
                if (user == null)
                    return BadRequest(TrataErro.GetResponse("Não há cadastro de usuário que possua este login!", true));

                //grava a nova senha provisória
                var senhaprovisoria = Helper.GerarSenhaProvisoria();
                var senhacriptografada = Helper.GerarHashMd5(senhaprovisoria).ToUpper();
                _seguserRepository.AtualizarSenhaProvisoria(ibge, (int)user.id, senhacriptografada);

                string telefone = string.Empty;
                if (string.IsNullOrWhiteSpace(user.telefone_1) && string.IsNullOrWhiteSpace(user.telefone_2))
                    return BadRequest(TrataErro.GetResponse("Telefone inválido ou faltando, favor entrar em contato com Suporte Técnico para recuperar senha.", true));
                else if (!string.IsNullOrWhiteSpace(user.telefone_1))
                    telefone = user.telefone_1;
                else if (!string.IsNullOrWhiteSpace(user.telefone_2))
                    telefone = user.telefone_2;

                //envia sms de nova senha de usuário
                var texto = $@"A nova senha de acesso para o usuário {user.nome} é: {senhaprovisoria}";
                var smsmodel = new Services.ViewModels.SmsViewModel();
                smsmodel.mensagem = texto;
                smsmodel.numero = Helper.RemoveCaracteresTelefone(telefone);
                Services.EnvioSms.RecuperarSenha.SmsContato(smsmodel);

                return Ok();
            }
            catch (System.Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPut("AtualizarSenha/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ParameterTypeFilter("editar")]
        public ActionResult AtualizarSenha([FromHeader] string ibge, [FromBody] UserParameters model, [FromRoute] int id)
        {
            try
            {
                ibge = _configuration.GetConnectionString(Connection.GetConnection(ibge));
                _seguserRepository.AtualizarSenhaProvisoria(ibge, id, model.senha);

                return Ok();
            }
            catch (System.Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPut("Editar/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ParameterTypeFilter("editar")]
        public ActionResult Editar([FromHeader] string ibge, [FromBody] UserParameters model, [FromRoute] int id)
        {
            try
            {
                ibge = _configuration.GetConnectionString(Connection.GetConnection(ibge));
                _seguserRepository.Update(ibge, id, model.telefone_1, model.telefone_2, model.email_1);
                var modeluser = _seguserRepository.GetSegUsuarioById(id, ibge);

                return Ok(modeluser);
            }
            catch (System.Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPost("AtualizarConfigUsuario")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ParameterTypeFilter("editar")]
        public ActionResult AtualizarConfigUsuario([FromHeader] string ibge, Configuracao_Usuario model)
        {
            try
            {
                ibge = _configuration.GetConnectionString(Connection.GetConnection(ibge));
                if (model.id == null)
                {
                    var id = _seguserRepository.GetNewIdConfiguration(ibge);
                    model.id = id;
                }
                _seguserRepository.InsertOrUpdateConfigUsuario(ibge, model);

                return Ok(model);
            }
            catch (System.Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        //[HttpGet("TesteRelatorio")]
        //public ActionResult TesteRelatorio()
        //{
        //    try
        //    {
        //        var resport = new Imunizacao.Report.reportteste.GeraReportTestePrincipal();
        //        resport.testeGeraReport();
        //        return Ok();
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return BadRequest();
        //    }
        //}

        [HttpGet("TestePerformance")]
        public ActionResult TestePerformance([FromHeader] string ibge)
        {
            try
            {
                ibge = _configuration.GetConnectionString(Connection.GetConnection(ibge));
                var lista = _seguserRepository.GetTestePerformance(ibge);
                return Ok(lista);
            }
            catch (System.Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPost("InsertUser")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ParameterTypeFilter("inserir")]
        public ActionResult InsertUser([FromHeader] string ibge, [FromBody] Seg_Usuario model)
        {
            try
            {
                ibge = _configuration.GetConnectionString(Connection.GetConnection(ibge));
                model.id = _seguserRepository.GetNewId(ibge);
                _seguserRepository.InsertUser(ibge, model);

                return Ok(model);

            }
            catch (System.Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPut("EditarUser/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ParameterTypeFilter("editar")]
        public ActionResult EditarUser([FromHeader] string ibge, [FromBody] Seg_Usuario model, [FromRoute] int id)
        {
            try
            {
                ibge = _configuration.GetConnectionString(Connection.GetConnection(ibge));
                model.id = id;
                _seguserRepository.UpdatetUser(ibge, model);
                return Ok(model);
            }
            catch (System.Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetAllPagination")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<Seg_Usuario>> GetAllPagination([FromHeader]string ibge, int page,
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
                        filtro += $@" WHERE SG.NOME CONTAINING '{search}' OR
                                            SG.LOGIN CONTAINING '{search}'";
                }

                int count = _seguserRepository.GetCountAll(ibge, filtro);
                if (count == 1)
                    page = 0;
                else
                    page = page * pagesize;

                Response.Headers.Add("X-Total-Count", count.ToString());
                List<Seg_Usuario> lista = _seguserRepository.GetAllPagination(ibge, pagesize, page, filtro);
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