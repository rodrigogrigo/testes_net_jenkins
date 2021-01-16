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
using RgCidadao.Api.Helpers;
using RgCidadao.Api.ViewModels.Endemias;
using RgCidadao.Domain.Entities.Endemias;
using RgCidadao.Domain.Repositories.Endemias;

namespace RgCidadao.Api.Areas.Endemias.Controllers
{
    [Route("api/Endemias/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("Endemias")]
    public class CicloController : ControllerBase
    {
        private ICicloRepository _repository;
        private IConfiguration _config;
        public CicloController(ICicloRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _config = configuration;
        }

        [HttpGet("GetAllPagination")]
        public ActionResult<List<Ciclo>> GetAllPagination([FromHeader]string ibge, int page,
                                                          int pagesize, DateTime? datainicial, DateTime? datafinal, int? id,
                                                          int? situacao, string numciclo)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                string filtro = string.Empty;
                if (datainicial != null)
                    filtro += $@" DATA_INICIAL >= '{datainicial?.ToString("yyyy.MM.dd")}'";

                if (datafinal != null && !string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" AND DATA_FINAL <= '{datafinal?.ToString("yyyy.MM.dd")}'";
                else if (datafinal != null && string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" DATA_FINAL <= '{datafinal?.ToString("yyyy.MM.dd")}'";

                if (id != null && !string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" AND ID = {id}";
                else if (id != null && string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" ID = {id}";

                if (situacao != null && !string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" AND SITUACAO = {situacao} ";
                else if (situacao != null && string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" SITUACAO = {situacao}";

                if (!string.IsNullOrWhiteSpace(numciclo) && !string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" AND NUM_CICLO = '{numciclo}' ";
                else if (!string.IsNullOrWhiteSpace(numciclo) && string.IsNullOrWhiteSpace(filtro))
                    filtro += $@" NUM_CICLO = '{numciclo}'";

                if (!string.IsNullOrWhiteSpace(filtro))
                    filtro = $@" WHERE {filtro}";

                int count = _repository.GetCountAll(ibge, filtro);
                if (count == 1)
                    page = 0;
                else
                    page = page * pagesize;

                Response.Headers.Add("X-Total-Count", count.ToString());
                List<Ciclo> lista = _repository.GetAllPagination(ibge, filtro, page, pagesize);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetCiclosDisponiveis")]
        public ActionResult<NumCiclosDisponiveisViewModel> GetCiclosDisponiveis([FromHeader]string ibge, DateTime? dataInicial)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                var listautilizados = _repository.GetciclosUtilizados(ibge);

                var proximadata = listautilizados.OrderByDescending(x => x.data_final).Take(1).Select(x => x.data_final).FirstOrDefault();
                var lista = new List<NumCiclosDisponiveisViewModel>();
                lista.Add(new NumCiclosDisponiveisViewModel() { num_ciclo = $"1/{dataInicial?.Year}", data_proxima = proximadata?.AddDays(1) });
                lista.Add(new NumCiclosDisponiveisViewModel() { num_ciclo = $"2/{dataInicial?.Year}", data_proxima = proximadata?.AddDays(1) });
                lista.Add(new NumCiclosDisponiveisViewModel() { num_ciclo = $"3/{dataInicial?.Year}", data_proxima = proximadata?.AddDays(1) });
                lista.Add(new NumCiclosDisponiveisViewModel() { num_ciclo = $"4/{dataInicial?.Year}", data_proxima = proximadata?.AddDays(1) });
                lista.Add(new NumCiclosDisponiveisViewModel() { num_ciclo = $"5/{dataInicial?.Year}", data_proxima = proximadata?.AddDays(1) });
                lista.Add(new NumCiclosDisponiveisViewModel() { num_ciclo = $"6/{dataInicial?.Year}", data_proxima = proximadata?.AddDays(1) });

                foreach (var item in listautilizados)
                {
                    lista = lista.Where(x => x.num_ciclo != item.num_ciclo).ToList();
                }
                NumCiclosDisponiveisViewModel retorno = lista.Take(1).FirstOrDefault();

                if (retorno == null)
                    return BadRequest(TrataErro.GetResponse("Não há mais ciclos disponíveis para o período atual.", true));

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetCicloByData")]
        public ActionResult<List<Ciclo>> GetCicloByData([FromHeader]string ibge, DateTime? data)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<Ciclo> itens = _repository.GetCicloByData(ibge, data);
                return Ok(itens);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetAllCiclosAtivos")]
        public ActionResult<List<Ciclo>> GetAllCiclosAtivos([FromHeader] string ibge)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<Ciclo> lista = _repository.GetAllCiclosAtivos(ibge);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetAll")]
        public ActionResult<List<Ciclo>> GetAll([FromHeader] string ibge)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                List<Ciclo> lista = _repository.GetAll(ibge);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPost("Inserir")]
        public ActionResult Inserir([FromHeader] string ibge, [FromBody]Ciclo model)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                //valida se existe ciclo com o mesmo período
                var itens = _repository.ValidaExistenciaCicloPeriodo(ibge, model.data_inicial, model.data_final);
                if (itens.Count > 0)
                    return BadRequest(TrataErro.GetResponse("Já existe um ciclo vigente no período informado.", true));

                model.id = _repository.GetNewId(ibge);
                _repository.Insert(ibge, model);

                var principal = HttpContext.User;
                int? id_usuario = null;
                if (principal?.Identities?.FirstOrDefault().Claims != null)
                    id_usuario = Convert.ToInt32(principal?.Identities?.FirstOrDefault().Claims.FirstOrDefault()?.Value);

                var ciclolog = new CicloLog
                {
                    id_ciclo = model.id,
                    situacao = model.situacao,
                    id_usuario = model.id_usuario
                };

                _repository.InserirLogCiclo(ibge, ciclolog);

                return Ok(model);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetCicloById/{id}")]
        public ActionResult<Ciclo> GetCicloById([FromHeader] string ibge, [FromRoute]int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                Ciclo item = _repository.GetCicloById(ibge, id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetLogCicloByCiclo/{id_ciclo}")]
        public ActionResult<List<CicloLog>> GetLogCicloByCiclo([FromHeader] string ibge, [FromRoute]int id_ciclo)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));

                List<CicloLog> item = _repository.GetLogCicloByCiclo(ibge, id_ciclo);
                return Ok(item);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPut("Editar/{id}")]
        public ActionResult Editar([FromHeader] string ibge, [FromBody]Ciclo model, [FromRoute] int? id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                //valida se existe ciclo com o mesmo período
                var itens = _repository.ValidaExistenciaCicloPeriodo(ibge, model.data_inicial, model.data_final);
                if (itens.Count > 0)
                    return BadRequest(TrataErro.GetResponse("Já existe um ciclo vigente no período informado.", true));

                var countvisitas = _repository.CountVisitasCiclo(ibge, (int)id, model.data_inicial, model.data_inicial);
                if (countvisitas > 0)
                {
                    var datainicial = _repository.GetDataMaximaCiclo(ibge, (int)id);
                    var datafinal = _repository.GetDataMinimaCiclo(ibge, (int)id);

                    return BadRequest(TrataErro.GetResponseCicloEdit($"As datas possíveis para o ciclo são entre {datainicial?.ToString("dd/MM/yyyy")} e {datafinal?.ToString("dd/MM/yyyy")}, pois já existem visitas entre essas datas.", true, datainicial, datafinal));
                }

                model.id = id;
                _repository.Update(ibge, model);

                var principal = HttpContext.User;
                int? id_usuario = null;
                if (principal?.Identities?.FirstOrDefault().Claims != null)
                    id_usuario = Convert.ToInt32(principal?.Identities?.FirstOrDefault().Claims.FirstOrDefault()?.Value);

                //insere log
                var ciclolog = new CicloLog
                {
                    id_ciclo = model.id,
                    situacao = model.situacao,
                    id_usuario = id_usuario
                };

                _repository.InserirLogCiclo(ibge, ciclolog);
                return Ok(model);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpDelete("Excluir/{id}")]
        public ActionResult Excluir([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Connection.GetConnection(ibge));
                _repository.Excluir(ibge, id);
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