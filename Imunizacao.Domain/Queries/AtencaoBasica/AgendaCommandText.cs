using RgCidadao.Domain.Commands.AtencaoBasica;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Queries.AtencaoBasica
{
    public class AgendaCommandText : IAgendaCommand
    {
        public string sqlGetAllPagination = $@"SELECT C.CSI_CONTROLE, C.CSI_DATAAG, C.CSI_HORARIO, C.CSI_CODPAC, P.CSI_NOMPAC,
                                                      (
                                                      CASE
                                                        WHEN C.ID_TIPO_ATENDIMENTO = 1 THEN 'Consula Agendada Programada Cuidado Contínuo'
                                                        WHEN C.ID_TIPO_ATENDIMENTO = 2 THEN 'Consulta Agendada'
                                                        WHEN C.ID_TIPO_ATENDIMENTO = 4 THEN 'Escuta Inicial ou Orientação'
                                                        WHEN C.ID_TIPO_ATENDIMENTO = 5 THEN 'Consulta no Dia'
                                                        WHEN C.ID_TIPO_ATENDIMENTO = 6 THEN 'Atendimento de Urgência'
                                                      END) AS CSI_TIPO_CONSULTA,
                                                      NAT.CSI_DESCNATATEND, C.CSI_STATUS, C.CSI_OBS, C.CSI_OBS_CANCELAMENTO, C.CSI_CODPONTO, DM.ID,
                                                      CAST(DM.CSI_HORARIO AS TIME) HORARIOINI, CAST(DM.CSI_HORARIOFINAL AS TIME) HORARIOFINAL,
                                                      DM.CSI_INTERVALO_AGENDAMENTO, DM.CSI_QTDECON, C.ID_DIASMED, C.CSI_DATACONF, C.CSI_ORDEM,
                                                      (
                                                      CASE
                                                        WHEN EF.CODIGO_CONSULTA IS NULL THEN 0
                                                        ELSE 1
                                                      END) AS TRIADO,
                                                      C.CSI_CODNATATEND, C.CSI_DATACON, C.CSI_DATAAG, DM.CSI_CBO, C.CSI_ID_LIBEXAMES, DM.CSI_FORMA_FATURAMENTO,
                                                      DM.CSI_FORMA_AGENDAMENTO, DM.CSI_ID_PRESTADOR, F.CSI_ID_PRESTADOR_CONSORCIO, PR.NOME AS NOME_PROCEDIMENTO
                                               FROM TSI_CONSULTAS C
                                               JOIN TSI_DIASMED DM ON (C.ID_DIASMED = DM.ID)
                                               JOIN TSI_CADPAC P ON (C.CSI_CODPAC = P.CSI_CODPAC)
                                               LEFT JOIN TSI_PROCEDIMENTO PR ON (DM.CSI_PROCEDIMENTO = PR.CODIGO)
                                               LEFT JOIN PEP_EXAME_FISICO EF ON (EF.CODIGO_CONSULTA = C.CSI_CONTROLE)
                                               LEFT JOIN TSI_NATATEND NAT ON NAT.CSI_CODNATATEND = C.CSI_CODNATATEND
                                               LEFT JOIN TSI_CADFOR F ON F.CSI_CODFOR = DM.CSI_ID_PRESTADOR
                                               WHERE DM.ID = @id_dia_med
                                               UNION
                                               SELECT NULL CSI_CONTROLE, NULL CSI_DATAAG, CI.HORARIO CSI_HORARIO, NULL CSI_CODPAC,
                                                      CASE WHEN COALESCE(CI.FLG_RESERVADO, 'F') <> 'T' THEN 'HORÁRIO VAGO'
                                                      ELSE 'RESERVADO'
                                                      END CSI_NOMPAC, NULL CSI_TIPO_CONSULTA,
                                                      NULL CSI_DESCNATATEND, NULL CSI_STATUS, NULL CSI_OBS, NULL CSI_OBS_CANCELAMENTO, NULL CSI_CODPONTO, DM.ID,
                                                      CAST(DM.CSI_HORARIO AS TIME) HORARIOINI, CAST(DM.CSI_HORARIOFINAL AS TIME) HORARIOFINAL,
                                                      DM.CSI_INTERVALO_AGENDAMENTO, DM.CSI_QTDECON, DM.ID ID_DIASMED, NULL CSI_DATACONF, CI.ORDEM CSI_ORDEM,
                                                      NULL TRIADO, NULL CSI_CODNATATEND, NULL CSI_DATACON, NULL CSI_DATAAG, DM.CSI_CBO, NULL CSI_ID_LIBEXAMES,
                                                      DM.CSI_FORMA_FATURAMENTO, DM.CSI_FORMA_AGENDAMENTO, DM.CSI_ID_PRESTADOR, F.CSI_ID_PRESTADOR_CONSORCIO,
                                                      PR.NOME AS NOME_PROCEDIMENTO
                                               FROM TSI_CONSULTAS_ITEM CI
                                               JOIN TSI_DIASMED DM ON DM.ID = CI.ID_DIASMED
                                               LEFT JOIN TSI_CADFOR F ON F.CSI_CODFOR = DM.CSI_ID_PRESTADOR
                                               LEFT JOIN TSI_PROCEDIMENTO PR ON (DM.CSI_PROCEDIMENTO = PR.CODIGO)
                                               WHERE CI.ORDEM NOT IN (SELECT C.CSI_ORDEM
                                                                      FROM TSI_CONSULTAS C
                                                                      WHERE C.ID_DIASMED = CI.ID_DIASMED AND
                                                                            C.CSI_STATUS != 'Cancelada') AND
                                                     DM.ID = @id_dia_med";
        string IAgendaCommand.GetAllPagination { get => sqlGetAllPagination; }

        public string sqlCountGetAll = $@"SELECT COUNT(*) FROM (
                                          SELECT C.CSI_CONTROLE, C.CSI_DATAAG, C.CSI_HORARIO,
                                                  C.CSI_CODPAC, P.CSI_NOMPAC,
                                          (CASE
                                          WHEN C.ID_TIPO_ATENDIMENTO = 1 then 'Consula Agendada Programada Cuidado Contínuo'
                                          WHEN C.ID_TIPO_ATENDIMENTO = 2 then 'Consulta Agendada'
                                          WHEN C.ID_TIPO_ATENDIMENTO = 4 then 'Escuta Inicial ou Orientação'
                                          WHEN C.ID_TIPO_ATENDIMENTO = 5 then 'Consulta no Dia'
                                          WHEN C.ID_TIPO_ATENDIMENTO = 6 then 'Atendimento de Urgência'
                                          END) AS CSI_TIPO_CONSULTA, NAT.CSI_DESCNATATEND,
                                                  C.CSI_STATUS, C.CSI_OBS, C.CSI_OBS_CANCELAMENTO, C.CSI_CODPONTO,
                                               DM.ID, CAST(DM.CSI_HORARIO AS TIME) HORARIOINI,
                                               CAST(DM.CSI_HORARIOFINAL AS TIME) HORARIOFINAL,
                                               DM.CSI_INTERVALO_AGENDAMENTO, DM.CSI_QTDECON, C.ID_DIASMED,
                                               C.CSI_DATACONF, C.CSI_ORDEM,
                                               (CASE WHEN EF.CODIGO_CONSULTA IS NULL THEN 0 ELSE 1 END) AS TRIADO,
                                               C.CSI_CODNATATEND,C.CSI_DATACON,C.CSI_DATAAG, DM.CSI_CBO,C.CSI_ID_LIBEXAMES,
                                               DM.CSI_FORMA_FATURAMENTO, DM.CSI_FORMA_AGENDAMENTO,DM.CSI_ID_PRESTADOR,
                                               F.CSI_ID_PRESTADOR_CONSORCIO, PR.NOME as NOME_PROCEDIMENTO
                                           FROM TSI_CONSULTAS C
                                           JOIN TSI_DIASMED DM ON (C.ID_DIASMED = DM.ID)
                                           JOIN TSI_CADPAC P ON (C.CSI_CODPAC = P.CSI_CODPAC)
                                           LEFT JOIN TSI_PROCEDIMENTO PR ON (DM.CSI_PROCEDIMENTO = PR.CODIGO)
                                           LEFT JOIN PEP_EXAME_FISICO EF ON (EF.CODIGO_CONSULTA = C.CSI_CONTROLE)
                                           LEFT JOIN TSI_NATATEND NAT ON NAT.CSI_CODNATATEND = C.CSI_CODNATATEND
                                           LEFT JOIN TSI_CADFOR F ON F.CSI_CODFOR = DM.CSI_ID_PRESTADOR
                                           WHERE DM.ID = @id_dia_med)";
        string IAgendaCommand.GetCountAll { get => sqlGetAllPagination; }

        public string sqlDiasMedByData = $@"SELECT ID ID_DIAS_MED, CSI_HORARIO || ' - ' || CSI_HORARIOFINAL HORARIO FROM TSI_DIASMED
                                            WHERE CSI_DATA = @data AND
                                                  CSI_CODMED = @id_profissional";
        string IAgendaCommand.DiasMedByData { get => sqlDiasMedByData; }

        public string sqlGetAgendaDias = $@"SELECT DATA, TIPO, SUM(VAGAS) VAGAS
                                            FROM (SELECT DISTINCT CAST(DM.CSI_DATA AS DATE) DATA, 1 TIPO,
                                                                  (SELECT COUNT(*)
                                                                   FROM TSI_CONSULTAS_ITEM CI
                                                                   WHERE CI.ORDEM NOT IN (SELECT C.CSI_ORDEM
                                                                                          FROM TSI_CONSULTAS C
                                                                                          WHERE C.ID_DIASMED = CI.ID_DIASMED AND
                                                                                                C.CSI_STATUS != 'CANCELADA') AND
                                                                         CI.ID_DIASMED = DM.ID) VAGAS
                                                  FROM TSI_DIASMED DM
                                                  WHERE DM.CSI_CODMED = @id_profissional AND
                                                        CAST(DM.CSI_DATA AS DATE) BETWEEN @data_inicial AND @data_final
                                                  ORDER BY DM.CSI_DATA)
                                            GROUP BY DATA, TIPO";
        string IAgendaCommand.GetAgendaDias { get => sqlGetAgendaDias; }

        public string sqlExcluiDiasMed = $@"DELETE FROM TSI_DIASMED
                                            WHERE ID = @id_dias_med";
        string IAgendaCommand.ExcluiDiasMed { get => sqlExcluiDiasMed; }

        #region Configuração de Agenda
        public string sqlGetConfiguracaoProjetoByData = $@"SELECT DM.ID,U.CSI_NOMUNI UNIDADE, MC.CSI_CBO,  CAST(DM.CSI_HORARIO AS TIME) HORARIOINI,
                                                                  CAST(DM.CSI_HORARIOFINAL AS TIME) HORARIOFINAL,
                                                                  DM.CSI_QTDECON, DM.CSI_INTERVALO_AGENDAMENTO
                                                           FROM TSI_CONSULTAS_ITEM CI
                                                           JOIN TSI_DIASMED DM ON DM.ID = CI.ID_DIASMED
                                                           JOIN TSI_PONTOS P ON P.CSI_CODPONTO = DM.ID
                                                           JOIN TSI_UNIDADE U ON U.CSI_CODUNI = P.ID_LOCAL_ATENDIMENTO
                                                           JOIN TSI_MEDICOS_CBO MC ON MC.CSI_CODMED = DM.CSI_CODMED
                                                           WHERE DM.CSI_CODMED = @csi_codmed AND
                                                                 DM.CSI_DATA = @csi_data";
        string IAgendaCommand.GetConfiguracaoProjetoByData { get => sqlGetConfiguracaoProjetoByData; }

        public string sqlGetConfigProjetoById = $@"SELECT * FROM TSI_DIASMED
                                                   WHERE ID = @id";
        string IAgendaCommand.GetConfigProjetoById { get => sqlGetConfigProjetoById; }

        public string sqlGravarAgendaDiasMed = $@"UPDATE OR INSERT INTO TSI_DIASMED(CSI_CODMED,CSI_DATA,CSI_HORARIO,CSI_QTDECON,CSI_COPDPONTO,CSI_PROCEDIMENTO,CSI_CBO,
                                                                         CSI_FORMA_AGENDAMENTO,CSI_INTERVALO_AGENDAMENTO,CSI_FORMA_FATURAMENTO, CSI_ID_PRESTADOR,CSI_DATA_CRIACAO,
                                                                         ID,CSI_HORARIOFINAL,ID_GRUPO_PROCEDIMENTO_COTA,ID_CONTROLE_SINCRONIZACAO_LOTE, UUID)
                                                  VALUES(@csi_codmed,@csi_data,@csi_horario,@csi_qtdecon,@csi_copdponto,@csi_procedimento,@csi_cbo,
                                                         @csi_forma_agendamento,@csi_intervalo_agendamento,@csi_forma_faturamento, @csi_id_prestador,@csi_data_criacao,
                                                         @id,csi_horariofinal,@id_grupo_procedimento_cota,@id_controle_sincronizacao_lote, @uuid)";
        string IAgendaCommand.GravarAgendaDiasMed { get => sqlGravarAgendaDiasMed; }

        public string sqlGetNewIdDiasMed = $@"SELECT GEN_ID(GEN_TSI_DIASMED_ID, 1) AS VLR FROM RDB$DATABASE";
        string IAgendaCommand.GetNewIdDiasMed { get => sqlGetNewIdDiasMed; }

        public string sqlInserirConsultasItens = $@"INSERT INTO TSI_CONSULTAS_ITEM (ID_DIASMED, ID_CONSULTAS, HORARIO, ORDEM, FLG_RESERVADO) 
                                                    VALUES (@id_diasmed, Null, @horario, @ordem, @reservado)";
        string IAgendaCommand.InserirConsultasItens { get => sqlInserirConsultasItens; }

        public string sqlExcluirConsultasItemByDiasMed = $@"DELETE FROM TSI_CONSULTAS_ITEM CI WHERE CI.ID_DIASMED = @id_dias_med";
        string IAgendaCommand.ExcluirConsultasItemByDiasMed { get => sqlExcluirConsultasItemByDiasMed; }

        public string sqlGetConsultaItemByDiasMed = $@"SELECT * TSI_CONSULTAS_ITEM CI
                                                       WHERE CI.ID_DIASMED = @id_dias_med";
        string IAgendaCommand.GetConsultaItemByDiasMed { get => sqlGetConsultaItemByDiasMed; }

        public string sqlGetNextOrdemConsultaItem = $@"SELECT FIRST(1) CI.ORDEM FROM TSI_CONSULTAS_ITEM CI
                                                       WHERE CI.ORDEM NOT IN (SELECT C.CSI_ORDEM FROM TSI_CONSULTAS C
                                                                               WHERE C.ID_DIASMED = CI.ID_DIASMED AND
                                                                                     C.CSI_STATUS != 'Cancelada' ) AND
                                                             CI.ID_DIASMED = @id_diasmed
                                                       ORDER BY CI.ORDEM";
        string IAgendaCommand.GetNextOrdemConsultaItem { get => sqlGetNextOrdemConsultaItem; }

        public string sqlGetItensByDiasMedOrdem = $@"SELECT CI.* FROM TSI_CONSULTAS_ITEM CI
                                                 WHERE CI.ID_DIASMED = @id_diasmed AND
                                                       CI.ORDEM = @ordem ";
        string IAgendaCommand.GetItensByDiasMedOrdem { get => sqlGetItensByDiasMedOrdem; }

        public string sqlCancelaReservaConsultaItem = $@"UPDATE TSI_CONSULTAS_ITEM SET FLG_RESERVADO = 'F'
                                                         WHERE (ID_DIASMED = @id_diasmed  AND
                                                                ORDEM = @ordem)";
        string IAgendaCommand.CancelaReservaConsultaItem { get => sqlCancelaReservaConsultaItem; }

        #endregion

        #region Agendamento de Consultas
        public string sqlInserirConsulta = $@"UPDATE OR INSERT TSI_CONSULTAS INTO(CSI_CONTROLE, CSI_DATAAG, CSI_DATACON, CSI_HORARIO, CSI_NOMUSU, CSI_CODMED, CSI_CODPONTO,
                                                                        CSI_CODPAC, CSI_ORDEM, CSI_SUPLENTE, CSI_DATACONF, CSI_NOMUSUCONF, CSI_DIAGNOSTICO,
                                                                        CSI_CODNATATEND, CSI_CODPACACOMP, CSI_NOMEACOMP, CSI_PARENTESCOACOMP, CSI_DATANACACOMP,
                                                                        CSI_DOCIDEACOMP, CSI_ENDTRABACOMP, CSI_CODCIDACOMP, CSI_CEPACOMP, CSI_DIAGPROVAVEL,
                                                                        CSI_ANAMNESE, CSI_REPATENDIMENTO, CSI_QTDREPATEND, CSI_DATAALTA, CSI_CODTIPOALTA,
                                                                        CSI_CODLEITO, CSI_NPRENATAL, CSI_CODESTNUT, CSI_STATUS, CSI_CODCON, CSI_ALTA, CSI_OBSALTA,
                                                                        CSI_MODELO, CSI_MODELOPUBLICO, CSI_NOMEMODELO, CSI_PESO, CSI_IMC, CSI_ALTURA, CSI_DIETAOBS,
                                                                        CSI_ORIENTNUTRI, CSI_CBO, CSI_UNIDADE_AGENDAMENTO, CSI_HISTORICO_EVOLUTIVO,
                                                                        CSI_ID_FILA_ESPERA, CSI_TIPO_CONSULTA, CSI_MEDIDA_CINTURA, CSI_RESTRICAO_MODELO,
                                                                        CSI_MODELO_RESTRITO_PAC, CSI_HIPERTENCAO, CSI_DIABETES, CSI_PRESSAO_ART_SISTOLICA,
                                                                        CSI_PRESSAO_ART_DIASTOLICA, CSI_GLICEMIA, CSI_TIPO_GLICEMIA, CSI_SEM_COMPLICACOES, CSI_ANGINA,
                                                                        CSI_IAM, CSI_AVC, CSI_PRE_DIABETICO, CSI_AMPUTACAO_DIABETE, CSI_DOENCA_RENAL, CSI_RETINOPATIA,
                                                                        CSI_HB_GLICOSILADA, CSI_CREATININA_SERICA, CSI_COLESTEROL_TOTAL, CSI_ECG, CSI_TRIGLICERIDES,
                                                                        CSI_URINA_TIPO1, CSI_MICROALBUMINURIA, CSI_QTDE_HIDROCLOROTIAZIDA, CSI_QTDE_PROPRANOLOL,
                                                                        CSI_QTDE_CAPTOPRIL, CSI_QTDE_GLIBENCLAMIDA, CSI_QTDE_METFORMINA, CSI_QTDE_INSULINA,
                                                                        CSI_ANT_FAM_CARDIO_VASCULAR, CSI_DIABETE_TIPO1, CSI_DIABETE_TIPO2, CSI_TABAGISMO,
                                                                        CSI_SEDENTARISMO, CSI_SOBREPESO, CSI_MEDICAMENTOSO, CSI_OUTROS_MEDICAMENTOS,
                                                                        CSI_OUTRAS_CORONARIOPATIAS, CSI_TIPO_RISCO, CSI_STATUS_HIPERDIA, CSI_OUTROS_MEDICAMENTOS_OBS,
                                                                        CSI_ID_LIBEXAMES, CSI_CODEXA_PADRAO, ID_DIASMED, ID_ATENDIMENTO_INDIVIDUAL,
                                                                        ID_TIPO_ATENDIMENTO, ID_ATENDIMENTO_ODONTOLOGICO, ID_CID, ID_ORGAO_PUBLICO, CSI_DATA_CANCELOU,
                                                                        CSI_NOMUSU_CANCELOU, CSI_OBS, CSI_UNIDADE_PACIENTE, UUID, ID_TIPO_CONSULTA, ID_SENHA,
                                                                        ID_PEP_EXAME_FISICO, SMS_ENVIADO, CSI_OBS_CANCELAMENTO, ID_CONTROLE_SINCRONIZACAO_LOTE,
                                                                        ID_EQUIPE)
                                              VALUES (@csi_controle, @csi_dataag, @csi_datacon, @csi_horario, @csi_nomusu, @csi_codmed, @csi_codponto,
                                                      @csi_codpac, @csi_ordem, @csi_suplente, @csi_dataconf, @csi_nomusuconf, @csi_diagnostico,
                                                      @csi_codnatatend, @csi_codpacacomp, @csi_nomeacomp, @csi_parentescoacomp, @csi_datanacacomp,
                                                      @csi_docideacomp, @csi_endtrabacomp, @csi_codcidacomp, @csi_cepacomp, @csi_diagprovavel, @csi_anamnese,
                                                      @csi_repatendimento, @csi_qtdrepatend, @csi_dataalta, @csi_codtipoalta, @csi_codleito, @csi_nprenatal,
                                                      @csi_codestnut, @csi_status, @csi_codcon, @csi_alta, @csi_obsalta, @csi_modelo, @csi_modelopublico,
                                                      @csi_nomemodelo, @csi_peso, @csi_imc, @csi_altura, @csi_dietaobs, @csi_orientnutri, @csi_cbo,
                                                      @csi_unidade_agendamento, @csi_historico_evolutivo, @csi_id_fila_espera, @csi_tipo_consulta,
                                                      @csi_medida_cintura, @csi_restricao_modelo, @csi_modelo_restrito_pac, @csi_hipertencao, @csi_diabetes,
                                                      @csi_pressao_art_sistolica, @csi_pressao_art_diastolica, @csi_glicemia, @csi_tipo_glicemia,
                                                      @csi_sem_complicacoes, @csi_angina, @csi_iam, @csi_avc, @csi_pre_diabetico, @csi_amputacao_diabete,
                                                      @csi_doenca_renal, @csi_retinopatia, @csi_hb_glicosilada, @csi_creatinina_serica, @csi_colesterol_total,
                                                      @csi_ecg, @csi_triglicerides, @csi_urina_tipo1, @csi_microalbuminuria, @csi_qtde_hidroclorotiazida,
                                                      @csi_qtde_propranolol, @csi_qtde_captopril, @csi_qtde_glibenclamida, @csi_qtde_metformina,
                                                      @csi_qtde_insulina, @csi_ant_fam_cardio_vascular, @csi_diabete_tipo1, @csi_diabete_tipo2, @csi_tabagismo,
                                                      @csi_sedentarismo, @csi_sobrepeso, @csi_medicamentoso, @csi_outros_medicamentos,
                                                      @csi_outras_coronariopatias, @csi_tipo_risco, @csi_status_hiperdia, @csi_outros_medicamentos_obs,
                                                      @csi_id_libexames, @csi_codexa_padrao, @id_diasmed, @id_atendimento_individual, @id_tipo_atendimento,
                                                      @id_atendimento_odontologico, @id_cid, @id_orgao_publico, @csi_data_cancelou, @csi_nomusu_cancelou,
                                                      @csi_obs, @csi_unidade_paciente, @uuid, @id_tipo_consulta, @id_senha, @id_pep_exame_fisico, @sms_enviado,
                                                      @csi_obs_cancelamento, @id_controle_sincronizacao_lote, @id_equipe)";
        string IAgendaCommand.InserirConsulta { get => sqlInserirConsulta; }

        public string sqlGetNewIdConsulta = $@"SELECT GEN_ID(GEN_TSI_CONSULTAS, 1) AS VLR FROM RDB$DATABASE";
        string IAgendaCommand.GetNewIdConsulta { get => sqlGetNewIdConsulta; }

        public string sqlGetConsultaByDiasMedOrdem = $@"SELECT * FROM TSI_CONSULTAS
                                                        WHERE ID_DIASMED = @id_diasmed AND
                                                              CSI_ORDEM = @ordem AND
                                                              CSI_STATUS <> 'Cancelada'";
        string IAgendaCommand.GetConsultaByDiasMedOrdem { get => sqlGetConsultaByDiasMedOrdem; }

        public string sqlCancelarConsulta = $@"UPDATE TSI_CONSULTAS
                                               SET CSI_STATUS = @status,
                                                   CSI_NOMUSU_CANCELOU = @user_cancelou,
                                                   CSI_DATA_CANCELOU = @data_cancelou,
                                                   CSI_OBS_CANCELAMENTO = @obs_cancelou
                                               WHERE (CSI_CONTROLE = @id_controle)";
        string IAgendaCommand.CancelarConsulta { get => sqlCancelarConsulta; }

        public string sqlConfirmaAgendaConsulta = $@"UPDATE TSI_CONSULTAS
                                                     SET CSI_STATUS = @status,
                                                         CSI_DATACONF = @data_conf,
                                                         CSI_NOMUSUCONF = @nome_usu
                                                     WHERE (CSI_CONTROLE = @id_controle)";
        string IAgendaCommand.ConfirmaAgendaConsulta { get => sqlConfirmaAgendaConsulta; }

        public string sqlGetAgendadosByDiasMed = $@"SELECT DM.ID DIASMED, CI.ID, CI.ORDEM, CI.FLG_RESERVADO, C.CSI_ORDEM ORDEM_PAC, C.CSI_CONTROLE,
                                                           LPAD(EXTRACT(HOUR FROM CI.HORARIO), 2, '0') || ':' || LPAD(EXTRACT(MINUTE FROM CI.HORARIO), 2, '0') AS CSI_HORARIO
                                                    FROM TSI_DIASMED DM
                                                    JOIN TSI_CONSULTAS_ITEM CI ON (DM.ID = CI.ID_DIASMED)
                                                    LEFT JOIN TSI_CONSULTAS C ON C.ID_DIASMED = DM.ID AND (C.CSI_ORDEM = CI.ORDEM AND COALESCE(C.CSI_STATUS, '') <> 'Cancelada')
                                                    WHERE DM.ID = @id_dias_med";
        string IAgendaCommand.GetAgendadosByDiasMed { get => sqlGetAgendadosByDiasMed; }

        public string sqlRemanejaOrdemConsulta = $@"UPDATE TSI_CONSULTAS SET CSI_ORDEM = @ordem, CSI_HORARIO = @csi_horario 
                                                    WHERE CSI_CONTROLE = @csi_controle";
        string IAgendaCommand.RemanejaOrdemConsulta { get => sqlRemanejaOrdemConsulta; }
        #endregion

        #region RG Saude Agenda
        public string sqlAtualizaRGSaudeAgenda = $@"UPDATE RG_SAUDE_AGENDA SET SITUACAO = 2, ID_AGENDAMENTO = @id_horario_agenda
                                                    WHERE (ID = @fRG_Saude_Agenda_ID)";
        string IAgendaCommand.AtualizaRGSaudeAgenda { get => sqlAtualizaRGSaudeAgenda; }
        #endregion
    }
}
