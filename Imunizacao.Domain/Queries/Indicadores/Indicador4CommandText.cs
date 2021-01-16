using RgCidadao.Domain.Commands.Indicadores;

namespace RgCidadao.Domain.Queries.Indicadores
{
    public class Indicador4CommandText : IIndicador4Command
    {
        public string sqlIndicador4 = $@" @select 
                                          COUNT(*) QTDE_INDIVIDUOS,
                                        
                                          -- METAS (SISTEMA)
                                          SUM((CASE WHEN ((SELECT SUM(IPE.CSI_QTDE)
                                          FROM TSI_PROCENFERMAGEM PE
                                          JOIN TSI_IPROCENFERMAGEM IPE ON IPE.CSI_CONTROLE = PE.CSI_CONTROLE
                                          WHERE (PE.CSI_CODPAC = P.CSI_CODPAC) AND
                                          (SUBSTRING(PE.CSI_CBO FROM 1 FOR 4) IN ('2251', '2252', '2253', '2231', '2235')) AND
                                          (IPE.CSI_CODPROC IN ('0201020033')) AND
                                          (PE.CSI_DATA BETWEEN(SELECT MES_ANTERIOR
                                          FROM COMPETENCIA_ANTERIOR((SELECT DATA FROM FECHAMENTO_QUADRIMESTRE(@quadrimestre, @ano)), 35))
                                          AND (SELECT DATA FROM FECHAMENTO_QUADRIMESTRE(@quadrimestre, @ano)))) > 0)
                                          THEN 1 ELSE 0 END)) AS QTDE_METAS,
                                        
                                          -- METAS (VALIDAS)
                                          SUM((CASE WHEN ((SELECT SUM(IPE.CSI_QTDE)
                                          FROM TSI_PROCENFERMAGEM PE
                                          JOIN TSI_IPROCENFERMAGEM IPE ON IPE.CSI_CONTROLE = PE.CSI_CONTROLE
                                          JOIN ESUS_EXPORTACAO_ITEM EI ON EI.ID = IPE.ID_ESUS_EXPORTACAO_ITEM
                                          JOIN ESUS_EXPORTACAO E ON E.ID = EI.ID_EXPORTACAO
                                          WHERE (PE.CSI_CODPAC = P.CSI_CODPAC) AND
                                          (E.PROCESSADO_CRITICAS = 'T') AND
                                          ((EI.FLG_ERRO <> 'T') OR (EI.FLG_ERRO IS NULL)) AND
                                          ((P.CSI_NCARTAO IS NOT NULL AND
                                          P.CSI_NCARTAO <> '') OR (P.CSI_CPFPAC IS NOT NULL AND
                                          P.CSI_CPFPAC <> '')) AND
                                          (SUBSTRING(PE.CSI_CBO FROM 1 FOR 4) IN ('2251', '2252', '2253', '2231', '2235')) AND
                                          (IPE.CSI_CODPROC IN ('0201020033')) AND
                                          (PE.CSI_DATA BETWEEN(SELECT MES_ANTERIOR
                                          FROM COMPETENCIA_ANTERIOR((SELECT DATA FROM FECHAMENTO_QUADRIMESTRE(@quadrimestre, @ano)), 35))
                                          AND (SELECT DATA FROM FECHAMENTO_QUADRIMESTRE(@quadrimestre, @ano))) ) > 0)
                                          THEN 1 ELSE 0 END)) AS QTDE_METAS_VALIDAS
                                        
                                        FROM TSI_CADPAC P
                                        JOIN ESUS_FAMILIA FAM ON FAM.ID = P.ID_FAMILIA
                                        JOIN VS_ESTABELECIMENTOS EST ON EST.ID = FAM.ID_DOMICILIO
                                        JOIN ESUS_MICROAREA MA ON MA.ID = EST.ID_MICROAREA
                                        JOIN TSI_MEDICOS ME ON ME.CSI_CODMED = MA.ID_PROFISSIONAL
                                        JOIN ESUS_EQUIPES EQ ON EQ.ID = MA.ID_EQUIPE
                                        JOIN ESUS_ESTABELECIMENTO_SAUDE ES ON ES.ID = EQ.ID_ESTABELECIMENTO
                                        WHERE UPPER(P.CSI_SEXPAC) = 'FEMININO' AND
                                        (SELECT IDADE FROM PRO_CALCULA_IDADE(P.CSI_DTNASC, (SELECT DATA FROM FECHAMENTO_QUADRIMESTRE(@quadrimestre, @ano)))) >= 25 AND
                                        (SELECT IDADE FROM PRO_CALCULA_IDADE(P.CSI_DTNASC, (SELECT DATA FROM INICIO_QUADRIMESTRE(@quadrimestre, @ano)))) <= 64 AND
                                        (MA.ID_EQUIPE IS NOT NULL)
                                        @filtros
                                        @agrupamento";

        string IIndicador4Command.Indicador4 { get => sqlIndicador4; }

        public string sqlPublicoAlvo = $@" @select
                                              P.CSI_CODPAC AS ID_INDIVIDUO,
                                              P.CSI_NOMPAC AS INDIVIDUO,
                                              P.CSI_DTNASC AS DATA_NASCIMENTO,
                                              (SELECT IDADE FROM PRO_CALCULA_IDADE(P.CSI_DTNASC,  CURRENT_DATE )) AS IDADE,
                                          
                                              -- DATA ULTIMO ATENDIMENTO (SISTEMA)
                                              (SELECT MAX(PE.CSI_DATA)
                                              FROM TSI_PROCENFERMAGEM PE
                                              JOIN TSI_IPROCENFERMAGEM IPE ON IPE.CSI_CONTROLE = PE.CSI_CONTROLE
                                              WHERE (PE.CSI_CODPAC = P.CSI_CODPAC) AND
                                              (SUBSTRING(PE.CSI_CBO FROM 1 FOR 4) IN ('2251', '2252', '2253', '2231', '2235')) AND
                                              (IPE.CSI_CODPROC IN ('0201020033'))) AS DATA_ULTIMO_ATENDIMENTO,
                                          
                                              -- VERIFICANDO SE DATA DO ULTIMO ATENDIMENTO FOI ENTRE OS 3 ULTIMOS ANOS (SISTEMA)
                                              CASE WHEN ((SELECT MAX(PE.CSI_DATA) FROM TSI_PROCENFERMAGEM PE
                                              JOIN TSI_IPROCENFERMAGEM IPE ON IPE.CSI_CONTROLE = PE.CSI_CONTROLE
                                              WHERE (PE.CSI_CODPAC = P.CSI_CODPAC) AND
                                              (SUBSTRING(PE.CSI_CBO FROM 1 FOR 4) IN ('2251', '2252', '2253', '2231', '2235')) AND
                                              (IPE.CSI_CODPROC IN ('0201020033')))
                                              BETWEEN(SELECT MES_ANTERIOR FROM COMPETENCIA_ANTERIOR((SELECT DATA FROM FECHAMENTO_QUADRIMESTRE(NULL, @ano)), 35))
                                              AND (SELECT DATA FROM FECHAMENTO_QUADRIMESTRE(NULL, @ano))) THEN 1 ELSE 0 END AS ATD_ULTIMOS_3ANOS,
                                          
                                              -- DATA ULTIMO ATENDIMENTO (VALIDO)
                                              (SELECT MAX(PE.CSI_DATA)
                                              FROM TSI_PROCENFERMAGEM PE
                                              JOIN TSI_IPROCENFERMAGEM IPE ON IPE.CSI_CONTROLE = PE.CSI_CONTROLE
                                              JOIN ESUS_EXPORTACAO_ITEM EI ON EI.ID = IPE.ID_ESUS_EXPORTACAO_ITEM
                                              JOIN ESUS_EXPORTACAO E ON E.ID = EI.ID_EXPORTACAO
                                              WHERE (PE.CSI_CODPAC = P.CSI_CODPAC) AND
                                              (E.PROCESSADO_CRITICAS = 'T') AND
                                              ((EI.FLG_ERRO <> 'T') OR (EI.FLG_ERRO IS NULL)) AND
                                              ((P.CSI_NCARTAO IS NOT NULL AND P.CSI_NCARTAO <> '') OR
                                              (P.CSI_CPFPAC IS NOT NULL AND P.CSI_CPFPAC <> '')) AND
                                              (SUBSTRING(PE.CSI_CBO FROM 1 FOR 4) IN ('2251', '2252', '2253', '2231', '2235')) AND
                                              (IPE.CSI_CODPROC IN ('0201020033'))) AS DATA_ULTIMO_ATD_VALIDO,
                                          
                                              -- VERIFICANDO SE DATA DO ULTIMO ATENDIMENTO FOI ENTRE OS 3 ULTIMOS ANOS (VALIDO)
                                              CASE WHEN ((SELECT MAX(PE.CSI_DATA) FROM TSI_PROCENFERMAGEM PE
                                              JOIN TSI_IPROCENFERMAGEM IPE ON IPE.CSI_CONTROLE = PE.CSI_CONTROLE
                                              JOIN ESUS_EXPORTACAO_ITEM EI ON EI.ID = IPE.ID_ESUS_EXPORTACAO_ITEM
                                              JOIN ESUS_EXPORTACAO E ON E.ID = EI.ID_EXPORTACAO
                                              WHERE (PE.CSI_CODPAC = P.CSI_CODPAC) AND
                                              (E.PROCESSADO_CRITICAS = 'T') AND
                                              ((EI.FLG_ERRO <> 'T') OR (EI.FLG_ERRO IS NULL)) AND
                                              ((P.CSI_NCARTAO IS NOT NULL AND P.CSI_NCARTAO <> '') OR
                                              (P.CSI_CPFPAC IS NOT NULL AND P.CSI_CPFPAC <> '')) AND
                                              (SUBSTRING(PE.CSI_CBO FROM 1 FOR 4) IN ('2251', '2252', '2253', '2231', '2235')) AND
                                              (IPE.CSI_CODPROC IN ('0201020033')))
                                              BETWEEN(SELECT MES_ANTERIOR FROM COMPETENCIA_ANTERIOR((SELECT DATA FROM FECHAMENTO_QUADRIMESTRE(NULL, @ano)), 35))
                                              AND (SELECT DATA FROM FECHAMENTO_QUADRIMESTRE(NULL, @ano))) THEN 1 ELSE 0 END AS ATD_VALIDO_ULTIMOS_3ANOS
                                          
                                          FROM TSI_CADPAC P
                                          JOIN ESUS_FAMILIA FAM ON FAM.ID = P.ID_FAMILIA
                                          JOIN VS_ESTABELECIMENTOS EST ON EST.ID = FAM.ID_DOMICILIO
                                          JOIN ESUS_MICROAREA MA ON MA.ID = EST.ID_MICROAREA          
                                          JOIN TSI_MEDICOS ME ON ME.CSI_CODMED = MA.ID_PROFISSIONAL
                                          JOIN ESUS_EQUIPES EQ ON EQ.ID = MA.ID_EQUIPE
                                          JOIN ESUS_ESTABELECIMENTO_SAUDE ES ON ES.ID = EQ.ID_ESTABELECIMENTO
                                          WHERE (UPPER(P.CSI_SEXPAC) = 'FEMININO' AND
                                          (SELECT IDADE FROM PRO_CALCULA_IDADE(P.CSI_DTNASC, (SELECT DATA FROM FECHAMENTO_QUADRIMESTRE(@quadrimestre, @ano)))) >= 25 AND
                                          (SELECT IDADE FROM PRO_CALCULA_IDADE(P.CSI_DTNASC,  (SELECT DATA FROM INICIO_QUADRIMESTRE(@quadrimestre, @ano)))) <= 64 AND
                                          (MA.ID_EQUIPE IS NOT NULL))
                                          @filtros
                                          ORDER BY P.CSI_NOMPAC";
        
        string IIndicador4Command.PublicoAlvo { get => sqlPublicoAlvo; }

        public string sqlCountPublicoAlvo = $@"SELECT COUNT(*) FROM (
                                                        SELECT P.CSI_CODPAC
                                                        FROM TSI_CADPAC P
                                                        JOIN ESUS_FAMILIA FAM ON FAM.ID = P.ID_FAMILIA
                                                        JOIN VS_ESTABELECIMENTOS EST ON EST.ID = FAM.ID_DOMICILIO
                                                        JOIN ESUS_MICROAREA MA ON MA.ID = EST.ID_MICROAREA          
                                                        JOIN TSI_MEDICOS ME ON ME.CSI_CODMED = MA.ID_PROFISSIONAL
                                                        JOIN ESUS_EQUIPES EQ ON EQ.ID = MA.ID_EQUIPE
                                                        JOIN ESUS_ESTABELECIMENTO_SAUDE ES ON ES.ID = EQ.ID_ESTABELECIMENTO
                                                        WHERE (UPPER(P.CSI_SEXPAC) = 'FEMININO' AND
                                                        (SELECT IDADE FROM PRO_CALCULA_IDADE(P.CSI_DTNASC, (SELECT DATA FROM FECHAMENTO_QUADRIMESTRE(@quadrimestre, @ano)))) >= 25 AND
                                                        (SELECT IDADE FROM PRO_CALCULA_IDADE(P.CSI_DTNASC,  (SELECT DATA FROM INICIO_QUADRIMESTRE(@quadrimestre, @ano)))) <= 64 AND
                                                        (MA.ID_EQUIPE IS NOT NULL))
                                                        @filtros)";

        string IIndicador4Command.CountPublicoAlvo { get => sqlCountPublicoAlvo; }

        public string sqlAtendimentos = $@" SELECT
                                                PE.CSI_DATA DATA,
                                                MED.CSI_NOMMED PROFISSIONAL,
                                                PE.CSI_CBO CBO,
                                                CBO.DESCRICAO DESCRICAO_CBO,
                                                PROC.NOME PROCEDIMENTO,
                                                EQ.DSC_AREA EQUIPE,
                                                U.CSI_NOMUNI UNIDADE_SAUDE,
                                                CASE WHEN (COALESCE(E.PROCESSADO_CRITICAS, 'F') = 'T') THEN 1 ELSE 0 END AS PROCESSADO_CRITICAS,
                                                CASE WHEN (COALESCE(EI.FLG_ERRO, 'F') = 'T') THEN 1 ELSE 0 END AS FLG_ERRO,
                                            
                                                CASE WHEN (E.ID IS NULL) THEN 'Registro não exportado'
                                                WHEN (COALESCE(E.PROCESSADO_CRITICAS, 'F') = 'T') THEN EI.DESCRICAO_ERRO
                                                ELSE 'Arquivo de críticas não processado'
                                                END AS DESCRICAO_ERRO,
                                            
                                                CASE WHEN (SUBSTRING(PE.CSI_CBO FROM 1 FOR 4) IN ('2251', '2252', '2253', '2231', '2235'))
                                                THEN 1 ELSE 0 END AS CBO_VALIDO,
                                            
                                                CASE WHEN (PE.CSI_DATA BETWEEN(SELECT MES_ANTERIOR
                                                FROM COMPETENCIA_ANTERIOR((SELECT DATA FROM FECHAMENTO_QUADRIMESTRE(NULL, @ano)), 35))
                                                AND (SELECT DATA FROM FECHAMENTO_QUADRIMESTRE(NULL, @ano)))
                                                THEN 1 ELSE 0 END AS DATA_VALIDA,
                                            
                                                (CASE WHEN ((SELECT COUNT(*) FROM TSI_IPROCENFERMAGEM AUX1
                                                WHERE AUX1.CSI_CONTROLE = IPE.CSI_CONTROLE
                                                AND (SUBSTRING(PE.CSI_CBO FROM 1 FOR 4) IN ('2251', '2252', '2253', '2231', '2235')) AND
                                                (PE.CSI_DATA BETWEEN(SELECT MES_ANTERIOR
                                                FROM COMPETENCIA_ANTERIOR((SELECT DATA FROM FECHAMENTO_QUADRIMESTRE(@quadrimestre, @ano)), 35))
                                                AND (SELECT DATA FROM FECHAMENTO_QUADRIMESTRE(@quadrimestre, @ano)))) > 0) THEN 1 ELSE 0 END) AS INDICADOR,
                                            
                                                (CASE WHEN ((SELECT COUNT(*) FROM TSI_IPROCENFERMAGEM AUX1
                                                JOIN ESUS_EXPORTACAO_ITEM EI ON EI.ID = AUX1.ID_ESUS_EXPORTACAO_ITEM
                                                JOIN ESUS_EXPORTACAO E ON E.ID = EI.ID_EXPORTACAO
                                                WHERE AUX1.CSI_CONTROLE = IPE.CSI_CONTROLE AND
                                                (E.PROCESSADO_CRITICAS = 'T') AND
                                                ((EI.FLG_ERRO <> 'T') OR (EI.FLG_ERRO IS NULL)) AND
                                                (SUBSTRING(PE.CSI_CBO FROM 1 FOR 4) IN ('2251', '2252', '2253', '2231', '2235')) AND
                                                (PE.CSI_DATA BETWEEN(SELECT MES_ANTERIOR
                                                FROM COMPETENCIA_ANTERIOR((SELECT DATA FROM FECHAMENTO_QUADRIMESTRE(@quadrimestre, @ano)), 35))
                                                AND (SELECT DATA FROM FECHAMENTO_QUADRIMESTRE(@quadrimestre, @ano)))) > 0) THEN 1 ELSE 0 END) AS INDICADOR_VALIDO,
                                                E.LOTE AS LOTE
                                            
                                            FROM TSI_PROCENFERMAGEM PE
                                            JOIN TSI_IPROCENFERMAGEM IPE ON IPE.CSI_CONTROLE = PE.CSI_CONTROLE
                                            JOIN TSI_PROCEDIMENTO PROC ON (IPE.CSI_CODPROC = PROC.CODIGO)
                                            JOIN TSI_MEDICOS MED ON (PE.CSI_CODMED = MED.CSI_CODMED)
                                            JOIN TSI_CBO CBO ON (PE.CSI_CBO = CBO.CODIGO)
                                            LEFT JOIN ESUS_EQUIPES EQ ON (PE.ID_EQUIPE = EQ.ID)
                                            JOIN TSI_UNIDADE U ON (PE.CSI_CODUNI = U.CSI_CODUNI)
                                            LEFT JOIN ESUS_EXPORTACAO_ITEM EI ON EI.ID = IPE.ID_ESUS_EXPORTACAO_ITEM
                                            LEFT JOIN ESUS_EXPORTACAO E ON E.ID = EI.ID_EXPORTACAO
                                            WHERE (PE.CSI_CODPAC = @id_individuo) AND
                                            (IPE.CSI_CODPROC IN ('0201020033'))
                                            ORDER BY PE.CSI_DATA DESC";

        string IIndicador4Command.Atendimentos { get => sqlAtendimentos; }
    }
}
