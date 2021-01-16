using RgCidadao.Domain.Commands.AtencaoBasica;

namespace RgCidadao.Domain.Queries.AtencaoBasica
{
    public class GestaoFamiliaCommandText : IGestaoFamiliaCommand
    {
        public string sqlGetEstabelecimentoByMicroarea = $@"SELECT E.ID_LOGRADOURO,LOGR.CSI_NOMEND LOGRADOURO,BAI.CSI_NOMBAI BAIRRO,
                                                                   E.ID, E.NUMERO_LOGRADOURO, CID.CSI_NOMCID CIDADE, CID.CSI_SIGEST SIGLA_ESTADO,
                                                                   E.TIPO_IMOVEL, E.LATITUDE, E.LONGITUDE, E.TIPO_IMOVEL         
                                                            FROM VS_ESTABELECIMENTOS E
                                                            JOIN TSI_LOGRADOURO LOGR ON LOGR.CSI_CODEND = E.ID_LOGRADOURO
                                                            JOIN TSI_BAIRRO BAI ON BAI.CSI_CODBAI = LOGR.CSI_CODBAI
                                                            JOIN TSI_CIDADE CID ON CID.CSI_CODCID = BAI.CSI_CODCID
                                                            WHERE E.ID_MICROAREA = @id_microarea";
        string IGestaoFamiliaCommand.GetEstabelecimentoByMicroarea { get => sqlGetEstabelecimentoByMicroarea; }

        public string sqlGetFamiliaByEstabelecimento = $@"SELECT EF.ID,EF.NUM_PRONTUARIO_FAMILIAR,PAC.CSI_NOMPAC RESPONSAVEL, EF.ID_RESPONSAVEL
                                                          FROM ESUS_FAMILIA EF
                                                          JOIN TSI_CADPAC PAC ON PAC.CSI_CODPAC = EF.ID_RESPONSAVEL
                                                          WHERE EF.ID_DOMICILIO = @id_estabelecimento";
        string IGestaoFamiliaCommand.GetFamiliaByEstabelecimento { get => sqlGetFamiliaByEstabelecimento; }

        public string sqlGetVisitaByEstabelecimento = $@"SELECT FIRST(1) EVD.ID, EVD.DATA_VISITA DATA, EVD.DESFECHO
                                                         FROM ESUS_VISITA_DOMICILIAR EVD
                                                         WHERE EVD.ID_ESTABELECIMENTO = @id_estabelecimento AND
                                                               EVD.COMPETENCIA = @competencia
                                                         ORDER BY EVD.DATA_VISITA DESC";
        string IGestaoFamiliaCommand.GetVisitaByEstabelecimento { get => sqlGetVisitaByEstabelecimento; }

        public string sqlGetEstatisticasByMicroarea = $@"SELECT
                                                           COUNT(ID_INDIVIDUO) INDIVIDUO_TOTAL,
                                                           COUNT(DISTINCT ID_FAMILIA) FAMILIA_TOTAL,
                                                           COUNT(DISTINCT ID_FAMILIA_VISITADA) FAMILIA_VISITADA,
                                                           SUM(VISITADO) INDIVIDUO_VISITADO,
                                                           SUM(DIABETICO) DIABETICO_TOTAL,
                                                           SUM(CASE WHEN (DIABETICO = 1 AND VISITADO = 1) THEN 1 ELSE 0 END) DIABETICO_VISITADO,
                                                           SUM(HIPERTENSO) HIPERTENSO_TOTAL,
                                                           SUM(CASE WHEN (HIPERTENSO = 1 AND VISITADO = 1) THEN 1 ELSE 0 END) HIPERTENSO_VISITADO,
                                                           SUM(GESTANTE) GESTANTE_TOTAL,
                                                           SUM(CASE WHEN (GESTANTE = 1 AND VISITADO = 1) THEN 1 ELSE 0 END) GESTANTE_VISITADO,
                                                           SUM(CRIANCA) CRIANCA_TOTAL,
                                                           SUM(CASE WHEN (CRIANCA = 1 AND VISITADO = 1) THEN 1 ELSE 0 END) CRIANCA_VISITADO,
                                                           SUM(IDOSO) IDOSO_TOTAL,
                                                           SUM(CASE WHEN (IDOSO = 1 AND VISITADO = 1) THEN 1 ELSE 0 END) IDOSO_VISITADO
                                                         FROM (
                                                         SELECT TAB_0.*,
                                                         (CASE WHEN VISITADO = 1 THEN ID_FAMILIA ELSE NULL END) AS ID_FAMILIA_VISITADA
                                                         FROM(
                                                         SELECT
                                                           CP.CSI_CODPAC ID_INDIVIDUO,
                                                           FAM.ID ID_FAMILIA,
                                                           CASE WHEN COALESCE(CP.DIABETES, 'F') = 'T' THEN 1 ELSE 0 END AS DIABETICO,
                                                           CASE WHEN COALESCE(CP.HIPERTENSO, 'F') = 'T' THEN 1 ELSE 0 END AS HIPERTENSO,
                                                           CASE WHEN COALESCE(G.FLG_GESTACAO_EM_ANDAMENTO, 'F') = 'T' THEN 1 ELSE 0 END AS GESTANTE,
                                                           CASE WHEN (SELECT IDADE FROM PRO_CALCULA_IDADE(CP.CSI_DTNASC, CURRENT_DATE)) <= 12 THEN 1 ELSE 0 END AS CRIANCA,
                                                           CASE WHEN (SELECT IDADE FROM PRO_CALCULA_IDADE(CP.CSI_DTNASC, CURRENT_DATE)) >= 60 THEN 1 ELSE 0 END AS IDOSO,
                                                           (SELECT (CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END) FROM ESUS_VISITA_DOMICILIAR VIS
                                                           WHERE VIS.ID_CIDADAO = CP.CSI_CODPAC AND VIS.COMPETENCIA = @competencia) VISITADO
                                                         FROM VS_ESTABELECIMENTOS EST
                                                         JOIN ESUS_FAMILIA FAM ON (EST.ID = FAM.ID_DOMICILIO)
                                                         JOIN TSI_CADPAC CP ON (FAM.ID = CP.ID_FAMILIA)
                                                         LEFT JOIN GESTACAO G ON (CP.CSI_CODPAC = G.ID_CIDADAO)
                                                         WHERE EST.ID_MICROAREA = @id_microarea) AS TAB_0
                                                         ) TAB_1";
        string IGestaoFamiliaCommand.GetEstatisticasByMicroarea { get => sqlGetEstatisticasByMicroarea; }

        public string sqlGetMembrosByFamilia = $@"SELECT CP.CSI_CODPAC, CP.CSI_NOMPAC, CP.CSI_DTNASC, COALESCE(CP.HIPERTENSO, 'F') HIPERTENSO, COALESCE(CP.DIABETES, 'F') DIABETICO,
                                                         CP.CSI_NCARTAO, CP.CSI_CPFPAC,
                                                         COALESCE((SELECT G.FLG_GESTACAO_EM_ANDAMENTO FROM GESTACAO G WHERE G.ID_CIDADAO = CP.CSI_CODPAC), 'F') GESTANTE,
                                                         (SELECT IDADE FROM PRO_CALCULA_IDADE(CP.CSI_DTNASC, CURRENT_DATE)) IDADE_ANOS,
                                                         (SELECT COUNT(*) FROM PNI_APRAZAMENTO APZ WHERE APZ.ID_INDIVIDUO = CP.CSI_CODPAC
                                                         AND APZ.ID_VACINADOS IS NULL AND APZ.DATA_LIMITE <= CURRENT_DATE) VACINAS_ATRASADAS, CP.CSI_SEXPAC
                                                  FROM TSI_CADPAC CP
                                                  WHERE CP.ID_FAMILIA = @id_familia;";
        string IGestaoFamiliaCommand.GetMembrosByFamilia { get => sqlGetMembrosByFamilia; }

        public string sqlGetEstatisticasByEquipes = $@" SELECT
                                                          COUNT(ID_INDIVIDUO) INDIVIDUO_TOTAL,
                                                          COUNT(DISTINCT ID_FAMILIA) FAMILIA_TOTAL,
                                                          COUNT(DISTINCT ID_FAMILIA_VISITADA) FAMILIA_VISITADA,
                                                          SUM(VISITADO) INDIVIDUO_VISITADO,
                                                          SUM(DIABETICO) DIABETICO_TOTAL,
                                                          SUM(CASE WHEN (DIABETICO = 1 AND VISITADO = 1) THEN 1 ELSE 0 END) DIABETICO_VISITADO,
                                                          SUM(HIPERTENSO) HIPERTENSO_TOTAL,
                                                          SUM(CASE WHEN (HIPERTENSO = 1 AND VISITADO = 1) THEN 1 ELSE 0 END) HIPERTENSO_VISITADO,
                                                          SUM(GESTANTE) GESTANTE_TOTAL,
                                                          SUM(CASE WHEN (GESTANTE = 1 AND VISITADO = 1) THEN 1 ELSE 0 END) GESTANTE_VISITADO,
                                                          SUM(CRIANCA) CRIANCA_TOTAL,
                                                          SUM(CASE WHEN (CRIANCA = 1 AND VISITADO = 1) THEN 1 ELSE 0 END) CRIANCA_VISITADO,
                                                          SUM(IDOSO) IDOSO_TOTAL,
                                                          SUM(CASE WHEN (IDOSO = 1 AND VISITADO = 1) THEN 1 ELSE 0 END) IDOSO_VISITADO
                                                        FROM (
                                                        SELECT TAB_0.*,
                                                        (CASE WHEN VISITADO = 1 THEN ID_FAMILIA ELSE NULL END) AS ID_FAMILIA_VISITADA
                                                        FROM(
                                                        SELECT
                                                          CP.CSI_CODPAC ID_INDIVIDUO,
                                                          FAM.ID ID_FAMILIA,
                                                          CASE WHEN COALESCE(CP.DIABETES, 'F') = 'T' THEN 1 ELSE 0 END AS DIABETICO,
                                                          CASE WHEN COALESCE(CP.HIPERTENSO, 'F') = 'T' THEN 1 ELSE 0 END AS HIPERTENSO,
                                                          CASE WHEN COALESCE(G.FLG_GESTACAO_EM_ANDAMENTO, 'F') = 'T' THEN 1 ELSE 0 END AS GESTANTE,
                                                          CASE WHEN (SELECT IDADE FROM PRO_CALCULA_IDADE(CP.CSI_DTNASC, CURRENT_DATE)) <= 12 THEN 1 ELSE 0 END AS CRIANCA,
                                                          CASE WHEN (SELECT IDADE FROM PRO_CALCULA_IDADE(CP.CSI_DTNASC, CURRENT_DATE)) >= 60 THEN 1 ELSE 0 END AS IDOSO,
                                                          (SELECT (CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END) FROM ESUS_VISITA_DOMICILIAR VIS
                                                          WHERE VIS.ID_CIDADAO = CP.CSI_CODPAC AND VIS.COMPETENCIA = @competencia) VISITADO
                                                        FROM VS_ESTABELECIMENTOS EST
                                                        JOIN ESUS_MICROAREA MI ON (EST.ID_MICROAREA = MI.ID)
                                                        JOIN ESUS_FAMILIA FAM ON (EST.ID = FAM.ID_DOMICILIO)
                                                        JOIN TSI_CADPAC CP ON (FAM.ID = CP.ID_FAMILIA)
                                                        LEFT JOIN GESTACAO G ON (CP.CSI_CODPAC = G.ID_CIDADAO)
                                                        @filtros
                                                        ) AS TAB_0) TAB_1";

        string IGestaoFamiliaCommand.GetEstatisticasByEquipes { get => sqlGetEstatisticasByEquipes; }

        public string sqlGetDiabeticosByEquipe = $@"@select 
                                                    TAB.ID, TAB.NOME_INDIVIDUO, TAB.EQUIPE, TAB.VISITADO FROM (
                                                    SELECT
                                                    CP.CSI_CODPAC ID,
                                                    CP.CSI_NOMPAC NOME_INDIVIDUO,
                                                    EQ.DESCRICAO EQUIPE,
                                                    CASE WHEN(SELECT FIRST 1 COALESCE(VIS.DESFECHO, 0) FROM ESUS_VISITA_DOMICILIAR VIS
                                                              WHERE VIS.ID_CIDADAO = CP.CSI_CODPAC AND
                                                                    VIS.COMPETENCIA = @competencia
                                                            @filtro1) IS NULL THEN NULL
                                                    ELSE (SELECT FIRST 1 VIS.DESFECHO FROM ESUS_VISITA_DOMICILIAR VIS
                                                          WHERE VIS.ID_CIDADAO = CP.CSI_CODPAC AND
                                                                    VIS.COMPETENCIA = @competencia
                                                            @filtro1)
                                                    END VISITADO
                                                   FROM VS_ESTABELECIMENTOS EST
                                                   JOIN ESUS_MICROAREA MI ON (EST.ID_MICROAREA = MI.ID)
                                                   JOIN ESUS_FAMILIA FAM ON (EST.ID = FAM.ID_DOMICILIO)
                                                   JOIN TSI_CADPAC CP ON (FAM.ID = CP.ID_FAMILIA)
                                                   LEFT JOIN ESUS_EQUIPES EQ ON EQ.ID = MI.ID_EQUIPE
                                                   WHERE COALESCE(CP.DIABETES, 'F') = 'T'
                                                   ORDER BY CP.CSI_NOMPAC) TAB
                                                   @filtro2";

        string IGestaoFamiliaCommand.GetDiabeticosByEquipe { get => sqlGetDiabeticosByEquipe; }

        public string sqlGetHipertensoByEquipe = $@"@select TAB.ID, TAB.NOME_INDIVIDUO, TAB.EQUIPE, TAB.VISITADO
                                                    FROM (SELECT CP.CSI_CODPAC ID, CP.CSI_NOMPAC NOME_INDIVIDUO, EQ.DESCRICAO EQUIPE,
                                                            CASE WHEN(SELECT FIRST 1 COALESCE(VIS.DESFECHO, 0) FROM ESUS_VISITA_DOMICILIAR VIS
                                                              WHERE VIS.ID_CIDADAO = CP.CSI_CODPAC AND
                                                                    VIS.COMPETENCIA = @competencia
                                                                    @filtro1) IS NULL THEN NULL
                                                            ELSE (SELECT FIRST 1 VIS.DESFECHO FROM ESUS_VISITA_DOMICILIAR VIS
                                                                  WHERE VIS.ID_CIDADAO = CP.CSI_CODPAC AND
                                                                            VIS.COMPETENCIA = @competencia
                                                                    @filtro1)
                                                            END VISITADO
                                                            FROM VS_ESTABELECIMENTOS EST
                                                            JOIN ESUS_MICROAREA MI ON (EST.ID_MICROAREA = MI.ID)
                                                            JOIN ESUS_FAMILIA FAM ON (EST.ID = FAM.ID_DOMICILIO)
                                                            JOIN TSI_CADPAC CP ON (FAM.ID = CP.ID_FAMILIA)
                                                            LEFT JOIN ESUS_EQUIPES EQ ON EQ.ID = MI.ID_EQUIPE
                                                            WHERE COALESCE(CP.HIPERTENSO, 'F') = 'T'
                                                            ORDER BY CP.CSI_NOMPAC) TAB
                                                            @filtro2";

        string IGestaoFamiliaCommand.GetHipertensosByEquipe { get => sqlGetHipertensoByEquipe; }

        public string sqlGetGestanteByEquipe = $@" @select TAB.ID, TAB.NOME_INDIVIDUO, TAB.EQUIPE,
                                                                            TAB.VISITADO
                                                   FROM (
                                                   SELECT CP.CSI_CODPAC ID,
                                                          CP.CSI_NOMPAC NOME_INDIVIDUO,
                                                          EQ.DESCRICAO EQUIPE,
                                                          CASE WHEN(SELECT FIRST 1 COALESCE(VIS.DESFECHO, 0) FROM ESUS_VISITA_DOMICILIAR VIS
                                                                       WHERE VIS.ID_CIDADAO = CP.CSI_CODPAC AND
                                                                             VIS.DATA_VISITA BETWEEN GI.DUM AND COALESCE(GI.DATA_NASCIMENTO,CURRENT_TIMESTAMP)
                                                                     @filtro1) IS NULL THEN NULL
                                                             ELSE (SELECT FIRST 1 VIS.DESFECHO FROM ESUS_VISITA_DOMICILIAR VIS
                                                                       WHERE VIS.ID_CIDADAO = CP.CSI_CODPAC AND
                                                                             VIS.DATA_VISITA BETWEEN GI.DUM AND COALESCE(GI.DATA_NASCIMENTO,CURRENT_TIMESTAMP)
                                                                     @filtro1)
                                                             END VISITADO
                                                   FROM VS_ESTABELECIMENTOS EST
                                                   JOIN ESUS_MICROAREA MI ON (EST.ID_MICROAREA = MI.ID)
                                                   JOIN ESUS_FAMILIA FAM ON (EST.ID = FAM.ID_DOMICILIO)
                                                   JOIN TSI_CADPAC CP ON (FAM.ID = CP.ID_FAMILIA)
                                                   JOIN GESTACAO G ON (CP.CSI_CODPAC = G.ID_CIDADAO)
                                                   JOIN GESTACAO_ITEM GI ON GI.ID_GESTACAO = G.ID
                                                   LEFT JOIN ESUS_EQUIPES EQ ON EQ.ID = MI.ID_EQUIPE
                                                   WHERE COALESCE(G.FLG_GESTACAO_EM_ANDAMENTO, 'F') = 'T' AND
                                                         @competencia BETWEEN  CAST((EXTRACT(YEAR FROM GI.DUM)||CASE WHEN EXTRACT(MONTH FROM GI.DUM) < 10 THEN '0'||EXTRACT(MONTH FROM GI.DUM) ELSE EXTRACT(MONTH FROM GI.DUM) END)AS INTEGER) AND
                                                         CAST((EXTRACT(YEAR FROM COALESCE(GI.DATA_NASCIMENTO,CURRENT_TIMESTAMP))||
                                                         CASE WHEN EXTRACT(MONTH FROM COALESCE(GI.DATA_NASCIMENTO,CURRENT_TIMESTAMP)) < 10 THEN '0'||EXTRACT(MONTH FROM COALESCE(GI.DATA_NASCIMENTO,CURRENT_TIMESTAMP)) ELSE EXTRACT(MONTH FROM COALESCE(GI.DATA_NASCIMENTO,CURRENT_TIMESTAMP)) END)AS INTEGER)
                                                   ORDER BY CP.CSI_NOMPAC) TAB
                                                   @filtro2";

        string IGestaoFamiliaCommand.GetGestantesByEquipe { get => sqlGetGestanteByEquipe; }

        public string sqlGetCriancaByEquipe = $@"@select
                                                 TAB.ID, TAB.NOME_INDIVIDUO, TAB.EQUIPE, TAB.VISITADO FROM (
                                                 SELECT
                                                    CP.CSI_CODPAC ID,
                                                    CP.CSI_NOMPAC NOME_INDIVIDUO,
                                                    EQ.DESCRICAO EQUIPE,
                                                    CASE WHEN(SELECT FIRST 1 COALESCE(VIS.DESFECHO,0) FROM ESUS_VISITA_DOMICILIAR VIS
                                                           WHERE VIS.ID_CIDADAO = CP.CSI_CODPAC AND
                                                                 VIS.COMPETENCIA = @competencia
                                                         @filtro1) IS NULL THEN NULL
                                                    ELSE (SELECT FIRST 1 VIS.DESFECHO FROM ESUS_VISITA_DOMICILIAR VIS
                                                           WHERE VIS.ID_CIDADAO = CP.CSI_CODPAC AND
                                                                 VIS.COMPETENCIA = @competencia
                                                         @filtro1)
                                                 END VISITADO
                                                 FROM VS_ESTABELECIMENTOS EST
                                                 JOIN ESUS_MICROAREA MI ON (EST.ID_MICROAREA = MI.ID)
                                                 JOIN ESUS_FAMILIA FAM ON (EST.ID = FAM.ID_DOMICILIO)
                                                 JOIN TSI_CADPAC CP ON (FAM.ID = CP.ID_FAMILIA)
                                                 LEFT JOIN ESUS_EQUIPES EQ ON EQ.ID = MI.ID_EQUIPE
                                                 WHERE (SELECT IDADE FROM PRO_CALCULA_IDADE(CP.CSI_DTNASC, CURRENT_DATE)) <= 12
                                                 ORDER BY CP.CSI_NOMPAC) TAB
                                                 @filtro2 ";

        string IGestaoFamiliaCommand.GetCriancasByEquipe { get => sqlGetCriancaByEquipe; }

        public string sqlGetIdosoByEquipe = $@"@select
                                               TAB.ID, TAB.NOME_INDIVIDUO, TAB.EQUIPE, TAB.VISITADO FROM (
                                               SELECT
                                                   CP.CSI_CODPAC ID,
                                                   CP.CSI_NOMPAC NOME_INDIVIDUO,
                                                   EQ.DESCRICAO EQUIPE,
                                                   CASE WHEN(SELECT FIRST 1 COALESCE(VIS.DESFECHO, 0) FROM ESUS_VISITA_DOMICILIAR VIS
                                                         WHERE VIS.ID_CIDADAO = CP.CSI_CODPAC AND
                                                               VIS.COMPETENCIA = @competencia
                                                       @filtro1) IS NULL THEN NULL
                                                   ELSE (SELECT FIRST 1 VIS.DESFECHO FROM ESUS_VISITA_DOMICILIAR VIS
                                                     WHERE VIS.ID_CIDADAO = CP.CSI_CODPAC AND
                                                               VIS.COMPETENCIA = @competencia
                                                       @filtro1)
                                                   END VISITADO
                                               FROM VS_ESTABELECIMENTOS EST
                                               JOIN ESUS_MICROAREA MI ON (EST.ID_MICROAREA = MI.ID)
                                               JOIN ESUS_FAMILIA FAM ON (EST.ID = FAM.ID_DOMICILIO)
                                               JOIN TSI_CADPAC CP ON (FAM.ID = CP.ID_FAMILIA)
                                               LEFT JOIN ESUS_EQUIPES EQ ON EQ.ID = MI.ID_EQUIPE
                                               WHERE (SELECT IDADE FROM PRO_CALCULA_IDADE(CP.CSI_DTNASC, CURRENT_DATE)) >= 60
                                               ORDER BY CP.CSI_NOMPAC) TAB
                                               @filtro2";

        string IGestaoFamiliaCommand.GetIdososByEquipe { get => sqlGetIdosoByEquipe; }

        public string sqlGetTotaisDiabeticoByEquipe = $@"SELECT COUNT(TAB.CSI_CODPAC) TOTAL, COUNT(TAB.VISITADOS) VISITADOS, SUM(TAB.NAO_VISITADOS) NAO_VISITADOS,
                                                             COUNT(TAB.AUSENTES_RECUSADOS) AUSENTES_RECUSADOS
                                                         FROM (SELECT CP.CSI_CODPAC,
                                                               (SELECT FIRST 1 1 FROM ESUS_VISITA_DOMICILIAR VIS
                                                                WHERE VIS.ID_CIDADAO = CP.CSI_CODPAC AND
                                                                      VIS.COMPETENCIA = @competencia AND
                                                                      VIS.DESFECHO = 0) VISITADOS,
                                                                 CASE WHEN(SELECT FIRST 1 1 FROM ESUS_VISITA_DOMICILIAR VIS
                                                                    WHERE VIS.ID_CIDADAO = CP.CSI_CODPAC AND
                                                                          VIS.COMPETENCIA = @competencia) IS NULL THEN 1
                                                                 ELSE 0
                                                                 END NAO_VISITADOS,
                                                               (SELECT FIRST 1 1 FROM ESUS_VISITA_DOMICILIAR VIS
                                                                WHERE VIS.ID_CIDADAO = CP.CSI_CODPAC AND
                                                                      VIS.COMPETENCIA = @competencia AND
                                                                      (VIS.DESFECHO = 1 OR
                                                                       VIS.DESFECHO = 2)) AUSENTES_RECUSADOS
                                                              FROM VS_ESTABELECIMENTOS EST
                                                              JOIN ESUS_MICROAREA MI ON (EST.ID_MICROAREA = MI.ID)
                                                              JOIN ESUS_FAMILIA FAM ON (EST.ID = FAM.ID_DOMICILIO)
                                                              JOIN TSI_CADPAC CP ON (FAM.ID = CP.ID_FAMILIA)
                                                              LEFT JOIN ESUS_EQUIPES EQ ON EQ.ID = MI.ID_EQUIPE
                                                              WHERE @filtros
                                                                    COALESCE(CP.DIABETES, 'F') = 'T') TAB";
        string IGestaoFamiliaCommand.GetTotaisDiabeticoByEquipe { get => sqlGetTotaisDiabeticoByEquipe; }

        public string sqlGetTotaisHipertensoByEquipe = $@"SELECT COUNT(TAB.CSI_CODPAC) TOTAL, COUNT(TAB.VISITADOS) VISITADOS, SUM(TAB.NAO_VISITADOS) NAO_VISITADOS,
                                                                 COUNT(TAB.AUSENTES_RECUSADOS) AUSENTES_RECUSADOS
                                                          FROM (SELECT CP.CSI_CODPAC,
                                                                   (SELECT FIRST 1 1 FROM ESUS_VISITA_DOMICILIAR VIS
                                                                WHERE VIS.ID_CIDADAO = CP.CSI_CODPAC AND
                                                                      VIS.COMPETENCIA = @competencia AND
                                                                      VIS.DESFECHO = 0) VISITADOS,
                                                                 CASE WHEN(SELECT FIRST 1 1 FROM ESUS_VISITA_DOMICILIAR VIS
                                                                    WHERE VIS.ID_CIDADAO = CP.CSI_CODPAC AND
                                                                          VIS.COMPETENCIA = @competencia) IS NULL THEN 1
                                                                 ELSE 0
                                                                 END NAO_VISITADOS,
                                                               (SELECT FIRST 1 1 FROM ESUS_VISITA_DOMICILIAR VIS
                                                                WHERE VIS.ID_CIDADAO = CP.CSI_CODPAC AND
                                                                      VIS.COMPETENCIA = @competencia AND
                                                                      (VIS.DESFECHO = 1 OR
                                                                       VIS.DESFECHO = 2)) AUSENTES_RECUSADOS
                                                                  FROM VS_ESTABELECIMENTOS EST
                                                                  JOIN ESUS_MICROAREA MI ON (EST.ID_MICROAREA = MI.ID)
                                                                  JOIN ESUS_FAMILIA FAM ON (EST.ID = FAM.ID_DOMICILIO)
                                                                  JOIN TSI_CADPAC CP ON (FAM.ID = CP.ID_FAMILIA)
                                                                  LEFT JOIN ESUS_EQUIPES EQ ON EQ.ID = MI.ID_EQUIPE
                                                                  WHERE @filtros
                                                                        COALESCE(CP.HIPERTENSO, 'F') = 'T') TAB";
        string IGestaoFamiliaCommand.GetTotaisHipertensoByEquipe { get => sqlGetTotaisHipertensoByEquipe; }

        public string sqlGetTotaisGestanteByEquipe = $@"SELECT COUNT(TAB.CSI_CODPAC) TOTAL,COUNT(TAB.VISITADOS) VISITADOS, SUM(TAB.NAO_VISITADOS) NAO_VISITADOS,
                                                               COUNT(TAB.AUSENTES_RECUSADOS) AUSENTES_RECUSADOS
                                                        FROM (SELECT CP.CSI_CODPAC,
                                                              (SELECT FIRST 1 1 FROM ESUS_VISITA_DOMICILIAR VIS
                                                                  WHERE VIS.ID_CIDADAO = CP.CSI_CODPAC AND
                                                                        VIS.DATA_VISITA BETWEEN GI.DUM AND COALESCE(GI.DATA_NASCIMENTO,CURRENT_TIMESTAMP) AND
                                                                        VIS.DESFECHO = 0) VISITADOS,
                                                                   CASE WHEN(SELECT FIRST 1 1 FROM ESUS_VISITA_DOMICILIAR VIS
                                                                             WHERE VIS.ID_CIDADAO = CP.CSI_CODPAC AND
                                                                                   VIS.DATA_VISITA BETWEEN GI.DUM AND COALESCE(GI.DATA_NASCIMENTO,CURRENT_TIMESTAMP)) IS NULL THEN 1
                                                                   ELSE 0
                                                                   END NAO_VISITADOS,
                                                                 (SELECT  FIRST 1 1 FROM ESUS_VISITA_DOMICILIAR VIS
                                                                  WHERE VIS.ID_CIDADAO = CP.CSI_CODPAC AND
                                                                        VIS.DATA_VISITA BETWEEN GI.DUM AND COALESCE(GI.DATA_NASCIMENTO,CURRENT_TIMESTAMP) AND
                                                                        (VIS.DESFECHO = 1 OR
                                                                         VIS.DESFECHO = 2)
                                                                  ORDER BY VIS.DESFECHO) AUSENTES_RECUSADOS
                                                        FROM VS_ESTABELECIMENTOS EST
                                                        JOIN ESUS_MICROAREA MI ON (EST.ID_MICROAREA = MI.ID)
                                                        JOIN ESUS_FAMILIA FAM ON (EST.ID = FAM.ID_DOMICILIO)
                                                        JOIN TSI_CADPAC CP ON (FAM.ID = CP.ID_FAMILIA)
                                                        JOIN GESTACAO G ON (CP.CSI_CODPAC = G.ID_CIDADAO)
                                                        JOIN GESTACAO_ITEM GI ON GI.ID_GESTACAO = G.ID
                                                        LEFT JOIN ESUS_EQUIPES EQ ON EQ.ID = MI.ID_EQUIPE
                                                        WHERE @filtros
                                                              COALESCE(G.FLG_GESTACAO_EM_ANDAMENTO, 'F') = 'T' AND
                                                              @competencia BETWEEN  CAST((EXTRACT(YEAR FROM GI.DUM)||CASE WHEN EXTRACT(MONTH FROM GI.DUM) < 10 THEN '0'||EXTRACT(MONTH FROM GI.DUM) ELSE EXTRACT(MONTH FROM GI.DUM) END)AS INTEGER) AND
                                                                       CAST((EXTRACT(YEAR FROM COALESCE(GI.DATA_NASCIMENTO,CURRENT_TIMESTAMP))||
                                                                       CASE WHEN EXTRACT(MONTH FROM COALESCE(GI.DATA_NASCIMENTO,CURRENT_TIMESTAMP)) < 10
                                                                       THEN '0'||EXTRACT(MONTH FROM COALESCE(GI.DATA_NASCIMENTO,CURRENT_TIMESTAMP))
                                                                       ELSE EXTRACT(MONTH FROM COALESCE(GI.DATA_NASCIMENTO,CURRENT_TIMESTAMP)) END)AS INTEGER)) TAB";
        string IGestaoFamiliaCommand.GetTotaisGestanteByEquipe { get => sqlGetTotaisGestanteByEquipe; }

        public string sqlGetTotaisCriancasByEquipe = $@"SELECT COUNT(TAB.CSI_CODPAC) TOTAL, COUNT(TAB.VISITADOS) VISITADOS, SUM(TAB.NAO_VISITADOS) NAO_VISITADOS,
                                                        COUNT(TAB.AUSENTES_RECUSADOS) AUSENTES_RECUSADOS
                                                        FROM (SELECT CP.CSI_CODPAC,
                                                          (SELECT FIRST 1 1 FROM ESUS_VISITA_DOMICILIAR VIS
                                                            WHERE VIS.ID_CIDADAO = CP.CSI_CODPAC AND
                                                                  VIS.COMPETENCIA = @competencia AND
                                                                  VIS.DESFECHO = 0) VISITADOS,
                                                             CASE WHEN(SELECT FIRST 1 1 FROM ESUS_VISITA_DOMICILIAR VIS
                                                                WHERE VIS.ID_CIDADAO = CP.CSI_CODPAC AND
                                                                      VIS.COMPETENCIA = @competencia) IS NULL THEN 1
                                                             ELSE 0
                                                             END NAO_VISITADOS,
                                                           (SELECT FIRST 1 1 FROM ESUS_VISITA_DOMICILIAR VIS
                                                            WHERE VIS.ID_CIDADAO = CP.CSI_CODPAC AND
                                                                  VIS.COMPETENCIA = @competencia AND
                                                                  (VIS.DESFECHO = 1 OR
                                                                   VIS.DESFECHO = 2)) AUSENTES_RECUSADOS
                                                        FROM VS_ESTABELECIMENTOS EST
                                                        JOIN ESUS_MICROAREA MI ON (EST.ID_MICROAREA = MI.ID)
                                                        JOIN ESUS_FAMILIA FAM ON (EST.ID = FAM.ID_DOMICILIO)
                                                        JOIN TSI_CADPAC CP ON (FAM.ID = CP.ID_FAMILIA)
                                                        LEFT JOIN ESUS_EQUIPES EQ ON EQ.ID = MI.ID_EQUIPE
                                                        WHERE @filtros
                                                                  (SELECT IDADE FROM PRO_CALCULA_IDADE(CP.CSI_DTNASC, CURRENT_DATE)) <= 12) TAB";
        string IGestaoFamiliaCommand.GetTotaisCriancasByEquipe { get => sqlGetTotaisCriancasByEquipe; }

        public string sqlGetTotaisIdososByEquipe = $@"SELECT COUNT(TAB.CSI_CODPAC) TOTAL, COUNT(TAB.VISITADOS) VISITADOS, SUM(TAB.NAO_VISITADOS) NAO_VISITADOS,
                                                      COUNT(TAB.AUSENTES_RECUSADOS) AUSENTES_RECUSADOS
                                                      FROM (SELECT CP.CSI_CODPAC,
                                                      (SELECT FIRST 1 1 FROM ESUS_VISITA_DOMICILIAR VIS
                                                          WHERE VIS.ID_CIDADAO = CP.CSI_CODPAC AND
                                                                VIS.COMPETENCIA = @competencia AND
                                                                VIS.DESFECHO = 0) VISITADOS,
                                                           CASE WHEN(SELECT FIRST 1 1 FROM ESUS_VISITA_DOMICILIAR VIS
                                                              WHERE VIS.ID_CIDADAO = CP.CSI_CODPAC AND
                                                                    VIS.COMPETENCIA = @competencia ) IS NULL THEN 1
                                                           ELSE 0
                                                           END NAO_VISITADOS,
                                                         (SELECT FIRST 1 1 FROM ESUS_VISITA_DOMICILIAR VIS
                                                          WHERE VIS.ID_CIDADAO = CP.CSI_CODPAC AND
                                                                VIS.COMPETENCIA = @competencia AND
                                                                (VIS.DESFECHO = 1 OR
                                                                 VIS.DESFECHO = 2)) AUSENTES_RECUSADOS
                                                      FROM VS_ESTABELECIMENTOS EST
                                                      JOIN ESUS_MICROAREA MI ON (EST.ID_MICROAREA = MI.ID)
                                                      JOIN ESUS_FAMILIA FAM ON (EST.ID = FAM.ID_DOMICILIO)
                                                      JOIN TSI_CADPAC CP ON (FAM.ID = CP.ID_FAMILIA)
                                                      LEFT JOIN ESUS_EQUIPES EQ ON EQ.ID = MI.ID_EQUIPE
                                                      WHERE @filtros
                                                          (SELECT IDADE FROM PRO_CALCULA_IDADE(CP.CSI_DTNASC, CURRENT_DATE)) >= 60) TAB";
        string IGestaoFamiliaCommand.GetTotaisIdososByEquipe { get => sqlGetTotaisIdososByEquipe; }
    }
}
