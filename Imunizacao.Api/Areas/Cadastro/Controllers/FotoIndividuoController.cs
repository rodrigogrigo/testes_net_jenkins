using System;
using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RgCidadao.Api.Helpers;
using RgCidadao.Domain.Entities.Cadastro;
using RgCidadao.Domain.Repositories.Cadastro;

namespace RgIndividuo.Api.Areas.Cadastro.Controllers
{ 
    [Route("api/Cadastro/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("Cadastro")]
    public class FotoIndividuoController : ControllerBase
    {
        public IFotoIndividuoRepository _repository;
        private IConfiguration _config;
        public FotoIndividuoController(IFotoIndividuoRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _config = configuration;
        }

        [HttpPost]
        public ActionResult UpdateOrInsert([FromHeader] string ibge, [FromForm] FotoIndividuo model, IFormFile file)
        {

            if (file == null)
            {
                return BadRequest(new { message = "Ocorreu um problema ao carregar a foto." });
            }

            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnectionFoto(ibge));

                model.csi_foto = FileHelper.GetByteArrayFromFile(file);
                model.csi_tipo = "C";
                model.data_alteracao = DateTime.Now;

                _repository.UpdateOrInsertByIdIndividuo(ibge, model);

                var fotoIndividuo =_repository.GetByIdIndividuo(ibge, model.csi_matricula);

                return Ok(new
                   {
                        csi_id = fotoIndividuo.csi_id,
                        csi_matricula = fotoIndividuo.csi_matricula,
                        csi_tipo = fotoIndividuo.csi_tipo,
                        data_alteracao = fotoIndividuo.data_alteracao,
                    }    
                );
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("{id_individuo}")]
        public ActionResult<FotoIndividuo> GetByIdIndividuo([FromHeader] string ibge, [FromRoute] int id_Individuo)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnectionFoto(ibge));
                FotoIndividuo fotoIndividuo = _repository.GetByIdIndividuo(ibge, id_Individuo);

                if (fotoIndividuo == null)
                {
                    return Ok(null);
                }

                var fotoBase64 = (fotoIndividuo.csi_foto != null)
                    ? Convert.ToBase64String(fotoIndividuo.csi_foto)
                    : string.Empty;

                return Ok(new
                    {
                        csi_id = fotoIndividuo.csi_id,
                        csi_matricula = fotoIndividuo.csi_matricula,
                        csi_tipo = fotoIndividuo.csi_tipo,
                        data_alteracao = fotoIndividuo.data_alteracao,
                        csi_foto = fotoBase64,
                    }
                );
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }
    }
}