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
using RgCidadao.Api.ViewModels.Indicadores.Indicador3;
using RgCidadao.Domain.Repositories.Indicadores;

namespace RgCidadao.Api.Areas.Indicadores.Controllers
{
    [Route("api/Indicadores/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("Indicadores")]
    public class Indicador3Controller : ControllerBase
    {
        public IIndicador3Repository _repository;
        private IConfiguration _config;
        public Indicador3Controller(IIndicador3Repository repository, IConfiguration configuration)
        {
            _repository = repository;
            _config = configuration;
        }

        [HttpGet("{desdobramento}")]
        public ActionResult Indicador3([FromHeader]string ibge, [FromRoute] ParamsRouteIndicador3 model, [FromQuery] ParamsQueryIndicador3 modelquery)
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
                    sqlSelectTipo += $@" COALESCE(ME.CSI_CODMED,-1) CODIGO_AGENTE, COALESCE(ME.CSI_NOMMED,'SEM AGENTE DE SAÚDE') AGENTE, ";

                // Filtros
                sqlFiltros = buildSQLFiltros(modelquery);

                // Agrupamentos
                if (model.desdobramento == "unidade_saude")
                    sqlAgrupamento = $@" GROUP BY ES.ID, ES.NOME_FANTASIA ORDER BY ES.NOME_FANTASIA ";
                else if (model.desdobramento == "equipe")
                    sqlAgrupamento = $@" GROUP BY EQ.ID, EQ.NOME_REFERENCIA, EQ.DSC_AREA, EQ.COD_INE ORDER BY EQ.NOME_REFERENCIA ";
                else if (model.desdobramento == "agente_saude")
                    sqlAgrupamento = $@" GROUP BY ME.CSI_CODMED, ME.CSI_NOMMED ";

                var registros = _repository.Indicador3(ibge, sqlSelectTipo, sqlFiltros, sqlAgrupamento);

                if (model.desdobramento == "total")
                {
                    var total = new Indicador3TotalViewModel()
                    {
                        porcentagem = Helper.CalculaPorcentagem(
                            registros.FirstOrDefault().qtde_gestantes,
                            registros.FirstOrDefault().qtde_metas
                        ),
                        porcentagem_valida = Helper.CalculaPorcentagem(
                            registros.FirstOrDefault().qtde_gestantes,
                            registros.FirstOrDefault().qtde_metas_validas
                        ),
                        qtde_gestantes = registros.FirstOrDefault().qtde_gestantes,
                        qtde_metas = registros.FirstOrDefault().qtde_metas,
                        qtde_metas_validas = registros.FirstOrDefault().qtde_metas_validas,
                    };
                    return Ok(total);
                }
                else if (model.desdobramento == "unidade_saude")
                {
                    var unidades = new List<Indicador3UnidadeSaudeViewModel>();
                    foreach (var item in registros)
                    {
                        var unidade = new Indicador3UnidadeSaudeViewModel()
                        {
                            codigo_unidade = item.codigo_unidade,
                            porcentagem = Helper.CalculaPorcentagem(item.qtde_gestantes, item.qtde_metas),
                            porcentagem_valida = Helper.CalculaPorcentagem(item.qtde_gestantes, item.qtde_metas_validas),
                            qtde_gestantes = item.qtde_gestantes,
                            qtde_metas = item.qtde_metas,
                            qtde_metas_validas = item.qtde_metas_validas,
                            unidade = item.unidade_saude
                        };
                        unidades.Add(unidade);
                    }

                    return Ok(unidades);
                }
                else if (model.desdobramento == "equipe")
                {
                    var equipes = new List<Indicador3EquipeViewModel>();
                    foreach (var item in registros)
                    {
                        var equipe = new Indicador3EquipeViewModel()
                        {
                            codigo_equipe = item.codigo_equipe,
                            equipe = item.equipe,
                            porcentagem = Helper.CalculaPorcentagem(item.qtde_gestantes, item.qtde_metas),
                            porcentagem_valida = Helper.CalculaPorcentagem(item.qtde_gestantes, item.qtde_metas_validas),
                            qtde_gestantes = item.qtde_gestantes,
                            qtde_metas = item.qtde_metas,
                            qtde_metas_validas = item.qtde_metas_validas
                        };
                        equipes.Add(equipe);
                    }
                    return Ok(equipes);
                }
                else if (model.desdobramento == "agente_saude")
                {
                    var agentes = new List<Indicador3AgenteSaudeViewModel>();
                    foreach (var item in registros)
                    {
                        var agente = new Indicador3AgenteSaudeViewModel()
                        {
                            porcentagem = Helper.CalculaPorcentagem(item.qtde_gestantes, item.qtde_metas),
                            porcentagem_valida = Helper.CalculaPorcentagem(item.qtde_gestantes, item.qtde_metas_validas),
                            agente = item.agente,
                            codigo_agente = item.codigo_agente,
                            qtde_gestantes = item.qtde_gestantes,
                            qtde_metas = item.qtde_metas,
                            qtde_metas_validas = item.qtde_metas_validas
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
        public ActionResult PublicoAlvo([FromHeader]string ibge, [FromRoute] ParamsRouteIndicador3 model, [FromQuery] ParamsQueryIndicador3 modelquery)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));

                if (modelquery.pagesize == null)
                    modelquery.pagesize = 20;

                string sqlSelect = string.Empty;
                string sqlFiltros = string.Empty;
                string filtro_valido = string.Empty;

                // Filtros
                sqlFiltros = buildSQLFiltros(modelquery);

                #region Filtro por validação de Sistema e ESUS

                if (modelquery.sistema == 1) //válido
                    filtro_valido = $@"WHERE TAB.ATENDIDO = 1";
                else if (modelquery.sistema == 2) //inválido
                    filtro_valido = $@"WHERE TAB.ATENDIDO = 0";

                if (modelquery.valido == 1) //válido
                    filtro_valido += !string.IsNullOrWhiteSpace(filtro_valido) ? $@" AND TAB.ATENDIDO_VALIDO = 1" : $@" WHERE TAB.ATENDIDO_VALIDO = 1";
                else if (modelquery.valido == 2) //inválido
                    filtro_valido += !string.IsNullOrWhiteSpace(filtro_valido) ? $@" AND TAB.ATENDIDO_VALIDO = 0" : $@"WHERE TAB.ATENDIDO_VALIDO = 0";

                #endregion

                // Qtde. de registros
                var countRegistros = _repository.CountPublicoAlvo(ibge, sqlFiltros, filtro_valido);
                Response.Headers.Add("X-Total-Count", countRegistros.ToString());

                // Select + paginação
                modelquery.page = modelquery.page * modelquery.pagesize;
                sqlSelect = $@" SELECT FIRST {modelquery.pagesize} SKIP {modelquery.page} ";

                var itens = _repository.PublicoAlvo(ibge, sqlSelect, sqlFiltros, filtro_valido);

                return Ok(itens);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("Atendimentos/individuo/{id_individuo}")]
        public ActionResult Atendimentos([FromHeader] string ibge, [FromRoute] int id_individuo, [FromQuery] DateTime? dum, DateTime? gi_data_nascimento)
        {
            try
            {
                if (dum == null)
                {
                    return BadRequest(
                        new { message = "Para buscar os atendimentos do indivíduo, é necessário informar a data da DUM" }
                    );
                }

                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));

                string data_nascimento = (gi_data_nascimento != null)
                    ? $"CAST('{gi_data_nascimento?.ToString("yyyy-MM-dd")}' AS TIMESTAMP)"
                    : "NULL";

                string data_dum = dum?.ToString("yyyy-MM-dd");

                var itens = _repository.Atendimentos(ibge, id_individuo, data_dum, data_nascimento);

                return Ok(itens);

            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        private string buildSQLFiltros(ParamsQueryIndicador3 modelQuery)
        {
            string sqlFiltros = string.Empty;
            string quadrimestre = modelQuery.quadrimestre != null ? modelQuery.quadrimestre.ToString() : "NULL";
            if (modelQuery.ano == null)
                modelQuery.ano = DateTime.Now.Year;

            if (modelQuery.flg_pn_particular == false)
            {
                sqlFiltros += $@" COALESCE(GI.PRE_NATAL_PARTICULAR, 'F') = 'F'";
            }

            if (modelQuery.flg_em_andamento == true)
            {
                if (modelQuery.idade_gestacional_minima == null)
                    modelQuery.idade_gestacional_minima = 0;

                if (modelQuery.idade_gestacional_maxima == null)
                    modelQuery.idade_gestacional_maxima = 42;

                sqlFiltros += ((sqlFiltros != "") ? " AND " : "") + $@" (((GI.FLG_DESFECHO = 2)
                        AND (GI.DATA_NASCIMENTO BETWEEN (SELECT DATA FROM INICIO_QUADRIMESTRE({quadrimestre}, {modelQuery.ano}))
                        AND (SELECT DATA FROM FECHAMENTO_QUADRIMESTRE({quadrimestre}, {modelQuery.ano}))))
                        OR (GI.FLG_DESFECHO = 0 AND ((CURRENT_DATE - CAST(GI.DUM AS DATE)) / 7)
                        BETWEEN {modelQuery.idade_gestacional_minima} AND {modelQuery.idade_gestacional_maxima})) ";
            }
            else
            {
                sqlFiltros += ((sqlFiltros != "") ? " AND " : "") + $@" ((GI.FLG_DESFECHO = 2)
                        AND (GI.DATA_NASCIMENTO BETWEEN (SELECT DATA FROM INICIO_QUADRIMESTRE({quadrimestre}, {modelQuery.ano}))
                        AND (SELECT DATA FROM FECHAMENTO_QUADRIMESTRE({quadrimestre}, {modelQuery.ano})))) ";
            }

            if (!string.IsNullOrEmpty(modelQuery.equipes))
                sqlFiltros += $@" AND EQ.ID IN({modelQuery.equipes}) ";

            if (modelQuery.agente_saude != null)
                sqlFiltros += $@" AND ME.CSI_CODMED = {modelQuery.agente_saude} ";

            return sqlFiltros;
        }
    }
}