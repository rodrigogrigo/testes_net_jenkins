using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.Extensions.Configuration;
using RgCidadao.Api.Helpers;
using RgCidadao.Api.ViewModels.Cadastro;
using RgCidadao.Domain.Repositories.Cadastro;

namespace RgCidadao.Api.Areas.Cadastro.Controllers
{
    [Route("api/Cadastro/[controller]")]
    [ApiController]
    [EnableCors("SiteCorsPolicy")]
    [Area("Cadastro")]
    [AllowAnonymous]
    public class VersaoController : ControllerBase
    {
        public IVersaoRepository _repository;
        private IConfiguration _config;
        public VersaoController(IVersaoRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _config = configuration;
        }

        [HttpPost("AtualizaBD")]
        public ActionResult AtualizaBD([FromHeader] string ibge, [FromBody]ParamAtualizaBDViewModel model)
        {
            try
            {
                string codibge = ibge;
                //recuperar bancos
                var ibgemun = _config.GetConnectionString(Connection.GetConnection(ibge));
                _repository.AtualizaBanco(ibgemun, model.command);

                var ibgefotos = _config.GetConnectionString(Connection.GetConnectionFoto(codibge));
                _repository.AtualizaBancoFotos(ibgemun, ibgefotos, model.command);
                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }
    }
}