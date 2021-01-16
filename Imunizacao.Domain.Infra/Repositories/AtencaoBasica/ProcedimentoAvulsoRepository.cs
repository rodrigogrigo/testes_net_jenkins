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
    public class ProcedimentoAvulsoRepository : IProcedimentoAvulsoRepository
    {
        private IProcedimentoAvulsoCommand _command;
        public ProcedimentoAvulsoRepository(IProcedimentoAvulsoCommand command)
        {
            _command = command;
        }

        public void Delete(string ibge, int id)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Execute(_command.Delete, new
                           {
                               @id_controle = id
                           }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteItem(string ibge, int id_controle, int id_codproc)
        {
            try
            {
                var sql = _command.ExcluirItens.Replace("@id_controle", id_controle.ToString()).Replace("@id_codproc", id_codproc.ToString());


                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                   conn.Execute(sql));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProcedimentoAvulso> GetAllPagination(string ibge, string filtro, int page, int pagesize, int? usuario)
        {
            try
            {
                string sql = _command.GetAllPagination;
                if (!string.IsNullOrWhiteSpace(filtro))
                    sql = sql.Replace("@filtros", filtro);
                else
                    sql = sql.Replace("@filtros", string.Empty);

                var itens = Helpers.HelperConnection.ExecuteCommand<List<ProcedimentoAvulso>>(ibge, conn =>
                           conn.Query<ProcedimentoAvulso>(sql, new
                           {
                               @pagesize = pagesize,
                               @page = page,                               
                           }).ToList());

                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetCountAll(string ibge, string filtro, int? usuario)
        {
            try
            {
                string sql = _command.GetCountAll;
                if (!string.IsNullOrWhiteSpace(filtro))
                    sql = sql.Replace("@filtros", filtro);
                else
                    sql = sql.Replace("@filtros", string.Empty);

                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.QueryFirstOrDefault<int>(sql, new {}));

                return itens;
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
                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                        conn.QueryFirstOrDefault<int>(_command.GetNewId));

                return itens;
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
                var codigo = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                       conn.Execute(_command.GetNewIdItem));

                return codigo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProcedimentosConsolidadosViewModel> GetProcedimentosConsolidados(string ibge, string cbo, string filtro)
        {
            try
            {
                string sql = string.Empty;
                sql = _command.GetProcedimentosConsolidados.Replace("@filtro1", filtro).Replace("@filtro2", filtro);

                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                       conn.Query<ProcedimentosConsolidadosViewModel>(sql, new { @cbo = cbo }).ToList());

                return itens;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<ProcedimentosConsolidadosViewModel> GetProcedimentosIndividualizado(string ibge, string cbo, string filtroSIGTAP, string filtroESUS)
        {
            try
            {
                string sql = string.Empty;
                sql = _command.GetProcedimentosIndividualizados.Replace("@filtro1", filtroSIGTAP).Replace("@filtro2", filtroSIGTAP).Replace("@filtro3", filtroESUS).Replace("@filtro4", filtroESUS);

                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                       conn.Query<ProcedimentosConsolidadosViewModel>(sql, new
                       {
                           @cbo = cbo,
                           @cbo2 = cbo
                       }).ToList());

                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProcEnfermagem GetProcEnfermagemById(string ibge, int id)
        {
            try
            {
                var codigo = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                      conn.QueryFirstOrDefault<ProcEnfermagem>(_command.GetProcedimentosById, new { @codigo = id }));

                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                     conn.Query<ProcEnfermagemItem>(_command.GetProcedimentosItensByPai, new { @codigo = id }).ToList());

                codigo.itens.AddRange(itens);

                return codigo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insert(string ibge, ProcEnfermagem model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                        conn.Execute(_command.Insert, new
                        {
                            @csi_controle = model.csi_controle,
                            @csi_codpac = model.csi_codpac,
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
                            @id_equipe = model.id_equipe
                        }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertItem(string ibge, ProcEnfermagemItem model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                   conn.Execute(_command.InsertItens, new
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

        public void Update(string ibge, ProcEnfermagem model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                        conn.Execute(_command.Update, new
                        {
                            @csi_codpac = model.csi_codpac,
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
                            @id_equipe = model.id_equipe,
                            @id_controle = model.csi_controle
                        }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
