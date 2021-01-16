using System;
using System.Collections.Generic;
using System.Net;
using RgCidadao.Api.Helpers;
using RgCidadao.Api.ViewModels;
using RgCidadao.Domain.Entities;
using RgCidadao.Domain.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RgCidadao.Domain.Repositories.Imunizacao;
using RgCidadao.Domain.Repositories.Cadastro;
using RgCidadao.Domain.Entities.Imunizacao;
using Microsoft.Extensions.Configuration;
using RgCidadao.Api.ViewModels.Cadastro;
using RgCidadao.Api.ViewModels.Imunizacao;
using RgCidadao.Api.Filters;

namespace RgCidadao.Api.Controllers
{
    [Route("api/Imunizacao/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("Imunizacao")]
    public class AprazamentoController : ControllerBase
    {
        public IAprazamentoRepository _repository;
        public ICidadaoRepository _cidadaorepository;
        public IGestacaoRepository _gestacaorepository;
        private IConfiguration _config;
        public AprazamentoController(IAprazamentoRepository repository, ICidadaoRepository cidadao, IGestacaoRepository gestacao, IConfiguration configuration)
        {
            _repository = repository;
            _cidadaorepository = cidadao;
            _gestacaorepository = gestacao;
            _config = configuration;
        }

        [HttpGet("GetAprazamentoByCidadao/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<Aprazamento>> GetAprazamentoByCidadao([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<Aprazamento> lista = _repository.GetAprazamentoByCidadao(ibge, id);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpDelete("Excluir/{id}")]
        [ParameterTypeFilter("excluir")]
        public ActionResult<Aprazamento> Excluir([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                var permite = _repository.PermiteExcluirAprazamento(ibge, id);

                if (permite)
                {
                    _repository.Delete(ibge, id);
                    return Ok();
                }
                else
                {
                    var response = new ResponseViewModel();
                    response.message = "O aprazamento não pode ser excluído!";
                    response.erro = true;
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPost("GeraAprazamentoByIndividuo")]
        [ParameterTypeFilter("gerar_aprazamento")]
        public ActionResult GeraAprazamentoByIndividuo([FromHeader] string ibge, [FromBody] ParametersAprazamento model)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));

                _repository.GeraAprazamentoPopGeralByIndividuo(ibge, (int)model.id); //executa popgeral

                string sql_estrutura = string.Empty;
                if (_cidadaorepository.VerificaExisteEsusFamilia(ibge))
                    sql_estrutura = $@"LEFT JOIN ESUS_FAMILIA D ON(D.ID = D.ID_FAMILIA)";
                else
                    sql_estrutura = $@"LEFT JOIN ESUS_CADDOMICILIAR D ON PAC.ID_ESUS_CADDOMICILIAR = D.ID";

                //recupera informações de cidadão
                var cidadao = _cidadaorepository.GetCidadaoById(ibge, (int)model.id, sql_estrutura);

                if (cidadao.csi_sexpac == "Feminino")
                {
                    _repository.GeraAprazamentoFemininoByIndividuo(ibge, (int)model.id); //executa feminino

                    var gestacao = _gestacaorepository.IsGestante(ibge, (int)model.id);
                    if (gestacao != null)
                        _repository.GeraAprazamentoGestacaoByIndividuo(ibge, (int)model.id); // executa gestacao
                    else
                    {
                        var algumagestacao = _gestacaorepository.GetGestacaoByCidadao(ibge, (int)model.id);
                        if (algumagestacao != null)
                            _repository.GeraAprazamentoPuerperaByIndividuo(ibge, (int)model.id); //executa puerpera
                    }
                }
                else if (cidadao.csi_sexpac == "Masculino")
                    _repository.GeraAprazamentoMasculinoByIndividuo(ibge, (int)model.id); //executa masculino

                if (cidadao.verifica_deficiencia == 1)
                    _repository.GeraAprazamentoDeficienciaByIndividuo(ibge, (int)model.id); //executa deficiencia

                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPost("GeraAprazamentoCalendarioBasico")]
        [ParameterTypeFilter("gerar_aprazamento")]
        public ActionResult GeraAprazamentoCalendarioBasico([FromHeader]string ibge, [FromBody] ParametersAprazamento model)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                _repository.GeraAprazamentoCalendarioBasico(ibge, (int)model.id_calendario_basico, (int)model.publico_alvo);

                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPost("GeraAprazamento")]
        public ActionResult GeraAprazamento([FromHeader] string ibge)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                _repository.GeraAprazamento(ibge);
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