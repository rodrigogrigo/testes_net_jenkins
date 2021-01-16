using RgCidadao.Domain.Commands.AtencaoBasica;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Queries.AtencaoBasica
{
    public class AtendOdontoCommandText : IAtendOdontoCommand
    {
        private string sqlGetAllPagination = $@"SELECT FIRST (@pagesize) SKIP (@page) ATEN.ID, ATEN.ID_PROFISSIONAL, MED.CSI_NOMMED NOME_PROFISSIONAL, ATEN.ID_CIDADAO, PAC.CSI_NOMPAC NOME_PACIENTE,
                                                       UNI.CSI_CODUNI ID_UNIDADE, UNI.CSI_NOMUNI UNIDADE, ATEN.DATA
                                                FROM ESUS_ATEND_ODONT_INDIVIDUAL ATEN
                                                JOIN TSI_MEDICOS MED ON MED.CSI_CODMED = ATEN.ID_PROFISSIONAL
                                                JOIN TSI_CADPAC PAC ON PAC.CSI_CODPAC = ATEN.ID_CIDADAO
                                                JOIN TSI_UNIDADE UNI ON UNI.CSI_CODUNI = ATEN.ID_UNIDADE
                                                @filtro";
        string IAtendOdontoCommand.GetAllPagination { get => sqlGetAllPagination; }

        private string sqlGetCountAll = $@"SELECT COUNT(*)
                                           FROM ESUS_ATEND_ODONT_INDIVIDUAL ATEN
                                           JOIN TSI_MEDICOS MED ON MED.CSI_CODMED = ATEN.ID_PROFISSIONAL
                                           JOIN TSI_CADPAC PAC ON PAC.CSI_CODPAC = ATEN.ID_CIDADAO
                                           JOIN TSI_UNIDADE UNI ON UNI.CSI_CODUNI = ATEN.ID_UNIDADE
                                           @filtro";
        string IAtendOdontoCommand.GetCountAll { get => sqlGetCountAll; }

        private string sqlGetAtendOdontoById = $@"SELECT * FROM ESUS_ATEND_ODONT_INDIVIDUAL
                                                  WHERE ID = @id";
        string IAtendOdontoCommand.GetAtendOdontoById { get => sqlGetAtendOdontoById; }

        private string sqlGetAtendOdontoItensByPai = $@"SELECT I.*, P.NOME FROM ESUS_IATEND_ODONT_INDIVIDUAL I
                                                        JOIN TSI_PROCEDIMENTO P ON P.CODIGO = I.ID_PROCEDIMENTO
                                                        WHERE ID_ATEND_ODONT = @id_pai";
        string IAtendOdontoCommand.GetAtendOdontoItensByPai { get => sqlGetAtendOdontoItensByPai; }

        public string sqlInsertupdate = $@"UPDATE OR INSERT INTO ESUS_ATEND_ODONT_INDIVIDUAL (ID, ID_PROFISSIONAL, ID_UNIDADE, TURNO, DATA, ID_CIDADAO, ID_LOCAL_ATENDIMENTO,
                                                                                    ID_TIPO_ATENDIMENTO, ID_TIPO_CONSULTA, FLG_VIG_ABSCESSO_DENTO,
                                                                                    FLG_VIG_ALTERACAO_TECIDOS, FLG_VIG_DOR_DENTE, FLG_VIG_FENDAS_FISSURAS,
                                                                                    FLG_VIG_FLUOROSE_DENTARIA, FLG_VIG_TRAUMALISMO, FLG_VIG_NAO_IDENTIFICADO,
                                                                                    ID_FORNECIMENTO, FLG_CONDUTA_RET_CONSULTA, FLG_CONDUTA_OUTROS_PROF,
                                                                                    FLG_CONDUTA_AGENDA_NASF, FLG_CONDUTA_AGENDA_GRUPO, FLG_TRATAMENTO_CONCLUIDO,
                                                                                    FLG_ENC_CONDUTA_NEC_ESPECIAIS, FLG_ENC_CONDUTA_CIRUR_BMF,
                                                                                    FLG_ENC_CONDUTA_ENDODONTIA, FLG_ENC_CONDUTA_ESTOMATOLOGIA,
                                                                                    FLG_ENC_CONDUTA_IMPLANTODONTIA, FLG_ENC_CONDUTA_ODONTOPEDIATRIA,
                                                                                    FLG_ENC_CONDUTA_ORTODONTIA, FLG_ENC_CONDUTA_PERIODONTIA,
                                                                                    FLG_ENC_CONDUTA_PROT_DENTARIA, FLG_ENC_CONDUTA_RADIOLOGIA,
                                                                                    FLG_ENC_CONDUTA_OUTROS, FLG_FOR_ESCOVA_DENTAL, FLG_FOR_CREME_DENTAL,
                                                                                    FLG_FOR_FIO_DENTAL, ID_PROFISSIONAL2, ID_PROFISSIONAL3, FLG_ATEND_GESTANTE,
                                                                                    FLG_ATEND_NESCECIDADE_ESPECIAL, FLG_CONDUTA_ALTA_EPSODIO, ID_CBO, UUID,
                                                                                    ID_USUARIO, DATA_FIM_ATENDIMENTO, ID_EQUIPE)
                                           VALUES (@id, @id_profissional, @id_unidade, @turno, @data, @id_cidadao, @id_local_atendimento, @id_tipo_atendimento,
                                                   @id_tipo_consulta, @flg_vig_abscesso_dento, @flg_vig_alteracao_tecidos, @flg_vig_dor_dente, @flg_vig_fendas_fissuras,
                                                   @flg_vig_fluorose_dentaria, @flg_vig_traumalismo, @flg_vig_nao_identificado, @id_fornecimento,
                                                   @flg_conduta_ret_consulta, @flg_conduta_outros_prof, @flg_conduta_agenda_nasf, @flg_conduta_agenda_grupo,
                                                   @flg_tratamento_concluido, @flg_enc_conduta_nec_especiais, @flg_enc_conduta_cirur_bmf, @flg_enc_conduta_endodontia,
                                                   @flg_enc_conduta_estomatologia, @flg_enc_conduta_implantodontia, @flg_enc_conduta_odontopediatria,
                                                   @flg_enc_conduta_ortodontia, @flg_enc_conduta_periodontia, @flg_enc_conduta_prot_dentaria,
                                                   @flg_enc_conduta_radiologia, @flg_enc_conduta_outros, @flg_for_escova_dental, @flg_for_creme_dental,
                                                   @flg_for_fio_dental, @id_profissional2, @id_profissional3, @flg_atend_gestante, @flg_atend_nescecidade_especial,
                                                   @flg_conduta_alta_epsodio, @id_cbo, @uuid, @id_usuario, @data_fim_atendimento, @id_equipe)";
        string IAtendOdontoCommand.InsertOrUpdate { get => sqlInsertupdate; }

        public string sqlInsertOrUpdateItens = $@"UPDATE OR INSERT INTO ESUS_IATEND_ODONT_INDIVIDUAL (ID, ID_ATEND_ODONT, ID_PROCEDIMENTO, QUANTIDADE_PROCEDIMENTO,
                                                                                                      ID_PRODUCAO , UUID,
                                                                                                      ID_ESUS_EXPORTACAO_ITEM, ID_ATEND_ODONT_REALIZADO, ID_DENTE,
                                                                                                      ID_LOCAL_PROCEDIMENTO_ODONTO, ID_LISTA_REGIAO_PROCEDIMENTO,
                                                                                                      ID_CLASSE, ID_ATEND_PRONTUARIO, ID_ATEND_PRONTUARIO_REALIZADO,
                                                                                                      OBSERVACAO, DATA_REALIZAR, DATA_REALIZADO,
                                                                                                      ID_CONTROLE_SINCRONIZACAO_LOTE)
                                                  VALUES (@id, @id_atend_odont, @id_procedimento, @quantidade_procedimento, @id_producao,
                                                          @uuid, @id_esus_exportacao_item, @id_atend_odont_realizado, @id_dente, @id_local_procedimento_odonto,
                                                          @id_lista_regiao_procedimento, @id_classe, @id_atend_prontuario, @id_atend_prontuario_realizado, @observacao,
                                                          @data_realizar, @data_realizado, @id_controle_sincronizacao_lote)";
        string IAtendOdontoCommand.InsertOrUpdateItens { get => sqlInsertOrUpdateItens; }

        public string sqlGetNewId = $@"SELECT GEN_ID(GEN_ESUS_ATEND_ODONT, 1) AS VLR FROM RDB$DATABASE";
        string IAtendOdontoCommand.GetNewId { get => sqlGetNewId; }

        public string sqlGetNewIdItem = $@"SELECT GEN_ID(GEN_ESUS_IATEND_ODONT, 1) AS VLR FROM RDB$DATABASE";
        string IAtendOdontoCommand.GetNewIdItem { get => sqlGetNewIdItem; }

        public string sqlExcluirItemPai = $@"DELETE FROM ESUS_ATEND_ODONT_INDIVIDUAL
                                             WHERE ID = @id";
        string IAtendOdontoCommand.ExcluirItemPai { get => sqlExcluirItemPai; }

        public string sqlExcluirItensByPai = $@"DELETE FROM ESUS_IATEND_ODONT_INDIVIDUAL
                                                WHERE ID_ATEND_ODONT = @id";
        string IAtendOdontoCommand.ExcluirItensByPai { get => sqlExcluirItensByPai; }

        public string sqlExcluirItemById = $@"DELETE FROM ESUS_IATEND_ODONT_INDIVIDUAL
                                              WHERE ID = @id";
        string IAtendOdontoCommand.ExcluirItemById { get => sqlExcluirItemById; }

        public string sqlGetAtendOdontoItemById = $@"SELECT * FROM ESUS_IATEND_ODONT_INDIVIDUAL
                                                     WHERE ID = @id";
        string IAtendOdontoCommand.GetAtendOdontoItemById { get => sqlGetAtendOdontoById; }

        public string sqlGetProcOdontoIndividualizado = $@"SELECT FIRST (@pagesize) SKIP (@page) TAB.CSI_CODPROC, TAB.NOME, TAB.REGISTRO
                                                           FROM (SELECT DISTINCT P.CODIGO CSI_CODPROC, P.NOME, PR.COD_PADRAO_REGISTRO_BPA AS REGISTRO
                                                                FROM TSI_PROCEDIMENTO P
                                                                JOIN TSI_PROCEDIMENTO_CBO CBO ON CBO.COD_PROCEDIMENTO = P.CODIGO
                                                                JOIN TSI_PROCEDIMENTO_REGISTRO PR ON PR.COD_PROCEDIMENTO = CBO.COD_PROCEDIMENTO AND PR.ID_COMPETENCIA = CBO.ID_COMPETENCIA AND PR.COD_PADRAO_REGISTRO_BPA IN ('01', '02', '10')
                                                                JOIN COMPETENCIA_SIGTAP CS ON CS.ID = P.ID_COMPETENCIA
                                                                WHERE CBO.COD_CBO = @cbo AND
                                                                      CS.ATIVO = 'T' AND
                                                                      ((P.CODIGO LIKE '01%') OR (P.CODIGO LIKE '02%') OR (P.CODIGO LIKE '03%') OR (P.CODIGO LIKE '04%') OR P.CODIGO IN (0701070064, 0701070072, 0701070080, 0701070099, 0701070102, 0701070110, 0701070129, 0701070137, 0701070056, 0701070145)) AND
                                                                      P.CODIGO NOT IN (0301040079)
                                                                UNION
                                                                SELECT DISTINCT P.CODIGO CSI_CODPROC, P.NOME, PR.COD_PADRAO_REGISTRO_BPA AS REGISTRO
                                                                FROM TSI_PROCEDIMENTO P
                                                                JOIN COMPETENCIA_SIGTAP CS ON CS.ID = P.ID_COMPETENCIA
                                                                JOIN TSI_PROCEDIMENTO_REGISTRO PR ON PR.COD_PROCEDIMENTO = P.CODIGO AND PR.ID_COMPETENCIA = CS.ID AND PR.COD_PADRAO_REGISTRO_BPA IN ('01', '02', '10')
                                                                WHERE P.CODIGO NOT IN (SELECT DISTINCT CBO.COD_PROCEDIMENTO
                                                                                       FROM TSI_PROCEDIMENTO_CBO CBO
                                                                                       JOIN COMPETENCIA_SIGTAP CSS ON CSS.ID = CBO.ID_COMPETENCIA
                                                                                       WHERE CSS.ATIVO = 'T') AND
                                                                      CS.ATIVO = 'T' AND
                                                                      ((P.CODIGO LIKE '01%') OR (P.CODIGO LIKE '02%') OR (P.CODIGO LIKE '03%') OR (P.CODIGO LIKE '04%') OR P.CODIGO IN (0701070064, 0701070072, 0701070080, 0701070099, 0701070102, 0701070110, 0701070129, 0701070137, 0701070056, 0701070145)) AND
                                                                      P.CODIGO NOT IN (0301040079)
                                                                UNION
                                                                SELECT DISTINCT E.CSI_CODSUS AS CSI_CODPROC, E.CSI_NOME AS NOME, E.SIGLA_ESUS
                                                                FROM TSI_CADEXAMES E
                                                                JOIN TSI_PROCEDIMENTO P ON P.CODIGO = E.CSI_CODSUS
                                                                JOIN COMPETENCIA_SIGTAP CS ON CS.ID = P.ID_COMPETENCIA
                                                                JOIN TSI_PROCEDIMENTO_REGISTRO PR ON PR.COD_PROCEDIMENTO = P.CODIGO AND PR.ID_COMPETENCIA = CS.ID AND PR.COD_PADRAO_REGISTRO_BPA IN ('01', '02', '10')
                                                                JOIN TSI_PROCEDIMENTO_CBO CBO ON CBO.COD_PROCEDIMENTO = P.CODIGO AND CBO.ID_COMPETENCIA = PR.ID_COMPETENCIA
                                                                WHERE E.SIGLA_ESUS IS NOT NULL AND
                                                                      CBO.COD_CBO = @cbo AND
                                                                      CS.ATIVO = 'T' AND
                                                                      ((E.CSI_CODSUS LIKE '01%') OR (E.CSI_CODSUS LIKE '02%') OR (E.CSI_CODSUS LIKE '03%') OR (E.CSI_CODSUS LIKE '04%') OR E.CSI_CODSUS IN (0701070064, 0701070072, 0701070080, 0701070099, 0701070102, 0701070110, 0701070129, 0701070137, 0701070056, 0701070145)) AND
                                                                      E.CSI_CODSUS NOT IN (0301040079)
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
                                                                      CS.ATIVO = 'T' AND
                                                                      ((E.CSI_CODSUS LIKE '01%') OR (E.CSI_CODSUS LIKE '02%') OR (E.CSI_CODSUS LIKE '03%') OR (E.CSI_CODSUS LIKE '04%') OR E.CSI_CODSUS IN (0701070064, 0701070072, 0701070080, 0701070099, 0701070102, 0701070110, 0701070129, 0701070137, 0701070056, 0701070145)) AND
                                                                      E.CSI_CODSUS NOT IN (0301040079)
                                                                UNION
                                                                SELECT DISTINCT E.CSI_CODSUS AS CSI_CODPROC, E.CSI_NOME AS NOME, E.SIGLA_ESUS
                                                                FROM TSI_CADEXAMES E
                                                                JOIN TSI_PROCEDIMENTO P ON P.CODIGO = E.CSI_CODSUS
                                                                WHERE E.SIGLA_ESUS IS NOT NULL AND
                                                                      ((E.CSI_CODSUS LIKE '01%') OR (E.CSI_CODSUS LIKE '02%') OR (E.CSI_CODSUS LIKE '03%') OR (E.CSI_CODSUS LIKE '04%') OR E.CSI_CODSUS IN (0701070064, 0701070072, 0701070080, 0701070099, 0701070102, 0701070110, 0701070129, 0701070137, 0701070056, 0701070145)) AND
                                                                      E.CSI_CODSUS NOT IN (0301040079)) TAB
                                                           @filtro
                                                           ORDER BY NOME";
        string IAtendOdontoCommand.GetProcOdontoIndividualizado { get => sqlGetProcOdontoIndividualizado; }

        public string sqlGetCountProcOdontoIndividualizado = $@"SELECT COUNT(*) 
                                                                FROM (SELECT DISTINCT P.CODIGO CSI_CODPROC, P.NOME, PR.COD_PADRAO_REGISTRO_BPA AS REGISTRO
                                                                     FROM TSI_PROCEDIMENTO P
                                                                     JOIN TSI_PROCEDIMENTO_CBO CBO ON CBO.COD_PROCEDIMENTO = P.CODIGO
                                                                     JOIN TSI_PROCEDIMENTO_REGISTRO PR ON PR.COD_PROCEDIMENTO = CBO.COD_PROCEDIMENTO AND PR.ID_COMPETENCIA = CBO.ID_COMPETENCIA AND PR.COD_PADRAO_REGISTRO_BPA IN ('01', '02', '10')
                                                                     JOIN COMPETENCIA_SIGTAP CS ON CS.ID = P.ID_COMPETENCIA
                                                                     WHERE CBO.COD_CBO = @cbo AND
                                                                           CS.ATIVO = 'T' AND
                                                                           ((P.CODIGO LIKE '01%') OR (P.CODIGO LIKE '02%') OR (P.CODIGO LIKE '03%') OR (P.CODIGO LIKE '04%') OR P.CODIGO IN (0701070064, 0701070072, 0701070080, 0701070099, 0701070102, 0701070110, 0701070129, 0701070137, 0701070056, 0701070145)) AND
                                                                           P.CODIGO NOT IN (0301040079)
                                                                     UNION
                                                                     SELECT DISTINCT P.CODIGO CSI_CODPROC, P.NOME, PR.COD_PADRAO_REGISTRO_BPA AS REGISTRO
                                                                     FROM TSI_PROCEDIMENTO P
                                                                     JOIN COMPETENCIA_SIGTAP CS ON CS.ID = P.ID_COMPETENCIA
                                                                     JOIN TSI_PROCEDIMENTO_REGISTRO PR ON PR.COD_PROCEDIMENTO = P.CODIGO AND PR.ID_COMPETENCIA = CS.ID AND PR.COD_PADRAO_REGISTRO_BPA IN ('01', '02', '10')
                                                                     WHERE P.CODIGO NOT IN (SELECT DISTINCT CBO.COD_PROCEDIMENTO
                                                                                            FROM TSI_PROCEDIMENTO_CBO CBO
                                                                                            JOIN COMPETENCIA_SIGTAP CSS ON CSS.ID = CBO.ID_COMPETENCIA
                                                                                            WHERE CSS.ATIVO = 'T') AND
                                                                           CS.ATIVO = 'T' AND
                                                                           ((P.CODIGO LIKE '01%') OR (P.CODIGO LIKE '02%') OR (P.CODIGO LIKE '03%') OR (P.CODIGO LIKE '04%') OR P.CODIGO IN (0701070064, 0701070072, 0701070080, 0701070099, 0701070102, 0701070110, 0701070129, 0701070137, 0701070056, 0701070145)) AND
                                                                           P.CODIGO NOT IN (0301040079)
                                                                     UNION
                                                                     SELECT DISTINCT E.CSI_CODSUS AS CSI_CODPROC, E.CSI_NOME AS NOME, E.SIGLA_ESUS
                                                                     FROM TSI_CADEXAMES E
                                                                     JOIN TSI_PROCEDIMENTO P ON P.CODIGO = E.CSI_CODSUS
                                                                     JOIN COMPETENCIA_SIGTAP CS ON CS.ID = P.ID_COMPETENCIA
                                                                     JOIN TSI_PROCEDIMENTO_REGISTRO PR ON PR.COD_PROCEDIMENTO = P.CODIGO AND PR.ID_COMPETENCIA = CS.ID AND PR.COD_PADRAO_REGISTRO_BPA IN ('01', '02', '10')
                                                                     JOIN TSI_PROCEDIMENTO_CBO CBO ON CBO.COD_PROCEDIMENTO = P.CODIGO AND CBO.ID_COMPETENCIA = PR.ID_COMPETENCIA
                                                                     WHERE E.SIGLA_ESUS IS NOT NULL AND
                                                                           CBO.COD_CBO = @cbo AND
                                                                           CS.ATIVO = 'T' AND
                                                                           ((E.CSI_CODSUS LIKE '01%') OR (E.CSI_CODSUS LIKE '02%') OR (E.CSI_CODSUS LIKE '03%') OR (E.CSI_CODSUS LIKE '04%') OR E.CSI_CODSUS IN (0701070064, 0701070072, 0701070080, 0701070099, 0701070102, 0701070110, 0701070129, 0701070137, 0701070056, 0701070145)) AND
                                                                           E.CSI_CODSUS NOT IN (0301040079)
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
                                                                           CS.ATIVO = 'T' AND
                                                                           ((E.CSI_CODSUS LIKE '01%') OR (E.CSI_CODSUS LIKE '02%') OR (E.CSI_CODSUS LIKE '03%') OR (E.CSI_CODSUS LIKE '04%') OR E.CSI_CODSUS IN (0701070064, 0701070072, 0701070080, 0701070099, 0701070102, 0701070110, 0701070129, 0701070137, 0701070056, 0701070145)) AND
                                                                           E.CSI_CODSUS NOT IN (0301040079)
                                                                     UNION
                                                                     SELECT DISTINCT E.CSI_CODSUS AS CSI_CODPROC, E.CSI_NOME AS NOME, E.SIGLA_ESUS
                                                                     FROM TSI_CADEXAMES E
                                                                     JOIN TSI_PROCEDIMENTO P ON P.CODIGO = E.CSI_CODSUS
                                                                     WHERE E.SIGLA_ESUS IS NOT NULL AND
                                                                           ((E.CSI_CODSUS LIKE '01%') OR (E.CSI_CODSUS LIKE '02%') OR (E.CSI_CODSUS LIKE '03%') OR (E.CSI_CODSUS LIKE '04%') OR E.CSI_CODSUS IN (0701070064, 0701070072, 0701070080, 0701070099, 0701070102, 0701070110, 0701070129, 0701070137, 0701070056, 0701070145)) AND
                                                                           E.CSI_CODSUS NOT IN (0301040079)) TAB
                                                                @filtro";
        string IAtendOdontoCommand.GetCountProcOdontoIndividualizado { get => sqlGetCountProcOdontoIndividualizado; }
    }
}
