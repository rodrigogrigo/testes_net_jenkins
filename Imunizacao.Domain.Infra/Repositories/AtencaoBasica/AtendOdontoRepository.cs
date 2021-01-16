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
    public class AtendOdontoRepository : IAtendOdontoRepository
    {
        private IAtendOdontoCommand _command;
        public AtendOdontoRepository(IAtendOdontoCommand command)
        {
            _command = command;
        }

        public void ExcluirItemById(string ibge, int id)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                     conn.Execute(_command.ExcluirItemById, new
                     {
                         @id = id
                     }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ExcluirItemPai(string ibge, int id)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                   conn.Execute(_command.ExcluirItemPai, new
                   {
                       @id = id
                   }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ExcluirItensByPai(string ibge, int id)
        {
            try
            {

                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                  conn.Execute(_command.ExcluirItensByPai, new
                  {
                      @id = id
                  }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AtendOdontoViewModel> GetAllPagination(string ibge, int page, int pagesize, string filtro)
        {
            try
            {
                string sql = _command.GetAllPagination;
                if (!string.IsNullOrWhiteSpace(filtro))
                    sql = sql.Replace("@filtro", filtro);
                else
                    sql = sql.Replace("@filtro", string.Empty);

                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                      conn.Query<AtendOdontoViewModel>(sql, new
                      {
                          @pagesize = pagesize,
                          @page = page
                      })).ToList();
                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AtendOdontoIndividual GetAtendOdontoById(string ibge, int id)
        {
            try
            {
                var objeto = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                      conn.QueryFirstOrDefault<AtendOdontoIndividual>(_command.GetAtendOdontoById, new
                      {
                          @id = id
                      }));

                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                     conn.Query<AtendOdontoIndividualItem>(_command.GetAtendOdontoItensByPai, new
                     {
                         @id_pai = id
                     })).ToList();

                objeto.itens.AddRange(itens);
                return objeto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AtendOdontoIndividualItem GetAtendOdontoItemById(string ibge, int id)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                     conn.QueryFirstOrDefault<AtendOdontoIndividualItem>(_command.GetAtendOdontoItemById, new
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

        public List<AtendOdontoIndividualItem> GetAtendOdontoItensByPai(string ibge, int id)
        {
            try
            {
                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                    conn.Query<AtendOdontoIndividualItem>(_command.GetAtendOdontoItensByPai, new
                    {
                        @id_pai = id
                    })).ToList();

                return itens;
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

                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                      conn.QueryFirstOrDefault<int>(sql));
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetNewId(string ibge)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                    conn.QueryFirstOrDefault<int>(_command.GetNewId));
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetNewIdItem(string ibge)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                  conn.QueryFirstOrDefault<int>(_command.GetNewIdItem));
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AtendOdontoProcedimentoViewModel> GetProcOdontoIndividualizado(string ibge, string filtros, string cbo, int page, int pagesize)
        {
            try
            {
                var sql = _command.GetProcOdontoIndividualizado.Replace("@filtro", $"{filtros}");
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                            conn.Query<AtendOdontoProcedimentoViewModel>(sql, new
                            {
                                @cbo = cbo,
                                @page = page,
                                pagesize = pagesize
                            }).ToList());
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetCountProcOdontoIndividualizado(string ibge, string filtros, string cbo)
        {
            try
            {
                var sql = _command.GetCountProcOdontoIndividualizado.Replace("@filtro", $"{filtros}");
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                            conn.QueryFirstOrDefault<int>(sql, new
                            {
                                @cbo = cbo
                            }));
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateOrInsert(string ibge, AtendOdontoIndividual model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                      conn.Execute(_command.InsertOrUpdate, new
                      {
                          @id = model.id,
                          @id_profissional = model.id_profissional,
                          @id_unidade = model.id_unidade,
                          @turno = model.turno,
                          @data = model.data,
                          @id_cidadao = model.id_cidadao,
                          @id_local_atendimento = model.id_local_atendimento,
                          @id_tipo_atendimento = model.id_tipo_atendimento,
                          @id_tipo_consulta = model.id_tipo_consulta,
                          @flg_vig_abscesso_dento = model.flg_vig_abscesso_dento,
                          @flg_vig_alteracao_tecidos = model.flg_vig_alteracao_tecidos,
                          @flg_vig_dor_dente = model.flg_vig_dor_dente,
                          @flg_vig_fendas_fissuras = model.flg_vig_fendas_fissuras,
                          @flg_vig_fluorose_dentaria = model.flg_vig_fluorose_dentaria,
                          @flg_vig_traumalismo = model.flg_vig_traumalismo,
                          @flg_vig_nao_identificado = model.flg_vig_nao_identificado,
                          @id_fornecimento = model.id_fornecimento,
                          @flg_conduta_ret_consulta = model.flg_conduta_ret_consulta,
                          @flg_conduta_outros_prof = model.flg_conduta_outros_prof,
                          @flg_conduta_agenda_nasf = model.flg_conduta_agenda_nasf,
                          @flg_conduta_agenda_grupo = model.flg_conduta_agenda_grupo,
                          @flg_tratamento_concluido = model.flg_tratamento_concluido,
                          @flg_enc_conduta_nec_especiais = model.flg_enc_conduta_nec_especiais,
                          @flg_enc_conduta_cirur_bmf = model.flg_enc_conduta_cirur_bmf,
                          @flg_enc_conduta_endodontia = model.flg_enc_conduta_endodontia,
                          @flg_enc_conduta_estomatologia = model.flg_enc_conduta_estomatologia,
                          @flg_enc_conduta_implantodontia = model.flg_enc_conduta_implantodontia,
                          @flg_enc_conduta_odontopediatria = model.flg_enc_conduta_odontopediatria,
                          @flg_enc_conduta_ortodontia = model.flg_enc_conduta_ortodontia,
                          @flg_enc_conduta_periodontia = model.flg_enc_conduta_periodontia,
                          @flg_enc_conduta_prot_dentaria = model.flg_enc_conduta_prot_dentaria,
                          @flg_enc_conduta_radiologia = model.flg_enc_conduta_radiologia,
                          @flg_enc_conduta_outros = model.flg_enc_conduta_outros,
                          @flg_for_escova_dental = model.flg_for_escova_dental,
                          @flg_for_creme_dental = model.flg_for_creme_dental,
                          @flg_for_fio_dental = model.flg_for_fio_dental,
                          @id_profissional2 = model.id_profissional2,
                          @id_profissional3 = model.id_profissional3,
                          @flg_atend_gestante = model.flg_atend_gestante,
                          @flg_atend_nescecidade_especial = model.flg_atend_nescecidade_especial,
                          @flg_conduta_alta_epsodio = model.flg_conduta_alta_epsodio,
                          @id_cbo = model.id_cbo,
                          @uuid = model.uuid,
                          @id_usuario = model.id_usuario,
                          @data_fim_atendimento = model.data_fim_atendimento,
                          @id_equipe = model.id_equipe
                      }));

                foreach (var item in model.itens)
                {
                    Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                    conn.Execute(_command.InsertOrUpdateItens, new
                    {
                        @id = item.id,
                        @id_atend_odont = model.id, // item pai
                        @id_procedimento = item.id_procedimento,
                        @quantidade_procedimento = item.quantidade_procedimento,
                        @id_producao = item.id_producao,
                        @uuid = item.uuid,
                        @id_esus_exportacao_item = item.id_esus_exportacao_item,
                        @id_atend_odont_realizado = item.id_atend_odont_realizado,
                        @id_dente = item.id_dente,
                        @id_local_procedimento_odonto = item.id_local_procedimento_odonto,
                        @id_lista_regiao_procedimento = item.id_lista_regiao_procedimento,
                        @id_classe = item.id_classe,
                        @id_atend_prontuario = item.id_atend_prontuario,
                        @id_atend_prontuario_realizado = item.id_atend_prontuario_realizado,
                        @observacao = item.observacao,
                        @data_realizar = item.data_realizar,
                        @data_realizado = item.data_realizado,
                        @id_controle_sincronizacao_lote = item.id_controle_sincronizacao_lote
                    }));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
