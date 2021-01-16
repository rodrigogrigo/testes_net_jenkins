using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using RgCidadao.Api.Helpers;
using RgCidadao.Domain.Repositories.Seguranca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RgCidadao.Api.Filters
{
    public class PermissaoModuloTelaFilter : Attribute, IAsyncActionFilter
    {
        private readonly IPerfilUsuarioRepository _Repository;
        private readonly IConfiguration _config;
        private string _acao;
        public PermissaoModuloTelaFilter(IPerfilUsuarioRepository repository, IConfiguration config)
        {
            _Repository = repository;
            _config = config;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
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

            //var resposta = _Repository.GetPermissaoModuloTela(ibge, (int)id_usuario, unidade, modulo, tela);

            //if (resposta>0)
                await next();
            //else
            //    context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);

        }
    }
}
