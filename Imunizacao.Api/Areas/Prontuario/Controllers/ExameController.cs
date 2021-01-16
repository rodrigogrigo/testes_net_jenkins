using System;
using System.Net;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using RgCidadao.Api.Filters;
using RgCidadao.Api.Helpers;

using RgCidadao.Domain.Entities.Prontuario;
using RgCidadao.Domain.Repositories.Prontuario;

using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace RgCidadao.Api.Controllers
{
    [Route("api/Prontuario/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("Prontuario")]

    public class ExameController : ControllerBase
    {
        private IExameRepository _repository;
        private IConfiguration _config;

        public ExameController(IExameRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _config = configuration;
        }

        [HttpGet("GetExamesComuns")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<Exame>> GetExamesComuns([FromHeader] string ibge)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<Exame> lista = _repository.GetExamesComuns(ibge);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetExamesAltoCustos")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<Exame>> GetExamesAltoCustos([FromHeader] string ibge)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<Exame> lista = _repository.GetExamesAltoCustos(ibge);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetCids")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<Cid>> GetCids([FromHeader] string ibge)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<Cid> lista = _repository.GetCid(ibge);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetAgrupamentosExame")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<Exame>> GetAgrupamentosExame([FromHeader] string ibge, [FromRoute] int id_exame)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<AgrupamentoExames> agrupamentos = _repository.GetListAgrupamentosExames(ibge);
                
                return Ok(agrupamentos);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetHistoricoSolicitacoesExameByPaciente/{id_paciente}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<ExamesAgrupados>> GetHistoricoSolicitacoesExameByPaciente([FromHeader] string ibge, [FromRoute] int id_paciente)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));

                List<AgrupamentoExames> agrupamentos = _repository.GetListAgrupamentosExames(ibge);

                List<ExamesAgrupados> itens = new List<ExamesAgrupados>();

                foreach (var agrupamento in agrupamentos)
                {
                    itens.Add( new ExamesAgrupados() {
                        nome_agrupamento = agrupamento.nome_agrupamento,
                        exames = _repository.GetHistoricoSolicitacoesExameByPaciente(ibge, id_paciente, agrupamento.codigo_agrupamento),
                    });  
                }

                return Ok(itens);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetHistoricoResultadoExameByPaciente")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<Exame>> GetHistoricoResultadoExameByPaciente([FromHeader] string ibge, [FromRoute] int id_exame)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<Exame> lista = _repository.GetHistoricoResultadoExameByPaciente(ibge, id_exame);
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
