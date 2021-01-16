using RgCidadao.Domain.Commands.Endemias;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Queries.Endemias
{
    public class VisitaCommandText : IVisitaCommand
    {
        public string sqlGetAllPagination = $@"SELECT FIRST (@pagesize) SKIP (@page)
                                               VI.ID ID_VISITA_IMOVEL,VI.UUID_REGISTRO_MOBILE,VI.ID_IMOVEL, I.QUARTEIRAO_LOGRADOURO,
                                               VI.DATA_HORA_ENTRADA, VI.DATA_HORA_SAIDA, VI.DATA_HORA_REGISTRO, VI.COMPETENCIA,
                                               VI.DESFECHO,VI.ATIVIDADE,VI.TIPO_VISITA,VI.ENCONTROU_FOCO, I.NUMERO_LOGRADOURO, 
                                               VI.DEPOSITO_INSPECIONADO_A1,VI.DEPOSITO_INSPECIONADO_A2, I.COMPLEMENTO_LOGRADOURO,
                                               VI.DEPOSITO_INSPECIONADO_B,VI.DEPOSITO_INSPECIONADO_C,B.CSI_NOMBAI,
                                               VI.DEPOSITO_INSPECIONADO_D1,VI.DEPOSITO_INSPECIONADO_D2, I.SEQUENCIA_QUARTEIRAO,
                                               I.SEQUENCIA_NUMERO,
                                               VI.DEPOSITO_INSPECIONADO_E,VI.DEPOSITO_ELIMINADO,VI.PENDENCIA_DESCRICAO,
                                               VI.TRABALHO_EDUCATIVO,VI.TRABALHO_MECANICO,VI.TRABALHO_QUIMICO,
                                               VI.TRAT_FOCAL_LARVI1_TIPO,VI.TRAT_FOCAL_LARVI1_QTD_GRAMAS,
                                               VI.TRAT_FOCAL_LARVI1_QTD_DEP_TRAT,
                                               VI.TRAT_PERIFOCAL_ADULT_TIPO,VI.TRAT_PERIFOCAL_ADULT_QTD_CARGA,
                                               VI.LATITUDE_CADASTRO,VI.LONGITUDE_CADASTRO,VI.LATITUDE_FOCO,
                                               VI.LONGITUDE_FOCO,VI.ID_PROFISSIONAL,VI.DATA_ALTERACAO_SERV, M.CSI_NOMMED, EC.NUM_CICLO,
                                               I.NOME_FANTASIA_APELIDO, I.RAZAO_SOCIAL_NOME, L.CSI_NOMEND LOGRADOURO
                                               FROM VISITA_IMOVEL VI
                                               INNER JOIN VS_ESTABELECIMENTOS I ON I.ID = VI.ID_ESTABELECIMENTO
                                               INNER JOIN TSI_MEDICOS M ON M.CSI_CODMED = VI.ID_PROFISSIONAL
                                               INNER JOIN TSI_LOGRADOURO L ON L.CSI_CODEND = I.ID_LOGRADOURO
                                               INNER JOIN TSI_BAIRRO B ON B.CSI_CODBAI = L.CSI_CODBAI
                                               INNER JOIN TSI_MEDICOS_UNIDADE MU ON MU.CSI_CODMED = M.CSI_CODMED
                                               INNER JOIN ENDEMIAS_CICLOS EC ON EC.ID = VI.ID_CICLO
                                               WHERE MU.CSI_CBO = '515140' AND 
                                                     MU.CSI_ATIVADO = 'T'
                                               @filtro
                                               ORDER BY VI.ID DESC";
        string IVisitaCommand.GetAllPagination { get => sqlGetAllPagination; }

        public string sqlGetCountAll = $@"SELECT COUNT(*) FROM (SELECT
                                                                VI.ID, M.CSI_NOMMED
                                                                FROM VISITA_IMOVEL VI
                                                                INNER JOIN VS_ESTABELECIMENTOS I ON I.ID = VI.ID_ESTABELECIMENTO
                                                                INNER JOIN TSI_MEDICOS M ON M.CSI_CODMED = VI.ID_PROFISSIONAL
                                                                INNER JOIN TSI_LOGRADOURO L ON L.CSI_CODEND = I.ID_LOGRADOURO
                                                                INNER JOIN TSI_BAIRRO B ON B.CSI_CODBAI = L.CSI_CODBAI
                                                                INNER JOIN TSI_MEDICOS_UNIDADE MU ON MU.CSI_CODMED = M.CSI_CODMED
                                                                INNER JOIN ENDEMIAS_CICLOS EC ON EC.ID = VI.ID_CICLO
                                                                WHERE 1=1 and MU.CSI_CBO = '515140' AND MU.CSI_ATIVADO = 'T'
                                                                @filtro)";
        string IVisitaCommand.GetCountAll { get => sqlGetCountAll; }

        public string sqlGetNewIdVisita = $@"SELECT GEN_ID(GEN_VISITA_IMOVEL_ID, 1) AS VLR FROM RDB$DATABASE";
        string IVisitaCommand.GetNewIdVisita { get => sqlGetNewIdVisita; }

        public string sqlInsertVisita = $@"INSERT INTO VISITA_IMOVEL (ID, UUID_REGISTRO_MOBILE, ID_IMOVEL, DATA_HORA_ENTRADA, DATA_HORA_SAIDA, DATA_HORA_REGISTRO,
                                                                      COMPETENCIA, DESFECHO, ATIVIDADE, TIPO_VISITA, ENCONTROU_FOCO, DEPOSITO_INSPECIONADO_A1,
                                                                      DEPOSITO_INSPECIONADO_A2, DEPOSITO_INSPECIONADO_B, DEPOSITO_INSPECIONADO_C,
                                                                      DEPOSITO_INSPECIONADO_D1, DEPOSITO_INSPECIONADO_D2, DEPOSITO_INSPECIONADO_E,
                                                                      DEPOSITO_ELIMINADO, PENDENCIA_DESCRICAO, TRABALHO_EDUCATIVO, TRABALHO_MECANICO,
                                                                      TRABALHO_QUIMICO, TRAT_FOCAL_LARVI1_TIPO, TRAT_FOCAL_LARVI1_QTD_GRAMAS,
                                                                      TRAT_FOCAL_LARVI1_QTD_DEP_TRAT,  TRAT_PERIFOCAL_ADULT_TIPO, TRAT_PERIFOCAL_ADULT_QTD_CARGA,
                                                                      LATITUDE_CADASTRO, LONGITUDE_CADASTRO, LATITUDE_FOCO, LONGITUDE_FOCO, ID_PROFISSIONAL,
                                                                      DATA_ALTERACAO_SERV, NUMERO_TUBITO, ID_ESUS_EXPORTACAO_ITEM, ID_ESTABELECIMENTO, ID_CICLO)
                                           VALUES (@id, @uuid_registro_mobile, @id_imovel, @data_hora_entrada, @data_hora_saida, @data_hora_registro, @competencia,
                                                   @desfecho, @atividade, @tipo_visita, @encontrou_foco, @deposito_inspecionado_a1, @deposito_inspecionado_a2,
                                                   @deposito_inspecionado_b, @deposito_inspecionado_c, @deposito_inspecionado_d1, @deposito_inspecionado_d2,
                                                   @deposito_inspecionado_e, @deposito_eliminado, @pendencia_descricao, @trabalho_educativo, @trabalho_mecanico,
                                                   @trabalho_quimico, @trat_focal_larvi1_tipo, @trat_focal_larvi1_qtd_gramas, @trat_focal_larvi1_qtd_dep_trat,
                                                   @trat_perifocal_adult_tipo,
                                                   @trat_perifocal_adult_qtd_carga, @latitude_cadastro, @longitude_cadastro, @latitude_foco, @longitude_foco,
                                                   @id_profissional, @data_alteracao_serv, @numero_tubito, @id_esus_exportacao_item, @id_estabelecimento, @id_ciclo)";
        string IVisitaCommand.InsertVisita { get => sqlInsertVisita; }

        public string sqlGetNewIdColeta = $@"SELECT GEN_ID(GEN_VISITA_IMOVEL_ID, 1) AS VLR FROM RDB$DATABASE";
        string IVisitaCommand.GetNewIdColeta { get => sqlGetNewIdColeta; }

        public string sqlInsertColeta = $@"INSERT INTO VA_COLETA (ID, UUID_REGISTRO_MOBILE, ID_VISITA, DEPOSITO, AMOSTRA, ID_PROFISSIONAL, QTDE_LARVAS)
                                           VALUES (@id, @uuid_registro_mobile, @id_visita, @deposito, @amostra, @id_profissional, @qtde_larvas)";

        string IVisitaCommand.InsertColeta { get => sqlInsertColeta; }

        public string sqlGetVisitaById = $@"SELECT VI.*, M.CSI_NOMMED, I.QUARTEIRAO_LOGRADOURO,
                                                   I.NUMERO_LOGRADOURO, I.SEQUENCIA_QUARTEIRAO, I.SEQUENCIA_NUMERO,
                                                   I.COMPLEMENTO_LOGRADOURO, LOGR.CSI_NOMEND, BAR.CSI_NOMBAI,
                                                   I.RAZAO_SOCIAL_NOME, EC.NUM_CICLO, EC.ID ID_CICLO
                                            FROM VISITA_IMOVEL VI
                                            LEFT JOIN VS_ESTABELECIMENTOS I ON I.ID = VI.ID_ESTABELECIMENTO
                                            LEFT JOIN TSI_LOGRADOURO LOGR ON LOGR.CSI_CODEND = I.ID_LOGRADOURO
                                            LEFT JOIN TSI_BAIRRO BAR ON BAR.CSI_CODBAI = LOGR.CSI_CODBAI
                                            LEFT JOIN TSI_MEDICOS M ON M.CSI_CODMED = VI.ID_PROFISSIONAL
                                            LEFT JOIN ENDEMIAS_CICLOS EC ON EC.ID = VI.ID_CICLO
                                            WHERE VI.ID = @id ORDER BY VI.ID DESC";
        string IVisitaCommand.GetVisitaById { get => sqlGetVisitaById; }

        public string sqlGetColetaByVisita = $@"SELECT * FROM VA_COLETA
                                                WHERE ID_VISITA = @id_visita";
        string IVisitaCommand.GetColetaByVisita { get => sqlGetColetaByVisita; }

        public string sqlUpdateVisita = $@"UPDATE VISITA_IMOVEL
                                           SET UUID_REGISTRO_MOBILE = @uuid_registro_mobile,
                                               ID_IMOVEL = @id_imovel,
                                               DATA_HORA_ENTRADA = @data_hora_entrada,
                                               DATA_HORA_SAIDA = @data_hora_saida,
                                               DATA_HORA_REGISTRO = @data_hora_registro,
                                               COMPETENCIA = @competencia,
                                               DESFECHO = @desfecho,
                                               ATIVIDADE = @atividade,
                                               TIPO_VISITA = @tipo_visita,
                                               ENCONTROU_FOCO = @encontrou_foco,
                                               DEPOSITO_INSPECIONADO_A1 = @deposito_inspecionado_a1,
                                               DEPOSITO_INSPECIONADO_A2 = @deposito_inspecionado_a2,
                                               DEPOSITO_INSPECIONADO_B = @deposito_inspecionado_b,
                                               DEPOSITO_INSPECIONADO_C = @deposito_inspecionado_c,
                                               DEPOSITO_INSPECIONADO_D1 = @deposito_inspecionado_d1,
                                               DEPOSITO_INSPECIONADO_D2 = @deposito_inspecionado_d2,
                                               DEPOSITO_INSPECIONADO_E = @deposito_inspecionado_e,
                                               DEPOSITO_ELIMINADO = @deposito_eliminado,
                                               PENDENCIA_DESCRICAO = @pendencia_descricao,
                                               TRABALHO_EDUCATIVO = @trabalho_educativo,
                                               TRABALHO_MECANICO = @trabalho_mecanico,
                                               TRABALHO_QUIMICO = @trabalho_quimico,
                                               TRAT_FOCAL_LARVI1_TIPO = @trat_focal_larvi1_tipo,
                                               TRAT_FOCAL_LARVI1_QTD_GRAMAS = @trat_focal_larvi1_qtd_gramas,
                                               TRAT_FOCAL_LARVI1_QTD_DEP_TRAT = @trat_focal_larvi1_qtd_dep_trat,
                                               TRAT_PERIFOCAL_ADULT_TIPO = @trat_perifocal_adult_tipo,
                                               TRAT_PERIFOCAL_ADULT_QTD_CARGA = @trat_perifocal_adult_qtd_carga,
                                               LATITUDE_CADASTRO = @latitude_cadastro,
                                               LONGITUDE_CADASTRO = @longitude_cadastro,
                                               LATITUDE_FOCO = @latitude_foco,
                                               LONGITUDE_FOCO = @longitude_foco,
                                               ID_PROFISSIONAL = @id_profissional,
                                               DATA_ALTERACAO_SERV = @data_alteracao_serv,
                                               NUMERO_TUBITO = @numero_tubito,
                                               ID_ESUS_EXPORTACAO_ITEM = @id_esus_exportacao_item,
                                               ID_ESTABELECIMENTO = @id_estabelecimento,
                                               ID_CICLO = @id_ciclo
                                           WHERE ID = @id";
        string IVisitaCommand.UpdateVisita { get => sqlUpdateVisita; }

        public string sqlUpdateColeta = $@"UPDATE VA_COLETA
                                           SET UUID_REGISTRO_MOBILE = @uuid_registro_mobile,
                                               ID_VISITA = @id_visita,
                                               DEPOSITO = @deposito,
                                               AMOSTRA = @amostra,
                                               ID_PROFISSIONAL = @id_profissional,
                                               QTDE_LARVAS = @qtde_larvas
                                           WHERE ID = @id";
        string IVisitaCommand.UpdateColeta { get => sqlUpdateColeta; }

        public string sqlDeleteVisita = $@"DELETE FROM VISITA_IMOVEL
                                           WHERE ID = @id";
        string IVisitaCommand.DeleteVisita { get => sqlDeleteVisita; }

        public string sqlDeleteColeta = $@"DELETE FROM VA_COLETA
                                           WHERE ID = @id";
        string IVisitaCommand.DeleteColeta { get => sqlDeleteColeta; }

        public string sqlGetVisitaByEstabelecimento = $@"SELECT * FROM VISITA_IMOVEL VI
                                                         JOIN ENDEMIAS_CICLOS EC ON EC.ID = VI.ID_CICLO
                                                         WHERE CAST(VI.DATA_HORA_ENTRADA as DATE) >= @data_inicial AND
                                                               CAST(VI.DATA_HORA_SAIDA as DATE) <= @datafinal AND
                                                               VI.ID_ESTABELECIMENTO = @id_estabelecimento";
        string IVisitaCommand.GetVisitaByEstabelecimento { get => sqlGetVisitaByEstabelecimento; }

        public string sqlGetQuarteiraoEstabelecimentoByBairro = $@"SELECT
                                                                     EST.ID ID_IMOVEL,
                                                                     CASE
                                                                       WHEN EST.RAZAO_SOCIAL_NOME IS NOT NULL THEN EST.RAZAO_SOCIAL_NOME
                                                                       WHEN EST.NOME_FANTASIA_APELIDO IS NOT NULL THEN EST.NOME_FANTASIA_APELIDO
                                                                       WHEN CP.CSI_NOMPAC IS NOT NULL THEN CP.CSI_NOMPAC
                                                                       ELSE 'NÃO INFORMADO'
                                                                     END AS IDENTIFICACAO_IMOVEL,
                                                                     COALESCE(EST.QUARTEIRAO_LOGRADOURO,'0') QUARTEIRAO,
                                                                     COALESCE(EST.SEQUENCIA_QUARTEIRAO,0) SEQUENCIA_QUARTEIRAO,
                                                                     LOG.CSI_CODBAI ID_BAIRRO,
                                                                     EST.TIPO_IMOVEL,
                                                                     EST.NUMERO_LOGRADOURO,
                                                                     EST.SEQUENCIA_NUMERO,
                                                                     LOG.CSI_NOMEND LOGRADOURO,
                                                                     BAI.CSI_NOMBAI BAIRRO,
                                                                     EST.COMPLEMENTO_LOGRADOURO
                                                                   FROM VS_ESTABELECIMENTOS EST
                                                                   JOIN TSI_LOGRADOURO LOG ON (EST.ID_LOGRADOURO = LOG.CSI_CODEND)
                                                                   JOIN TSI_BAIRRO BAI ON (BAI.CSI_CODBAI = LOG.CSI_CODBAI)
                                                                   LEFT JOIN ESUS_FAMILIA FAM ON (EST.ID = FAM.ID_DOMICILIO)
                                                                   LEFT JOIN TSI_CADPAC CP ON (FAM.ID_RESPONSAVEL = CP.CSI_CODPAC)
                                                                   WHERE LOG.CSI_CODBAI = @id_bairro @filtro";
        string IVisitaCommand.GetQuarteiraoEstabelecimentoByBairro { get => sqlGetQuarteiraoEstabelecimentoByBairro; }

        public string sqlGetUltimaVisitaCicloByEstabelecimento = $@"SELECT FIRST(1) VI1.ID, EC.NUM_CICLO CICLO, VI1.DATA_HORA_ENTRADA,
                                                                                    VI1.TIPO_VISITA, VI1.DESFECHO, MED.CSI_NOMMED AGENTE
                                                                    FROM VISITA_IMOVEL VI1
                                                                    JOIN ENDEMIAS_CICLOS EC ON EC.ID = VI1.ID_CICLO
                                                                    JOIN TSI_MEDICOS MED ON MED.CSI_CODMED = VI1.ID_PROFISSIONAL
                                                                    WHERE VI1.ID_ESTABELECIMENTO = @id_estabelecimento AND
                                                                          EC.ID = @id_ciclo
                                                                    ORDER BY VI1.ID DESC, VI1.ID DESC";
        string IVisitaCommand.GetUltimaVisitaCicloByEstabelecimento { get => sqlGetUltimaVisitaCicloByEstabelecimento; }

        public string sqlGetVisitasByCiclo = $@"SELECT VI.*
                                                FROM VISITA_IMOVEL VI
                                                JOIN ENDEMIAS_CICLOS EC ON EC.ID = VI.ID_CICLO
                                                WHERE EC.ID = @id_ciclo
                                                ORDER BY VI.DATA_HORA_ENTRADA DESC, VI.ID DESC";
        string IVisitaCommand.GetVisitasByCiclo { get => sqlGetVisitasByCiclo; }

    }
}
