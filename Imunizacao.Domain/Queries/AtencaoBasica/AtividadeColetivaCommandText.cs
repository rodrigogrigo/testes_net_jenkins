using RgCidadao.Domain.Commands.AtencaoBasica;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Queries.AtencaoBasica
{
    public class AtividadeColetivaCommandText : IAtividadeColetivaCommand
    {
        public string sqlGetAll = $@"SELECT AC.ID, AC.DATA_ATIVIDADE, AC.HORA_INICIO, AC.HORA_FIM, AC.NUMERO_INEP_ESCOLA,
                                            (
                                            CASE
                                              WHEN AC.ID_EQUIPE IS NOT NULL THEN EQ.SIGLA || '-' || EQ.NOME_REFERENCIA || '(' || EQ.COD_INE || ')'
                                              ELSE 'PROFISSIONAL SEM LOTAÇÃO EM EQUIPE'
                                            END) AS EQUIPE,
                                            M.CSI_NOMMED AS PROFISSIONAL, UNI.CSI_NOMUNI AS UNIDADE, M.CSI_CBO CBO
                                     FROM ESUS_ATIVIDADE_COLETIVA AC
                                     JOIN TSI_UNIDADE UNI ON UNI.CSI_CODUNI = AC.ID_UNIDADE
                                     JOIN TSI_MEDICOS M ON (M.CSI_CODMED = AC.ID_PROFISSIONAL)
                                     LEFT JOIN ESUS_EQUIPES EQ ON (EQ.ID = AC.ID_EQUIPE) 
                    ";
        string IAtividadeColetivaCommand.GetAll { get => sqlGetAll; }

        public string sqlGetAllPagination = $@"SELECT FIRST (@pagesize) SKIP (@page) AC.ID, AC.DATA_ATIVIDADE, AC.HORA_INICIO, AC.HORA_FIM,
                                                      AC.NUMERO_INEP_ESCOLA,
                                                (CASE WHEN AC.ID_EQUIPE IS NOT NULL
                                                      THEN EQ.SIGLA || '-' || EQ.NOME_REFERENCIA || '(' || EQ.COD_INE || ')'
                                                      ELSE 'PROFISSIONAL SEM LOTAÇÃO EM EQUIPE' END) AS EQUIPE,
                                                M.CSI_NOMMED AS PROFISSIONAL, UNI.CSI_NOMUNI AS UNIDADE, M.CSI_CBO CBO
                                               FROM ESUS_ATIVIDADE_COLETIVA AC
                                               JOIN TSI_UNIDADE UNI ON UNI.CSI_CODUNI = AC.ID_UNIDADE
                                               JOIN TSI_MEDICOS M ON (M.CSI_CODMED = AC.ID_PROFISSIONAL)
                                               LEFT JOIN ESUS_EQUIPES EQ ON (EQ.ID = AC.ID_EQUIPE)
                                               WHERE AC.ID_UNIDADE IN (SELECT ID_UNIDADE FROM SEG_PERFIL_USUARIO WHERE ID_USUARIO = @user) 
                                                     @filtros
                                                ORDER BY AC.ID DESC";
        string IAtividadeColetivaCommand.GetAllPagination { get => sqlGetAllPagination; }

        public string sqlGetCountAll = $@"SELECT COUNT(*) FROM (SELECT AC.ID CODIGO, AC.DATA_ATIVIDADE DATA, AC.HORA_INICIO INICIO, AC.HORA_FIM FIM,
                                                                       AC.NUMERO_INEP_ESCOLA INEP,
                                                                 (CASE WHEN AC.ID_EQUIPE IS NOT NULL
                                                                       THEN EQ.SIGLA || '-' || EQ.NOME_REFERENCIA || '(' || EQ.COD_INE || ')'
                                                                       ELSE 'PROFISSIONAL SEM LOTAÇÃO EM EQUIPE' END) AS EQUIPE,
                                                                 M.CSI_NOMMED AS PROFISSIONAL, UNI.CSI_NOMUNI AS UNIDADE
                                                                FROM ESUS_ATIVIDADE_COLETIVA AC
                                                                JOIN TSI_UNIDADE UNI ON UNI.CSI_CODUNI = AC.ID_UNIDADE
                                                                JOIN TSI_MEDICOS M ON (M.CSI_CODMED = AC.ID_PROFISSIONAL)
                                                                LEFT JOIN ESUS_EQUIPES EQ ON (EQ.ID = AC.ID_EQUIPE)
                                                                WHERE AC.ID_UNIDADE IN (SELECT ID_UNIDADE FROM SEG_PERFIL_USUARIO WHERE ID_USUARIO = @user)
                                                                      @filtros)";
        string IAtividadeColetivaCommand.GetCountAll { get => sqlGetCountAll; }

        public string sqlNewId = $@"SELECT GEN_ID(GEN_ESUS_ATIVIDADE_COLETIVA_ID, 1) AS VLR FROM RDB$DATABASE";
        string IAtividadeColetivaCommand.GetNewId { get => sqlNewId; }

        public string sqlInsert = $@"INSERT INTO ESUS_ATIVIDADE_COLETIVA (ID, TIPO_ATIVIDADE, NUMERO_INEP_ESCOLA, DATA_ATIVIDADE, HORA_FIM, HORA_INICIO,
                                                                          LOCAL_ATIVIDADE, NUM_ALTERACOES, NUM_PARTICIPANTES, PRATICA_ALI_SAUDAVEL,
                                                                          PRATICA_APLICA_FLUOR, PRATICA_ACUID_VISUAL, PRATICA_AUTOCUIDADO,
                                                                          PRATICA_CIDAD_DIREITOS, PRATICA_SAUDE_TRABALHA, PRATICA_DEPED_QUIMICA,
                                                                          PRATICA_ENVELHECIMENTO, PRATICA_ESCOVA_DENTAL, PRATICA_PLANTA_MEDICI,
                                                                          PRATICA_ATIVIDADE_FISIC, PRATICA_CORPO_MENTAL, PRATICA_CORP_MENT_PIC,
                                                                          PRATICA_PREV_VIOL_CULTU, PRATICA_SAUDE_AMBIENTAL, PRATICA_SAUDE_BUCAL,
                                                                          PRATICA_SAUDE_MENTAL, PRATICA_SEXUAL_REPRODUT, PRATICA_SAUDE_ESCOLA,
                                                                          PRATICA_AGRAVO_NEGLIGEN, PRATICA_ANTROPOMETRIA, PRATICA_OUTROS,
                                                                          PREVISAO_PARTICIPANTES, PUBLICO_COMUNIDADE, PUBLICO_CRIAN_0_3_ANOS,
                                                                          PUBLICO_CRIAN_4_5_ANOS, PUBLICO_CRIAN_6_11_ANOS, PUBLICO_ADOLESCENTE,
                                                                          PUBLICO_MULHER, PUBLICO_GESTANTE, PUBLICO_HOMEM, PUBLICO_FAMILIARES,
                                                                          PUBLICO_IDOSOS, PUBLICO_PESS_DOENCA, PUBLICO_USUA_TABACO, PUBLICO_USUA_ALCOOL,
                                                                          PUBLICO_USUA_DROGAS, PUBLICO_SOFRIM_MENTAL, PUBLICO_PROF_EDUCACAO, PUBLICO_OUTROS,
                                                                          ID_LOTACAO_RESPONSAVEL, SITUACAO_ENVIO, TEMA_QUESTO_ADMINISTR,
                                                                          TEMA_PROCESSO_TRABALHO, TEMA_DIAGNOSTICO_TERRIT, TEMA_PLANEJA_MONITORAM,
                                                                          TEMA_DISCUSSAO_CASA, TEMA_EDUCA_PERMANENT, TEMA_OUTROS, EXPORTADO_ESUS,
                                                                          DATA_ALTERACAO_SERV, SAUDE_AUDITIVA,
                                                                          DESENVOLVIMENTO_LINGUAGEM, VERIFICACAO_SITUACAO_VACINAL, PNCT_SESSAO_1,
                                                                          PNCT_SESSAO_2, PNCT_SESSAO_3, PNCT_SESSAO_4, TURNO, CODIGO_SIGTAP, CNES,
                                                                          ID_ESUS_EXPORTACAO_ITEM, ID_PROFISSIONAL, ID_EQUIPE, ID_UNIDADE,
                                                                          FLG_ATENDIMENTO_ESPECIALIZADO, ID_USUARIO, FLG_PRATICA_EDUCATIVA,
                                                                          FLG_PRATICA_SAUDE, ID_CONTROLE_SINCRONIZACAO_LOTE, LOCAL_ATENDIMENTO, ID_ESCOLA,
                                                                          ID_ESTABELECIMENTO)
                                     VALUES (@id, @tipo_atividade, @numero_inep_escola, @data_atividade, @hora_fim, @hora_inicio, @local_atividade, @num_alteracoes,
                                             @num_participantes, @pratica_ali_saudavel, @pratica_aplica_fluor, @pratica_acuid_visual, @pratica_autocuidado,
                                             @pratica_cidad_direitos, @pratica_saude_trabalha, @pratica_deped_quimica, @pratica_envelhecimento,
                                             @pratica_escova_dental, @pratica_planta_medici, @pratica_atividade_fisic, @pratica_corpo_mental,
                                             @pratica_corp_ment_pic, @pratica_prev_viol_cultu, @pratica_saude_ambiental, @pratica_saude_bucal,
                                             @pratica_saude_mental, @pratica_sexual_reprodut, @pratica_saude_escola, @pratica_agravo_negligen,
                                             @pratica_antropometria, @pratica_outros, @previsao_participantes, @publico_comunidade, @publico_crian_0_3_anos,
                                             @publico_crian_4_5_anos, @publico_crian_6_11_anos, @publico_adolescente, @publico_mulher, @publico_gestante,
                                             @publico_homem, @publico_familiares, @publico_idosos, @publico_pess_doenca, @publico_usua_tabaco,
                                             @publico_usua_alcool, @publico_usua_drogas, @publico_sofrim_mental, @publico_prof_educacao, @publico_outros,
                                             @id_lotacao_responsavel, @situacao_envio, @tema_questo_administr, @tema_processo_trabalho, @tema_diagnostico_territ,
                                             @tema_planeja_monitoram, @tema_discussao_casa, @tema_educa_permanent, @tema_outros, @exportado_esus,
                                             @data_alteracao_serv, @saude_auditiva, @desenvolvimento_linguagem,
                                             @verificacao_situacao_vacinal, @pnct_sessao_1, @pnct_sessao_2, @pnct_sessao_3, @pnct_sessao_4, @turno, @codigo_sigtap,
                                             @cnes, @id_esus_exportacao_item, @id_profissional, @id_equipe, @id_unidade, @flg_atendimento_especializado,
                                             @id_usuario, @flg_pratica_educativa, @flg_pratica_saude, @id_controle_sincronizacao_lote, @local_atendimento,
                                             @id_escola, @id_estabelecimento)";

        string IAtividadeColetivaCommand.Insert { get => sqlInsert; }

        //JAKISON 
        public string sqlNewIdProf = $@"SELECT GEN_ID(GEN_ESUS_ATIVID_COLETI_PROF_ID, 1) AS VLR FROM RDB$DATABASE";
        string IAtividadeColetivaCommand.GetNewIdProf { get => sqlNewIdProf; }

        public string sqlInsertProfissionalParticipante = $@"UPDATE OR INSERT INTO ESUS_ATIVID_COLETI_PROF (ID, ID_ATIVIDADE, ID_MEDICO, ID_CBO, ID_LOTACAO)
                                            VALUES (@id, @id_atividade, @id_medico, @id_cbo, @id_lotacao)";

        string IAtividadeColetivaCommand.InsertProfissionalParticipante { get => sqlInsertProfissionalParticipante; }

        public string sqlNewIdPartic = $@"SELECT GEN_ID(GEN_ESUS_ATIVI_COLET_PARTIC_ID, 1) AS VLR FROM RDB$DATABASE";
        string IAtividadeColetivaCommand.GetNewIdPartic { get => sqlNewIdPartic; }

        public string sqlInsertPessoaParticipante = $@"UPDATE OR INSERT INTO ESUS_ATIVI_COLET_PARTIC (ID, ID_CIDADAO, ID_ATIVIDADE, PESO, ALTURA, AVALIA_ALTERADA, CESSOU_HABITO_FUMAR, ABANDONO_GRUPO, CNS_CIDADAO, DTNASCIMENTO, SEXO_CIDADAO)
                                            VALUES (@id, @id_cidadao, @id_atividade, @peso, @altura, @avalia_alterada, @cessou_habito_fumar, @abandono_grupo, @cns_cidadao, @dtnascimento, @sexo_cidadao)";

        string IAtividadeColetivaCommand.InsertPessoaParticipante { get => sqlInsertPessoaParticipante; }
        //JAKISON

        public string sqlUpdate = $@"UPDATE ESUS_ATIVIDADE_COLETIVA
                                     SET TIPO_ATIVIDADE = @tipo_atividade,
                                         NUMERO_INEP_ESCOLA = @numero_inep_escola,
                                         DATA_ATIVIDADE = @data_atividade,
                                         HORA_FIM = @hora_fim,
                                         HORA_INICIO = @hora_inicio,
                                         LOCAL_ATIVIDADE = @local_atividade,
                                         NUM_ALTERACOES = @num_alteracoes,
                                         NUM_PARTICIPANTES = @num_participantes,
                                         PRATICA_ALI_SAUDAVEL = @pratica_ali_saudavel,
                                         PRATICA_APLICA_FLUOR = @pratica_aplica_fluor,
                                         PRATICA_ACUID_VISUAL = @pratica_acuid_visual,
                                         PRATICA_AUTOCUIDADO = @pratica_autocuidado,
                                         PRATICA_CIDAD_DIREITOS = @pratica_cidad_direitos,
                                         PRATICA_SAUDE_TRABALHA = @pratica_saude_trabalha,
                                         PRATICA_DEPED_QUIMICA = @pratica_deped_quimica,
                                         PRATICA_ENVELHECIMENTO = @pratica_envelhecimento,
                                         PRATICA_ESCOVA_DENTAL = @pratica_escova_dental,
                                         PRATICA_PLANTA_MEDICI = @pratica_planta_medici,
                                         PRATICA_ATIVIDADE_FISIC = @pratica_atividade_fisic,
                                         PRATICA_CORPO_MENTAL = @pratica_corpo_mental,
                                         PRATICA_CORP_MENT_PIC = @pratica_corp_ment_pic,
                                         PRATICA_PREV_VIOL_CULTU = @pratica_prev_viol_cultu,
                                         PRATICA_SAUDE_AMBIENTAL = @pratica_saude_ambiental,
                                         PRATICA_SAUDE_BUCAL = @pratica_saude_bucal,
                                         PRATICA_SAUDE_MENTAL = @pratica_saude_mental,
                                         PRATICA_SEXUAL_REPRODUT = @pratica_sexual_reprodut,
                                         PRATICA_SAUDE_ESCOLA = @pratica_saude_escola,
                                         PRATICA_AGRAVO_NEGLIGEN = @pratica_agravo_negligen,
                                         PRATICA_ANTROPOMETRIA = @pratica_antropometria,
                                         PRATICA_OUTROS = @pratica_outros,
                                         PREVISAO_PARTICIPANTES = @previsao_participantes,
                                         PUBLICO_COMUNIDADE = @publico_comunidade,
                                         PUBLICO_CRIAN_0_3_ANOS = @publico_crian_0_3_anos,
                                         PUBLICO_CRIAN_4_5_ANOS = @publico_crian_4_5_anos,
                                         PUBLICO_CRIAN_6_11_ANOS = @publico_crian_6_11_anos,
                                         PUBLICO_ADOLESCENTE = @publico_adolescente,
                                         PUBLICO_MULHER = @publico_mulher,
                                         PUBLICO_GESTANTE = @publico_gestante,
                                         PUBLICO_HOMEM = @publico_homem,
                                         PUBLICO_FAMILIARES = @publico_familiares,
                                         PUBLICO_IDOSOS = @publico_idosos,
                                         PUBLICO_PESS_DOENCA = @publico_pess_doenca,
                                         PUBLICO_USUA_TABACO = @publico_usua_tabaco,
                                         PUBLICO_USUA_ALCOOL = @publico_usua_alcool,
                                         PUBLICO_USUA_DROGAS = @publico_usua_drogas,
                                         PUBLICO_SOFRIM_MENTAL = @publico_sofrim_mental,
                                         PUBLICO_PROF_EDUCACAO = @publico_prof_educacao,
                                         PUBLICO_OUTROS = @publico_outros,
                                         ID_LOTACAO_RESPONSAVEL = @id_lotacao_responsavel,
                                         SITUACAO_ENVIO = @situacao_envio,
                                         TEMA_QUESTO_ADMINISTR = @tema_questo_administr,
                                         TEMA_PROCESSO_TRABALHO = @tema_processo_trabalho,
                                         TEMA_DIAGNOSTICO_TERRIT = @tema_diagnostico_territ,
                                         TEMA_PLANEJA_MONITORAM = @tema_planeja_monitoram,
                                         TEMA_DISCUSSAO_CASA = @tema_discussao_casa,
                                         TEMA_EDUCA_PERMANENT = @tema_educa_permanent,
                                         TEMA_OUTROS = @tema_outros,
                                         EXPORTADO_ESUS = @exportado_esus,
                                         DATA_ALTERACAO_SERV = @data_alteracao_serv,
                                         SAUDE_AUDITIVA = @saude_auditiva,
                                         DESENVOLVIMENTO_LINGUAGEM = @desenvolvimento_linguagem,
                                         VERIFICACAO_SITUACAO_VACINAL = @verificacao_situacao_vacinal,
                                         PNCT_SESSAO_1 = @pnct_sessao_1,
                                         PNCT_SESSAO_2 = @pnct_sessao_2,
                                         PNCT_SESSAO_3 = @pnct_sessao_3,
                                         PNCT_SESSAO_4 = @pnct_sessao_4,
                                         TURNO = @turno,
                                         CODIGO_SIGTAP = @codigo_sigtap,
                                         CNES = @cnes,
                                         ID_ESUS_EXPORTACAO_ITEM = @id_esus_exportacao_item,
                                         ID_PROFISSIONAL = @id_profissional,
                                         ID_EQUIPE = @id_equipe,
                                         ID_UNIDADE = @id_unidade,
                                         FLG_ATENDIMENTO_ESPECIALIZADO = @flg_atendimento_especializado,
                                         ID_USUARIO = @id_usuario,
                                         FLG_PRATICA_EDUCATIVA = @flg_pratica_educativa,
                                         FLG_PRATICA_SAUDE = @flg_pratica_saude,
                                         ID_CONTROLE_SINCRONIZACAO_LOTE = @id_controle_sincronizacao_lote,
                                         LOCAL_ATENDIMENTO = @local_atendimento,
                                         ID_ESCOLA = @id_escola,
                                         ID_ESTABELECIMENTO = @id_estabelecimento
                                     WHERE ID = @id";
        string IAtividadeColetivaCommand.Update { get => sqlUpdate; }

        public string sqlDelete = $@"DELETE FROM ESUS_ATIVIDADE_COLETIVA
                                     WHERE ID = @id";
        string IAtividadeColetivaCommand.Delete { get => sqlDelete; }

       

        public string sqlGetAtividadeColetivaById = $@"SELECT EAC.*, (CASE WHEN EAC.ID_EQUIPE IS NOT NULL
                                                          THEN EE.SIGLA || '-' || EE.NOME_REFERENCIA || '(' || EE.COD_INE || ')'
                                                          ELSE 'PROFISSIONAL SEM LOTAÇÃO EM EQUIPE' END) EQUIPE, MED.CSI_INATIVO CSI_INATIVO_PROFISSIONAL,
                                                        PROC.CODIGO || ' - ' || PROC.NOME NOME_SIGTAP, MED.CSI_CODMED,
                                                        MED.CSI_INATIVO CSI_INATIVO_PROFISSIONAL, MED.CSI_NOMMED, MED.CSI_CODMED || ' - ' || MED.CSI_NOMMED DESCRICAO,
                                                        MED.CSI_CBO 
                                                        FROM ESUS_ATIVIDADE_COLETIVA EAC
                                                        LEFT JOIN ESUS_EQUIPES EE ON EE.ID = EAC.ID_EQUIPE
                                                        JOIN TSI_MEDICOS MED ON MED.CSI_CODMED = EAC.ID_PROFISSIONAL
                                                        LEFT JOIN TSI_PROCEDIMENTO PROC ON PROC.CODIGO = EAC.CODIGO_SIGTAP
                                                        WHERE EAC.ID = @codigo";
        string IAtividadeColetivaCommand.GetAtividadeColetivaById { get => sqlGetAtividadeColetivaById; }

        public string sqlGetProfissionalByAtividadeColetiva = $@"SELECT PROF.ID, MED.CSI_CODMED, MED.CSI_NOMMED, CBO.CODIGO CSI_CBO FROM ESUS_ATIVID_COLETI_PROF PROF
                                                                 JOIN TSI_MEDICOS MED ON MED.CSI_CODMED = PROF.ID_MEDICO
                                                                 JOIN TSI_CBO CBO ON CBO.CODIGO = PROF.ID_CBO
                                                                 WHERE ID_ATIVIDADE = @id_atividade ORDER BY PROF.ID DESC";
        string IAtividadeColetivaCommand.GetProfissionalByAtividadeColetiva { get => sqlGetProfissionalByAtividadeColetiva; }

        public string sqlGetPacienteByAtividadeColetiva = $@"SELECT EACP.ID, PAC.CSI_CODPAC, PAC.CSI_NOMPAC, PAC.CSI_SEXPAC, PAC.CSI_NCARTAO, PAC.CSI_CPFPAC,
                                                                    EACP.CESSOU_HABITO_FUMAR, EACP.AVALIA_ALTERADA, EACP.PESO CSI_PESO, EACP.ALTURA CSI_ALTURA, EACP.DTNASCIMENTO
                                                             FROM ESUS_ATIVI_COLET_PARTIC EACP
                                                             JOIN TSI_CADPAC PAC ON PAC.CSI_CODPAC = EACP.ID_CIDADAO
                                                             WHERE ID_ATIVIDADE = @id_atividade ORDER BY EACP.ID DESC";
        string IAtividadeColetivaCommand.GetPacienteByAtividadeColetiva { get => sqlGetPacienteByAtividadeColetiva; }

        public string sqlDeleteProfissional = $@"DELETE FROM ESUS_ATIVID_COLETI_PROF
                                                 WHERE ID = @id";
        string IAtividadeColetivaCommand.DeleteProfissional { get => sqlDeleteProfissional; }

        public string sqlDeleteParticipante = $@"DELETE FROM ESUS_ATIVI_COLET_PARTIC
                                                 WHERE ID = @id";
        string IAtividadeColetivaCommand.DeleteParticipante { get => sqlDeleteParticipante; }

        public string sqlDeleteProfissionalByAtividade = $@"DELETE FROM ESUS_ATIVID_COLETI_PROF
                                                            WHERE ID_ATIVIDADE = @id";
        string IAtividadeColetivaCommand.DeleteProfissionalByAtividade { get => sqlDeleteProfissionalByAtividade; }

        public string sqlDeleteParticipanteByAtividade = $@"DELETE FROM ESUS_ATIVI_COLET_PARTIC
                                                            WHERE ID_ATIVIDADE = @id";
        string IAtividadeColetivaCommand.DeleteParticipanteByAtividade { get => sqlDeleteParticipanteByAtividade; }
    }
}
