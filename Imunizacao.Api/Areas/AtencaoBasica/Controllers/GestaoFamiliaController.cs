using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RgCidadao.Api.Enums;
using RgCidadao.Api.Filters;
using RgCidadao.Api.Helpers;
using RgCidadao.Api.ViewModels.AtencaoBasica;
using RgCidadao.Domain.Repositories.AtencaoBasica;
using RgCidadao.Domain.Repositories.Cadastro;
using RgCidadao.Domain.ViewModels;
using RgCidadao.Domain.ViewModels.AtencaoBasica;

namespace RgCidadao.Api.Areas.AtencaoBasica.Controllers
{
    [Route("api/AtencaoBasica/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("AtencaoBasica")]
    public class GestaoFamiliaController : ControllerBase
    {
        public IGestaoFamiliaRepository _repository;
        public ISegUserRepository _seguserRepository;
        public IEquipeRepository _equipeRepository;
        private IConfiguration _config;

        public GestaoFamiliaController(
            IGestaoFamiliaRepository repository,
            ISegUserRepository seguserRepository,
            IEquipeRepository equipeRepository,
            IConfiguration configuration
        )
        {
            _repository = repository;
            _seguserRepository = seguserRepository;
            _equipeRepository = equipeRepository;
            _config = configuration;
        }

        [HttpGet("GetGestaoFamilia")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<GestaoFamiliaViewModel>> GetGestaoFamilia([FromHeader]string ibge, int competencia, int microarea)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                string filtro = string.Empty;
                List<GestaoFamiliaViewModel> lista = _repository.GetGestaoFamilia(ibge, competencia, microarea);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetEstatisticasByMicroarea")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<EstatisticaGestaoFamiliaViewModel> GetEstatisticaByMicroarea([FromHeader]string ibge, int microarea, int competencia)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                EstatisticaGestaoFamiliaViewModel lista = _repository.GetEstatisticasByMicroarea(ibge, microarea, competencia);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetEstatisticasByEquipes")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<EstatisticaGestaoFamiliaViewModel>> GetEstatisticaByEquipes([FromHeader] string ibge, [FromQuery] string ids_equipes, int competencia)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                string sqlFiltros = string.Empty;

                if (string.IsNullOrEmpty(ids_equipes))
                {
                    string filtrosEquipe = string.Empty;

                    var id_usuario = Convert.ToInt32(HttpContext.User.Identities?.FirstOrDefault().Claims.FirstOrDefault()?.Value);
                    int? tipoUsuario = _seguserRepository.GetTipoUsuarioById(ibge, id_usuario);

                    if (tipoUsuario == 3)
                    {
                        ids_equipes = GetIdsEquipesUsuarioPossuiAcesso(ibge, id_usuario);
                        sqlFiltros += $@" WHERE MI.ID_EQUIPE IN ({ids_equipes}) ";
                    }
                }
                else
                {
                    sqlFiltros += $@" WHERE MI.ID_EQUIPE IN ({ids_equipes}) ";
                }


                EstatisticaGestaoFamiliaViewModel item = _repository.GetEstatisticasByEquipes(ibge, sqlFiltros, competencia);

                return Ok(new
                {
                    familia = new
                    {
                        total = item.familia_total,
                        visitado = item.familia_visitada,
                        porcentagem = Helper.CalculaPorcentagem((double)item.familia_total, (double)item.familia_visitada),
                    },
                    individuo = new
                    {
                        total = item.individuo_total,
                        visitado = item.individuo_visitado,
                        porcentagem = Helper.CalculaPorcentagem((double)item.individuo_total, (double)item.individuo_visitado),
                    },
                    diabetico = new
                    {
                        total = item.diabetico_total,
                        visitado = item.diabetico_visitado,
                        porcentagem = Helper.CalculaPorcentagem((double)item.diabetico_total, (double)item.diabetico_visitado),
                    },
                    hipertenso = new
                    {
                        total = item.hipertenso_total,
                        visitado = item.hipertenso_visitado,
                        porcentagem = Helper.CalculaPorcentagem((double)item.hipertenso_total, (double)item.hipertenso_visitado),
                    },
                    gestante = new
                    {
                        total = item.gestante_total,
                        visitado = item.gestante_visitado,
                        porcentagem = Helper.CalculaPorcentagem((double)item.gestante_total, (double)item.gestante_visitado),
                    },
                    crianca = new
                    {
                        total = item.crianca_total,
                        visitado = item.crianca_visitado,
                        porcentagem = Helper.CalculaPorcentagem((double)item.crianca_total, (double)item.crianca_visitado),
                    },
                    idoso = new
                    {
                        total = item.idoso_total,
                        visitado = item.idoso_visitado,
                        porcentagem = Helper.CalculaPorcentagem((double)item.idoso_total, (double)item.idoso_visitado),
                    },
                });
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetMembrosByFamilia/{id_familia}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<MembroFamiliaViewModel>> GetMembrosByFamilia([FromHeader]string ibge, [FromRoute] int id_familia)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                string filtro = string.Empty;
                List<MembroFamiliaViewModel> lista = _repository.GetMembrosByFamilia(ibge, id_familia);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetDiabeticosByEquipe")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<GrupoIndividuosEquipeViewModel> GetDiabeticosByEquipe([FromHeader]string ibge, [FromQuery] QueryParamsGrupoIndividuos modelQuery)
        {
            try
            {
                if (modelQuery.competencia == null)
                {
                    var response = TrataErro.GetResponse("É necessário informar a competência para realizar a consulta", true);
                    return BadRequest(response);
                }

                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));


                string sqlFiltros1 = string.Empty;
                string sqlFiltros2 = string.Empty;
                int countRegistros = 0;

                if (string.IsNullOrEmpty(modelQuery.ids_equipes))
                {
                    string filtrosEquipe = string.Empty;

                    var id_usuario = Convert.ToInt32(HttpContext.User.Identities?.FirstOrDefault().Claims.FirstOrDefault()?.Value);
                    int? tipoUsuario = _seguserRepository.GetTipoUsuarioById(ibge, id_usuario);

                    if (tipoUsuario == 3)
                    {
                        modelQuery.ids_equipes = GetIdsEquipesUsuarioPossuiAcesso(ibge, id_usuario);
                        sqlFiltros1 += $@" MI.ID_EQUIPE IN ({modelQuery.ids_equipes}) AND ";
                    }
                }
                else
                    sqlFiltros1 += $@" MI.ID_EQUIPE IN ({modelQuery.ids_equipes}) AND ";

                var contagemgeral = _repository.GetTotaisDiabeticoByEquipe(ibge, sqlFiltros2, (int)modelQuery.competencia);

                if (modelQuery.status == (int)SituacaoGestaoFamilia.visitados) // visitados
                {
                    countRegistros = (int)contagemgeral.visitados;
                    sqlFiltros1 = $@" AND (VIS.DESFECHO = 0)";
                    sqlFiltros2 += $@" TAB.VISITADO = 0";
                }
                else if (modelQuery.status == SituacaoGestaoFamilia.nao_visitados) // não visitados
                {
                    countRegistros = (int)contagemgeral.nao_visitados;
                    sqlFiltros1 = $@" ";
                    sqlFiltros2 += $@" TAB.VISITADO IS NULL";
                    
                }
                else if (modelQuery.status == (int)SituacaoGestaoFamilia.ausentes_recusados) //ausentes/recusados
                {
                    countRegistros = (int)contagemgeral.ausentes_recusados;
                    sqlFiltros1 = $@" AND (VIS.DESFECHO = 1 OR
                                      VIS.DESFECHO = 2)";
                    sqlFiltros2 += $@" TAB.VISITADO = 1 OR TAB.VISITADO = 2";
                    
                }
                else
                    countRegistros = (int)contagemgeral.total; //(int)(contagemgeral.ausentes_recusados + contagemgeral.nao_visitados + contagemgeral.visitados);

                if (!string.IsNullOrWhiteSpace(sqlFiltros2))
                    sqlFiltros2 = $" WHERE " + sqlFiltros2;

                Response.Headers.Add("X-Total-Count", countRegistros.ToString());

                string sqlSelect = $@" SELECT FIRST({modelQuery.page_size}) SKIP({modelQuery.page * modelQuery.page_size}) ";
                List<GrupoIndividuosEquipeViewModel> lista = _repository.GetDiabeticosByEquipe(ibge, sqlSelect, sqlFiltros1, (int)modelQuery.competencia, sqlFiltros2);
                lista = lista.OrderBy(x => x.nome_individuo).ToList();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetHipertensosByEquipe")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<GrupoIndividuosEquipeViewModel>> GetHipertensosByEquipe([FromHeader] string ibge, [FromQuery] QueryParamsGrupoIndividuos modelQuery)
        {
            try
            {
                if (modelQuery.competencia == null)
                {
                    var response = TrataErro.GetResponse("É necessário informar a competência para realizar a consulta", true);
                    return BadRequest(response);
                }

                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));

                string sqlSelect = $@" SELECT FIRST({modelQuery.page_size}) SKIP({(modelQuery.page * modelQuery.page_size)}) ";
                string sqlFiltros1 = string.Empty;
                string sqlFiltros2 = string.Empty;
                int countRegistros = 0;

                if (string.IsNullOrEmpty(modelQuery.ids_equipes))
                {
                    string filtrosEquipe = string.Empty;

                    var id_usuario = Convert.ToInt32(HttpContext.User.Identities?.FirstOrDefault().Claims.FirstOrDefault()?.Value);
                    int? tipoUsuario = _seguserRepository.GetTipoUsuarioById(ibge, id_usuario);

                    if (tipoUsuario == 3)
                    {
                        modelQuery.ids_equipes = GetIdsEquipesUsuarioPossuiAcesso(ibge, id_usuario);
                        sqlFiltros1 += $@" MI.ID_EQUIPE IN ({modelQuery.ids_equipes}) AND";
                    }
                }
                else
                    sqlFiltros1 += $@" MI.ID_EQUIPE IN ({modelQuery.ids_equipes}) AND";

                var contagemgeral = _repository.GetTotaisHipertensoByEquipe(ibge, sqlFiltros2, (int)modelQuery.competencia);


                if (modelQuery.status == (int)SituacaoGestaoFamilia.visitados) // visitados
                {
                    countRegistros = (int)contagemgeral.visitados;
                    sqlFiltros1 = $@" AND (VIS.DESFECHO = 0)";
                    sqlFiltros2 += $@" TAB.VISITADO = 0";
                }
                else if (modelQuery.status == (int)SituacaoGestaoFamilia.nao_visitados) //não visitado
                {
                    countRegistros = (int)contagemgeral.nao_visitados;
                    sqlFiltros1 = $@" ";
                    sqlFiltros2 += $@" TAB.VISITADO IS NULL";
                }
                else if (modelQuery.status == (int)SituacaoGestaoFamilia.ausentes_recusados) //ausentes/recusados
                {
                    countRegistros = (int)contagemgeral.ausentes_recusados;
                    sqlFiltros1 = $@" AND (VIS.DESFECHO = 1 OR
                                      VIS.DESFECHO = 2)";
                    sqlFiltros2 += $@" TAB.VISITADO = 1 OR TAB.VISITADO = 2";
                }
                else
                    countRegistros = (int)contagemgeral.total; //(int)(contagemgeral.ausentes_recusados + contagemgeral.nao_visitados + contagemgeral.visitados);

                if (!string.IsNullOrWhiteSpace(sqlFiltros2))
                    sqlFiltros2 = $" WHERE " + sqlFiltros2;

                Response.Headers.Add("X-Total-Count", countRegistros.ToString());

                List<GrupoIndividuosEquipeViewModel> lista = _repository.GetHipertensosByEquipe(ibge, sqlSelect, sqlFiltros1, (int)modelQuery.competencia, sqlFiltros2);
                lista = lista.OrderBy(x => x.nome_individuo).ToList();

                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetGestantesByEquipe")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<GrupoIndividuosEquipeViewModel>> GetGestantesByEquipe([FromHeader] string ibge, [FromQuery] QueryParamsGrupoIndividuos modelQuery)

        {
            try
            {
                if (modelQuery.competencia == null)
                {
                    var response = TrataErro.GetResponse("É necessário informar a competência para realizar a consulta", true);
                    return BadRequest(response);
                }

                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));

                string sqlSelect = $@" SELECT FIRST({modelQuery.page_size}) SKIP({(modelQuery.page * modelQuery.page_size)}) ";
                string sqlFiltros1 = string.Empty;
                string sqlFiltros2 = string.Empty;
                int countRegistros = 0;

                if (string.IsNullOrEmpty(modelQuery.ids_equipes))
                {
                    string filtrosEquipe = string.Empty;

                    var id_usuario = Convert.ToInt32(HttpContext.User.Identities?.FirstOrDefault().Claims.FirstOrDefault()?.Value);
                    int? tipoUsuario = _seguserRepository.GetTipoUsuarioById(ibge, id_usuario);

                    if (tipoUsuario == 3)
                    {
                        modelQuery.ids_equipes = GetIdsEquipesUsuarioPossuiAcesso(ibge, id_usuario);
                        sqlFiltros2 += $@" MI.ID_EQUIPE IN ({modelQuery.ids_equipes}) AND";
                    }
                }
                else
                    sqlFiltros2 += $@" MI.ID_EQUIPE IN ({modelQuery.ids_equipes}) AND";

                var contagemgeral = _repository.GetTotaisGestanteByEquipe(ibge, sqlFiltros2, (int)modelQuery.competencia);

                if (modelQuery.status == (int)SituacaoGestaoFamilia.visitados) // visitados
                {
                    countRegistros = (int)contagemgeral.visitados;
                    sqlFiltros1 = $@" AND (VIS.DESFECHO = 0)";
                    sqlFiltros2 += $@" TAB.VISITADO = 0";
                }
                else if (modelQuery.status == (int)SituacaoGestaoFamilia.nao_visitados) // não visitados
                {
                    countRegistros = (int)contagemgeral.nao_visitados;
                    sqlFiltros1 = $@" ";
                    sqlFiltros2 += $@" TAB.VISITADO IS NULL";
                }
                else if (modelQuery.status == (int)SituacaoGestaoFamilia.ausentes_recusados) //ausentes/recusados
                {
                    countRegistros = (int)contagemgeral.ausentes_recusados;
                    sqlFiltros1 = $@" AND (VIS.DESFECHO = 1 OR
                                      VIS.DESFECHO = 2)";
                    sqlFiltros2 += $@" TAB.VISITADO = 1 OR TAB.VISITADO = 2";
                }
                else
                    countRegistros = (int)contagemgeral.total; //(int)(contagemgeral.ausentes_recusados + contagemgeral.nao_visitados + contagemgeral.visitados);

                if (!string.IsNullOrWhiteSpace(sqlFiltros2))
                    sqlFiltros2 = $" WHERE " + sqlFiltros2;

                Response.Headers.Add("X-Total-Count", countRegistros.ToString());

                List<GrupoIndividuosEquipeViewModel> lista = _repository.GetGestantesByEquipe(ibge, sqlSelect, sqlFiltros1, (int)modelQuery.competencia, sqlFiltros2);
                lista = lista.OrderBy(x => x.nome_individuo).ToList();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetCriancasByEquipe")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<GrupoIndividuosEquipeViewModel>> GetCriancasByEquipe([FromHeader] string ibge, [FromQuery] QueryParamsGrupoIndividuos modelQuery)
        {
            try
            {
                if (modelQuery.competencia == null)
                {
                    var response = TrataErro.GetResponse("É necessário informar a competência para realizar a consulta", true);
                    return BadRequest(response);
                }

                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));

                string sqlSelect = $@" SELECT FIRST({modelQuery.page_size}) SKIP({(modelQuery.page * modelQuery.page_size)}) ";
                string sqlFiltros1 = string.Empty;
                string sqlFiltros2 = string.Empty;
                int countRegistros = 0;

                if (string.IsNullOrEmpty(modelQuery.ids_equipes))
                {
                    string filtrosEquipe = string.Empty;

                    var id_usuario = Convert.ToInt32(HttpContext.User.Identities?.FirstOrDefault().Claims.FirstOrDefault()?.Value);
                    int? tipoUsuario = _seguserRepository.GetTipoUsuarioById(ibge, id_usuario);

                    if (tipoUsuario == 3)
                    {
                        modelQuery.ids_equipes = GetIdsEquipesUsuarioPossuiAcesso(ibge, id_usuario);
                        sqlFiltros1 += $@" MI.ID_EQUIPE IN ({modelQuery.ids_equipes}) AND";
                    }
                }
                else
                    sqlFiltros1 += $@" MI.ID_EQUIPE IN ({modelQuery.ids_equipes}) AND";

                var contagemgeral = _repository.GetTotaisCriancasByEquipe(ibge, sqlFiltros2, (int)modelQuery.competencia);

                if (modelQuery.status == (int)SituacaoGestaoFamilia.visitados) // visitados
                {
                    countRegistros = (int)contagemgeral.visitados;
                    sqlFiltros1 = $@" AND (VIS.DESFECHO = 0)";
                    sqlFiltros2 += $@" TAB.VISITADO = 0";
                }
                else if (modelQuery.status == (int)SituacaoGestaoFamilia.nao_visitados) // não visitados
                {
                    countRegistros = (int)contagemgeral.nao_visitados;
                    sqlFiltros1 = $@" ";
                    sqlFiltros2 += $@" TAB.VISITADO IS NULL";
                }
                else if (modelQuery.status == (int)SituacaoGestaoFamilia.ausentes_recusados) //ausentes/recusados
                {
                    countRegistros = (int)contagemgeral.ausentes_recusados;
                    sqlFiltros1 = $@" AND (VIS.DESFECHO = 1 OR
                                      VIS.DESFECHO = 2)";
                    sqlFiltros2 += $@" TAB.VISITADO = 1 OR TAB.VISITADO = 2";
                }
                else
                    countRegistros = (int)contagemgeral.total; //(int)(contagemgeral.ausentes_recusados + contagemgeral.nao_visitados + contagemgeral.visitados);

                if (!string.IsNullOrWhiteSpace(sqlFiltros2))
                    sqlFiltros2 = $" WHERE " + sqlFiltros2;

                Response.Headers.Add("X-Total-Count", countRegistros.ToString());

                List<GrupoIndividuosEquipeViewModel> lista = _repository.GetCriancasByEquipe(ibge, sqlSelect, sqlFiltros1, (int)modelQuery.competencia, sqlFiltros2);
                lista = lista.OrderBy(x => x.nome_individuo).ToList();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetIdososByEquipe")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<GrupoIndividuosEquipeViewModel>> GetIdososByEquipe([FromHeader] string ibge, [FromQuery] QueryParamsGrupoIndividuos modelQuery)
        {
            try
            {
                if (modelQuery.competencia == null)
                {
                    var response = TrataErro.GetResponse("É necessário informar a competência para realizar a consulta", true);
                    return BadRequest(response);
                }

                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));

                string sqlFiltros1 = string.Empty;
                string sqlFiltros2 = string.Empty;
                int countRegistros = 0;

                if (string.IsNullOrEmpty(modelQuery.ids_equipes))
                {
                    string filtrosEquipe = string.Empty;

                    var id_usuario = Convert.ToInt32(HttpContext.User.Identities?.FirstOrDefault().Claims.FirstOrDefault()?.Value);
                    int? tipoUsuario = _seguserRepository.GetTipoUsuarioById(ibge, id_usuario);

                    if (tipoUsuario == 3)
                    {
                        modelQuery.ids_equipes = GetIdsEquipesUsuarioPossuiAcesso(ibge, id_usuario);
                        sqlFiltros1 += $@" MI.ID_EQUIPE IN ({modelQuery.ids_equipes}) AND";
                    }
                }
                else
                    sqlFiltros1 += $@" MI.ID_EQUIPE IN ({modelQuery.ids_equipes}) AND";

                var contagemgeral = _repository.GetTotaisIdososByEquipe(ibge, sqlFiltros2, (int)modelQuery.competencia);

                if (modelQuery.status == (int)SituacaoGestaoFamilia.visitados) // visitados
                {
                    countRegistros = (int)contagemgeral.visitados;
                    sqlFiltros1 = $@" AND (VIS.DESFECHO = 0)";
                    sqlFiltros2 += $@" TAB.VISITADO = 0";
                }
                else if (modelQuery.status == (int)SituacaoGestaoFamilia.nao_visitados) // não visitados
                {
                    countRegistros = (int)contagemgeral.nao_visitados;
                    sqlFiltros1 = $@" ";
                    sqlFiltros2 += $@" TAB.VISITADO IS NULL";
                }
                else if (modelQuery.status == (int)SituacaoGestaoFamilia.ausentes_recusados) //ausentes/recusados
                {
                    countRegistros = (int)contagemgeral.ausentes_recusados;
                    sqlFiltros1 = $@" AND (VIS.DESFECHO = 1 OR
                                      VIS.DESFECHO = 2)";
                    sqlFiltros2 += $@" TAB.VISITADO = 1 OR TAB.VISITADO = 2";
                }
                else
                    countRegistros = (int)contagemgeral.total; //(int)(contagemgeral.ausentes_recusados + contagemgeral.nao_visitados + contagemgeral.visitados);

                if (!string.IsNullOrWhiteSpace(sqlFiltros2))
                    sqlFiltros2 = $" WHERE " + sqlFiltros2;

                Response.Headers.Add("X-Total-Count", countRegistros.ToString());

                string sqlSelect = $@" SELECT FIRST({modelQuery.page_size}) SKIP({modelQuery.page * modelQuery.page_size}) ";
                List<GrupoIndividuosEquipeViewModel> lista = _repository.GetIdososByEquipe(ibge, sqlSelect, sqlFiltros1, (int)modelQuery.competencia, sqlFiltros2);
                lista = lista.OrderBy(x => x.nome_individuo).ToList();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        private string GetIdsEquipesUsuarioPossuiAcesso(string ibge, int id_usuario)
        {
            string filtrosEquipe = string.Empty;

            filtrosEquipe += $@" WHERE UN.CSI_CODUNI IN (
                SELECT PU.ID_UNIDADE
                FROM SEG_PERFIL_USUARIO PU
                WHERE PU.ID_USUARIO = {id_usuario})";

            var equipes = _equipeRepository.GetEquipeByPerfil(ibge, filtrosEquipe);

            string ids_equipes_usuario3 = String.Join(
                ",", equipes.Select(equipe => equipe.id.ToString()).ToArray()
            );

            return ids_equipes_usuario3;
        }

        [HttpGet("GetTotaisDiabeticoByEquipe")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<TotalPorEquipeViewModel> GetTotaisDiabeticoByEquipe([FromHeader] string ibge, [FromQuery] QueryParamsGrupoIndividuos modelQuery)
        {
            try
            {
                if (modelQuery.competencia == null)
                {
                    var response = TrataErro.GetResponse("É necessário informar a competência para realizar a consulta", true);
                    return BadRequest(response);
                }

                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));

                string sqlFiltros = string.Empty;
                if (string.IsNullOrEmpty(modelQuery.ids_equipes))
                {
                    string filtrosEquipe = string.Empty;

                    var id_usuario = Convert.ToInt32(HttpContext.User.Identities?.FirstOrDefault().Claims.FirstOrDefault()?.Value);
                    int? tipoUsuario = _seguserRepository.GetTipoUsuarioById(ibge, id_usuario);

                    if (tipoUsuario == 3)
                    {
                        modelQuery.ids_equipes = GetIdsEquipesUsuarioPossuiAcesso(ibge, id_usuario);
                        sqlFiltros += $@" MI.ID_EQUIPE IN ({modelQuery.ids_equipes}) AND ";
                    }
                }
                else
                    sqlFiltros += $@" MI.ID_EQUIPE IN ({modelQuery.ids_equipes}) AND ";

                var item = _repository.GetTotaisDiabeticoByEquipe(ibge, sqlFiltros, (int)modelQuery.competencia);

                return Ok(item);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetTotaisHipertensoByEquipe")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<TotalPorEquipeViewModel> GetTotaisHipertensoByEquipe([FromHeader] string ibge, [FromQuery] QueryParamsGrupoIndividuos modelQuery)
        {
            try
            {
                if (modelQuery.competencia == null)
                {
                    var response = TrataErro.GetResponse("É necessário informar a competência para realizar a consulta", true);
                    return BadRequest(response);
                }

                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));

                string sqlFiltros = string.Empty;
                if (string.IsNullOrEmpty(modelQuery.ids_equipes))
                {
                    string filtrosEquipe = string.Empty;

                    var id_usuario = Convert.ToInt32(HttpContext.User.Identities?.FirstOrDefault().Claims.FirstOrDefault()?.Value);
                    int? tipoUsuario = _seguserRepository.GetTipoUsuarioById(ibge, id_usuario);

                    if (tipoUsuario == 3)
                    {
                        modelQuery.ids_equipes = GetIdsEquipesUsuarioPossuiAcesso(ibge, id_usuario);
                        sqlFiltros += $@" MI.ID_EQUIPE IN ({modelQuery.ids_equipes}) AND ";
                    }
                }
                else
                    sqlFiltros += $@" MI.ID_EQUIPE IN ({modelQuery.ids_equipes}) AND ";

                var item = _repository.GetTotaisHipertensoByEquipe(ibge, sqlFiltros, (int)modelQuery.competencia);

                return Ok(item);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetTotaisGestanteByEquipe")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<TotalPorEquipeViewModel> GetTotaisGestanteByEquipe([FromHeader] string ibge, [FromQuery] QueryParamsGrupoIndividuos modelQuery)
        {
            try
            {
                if (modelQuery.competencia == null)
                {
                    var response = TrataErro.GetResponse("É necessário informar a competência para realizar a consulta", true);
                    return BadRequest(response);
                }

                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));

                string sqlFiltros = string.Empty;
                if (string.IsNullOrEmpty(modelQuery.ids_equipes))
                {
                    string filtrosEquipe = string.Empty;

                    var id_usuario = Convert.ToInt32(HttpContext.User.Identities?.FirstOrDefault().Claims.FirstOrDefault()?.Value);
                    int? tipoUsuario = _seguserRepository.GetTipoUsuarioById(ibge, id_usuario);

                    if (tipoUsuario == 3)
                    {
                        modelQuery.ids_equipes = GetIdsEquipesUsuarioPossuiAcesso(ibge, id_usuario);
                        sqlFiltros += $@" MI.ID_EQUIPE IN ({modelQuery.ids_equipes}) AND ";
                    }
                }
                else
                    sqlFiltros += $@" MI.ID_EQUIPE IN ({modelQuery.ids_equipes}) AND ";

                var item = _repository.GetTotaisGestanteByEquipe(ibge, sqlFiltros, (int)modelQuery.competencia);

                return Ok(item);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetTotaisCriancasByEquipe")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<TotalPorEquipeViewModel> GetTotaisCriancasByEquipe([FromHeader] string ibge, [FromQuery] QueryParamsGrupoIndividuos modelQuery)
        {
            try
            {
                if (modelQuery.competencia == null)
                {
                    var response = TrataErro.GetResponse("É necessário informar a competência para realizar a consulta", true);
                    return BadRequest(response);
                }

                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));

                string sqlFiltros = string.Empty;
                if (string.IsNullOrEmpty(modelQuery.ids_equipes))
                {
                    string filtrosEquipe = string.Empty;

                    var id_usuario = Convert.ToInt32(HttpContext.User.Identities?.FirstOrDefault().Claims.FirstOrDefault()?.Value);
                    int? tipoUsuario = _seguserRepository.GetTipoUsuarioById(ibge, id_usuario);

                    if (tipoUsuario == 3)
                    {
                        modelQuery.ids_equipes = GetIdsEquipesUsuarioPossuiAcesso(ibge, id_usuario);
                        sqlFiltros += $@" MI.ID_EQUIPE IN ({modelQuery.ids_equipes}) AND ";
                    }
                }
                else
                    sqlFiltros += $@" MI.ID_EQUIPE IN ({modelQuery.ids_equipes}) AND ";

                var item = _repository.GetTotaisCriancasByEquipe(ibge, sqlFiltros, (int)modelQuery.competencia);

                return Ok(item);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetTotaisIdososByEquipe")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<TotalPorEquipeViewModel> GetTotaisIdososByEquipe([FromHeader] string ibge, [FromQuery] QueryParamsGrupoIndividuos modelQuery)
        {
            try
            {
                if (modelQuery.competencia == null)
                {
                    var response = TrataErro.GetResponse("É necessário informar a competência para realizar a consulta", true);
                    return BadRequest(response);
                }

                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));

                string sqlFiltros = string.Empty;
                if (string.IsNullOrEmpty(modelQuery.ids_equipes))
                {
                    string filtrosEquipe = string.Empty;

                    var id_usuario = Convert.ToInt32(HttpContext.User.Identities?.FirstOrDefault().Claims.FirstOrDefault()?.Value);
                    int? tipoUsuario = _seguserRepository.GetTipoUsuarioById(ibge, id_usuario);

                    if (tipoUsuario == 3)
                    {
                        modelQuery.ids_equipes = GetIdsEquipesUsuarioPossuiAcesso(ibge, id_usuario);
                        sqlFiltros += $@" MI.ID_EQUIPE IN ({modelQuery.ids_equipes}) AND ";
                    }
                }
                else
                    sqlFiltros += $@" MI.ID_EQUIPE IN ({modelQuery.ids_equipes}) AND ";

                var item = _repository.GetTotaisIdososByEquipe(ibge, sqlFiltros, (int)modelQuery.competencia);

                return Ok(item);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        public int SomaVisitas(TotalPorEquipeViewModel item)
        {
            try
            {
                item.total = (item.visitados + item.nao_visitados + item.ausentes_recusados);

                return (int)item.total;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}