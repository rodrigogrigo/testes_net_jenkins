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
using RgCidadao.Api.Filters;
using RgCidadao.Api.Helpers;
using RgCidadao.Api.ViewModels.AtencaoBasica;
using RgCidadao.Domain.Entities.AtencaoBasica;
using RgCidadao.Domain.Entities.Cadastro;
using RgCidadao.Domain.Repositories.AtencaoBasica;
using RgCidadao.Domain.Repositories.Cadastro;
using RgCidadao.Domain.ViewModels.AtencaoBasica;

namespace RgCidadao.Api.Areas.AtencaoBasica.Controllers
{
    [Route("api/AtencaoBasica/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("SiteCorsPolicy")]
    [Area("AtencaoBasica")]
    public class AgendaController : ControllerBase
    {
        private IConfiguration _config;
        private IAgendaRepository _repository;
        private IFeriadoRepository _feriadosrepository;
        public AgendaController(IConfiguration config, IAgendaRepository repository, IFeriadoRepository feriadoRepository)
        {
            _repository = repository;
            _config = config;
            _feriadosrepository = feriadoRepository;
        }

        [HttpGet("GetAll")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<AgendaHorariosViewModel>> GetAll([FromHeader]string ibge, DateTime data, int id_profissional)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                string filtro = string.Empty;

                List<AgendaHorariosViewModel> lista = _repository.GetAll(ibge, data, id_profissional);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetAgendaTipos")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<AgendaDiasViewModel>> GetAgendaTipos([FromHeader]string ibge, int profissional, int mes, int ano)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                DateTime dataAtual = new DateTime(ano, mes, 1);
                DateTime? datainicial = dataAtual.AddMonths(-1);
                DateTime? datafinal = dataAtual.AddMonths(1);

                List<AgendaDiasViewModel> itens = _repository.GetAgendaDias(ibge, profissional, datainicial, datafinal);
                List<Feriado> feriados = _feriadosrepository.GetAll(ibge).Where(x => x.csi_data >= DateTime.Now).ToList();

                foreach (var item in feriados)
                {
                    if (!itens.Any(x => x.Data == item.csi_data))
                    {
                        AgendaDiasViewModel itensAgenda = new AgendaDiasViewModel();
                        itensAgenda.Data = item.csi_data;
                        itensAgenda.tipo = 2;

                        itens.Add(itensAgenda);
                    }
                }

                return Ok(itens);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        #region Configuração de Agenda
        [HttpGet("GetConfiguracaoProjetoByData")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<List<ConfiguraAgendaViewModel>> GetConfiguracaoProjetoByData(string ibge, int codmed, DateTime data)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                List<ConfiguraAgendaViewModel> lista = _repository.GetConfiguracaoProjetoByData(ibge, codmed, data);

                return Ok(lista);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet("GetConfigProjetoById/{id}")]
        [ParameterTypeFilter("visualizar")]
        public ActionResult<DiasMed> GetConfigProjetoById([FromHeader] string ibge, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                DiasMed model = _repository.GetConfigProjetoById(ibge, id);
                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        //fornecedor em Fornecedor/GetPrestadoresVigencia
        //cbo em Profissional/GetCBOByMedicoUnidade
        //local de atendimento em Unidade/GetLocaisAtendimentoByUnidade
        //procedimento em Procedimento/GetProcedimentoBycbo

        [HttpPost("Inserir")]
        [ParameterTypeFilter("inserir")]
        public ActionResult Inserir([FromHeader] string ibge, [FromBody]DiasMed model)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));

                if (model.id == null || model.id == 0)
                    model.id = _repository.GetNewIdDiasMed(ibge); // gera id de TSI_DIASMED

                _repository.GravarAgendaDiasMed(ibge, model);//insere em TSI_DIASMED

                TimeSpan horarioini = new TimeSpan(Convert.ToInt32(model.csi_horario.Split(":")[0]), Convert.ToInt32(model.csi_horario.Split(":")[1]), Convert.ToInt32(model.csi_horario.Split(":")[2]));
                TimeSpan horariofim = new TimeSpan(Convert.ToInt32(model.csi_horariofinal.Split(":")[0]), Convert.ToInt32(model.csi_horariofinal.Split(":")[1]), Convert.ToInt32(model.csi_horariofinal.Split(":")[2]));
                _repository.InserirConsultasItens(ibge, horarioini, horariofim, (int)model.csi_qtdecon, (int)model.id, (int)model.csi_intervalo_agendamento);

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
        public ActionResult Editar([FromHeader] string ibge, [FromBody]DiasMed model, [FromRoute]int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                model.id = id;
                //edita primeiro o registro de TSI_DIAS_MED
                _repository.GravarAgendaDiasMed(ibge, model);//insere em TSI_DIASMED

                //exclui registros de TSI_CONSULTAS_ITEM
                _repository.ExcluirConsultasItemByDiasMed(ibge, id);

                //insere novamente registros em TSI_CONSULTAS_ITEM
                TimeSpan horarioini = new TimeSpan(Convert.ToInt32(model.csi_horario.Split(":")[0]), Convert.ToInt32(model.csi_horario.Split(":")[1]), Convert.ToInt32(model.csi_horario.Split(":")[2]));
                TimeSpan horariofim = new TimeSpan(Convert.ToInt32(model.csi_horariofinal.Split(":")[0]), Convert.ToInt32(model.csi_horariofinal.Split(":")[1]), Convert.ToInt32(model.csi_horariofinal.Split(":")[2]));
                _repository.InserirConsultasItens(ibge, horarioini, horariofim, (int)model.csi_qtdecon, (int)model.id, (int)model.csi_intervalo_agendamento);

                return Ok(model);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpDelete("Excluir/{id}")]
        [ParameterTypeFilter("excluir")]
        public ActionResult Excluir([FromHeader] string ibge, [FromRoute] int id) //id_dias_med
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                //verifica se ja existem agendamentos
                var itens = _repository.GetConsultasItensByDiasMed(ibge, id);
                if (itens.Where(x => x.id_consultas != null).ToList().Count() != 0)
                    return BadRequest(TrataErro.GetResponse("Não é possível excluir essa agenda pois já existem pessoas Agendadas! ", true));

                //exclui TSI_CONSULTAS_ITEM
                _repository.ExcluirConsultasItemByDiasMed(ibge, id);

                //exclui TSI_DIAS_MED
                _repository.ExcluiDiasMed(ibge, id);

                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }
        #endregion

        #region Agendamento de Consultas
        [HttpPost("InserirConsulta")]
        [ParameterTypeFilter("inserir")]
        public ActionResult InserirConsulta([FromHeader] string ibge, [FromBody]Consultas model)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                model.csi_controle = _repository.GetNewIdConsulta(ibge);

                //Busca aqui o proximo numero de ordem que deverá ser vinculado
                model.csi_ordem = _repository.GetNextOrdemConsultaItem(ibge, (int)model.id_diasmed);

                _repository.InserirConsulta(ibge, model); //Grava registro de consulta
                return Ok(model);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPut("EditarConsulta/{id}")]
        [ParameterTypeFilter("inserir")]
        public ActionResult EditarConsulta([FromHeader] string ibge, [FromBody]Consultas model, [FromRoute] int id)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                model.csi_controle = id;
                _repository.InserirConsulta(ibge, model);

                return Ok(model);
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPut("CancelarConsulta")]
        [ParameterTypeFilter("editar")]
        public ActionResult CancelarConsulta([FromHeader] string ibge, [FromBody] CancelaAgendaViewModel model)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                var agendamento = _repository.GetConsultaByDiasMedOrdem(ibge, model.id_dias_med, model.ordem);
                if (agendamento?.csi_status == "Cancelada")
                    return BadRequest(TrataErro.GetResponse("Esta Consulta já foi Cancelada.", true));

                var agendaitens = _repository.GetItensByDiasMedOrdem(ibge, model.id_dias_med, model.ordem);

                if (agendaitens.flg_reservado == "T")
                {
                    //atualiza consulta item reservado
                    _repository.CancelaReservaConsultaItem(ibge, model.id_dias_med, model.ordem);
                }
                else
                {
                    if (agendamento != null && !Helper.DisponivelParaCancelar(agendamento.csi_status))
                        return BadRequest(TrataErro.GetResponse($@"Não é possível cancelar uma Consulta com a situação {agendamento.csi_status}", true));

                    if (agendamento != null && agendamento.csi_status == Helper.Consultado)
                        return BadRequest(TrataErro.GetResponse($@"Não é possível cancelar uma Consulta com a situação {agendamento.csi_status}", true));

                    //cancela consulta aqui
                    _repository.CancelarConsulta(ibge, Helper.Cancelada, model.login, DateTime.Now, (int)agendamento.csi_controle, model.obs);

                    if (model.fRG_Saude_Agenda_ID != null && model.fRG_Saude_Agenda_ID != 0)
                        _repository.AtualizaRGSaudeAgenda(ibge, (int)agendamento.csi_controle, (int)model.fRG_Saude_Agenda_ID);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPut("ConfirmarConsulta/{id_controle}")]
        [ParameterTypeFilter("editar")]
        public ActionResult ConfirmarConsulta([FromHeader] string ibge, [FromBody] ConfirmaAgendaViewModel model, [FromRoute] int id_controle)
        {
            try
            {
                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                _repository.ConfirmaAgendaConsulta(ibge, Helper.AConsultar, DateTime.Now, model.nome_usu, id_controle);
                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPut("RemanejarOrdem/{id_ordem_atual:int}")]
        [ParameterTypeFilter("editar")] // ACAO = 1 - DESCER, 2 - SUBIR
        public ActionResult RemanejarOrdem([FromHeader] string ibge, int id_dias_med, int acao, int id_ordem_atual)
        {
            try
            {
                int? origemOrdem = null;
                int? origemIDConsulta = null;
                string origemHorario = string.Empty;
                string destinoHorario = string.Empty;
                int? destinoOrdem = null;
                int? destinoIDConsulta = null;
                bool existeDestino = false;

                ibge = _config.GetConnectionString(Helpers.Connection.GetConnection(ibge));
                var itens = _repository.GetAgendadosByDiasMed(ibge, id_dias_med).FirstOrDefault();

                if (itens.flg_reservado == "T")
                    return BadRequest(TrataErro.GetResponse("Horário reservado não pode ser reordenado.", true));

                origemOrdem = id_ordem_atual;
                origemIDConsulta = itens.csi_controle;
                origemHorario = itens.csi_horario;
                destinoOrdem = id_ordem_atual;

                if (itens.flg_reservado == "F")
                {
                    destinoIDConsulta = itens.csi_controle;
                    destinoHorario = itens.csi_horario;
                    existeDestino = true;
                }

                if (acao == 1)
                    destinoOrdem = destinoOrdem++;
                else
                    destinoOrdem = destinoOrdem--;

                if (existeDestino)
                {
                    if (origemIDConsulta != null && origemIDConsulta != 0)
                        _repository.RemanejaOrdemConsulta(ibge, (int)destinoOrdem, destinoHorario, (int)origemIDConsulta);
                    else if (destinoIDConsulta != null && destinoIDConsulta != 0)
                        _repository.RemanejaOrdemConsulta(ibge, (int)origemOrdem, origemHorario, (int)destinoIDConsulta);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                var response = TrataErro.GetResponse(ex.Message, true);
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }
        #endregion
    }
}