using Dapper;
using RgCidadao.Domain.Commands.E_SUS;
using RgCidadao.Domain.Entities.E_SUS;
using RgCidadao.Domain.Repositories.E_SUS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RgCidadao.Domain.Infra.Repositories.E_SUS
{
    public class ProcedimentoRepository : IProcedimentoRepository
    {
        private IProcedimentoCommand _command;
        public ProcedimentoRepository(IProcedimentoCommand command)
        {
            _command = command;
        }

        public List<ProcedimentoAvulso> GetAllPagination(string ibge, string filtro, int page, int pagesize)
        {
            try
            {
                var lista = new List<ProcedimentoAvulso>();

                if (!string.IsNullOrWhiteSpace(filtro))
                {
                    lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                     conn.Query<ProcedimentoAvulso>(_command.GetAllPagination.Replace("@filtro", filtro), new
                     {
                         @pagesize = pagesize,
                         @page = page
                     })).ToList();
                }
                else
                {
                    lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                    conn.Query<ProcedimentoAvulso>(_command.GetAllPagination.Replace("@filtro", string.Empty), new
                    {
                        @pagesize = pagesize,
                        @page = page
                    })).ToList();
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Editar(string ibge, ProcedimentoAvulso model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                     conn.Execute(_command.Editar, new
                     {
                         @csi_data = model.csi_data,
                         @csi_codmed = model.csi_codmed,
                         @csi_nomusu = model.csi_nomusu,
                         @csi_datainc = model.csi_datainc,
                         @csi_obs = model.csi_obs,
                         @csi_cbo = model.csi_cbo,
                         @csi_coduni = model.csi_coduni,
                         @idtriagem = model.idtriagem,
                         @idestabelecimento = model.idestabelecimento,
                         @idatend_odontologico = model.idatend_odontologico,
                         @idatividade_coletiva = model.idatividade_coletiva,
                         @idvisita_domiciliar = model.idvisita_domiciliar,
                         @csi_local_atendimento = model.csi_local_atendimento,
                         @turno = model.turno,
                         @id_denuncia = model.id_denuncia,
                         @id_inspecao = model.id_inspecao,
                         @id_atendimento_individual = model.id_atendimento_individual,
                         @id_pep_anamnese = model.id_pep_anamnese,
                         @id_licenca = model.id_licenca,
                         @id_inspecao_veiculo = model.id_inspecao_veiculo,
                         @id_denuncia_andamento = model.id_denuncia_andamento,
                         @id_pep_exame_fisico = model.id_pep_exame_fisico,
                         @id_administrar_medicamento = model.id_administrar_medicamento,
                         @csi_controle = model.csi_controle,
                         @csi_codpac = model.csi_codpac,
                     }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EditarItem(string ibge, ProcedimentoAvulsoItem model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                     conn.Execute(_command.EditarItem, new
                     {
                         @csi_controle = model.csi_controle,
                         @csi_codproc = model.csi_codproc,
                         @csi_qtde = model.csi_qtde,
                         @csi_id_producao = model.csi_id_producao,
                         @csi_idade = model.csi_idade,
                         @csi_codcid = model.csi_codcid,
                         @csi_escuta_inicial = model.csi_escuta_inicial,
                         @id_esus_exportacao_item = model.id_esus_exportacao_item,
                         @uuid = model.uuid,
                         @id_sequencial = model.id_sequencial
                     }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Excluir(string ibge, int id)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                     conn.Execute(_command.Excluir, new
                     {
                         @csi_controle = id
                     }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ExcluirItem(string ibge, int id, int idproc)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                     conn.Execute(_command.ExcluirItem, new
                     {
                         @csi_controle = id,
                         @csi_codproc = idproc
                     }));
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
                int contagem = 0;

                if (!string.IsNullOrWhiteSpace(filtro))
                {
                    contagem = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                     conn.QueryFirstOrDefault<int>(_command.GetCountAll.Replace("@filtro", filtro)));
                }
                else
                {
                    contagem = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                     conn.QueryFirstOrDefault<int>(_command.GetCountAll.Replace("@filtro", string.Empty)));
                }
                return contagem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProcedimentoAvulso GetProcEnfermagemById(string ibge, int id)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                               conn.QueryFirstOrDefault<ProcedimentoAvulso>(_command.GetProcEnfermagemById, new
                               {
                                   @id = id
                               }));
                return item;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ProcedimentoAvulsoItem GetProcEnfermagemItemByPaiProc(string ibge, int id, int proc)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                               conn.QueryFirstOrDefault<ProcedimentoAvulsoItem>(_command.GetProcEnfermagemById, new
                               {
                                   @id_pai = id,
                                   @id_proc = proc
                               }));
                return item;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ProcedimentoAvulsoItem> GetProcEnfermagemItensByPai(string ibge, int id)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                               conn.Query<ProcedimentoAvulsoItem>(_command.GetProcEnfermagemById, new
                               {
                                   @id_pai = id
                               })).ToList();
                return item;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Inserir(string ibge, ProcedimentoAvulso model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                   conn.Execute(_command.Inserir, new
                   {
                       @csi_data = model.csi_data,
                       @csi_codmed = model.csi_codmed,
                       @csi_nomusu = model.csi_nomusu,
                       @csi_datainc = model.csi_datainc,
                       @csi_obs = model.csi_obs,
                       @csi_cbo = model.csi_cbo,
                       @csi_coduni = model.csi_coduni,
                       @idtriagem = model.idtriagem,
                       @idestabelecimento = model.idestabelecimento,
                       @idatend_odontologico = model.idatend_odontologico,
                       @idatividade_coletiva = model.idatividade_coletiva,
                       @idvisita_domiciliar = model.idvisita_domiciliar,
                       @csi_local_atendimento = model.csi_local_atendimento,
                       @turno = model.turno,
                       @id_denuncia = model.id_denuncia,
                       @id_inspecao = model.id_inspecao,
                       @id_atendimento_individual = model.id_atendimento_individual,
                       @id_pep_anamnese = model.id_pep_anamnese,
                       @id_licenca = model.id_licenca,
                       @id_inspecao_veiculo = model.id_inspecao_veiculo,
                       @id_denuncia_andamento = model.id_denuncia_andamento,
                       @id_pep_exame_fisico = model.id_pep_exame_fisico,
                       @id_administrar_medicamento = model.id_administrar_medicamento,
                       @csi_controle = model.csi_controle,
                       @csi_codpac = model.csi_codpac,
                   }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InserirItem(string ibge, ProcedimentoAvulsoItem model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                   conn.Execute(_command.EditarItem, new
                   {
                       @csi_qtde = model.csi_qtde,
                       @csi_id_producao = model.csi_id_producao,
                       @csi_idade = model.csi_idade,
                       @csi_codcid = model.csi_codcid,
                       @csi_escuta_inicial = model.csi_escuta_inicial,
                       @id_esus_exportacao_item = model.id_esus_exportacao_item,
                       @uuid = model.uuid,
                       @id_sequencial = model.id_sequencial,
                       @csi_controle = model.csi_controle,
                       @csi_codproc = model.csi_codproc
                   }));
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
                var id = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                            conn.QueryFirstOrDefault<int>(_command.GetNewId));
                return id;
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
                var id = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.QueryFirstOrDefault<int>(_command.GetNewIdItem));
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetNewUuid(string ibge)
        {
            try
            {
                var id = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.QueryFirstOrDefault<string>(_command.GetNewUuid));
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
