using RgCidadao.Domain.Commands.Endemias;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Queries.Endemias
{
    public class ResultadoAmostraCommandText : IResultadoAmostraCommand
    {
        public string sqlGetAllPagination = $@"SELECT FIRST (@pagesize) SKIP (@page) M.CSI_CODMED,
                                                        M.CSI_NOMMED AGENTE,
                                                        VI.ID_PROFISSIONAL
                                                    FROM VISITA_IMOVEL VI
                                                    JOIN TSI_MEDICOS M
                                                        ON (M.CSI_CODMED = VI.ID_PROFISSIONAL)
                                                    JOIN TSI_MEDICOS_UNIDADE MU
                                                        ON( MU.CSI_CODMED = M.CSI_CODMED)
                                                    JOIN TSI_UNIDADE UNI
                                                        ON( UNI.csi_coduni = MU.csi_coduni)
                                                    JOIN ENDEMIAS_CICLOS C
                                                        ON (C.ID = VI.ID_CICLO)
                                                    @filtro
                                                    GROUP BY M.CSI_CODMED, M.CSI_NOMMED, VI.ID_PROFISSIONAL";
        string IResultadoAmostraCommand.GetAllPagination { get => sqlGetAllPagination; }

        public string sqlPendenteVisitaGetAllPagination = $@"SELECT COUNT(*) FROM (SELECT C.ID, C.ID_VISITA, C.DEPOSITO, C.AMOSTRA, C.ID_PROFISSIONAL, C.QTDE_LARVAS, M.CSI_NOMMED,
                                                                  COALESCE(C.QTDE_LARVAS - SUM(R.QTDE), C.QTDE_LARVAS) AS RESTANTE
                                                               FROM VA_COLETA C
                                                               INNER JOIN TSI_MEDICOS M ON M.CSI_CODMED = C.ID_PROFISSIONAL
                                                               INNER JOIN VISITA_IMOVEL VI ON VI.ID = C.ID_VISITA
                                                               LEFT JOIN VA_COLETA_RESULTADO R ON R.ID_COLETA = C.ID
                                                               LEFT JOIN TSI_MEDICOS_UNIDADE MU ON MU.CSI_CODMED = M.CSI_CODMED
                                                               LEFT JOIN TSI_UNIDADE UNI ON UNI.CSI_CODUNI = MU.CSI_CODUNI
                                                               LEFT JOIN ENDEMIAS_CICLOS CIC ON CIC.ID = VI.ID_CICLO
                                                               WHERE C.ID_PROFISSIONAL = @profissional
                                                               AND C.ID_VISITA IS NOT NULL @filtroamostra
                                                               GROUP BY C.ID, C.ID_VISITA, C.DEPOSITO, C.AMOSTRA, C.ID_PROFISSIONAL, C.QTDE_LARVAS, M.CSI_NOMMED
                                                               HAVING COALESCE(SUM(R.QTDE), 0) < C.QTDE_LARVAS)";
        string IResultadoAmostraCommand.PendenteVisitaGetAllPagination { get => sqlPendenteVisitaGetAllPagination; }

        public string sqlLancadaVisitaGetAllPagination = $@"SELECT COUNT(*) FROM (SELECT C.ID, C.ID_VISITA, C.DEPOSITO, C.AMOSTRA, C.ID_PROFISSIONAL, C.QTDE_LARVAS, M.CSI_NOMMED,
                                                                  COALESCE(C.QTDE_LARVAS - SUM(R.QTDE), C.QTDE_LARVAS) AS RESTANTE
                                                               FROM VA_COLETA C
                                                               INNER JOIN TSI_MEDICOS M ON M.CSI_CODMED = C.ID_PROFISSIONAL
                                                               INNER JOIN VISITA_IMOVEL VI ON VI.ID = C.ID_VISITA
                                                               LEFT JOIN VA_COLETA_RESULTADO R ON R.ID_COLETA = C.ID
                                                               LEFT JOIN TSI_MEDICOS_UNIDADE MU ON MU.CSI_CODMED = M.CSI_CODMED
                                                               LEFT JOIN TSI_UNIDADE UNI ON UNI.CSI_CODUNI = MU.CSI_CODUNI
                                                               LEFT JOIN ENDEMIAS_CICLOS CIC ON CIC.ID = VI.ID_CICLO
                                                               WHERE C.ID_PROFISSIONAL = @profissional
                                                               AND C.ID_VISITA IS NOT NULL @filtroamostra
                                                               GROUP BY C.ID, C.ID_VISITA, C.DEPOSITO, C.AMOSTRA, C.ID_PROFISSIONAL, C.QTDE_LARVAS, M.CSI_NOMMED
                                                               HAVING COALESCE(SUM(R.QTDE), 0) >= C.QTDE_LARVAS)";
        string IResultadoAmostraCommand.LancadaVisitaGetAllPagination { get => sqlLancadaVisitaGetAllPagination; }

        public string sqlGetCountAll = $@"SELECT COUNT(*) FROM (SELECT M.CSI_CODMED,
                                                        M.CSI_NOMMED AGENTE,
                                                        VI.ID_PROFISSIONAL
                                                    FROM VISITA_IMOVEL VI
                                                    JOIN TSI_MEDICOS M
                                                        ON (M.CSI_CODMED = VI.ID_PROFISSIONAL)
                                                    JOIN TSI_MEDICOS_UNIDADE MU
                                                        ON( MU.CSI_CODMED = M.CSI_CODMED)
                                                    JOIN TSI_UNIDADE UNI
                                                        ON( UNI.csi_coduni = MU.csi_coduni)
                                                    JOIN ENDEMIAS_CICLOS C
                                                        ON (C.ID = VI.ID_CICLO)
                                                    @filtro
                                                    GROUP BY M.CSI_CODMED, M.CSI_NOMMED, VI.ID_PROFISSIONAL)";
        string IResultadoAmostraCommand.GetCountAll { get => sqlGetCountAll; }

        public string sqlGetResultadoColetaByProfissional = $@"SELECT C.ID, C.ID_VISITA, C.DEPOSITO, C.AMOSTRA, C.ID_PROFISSIONAL, C.QTDE_LARVAS, M.CSI_NOMMED,
                                                                          COALESCE(C.QTDE_LARVAS - SUM(R.QTDE), C.QTDE_LARVAS) AS RESTANTE
                                                                   FROM VA_COLETA C
                                                                   INNER JOIN TSI_MEDICOS M ON M.CSI_CODMED = C.ID_PROFISSIONAL
                                                                   INNER JOIN VISITA_IMOVEL V ON V.ID = C.ID_VISITA
                                                                   LEFT JOIN VA_COLETA_RESULTADO R ON R.ID_COLETA = C.ID
                                                                   WHERE C.ID_PROFISSIONAL = @id_profissional AND C.ID_VISITA IS NOT NULL
                                                                   GROUP BY C.ID, C.ID_VISITA, C.DEPOSITO, C.AMOSTRA, C.ID_PROFISSIONAL, C.QTDE_LARVAS, M.CSI_NOMMED
                                                                   HAVING COALESCE(SUM(R.QTDE), 0) < C.QTDE_LARVAS";
        string IResultadoAmostraCommand.GetResultadoColetaByProfissional { get => sqlGetResultadoColetaByProfissional; }

        public string sqlUpdateColetaResultado = $@"UPDATE VA_COLETA_RESULTADO SET ID_ESPECIME = @id_especime,
                                                                                   QTDE = @qtde,
                                                                                   EXEMPLAR = @exemplar
                                                    WHERE ID = @id";
        string IResultadoAmostraCommand.UpdateColetaResultado { get => sqlUpdateColetaResultado; }

        public string sqlInsertColetaResultado = $@"INSERT INTO VA_COLETA_RESULTADO (ID,ID_COLETA,ID_ESPECIME,QTDE, EXEMPLAR)
                                                    VALUES(@id,@id_coleta,@id_especime,@qtde, @exemplar)";
        string IResultadoAmostraCommand.InsertColetaResultado { get => sqlInsertColetaResultado; }

        public string sqlDeleteColetaResultado = $@"DELETE FROM VA_COLETA_RESULTADO
                                                    WHERE ID = @id";
        string IResultadoAmostraCommand.DeleteColetaResultado { get => sqlDeleteColetaResultado; }

        public string sqlGetColetaResultadoByColeta = $@"SELECT * FROM VA_COLETA_RESULTADO
                                                         WHERE ID_COLETA = @id_coleta";
        string IResultadoAmostraCommand.GetColetaResultadoByColeta { get => sqlGetColetaResultadoByColeta; }

        public string sqlDeleteColetaResultadoByColeta = $@"DELETE FROM VA_COLETA_RESULTADO
                                                            WHERE ID_COLETA = @id_coleta";
        string IResultadoAmostraCommand.DeleteColetaResultadoByColeta { get => sqlDeleteColetaResultadoByColeta; }

        public string sqlGetNewIdResultadoAmostra = $@"SELECT GEN_ID(GEN_VA_COLETA_RESULTADO, 1) AS VLR FROM RDB$DATABASE";
        string IResultadoAmostraCommand.GetResultadoAmostraNewId { get => sqlGetNewIdResultadoAmostra; }

        public string sqlGetColetaResultadoByVisita = $@"SELECT C.ID, C.ID_VISITA, C.DEPOSITO, C.AMOSTRA, C.ID_PROFISSIONAL, C.QTDE_LARVAS, M.CSI_NOMMED,
                                                               COALESCE(C.QTDE_LARVAS - SUM(R.QTDE), C.QTDE_LARVAS) AS RESTANTE
                                                         FROM VA_COLETA C
                                                         INNER JOIN TSI_MEDICOS M ON M.CSI_CODMED = C.ID_PROFISSIONAL
                                                         INNER JOIN VISITA_IMOVEL V ON V.ID = C.ID_VISITA
                                                         LEFT JOIN VA_COLETA_RESULTADO R ON R.ID_COLETA = C.ID
                                                         WHERE C.ID_VISITA = @visita
                                                         GROUP BY C.ID, C.ID_VISITA, C.DEPOSITO, C.AMOSTRA, C.ID_PROFISSIONAL, C.QTDE_LARVAS, M.CSI_NOMMED
                                                         HAVING COALESCE(SUM(R.QTDE), 0) < C.QTDE_LARVAS";
        string IResultadoAmostraCommand.GetColetaResultadoByVisita { get => sqlGetColetaResultadoByVisita; }

        public string sqlResultadoAmostraByVisita = $@"SELECT CR.ID, CR.ID_COLETA, CR.ID_ESPECIME, CR.QTDE QTDE_LARVAS, CR.EXEMPLAR, C.AMOSTRA,
                                                              C.DEPOSITO, C.ID_PROFISSIONAL, C.QTDE_LARVAS QTDE_LARVAS_COLETA
                                                       FROM VA_COLETA_RESULTADO CR
                                                       JOIN VA_COLETA C ON C.ID = CR.ID_COLETA
                                                       WHERE C.ID_VISITA = @id_visita";
        string IResultadoAmostraCommand.GetResultadoAmostraByVisita { get => sqlResultadoAmostraByVisita; }

        public string sqlGetResultadoAmostraByProfissional = $@"SELECT CR.ID, C.ID_VISITA, CR.ID_COLETA, CR.ID_ESPECIME, CR.QTDE QTDE_LARVAS, CR.EXEMPLAR, C.AMOSTRA,
                                                                       C.DEPOSITO, C.ID_PROFISSIONAL, C.QTDE_LARVAS QTDE_LARVAS_COLETA
                                                                FROM VA_COLETA_RESULTADO CR
                                                                JOIN VA_COLETA C ON C.ID = CR.ID_COLETA
                                                                INNER JOIN VISITA_IMOVEL VI ON VI.ID = C.ID_VISITA
                                                                JOIN TSI_MEDICOS MED ON MED.CSI_CODMED = C.ID_PROFISSIONAL
                                                                LEFT JOIN TSI_MEDICOS_UNIDADE MU ON MU.CSI_CODMED = MED.CSI_CODMED
                                                                LEFT JOIN TSI_UNIDADE UNI ON UNI.CSI_CODUNI = MU.CSI_CODUNI
                                                                LEFT JOIN ENDEMIAS_CICLOS CIC ON CIC.ID = VI.ID_CICLO
                                                                WHERE C.ID_PROFISSIONAL = @profissional @filtro";
        string IResultadoAmostraCommand.GetResultadoAmostraByProfissional { get => sqlGetResultadoAmostraByProfissional; }
    }
}
