using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.Extensions.Configuration;
using RgCidadao.Api.Filters;
using RgCidadao.Api.Helpers;
using RgCidadao.Domain.Entities.AtencaoBasica;
using RgCidadao.Domain.Repositories.AtencaoBasica;
using RgCidadao.Domain.ViewModels.AtencaoBasica;

namespace RgCidadao.Api.Areas.AtencaoBasica.Controllers
{
    [Route("api/AtencaoBasica/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("AtencaoBasica")]
    public class ExameFisicoController : ControllerBase
    {
        public IExameFisicoRepository _repository;
        private IConfiguration _config;
        public ExameFisicoController(IExameFisicoRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _config = configuration;
        }

        [HttpGet("GetAllPagination")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<ExameFisicoViewModel>> GetAllPagination([FromHeader] string ibge, int page,
                                                          int pagesize, string paciente, string profissional, DateTime? data_inicial, DateTime? data_final, int unidade)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                string filtro = string.Empty;

                if (!string.IsNullOrWhiteSpace(paciente))
                    filtro += $@" PAC.CSI_CODPAC = '{paciente}' ";

                //if (unidade != null && !string.IsNullOrWhiteSpace(filtro)) '{unidade}' ";
                //else if (unidade != null && string.IsNullOrWhiteSpace(filtro))
                //   filtro += $@" MED.CSI_NOMMED CONTAINING '{unidade}' ";

                //   filtro += $@" AND MED.CSI_NOMMED CONTAINING
                if (!string.IsNullOrWhiteSpace(profissional) && !string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" AND MED.CSI_CODMED = '{profissional}' ";
                else if (!string.IsNullOrWhiteSpace(profissional) && string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" MED.CSI_CODMED = '{profissional}' ";

                if (data_inicial != null && !string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" AND CAST(EF.DATA_EXAME_FISICO AS DATE) >= '{data_inicial?.ToString("dd.MM.yyyy")}'";
                else if (data_inicial != null && string.IsNullOrWhiteSpace(filtro))
                    filtro += $@"  CAST(EF.DATA_EXAME_FISICO AS DATE) >= '{data_inicial?.ToString("dd.MM.yyyy")}'";

                if (data_final != null && !string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" AND CAST(EF.DATA_EXAME_FISICO AS DATE) <= '{data_final?.ToString("dd.MM.yyyy")}'";
                else if (data_final != null && string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" CAST(EF.DATA_EXAME_FISICO AS DATE) <= '{data_final?.ToString("dd.MM.yyyy")}'";

                if (!string.IsNullOrWhiteSpace(filtro))
                    filtro = $@" WHERE " + filtro;


                int count = _repository.GetCountAll(ibge, filtro);
                if (count == 1)
                    page = 0;
                else
                    page = page * pagesize;

                Response.Headers.Add("X-Total-Count", count.ToString());
                List<ExameFisicoViewModel> lista = _repository.GetAllPagination(ibge, page, pagesize, filtro);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPost("Inserir")]
        [ParameterTypeFilter("inserir")]
        public ActionResult Inserir([FromHeader] string ibge, [FromBody] ExameFisico model)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                model.codigo_exame_fisico = _repository.GetNewCodigoExameFisico(ibge);
                model.csi_controle = _repository.GetNewCodigoControle(ibge);

                _repository.Insert(ibge, model);
                _repository.InsertUpdateProcenfermagem(ibge, model);
                _repository.InsertUpdateIProcenfermagem(ibge, model);

                return Ok(model);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPut("Editar/{id}")]
        [ParameterTypeFilter("editar")]
        public ActionResult Editar([FromHeader] string ibge, [FromBody] ExameFisico model, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                model.codigo_exame_fisico = id;
                _repository.Update(ibge, model);
                _repository.InsertUpdateProcenfermagem(ibge, model);
                _repository.InsertUpdateIProcenfermagem(ibge, model);

                return Ok(model);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetHistoricoAltura/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<HistoricoExameFisicoPac>> GetHistoricoAltura([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                List<HistoricoExameFisicoPac> item = _repository.GetHistoricoAltura(ibge, id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetHistoricoPeso/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<HistoricoExameFisicoPac>> GetHistoricoPeso([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                List<HistoricoExameFisicoPac> item = _repository.GetHistoricoPeso(ibge, id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetHistoricoIMC/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<HistoricoExameFisicoPac>> GetHistoricoIMC([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                List<HistoricoExameFisicoPac> item = _repository.GetHistoricoIMC(ibge, id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetHistoricoTemperatura/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<HistoricoExameFisicoPac>> GetHistoricoTemperatura([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                List<HistoricoExameFisicoPac> item = _repository.GetHistoricoTemperatura(ibge, id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetHistoricoCircunferenciaAbdominal/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<HistoricoExameFisicoPac>> GetHistoricoCircunferenciaAbdominal([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                List<HistoricoExameFisicoPac> item = _repository.GetHistoricoCircunferenciaAbdominal(ibge, id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetHistoricoCircunferenciaToracica/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<HistoricoExameFisicoPac>> GetHistoricoCircunferenciaToracica([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                List<HistoricoExameFisicoPac> item = _repository.GetHistoricoCircunferenciaToracica(ibge, id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetHistoricoPressaoArterial/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<HistoricoExameFisicoPac>> GetHistoricoPressaoArterial([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                List<HistoricoExameFisicoPac> item = _repository.GetHistoricoPressaoArterial(ibge, id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetHistoricoPressaoArterialSistolica/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<HistoricoExameFisicoPac>> GetHistoricoPressaoArterialSistolica([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                List<HistoricoExameFisicoPac> item = _repository.GetHistoricoPressaoArterialSistolica(ibge, id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetHistoricoPressaoArterialDiastolica/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<HistoricoExameFisicoPac>> GetHistoricoPressaoArterialDiastolica([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                List<HistoricoExameFisicoPac> item = _repository.GetHistoricoPressaoArterialDiastolica(ibge, id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetHistoricoGlicemia/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<HistoricoExameFisicoPac>> GetHistoricoGlicemia([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                List<HistoricoExameFisicoPac> item = _repository.GetHistoricoGlicemia(ibge, id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetHistoricoSaturacao/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<HistoricoExameFisicoPac>> GetHistoricoSaturacao([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                List<HistoricoExameFisicoPac> item = _repository.GetHistoricoSaturacao(ibge, id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetHistoricoFrequenciaCardiaca/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<HistoricoExameFisicoPac>> GetHistoricoFrequenciaCardiaca([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                List<HistoricoExameFisicoPac> item = _repository.GetHistoricoFrequenciaCardiaca(ibge, id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetHistoricoFrequenciaRespiratoria/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<HistoricoExameFisicoPac>> GetHistoricoFrequenciaRespiratoria([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                List<HistoricoExameFisicoPac> item = _repository.GetHistoricoFrequenciaRespiratoria(ibge, id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetHistoricoPerimetroCefalico/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<HistoricoExameFisicoPac>> GetHistoricoPerimetroCefalico([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                List<HistoricoExameFisicoPac> item = _repository.GetHistoricoFrequenciaCefalico(ibge, id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetHistoricoGlassGow/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<HistoricoExameFisicoPac>> GetHistoricoGlassGow([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                List<HistoricoExameFisicoPac> item = _repository.GetHistoricoGlassGow(ibge, id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetHistoricoReguaDor/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<HistoricoExameFisicoPac>> GetHistoricoReguaDor([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                List<HistoricoExameFisicoPac> item = _repository.GetHistoricoReguaDor(ibge, id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetHistoricoProcedimentosGerados/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<HistoricoExameFisicoPac>> GetHistoricoProcedimentosGerados([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                List<HistoricoExameFisicoPac> item = _repository.GetHistoricoProcedimentosGerados(ibge, id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetHistoricoExameFisicoPac/{paciente}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<HistoricoExameFisicoPac>> GetHistoricoExameFisicoPac([FromHeader] string ibge, [FromRoute] int paciente)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                List<HistoricoExameFisicoPac> item = _repository.GetHistoricoExameFisicoPac(ibge, paciente);
                return Ok(item);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetHistoricoObservacaoByPaciente/{paciente}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<HistoricoObservacaoViewModel> GetHistoricoObservacaoByPaciente([FromHeader] string ibge, [FromRoute] int? paciente)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                List<HistoricoObservacaoViewModel> lista = _repository.GetHistoricoObservacaoByPaciente(ibge, (int)paciente);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetLastExameFisicoPac/{paciente}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<UltimoExameFisicoPac> GetLastExameFisicoPac([FromHeader] string ibge, [FromRoute] int paciente)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                UltimoExameFisicoPac item = _repository.GetLastExameFisicoPac(ibge, paciente);
                return Ok(item);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetExameFisicoById/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<ExameFisico> GetExameFisicoById([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                ExameFisico item = _repository.GetExameFisicoById(ibge, id);
                item.iprocenfermagem = _repository.GetIProcenfermagemById(ibge, item.csi_controle);

                return Ok(item);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }
    }
}