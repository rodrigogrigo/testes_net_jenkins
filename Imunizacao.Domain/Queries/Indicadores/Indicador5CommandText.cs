using RgCidadao.Domain.Commands.Indicadores;

namespace RgCidadao.Domain.Queries.Indicadores
{
    public class Indicador5CommandText : IIndicador5Command
    {
        public string sqlIndicador5 = $@" @select
                                           COUNT(*) QTDE_INDIVIDUOS,
                                       
                                           SUM((CASE WHEN (SELECT COUNT(DISTINCT V.ID)
                                           FROM PNI_VACINADOS V
                                           JOIN PNI_PRODUTO P ON (V.ID_PRODUTO = P.ID)
                                           JOIN PNI_DOSE D ON D.ID = V.ID_DOSE
                                           WHERE V.ID_PACIENTE = C.CSI_CODPAC AND
                                           ((SUBSTRING(V.CBO FROM 1 FOR 4) IN ('2251', '2252', '2253', '2231', '2235', '3222')) OR (V.REGISTRO_ANTERIOR = 'T')) AND
                                           P.ID = 22 AND D.SIGLA = 'D3') > 0
                                           THEN 1 ELSE 0 END)) AS QTDE_POLIO,
                                       
                                           SUM((CASE WHEN (SELECT COUNT(DISTINCT V.ID)
                                           FROM PNI_VACINADOS V
                                           JOIN PNI_PRODUTO PR ON (V.ID_PRODUTO = PR.ID)
                                           JOIN PNI_DOSE D ON D.ID = V.ID_DOSE
                                           WHERE V.ID_PACIENTE = C.CSI_CODPAC AND
                                           ((SUBSTRING(V.CBO FROM 1 FOR 4) IN ('2251', '2252', '2253', '2231', '2235', '3222')) OR (V.REGISTRO_ANTERIOR = 'T')) AND
                                           PR.ID = 42 AND D.SIGLA = 'D3') > 0 THEN 1
                                           ELSE 0 END)) AS QTDE_PENTA,
                                       
                                           -- METAS (SISTEMA)
                                           SUM((CASE WHEN ((SELECT COUNT(DISTINCT V.ID)
                                           FROM PNI_VACINADOS V
                                           JOIN PNI_PRODUTO PR ON (V.ID_PRODUTO = PR.ID)
                                           JOIN PNI_DOSE D ON D.ID = V.ID_DOSE
                                           WHERE V.ID_PACIENTE = C.CSI_CODPAC AND
                                           ((SUBSTRING(V.CBO FROM 1 FOR 4) IN ('2251','2252','2253','2231','2235','3222')) OR (V.REGISTRO_ANTERIOR = 'T')) AND
                                           PR.ID = 22 AND D.SIGLA = 'D3') > 0)
                                           AND
                                           ((SELECT COUNT(DISTINCT V.ID)
                                           FROM PNI_VACINADOS V
                                           JOIN PNI_PRODUTO PR ON (V.ID_PRODUTO = PR.ID)
                                           JOIN PNI_DOSE D ON D.ID = V.ID_DOSE
                                           WHERE V.ID_PACIENTE = C.CSI_CODPAC AND
                                           ((SUBSTRING(V.CBO FROM 1 FOR 4) IN ('2251','2252','2253','2231','2235','3222')) OR (V.REGISTRO_ANTERIOR = 'T')) AND
                                           PR.ID = 42 AND D.SIGLA = 'D3') > 0)
                                           THEN 1 ELSE 0 END)) AS QTDE_METAS,
                                       
                                           -- METAS (VALIDO)
                                           SUM((CASE WHEN ((SELECT COUNT(DISTINCT V.ID)
                                           FROM PNI_VACINADOS V
                                           JOIN PNI_PRODUTO PR ON (V.ID_PRODUTO = PR.ID)
                                           JOIN PNI_DOSE D ON D.ID = V.ID_DOSE
                                           JOIN ESUS_EXPORTACAO_ITEM EI ON EI.ID = V.ID_ESUS_EXPORTACAO_ITEM
                                           JOIN ESUS_EXPORTACAO E ON E.ID = EI.ID_EXPORTACAO
                                           WHERE V.ID_PACIENTE = C.CSI_CODPAC AND
                                           (E.PROCESSADO_CRITICAS = 'T') AND
                                           ((EI.FLG_ERRO <> 'T') OR (EI.FLG_ERRO IS NULL)) AND
                                           ((SUBSTRING(V.CBO FROM 1 FOR 4) IN ('2251','2252','2253','2231','2235','3222')) OR (V.REGISTRO_ANTERIOR = 'T')) AND
                                           PR.ID = 22 AND D.SIGLA = 'D3') > 0)
                                           AND
                                           ((C.CSI_NCARTAO IS NOT NULL AND C.CSI_NCARTAO <> '') OR
                                           (C.CSI_CPFPAC IS NOT NULL AND C.CSI_CPFPAC <> ''))
                                           AND
                                           ((SELECT COUNT(DISTINCT V.ID)
                                           FROM PNI_VACINADOS V
                                           JOIN PNI_PRODUTO PR ON (V.ID_PRODUTO = PR.ID)
                                           JOIN PNI_DOSE D ON D.ID = V.ID_DOSE
                                           JOIN ESUS_EXPORTACAO_ITEM EI ON EI.ID = V.ID_ESUS_EXPORTACAO_ITEM
                                           JOIN ESUS_EXPORTACAO E ON E.ID = EI.ID_EXPORTACAO
                                           WHERE V.ID_PACIENTE = C.CSI_CODPAC AND
                                           (E.PROCESSADO_CRITICAS = 'T') AND
                                           ((EI.FLG_ERRO <> 'T') OR (EI.FLG_ERRO IS NULL)) AND
                                           ((SUBSTRING(V.CBO FROM 1 FOR 4) IN ('2251','2252','2253','2231','2235','3222')) OR (V.REGISTRO_ANTERIOR = 'T')) AND
                                           PR.ID = 42 AND D.SIGLA = 'D3') > 0)
                                           THEN 1 ELSE 0 END)) AS QTDE_METAS_VALIDAS
                                       
                                       FROM TSI_CADPAC C
                                       JOIN ESUS_FAMILIA FAM ON FAM.ID = C.ID_FAMILIA
                                       JOIN VS_ESTABELECIMENTOS EST ON EST.ID = FAM.ID_DOMICILIO
                                       JOIN ESUS_MICROAREA MA ON MA.ID = EST.ID_MICROAREA
                                       JOIN TSI_MEDICOS ME ON ME.CSI_CODMED = MA.ID_PROFISSIONAL
                                       JOIN ESUS_EQUIPES EQ ON EQ.ID = MA.ID_EQUIPE
                                       JOIN ESUS_ESTABELECIMENTO_SAUDE ES ON ES.ID = EQ.ID_ESTABELECIMENTO
                                       WHERE (C.CSI_DTNASC >=
                                                (SELECT MES_ANTERIOR FROM COMPETENCIA_ANTERIOR((SELECT DATA FROM FECHAMENTO_QUADRIMESTRE(@quadrimestre, @ano)) , 15)))
                                       @filtros
                                       @agrupamento";// (SELECT DATA FROM FECHAMENTO_QUADRIMESTRE(@quadrimestre, @ano)

        string IIndicador5Command.Indicador5 { get => sqlIndicador5; }

        public string sqlPublicoAlvo = $@" @select  
                                                    C.CSI_CODPAC ID_INDIVIDUO,
                                                    C.CSI_NOMPAC INDIVIDUO,
                                                    CAST((SELECT IDADE FROM PRO_IDADE_PACIENTE(C.CSI_DTNASC, CURRENT_DATE)) AS NUMERIC(15,2)) AS IDADE,
                                                    EQ.NOME_REFERENCIA EQUIPE,

                                                    -- POLIO (SISTEMA)
                                                    SUM((CASE WHEN (SELECT COUNT(DISTINCT V.ID)
                                                    FROM PNI_VACINADOS V
                                                    JOIN PNI_PRODUTO P ON (V.ID_PRODUTO = P.ID)
                                                    JOIN PNI_DOSE D ON D.ID = V.ID_DOSE
                                                    WHERE V.ID_PACIENTE = C.CSI_CODPAC AND
                                                    ((SUBSTRING(V.CBO FROM 1 FOR 4) IN ('2251', '2252', '2253', '2231', '2235', '3222')) OR (V.REGISTRO_ANTERIOR = 'T')) AND
                                                    P.ID = 22 AND D.SIGLA = 'D3') > 0
                                                    THEN 1 ELSE 0 END)) AS POLIO,

                                                    -- PENTA (SISTEMA)
                                                    SUM((CASE WHEN (SELECT COUNT(DISTINCT V.ID)
                                                    FROM PNI_VACINADOS V
                                                    JOIN PNI_PRODUTO PR ON (V.ID_PRODUTO = PR.ID)
                                                    JOIN PNI_DOSE D ON D.ID = V.ID_DOSE
                                                    WHERE V.ID_PACIENTE = C.CSI_CODPAC AND
                                                    ((SUBSTRING(V.CBO FROM 1 FOR 4) IN ('2251', '2252', '2253', '2231', '2235', '3222')) OR (V.REGISTRO_ANTERIOR = 'T')) AND
                                                    PR.ID = 42 AND D.SIGLA = 'D3') > 0
                                                    THEN 1 ELSE 0 END)) AS PENTA,

                                                    -- POLIO (VALIDA)
                                                    SUM((CASE WHEN (SELECT COUNT(DISTINCT V.ID)
                                                    FROM PNI_VACINADOS V
                                                    JOIN PNI_PRODUTO P ON (V.ID_PRODUTO = P.ID)
                                                    JOIN PNI_DOSE D ON D.ID = V.ID_DOSE
                                                    JOIN ESUS_EXPORTACAO_ITEM EI ON EI.ID = V.ID_ESUS_EXPORTACAO_ITEM
                                                    JOIN ESUS_EXPORTACAO E ON E.ID = EI.ID_EXPORTACAO
                                                    WHERE V.ID_PACIENTE = C.CSI_CODPAC AND
                                                    ((C.CSI_NCARTAO IS NOT NULL AND C.CSI_NCARTAO <> '') OR
                                                    (C.CSI_CPFPAC IS NOT NULL AND C.CSI_CPFPAC <> '')) AND
                                                    (E.PROCESSADO_CRITICAS = 'T') AND 
                                                    ((EI.FLG_ERRO <> 'T') OR (EI.FLG_ERRO IS NULL)) AND
                                                    ((SUBSTRING(V.CBO FROM 1 FOR 4) IN ('2251', '2252', '2253', '2231', '2235', '3222'))) AND
                                                    P.ID = 22 AND D.SIGLA = 'D3') > 0
                                                    THEN 1 ELSE 0 END)) AS POLIO_VALIDA,

                                                    -- PENTA (VALIDA)
                                                    SUM((CASE WHEN (SELECT COUNT(DISTINCT V.ID)
                                                    FROM PNI_VACINADOS V
                                                    JOIN PNI_PRODUTO PR ON (V.ID_PRODUTO = PR.ID)
                                                    JOIN PNI_DOSE D ON D.ID = V.ID_DOSE
                                                    JOIN ESUS_EXPORTACAO_ITEM EI ON EI.ID = V.ID_ESUS_EXPORTACAO_ITEM
                                                    JOIN ESUS_EXPORTACAO E ON E.ID = EI.ID_EXPORTACAO
                                                    WHERE V.ID_PACIENTE = C.CSI_CODPAC AND
                                                    ((C.CSI_NCARTAO IS NOT NULL AND C.CSI_NCARTAO <> '') OR
                                                    (C.CSI_CPFPAC IS NOT NULL AND C.CSI_CPFPAC <> '')) AND
                                                    (E.PROCESSADO_CRITICAS = 'T') AND 
                                                    ((EI.FLG_ERRO <> 'T') OR (EI.FLG_ERRO IS NULL)) AND
                                                    ((SUBSTRING(V.CBO FROM 1 FOR 4) IN ('2251', '2252', '2253', '2231', '2235', '3222'))) AND
                                                    PR.ID = 42 AND D.SIGLA = 'D3') > 0
                                                    THEN 1 ELSE 0 END)) AS PENTA_VALIDA

                                                FROM TSI_CADPAC C
                                                JOIN ESUS_FAMILIA FAM ON FAM.ID = C.ID_FAMILIA
                                                JOIN VS_ESTABELECIMENTOS EST ON EST.ID = FAM.ID_DOMICILIO
                                                JOIN ESUS_MICROAREA MA ON MA.ID = EST.ID_MICROAREA
                                                JOIN TSI_MEDICOS ME ON ME.CSI_CODMED = MA.ID_PROFISSIONAL
                                                JOIN ESUS_EQUIPES EQ ON EQ.ID = MA.ID_EQUIPE
                                                JOIN ESUS_ESTABELECIMENTO_SAUDE ES ON ES.ID = EQ.ID_ESTABELECIMENTO
                                                WHERE (C.CSI_DTNASC >=
                                                (SELECT MES_ANTERIOR FROM COMPETENCIA_ANTERIOR((SELECT DATA FROM FECHAMENTO_QUADRIMESTRE(@quadrimestre, @ano)) , 15)))
                                                @filtros
                                                GROUP BY C.CSI_CODPAC, C.CSI_NOMPAC, C.CSI_DTNASC, (SELECT IDADE
                                                FROM PRO_IDADE_PACIENTE(C.CSI_DTNASC, CURRENT_DATE)), EQ.NOME_REFERENCIA
                                                ORDER BY C.CSI_NOMPAC";

        string IIndicador5Command.PublicoAlvo { get => sqlPublicoAlvo; }

        public string sqlCountPublicoAlvo = $@"SELECT COUNT(*) FROM (
                                                   SELECT C.CSI_CODPAC
                                                   FROM TSI_CADPAC C
                                                   JOIN ESUS_FAMILIA FAM ON FAM.ID = C.ID_FAMILIA
                                                   JOIN VS_ESTABELECIMENTOS EST ON EST.ID = FAM.ID_DOMICILIO
                                                   JOIN ESUS_MICROAREA MA ON MA.ID = EST.ID_MICROAREA
                                                   JOIN TSI_MEDICOS ME ON ME.CSI_CODMED = MA.ID_PROFISSIONAL
                                                   JOIN ESUS_EQUIPES EQ ON EQ.ID = MA.ID_EQUIPE
                                                   JOIN ESUS_ESTABELECIMENTO_SAUDE ES ON ES.ID = EQ.ID_ESTABELECIMENTO
                                                   WHERE (C.CSI_DTNASC >=
                                                   (SELECT MES_ANTERIOR FROM COMPETENCIA_ANTERIOR((SELECT DATA FROM FECHAMENTO_QUADRIMESTRE(@quadrimestre, @ano)) , 15)))
                                                   @filtros
                                                   GROUP BY C.CSI_CODPAC, C.CSI_NOMPAC, C.CSI_DTNASC, (SELECT IDADE
                                                   FROM PRO_IDADE_PACIENTE(C.CSI_DTNASC, CURRENT_DATE)), EQ.NOME_REFERENCIA)";

        string IIndicador5Command.CountPublicoAlvo { get => sqlCountPublicoAlvo; }

        public string sqlAtendimentos = $@"SELECT
                                               V.DATA_APLICACAO DATA,
                                               CASE WHEN V.CBO IS NOT NULL THEN
                                               MED.CSI_NOMMED
                                               ELSE 'Registro Anterior'
                                               END PROFISSIONAL,
                                               V.CBO,
                                               CBO.DESCRICAO DESCRICAO_CBO,
                                               P.ABREVIATURA || ' (' ||P.SIGLA|| ')' ABREVIATURA_PRODUTO,
                                               P.NOME NOME_PRODUTO,
                                               EQ.DSC_AREA EQUIPE,
                                               U.CSI_NOMUNI UNIDADE_SAUDE,
                                               CASE WHEN (COALESCE(E.PROCESSADO_CRITICAS, 'F') = 'T') THEN 1 ELSE 0 END AS PROCESSADO_CRITICAS,
                                               CASE WHEN (COALESCE(EI.FLG_ERRO, 'F') = 'T') THEN 1 ELSE 0 END AS FLG_ERRO,
                                               D.DESCRICAO DOSE,
                                               CASE WHEN (D.SIGLA = 'D3') THEN 1 ELSE 0 END AS TERCEIRA_DOSE,
                                               CAST((SELECT IDADE FROM PRO_IDADE_PACIENTE(CP.CSI_DTNASC, V.DATA_APLICACAO)) AS NUMERIC(15,2)) AS IDADE_COM_BASE_DATA_APLICACAO,
                                           
                                               CASE WHEN (E.ID IS NULL) THEN 'Registro não exportado'
                                               WHEN (COALESCE(E.PROCESSADO_CRITICAS, 'F') = 'T') THEN EI.DESCRICAO_ERRO
                                               ELSE 'Arquivo de críticas não processado'
                                               END AS DESCRICAO_ERRO,
                                           
                                               CASE WHEN (SUBSTRING(V.CBO FROM 1 FOR 4) IN ('2251', '2252', '2253', '2231', '2235', '3222'))
                                               THEN 1 ELSE 0 END AS CBO_VALIDO,
                                           
                                               CASE WHEN (P.ID = 42 OR P.ID = 22) THEN 1 ELSE 0 END AS PRODUTO_VALIDO,
                                           
                                               CASE WHEN ((SELECT COUNT(*) FROM PNI_VACINADOS AUX1
                                               WHERE AUX1.ID = V.ID AND
                                               ((SUBSTRING(V.CBO FROM 1 FOR 4) IN ('2251', '2252', '2253', '2231', '2235', '3222'))
                                               OR (V.REGISTRO_ANTERIOR = 'T')) AND ((P.ID = 42 AND D.SIGLA = 'D3') OR (P.ID = 22 AND D.SIGLA = 'D3'))) > 0)
                                               THEN 1 ELSE 0 END AS INDICADOR,
                                           
                                               CASE WHEN ((SELECT COUNT(*) FROM PNI_VACINADOS AUX1
                                               JOIN ESUS_EXPORTACAO_ITEM EI ON EI.ID = AUX1.ID_ESUS_EXPORTACAO_ITEM
                                               JOIN ESUS_EXPORTACAO E ON E.ID = EI.ID_EXPORTACAO
                                               WHERE AUX1.ID = V.ID AND (E.PROCESSADO_CRITICAS = 'T') AND
                                               ((EI.FLG_ERRO <> 'T') OR (EI.FLG_ERRO IS NULL)) AND
                                               ((P.ID = 42 AND D.SIGLA = 'D3') OR (P.ID = 22 AND D.SIGLA = 'D3')) AND
                                               ((SUBSTRING(V.CBO FROM 1 FOR 4) IN ('2251', '2252', '2253', '2231', '2235', '3222')))) > 0)
                                               THEN 1 ELSE 0 END AS INDICADOR_VALIDO,
                                               E.LOTE AS LOTE
                                           
                                           FROM PNI_VACINADOS V
                                           JOIN PNI_PRODUTO P ON (V.ID_PRODUTO = P.ID)
                                           JOIN PNI_DOSE D ON (D.ID = V.ID_DOSE)
                                           JOIN TSI_CADPAC CP ON (V.ID_PACIENTE = CP.CSI_CODPAC)
                                           LEFT JOIN TSI_MEDICOS MED ON (V.ID_PROFISIONAL = MED.CSI_CODMED)
                                           LEFT JOIN TSI_CBO CBO ON (V.CBO = CBO.CODIGO)
                                           LEFT JOIN ESUS_EQUIPES EQ ON (V.ID_EQUIPE = EQ.ID)
                                           LEFT JOIN TSI_UNIDADE U ON (V.ID_UNIDADE = U.CSI_CODUNI)
                                           LEFT JOIN ESUS_EXPORTACAO_ITEM EI ON EI.ID = V.ID_ESUS_EXPORTACAO_ITEM
                                           LEFT JOIN ESUS_EXPORTACAO E ON E.ID = EI.ID_EXPORTACAO
                                           WHERE V.ID_PACIENTE = @id_individuo
                                           ORDER BY V.DATA_APLICACAO";

        string IIndicador5Command.Atendimentos { get => sqlAtendimentos; }
    }
}
