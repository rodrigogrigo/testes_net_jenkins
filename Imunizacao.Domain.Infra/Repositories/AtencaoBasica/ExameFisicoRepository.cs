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
    public class ExameFisicoRepository : IExameFisicoRepository
    {
        private IExameFisicoCommand _command;
        public ExameFisicoRepository(IExameFisicoCommand command)
        {
            _command = command;
        }

        public int GetNewCodigoExameFisico(string ibge)
        {
            try
            {
                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                    conn.QueryFirstOrDefault<int>(_command.GetNewCodigoExameFisico));
                return itens;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int GetNewCodigoControle(string ibge)
        {
            try
            {
                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                    conn.QueryFirstOrDefault<int>(_command.GetNewCodigoControle));
                return itens;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Insert(string ibge, ExameFisico model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                     conn.Execute(_command.Insert, new
                                     {
                                         @codigo_exame_fisico = model.codigo_exame_fisico,
                                         @codigo_atendimento = model.codigo_atendimento,
                                         @data_exame_fisico = model.data_exame_fisico,
                                         @valor_saturacao = model.valor_saturacao,
                                         @valor_cefalico = model.valor_cefalico,
                                         @valor_circ_abdominal = model.valor_circ_abdominal,
                                         @valor_pa1 = model.valor_pa1,
                                         @valor_pa2 = model.valor_pa2,
                                         @valor_freq_cardiaca = model.valor_freq_cardiaca,
                                         @valor_freq_respiratoria = model.valor_freq_respiratoria,
                                         @valor_temperatura = model.valor_temperatura,
                                         @valor_glicemia = model.valor_glicemia,
                                         @observacao = model.observacao,
                                         @codigo_profissional = model.codigo_profissional,
                                         @origem_dados = model.origem_dados,
                                         @codigo_triagem = model.codigo_triagem,
                                         @valor_peso = model.valor_peso,
                                         @valor_altura = model.valor_altura,
                                         @valor_imc = model.valor_imc,
                                         @id_paciente = model.id_paciente,
                                         @codigo_consulta = model.codigo_consulta,
                                         @momento_coleta = model.momento_coleta,
                                         @origem = model.origem,
                                         @glasgow = model.glasgow,
                                         @regua_dor = model.regua_dor,
                                         @id_classificacao_triagem = model.id_classificacao_triagem,
                                         @id_profissional_clas_triagem = model.id_profissional_clas_triagem,
                                         @id_grupo_triagem = model.id_grupo_triagem,
                                         @id_subgrupo_triagem = model.id_subgrupo_triagem,
                                         @descricao_sintoma_triagem = model.descricao_sintoma_triagem,
                                         @valor_circ_toracica = model.valor_circ_toracica,
                                     }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(string ibge, ExameFisico model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                     conn.Execute(_command.Update, new
                                     {
                                         @codigo_exame_fisico = model.codigo_exame_fisico,
                                         @codigo_atendimento = model.codigo_atendimento,
                                         @data_exame_fisico = model.data_exame_fisico,
                                         @valor_saturacao = model.valor_saturacao,
                                         @valor_cefalico = model.valor_cefalico,
                                         @valor_circ_abdominal = model.valor_circ_abdominal,
                                         @valor_circ_toracica = model.valor_circ_toracica,
                                         @valor_pa1 = model.valor_pa1,
                                         @valor_pa2 = model.valor_pa2,
                                         @valor_freq_cardiaca = model.valor_freq_cardiaca,
                                         @valor_freq_respiratoria = model.valor_freq_respiratoria,
                                         @valor_temperatura = model.valor_temperatura,
                                         @valor_glicemia = model.valor_glicemia,
                                         @observacao = model.observacao,
                                         @codigo_profissional = model.codigo_profissional,
                                         @origem_dados = model.origem_dados,
                                         @codigo_triagem = model.codigo_triagem,
                                         @valor_peso = model.valor_peso,
                                         @valor_altura = model.valor_altura,
                                         @valor_imc = model.valor_imc,
                                         @id_paciente = model.id_paciente,
                                         @codigo_consulta = model.codigo_consulta,
                                         @momento_coleta = model.momento_coleta,
                                         @origem = model.origem,
                                         @glasgow = model.glasgow,
                                         @regua_dor = model.regua_dor,
                                         @id_classificacao_triagem = model.id_classificacao_triagem,
                                         @id_profissional_clas_triagem = model.id_profissional_clas_triagem,
                                         @id_grupo_triagem = model.id_grupo_triagem,
                                         @id_subgrupo_triagem = model.id_subgrupo_triagem,
                                         @descricao_sintoma_triagem = model.descricao_sintoma_triagem,
                                     }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertUpdateProcenfermagem(string ibge, ExameFisico model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                    conn.Execute(_command.InsertUpdateProcenfermagem, new
                                    {
                                        @csi_controle = model.csi_controle, //pk autoincremento
                                        @csi_data = model.data_exame_fisico, //data retorativa
                                        @csi_codmed = model.csi_codmed, // codigo profissional
                                        @csi_nomusu = model.csi_nomusu, //nome usuario logado
                                        @csi_datainc = model.data_exame_fisico, // data de envio
                                        @csi_cbo = model.csi_cbo, //CBO  do profissional
                                        @csi_coduni = model.csi_coduni,//Unidade Logada
                                        @id_pep_exame_fisico = model.codigo_exame_fisico, //PK da PEP_EXAME_FISICO
                                        @csi_local_atendimento = model.csi_local_atendimento,
                                        @turno = model.turno,// com base na data e hora
                                        @csi_codpac = model.csi_codpac// codigo do paciente
                                    }));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertUpdateIProcenfermagem(string ibge, ExameFisico model)
        {
            try
            {
                foreach (var iprocenfermagem in model.iprocenfermagem)
                {
                    Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                     conn.Execute(_command.InsertUpdateIProcenfermagem, new
                                     {
                                         @csi_controle = model.csi_controle,//pk do procenfermagem
                                         @csi_codproc = iprocenfermagem.csi_codproc,// codigo do procediemento
                                         @csi_qtde = 1, // 1
                                         @csi_idade = model.csi_idade, // idade do paciente
                                     }));
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ExameFisicoViewModel> GetAllPagination(string ibge, int? page, int? pagesize, string filtro)
        {
            try
            {
                string sql = string.Empty;
                if (!string.IsNullOrWhiteSpace(filtro))
                    sql = _command.GetAllPagination.Replace("@filtro", filtro);
                else
                    sql = _command.GetAllPagination.Replace("@filtro", string.Empty);

                var listaaltura = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Query<ExameFisicoViewModel>(sql, new
                           {
                               @pagesize = pagesize,
                               @page = page
                           }).ToList());

                return listaaltura;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetCountAll(string ibge, string filtro)
        {
            try
            {
                string sql = string.Empty;
                if (!string.IsNullOrWhiteSpace(filtro))
                    sql = _command.GetCountAll.Replace("@filtro", filtro);
                else
                    sql = _command.GetCountAll.Replace("@filtro", string.Empty);

                var listaaltura = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.QueryFirstOrDefault<int>(sql));

                return listaaltura;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ExameFisico GetExameFisicoById(string ibge, int id)
        {
            try
            {
                var exameFisico = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                           conn.QueryFirstOrDefault<ExameFisico>(_command.GetExameFisicoById, new { @id = id }));
                return exameFisico;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<IProcenfermagem> GetIProcenfermagemById(string ibge, int csi_controle)
        {
            try
            {
                var listaiprocenfermagem = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Query<IProcenfermagem>(_command.GetIprocenfermagemById, new { @csi_controle = csi_controle }).ToList());

                return listaiprocenfermagem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HistoricoExameFisicoPac> GetHistoricoAltura(string ibge, int idpaciente)
        {
            try
            {
                var listaaltura = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Query<HistoricoExameFisicoPac>(_command.GetListaAlturaByPaciente, new { @id_paciente = idpaciente }).ToList());

                return listaaltura;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HistoricoExameFisicoPac> GetHistoricoCircunferenciaAbdominal(string ibge, int idpaciente)
        {
            try
            {
                var listaaltura = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Query<HistoricoExameFisicoPac>(_command.GetListaCircunferenciaAbdominalByPaciente, new { @id_paciente = idpaciente }).ToList());

                return listaaltura;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HistoricoExameFisicoPac> GetHistoricoCircunferenciaToracica(string ibge, int idpaciente)
        {
            try
            {
                var listaaltura = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Query<HistoricoExameFisicoPac>(_command.GetListaCircunferenciaToracicaByPaciente, new { @id_paciente = idpaciente }).ToList());

                return listaaltura;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HistoricoExameFisicoPac> GetHistoricoExameFisicoPac(string ibge, int idpaciente)
        {
            try
            {
                var ultimopeso = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                            conn.QueryFirstOrDefault<dynamic>(_command.GetLastPesoByPaciente, new { @id_paciente = idpaciente }));

                var ultimaaltura = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.QueryFirstOrDefault<dynamic>(_command.GetLastAlturaPaciente, new { @id_paciente = idpaciente }));

                var ultimoimc = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.QueryFirstOrDefault<dynamic>(_command.GetLastImcPaciente, new { @id_paciente = idpaciente }));

                var ultimoTemperaturaPaciente = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                  conn.QueryFirstOrDefault<dynamic>(_command.GetLastTemperaturaPaciente, new { @id_paciente = idpaciente }));

                var ultimoCircunferenciaAbdominal = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                               conn.QueryFirstOrDefault<dynamic>(_command.GetLastCircunferenciaAbdominalPaciente, new { @id_paciente = idpaciente }));

                var ultimoCircunferenciaToracica = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                             conn.QueryFirstOrDefault<dynamic>(_command.GetLastCircunferenciaToracicaPaciente, new { @id_paciente = idpaciente }));

                var ultimoPressaoArterial = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.QueryFirstOrDefault<dynamic>(_command.GetLastPressaoArterialPaciente, new { @id_paciente = idpaciente }));

                //var ultimoPressaoArterialSistolica = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                //           conn.QueryFirstOrDefault<dynamic>(_command.GetLastPressaoArterialSistolicaPaciente, new { @id_paciente = idpaciente }));

                //var ultimoPressaoArterialDiastolica = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                //           conn.QueryFirstOrDefault<dynamic>(_command.GetLastPressaoArterialDiastolicaPaciente, new { @id_paciente = idpaciente }));

                var ultimoGlicemia = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.QueryFirstOrDefault<dynamic>(_command.GetLastGlicemiaPaciente, new { @id_paciente = idpaciente }));

                var ultimoSaturacaoPaciente = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.QueryFirstOrDefault<dynamic>(_command.GetLastSaturacaoPaciente, new { @id_paciente = idpaciente }));

                var ultimoFrequenciaCardiaca = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                       conn.QueryFirstOrDefault<dynamic>(_command.GetLastFrequenciaCardiacaPaciente, new { @id_paciente = idpaciente }));

                var ultimoFrequenciaRespiratoria = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                      conn.QueryFirstOrDefault<dynamic>(_command.GetLastFrequenciaRespiratoriaPaciente, new { @id_paciente = idpaciente }));

                var ultimoFrequenciaCefalico = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                   conn.QueryFirstOrDefault<dynamic>(_command.GetLastFrequenciaCefalicoPaciente, new { @id_paciente = idpaciente }));

                var ultimoGlassGow = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                 conn.QueryFirstOrDefault<dynamic>(_command.GetLastGlassGowPaciente, new { @id_paciente = idpaciente }));

                var ultimoReguaDor = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                 conn.QueryFirstOrDefault<dynamic>(_command.GetLastReguaDorPaciente, new { @id_paciente = idpaciente }));

                var lista = new List<HistoricoExameFisicoPac>();

                DateTime? datanull = Convert.ToDateTime("01/01/0001 00:00:00");

                var itemPressaoArterial = new HistoricoExameFisicoPac
                {
                    descricao = "pressão arterial",
                    data = ultimoPressaoArterial?.DATA ?? datanull,
                    pressaoarterial = ultimoPressaoArterial?.VALOR ?? "0"
                };
                lista.Add(itemPressaoArterial);

                //var itemPressaoArterialSistolica = new HistoricoExameFisicoPac
                //{
                //    descricao = "pressão arterial sitolica",
                //    data = ultimoPressaoArterialSistolica?.DATA ?? datanull,
                //    pressaoArterialSistolica = ultimoPressaoArterialSistolica?.VALOR ?? 0
                //};
                //lista.Add(itemPressaoArterialSistolica);

                //var itemPressaoArterialDiastolica = new HistoricoExameFisicoPac
                //{
                //    descricao = "pressão arterial diastolica",
                //    data = ultimoPressaoArterialDiastolica?.DATA ?? datanull,
                //    pressaoArterialDiastolica = ultimoPressaoArterialDiastolica?.VALOR ?? 0
                //};
                //lista.Add(itemPressaoArterialDiastolica);

                var itempeso = new HistoricoExameFisicoPac
                {
                    descricao = "peso",
                    data = ultimopeso?.DATA_PESO ?? datanull,
                    valor = ultimopeso?.PESO ?? 0
                };
                lista.Add(itempeso);

                var itemaltura = new HistoricoExameFisicoPac
                {
                    descricao = "altura",
                    data = ultimaaltura?.DATA_ALTURA ?? datanull,
                    valor = ultimaaltura?.ALTURA ?? 0
                };
                lista.Add(itemaltura);

                var itemimc = new HistoricoExameFisicoPac
                {
                    descricao = "imc",
                    data = ultimoimc?.DATA_IMC ?? datanull,
                    valor = ultimoimc?.IMC ?? 0
                };
                lista.Add(itemimc);

                var itemtemperatura = new HistoricoExameFisicoPac
                {
                    descricao = "Temperatura",
                    data = ultimoTemperaturaPaciente?.DATA ?? datanull,
                    valor = ultimoTemperaturaPaciente?.VALOR ?? 0
                };
                lista.Add(itemtemperatura);

                var itemGlicemia = new HistoricoExameFisicoPac
                {
                    descricao = "glicemia",
                    data = ultimoGlicemia?.DATA ?? datanull,
                    valor = ultimoGlicemia?.VALOR ?? 0
                };
                lista.Add(itemGlicemia);

                var itemSaturacao = new HistoricoExameFisicoPac
                {
                    descricao = "saturação",
                    data = ultimoSaturacaoPaciente?.DATA ?? datanull,
                    valor = ultimoSaturacaoPaciente?.VALOR ?? 0
                };
                lista.Add(itemSaturacao);

                var itemCircunferenciaAbdominal = new HistoricoExameFisicoPac
                {
                    descricao = "circunferência abdominal",
                    data = ultimoCircunferenciaAbdominal?.DATA ?? datanull,
                    valor = ultimoCircunferenciaAbdominal?.VALOR ?? 0
                };
                lista.Add(itemCircunferenciaAbdominal);

                var itemCircunferenciaToracica = new HistoricoExameFisicoPac
                {
                    descricao = "circunferência torácica",
                    data = ultimoCircunferenciaToracica?.DATA ?? datanull,
                    valor = ultimoCircunferenciaToracica?.VALOR ?? 0
                };
                lista.Add(itemCircunferenciaToracica);

                var itemFrequenciaCardiaca = new HistoricoExameFisicoPac
                {
                    descricao = "frequência cardiaca",
                    data = ultimoFrequenciaCardiaca?.DATA ?? datanull,
                    valor = ultimoFrequenciaCardiaca?.VALOR ?? 0
                };
                lista.Add(itemFrequenciaCardiaca);

                var itemFrequenciaRespiratoria = new HistoricoExameFisicoPac
                {
                    descricao = "frequência respiratória",
                    data = ultimoFrequenciaRespiratoria?.DATA ?? datanull,
                    valor = ultimoFrequenciaRespiratoria?.VALOR ?? 0
                };
                lista.Add(itemFrequenciaRespiratoria);

                var itemFrequenciaCefalico = new HistoricoExameFisicoPac
                {
                    descricao = "Perímetro cefálico",
                    data = ultimoFrequenciaCefalico?.DATA ?? datanull,
                    valor = ultimoFrequenciaCefalico?.VALOR ?? 0
                };
                lista.Add(itemFrequenciaCefalico);

                var itemGlassGow = new HistoricoExameFisicoPac
                {
                    descricao = "glass gow",
                    data = ultimoGlassGow?.DATA ?? datanull,
                    valor = ultimoGlassGow?.VALOR ?? 0
                };
                lista.Add(itemGlassGow);

                var itemReguaDor = new HistoricoExameFisicoPac
                {
                    descricao = "Regua de dor",
                    data = ultimoReguaDor?.DATA ?? datanull,
                    valor = ultimoReguaDor?.VALOR ?? 0
                };
                lista.Add(itemReguaDor);

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HistoricoExameFisicoPac> GetHistoricoFrequenciaCardiaca(string ibge, int idpaciente)
        {
            try
            {
                var listaaltura = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Query<HistoricoExameFisicoPac>(_command.GetListaFrequenciaCardiacaByPaciente, new { @id_paciente = idpaciente }).ToList());

                return listaaltura;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HistoricoExameFisicoPac> GetHistoricoFrequenciaCefalico(string ibge, int idpaciente)
        {
            try
            {
                var listaaltura = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Query<HistoricoExameFisicoPac>(_command.GetListaFrequenciaCefalicoByPaciente, new { @id_paciente = idpaciente }).ToList());

                return listaaltura;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HistoricoExameFisicoPac> GetHistoricoFrequenciaRespiratoria(string ibge, int idpaciente)
        {
            try
            {
                var listaaltura = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Query<HistoricoExameFisicoPac>(_command.GetListaFrequenciaRespiratoriaByPaciente, new { @id_paciente = idpaciente }).ToList());

                return listaaltura;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HistoricoExameFisicoPac> GetHistoricoGlassGow(string ibge, int idpaciente)
        {
            try
            {
                var listaaltura = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Query<HistoricoExameFisicoPac>(_command.GetListaGlassGowByPaciente, new { @id_paciente = idpaciente }).ToList());

                return listaaltura;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HistoricoExameFisicoPac> GetHistoricoGlicemia(string ibge, int idpaciente)
        {
            try
            {
                var listaaltura = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Query<HistoricoExameFisicoPac>(_command.GetListaGlicemiaByPaciente, new { @id_paciente = idpaciente }).ToList());

                return listaaltura;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HistoricoExameFisicoPac> GetHistoricoIMC(string ibge, int idpaciente)
        {
            try
            {
                var listaaltura = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Query<HistoricoExameFisicoPac>(_command.GetListaIMCByPaciente, new { @id_paciente = idpaciente }).ToList());

                return listaaltura;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HistoricoObservacaoViewModel> GetHistoricoObservacaoByPaciente(string ibge, int paciente)
        {
            try
            {
                try
                {
                    var lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                               conn.Query<HistoricoObservacaoViewModel>(_command.GetHistoricoObservacaoByPaciente, new { @id_paciente = paciente }).ToList());

                    return lista;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HistoricoExameFisicoPac> GetHistoricoPeso(string ibge, int idpaciente)
        {
            try
            {
                var listapeso = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                 conn.Query<HistoricoExameFisicoPac>(_command.GetListaPesoByPaciente, new { @id_paciente = idpaciente }).ToList());

                return listapeso;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HistoricoExameFisicoPac> GetHistoricoPressaoArterial(string ibge, int idpaciente)
        {
            try
            {
                var listaPressaoArterial = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Query<HistoricoExameFisicoPac>(_command.GetListaPressaoArterialByPaciente, new { @id_paciente = idpaciente }).ToList());

                return listaPressaoArterial;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HistoricoExameFisicoPac> GetHistoricoPressaoArterialSistolica(string ibge, int idpaciente)
        {
            try
            {
                var listaPressaoArterialSistolica = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Query<HistoricoExameFisicoPac>(_command.GetLastPressaoArterialSistolicaPaciente, new { @id_paciente = idpaciente }).ToList());

                return listaPressaoArterialSistolica;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HistoricoExameFisicoPac> GetHistoricoPressaoArterialDiastolica(string ibge, int idpaciente)
        {
            try
            {
                var listaPressaoArterialDiastolica = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Query<HistoricoExameFisicoPac>(_command.GetLastPressaoArterialDiastolicaPaciente, new { @id_paciente = idpaciente }).ToList());

                return listaPressaoArterialDiastolica;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HistoricoExameFisicoPac> GetHistoricoProcedimentosGerados(string ibge, int idpaciente)
        {
            try
            {
                var listaaltura = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Query<HistoricoExameFisicoPac>(_command.GetProcedimentosGeradosByPaciente, new { @id_paciente = idpaciente }).ToList());

                return listaaltura;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HistoricoExameFisicoPac> GetHistoricoReguaDor(string ibge, int idpaciente)
        {
            try
            {
                var listaaltura = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Query<HistoricoExameFisicoPac>(_command.GetListaReguaDorByPaciente, new { @id_paciente = idpaciente }).ToList());

                return listaaltura;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HistoricoExameFisicoPac> GetHistoricoSaturacao(string ibge, int idpaciente)
        {
            try
            {
                var listaaltura = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Query<HistoricoExameFisicoPac>(_command.GetListaSaturacaoByPaciente, new { @id_paciente = idpaciente }).ToList());

                return listaaltura;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HistoricoExameFisicoPac> GetHistoricoTemperatura(string ibge, int idpaciente)
        {
            try
            {
                var listaaltura = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Query<HistoricoExameFisicoPac>(_command.GetListaTemperaturaByPaciente, new { @id_paciente = idpaciente }).ToList());

                return listaaltura;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public HistoricoExameFisicoPac GetHistoricoExameFisicoPac(string ibge, int idpaciente)
        //{
        //    try
        //    {
        //        var listapeso = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
        //                   conn.Query<HistoricoPesoPac>(_command.GetListaPesoByPaciente, new { @id_paciente = idpaciente }).ToList());

        //        var listaaltura = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
        //                   conn.Query<HistoricoAlturaPac>(_command.GetListaAlturaByPaciente, new { @id_paciente = idpaciente }).ToList());

        //        var lista = new HistoricoExameFisicoPac
        //        {
        //            peso = listapeso,
        //            altura = listaaltura
        //        };
        //        return lista;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public UltimoExameFisicoPac GetLastExameFisicoPac(string ibge, int id_paciente)
        {
            try
            {
                var ultimopeso = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.QueryFirstOrDefault<dynamic>(_command.GetLastPesoByPaciente, new { @id_paciente = id_paciente }));

                var ultimaaltura = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.QueryFirstOrDefault<dynamic>(_command.GetLastAlturaPaciente, new { @id_paciente = id_paciente }));

                var model = new UltimoExameFisicoPac();
                model.data_altura = ultimaaltura?.DATA_ALTURA;
                model.altura = ultimaaltura?.ALTURA;
                model.data_peso = ultimopeso?.DATA_PESO;
                model.peso = ultimopeso?.PESO;

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
