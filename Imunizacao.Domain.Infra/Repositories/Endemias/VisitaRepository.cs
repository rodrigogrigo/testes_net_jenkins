using Dapper;
using RgCidadao.Domain.Commands.Endemias;
using RgCidadao.Domain.Entities.Endemias;
using RgCidadao.Domain.Repositories.Endemias;
using RgCidadao.Domain.ViewModels.Endemias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RgCidadao.Domain.Infra.Repositories.Endemias
{
    public class VisitaRepository : IVisitaRepository
    {
        public IVisitaCommand _command;
        public IResultadoAmostraCommand _resultcommand;
        public VisitaRepository(IVisitaCommand command, IResultadoAmostraCommand resultcommand)
        {
            _command = command;
            _resultcommand = resultcommand;
        }

        public void DeleteColeta(string ibge, int? coleta)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                     conn.Execute(_resultcommand.DeleteColetaResultadoByColeta, new { @id_coleta = coleta }));

                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                      conn.Execute(_command.DeleteColeta, new { @id = coleta }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteVisita(string ibge, int? visita)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                      conn.Execute(_command.DeleteVisita, new { @id = visita }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Visita> GetAllPagination(string ibge, int page, int pagesize, string filtro)
        {
            try
            {
                string sql = _command.GetAllPagination;
                if (!string.IsNullOrWhiteSpace(filtro))
                    sql = sql.Replace("@filtro", filtro);
                else
                    sql = sql.Replace("@filtro", string.Empty);

                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                      conn.Query<Visita>(sql, new
                      {
                          @pagesize = pagesize,
                          @page = page
                      })).ToList();
                return itens.OrderByDescending(x => x.id_visita_imovel).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Coleta> GetColetaByVisita(string ibge, int visita)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                       conn.Query<Coleta>(_command.GetColetaByVisita, new { @id_visita = visita })).ToList();
                return item;
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
                string sql = _command.GetCountAll;
                if (!string.IsNullOrWhiteSpace(filtro))
                    sql = sql.Replace("@filtro", filtro);
                else
                    sql = sql.Replace("@filtro", string.Empty);

                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                      conn.QueryFirstOrDefault<int>(sql));
                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetNewIdColeta(string ibge)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                       conn.QueryFirstOrDefault<int>(_command.GetNewIdColeta));
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetNewIdVisita(string ibge)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                      conn.QueryFirstOrDefault<int>(_command.GetNewIdVisita));
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<VisitaBairroViewModel> GetQuarteiraoEstabelecimentoByBairro(string ibge, int bairro, int id_ciclo, string filtro)
        {
            try
            {
                string sql = _command.GetQuarteiraoEstabelecimentoByBairro;
                if (!string.IsNullOrWhiteSpace(filtro))
                    sql = sql.Replace("@filtro", filtro);
                else
                    sql = sql.Replace("@filtro", string.Empty);

                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                     conn.Query<dynamic>(sql, new { @id_bairro = bairro }).ToList());

                var listaquarteirao = itens.Select(x => new
                {
                    quarteirao = x.QUARTEIRAO,
                    sequencia_logradouro = x.SEQUENCIA_QUARTEIRAO
                }).Distinct().OrderBy(x => x.quarteirao).ThenBy(x => x.sequencia_logradouro).ToList();

                var lista = new List<VisitaBairroViewModel>();
                foreach (var item in listaquarteirao)
                {
                    var model = new VisitaBairroViewModel();
                    model.quarteirao_logradouro = item.quarteirao;
                    model.quarteirao = item.quarteirao;
                    model.sequencia_quarteirao = item.sequencia_logradouro;

                    var listaimoveis = itens.Where(x => x.QUARTEIRAO == item.quarteirao &&
                                                        x.SEQUENCIA_QUARTEIRAO == item.sequencia_logradouro)
                                            .Select(x => new VisitaEstabelecimentoViewModel()
                                            {
                                                id = x.ID_IMOVEL,
                                                identificacao_imovel = x.IDENTIFICACAO_IMOVEL,
                                                tipo_imovel = x.TIPO_IMOVEL,
                                                numero_logradouro = x.NUMERO_LOGRADOURO,
                                                sequencia_numero = x.SEQUENCIA_NUMERO,
                                                logradouro = x.LOGRADOURO,
                                                bairro = x.BAIRRO,
                                                complemento_logradouro = x.COMPLEMENTO_LOGRADOURO
                                            }).OrderBy(x => x.numero_logradouro).ToList();

                    foreach (var itemimovel in listaimoveis)
                    {
                        var itensvisitas = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                        conn.QueryFirstOrDefault<dynamic>(_command.GetUltimaVisitaCicloByEstabelecimento, new
                                        {
                                            @id_estabelecimento = itemimovel.id,
                                            @id_ciclo = id_ciclo
                                        }));
                        if (itensvisitas != null)
                        {
                            itemimovel.ciclo = itensvisitas.CICLO;
                            itemimovel.data_ultima_visita = itensvisitas.DATA_HORA_ENTRADA;
                            itemimovel.tipo_visita = itensvisitas.TIPO_VISITA;
                            itemimovel.desfecho_visita = itensvisitas.DESFECHO;
                            itemimovel.agente_visita = itensvisitas.AGENTE;

                            List<Coleta> coletasbyvisita = GetColetaByVisita(ibge, itensvisitas.ID);
                            itemimovel.possuiColeta = coletasbyvisita.Count() > 0 ? "T" : "F";
                        }
                    }

                    model.imoveis.AddRange(listaimoveis);
                    lista.Add(model);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Visita GetVisitaByEstabelecimento(string ibge, DateTime? datainicial, DateTime? datafinal, int? estabelecimento, int? id_ciclo)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                      conn.QueryFirstOrDefault<Visita>(_command.GetUltimaVisitaCicloByEstabelecimento, new
                      {
                          //@data_inicial = datainicial,
                          //@datafinal = datafinal,
                          @id_estabelecimento = estabelecimento,
                          @id_ciclo = id_ciclo
                      }));
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Visita GetVisitaById(string ibge, int? id)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                       conn.QueryFirstOrDefault<Visita>(_command.GetVisitaById, new { @id = id }));
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertColeta(string ibge, Coleta model)
        {
            try
            {
                if (model.qtde != null && model.qtde != 0)
                    model.qtde_larvas = model.qtde;

                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                      conn.Execute(_command.InsertColeta, new
                      {
                          @id = model.id,
                          @uuid_registro_mobile = model.uuid_registro_mobile,
                          @id_visita = model.id_visita,
                          @deposito = model.deposito,
                          @amostra = model.amostra,
                          @id_profissional = model.id_profissional,
                          @qtde_larvas = model.qtde_larvas
                      }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertVisita(string ibge, Visita model)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                      conn.Execute(_command.InsertVisita, new
                      {
                          @id = model.id,
                          @uuid_registro_mobile = model.uuid_registro_mobile,
                          @id_imovel = model.id_imovel,
                          @data_hora_entrada = model.data_hora_entrada,
                          @data_hora_saida = model.data_hora_saida,
                          @data_hora_registro = model.data_hora_registro,
                          @competencia = model.competencia,
                          @desfecho = model.desfecho,
                          atividade = model.atividade,
                          @tipo_visita = model.tipo_visita,
                          @encontrou_foco = model.encontrou_foco,
                          @deposito_inspecionado_a1 = model.deposito_inspecionado_a1,
                          @deposito_inspecionado_a2 = model.deposito_inspecionado_a2,
                          @deposito_inspecionado_b = model.deposito_inspecionado_b,
                          @deposito_inspecionado_c = model.deposito_inspecionado_c,
                          @deposito_inspecionado_d1 = model.deposito_inspecionado_d1,
                          @deposito_inspecionado_d2 = model.deposito_inspecionado_d2,
                          @deposito_inspecionado_e = model.deposito_inspecionado_e,
                          @deposito_eliminado = model.deposito_eliminado,
                          @pendencia_descricao = model.pendencia_descricao,
                          @trabalho_educativo = model.trabalho_educativo,
                          @trabalho_mecanico = model.trabalho_mecanico,
                          @trabalho_quimico = model.trabalho_quimico,
                          @trat_focal_larvi1_tipo = model.trat_focal_larvi1_tipo,
                          @trat_focal_larvi1_qtd_gramas = model.trat_focal_larvi1_qtd_gramas,
                          @trat_focal_larvi1_qtd_dep_trat = model.trat_focal_larvi1_qtd_dep_trat,
                          //@trat_focal_larvi2_tipo = model.trat_focal_larvi2_tipo,
                          //@trat_focal_larvi2_qtd_gramas = model.trat_focal_larvi2_qtd_gramas,
                          //@trat_focal_larvi2_qtd_dep_trat = model.trat_focal_larvi2_qtd_dep_trat,
                          @trat_perifocal_adult_tipo = model.trat_perifocal_adult_tipo,
                          @trat_perifocal_adult_qtd_carga = model.trat_perifocal_adult_qtd_carga,
                          @latitude_cadastro = model.latitude_cadastro,
                          @longitude_cadastro = model.longitude_cadastro,
                          @latitude_foco = model.latitude_foco,
                          @longitude_foco = model.longitude_foco,
                          @id_profissional = model.id_profissional,
                          @data_alteracao_serv = model.data_alteracao_serv,
                          @numero_tubito = model.numero_tubito,
                          @id_esus_exportacao_item = model.id_esus_exportacao_item,
                          @id_estabelecimento = model.id_estabelecimento,
                          @id_ciclo = model.id_ciclo
                      }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateColeta(string ibge, Coleta model)
        {
            try
            {
                if (model.qtde != null && model.qtde != 0)
                    model.qtde_larvas = model.qtde;

                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                      conn.Execute(_command.UpdateColeta, new
                      {
                          @uuid_registro_mobile = model.uuid_registro_mobile,
                          @id_visita = model.id_visita,
                          @deposito = model.deposito,
                          @amostra = model.amostra,
                          @id_profissional = model.id_profissional,
                          @qtde_larvas = model.qtde_larvas,
                          @id = model.id,
                      }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateVisita(string ibge, Visita model)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                      conn.Execute(_command.UpdateVisita, new
                      {
                          @id = model.id,
                          @uuid_registro_mobile = model.uuid_registro_mobile,
                          @id_imovel = model.id_imovel,
                          @data_hora_entrada = model.data_hora_entrada,
                          @data_hora_saida = model.data_hora_saida,
                          @data_hora_registro = model.data_hora_registro,
                          @competencia = model.competencia,
                          @desfecho = model.desfecho,
                          atividade = model.atividade,
                          @tipo_visita = model.tipo_visita,
                          @encontrou_foco = model.encontrou_foco,
                          @deposito_inspecionado_a1 = model.deposito_inspecionado_a1,
                          @deposito_inspecionado_a2 = model.deposito_inspecionado_a2,
                          @deposito_inspecionado_b = model.deposito_inspecionado_b,
                          @deposito_inspecionado_c = model.deposito_inspecionado_c,
                          @deposito_inspecionado_d1 = model.deposito_inspecionado_d1,
                          @deposito_inspecionado_d2 = model.deposito_inspecionado_d2,
                          @deposito_inspecionado_e = model.deposito_inspecionado_e,
                          @deposito_eliminado = model.deposito_eliminado,
                          @pendencia_descricao = model.pendencia_descricao,
                          @trabalho_educativo = model.trabalho_educativo,
                          @trabalho_mecanico = model.trabalho_mecanico,
                          @trabalho_quimico = model.trabalho_quimico,
                          @trat_focal_larvi1_tipo = model.trat_focal_larvi1_tipo,
                          @trat_focal_larvi1_qtd_gramas = model.trat_focal_larvi1_qtd_gramas,
                          @trat_focal_larvi1_qtd_dep_trat = model.trat_focal_larvi1_qtd_dep_trat,
                          //@trat_focal_larvi2_tipo = model.trat_focal_larvi2_tipo,
                          //@trat_focal_larvi2_qtd_gramas = model.trat_focal_larvi2_qtd_gramas,
                          //@trat_focal_larvi2_qtd_dep_trat = model.trat_focal_larvi2_qtd_dep_trat,
                          @trat_perifocal_adult_tipo = model.trat_perifocal_adult_tipo,
                          @trat_perifocal_adult_qtd_carga = model.trat_perifocal_adult_qtd_carga,
                          @latitude_cadastro = model.latitude_cadastro,
                          @longitude_cadastro = model.longitude_cadastro,
                          @latitude_foco = model.latitude_foco,
                          @longitude_foco = model.longitude_foco,
                          @id_profissional = model.id_profissional,
                          @data_alteracao_serv = model.data_alteracao_serv,
                          @numero_tubito = model.numero_tubito,
                          @id_esus_exportacao_item = model.id_esus_exportacao_item,
                          @id_estabelecimento = model.id_estabelecimento,
                          @id_ciclo = model.id_ciclo
                      }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Visita> GetVisitasByCiclo(string ibge, int id)
        {
            try
            {
                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                      conn.Query<Visita>(_command.GetVisitasByCiclo, new { @id_ciclo = id })).ToList();
                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
