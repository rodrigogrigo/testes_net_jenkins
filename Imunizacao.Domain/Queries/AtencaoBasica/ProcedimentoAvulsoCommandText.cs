using RgCidadao.Domain.Commands.AtencaoBasica;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Queries.AtencaoBasica
{
    public class ProcedimentoAvulsoCommandText : IProcedimentoAvulsoCommand
    {
        public string sqlGetAllPagination = $@"SELECT FIRST (@pagesize) SKIP (@page) PE.CSI_CONTROLE, PE.CSI_CODPAC, P.CSI_NOMPAC,
                                                      PE.CSI_CODMED, M.CSI_NOMMED, PE.CSI_DATA,
                                                      US.NOME AS CSI_NOMUSU, PE.CSI_DATAINC, M.CSI_CBO, PE.CSI_CODUNI, UNI.CSI_NOMUNI
                                               FROM TSI_PROCENFERMAGEM PE
                                               JOIN TSI_UNIDADE UNI ON UNI.CSI_CODUNI = PE.CSI_CODUNI
                                               LEFT OUTER JOIN TSI_CADPAC P ON (P.CSI_CODPAC = PE.CSI_CODPAC)
                                               INNER JOIN TSI_MEDICOS M ON (M.CSI_CODMED = PE.CSI_CODMED)
                                               LEFT JOIN SEG_USUARIO US ON (US.LOGIN = PE.CSI_NOMUSU)
                                               LEFT JOIN SEG_PERFIL_USUARIO UU ON (UU.ID_UNIDADE = UNI.CSI_CODUNI AND UU.ID_USUARIO = US.ID)
                                               WHERE 1 = 1 @filtros";
        string IProcedimentoAvulsoCommand.GetAllPagination { get => sqlGetAllPagination; }

        public string sqlGetCountAll = $@"SELECT COUNT(*) FROM TSI_PROCENFERMAGEM PE WHERE 1 = 1 @filtros ";
        string IProcedimentoAvulsoCommand.GetCountAll { get => sqlGetCountAll; }

        public string sqlGetNewId = $@"SELECT GEN_ID(GEN_TSI_PROCENFERMAGEM, 1) AS VLR FROM RDB$DATABASE";
        string IProcedimentoAvulsoCommand.GetNewId { get => sqlGetNewId; }

        public string sqlGetProcedimentosById = $@" SELECT * FROM TSI_PROCENFERMAGEM
                                                    WHERE CSI_CONTROLE = @codigo";
        string IProcedimentoAvulsoCommand.GetProcedimentosById { get => sqlGetProcedimentosById; }

        public string sqlGetProcedimentosItensByPai = $@"SELECT *
                                                         FROM TSI_IPROCENFERMAGEM
                                                         WHERE CSI_CONTROLE = @codigo";
        string IProcedimentoAvulsoCommand.GetProcedimentosItensByPai { get => sqlGetProcedimentosItensByPai; }

        public string sqlInsert = $@"INSERT INTO TSI_PROCENFERMAGEM (CSI_CONTROLE, CSI_CODPAC, CSI_DATA, CSI_CODMED, CSI_NOMUSU, CSI_DATAINC, CSI_OBS,
                                                                     CSI_CBO, CSI_CODUNI, IDTRIAGEM, IDESTABELECIMENTO, IDATEND_ODONTOLOGICO,
                                                                     IDATIVIDADE_COLETIVA, IDVISITA_DOMICILIAR, CSI_LOCAL_ATENDIMENTO, TURNO, ID_DENUNCIA,
                                                                     ID_INSPECAO, ID_ATENDIMENTO_INDIVIDUAL, ID_PEP_ANAMNESE, ID_LICENCA,
                                                                     ID_INSPECAO_VEICULO, ID_DENUNCIA_ANDAMENTO, ID_PEP_EXAME_FISICO,
                                                                     ID_ADMINISTRAR_MEDICAMENTO, ID_EQUIPE)
                                     VALUES (@csi_controle, @csi_codpac, @csi_data, @csi_codmed, @csi_nomusu, @csi_datainc, @csi_obs, @csi_cbo, @csi_coduni,
                                             @idtriagem, @idestabelecimento, @idatend_odontologico, @idatividade_coletiva, @idvisita_domiciliar,
                                             @csi_local_atendimento, @turno, @id_denuncia, @id_inspecao, @id_atendimento_individual, @id_pep_anamnese, @id_licenca,
                                             @id_inspecao_veiculo, @id_denuncia_andamento, @id_pep_exame_fisico, @id_administrar_medicamento, @id_equipe) ";
        string IProcedimentoAvulsoCommand.Insert { get => sqlInsert; }

        public string sqlUpdate = $@"UPDATE TSI_PROCENFERMAGEM
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
                                         ID_ADMINISTRAR_MEDICAMENTO = @id_administrar_medicamento,
                                         ID_EQUIPE = @id_equipe
                                     WHERE CSI_CONTROLE = @id_controle";
        string IProcedimentoAvulsoCommand.Update { get => sqlUpdate; }

        public string sqlDelete = $@"DELETE FROM TSI_PROCENFERMAGEM
                                     WHERE CSI_CONTROLE = @id_controle";
        string IProcedimentoAvulsoCommand.Delete { get => sqlDelete; }

        public string sqlGetNewIdItem = $@"SELECT GEN_ID(GEN_TSI_IPROCENFERMAGEM_ID, 1) AS VLR FROM RDB$DATABASE";
        string IProcedimentoAvulsoCommand.GetNewIdItem { get => sqlGetNewId; }

        public string sqlInsertItens = $@"UPDATE OR INSERT INTO TSI_IPROCENFERMAGEM (CSI_CONTROLE, CSI_CODPROC, CSI_QTDE, CSI_ID_PRODUCAO, CSI_IDADE, CSI_CODCID,
                                                                           CSI_ESCUTA_INICIAL, ID_ESUS_EXPORTACAO_ITEM, UUID, ID_SEQUENCIAL)
                                          VALUES (@csi_controle, @csi_codproc, @csi_qtde, @csi_id_producao, @csi_idade, @csi_codcid, @csi_escuta_inicial,
                                                  @id_esus_exportacao_item, @uuid, @id_sequencial)";
        string IProcedimentoAvulsoCommand.InsertItens { get => sqlInsertItens; }

        public string sqlExcluirItens = $@"DELETE FROM TSI_IPROCENFERMAGEM
                                           WHERE CSI_CONTROLE = @id_controle and
                                                 CSI_CODPROC = @id_codproc";
        string IProcedimentoAvulsoCommand.ExcluirItens { get => sqlExcluirItens; }

        public string sqlGetProcedimentosConsolidados = $@"SELECT DISTINCT E.CSI_CODSUS AS CSI_CODPROC, E.CSI_NOME AS NOME, E.SIGLA_ESUS
                                                           FROM TSI_CADEXAMES E
                                                           JOIN TSI_PROCEDIMENTO P ON P.CODIGO = E.CSI_CODSUS
                                                           JOIN COMPETENCIA_SIGTAP CS ON CS.ID = P.ID_COMPETENCIA
                                                           JOIN TSI_PROCEDIMENTO_REGISTRO PR ON PR.COD_PROCEDIMENTO = P.CODIGO AND PR.ID_COMPETENCIA = CS.ID AND PR.COD_PADRAO_REGISTRO_BPA IN ('01', '02', '10')
                                                           JOIN TSI_PROCEDIMENTO_CBO CBO ON CBO.COD_PROCEDIMENTO = P.CODIGO AND CBO.ID_COMPETENCIA = PR.ID_COMPETENCIA
                                                           WHERE E.SIGLA_ESUS IS NOT NULL AND
                                                                 CBO.COD_CBO = @cbo AND
                                                                 CS.ATIVO = 'T' @filtro1 AND
                                                                 E.CSI_CODSUS IN (0101040024, 0301100039, 0401010023, 0201020041, 0214010015)
                                                           UNION
                                                           SELECT DISTINCT E.CSI_CODSUS AS CSI_CODPROC, E.CSI_NOME AS NOME, E.SIGLA_ESUS
                                                           FROM TSI_CADEXAMES E
                                                           JOIN TSI_PROCEDIMENTO P ON P.CODIGO = E.CSI_CODSUS
                                                           JOIN COMPETENCIA_SIGTAP CS ON CS.ID = P.ID_COMPETENCIA
                                                           JOIN TSI_PROCEDIMENTO_REGISTRO PR ON PR.COD_PROCEDIMENTO = P.CODIGO AND PR.ID_COMPETENCIA = CS.ID AND PR.COD_PADRAO_REGISTRO_BPA IN ('01', '02', '10')
                                                           WHERE P.CODIGO NOT IN (SELECT DISTINCT CBO.COD_PROCEDIMENTO
                                                                                  FROM TSI_PROCEDIMENTO_CBO CBO
                                                                                  JOIN COMPETENCIA_SIGTAP CSS ON CSS.ID = CBO.ID_COMPETENCIA
                                                                                  WHERE CSS.ATIVO = 'T') AND
                                                                 E.SIGLA_ESUS IS NOT NULL AND
                                                                 CS.ATIVO = 'T' @filtro2 AND
                                                                 E.CSI_CODSUS IN (0101040024, 0301100039, 0401010023, 0201020041, 0214010015)
                                                           UNION
                                                           SELECT DISTINCT E.CSI_CODSUS AS CSI_CODPROC, E.CSI_NOME AS NOME, E.SIGLA_ESUS
                                                           FROM TSI_CADEXAMES E
                                                           JOIN TSI_PROCEDIMENTO P ON P.CODIGO = E.CSI_CODSUS
                                                           WHERE E.SIGLA_ESUS IS NOT NULL AND
                                                                 E.CSI_CODSUS IN (9800000008) ";
        string IProcedimentoAvulsoCommand.GetProcedimentosConsolidados { get => sqlGetProcedimentosConsolidados; }

        public string sqlGetProcedimentosIndividualizados = $@"SELECT DISTINCT P.CODIGO CSI_CODPROC, P.NOME, PR.COD_PADRAO_REGISTRO_BPA AS REGISTRO
                                                               FROM TSI_PROCEDIMENTO P
                                                               JOIN TSI_PROCEDIMENTO_CBO CBO ON CBO.COD_PROCEDIMENTO = P.CODIGO
                                                               JOIN TSI_PROCEDIMENTO_REGISTRO PR ON PR.COD_PROCEDIMENTO = CBO.COD_PROCEDIMENTO AND PR.ID_COMPETENCIA = CBO.ID_COMPETENCIA AND PR.COD_PADRAO_REGISTRO_BPA IN ('01', '02', '10')
                                                               JOIN COMPETENCIA_SIGTAP CS ON CS.ID = P.ID_COMPETENCIA
                                                               WHERE CBO.COD_CBO = @cbo AND
                                                                     CS.ATIVO = 'T'
                                                                     @filtro1
                                                               UNION
                                                               SELECT DISTINCT P.CODIGO CSI_CODPROC, P.NOME, PR.COD_PADRAO_REGISTRO_BPA AS REGISTRO
                                                               FROM TSI_PROCEDIMENTO P
                                                               JOIN COMPETENCIA_SIGTAP CS ON CS.ID = P.ID_COMPETENCIA
                                                               JOIN TSI_PROCEDIMENTO_REGISTRO PR ON PR.COD_PROCEDIMENTO = P.CODIGO AND PR.ID_COMPETENCIA = CS.ID AND PR.COD_PADRAO_REGISTRO_BPA IN ('01', '02', '10')
                                                               WHERE P.CODIGO NOT IN (SELECT DISTINCT CBO.COD_PROCEDIMENTO
                                                                                      FROM TSI_PROCEDIMENTO_CBO CBO
                                                                                      JOIN COMPETENCIA_SIGTAP CSS ON CSS.ID = CBO.ID_COMPETENCIA
                                                                                      WHERE CSS.ATIVO = 'T') AND
                                                                     CS.ATIVO = 'T'
                                                                     @filtro2
                                                               UNION
                                                               SELECT DISTINCT E.CSI_CODSUS AS CSI_CODPROC, E.CSI_NOME AS NOME, E.SIGLA_ESUS
                                                               FROM TSI_CADEXAMES E
                                                               JOIN TSI_PROCEDIMENTO P ON P.CODIGO = E.CSI_CODSUS
                                                               JOIN COMPETENCIA_SIGTAP CS ON CS.ID = P.ID_COMPETENCIA
                                                               JOIN TSI_PROCEDIMENTO_REGISTRO PR ON PR.COD_PROCEDIMENTO = P.CODIGO AND PR.ID_COMPETENCIA = CS.ID AND PR.COD_PADRAO_REGISTRO_BPA IN ('01', '02', '10')
                                                               JOIN TSI_PROCEDIMENTO_CBO CBO ON CBO.COD_PROCEDIMENTO = P.CODIGO AND CBO.ID_COMPETENCIA = PR.ID_COMPETENCIA
                                                               WHERE E.SIGLA_ESUS IS NOT NULL AND
                                                                     CBO.COD_CBO = @cbo2 AND
                                                                     CS.ATIVO = 'T' @filtro3 AND
                                                                     E.CSI_CODSUS IN (0301100101, 0309050022, 0101040059, 0301100047, 0303080019, 0401010031, 0211020036, 0201020033, 0301040095, 0303090030,
                                                                                      0404010300, 0401010112, 0404010270, 0301100152, 0401010066, 0211060275, 0214010066, 0303100036, 0214010090, 0214010074,
                                                                                      0301100020, 0401020177, 0401010074, 0404010342, 0301100063, 0211060100, 0214010058)
                                                               UNION
                                                               SELECT DISTINCT E.CSI_CODSUS AS CSI_CODPROC, E.CSI_NOME AS NOME, E.SIGLA_ESUS
                                                               FROM TSI_CADEXAMES E
                                                               JOIN TSI_PROCEDIMENTO P ON P.CODIGO = E.CSI_CODSUS
                                                               JOIN COMPETENCIA_SIGTAP CS ON CS.ID = P.ID_COMPETENCIA
                                                               JOIN TSI_PROCEDIMENTO_REGISTRO PR ON PR.COD_PROCEDIMENTO = P.CODIGO AND PR.ID_COMPETENCIA = CS.ID AND PR.COD_PADRAO_REGISTRO_BPA IN ('01', '02', '10')
                                                               WHERE P.CODIGO NOT IN (SELECT DISTINCT CBO.COD_PROCEDIMENTO
                                                                                      FROM TSI_PROCEDIMENTO_CBO CBO
                                                                                      JOIN COMPETENCIA_SIGTAP CSS ON CSS.ID = CBO.ID_COMPETENCIA
                                                                                      WHERE CSS.ATIVO = 'T') AND
                                                                     E.SIGLA_ESUS IS NOT NULL AND
                                                                     CS.ATIVO = 'T'
                                                                     @filtro4 AND
                                                                     E.CSI_CODSUS IN (0301100101, 0309050022, 0101040059, 0301100047, 0303080019, 0401010031, 0211020036, 0201020033, 0301040095, 0303090030,
                                                                                      0404010300, 0401010112, 0404010270, 0301100152, 0401010066, 0211060275, 0214010066, 0303100036, 0214010090, 0214010074,
                                                                                      0301100020, 0401020177, 0401010074, 0404010342, 0301100063, 0211060100, 0214010058)
                                                               UNION
                                                               SELECT DISTINCT E.CSI_CODSUS AS CSI_CODPROC, E.CSI_NOME AS NOME, E.SIGLA_ESUS
                                                               FROM TSI_CADEXAMES E
                                                               JOIN TSI_PROCEDIMENTO P ON P.CODIGO = E.CSI_CODSUS
                                                               WHERE E.SIGLA_ESUS IS NOT NULL AND
                                                                     E.CSI_CODSUS IN (9800000020, 9800000009, 9800000010, 9800000002, 9800000003, 9800000006, 9800000007)  ";
        string IProcedimentoAvulsoCommand.GetProcedimentosIndividualizados { get => sqlGetProcedimentosIndividualizados; }
    }
}
