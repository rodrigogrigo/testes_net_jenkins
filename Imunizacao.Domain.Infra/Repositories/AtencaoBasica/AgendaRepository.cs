using Dapper;
using RgCidadao.Domain.Commands.AtencaoBasica;
using RgCidadao.Domain.Entities.AtencaoBasica;
using RgCidadao.Domain.Repositories.AtencaoBasica;
using RgCidadao.Domain.ViewModels.AtencaoBasica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RgCidadao.Domain.Infra.Repositories.AtencaoBasica
{
    public class AgendaRepository : IAgendaRepository
    {
        private IAgendaCommand _command;
        public AgendaRepository(IAgendaCommand command)
        {
            _command = command;
        }

        public List<AgendaHorariosViewModel> GetAll(string ibge, DateTime data, int id_profissional)
        {
            try
            {
                var lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                            conn.Query<AgendaHorariosViewModel>(_command.DiasMedByData, new
                            {
                                @data = data,
                                @id_profissional = id_profissional
                            })).ToList();

                foreach (var item in lista)
                {
                    item.itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                 conn.Query<AgendaConsultaViewModel>(_command.GetAllPagination, new
                                 {
                                     @id_dia_med = item.id_dias_med
                                 })).OrderBy(x => x.csi_ordem).ToList();
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetCountAll(string ibge, int id_dia_med)
        {
            try
            {
                string sql = _command.GetCountAll;
                //if (!string.IsNullOrWhiteSpace(filtro))
                //    sql = sql.Replace("@filtro", filtro);
                //else
                //    sql = sql.Replace("@filtro", string.Empty);

                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                      conn.QueryFirstOrDefault<int>(sql, new
                      {
                          @id_dia_med = id_dia_med
                      }));
                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ExcluiDiasMed(string ibge, int id_dias_med)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                      conn.Execute(_command.ExcluiDiasMed, new
                      {
                          @id_dias_med = id_dias_med
                      }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Configuração de Agenda
        public List<ConfiguraAgendaViewModel> GetConfiguracaoProjetoByData(string ibge, int codmed, DateTime data)
        {
            try
            {
                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                      conn.Query<ConfiguraAgendaViewModel>(_command.GetConfiguracaoProjetoByData, new
                      {
                          @csi_codmed = codmed,
                          @csi_data = data
                      })).ToList();
                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DiasMed GetConfigProjetoById(string ibge, int id)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                      conn.QueryFirstOrDefault<DiasMed>(_command.GetConfigProjetoById, new
                      {
                          @id = id
                      }));
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AgendaDiasViewModel> GetAgendaDias(string ibge, int id_profissional, DateTime? datainicial, DateTime? datafinal)
        {
            try
            {
                var lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Query<AgendaDiasViewModel>(_command.GetAgendaDias, new
                           {
                               @id_profissional = id_profissional,
                               @data_inicial = datainicial,
                               @data_final = datafinal
                           })).ToList();

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //INSERE E EDITA
        public void GravarAgendaDiasMed(string ibge, DiasMed model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Execute(_command.GravarAgendaDiasMed, new
                           {
                               @csi_codmed = model.csi_codmed,
                               @csi_data = model.csi_data,
                               @csi_horario = model.csi_horario,
                               @csi_qtdecon = model.csi_qtdecon,
                               @csi_copdponto = model.csi_copdponto,
                               @csi_procedimento = model.csi_procedimento,
                               @csi_cbo = model.csi_cbo,
                               @csi_forma_agendamento = model.csi_forma_agendamento,
                               @csi_intervalo_agendamento = model.csi_intervalo_agendamento,
                               @csi_forma_faturamento = model.csi_forma_faturamento,
                               @csi_id_prestador = model.csi_id_prestador,
                               @csi_data_criacao = model.csi_data_criacao,
                               @id = model.id,
                               csi_horariofinal = model.csi_horariofinal,
                               @id_grupo_procedimento_cota = model.id_grupo_procedimento_cota,
                               @id_controle_sincronizacao_lote = model.id_controle_sincronizacao_lote,
                               @uuid = model.uuid
                           }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetNewIdDiasMed(string ibge)
        {
            try
            {
                int id = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.QueryFirstOrDefault<int>(_command.GetNewIdDiasMed));

                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InserirConsultasItens(string ibge, TimeSpan horaini, TimeSpan horafim, int qtdeVagas, int id_dias_med, int intervalo)
        {
            try
            {
                int ordem = 1;

                if (intervalo > 0)
                {
                    while (horaini < horafim)
                    {
                        Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.QueryFirstOrDefault<int>(_command.InserirConsultasItens, new
                           {
                               @id_diasmed = id_dias_med,
                               @horario = horaini,
                               @ordem = ordem,
                               @reservado = "F"
                           }));
                        ordem = ordem + 1;
                        TimeSpan min = new TimeSpan(0, intervalo, 0);
                        horaini = horaini.Add(min);
                    }
                }
                else if (qtdeVagas > 0) // quando são adicionadas as vagas o horario de todas as consultas itens serão o horario inicial informado
                {
                    for (int i = 1; i <= qtdeVagas; i++)
                    {
                        Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.QueryFirstOrDefault<int>(_command.InserirConsultasItens, new
                          {
                              @id_diasmed = id_dias_med,
                              @horario = horaini,
                              @ordem = ordem,
                              @reservado = "F"
                          }));
                        ordem = ordem + 1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ExcluirConsultasItemByDiasMed(string ibge, int id_dias_med)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Execute(_command.ExcluirConsultasItemByDiasMed, new { @id_dias_me = id_dias_med }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ConsultasItem> GetConsultasItensByDiasMed(string ibge, int id_dias_med)
        {
            try
            {
                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Query<ConsultasItem>(_command.GetConsultaItemByDiasMed, new { @id_dias_med = id_dias_med }).ToList());

                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ConsultasItem GetItensByDiasMedOrdem(string ibge, int id_diasmed, int ordem)
        {
            try
            {
                ConsultasItem itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                      conn.QueryFirstOrDefault<ConsultasItem>(_command.GetItensByDiasMedOrdem, new
                                      {
                                          @id_diasmed = id_diasmed,
                                          @ordem = ordem
                                      }));
                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CancelaReservaConsultaItem(string ibge, int dias_med, int ordem)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                      conn.Execute(_command.CancelaReservaConsultaItem, new
                                      {
                                          @id_diasmed = dias_med,
                                          @ordem = ordem
                                      }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetNextOrdemConsultaItem(string ibge, int id_diasmed)
        {
            try
            {
                var ordem = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                             conn.QueryFirstOrDefault<int>(_command.AtualizaRGSaudeAgenda, new
                             {
                                 @id_diasmed = id_diasmed
                             }));

                return ordem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Agendamento de Consultas
        public void InserirConsulta(string ibge, Consultas model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.Execute(_command.InserirConsulta, new
                          {
                              @csi_controle = model.csi_controle,
                              @csi_dataag = model.csi_dataag,
                              @csi_datacon = model.csi_datacon,
                              @csi_horario = model.csi_horario,
                              @csi_nomusu = model.csi_nomusu,
                              @csi_codmed = model.csi_codmed,
                              @csi_codponto = model.csi_codponto,
                              @csi_codpac = model.csi_codpac,
                              @csi_ordem = model.csi_ordem,
                              @csi_suplente = model.csi_suplente,
                              @csi_dataconf = model.csi_dataconf,
                              @csi_nomusuconf = model.csi_nomusuconf,
                              @csi_diagnostico = model.csi_diagnostico,
                              @csi_codnatatend = model.csi_codnatatend,
                              @csi_codpacacomp = model.csi_codpacacomp,
                              @csi_nomeacomp = model.csi_nomeacomp,
                              @csi_parentescoacomp = model.csi_parentescoacomp,
                              @csi_datanacacomp = model.csi_datanacacomp,
                              @csi_docideacomp = model.csi_docideacomp,
                              @csi_endtrabacomp = model.csi_endtrabacomp,
                              @csi_codcidacomp = model.csi_codcidacomp,
                              @csi_cepacomp = model.csi_cepacomp,
                              @csi_diagprovavel = model.csi_diagprovavel,
                              @csi_anamnese = model.csi_anamnese,
                              @csi_repatendimento = model.csi_repatendimento,
                              @csi_qtdrepatend = model.csi_qtdrepatend,
                              @csi_dataalta = model.csi_dataalta,
                              @csi_codtipoalta = model.csi_codtipoalta,
                              @csi_codleito = model.csi_codleito,
                              @csi_nprenatal = model.csi_nprenatal,
                              @csi_codestnut = model.csi_codestnut,
                              @csi_status = model.csi_status,
                              @csi_codcon = model.csi_codcon,
                              @csi_alta = model.csi_alta,
                              @csi_obsalta = model.csi_obsalta,
                              @csi_modelo = model.csi_modelo,
                              @csi_modelopublico = model.csi_modelopublico,
                              @csi_nomemodelo = model.csi_nomemodelo,
                              @csi_peso = model.csi_peso,
                              @csi_imc = model.csi_imc,
                              @csi_altura = model.csi_altura,
                              @csi_dietaobs = model.csi_dietaobs,
                              @csi_orientnutri = model.csi_orientnutri,
                              @csi_cbo = model.csi_cbo,
                              @csi_unidade_agendament = model.csi_unidade_agendamento,
                              @csi_historico_evolutivo = model.csi_historico_evolutivo,
                              @csi_id_fila_espera = model.csi_id_fila_espera,
                              @csi_tipo_consulta = model.csi_tipo_consulta,
                              @csi_medida_cintura = model.csi_medida_cintura,
                              @csi_restricao_modelo = model.csi_restricao_modelo,
                              @csi_modelo_restrito_pac = model.csi_modelo_restrito_pac,
                              @csi_hipertencao = model.csi_hipertencao,
                              @csi_diabetes = model.csi_diabetes,
                              @csi_pressao_art_sistolica = model.csi_pressao_art_sistolica,
                              @csi_pressao_art_diastolica = model.csi_pressao_art_diastolica,
                              @csi_glicemia = model.csi_glicemia,
                              @csi_tipo_glicemia = model.csi_tipo_glicemia,
                              @csi_sem_complicacoes = model.csi_sem_complicacoes,
                              @csi_angina = model.csi_angina,
                              @csi_iam = model.csi_iam,
                              @csi_avc = model.csi_avc,
                              @csi_pre_diabetico = model.csi_pre_diabetico,
                              @csi_amputacao_diabete = model.csi_amputacao_diabete,
                              @csi_doenca_renal = model.csi_doenca_renal,
                              @csi_retinopatia = model.csi_retinopatia,
                              @csi_hb_glicosilada = model.csi_hb_glicosilada,
                              @csi_creatinina_serica = model.csi_creatinina_serica,
                              @csi_colesterol_total = model.csi_colesterol_total,
                              @csi_ecg = model.csi_ecg,
                              @csi_triglicerides = model.csi_triglicerides,
                              @csi_urina_tipo1 = model.csi_urina_tipo1,
                              @csi_microalbuminuria = model.csi_microalbuminuria,
                              @csi_qtde_hidroclorotiazida = model.csi_qtde_hidroclorotiazida,
                              @csi_qtde_propranolol = model.csi_qtde_propranolol,
                              @csi_qtde_captopril = model.csi_qtde_captopril,
                              @csi_qtde_glibenclamida = model.csi_qtde_glibenclamida,
                              @csi_qtde_metformina = model.csi_qtde_metformina,
                              @csi_qtde_insulina = model.csi_qtde_insulina,
                              @csi_ant_fam_cardio_vascular = model.csi_ant_fam_cardio_vascular,
                              @csi_diabete_tipo1 = model.csi_diabete_tipo1,
                              @csi_diabete_tipo2 = model.csi_diabete_tipo2,
                              @csi_tabagismo = model.csi_tabagismo,
                              @csi_sedentarismo = model.csi_sedentarismo,
                              @csi_sobrepeso = model.csi_sobrepeso,
                              @csi_medicamentoso = model.csi_medicamentoso,
                              @csi_outros_medicamentos = model.csi_outros_medicamentos,
                              @csi_outras_coronariopatias = model.csi_outras_coronariopatias,
                              @csi_tipo_risco = model.csi_tipo_risco,
                              @csi_status_hiperdia = model.csi_status_hiperdia,
                              @csi_outros_medicamentos_obs = model.csi_outros_medicamentos_obs,
                              @csi_id_libexames = model.csi_id_libexames,
                              @csi_codexa_padrao = model.csi_codexa_padrao,
                              @id_diasmed = model.id_diasmed,
                              @id_atendimento_individual = model.id_atendimento_individual,
                              @id_tipo_atendimento = model.id_tipo_atendimento,
                              @id_atendimento_odontologico = model.id_atendimento_odontologico,
                              @id_cid = model.id_cid,
                              @id_orgao_publico = model.id_orgao_publico,
                              @csi_data_cancelou = model.csi_data_cancelou,
                              @csi_nomusu_cancelou = model.csi_nomusu_cancelou,
                              @csi_obs = model.csi_obs,
                              @csi_unidade_paciente = model.csi_unidade_paciente,
                              @uuid = model.uuid,
                              @id_tipo_consulta = model.id_tipo_consulta,
                              @id_senha = model.id_senha,
                              @id_pep_exame_fisico = model.id_pep_exame_fisico,
                              @sms_enviado = model.sms_enviado,
                              @csi_obs_cancelamento = model.csi_obs_cancelamento,
                              @id_controle_sincronizacao_lote = model.id_controle_sincronizacao_lote,
                              @id_equipe = model.id_equipe
                          }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetNewIdConsulta(string ibge)
        {
            try
            {
                int id = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                            conn.QueryFirstOrDefault<int>(_command.GetNewIdConsulta));
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Consultas GetConsultaByDiasMedOrdem(string ibge, int id_dias_med, int ordem)
        {
            try
            {
                Consultas itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                        conn.QueryFirstOrDefault<Consultas>(_command.GetConsultaByDiasMedOrdem, new
                                        {
                                            @id_diasmed = id_dias_med,
                                            @ordem = ordem
                                        }));
                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CancelarConsulta(string ibge, string status, string usuario, DateTime data, int id_controle, string obs)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                conn.Execute(_command.CancelarConsulta, new
                {
                    @status = status,
                    @user_cancelou = usuario,
                    @data_cancelou = data,
                    @obs_cancelou = obs,
                    @id_controle = id_controle
                }));
            }
            catch (Exception ex)
            {
                throw new Exception($@"Erro ao Cancelar Consulta: {ex.Message}");
            }
        }

        public void ConfirmaAgendaConsulta(string ibge, string status, DateTime data, string nome_usu, int id_controle)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
               conn.Execute(_command.ConfirmaAgendaConsulta, new
               {
                   @status = status,
                   @data_conf = data,
                   @nome_usu = nome_usu,
                   @id_controle = id_controle
               }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AgendaDiasViewModel> GetAgendadosByDiasMed(string ibge, int id_diasmed)
        {
            try
            {
                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                            conn.Query<AgendaDiasViewModel>(_command.GetAgendadosByDiasMed, new
                            {
                                @id_dias_med = id_diasmed
                            }).ToList());

                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RemanejaOrdemConsulta(string ibge, int ordem, string horario, int csi_controle)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Execute(_command.RemanejaOrdemConsulta, new
                           {
                               @ordem = ordem,
                               @csi_horario = horario,
                               @csi_controle = csi_controle
                           }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region RG Saude Agenda
        public void AtualizaRGSaudeAgenda(string ibge, int id_consulta, int fRG_Saude_Agenda_ID)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                conn.Execute(_command.AtualizaRGSaudeAgenda, new
                {
                    @id_horario_agenda = id_consulta,
                    @fRG_Saude_Agenda_ID = fRG_Saude_Agenda_ID
                }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
