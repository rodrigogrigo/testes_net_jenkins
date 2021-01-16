using RgCidadao.Domain.Commands.Indicadores;

namespace RgCidadao.Domain.Queries.Indicadores
{
    public class Indicador7CommandText : IIndicador7Command
    {
        private string sqlIndicador7 = $@" @select
                                              COUNT(*) QTDE_INDIVIDUOS,
                                          
                                              -- HBA1C (SISTEMA)
                                              SUM(CASE WHEN ((SELECT COUNT(DISTINCT PA.ID)
                                              FROM PEP_REQUISICAO_EXAME RE
                                              JOIN PEP_ATENDIMENTO PA ON PA.ID = RE.ID_ATENDIMENTO
                                              JOIN TSI_CADEXAMES EX ON EX.CSI_CODEXA = RE.ID_EXAME
                                              JOIN PEP_CONDICAO_AVALIADA CA ON CA.CSI_ID_ATEND = PA.ID AND
                                              ((CA.ID_CIAP IN ('T89','T90','W85')) OR
                                              (CA.ID_CID IN ('E10','E100', 'E101','E102','E103','E104','E105','E106','E107',
                                              'E108','E109','E11', 'E110','E111','E112','E113','E114','E115',
                                              'E116','E117','E118','E119','E12', 'E120','E121','E122','E123',
                                              'E124','E125','E126','E127','E128','E129','E13', 'E130','E131',
                                              'E132','E133','E134','E135','E136','E137','E138','E139','E14',
                                              'E140','E141','E142','E143','E144','E145','E146','E147','E148',
                                              'E149','O24', '0240','O241','O242','O243','O244','O249','P702')))
                                              WHERE PA.ID_PACIENTE = P.CSI_CODPAC AND
                                              (SUBSTRING(PA.ID_CBO FROM 1 FOR 4) IN ('2251', '2252', '2253', '2231', '2235')) AND
                                              EX.CSI_CODSUS = '0202010503' AND
                                              (PA.DATA BETWEEN
                                              (SELECT MES_ANTERIOR FROM COMPETENCIA_ANTERIOR((SELECT DATA FROM FECHAMENTO_QUADRIMESTRE(@quadrimestre, @ano)), 11))
                                              AND (SELECT DATA FROM FECHAMENTO_QUADRIMESTRE(@quadrimestre, @ano))) ) > 0)
                                              THEN 1 ELSE 0 END) AS QTDE_METAS,
                                          
                                              -- HBA1C (VALIDO)
                                              SUM(CASE WHEN ((SELECT COUNT(DISTINCT PA.ID)
                                              FROM PEP_REQUISICAO_EXAME RE
                                              JOIN PEP_ATENDIMENTO PA ON PA.ID = RE.ID_ATENDIMENTO
                                              JOIN ESUS_EXPORTACAO_ITEM EI ON EI.ID = PA.ID_ESUS_EXPORTACAO_ITEM
                                              JOIN ESUS_EXPORTACAO E ON E.ID = EI.ID_EXPORTACAO
                                              JOIN TSI_CADEXAMES EX ON EX.CSI_CODEXA = RE.ID_EXAME
                                              JOIN PEP_CONDICAO_AVALIADA CA ON CA.CSI_ID_ATEND = PA.ID AND
                                              ((CA.ID_CIAP IN ('T89','T90','W85')) OR
                                              (CA.ID_CID IN ('E10','E100', 'E101','E102','E103','E104','E105','E106','E107',
                                              'E108','E109','E11', 'E110','E111','E112','E113','E114','E115',
                                              'E116','E117','E118','E119','E12', 'E120','E121','E122','E123',
                                              'E124','E125','E126','E127','E128','E129','E13', 'E130','E131',
                                              'E132','E133','E134','E135','E136','E137','E138','E139','E14',
                                              'E140','E141','E142','E143','E144','E145','E146','E147','E148',
                                              'E149','O24', '0240','O241','O242','O243','O244','O249','P702')))
                                              WHERE PA.ID_PACIENTE = P.CSI_CODPAC AND
                                              (E.PROCESSADO_CRITICAS = 'T') AND
                                              ((EI.FLG_ERRO <> 'T') OR (EI.FLG_ERRO IS NULL)) AND
                                              ((P.CSI_NCARTAO IS NOT NULL AND
                                              P.CSI_NCARTAO <> '') OR (P.CSI_CPFPAC IS NOT NULL AND
                                              P.CSI_CPFPAC <> '')) AND
                                              (SUBSTRING(PA.ID_CBO FROM 1 FOR 4) IN ('2251', '2252', '2253', '2231', '2235')) AND
                                              EX.CSI_CODSUS = '0202010503' AND
                                              (PA.DATA BETWEEN
                                              (SELECT MES_ANTERIOR FROM COMPETENCIA_ANTERIOR((SELECT DATA FROM FECHAMENTO_QUADRIMESTRE(@quadrimestre, @ano)), 11))
                                              AND (SELECT DATA FROM FECHAMENTO_QUADRIMESTRE(@quadrimestre, @ano))) ) > 0)
                                              THEN 1 ELSE 0 END) AS QTDE_METAS_VALIDAS
                                          
                                          FROM TSI_CADPAC P
                                          JOIN ESUS_FAMILIA FAM ON FAM.ID = P.ID_FAMILIA 
                                          JOIN VS_ESTABELECIMENTOS EST ON EST.ID = FAM.ID_DOMICILIO 
                                          JOIN ESUS_MICROAREA MA ON MA.ID = EST.ID_MICROAREA
                                          JOIN TSI_MEDICOS ME ON ME.CSI_CODMED = MA.ID_PROFISSIONAL
                                          JOIN ESUS_EQUIPES EQ ON EQ.ID = MA.ID_EQUIPE
                                          JOIN ESUS_ESTABELECIMENTO_SAUDE ES ON ES.ID = EQ.ID_ESTABELECIMENTO
                                          WHERE P.DIABETES = 'T'
                                          @filtros
                                          @agrupamento";

        string IIndicador7Command.Indicador7 { get => sqlIndicador7; }

        private string sqlPublicoAlvo = $@" @select
                                               P.CSI_CODPAC ID_INDIVIDUO,
                                               P.CSI_NOMPAC INDIVIDUO,
                                               EQ.NOME_REFERENCIA EQUIPE,
                                           
                                               -- HBA1C (SISTEMA)
                                               CASE WHEN ((SELECT COUNT(DISTINCT PA.ID)
                                               FROM PEP_REQUISICAO_EXAME RE
                                               JOIN PEP_ATENDIMENTO PA ON PA.ID = RE.ID_ATENDIMENTO
                                               JOIN TSI_CADEXAMES EX ON EX.CSI_CODEXA = RE.ID_EXAME
                                               JOIN PEP_CONDICAO_AVALIADA CA ON CA.CSI_ID_ATEND = PA.ID AND
                                               ((CA.ID_CIAP IN ('T89','T90','W85')) OR
                                               (CA.ID_CID IN ('E10','E100', 'E101','E102','E103','E104','E105','E106','E107',
                                               'E108','E109','E11', 'E110','E111','E112','E113','E114','E115',
                                               'E116','E117','E118','E119','E12', 'E120','E121','E122','E123',
                                               'E124','E125','E126','E127','E128','E129','E13', 'E130','E131',
                                               'E132','E133','E134','E135','E136','E137','E138','E139','E14',
                                               'E140','E141','E142','E143','E144','E145','E146','E147','E148',
                                               'E149','O24', '0240','O241','O242','O243','O244','O249','P702')))
                                               WHERE PA.ID_PACIENTE = P.CSI_CODPAC AND
                                               (SUBSTRING(PA.ID_CBO FROM 1 FOR 4) IN ('2251', '2252', '2253', '2231', '2235')) AND
                                               (SELECT PAI FROM PRO_PROCEDIMENTO_PAI(EX.CSI_CODSUS)) IN ('0202010503') AND
                                               (PA.DATA BETWEEN
                                               (SELECT MES_ANTERIOR FROM COMPETENCIA_ANTERIOR((SELECT DATA FROM FECHAMENTO_QUADRIMESTRE(@quadrimestre, @ano)), 11))
                                               AND (SELECT DATA FROM FECHAMENTO_QUADRIMESTRE(@quadrimestre, @ano)))) > 0)
                                               THEN 1 ELSE 0 END AS HBA1C,
                                           
                                               -- HBA1C (SISTEMA)
                                               CASE WHEN ((SELECT COUNT(DISTINCT PA.ID)
                                               FROM PEP_REQUISICAO_EXAME RE
                                               JOIN PEP_ATENDIMENTO PA ON PA.ID = RE.ID_ATENDIMENTO
                                               JOIN ESUS_EXPORTACAO_ITEM EI ON EI.ID = PA.ID_ESUS_EXPORTACAO_ITEM
                                               JOIN ESUS_EXPORTACAO E ON E.ID = EI.ID_EXPORTACAO
                                               JOIN TSI_CADEXAMES EX ON EX.CSI_CODEXA = RE.ID_EXAME
                                               JOIN PEP_CONDICAO_AVALIADA CA ON CA.CSI_ID_ATEND = PA.ID AND
                                               ((CA.ID_CIAP IN ('T89','T90','W85')) OR
                                               (CA.ID_CID IN ('E10','E100', 'E101','E102','E103','E104','E105','E106','E107',
                                               'E108','E109','E11', 'E110','E111','E112','E113','E114','E115',
                                               'E116','E117','E118','E119','E12', 'E120','E121','E122','E123',
                                               'E124','E125','E126','E127','E128','E129','E13', 'E130','E131',
                                               'E132','E133','E134','E135','E136','E137','E138','E139','E14',
                                               'E140','E141','E142','E143','E144','E145','E146','E147','E148',
                                               'E149','O24', '0240','O241','O242','O243','O244','O249','P702')))
                                               WHERE PA.ID_PACIENTE = P.CSI_CODPAC AND
                                               ((P.CSI_NCARTAO IS NOT NULL AND P.CSI_NCARTAO <> '') OR
                                               (P.CSI_CPFPAC IS NOT NULL AND P.CSI_CPFPAC <> '')) AND
                                               (E.PROCESSADO_CRITICAS = 'T') AND
                                               ((EI.FLG_ERRO <> 'T') OR (EI.FLG_ERRO IS NULL)) AND
                                               (SUBSTRING(PA.ID_CBO FROM 1 FOR 4) IN ('2251', '2252', '2253', '2231', '2235')) AND
                                               (SELECT PAI FROM PRO_PROCEDIMENTO_PAI(EX.CSI_CODSUS)) IN ('0202010503') AND
                                               (PA.DATA BETWEEN
                                               (SELECT MES_ANTERIOR FROM COMPETENCIA_ANTERIOR((SELECT DATA FROM FECHAMENTO_QUADRIMESTRE(@quadrimestre, @ano)), 11))
                                               AND (SELECT DATA FROM FECHAMENTO_QUADRIMESTRE(@quadrimestre, @ano)))) > 0)
                                               THEN 1 ELSE 0 END AS HBA1C_VALIDO
                                           
                                           FROM TSI_CADPAC P
                                           JOIN ESUS_FAMILIA FAM ON FAM.ID = P.ID_FAMILIA 
                                           JOIN VS_ESTABELECIMENTOS EST ON EST.ID = FAM.ID_DOMICILIO 
                                           JOIN ESUS_MICROAREA MA ON MA.ID = EST.ID_MICROAREA
                                           JOIN TSI_MEDICOS ME ON ME.CSI_CODMED = MA.ID_PROFISSIONAL
                                           JOIN ESUS_EQUIPES EQ ON EQ.ID = MA.ID_EQUIPE
                                           JOIN ESUS_ESTABELECIMENTO_SAUDE ES ON ES.ID = EQ.ID_ESTABELECIMENTO
                                           WHERE P.DIABETES = 'T'
                                           @filtros
                                           ORDER BY P.CSI_NOMPAC";

        string IIndicador7Command.PublicoAlvo { get => sqlPublicoAlvo; }

        private string sqlCountPublicoAlvo = $@"SELECT COUNT(*) FROM (
                                                  SELECT P.CSI_CODPAC
                                                  FROM TSI_CADPAC P
                                                  JOIN ESUS_FAMILIA FAM ON FAM.ID = P.ID_FAMILIA 
                                                  JOIN VS_ESTABELECIMENTOS EST ON EST.ID = FAM.ID_DOMICILIO 
                                                  JOIN ESUS_MICROAREA MA ON MA.ID = EST.ID_MICROAREA
                                                  JOIN TSI_MEDICOS ME ON ME.CSI_CODMED = MA.ID_PROFISSIONAL
                                                  JOIN ESUS_EQUIPES EQ ON EQ.ID = MA.ID_EQUIPE
                                                  JOIN ESUS_ESTABELECIMENTO_SAUDE ES ON ES.ID = EQ.ID_ESTABELECIMENTO
                                                  WHERE P.DIABETES = 'T'
                                                  @filtros)";

        string IIndicador7Command.CountPublicoAlvo { get => sqlCountPublicoAlvo; }

        private string sqlAtendimentos = $@"SELECT
                                               PA.DATA DATA,
                                               PROF.CSI_NOMMED PROFISSIONAL,
                                               PA.ID_CBO CBO,
                                               CBO.DESCRICAO DESCRICAO_CBO,
                                           
                                               COALESCE((SELECT 'CIAP: '||LIST(COALESCE(CA.ID_CIAP, '')) || ' | ' || 'CID: '||LIST(COALESCE(CA.ID_CID, ''))
                                               FROM PEP_CONDICAO_AVALIADA CA WHERE CA.CSI_ID_ATEND = PA.ID AND (CA.ID_CIAP IS NOT NULL OR CA.ID_CID IS NOT NULL)), '--') AS CONDICAO_AVALIADA,
                                           
                                               EX.CSI_NOME EXAME,
                                               EQ.DSC_AREA EQUIPE,
                                               U.CSI_NOMUNI UNIDADE_SAUDE,
                                               CASE WHEN (COALESCE(E.PROCESSADO_CRITICAS, 'F') = 'T') THEN 1 ELSE 0 END AS PROCESSADO_CRITICAS,
                                               CASE WHEN (COALESCE(EI.FLG_ERRO, 'F') = 'T') THEN 1 ELSE 0 END AS FLG_ERRO,
                                           
                                               CASE WHEN (E.ID IS NULL) THEN 'Registro não exportado'
                                               WHEN (COALESCE(E.PROCESSADO_CRITICAS, 'F') = 'T') THEN EI.DESCRICAO_ERRO
                                               ELSE 'Arquivo de críticas não processado'
                                               END AS DESCRICAO_ERRO,
                                           
                                               CASE WHEN (SUBSTRING(PA.ID_CBO FROM 1 FOR 4) IN ('2251', '2252', '2253', '2231', '2235'))
                                               THEN 1 ELSE 0 END AS CBO_VALIDO,
                                           
                                               CASE WHEN ((SELECT COUNT(DISTINCT AUX.ID) FROM PEP_ATENDIMENTO AUX
                                               JOIN PEP_CONDICAO_AVALIADA CA ON CA.CSI_ID_ATEND = AUX.ID AND
                                               ((CA.ID_CIAP IN ('T89','T90','W85')) OR
                                               (CA.ID_CID IN ('E10','E100', 'E101','E102','E103','E104','E105','E106','E107',
                                               'E108','E109','E11', 'E110','E111','E112','E113','E114','E115',
                                               'E116','E117','E118','E119','E12', 'E120','E121','E122','E123',
                                               'E124','E125','E126','E127','E128','E129','E13', 'E130','E131',
                                               'E132','E133','E134','E135','E136','E137','E138','E139','E14',
                                               'E140','E141','E142','E143','E144','E145','E146','E147','E148',
                                               'E149','O24', '0240','O241','O242','O243','O244','O249','P702')))
                                               WHERE PA.ID = AUX.ID) > 0)
                                               THEN 1 ELSE 0 END AS CID_CIAP_VALIDO,
                                           
                                               CASE WHEN (PA.DATA BETWEEN
                                               (SELECT MES_ANTERIOR FROM COMPETENCIA_ANTERIOR((SELECT DATA FROM FECHAMENTO_QUADRIMESTRE(NULL, @ano)), 11))
                                               AND (SELECT DATA FROM FECHAMENTO_QUADRIMESTRE(NULL, @ano)))
                                               THEN 1 ELSE 0 END AS DATA_VALIDA,
                                           
                                               CASE WHEN ((SELECT COUNT(DISTINCT AUX.ID) FROM PEP_ATENDIMENTO AUX
                                               JOIN PEP_CONDICAO_AVALIADA CA ON CA.CSI_ID_ATEND = AUX.ID AND
                                               ((CA.ID_CIAP IN ('T89','T90','W85')) OR
                                               (CA.ID_CID IN ('E10','E100', 'E101','E102','E103','E104','E105','E106','E107',
                                               'E108','E109','E11', 'E110','E111','E112','E113','E114','E115',
                                               'E116','E117','E118','E119','E12', 'E120','E121','E122','E123',
                                               'E124','E125','E126','E127','E128','E129','E13', 'E130','E131',
                                               'E132','E133','E134','E135','E136','E137','E138','E139','E14',
                                               'E140','E141','E142','E143','E144','E145','E146','E147','E148',
                                               'E149','O24', '0240','O241','O242','O243','O244','O249','P702')))
                                               WHERE PA.ID = AUX.ID AND
                                               (SUBSTRING(PA.ID_CBO FROM 1 FOR 4) IN ('2251', '2252', '2253', '2231', '2235')) AND
                                               (PA.DATA BETWEEN (SELECT MES_ANTERIOR FROM COMPETENCIA_ANTERIOR((SELECT DATA FROM FECHAMENTO_QUADRIMESTRE(NULL, @ano)), 11))
                                               AND (SELECT DATA FROM FECHAMENTO_QUADRIMESTRE(NULL, @ano)))) > 0)
                                               THEN 1 ELSE 0 END AS INDICADOR,
                                           
                                               CASE WHEN ((SELECT COUNT(DISTINCT AUX.ID) FROM PEP_ATENDIMENTO AUX
                                               JOIN ESUS_EXPORTACAO_ITEM EI ON EI.ID = AUX.ID_ESUS_EXPORTACAO_ITEM
                                               JOIN ESUS_EXPORTACAO E ON E.ID = EI.ID_EXPORTACAO
                                               JOIN PEP_CONDICAO_AVALIADA CA ON CA.CSI_ID_ATEND = AUX.ID AND
                                               ((CA.ID_CIAP IN ('T89','T90','W85')) OR
                                               (CA.ID_CID IN ('E10','E100', 'E101','E102','E103','E104','E105','E106','E107',
                                               'E108','E109','E11', 'E110','E111','E112','E113','E114','E115',
                                               'E116','E117','E118','E119','E12', 'E120','E121','E122','E123',
                                               'E124','E125','E126','E127','E128','E129','E13', 'E130','E131',
                                               'E132','E133','E134','E135','E136','E137','E138','E139','E14',
                                               'E140','E141','E142','E143','E144','E145','E146','E147','E148',
                                               'E149','O24', '0240','O241','O242','O243','O244','O249','P702')))
                                               WHERE PA.ID = AUX.ID AND
                                               (E.PROCESSADO_CRITICAS = 'T') AND
                                               ((EI.FLG_ERRO <> 'T') OR (EI.FLG_ERRO IS NULL)) AND
                                               (SUBSTRING(PA.ID_CBO FROM 1 FOR 4) IN ('2251', '2252', '2253', '2231', '2235')) AND
                                               (PA.DATA BETWEEN (SELECT MES_ANTERIOR FROM COMPETENCIA_ANTERIOR((SELECT DATA FROM FECHAMENTO_QUADRIMESTRE(NULL, @ano)), 11))
                                               AND (SELECT DATA FROM FECHAMENTO_QUADRIMESTRE(NULL, @ano)))) > 0)
                                               THEN 1 ELSE 0 END AS INDICADOR_VALIDO,
                                               E.LOTE AS LOTE
                                           
                                           FROM PEP_REQUISICAO_EXAME RE
                                           JOIN PEP_ATENDIMENTO PA ON PA.ID = RE.ID_ATENDIMENTO
                                           JOIN TSI_CADEXAMES EX ON EX.CSI_CODEXA = RE.ID_EXAME
                                           JOIN TSI_MEDICOS PROF ON PROF.CSI_CODMED = PA.ID_MEDICO
                                           JOIN TSI_CBO CBO ON CBO.CODIGO = PA.ID_CBO
                                           JOIN TSI_UNIDADE U ON PA.ID_UNIDADE = U.CSI_CODUNI
                                           LEFT JOIN ESUS_EXPORTACAO_ITEM EI ON EI.ID = PA.ID_ESUS_EXPORTACAO_ITEM
                                           LEFT JOIN ESUS_EXPORTACAO E ON E.ID = EI.ID_EXPORTACAO
                                           LEFT JOIN ESUS_EQUIPES EQ ON EQ.ID = PA.ID_EQUIPE
                                           WHERE PA.ID_PACIENTE = @id_individuo AND
                                           (SELECT PAI FROM PRO_PROCEDIMENTO_PAI(EX.CSI_CODSUS)) IN ('0202010503')
                                           ORDER BY PA.DATA";

        string IIndicador7Command.Atendimentos { get => sqlAtendimentos; }
    }
}
