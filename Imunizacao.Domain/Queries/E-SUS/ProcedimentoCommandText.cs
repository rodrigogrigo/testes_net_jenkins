using RgCidadao.Domain.Commands.E_SUS;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Queries.E_SUS
{
    public class ProcedimentoCommandText : IProcedimentoCommand
    {
        public static string sqlGetAllPagination = $@"SELECT FIRST(@pagesize) SKIP(@page)
                                                      FROM TSI_PROCENFERMAGEM 
                                                      @filtro";
        string IProcedimentoCommand.GetAllPagination { get => sqlGetAllPagination; }

        public static string sqlGetCountAll = $@"SELECT COUNT(*)
                                                 FROM TSI_PROCENFERMAGEM
                                                 @filtro";
        string IProcedimentoCommand.GetCountAll { get => sqlGetCountAll; }

        public static string sqlGetNewId = $@"SELECT GEN_ID(GEN_TSI_PROCENFERMAGEM, 1) AS VLR FROM RDB$DATABASE";
        string IProcedimentoCommand.GetNewId { get => sqlGetNewId; }

        public static string sqlGetProcEnfermagemById = $@"SELECT * FROM TSI_PROCENFERMAGEM
                                                           WHERE CSI_CONTROLE = @id";
        string IProcedimentoCommand.GetProcEnfermagemById { get => sqlGetProcEnfermagemById; }

        public static string sqlGetProcEnfermagemItensByPai = $@"SELECT * FROM TSI_IPROCENFERMAGEM
                                                                 WHERE CSI_CONTROLE = @id_pai";
        string IProcedimentoCommand.GetProcEnfermagemItensByPai { get => sqlGetProcEnfermagemItensByPai; }

        public static string sqlGetProcEnfermagemItemByPaiProc = $@"SELECT * FROM TSI_IPROCENFERMAGEM
                                                                    WHERE CSI_CONTROLE = @id_pai AND
                                                                          CSI_CODPROC = @id_proc";
        string IProcedimentoCommand.GetProcEnfermagemItemByPaiProc { get => sqlGetProcEnfermagemItemByPaiProc; }

        public static string sqlInserir = $@"INSERT INTO TSI_PROCENFERMAGEM (CSI_CONTROLE, CSI_CODPAC, CSI_DATA, CSI_CODMED, CSI_NOMUSU, CSI_DATAINC, CSI_OBS,
                                                                             CSI_CBO, CSI_CODUNI, IDTRIAGEM, IDESTABELECIMENTO, IDATEND_ODONTOLOGICO,
                                                                             IDATIVIDADE_COLETIVA, IDVISITA_DOMICILIAR, CSI_LOCAL_ATENDIMENTO, TURNO, ID_DENUNCIA,
                                                                             ID_INSPECAO, ID_ATENDIMENTO_INDIVIDUAL, ID_PEP_ANAMNESE, ID_LICENCA,
                                                                             ID_INSPECAO_VEICULO, ID_DENUNCIA_ANDAMENTO, ID_PEP_EXAME_FISICO,
                                                                             ID_ADMINISTRAR_MEDICAMENTO)
                                             VALUES (@csi_controle, @csi_codpac, @csi_data, @csi_codmed, @csi_nomusu, @csi_datainc, @csi_obs, @csi_cbo,
                                                     @csi_coduni, @idtriagem, @idestabelecimento, @idatend_odontologico, @idatividade_coletiva,
                                                     @idvisita_domiciliar, @csi_local_atendimento, @turno, @id_denuncia, @id_inspecao,
                                                     @id_atendimento_individual, @id_pep_anamnese, @id_licenca, @id_inspecao_veiculo, @id_denuncia_andamento,
                                                     @id_pep_exame_fisico, @id_administrar_medicamento)";
        string IProcedimentoCommand.Inserir { get => sqlInserir; }

        public static string sqlGetNewIdItem = $@"SELECT GEN_ID(GEN_TSI_IPROCENFERMAGEM_ID, 1) AS VLR FROM RDB$DATABASE";
        string IProcedimentoCommand.GetNewIdItem { get => sqlGetNewIdItem; }

        public static string sqlGetNewUuid = $@"SELECT UUID_TO_CHAR(GEN_UUID()) FROM RDB$DATABASE";
        string IProcedimentoCommand.GetNewUuid { get => sqlGetNewUuid; }

        public static string sqlInserirItem = $@"INSERT INTO TSI_IPROCENFERMAGEM (CSI_CONTROLE, CSI_CODPROC, CSI_QTDE, CSI_ID_PRODUCAO, CSI_IDADE, CSI_CODCID,
                                                                                  CSI_ESCUTA_INICIAL, ID_ESUS_EXPORTACAO_ITEM, UUID, ID_SEQUENCIAL)
                                                 VALUES (@csi_controle, @csi_codproc, @csi_qtde, @csi_id_producao, @csi_idade, @csi_codcid, @csi_escuta_inicial,
                                                         @id_esus_exportacao_item, @uuid, @id_sequencial)";
        string IProcedimentoCommand.InserirItem { get => sqlInserirItem; }

        public static string sqlEditar = $@"UPDATE TSI_PROCENFERMAGEM
                                            SET CSI_CODPAC = @csi_codpac,
                                                CSI_DATA = @csi_data,
                                                CSI_CODMED = @csi_codmed,
                                                CSI_NOMUSU = @csi_nomusu,
                                                CSI_DATAINC = @csi_datainc,
                                                CSI_OBS = @csi_obs,
                                                CSI_CBO = @csi_cbo,
                                                CSI_CODUNI = @csi_coduni,
                                                IDTRIAGEM = @idtriagem,
                                                IDESTABELECIMENTO = @idestabelecimento,
                                                IDATEND_ODONTOLOGICO = @idatend_odontologico,
                                                IDATIVIDADE_COLETIVA = @idatividade_coletiva,
                                                IDVISITA_DOMICILIAR = @idvisita_domiciliar,
                                                CSI_LOCAL_ATENDIMENTO = @csi_local_atendimento,
                                                TURNO = @turno,
                                                ID_DENUNCIA = @id_denuncia,
                                                ID_INSPECAO = @id_inspecao,
                                                ID_ATENDIMENTO_INDIVIDUAL = @id_atendimento_individual,
                                                ID_PEP_ANAMNESE = @id_pep_anamnese,
                                                ID_LICENCA = @id_licenca,
                                                ID_INSPECAO_VEICULO = @id_inspecao_veiculo,
                                                ID_DENUNCIA_ANDAMENTO = @id_denuncia_andamento,
                                                ID_PEP_EXAME_FISICO = @id_pep_exame_fisico,
                                                ID_ADMINISTRAR_MEDICAMENTO = @id_administrar_medicamento
                                            WHERE CSI_CONTROLE = @csi_controle";
        string IProcedimentoCommand.Editar { get => sqlEditar; }

        public static string sqlEditarItem = $@"UPDATE TSI_IPROCENFERMAGEM
                                                SET CSI_QTDE = @csi_qtde,
                                                    CSI_ID_PRODUCAO = @csi_id_producao,
                                                    CSI_IDADE = @csi_idade,
                                                    CSI_CODCID = @csi_codcid,
                                                    CSI_ESCUTA_INICIAL = @csi_escuta_inicial,
                                                    ID_ESUS_EXPORTACAO_ITEM = @id_esus_exportacao_item,
                                                    UUID = @uuid,
                                                    ID_SEQUENCIAL = @id_sequencial
                                                WHERE CSI_CONTROLE = @csi_controle,
                                                      CSI_CODPROC = @csi_codproc";
        string IProcedimentoCommand.EditarItem { get => sqlEditarItem; }

        public static string sqlExcluir = $@"DELETE FROM TSI_PROCENFERMAGEM
                                             WHERE CSI_CONTROLE = @csi_controle";
        string IProcedimentoCommand.Excluir { get => sqlExcluir; }

        public static string sqlExcluirItem = $@"DELETE FROM TSI_IPROCENFERMAGEM
                                                 WHERE CSI_CONTROLE = @csi_controle,
                                                       CSI_CODPROC = @csi_codproc";
        string IProcedimentoCommand.ExcluirItem { get => sqlExcluirItem; }
    }
}
