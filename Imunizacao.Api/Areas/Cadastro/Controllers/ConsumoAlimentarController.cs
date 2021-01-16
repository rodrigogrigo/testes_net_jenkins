using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RgCidadao.Domain.Repositories.E_SUS;

namespace RgCidadao.Api.Areas.Cadastro.Controllers
{
    [Route("api/Cadastro/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("Cadastro")]
    public class ConsumoAlimentarController : ControllerBase
    {
        private readonly IConsumoAlimentarRepository _Repository;
        private IConfiguration _config;
        public ConsumoAlimentarController(IConsumoAlimentarRepository feriadorepository, IConfiguration configuration)
        {
            _Repository = feriadorepository;
            _config = configuration;
        }
    }
}