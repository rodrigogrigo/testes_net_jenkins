using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RgCidadao.Api.Helpers;
using RgCidadao.Api.ViewModels.Indicadores.Indicador5;
using RgCidadao.Domain.Repositories.Indicadores;

namespace RgCidadao.Api.Areas.Indicadores.Controllers
{
    [Route("api/Indicadores/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("Indicadores")]
    public class Indicador5Controller : ControllerBase
    {
        public IIndicador5Repository _repository;
        public IIndicador1Repository _repository1;
        private IConfiguration _config;
        public Indicador5Controller(IIndicador5Repository repository, IIndicador1Repository repository1, IConfiguration configuration)
        {
            _repository = repository;
            _config = configuration;
            _repository1 = repository1;
        }

        [HttpGet("{desdobramento}")]
        public ActionResult Indicador5([FromHeader]string ibge, [FromRoute] ParamsRouteIndicador5 model, [FromQuery] ParamsQueryIndicador5 modelquery)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                string sqlSelectTipo = string.Empty;
                string sqlFiltros = string.Empty;
                string sqlAgrupamento = string.Empty;

                // Campos do Select
                sqlSelectTipo = " SELECT ";
                if (model.desdobramento == "unidade_saude")
                    sqlSelectTipo += $@" COALESCE(ES.ID,-1) CODIGO_UNIDADE, COALESCE(ES.NOME_FANTASIA,'SEM UNIDADE DE SAÚDE') UNIDADE_SAUDE, ";
                else if (model.desdobramento == "equipe")
                    sqlSelectTipo += $@" COALESCE(EQ.ID,-1) CODIGO_EQUIPE, COALESCE(EQ.DSC_AREA || ' - ' || EQ.COD_INE, 'SEM EQUIPE') EQUIPE, ";
                else if (model.desdobramento == "agente_saude")
                    sqlSelectTipo += $@" COALESCE(ME.CSI_CODMED,-1) CODIGO_AGENTE, COALESCE(ME.CSI_NOMMED,'SEM AGENTE DE SAÚDE') AGENTE_SAUDE, ";


                // Filtros
                string quadrimestre = modelquery.quadrimestre != null ? modelquery.quadrimestre.ToString() : "NULL";
                int? ano = modelquery.ano != null ? modelquery.ano : DateTime.Now.Year;

                //filtro por idade inicial
                if (modelquery.idade_inicial != null)
                    sqlFiltros += $@" AND CAST((SELECT IDADE FROM PRO_IDADE_PACIENTE(C.CSI_DTNASC, CURRENT_DATE)) AS NUMERIC(15,2)) >= {(modelquery.idade_inicial / 100)?.ToString("N2").Replace(",", ".")}";

                //filtro por idade final
                if (modelquery.idade_final != null)
                    sqlFiltros += $@" AND CAST((SELECT IDADE FROM PRO_IDADE_PACIENTE(C.CSI_DTNASC, CURRENT_DATE)) AS NUMERIC(15,2)) <= {(modelquery.idade_final / 100)?.ToString("N2").Replace(",", ".")}";

                if (!string.IsNullOrEmpty(modelquery.equipes))
                    sqlFiltros += $@" AND EQ.ID IN({modelquery.equipes}) ";

                if (modelquery.agente_saude != null)
                    sqlFiltros += $@" AND ME.CSI_CODMED = {modelquery.agente_saude} ";

                // Agrupamentos
                if (model.desdobramento == "unidade_saude")
                    sqlAgrupamento = $@" GROUP BY ES.ID, ES.NOME_FANTASIA ORDER BY ES.NOME_FANTASIA ";
                else if (model.desdobramento == "equipe")
                    sqlAgrupamento = $@" GROUP BY EQ.ID, EQ.NOME_REFERENCIA, EQ.DSC_AREA, EQ.COD_INE ORDER BY EQ.NOME_REFERENCIA ";
                else if (model.desdobramento == "agente_saude")
                    sqlAgrupamento = $@" GROUP BY ME.CSI_CODMED, ME.CSI_NOMMED ";


                var registros = _repository.Indicador5(ibge, sqlSelectTipo, sqlFiltros, sqlAgrupamento, quadrimestre, (int)ano);

                if (model.desdobramento == "total")
                {
                    var total = new Indicador5TotalViewModel()
                    {
                        porcentagem = Helper.CalculaPorcentagem(
                            registros.FirstOrDefault().qtde_individuos,
                            registros.FirstOrDefault().qtde_metas
                        ),
                        porcentagem_valida = Helper.CalculaPorcentagem(
                            registros.FirstOrDefault().qtde_individuos,
                            registros.FirstOrDefault().qtde_metas_validas
                        ),
                        qtde_individuos = registros.FirstOrDefault().qtde_individuos,
                        qtde_metas = registros.FirstOrDefault().qtde_metas,
                        qtde_metas_validas = registros.FirstOrDefault().qtde_metas_validas,
                    };
                    return Ok(total);
                }
                else if (model.desdobramento == "unidade_saude")
                {
                    var unidades = new List<Indicador5UnidadeSaudeViewModel>();
                    foreach (var item in registros)
                    {
                        var unidade = new Indicador5UnidadeSaudeViewModel()
                        {
                            codigo_unidade = item.codigo_unidade,
                            porcentagem = Helper.CalculaPorcentagem(item.qtde_individuos, item.qtde_metas),
                            porcentagem_valida = Helper.CalculaPorcentagem(item.qtde_individuos, item.qtde_metas_validas),
                            qtde_individuos = item.qtde_individuos,
                            qtde_metas = item.qtde_metas,
                            qtde_metas_validas = item.qtde_metas_validas,
                            unidade_saude = item.unidade_saude
                        };
                        unidades.Add(unidade);
                    }

                    return Ok(unidades);
                }
                else if (model.desdobramento == "equipe")
                {
                    var equipes = new List<Indicador5EquipeViewModel>();
                    foreach (var item in registros)
                    {
                        var equipe = new Indicador5EquipeViewModel()
                        {
                            codigo_equipe = item.codigo_equipe,
                            equipe = item.equipe,
                            porcentagem = Helper.CalculaPorcentagem(item.qtde_individuos, item.qtde_metas),
                            porcentagem_valida = Helper.CalculaPorcentagem(item.qtde_individuos, item.qtde_metas_validas),
                            qtde_individuos = item.qtde_individuos,
                            qtde_metas = item.qtde_metas,
                            qtde_metas_validas = item.qtde_metas_validas,
                        };
                        equipes.Add(equipe);
                    }
                    return Ok(equipes);
                }
                else if (model.desdobramento == "agente_saude")
                {
                    var agentes = new List<Indicador5AgenteSaudeViewModel>();
                    foreach (var item in registros)
                    {
                        var agente = new Indicador5AgenteSaudeViewModel()
                        {
                            porcentagem = Helper.CalculaPorcentagem(item.qtde_individuos, item.qtde_metas),
                            porcentagem_valida = Helper.CalculaPorcentagem(item.qtde_individuos, item.qtde_metas_validas),
                            agente_saude = item.agente_saude,
                            codigo_agente = item.codigo_agente,
                            qtde_individuos = item.qtde_individuos,
                            qtde_metas = item.qtde_metas,
                            qtde_metas_validas = item.qtde_metas_validas,
                        };
                        agentes.Add(agente);
                    }
                    return Ok(agentes);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("PublicoAlvo")]
        public ActionResult PublicoAlvo([FromHeader]string ibge, [FromRoute] ParamsRouteIndicador5 model, [FromQuery] ParamsQueryIndicador5 modelquery)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));

                if (modelquery.pagesize == null)
                    modelquery.pagesize = 20;

                string sqlSelect = string.Empty;
                string sqlFiltros = string.Empty;

                // Filtros
                string quadrimestre = modelquery.quadrimestre != null ? modelquery.quadrimestre.ToString() : "NULL";
                int? ano = modelquery.ano != null ? modelquery.ano : DateTime.Now.Year;

                //filtro por idade inicial
                if (modelquery.idade_inicial != null)
                    sqlFiltros += $@" AND CAST((SELECT IDADE FROM PRO_IDADE_PACIENTE(C.CSI_DTNASC, CURRENT_DATE)) AS NUMERIC(15,2)) >= {(modelquery.idade_inicial / 100)?.ToString("N2").Replace(",", ".")}";

                //filtro por idade final
                if (modelquery.idade_final != null)
                    sqlFiltros += $@" AND CAST((SELECT IDADE FROM PRO_IDADE_PACIENTE(C.CSI_DTNASC, CURRENT_DATE)) AS NUMERIC(15,2)) <= {(modelquery.idade_final / 100)?.ToString("N2").Replace(",", ".")}";

                if (modelquery.equipes != null && modelquery.equipes.Length > 0)
                {
                    string joinEquipes = String.Join(", ", modelquery.equipes);
                    sqlFiltros += $@" AND EQ.ID IN({joinEquipes}) ";
                }

                if (modelquery.agente_saude != null)
                    sqlFiltros += $@" AND ME.CSI_CODMED = {modelquery.agente_saude} ";


                // Qtde. de registros
                var countRegistros = _repository.CountPublicoAlvo(ibge, sqlFiltros, quadrimestre, (int)ano);
                Response.Headers.Add("X-Total-Count", countRegistros.ToString());

                // Select + paginação
                modelquery.page = modelquery.page * modelquery.pagesize;
                sqlSelect = $@" SELECT FIRST {modelquery.pagesize} SKIP {modelquery.page} ";

                var itens = _repository.PublicoAlvo(ibge, sqlSelect, sqlFiltros, quadrimestre, (int)ano);

                return Ok(itens);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("Atendimentos/individuo/{id_individuo}")]
        public ActionResult Atendimentos([FromHeader] string ibge, [FromRoute] int id_individuo)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));

                var itens = _repository.Atendimentos(ibge, id_individuo);

                return Ok(itens);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }
    }
}