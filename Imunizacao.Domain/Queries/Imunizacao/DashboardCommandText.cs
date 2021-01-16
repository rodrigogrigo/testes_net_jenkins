using RgCidadao.Domain.Commands.Imunizacao;

namespace RgCidadao.Domain.Queries.Imunizacao
{
    public class DashboardCommandText : IDashboardCommand
    {
        public string sqlVacinasDia = $@"SELECT COUNT(*)
                                         FROM PNI_VACINADOS V
                                         WHERE V.DATA_APLICACAO = CURRENT_DATE";
        string IDashboardCommand.TotalVacinasDia { get => sqlVacinasDia; }

        public string sqlVacinaVencida = $@"SELECT COUNT(DISTINCT V.ID)
                                            FROM PNI_VACINADOS V
                                            JOIN PNI_PRODUTO P ON (V.ID_PRODUTO = P.ID)
                                            JOIN PNI_DOSE D ON D.ID = V.ID_DOSE
                                            WHERE V.DATA_APLICACAO = CURRENT_DATE AND
                                            V.ID_UNIDADE = @id";
        string IDashboardCommand.TotalVacinaVencida { get => sqlVacinaVencida; }

        public string sqlImunizadasMes = $@"SELECT COUNT(*), EXTRACT(MONTH FROM V.DATA_APLICACAO) MES_APLICACAO
                                            FROM PNI_VACINADOS V
                                            WHERE
                                            EXTRACT(YEAR FROM V.DATA_APLICACAO) = EXTRACT(YEAR FROM CURRENT_DATE)
                                            GROUP BY MES_APLICACAO";
        string IDashboardCommand.TotalImunizadasMes { get => sqlImunizadasMes; }

        public string sqlGetVacinas = $@"SELECT
                                        (EXTRACT (HOUR FROM V.DATA_HORA)) AS HORA,
                                        COUNT(DISTINCT V.ID) AS QTDE_DOSES
                                        FROM PNI_VACINADOS V
                                        WHERE V.DATA_HORA >= CURRENT_DATE-30
                                        AND V.ID_UNIDADE = @unidade
                                        GROUP BY EXTRACT (HOUR FROM V.DATA_HORA)";
        string IDashboardCommand.GetVacinas { get => sqlGetVacinas; }

        public string sqlPercentual = $@"SELECT COUNT(C.CSI_CODPAC) QTDE_INDIVIDUOS, SUM(
                                                CASE
                                                  WHEN 
                                                ((SELECT COUNT(DISTINCT V.ID)
                                                  FROM PNI_VACINADOS V
                                                  JOIN PNI_PRODUTO PR ON (V.ID_PRODUTO = PR.ID)
                                                  JOIN PNI_DOSE D ON D.ID = V.ID_DOSE
                                                  WHERE V.ID_PACIENTE = C.CSI_CODPAC AND
                                                        SUBSTRING(V.CBO FROM 1 FOR 4) IN ('2251', '2252', '2253', '2231', '2235', '3222') AND
                                                        PR.ID = 22 AND
                                                        D.SIGLA = 'D3') > 0) AND
                                                
                                                ((SELECT COUNT(DISTINCT V.ID)
                                                  FROM PNI_VACINADOS V
                                                  JOIN PNI_PRODUTO PR ON (V.ID_PRODUTO = PR.ID)
                                                  JOIN PNI_DOSE D ON D.ID = V.ID_DOSE
                                                  WHERE V.ID_PACIENTE = C.CSI_CODPAC AND
                                                        SUBSTRING(V.CBO FROM 1 FOR 4) IN ('2251', '2252', '2253', '2231', '2235', '3222') AND
                                                        PR.ID = 42 AND
                                                        D.SIGLA = 'D3') > 0) AND ((C.CSI_NCARTAO <> '' AND C.CSI_NCARTAO IS NOT NULL) OR (C.CSI_CPFPAC <> '' AND C.CSI_CPFPAC IS NOT NULL)) THEN 1
                                                  ELSE 0
                                                END) AS INDICADOR
                                         FROM TSI_CADPAC C
                                         JOIN ESUS_CADDOMICILIAR CD ON CD.ID = C.ID_ESUS_CADDOMICILIAR
                                         JOIN ESUS_MICROAREA MA ON MA.ID = CD.ID_MICROAREA
                                         JOIN TSI_MEDICOS ACS ON ACS.CSI_CODMED = MA.ID_PROFISSIONAL
                                         JOIN ESUS_EQUIPES EQ ON EQ.ID = MA.ID_EQUIPE
                                         JOIN ESUS_ESTABELECIMENTO_SAUDE ES ON ES.ID = EQ.ID_ESTABELECIMENTO
                                         JOIN TSI_UNIDADE U ON U.CSI_CNES = ES.CNES
                                         WHERE (C.CSI_DTNASC >= (CURRENT_DATE -365)) AND
                                               U.CSI_CODUNI = @unidade";
        string IDashboardCommand.GetPercentualPolioPenta { get => sqlPercentual; }
    }
}
