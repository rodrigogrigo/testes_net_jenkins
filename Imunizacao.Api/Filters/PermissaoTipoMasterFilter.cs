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
    public class PermissaoTipoMasterFilter : Attribute, IAsyncActionFilter
    {
        private readonly IPerfilUsuarioRepository _Repository;
        private readonly IConfiguration _config;
        private string _acao;
        public PermissaoTipoMasterFilter(IPerfilUsuarioRepository repository, IConfiguration config)
        {
            _Repository = repository;
            _config = config;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            try
            {
                //string ibge = context.HttpContext.Request.Headers["ibge"];
                //ibge = _config.GetConnectionString(Connection.GetConnection(ibge));

                //var principal = context.HttpContext.User;
                //int? id_usuario = null;
                //if (principal?.Identities?.FirstOrDefault().Claims != null)
                //    id_usuario = Convert.ToInt32(principal?.Identities?.FirstOrDefault().Claims.FirstOrDefault()?.Value);

                //var tipo = _Repository.GetUsuarioPermissaoTipo1e2(ibge, (int)id_usuario);
                //if(tipo==1 || tipo == 2)
                    await next();
                //else
                //    context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
