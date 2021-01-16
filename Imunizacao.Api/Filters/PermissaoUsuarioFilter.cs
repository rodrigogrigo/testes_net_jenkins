using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using RgCidadao.Domain.Repositories.Cadastro;
using RgCidadao.Domain.Repositories.Seguranca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using RgCidadao.Api.Helpers;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace RgCidadao.Api.Filters
{
    public class PermissaoUsuarioFilter : Attribute, IAsyncActionFilter
    {
        private readonly IPerfilUsuarioRepository _Repository;
        private readonly IConfiguration _config;
        private string _acao;
        public PermissaoUsuarioFilter(IPerfilUsuarioRepository repository, IConfiguration config, string acao)
        {
            _Repository = repository;
            _config = config;
            _acao = acao;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            try
            {
                //Getting the media type
                //string ibge = context.HttpContext.Request.Headers["ibge"];
                //ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                //string unidade = context.HttpContext.Request.Headers["unidade"];
                //string clientOrigin = context.HttpContext.Request.Headers["clientOrigin"];

                //var principal = context.HttpContext.User;
                //int? id_usuario = null;
                //if (principal?.Identities?.FirstOrDefault().Claims != null)
                //    id_usuario = Convert.ToInt32(principal?.Identities?.FirstOrDefault().Claims.FirstOrDefault()?.Value);

                //var modulo = clientOrigin.Split(".")[0];
                //var tela = clientOrigin.Split(".")[1];

                //var resultado = _Repository.GetPermissaoByUserTelaModulo(ibge, (int)id_usuario, Convert.ToInt32(unidade), modulo, tela, _acao);
                //if (resultado)
                    await next();
                //else
                //    context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden); 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
