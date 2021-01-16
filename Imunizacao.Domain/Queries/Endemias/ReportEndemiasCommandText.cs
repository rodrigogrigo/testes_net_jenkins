using RgCidadao.Domain.Commands.Endemias;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Queries.Endemias
{
    public class ReportEndemiasCommandText : IReportEndemiasCommand
    {
        public string sqlInfestacaoPredialSintetico = $@"SELECT TAB2.BAIRRO, TAB2.IMOVEIS_PESQUISADOS, TAB2.TOTAL_POSITIVOS_AEGYPTI, TAB2.IND_AEGYPTI_PRED,
                                                              TAB2.TOTAL_POSITIVOS_ALBOPICTUS, TAB2.IND_ALBOPICTUS_PRED, TAB2.DEP_AEGYPTI_POS, TAB2.DEP_ALBOPICTUS_POS,
                                                              TAB2.IND_AEGYPTI_BRET, TAB2.IND_ALBOPICTUS_BRET, TAB2.TOT_REC_POS_AEGYPTI, TAB2.A1_AEGYPTI_COUNT, TAB2.A2_AEGYPTI_COUNT,
                                                              TAB2.B_AEGYPTI_COUNT,TAB2.C_AEGYPTI_COUNT, TAB2.D1_AEGYPTI_COUNT,TAB2.D2_AEGYPTI_COUNT,TAB2.E_AEGYPTI_COUNT,
                                                              TAB2.TOT_REC_POS_ALBOPICTUS,TAB2.A1_ALBOPICTUS_COUNT,TAB2.A2_ALBOPICTUS_COUNT,TAB2.B_ALBOPICTUS_COUNT,
                                                              TAB2.C_ALBOPICTUS_COUNT,TAB2.D1_ALBOPICTUS_COUNT, TAB2.D2_ALBOPICTUS_COUNT,TAB2.E_ALBOPICTUS_COUNT,
                                                              TAB2.A1_TOT, TAB2.A2_TOT, TAB2.B_TOT, TAB2.C_TOT,
                                                              TAB2.D1_TOT, TAB2.D2_TOT, TAB2.E_TOT
                                                         FROM (SELECT TAB.CSI_NOMBAI BAIRRO, COUNT(TAB.IMOVEL_VISITADO) IMOVEIS_PESQUISADOS,
                                                                      SUM(TAB.TOTAL_POSITIVOS_AEGYPTI) TOTAL_POSITIVOS_AEGYPTI,
                                                                      (SUM(TAB.TOTAL_POSITIVOS_AEGYPTI) / (COUNT(TAB.IMOVEL_VISITADO) * 100)) IND_AEGYPTI_PRED,
                                                                      SUM(TAB.TOTAL_POSITIVOS_ALBOPICTUS) TOTAL_POSITIVOS_ALBOPICTUS,
                                                                      (SUM(TAB.TOTAL_POSITIVOS_ALBOPICTUS) / (COUNT(TAB.IMOVEL_VISITADO) * 100)) IND_ALBOPICTUS_PRED,
                                                                      SUM(TAB.DEP_AEGYPTI_POS) DEP_AEGYPTI_POS, SUM(TAB.DEP_ALBOPICTUS_POS) DEP_ALBOPICTUS_POS,
                                                                      (SUM(TAB.DEP_AEGYPTI_POS) / (COUNT(TAB.IMOVEL_VISITADO) * 100)) IND_AEGYPTI_BRET,
                                                                      (SUM(TAB.DEP_ALBOPICTUS_POS) / (COUNT(TAB.IMOVEL_VISITADO) * 100)) IND_ALBOPICTUS_BRET,
                                                                      -- POR TIPO DE RECIPIENTE AEGYPTI--
                                                                      SUM(TAB.A1_AEGYPTI_COUNT) A1_AEGYPTI_COUNT, SUM(TAB.A2_AEGYPTI_COUNT) A2_AEGYPTI_COUNT,
                                                                      SUM(TAB.B_AEGYPTI_COUNT) B_AEGYPTI_COUNT, SUM(TAB.C_AEGYPTI_COUNT) C_AEGYPTI_COUNT,
                                                                      SUM(TAB.D1_AEGYPTI_COUNT) D1_AEGYPTI_COUNT, SUM(TAB.D2_AEGYPTI_COUNT) D2_AEGYPTI_COUNT,
                                                                      SUM(TAB.E_AEGYPTI_COUNT) E_AEGYPTI_COUNT,
                                                                      (SUM(TAB.A1_AEGYPTI_COUNT) + SUM(TAB.A2_AEGYPTI_COUNT) + SUM(TAB.B_AEGYPTI_COUNT) +
                                                                      SUM(TAB.C_AEGYPTI_COUNT) + SUM(TAB.D1_AEGYPTI_COUNT) + SUM(TAB.D2_AEGYPTI_COUNT) +
                                                                      SUM(TAB.E_AEGYPTI_COUNT)) TOT_REC_POS_AEGYPTI,
                                                                      -- ÍNDICE POR TIPO DE RECIPIENTE ALBOPICTUS--
                                                                      SUM(TAB.A1_ALBOPICTUS_COUNT) A1_ALBOPICTUS_COUNT, SUM(TAB.A2_ALBOPICTUS_COUNT) A2_ALBOPICTUS_COUNT,
                                                                      SUM(TAB.B_ALBOPICTUS_COUNT) B_ALBOPICTUS_COUNT, SUM(TAB.C_ALBOPICTUS_COUNT) C_ALBOPICTUS_COUNT,
                                                                      SUM(TAB.D1_ALBOPICTUS_COUNT) D1_ALBOPICTUS_COUNT, SUM(TAB.D2_ALBOPICTUS_COUNT) D2_ALBOPICTUS_COUNT,
                                                                      SUM(TAB.E_ALBOPICTUS_COUNT) E_ALBOPICTUS_COUNT,
                                                                      (SUM(TAB.A1_ALBOPICTUS_COUNT) + SUM(TAB.A2_ALBOPICTUS_COUNT) + SUM(TAB.B_ALBOPICTUS_COUNT) +
                                                                      SUM(TAB.C_ALBOPICTUS_COUNT) + SUM(TAB.D1_ALBOPICTUS_COUNT) + SUM(TAB.D2_ALBOPICTUS_COUNT) +
                                                                      SUM(TAB.E_ALBOPICTUS_COUNT)) TOT_REC_POS_ALBOPICTUS,
                                                                      -- TOTAL POR DEPOSITOS
                                                                      SUM(TAB.A1_TOT) A1_TOT, SUM(A2_TOT) A2_TOT,
                                                                      SUM(TAB.B_TOT) B_TOT, SUM(TAB.C_TOT) C_TOT,
                                                                      SUM(TAB.D1_TOT) D1_TOT, SUM(TAB.D2_TOT) D2_TOT,
                                                                      SUM(TAB.E_TOT) E_TOT
                                                          FROM (SELECT B.CSI_NOMBAI,
                                                                          -- ÍNDICE DE INFESTAÇÃO PREDIAL      --
                                                                          E.ID IMOVEL_VISITADO,
                                                                          (SELECT
                                                                                  CASE
                                                                                      WHEN COUNT(ESP.ID) > 0 THEN 1
                                                                                      ELSE 0
                                                                                  END TOTAL_POSITIVOS
                                                                          FROM VA_COLETA_RESULTADO COL_RES
                                                                          JOIN VA_ESPECIME ESP ON ESP.ID = COL_RES.ID_ESPECIME
                                                                          JOIN VA_COLETA COL ON COL.ID = COL_RES.ID_COLETA
                                                                          WHERE COL.ID_VISITA = VI.ID AND
                                                                                  ESP.TIPO_ESPECIME = 1) TOTAL_POSITIVOS_AEGYPTI,
                                                                          (SELECT
                                                                                  CASE
                                                                                      WHEN COUNT(ESP.ID) > 0 THEN 1
                                                                                      ELSE 0
                                                                                  END TOTAL_POSITIVOS
                                                                          FROM VA_COLETA_RESULTADO COL_RES
                                                                          JOIN VA_ESPECIME ESP ON ESP.ID = COL_RES.ID_ESPECIME
                                                                          JOIN VA_COLETA COL ON COL.ID = COL_RES.ID_COLETA
                                                                          WHERE COL.ID_VISITA = VI.ID AND
                                                                                  ESP.TIPO_ESPECIME = 2) TOTAL_POSITIVOS_ALBOPICTUS,
                                                                          -- ÍNDICE DE BRETEAU --
                                                                          (SELECT COUNT(COL.ID)
                                                                          FROM VA_COLETA COL
                                                                          JOIN VA_COLETA_RESULTADO COL_RES ON COL_RES.ID_COLETA = COL.ID
                                                                          JOIN VA_ESPECIME ESP ON ESP.ID = COL_RES.ID_ESPECIME
                                                                          WHERE COL.ID_VISITA = VI.ID AND
                                                                                  ESP.TIPO_ESPECIME = 1) DEP_AEGYPTI_POS,
                                                                          (SELECT COUNT(COL.ID)
                                                                          FROM VA_COLETA COL
                                                                          JOIN VA_COLETA_RESULTADO COL_RES ON COL_RES.ID_COLETA = COL.ID
                                                                          JOIN VA_ESPECIME ESP ON ESP.ID = COL_RES.ID_ESPECIME
                                                                          WHERE COL.ID_VISITA = VI.ID AND
                                                                                  ESP.TIPO_ESPECIME = 2) DEP_ALBOPICTUS_POS,
                                                                          ---- ÍNDICE POR TIPO DE RECIPIENTE AEGYPTI-------------
                                                                          (SELECT COUNT(COL.ID)
                                                                          FROM VA_COLETA COL
                                                                          JOIN VA_COLETA_RESULTADO COL_RES ON COL_RES.ID_COLETA = COL.ID
                                                                          JOIN VA_ESPECIME ESP ON ESP.ID = COL_RES.ID_ESPECIME
                                                                          WHERE COL.ID_VISITA = VI.ID AND
                                                                                  ESP.TIPO_ESPECIME = 1 AND
                                                                                  COL.DEPOSITO = 0) A1_AEGYPTI_COUNT,
                                                                          (SELECT COUNT(COL.ID)
                                                                          FROM VA_COLETA COL
                                                                          JOIN VA_COLETA_RESULTADO COL_RES ON COL_RES.ID_COLETA = COL.ID
                                                                          JOIN VA_ESPECIME ESP ON ESP.ID = COL_RES.ID_ESPECIME
                                                                          WHERE COL.ID_VISITA = VI.ID AND
                                                                                  ESP.TIPO_ESPECIME = 1 AND
                                                                                  COL.DEPOSITO = 1) A2_AEGYPTI_COUNT,
                                                                          (SELECT COUNT(COL.ID)
                                                                          FROM VA_COLETA COL
                                                                          JOIN VA_COLETA_RESULTADO COL_RES ON COL_RES.ID_COLETA = COL.ID
                                                                          JOIN VA_ESPECIME ESP ON ESP.ID = COL_RES.ID_ESPECIME
                                                                          WHERE COL.ID_VISITA = VI.ID AND
                                                                                  ESP.TIPO_ESPECIME = 1 AND
                                                                                  COL.DEPOSITO = 2) B_AEGYPTI_COUNT,
                                                                          (SELECT COUNT(COL.ID)
                                                                          FROM VA_COLETA COL
                                                                          JOIN VA_COLETA_RESULTADO COL_RES ON COL_RES.ID_COLETA = COL.ID
                                                                          JOIN VA_ESPECIME ESP ON ESP.ID = COL_RES.ID_ESPECIME
                                                                          WHERE COL.ID_VISITA = VI.ID AND
                                                                                  ESP.TIPO_ESPECIME = 1 AND
                                                                                  COL.DEPOSITO = 3) C_AEGYPTI_COUNT,
                                                                          (SELECT COUNT(COL.ID)
                                                                          FROM VA_COLETA COL
                                                                          JOIN VA_COLETA_RESULTADO COL_RES ON COL_RES.ID_COLETA = COL.ID
                                                                          JOIN VA_ESPECIME ESP ON ESP.ID = COL_RES.ID_ESPECIME
                                                                          WHERE COL.ID_VISITA = VI.ID AND
                                                                                  ESP.TIPO_ESPECIME = 1 AND
                                                                                  COL.DEPOSITO = 4) D1_AEGYPTI_COUNT,
                                                                          (SELECT COUNT(COL.ID)
                                                                          FROM VA_COLETA COL
                                                                          JOIN VA_COLETA_RESULTADO COL_RES ON COL_RES.ID_COLETA = COL.ID
                                                                          JOIN VA_ESPECIME ESP ON ESP.ID = COL_RES.ID_ESPECIME
                                                                          WHERE COL.ID_VISITA = VI.ID AND
                                                                                  ESP.TIPO_ESPECIME = 1 AND
                                                                                  COL.DEPOSITO = 5) D2_AEGYPTI_COUNT,
                                                                          (SELECT COUNT(COL.ID)
                                                                          FROM VA_COLETA COL
                                                                          JOIN VA_COLETA_RESULTADO COL_RES ON COL_RES.ID_COLETA = COL.ID
                                                                          JOIN VA_ESPECIME ESP ON ESP.ID = COL_RES.ID_ESPECIME
                                                                          WHERE COL.ID_VISITA = VI.ID AND
                                                                                  ESP.TIPO_ESPECIME = 1 AND
                                                                                  COL.DEPOSITO = 6) E_AEGYPTI_COUNT,
                                                                          ---------- ÍNDICE POR TIPO DE RECIPIENTE ALBOPICTUS-------------
                                                                          (SELECT COUNT(COL.ID)
                                                                          FROM VA_COLETA COL
                                                                          JOIN VA_COLETA_RESULTADO COL_RES ON COL_RES.ID_COLETA = COL.ID
                                                                          JOIN VA_ESPECIME ESP ON ESP.ID = COL_RES.ID_ESPECIME
                                                                          WHERE COL.ID_VISITA = VI.ID AND
                                                                                  ESP.TIPO_ESPECIME = 2 AND
                                                                                  COL.DEPOSITO = 0) A1_ALBOPICTUS_COUNT,
                                                                          (SELECT COUNT(COL.ID)
                                                                          FROM VA_COLETA COL
                                                                          JOIN VA_COLETA_RESULTADO COL_RES ON COL_RES.ID_COLETA = COL.ID
                                                                          JOIN VA_ESPECIME ESP ON ESP.ID = COL_RES.ID_ESPECIME
                                                                          WHERE COL.ID_VISITA = VI.ID AND
                                                                                  ESP.TIPO_ESPECIME = 2 AND
                                                                                  COL.DEPOSITO = 1) A2_ALBOPICTUS_COUNT,
                                                                          (SELECT COUNT(COL.ID)
                                                                          FROM VA_COLETA COL
                                                                          JOIN VA_COLETA_RESULTADO COL_RES ON COL_RES.ID_COLETA = COL.ID
                                                                          JOIN VA_ESPECIME ESP ON ESP.ID = COL_RES.ID_ESPECIME
                                                                          WHERE COL.ID_VISITA = VI.ID AND
                                                                                  ESP.TIPO_ESPECIME = 2 AND
                                                                                  COL.DEPOSITO = 2) B_ALBOPICTUS_COUNT,
                                                                          (SELECT COUNT(COL.ID)
                                                                          FROM VA_COLETA COL
                                                                          JOIN VA_COLETA_RESULTADO COL_RES ON COL_RES.ID_COLETA = COL.ID
                                                                          JOIN VA_ESPECIME ESP ON ESP.ID = COL_RES.ID_ESPECIME
                                                                          WHERE COL.ID_VISITA = VI.ID AND
                                                                                  ESP.TIPO_ESPECIME = 2 AND
                                                                                  COL.DEPOSITO = 3) C_ALBOPICTUS_COUNT,
                                                                          (SELECT COUNT(COL.ID)
                                                                          FROM VA_COLETA COL
                                                                          JOIN VA_COLETA_RESULTADO COL_RES ON COL_RES.ID_COLETA = COL.ID
                                                                          JOIN VA_ESPECIME ESP ON ESP.ID = COL_RES.ID_ESPECIME
                                                                          WHERE COL.ID_VISITA = VI.ID AND
                                                                                  ESP.TIPO_ESPECIME = 2 AND
                                                                                  COL.DEPOSITO = 4) D1_ALBOPICTUS_COUNT,
                                                                          (SELECT COUNT(COL.ID)
                                                                          FROM VA_COLETA COL
                                                                          JOIN VA_COLETA_RESULTADO COL_RES ON COL_RES.ID_COLETA = COL.ID
                                                                          JOIN VA_ESPECIME ESP ON ESP.ID = COL_RES.ID_ESPECIME
                                                                          WHERE COL.ID_VISITA = VI.ID AND
                                                                                  ESP.TIPO_ESPECIME = 2 AND
                                                                                  COL.DEPOSITO = 5) D2_ALBOPICTUS_COUNT,
                                                                          (SELECT COUNT(COL.ID)
                                                                          FROM VA_COLETA COL
                                                                          JOIN VA_COLETA_RESULTADO COL_RES ON COL_RES.ID_COLETA = COL.ID
                                                                          JOIN VA_ESPECIME ESP ON ESP.ID = COL_RES.ID_ESPECIME
                                                                          WHERE COL.ID_VISITA = VI.ID AND
                                                                                  ESP.TIPO_ESPECIME = 2 AND
                                                                                  COL.DEPOSITO = 6) E_ALBOPICTUS_COUNT,
                                                                          ------- total de depositos por recipiente independente do tipo ----
                                                                          (SELECT COUNT(COL.ID)
                                                                          FROM VA_COLETA COL
                                                                          WHERE COL.ID_VISITA = VI.ID  AND
                                                                                  COL.DEPOSITO = 0) A1_TOT,
                                                                          (SELECT COUNT(COL.ID)
                                                                          FROM VA_COLETA COL
                                                                          WHERE COL.ID_VISITA = VI.ID AND
                                                                                COL.DEPOSITO = 1) A2_TOT,
                                                                          (SELECT COUNT(COL.ID)
                                                                          FROM VA_COLETA COL
                                                                          WHERE COL.ID_VISITA = VI.ID AND
                                                                                COL.DEPOSITO = 2) B_TOT,
                                                                          (SELECT COUNT(COL.ID)
                                                                          FROM VA_COLETA COL
                                                                          WHERE COL.ID_VISITA = VI.ID AND
                                                                                COL.DEPOSITO = 3) C_TOT,
                                                                          (SELECT COUNT(COL.ID)
                                                                          FROM VA_COLETA COL
                                                                          WHERE COL.ID_VISITA = VI.ID AND
                                                                                COL.DEPOSITO = 4) D1_TOT,
                                                                          (SELECT COUNT(COL.ID)
                                                                          FROM VA_COLETA COL
                                                                          WHERE COL.ID_VISITA = VI.ID AND
                                                                                  COL.DEPOSITO = 5) D2_TOT,
                                                                          (SELECT COUNT(COL.ID)
                                                                          FROM VA_COLETA COL
                                                                          WHERE COL.ID_VISITA = VI.ID AND
                                                                                  COL.DEPOSITO = 6) E_TOT
                                                                  FROM VISITA_IMOVEL VI
                                                                  JOIN VS_ESTABELECIMENTOS E ON E.ID = VI.ID_ESTABELECIMENTO
                                                                  INNER JOIN TSI_LOGRADOURO L ON L.CSI_CODEND = E.ID_LOGRADOURO
                                                                  INNER JOIN TSI_BAIRRO B ON B.CSI_CODBAI = L.CSI_CODBAI
                                                                  WHERE VI.ENCONTROU_FOCO = 1
                                                                   @filtro) TAB
                                                          GROUP BY TAB.CSI_NOMBAI) TAB2
                                                         
                                                         GROUP BY TAB2.BAIRRO, TAB2.IMOVEIS_PESQUISADOS, TAB2.TOTAL_POSITIVOS_AEGYPTI, TAB2.IND_AEGYPTI_PRED,
                                                                  TAB2.TOTAL_POSITIVOS_ALBOPICTUS, TAB2.IND_ALBOPICTUS_PRED, TAB2.DEP_AEGYPTI_POS, TAB2.DEP_ALBOPICTUS_POS,
                                                                  TAB2.IND_AEGYPTI_BRET, TAB2.IND_ALBOPICTUS_BRET, TAB2.TOT_REC_POS_AEGYPTI, TAB2.A1_AEGYPTI_COUNT, TAB2.A2_AEGYPTI_COUNT,
                                                                  TAB2.B_AEGYPTI_COUNT,TAB2.C_AEGYPTI_COUNT, TAB2.D1_AEGYPTI_COUNT,TAB2.D2_AEGYPTI_COUNT,TAB2.E_AEGYPTI_COUNT,
                                                                  TAB2.TOT_REC_POS_ALBOPICTUS,TAB2.A1_ALBOPICTUS_COUNT,TAB2.A2_ALBOPICTUS_COUNT,TAB2.B_ALBOPICTUS_COUNT,
                                                                  TAB2.C_ALBOPICTUS_COUNT,TAB2.D1_ALBOPICTUS_COUNT, TAB2.D2_ALBOPICTUS_COUNT,TAB2.E_ALBOPICTUS_COUNT,
                                                                  TAB2.A1_TOT, TAB2.A2_TOT, TAB2.B_TOT, TAB2.C_TOT,
                                                                  TAB2.D1_TOT, TAB2.D2_TOT, TAB2.E_TOT";

        string IReportEndemiasCommand.InfestacaoPredialSintetico { get => sqlInfestacaoPredialSintetico; }

        public string sqlServicoAntivetorialAnalitico = $@"SELECT VI.ID_PROFISSIONAL, ME.CSI_NOMMED,CI.CSI_CODCID,CI.CSI_NOMCID,B.CSI_CODBAI,B.CSI_NOMBAI,UPPER(B.CATEGORIA) AS CATEGORIA,I.QUARTEIRAO_LOGRADOURO,
                                                          I.SEQUENCIA_QUARTEIRAO,I.SEQUENCIA_NUMERO, L.CSI_NOMEND,I.NUMERO_LOGRADOURO, I.COMPLEMENTO_LOGRADOURO, VI.NUMERO_TUBITO, VI.DEPOSITO_ELIMINADO, VI.DESFECHO,
                                                          CASE I.TIPO_IMOVEL WHEN 0 THEN 'R' WHEN 1 THEN 'C' WHEN 2 THEN 'TB' WHEN 3 THEN 'PE' ELSE '0' END TIPO_IMOVEL,
                                                          CASE VI.DESFECHO WHEN 0 THEN '' WHEN 1 THEN 'F' WHEN 2 THEN 'R' END AS TEMPENDENCIA,
                                                          VI.DATA_HORA_ENTRADA, VI.TIPO_VISITA,
                                                          VI.DEPOSITO_INSPECIONADO_A1 AS DI_A1,
                                                          VI.DEPOSITO_INSPECIONADO_A2 AS DI_A2,
                                                          VI.DEPOSITO_INSPECIONADO_B AS DI_B,
                                                          VI.DEPOSITO_INSPECIONADO_C AS DI_C,
                                                          VI.DEPOSITO_INSPECIONADO_D1 AS DI_D1,
                                                          VI.DEPOSITO_INSPECIONADO_D2 AS DI_D2,
                                                          VI.DEPOSITO_INSPECIONADO_E AS DI_E,
                                                          VI.TRAT_FOCAL_LARVI1_TIPO, VI.TRAT_FOCAL_LARVI2_TIPO, VI.TRAT_PERIFOCAL_ADULT_TIPO,
                                                          VI.TRAT_FOCAL_LARVI1_QTD_GRAMAS, VI.TRAT_FOCAL_LARVI2_QTD_GRAMAS,
                                                          VI.TRAT_FOCAL_LARVI1_QTD_DEP_TRAT, VI.TRAT_FOCAL_LARVI2_QTD_DEP_TRAT,
                                                          VI.TRAT_PERIFOCAL_ADULT_QTD_CARGA,
                                                          CASE WHEN ((COALESCE(VI.TRAT_FOCAL_LARVI1_TIPO,-1) > -1) OR (COALESCE(VI.TRAT_FOCAL_LARVI2_TIPO,-1) > -1)
                                                          OR (COALESCE(VI.TRAT_PERIFOCAL_ADULT_TIPO,-1) > -1)) THEN 'X' ELSE '' END AS IMOVEL_TRAT,
                                                          CASE WHEN ((COALESCE(VI.TRAT_FOCAL_LARVI1_TIPO,-1) > -1) OR (COALESCE(VI.TRAT_FOCAL_LARVI2_TIPO,-1) > -1)
                                                          OR (COALESCE(VI.TRAT_PERIFOCAL_ADULT_TIPO,-1) > -1)) THEN 1 ELSE 0 END AS COUNTIMOV_TRAT,
                                                          CASE WHEN (VI.DESFECHO = 0) THEN 'X' ELSE '' END AS IMOVEL_INSPC,
                                                          CASE WHEN (VI.DESFECHO = 0) THEN 1 ELSE 0 END AS COUNTIMOV_INSP,
                                                          CASE WHEN TIPO_VISITA = 0 THEN 'N' ELSE 'R' END AS TIPO_VISITASTR, A_INICIAL, A_FINAL, A_TOTAL
                                                          FROM VS_ESTABELECIMENTOS I
                                                          JOIN VISITA_IMOVEL VI ON VI.ID_ESTABELECIMENTO = I.ID
                                                          JOIN TSI_MEDICOS ME ON ME.CSI_CODMED = VI.ID_PROFISSIONAL
                                                          JOIN TSI_LOGRADOURO L ON L.CSI_CODEND = I.ID_LOGRADOURO
                                                          LEFT JOIN TSI_BAIRRO B ON B.CSI_CODBAI = L.CSI_CODBAI
                                                          LEFT JOIN TSI_CIDADE CI ON CI.CSI_CODCID = B.CSI_CODCID
                                                          LEFT JOIN (
                                                          SELECT CO.ID_VISITA, MIN(AMOSTRA) A_INICIAL, MAX(AMOSTRA) A_FINAL, CAST(COUNT(AMOSTRA)AS INTEGER) A_TOTAL
                                                          FROM VA_COLETA CO
                                                          GROUP BY 1
                                                          )CO ON CO.ID_VISITA = VI.ID
                                                           @filtro
                                                          ORDER BY B.CSI_CODBAI,VI.DATA_HORA_ENTRADA";

        string IReportEndemiasCommand.ServicoAntivetorialAnalitico { get => sqlServicoAntivetorialAnalitico; }

        public string sqlServicoAntivetorialTotalizador = $@"SELECT 'NOME DO MUNICIPIO PEGAR DEPOIS' AS MUNICIPIO, 'NOME DO BAIRRO PEGAR DEPOIS' AS BAIRRO,
                                                             CAST(0 AS BIGINT) AS RESIDENCIA, CAST(0 AS BIGINT) AS COMERCIO, CAST(0 AS BIGINT) AS TERRENO_BALDIO,
                                                             CAST(0 AS BIGINT) AS PONTO_ESTRATEGICO, CAST(0 AS BIGINT) AS OUTRO,
                                                             CAST(0 AS BIGINT) AS TOTAL_TRABALHADO, SUM(CO.AMOSTRAS_COLETADAS) AMOSTRAS_COLETADAS,
                                                             COUNT(DISTINCT VI.ID_PROFISSIONAL) AGENTES_SEMANA,
                                                             COUNT(DISTINCT CAST(DATA_HORA_ENTRADA AS DATE)) DIAS_TRABALHADOS,
                                                             SUM(CASE WHEN ((COALESCE(VI.TRAT_FOCAL_LARVI1_TIPO,'') <> '')
                                                             OR (COALESCE(VI.TRAT_FOCAL_LARVI2_TIPO,'') <> ''))  THEN 1 ELSE 0 END) TRATAMENTO_FOCAL,
                                                             SUM(CASE WHEN COALESCE(VI.TRAT_PERIFOCAL_ADULT_TIPO,'') <> '' THEN 1 ELSE 0 END) TRATAMENTO_PERIFICAL,
                                                             SUM(COALESCE(VI.DEPOSITO_ELIMINADO,0)) DEPOSITO_ELIMINADO,
                                                             SUM(COALESCE(VI.deposito_inspecionado_a1,0)) A1,
                                                             SUM(COALESCE(VI.deposito_inspecionado_a2,0)) A2,
                                                             SUM(COALESCE(VI.deposito_inspecionado_b,0)) B,
                                                             SUM(COALESCE(VI.deposito_inspecionado_c,0)) C,
                                                             SUM(COALESCE(VI.deposito_inspecionado_d1,0)) D1,
                                                             SUM(COALESCE(VI.deposito_inspecionado_d2,0)) D2,
                                                             SUM(COALESCE(VI.deposito_inspecionado_e,0)) E, 0 TOTAL_INSPECIONADO,
                                                             IMOVEL_INSPECIONADO,
                                                             SUM(CASE WHEN VI.desfecho = 1 THEN 1 ELSE 0 END) IMOVEL_FECHADO,
                                                             SUM(CASE WHEN VI.desfecho = 2 THEN 1 ELSE 0 END) IMOVEL_RECUSADO,
                                                             SUM(CASE WHEN VI.desfecho = 3 THEN 1 ELSE 0 END) IMOVEL_DEMOLIDO,
                                                             SUM(CASE WHEN VI.tipo_visita = 1 THEN 1 ELSE 0 END) IMOVEL_RECUPERADO,
                                                             SUM(CASE WHEN VI.atividade = 0 THEN 1 ELSE 0 END) LI,
                                                             SUM(CASE WHEN VI.atividade = 1 THEN 1 ELSE 0 END) LIT,
                                                             SUM(CASE WHEN VI.atividade = 2 THEN 1 ELSE 0 END) PE,
                                                             SUM(CASE WHEN VI.atividade = 3 THEN 1 ELSE 0 END) TRAT,
                                                             SUM(CASE WHEN VI.atividade = 4 THEN 1 ELSE 0 END) DF,
                                                             SUM(CASE WHEN VI.atividade = 5 THEN 1 ELSE 0 END) PVE,
                                                             SUM(CASE WHEN VI.trat_focal_larvi1_tipo = 0 then VI.trat_focal_larvi1_qtd_gramas ELSE 0 END) LARV1_GRAMAS_RESIDUAL,
                                                             SUM(CASE WHEN VI.trat_focal_larvi1_tipo = 1 then VI.trat_focal_larvi1_qtd_gramas ELSE 0 END) LARV1_GRAMAS_ESPACIAL,
                                                             SUM(CASE WHEN VI.trat_focal_larvi1_tipo = 2 then VI.trat_focal_larvi1_qtd_gramas ELSE 0 END) LARV1_GRAMAS_FOCAL,
                                                             SUM(CASE WHEN VI.trat_focal_larvi1_tipo = 0 then VI.trat_focal_larvi1_qtd_dep_trat ELSE 0 END) LARV1_TRATADOS_RESIDUAL,
                                                             SUM(CASE WHEN VI.trat_focal_larvi1_tipo = 1 then VI.trat_focal_larvi1_qtd_dep_trat ELSE 0 END) LARV1_TRATADOS_ESPACIAL,
                                                             SUM(CASE WHEN VI.trat_focal_larvi1_tipo = 2 then VI.trat_focal_larvi1_qtd_dep_trat ELSE 0 END) LARV1_TRATADOS_FOCAL,
                                                             SUM(CASE WHEN VI.trat_focal_larvi2_tipo = 0 then VI.trat_focal_larvi2_qtd_gramas ELSE 0 END) LARV2_GRAMAS_RESIDUAL,
                                                             SUM(CASE WHEN VI.trat_focal_larvi2_tipo = 1 then VI.trat_focal_larvi2_qtd_gramas ELSE 0 END) LARV2_GRAMAS_ESPACIAL,
                                                             SUM(CASE WHEN VI.trat_focal_larvi2_tipo = 2 then VI.trat_focal_larvi2_qtd_gramas ELSE 0 END) LARV2_GRAMAS_FOCAL,
                                                             SUM(CASE WHEN VI.trat_focal_larvi2_tipo = 0 then VI.trat_focal_larvi2_qtd_dep_trat ELSE 0 END) LARV2_TRATADOS_RESIDUAL,
                                                             SUM(CASE WHEN VI.trat_focal_larvi2_tipo = 1 then VI.trat_focal_larvi2_qtd_dep_trat ELSE 0 END) LARV2_TRATADOS_ESPACIAL,
                                                             SUM(CASE WHEN VI.trat_focal_larvi2_tipo = 2 then VI.trat_focal_larvi2_qtd_dep_trat ELSE 0 END) LARV2_TRATADOS_FOCAL,
                                                             SUM(CASE WHEN VI.trat_perifocal_adult_tipo = 0 then VI.trat_perifocal_adult_qtd_carga ELSE 0 END) PERIF_CARGA_RESIDUAL,
                                                             SUM(CASE WHEN VI.trat_perifocal_adult_tipo = 1 then VI.trat_perifocal_adult_qtd_carga ELSE 0 END) PERIF_CARGA_ESPACIAL,
                                                             SUM(CASE WHEN VI.trat_perifocal_adult_tipo = 2 then VI.trat_perifocal_adult_qtd_carga ELSE 0 END) PERIF_CARGA_FOCAL
                                                             FROM VS_ESTABELECIMENTOS I
                                                             JOIN VISITA_IMOVEL VI ON VI.ID_ESTABELECIMENTO = I.ID
                                                             JOIN TSI_LOGRADOURO L ON L.CSI_CODEND = I.ID_LOGRADOURO
                                                             JOIN TSI_BAIRRO B ON B.CSI_CODBAI = L.CSI_CODBAI
                                                             LEFT JOIN (
                                                             SELECT CO.ID_VISITA, COUNT(*) AMOSTRAS_COLETADAS
                                                             FROM VA_COLETA CO
                                                             GROUP BY CO.ID_VISITA
                                                             )CO ON CO.ID_VISITA = VI.ID
                                                             JOIN (SELECT COUNT(DISTINCT I.ID) IMOVEL_INSPECIONADO
                                                               FROM VISITA_IMOVEL VI
                                                               JOIN VS_ESTABELECIMENTOS I ON VI.ID_ESTABELECIMENTO= I.ID
                                                               JOIN TSI_LOGRADOURO L ON L.CSI_CODEND = I.ID_LOGRADOURO
                                                               JOIN TSI_BAIRRO B ON B.CSI_CODBAI = L.CSI_CODBAI
                                                               WHERE @filtro1
                                                               AND VI.DESFECHO = 0) TOTAL ON 1 = 1
                                                             WHERE @filtro2
                                                             GROUP BY RESIDENCIA, COMERCIO, TERRENO_BALDIO, PONTO_ESTRATEGICO, OUTRO, IMOVEL_INSPECIONADO";
        string IReportEndemiasCommand.ServicoAntivetorialTotalizador { get => sqlServicoAntivetorialTotalizador; }

        public string SqlServAntivetorialTrabalhoCampoTotais = $@"SELECT
                                                                  SUM(CASE WHEN TIPO_IMOVEL = 0 THEN QTDE ELSE 0 END) RESIDENCIA,
                                                                  SUM(CASE WHEN TIPO_IMOVEL = 1 THEN QTDE ELSE 0 END) COMERCIO,
                                                                  SUM(CASE WHEN TIPO_IMOVEL = 2 THEN QTDE ELSE 0 END) TERRENO_BALDIO,
                                                                  SUM(CASE WHEN TIPO_IMOVEL = 3 THEN QTDE ELSE 0 END) PONTO_ESTRATEGICO,
                                                                  SUM(CASE WHEN TIPO_IMOVEL = 4 THEN QTDE ELSE 0 END) OUTRO
                                                                  FROM(SELECT 0 TIPO_IMOVEL,0 QTDE FROM RDB$DATABASE UNION ALL
                                                                       SELECT 1 TIPO_IMOVEL,0 QTDE FROM RDB$DATABASE UNION ALL
                                                                       SELECT 2 TIPO_IMOVEL,0 QTDE FROM RDB$DATABASE UNION ALL
                                                                       SELECT 3 TIPO_IMOVEL,0 QTDE FROM RDB$DATABASE UNION ALL
                                                                       SELECT 4 TIPO_IMOVEL,0 QTDE FROM RDB$DATABASE UNION ALL
                                                                       SELECT I.TIPO_IMOVEL, COUNT(TIPO_IMOVEL)QTDE
                                                                       FROM VS_ESTABELECIMENTOS I
                                                                       JOIN VISITA_IMOVEL VI ON VI.ID_ESTABELECIMENTO = I.ID
                                                                           AND VI.ID = (SELECT MAX(ID) FROM VISITA_IMOVEL WHERE ID_IMOVEL = I.ID)
                                                                       JOIN TSI_LOGRADOURO L ON L.CSI_CODEND = I.ID_LOGRADOURO
                                                                       JOIN TSI_BAIRRO B ON B.CSI_CODBAI = L.CSI_CODBAI
                                                                       WHERE VI.DESFECHO = 0 AND
                                                                             @filtro
                                                                       GROUP BY TIPO_IMOVEL)";
        string IReportEndemiasCommand.ServAntivetorialTrabalhoCampoTotais { get => SqlServAntivetorialTrabalhoCampoTotais; }

        public string SqlServAntivetorialResumoLab = $@"SELECT TAB.ID, TAB.ESPECIME, SUM(TAB.QTDE_RESIDENCIA) AS QTDE_RESIDENCIA, SUM(TAB.QTDE_COMERCIO) AS QTDE_COMERCIO,
                                                               SUM(TAB.QTDE_TERRENOBALDIO) AS QTDE_TERRENOBALDIO, SUM(TAB.QTDE_PONTOESTRATEGICO) AS QTDE_PONTOESTRATEGICO,
                                                               SUM(TAB.QTDE_OUTRO) AS QTDE_OUTRO, SUM(TAB.QTDE_A1) QTDE_A1, SUM(TAB.QTDE_A2) QTDE_A2, SUM(TAB.QTDE_B) QTDE_B,
                                                               SUM(TAB.QTDE_C) QTDE_C, SUM(TAB.QTDE_D1) QTDE_D1, SUM(TAB.QTDE_D2) QTDE_D2, SUM(TAB.QTDE_E) QTDE_E,
                                                               SUM(TAB.QTDE_LARVAS) QTDE_LARVAS, SUM(TAB.QTDE_PUPAS) QTDE_PUPAS, SUM(TAB.QTDE_EXUVIA) QTDE_EXUVIA,
                                                               SUM(TAB.QTDE_ADULTO) QTDE_ADULTO
                                                        FROM (SELECT DISTINCT I.ID IMOVEL, E.TIPO_ESPECIME ID, E.ESPECIME,
                                                                              (
                                                                              CASE
                                                                                WHEN I.TIPO_IMOVEL = 0 THEN 1
                                                                                ELSE 0
                                                                              END) QTDE_RESIDENCIA,
                                                                              (
                                                                              CASE
                                                                                WHEN I.TIPO_IMOVEL = 1 THEN 1
                                                                                ELSE 0
                                                                              END) QTDE_COMERCIO,
                                                                              (
                                                                              CASE
                                                                                WHEN I.TIPO_IMOVEL = 2 THEN 1
                                                                                ELSE 0
                                                                              END) QTDE_TERRENOBALDIO,
                                                                              (
                                                                              CASE
                                                                                WHEN I.TIPO_IMOVEL = 3 THEN 1
                                                                                ELSE 0
                                                                              END) QTDE_PONTOESTRATEGICO,
                                                                              (
                                                                              CASE
                                                                                WHEN I.TIPO_IMOVEL = 4 THEN 1
                                                                                ELSE 0
                                                                              END) QTDE_OUTRO,
                                                                              0 QTDE_A1, 0 QTDE_A2, 0 QTDE_B, 0 QTDE_C, 0 QTDE_D1, 0 QTDE_D2, 0 QTDE_E, 0 QTDE_LARVAS,
                                                                              0 QTDE_PUPAS, 0 QTDE_EXUVIA, 0 QTDE_ADULTO
                                                              FROM VS_ESTABELECIMENTOS I
                                                              JOIN VISITA_IMOVEL VI ON VI.ID_ESTABELECIMENTO = I.ID
                                                              JOIN VA_COLETA C ON VI.ID = C.ID_VISITA
                                                              JOIN VA_COLETA_RESULTADO CR ON C.ID = CR.ID_COLETA
                                                              JOIN VA_ESPECIME E ON E.ID = CR.ID_ESPECIME
                                                              JOIN TSI_LOGRADOURO L ON L.CSI_CODEND = I.ID_LOGRADOURO
                                                              JOIN TSI_BAIRRO B ON B.CSI_CODBAI = L.CSI_CODBAI
                                                              WHERE @filtro1
                                                              UNION ALL
                                                              SELECT DISTINCT I.ID IMOVEL, E.TIPO_ESPECIME ID_ESPECIME, E.ESPECIME, 0 QTDE_RESIDENCIA, 0 QTDE_COMERCIO,
                                                                              0 QTDE_TERRENOBALDIO, 0 QTDE_PONTOESTRATEGICO, 0 QTDE_OUTRO, COUNT(
                                                                              CASE
                                                                                WHEN C.DEPOSITO = 0 THEN 1
                                                                              END) QTDE_A1,
                                                                              COUNT(
                                                                              CASE
                                                                                WHEN C.DEPOSITO = 1 THEN 1
                                                                              END) QTDE_A2,
                                                                              COUNT(
                                                                              CASE
                                                                                WHEN C.DEPOSITO = 2 THEN 1
                                                                              END) QTDE_B,
                                                                              COUNT(
                                                                              CASE
                                                                                WHEN C.DEPOSITO = 3 THEN 1
                                                                              END) QTDE_C,
                                                                              COUNT(
                                                                              CASE
                                                                                WHEN C.DEPOSITO = 4 THEN 1
                                                                              END) QTDE_D1,
                                                                              COUNT(
                                                                              CASE
                                                                                WHEN C.DEPOSITO = 5 THEN 1
                                                                              END) QTDE_D2,
                                                                              COUNT(
                                                                              CASE
                                                                                WHEN C.DEPOSITO = 6 THEN 1
                                                                              END) QTDE_E,
                                                                              SUM(
                                                                              CASE
                                                                                WHEN CR.EXEMPLAR = 0 THEN CR.QTDE
                                                                              END) QTDE_LARVAS,
                                                                              SUM(
                                                                              CASE
                                                                                WHEN CR.EXEMPLAR = 1 THEN CR.QTDE
                                                                              END) QTDE_PUPAS,
                                                                              SUM(
                                                                              CASE
                                                                                WHEN CR.EXEMPLAR = 2 THEN CR.QTDE
                                                                              END) QTDE_EXUVIA,
                                                                              SUM(
                                                                              CASE
                                                                                WHEN CR.EXEMPLAR = 3 THEN CR.QTDE
                                                                              END) QTDE_ADULTO
                                                              FROM VA_COLETA C
                                                              INNER JOIN VA_COLETA_RESULTADO CR ON C.ID = CR.ID_COLETA
                                                              INNER JOIN VA_ESPECIME E ON E.ID = CR.ID_ESPECIME
                                                              INNER JOIN VISITA_IMOVEL VI ON VI.ID = C.ID_VISITA
                                                              INNER JOIN VS_ESTABELECIMENTOS I ON I.ID = VI.ID_ESTABELECIMENTO
                                                              INNER JOIN TSI_LOGRADOURO L ON L.CSI_CODEND = I.ID_LOGRADOURO
                                                              INNER JOIN TSI_BAIRRO B ON B.CSI_CODBAI = L.CSI_CODBAI
                                                              WHERE @filtro2
                                                              GROUP BY I.ID, E.TIPO_ESPECIME, E.ESPECIME) AS TAB
                                                        GROUP BY TAB.ESPECIME, TAB.ID;";
        string IReportEndemiasCommand.ServAntivetorialResumoLab { get => SqlServAntivetorialResumoLab; }

        public string SqlServAntivetorialInfectados = $@"SELECT DISTINCT I.QUARTEIRAO_LOGRADOURO || ' / ' || COALESCE(I.SEQUENCIA_QUARTEIRAO, '') AS LOCAL, E.TIPO_ESPECIME AS ESPECIME
                                                         FROM VA_COLETA_RESULTADO R
                                                         INNER JOIN VA_COLETA C ON C.ID = R.ID_COLETA
                                                         INNER JOIN VA_ESPECIME E ON E.ID = R.ID_ESPECIME
                                                         INNER JOIN VISITA_IMOVEL VI ON VI.ID = C.ID_VISITA
                                                         INNER JOIN VS_ESTABELECIMENTOS I ON I.ID = VI.ID_ESTABELECIMENTO
                                                         INNER JOIN TSI_LOGRADOURO L ON L.CSI_CODEND = I.ID_LOGRADOURO
                                                         INNER JOIN TSI_BAIRRO B ON B.CSI_CODBAI = L.CSI_CODBAI
                                                         WHERE E.TIPO_ESPECIME IN (1, 2, 3) AND
                                                               @filtro";
        string IReportEndemiasCommand.ServAntivetorialInfectados { get => SqlServAntivetorialInfectados; }
    }
}
