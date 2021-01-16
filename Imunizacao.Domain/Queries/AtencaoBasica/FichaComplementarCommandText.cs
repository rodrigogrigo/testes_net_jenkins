using RgCidadao.Domain.Commands.AtencaoBasica;

namespace RgCidadao.Domain.Queries.AtencaoBasica
{
    public class FichaComplementarCommandText : IFichaComplementarCommand
    {
        public string sqlGetAllPagination = $@"SELECT FIRST (@pagesize) SKIP (@page) FC.ID, FC.DATA, MED.CSI_NOMMED, PAC.CSI_NOMPAC , UNI.CSI_NOMUNI
                                                FROM ESUS_FICHA_COMPLEMENTAR FC
                                                    LEFT JOIN TSI_MEDICOS MED
                                                        ON(MED.CSI_CODMED = FC.ID_PROFISSIONAL)
                                                    LEFT JOIN TSI_CADPAC PAC
                                                        ON(PAC.CSI_NCARTAO = FC.CNS_CIDADAO)
                                                    LEFT JOIN TSI_UNIDADE UNI
                                                    ON (UNI.CSI_CODUNI = FC.ID_UNIDADE) 
                                                @filtros
                                                ORDER BY FC.ID DESC";
        string IFichaComplementarCommand.GetAllPagination { get => sqlGetAllPagination; }

        public string sqlGetCountAll = $@"SELECT COUNT(*) FROM (SELECT FC.ID, FC.DATA, MED.CSI_NOMMED, PAC.CSI_NOMPAC , UNI.CSI_NOMUNI
                                                FROM ESUS_FICHA_COMPLEMENTAR FC
                                                    LEFT JOIN TSI_MEDICOS MED
                                                        ON(MED.CSI_CODMED = FC.ID_PROFISSIONAL)
                                                    LEFT JOIN TSI_CADPAC PAC
                                                        ON(PAC.CSI_NCARTAO = FC.CNS_CIDADAO)
                                                    LEFT JOIN TSI_UNIDADE UNI
                                                    ON (UNI.CSI_CODUNI = FC.ID_UNIDADE) 
                                                @filtros
                                                ORDER BY FC.ID DESC)";
        string IFichaComplementarCommand.GetCountAll { get => sqlGetCountAll; }

        public string sqlGetFichaComplementarById = $@"SELECT
                                                        FC.DATA, FC.TURNO, FC.DATA_TESTE_OLHINHO, FC.FLG_TESTE_OLHINHO, FC.DATA_EXAME_FUNDO_OLHO, FC.FLG_EXAME_FUNDO_OLHO,
                                                        FC.DATA_TESTE_ORELHINHA_PEATE, FC.FLG_TESTE_ORELHINHA_PEATE, FC.DATA_US_TRANSFONTANELA, FC.FLG_US_TRANSFONTANELA,
                                                        FC.DATA_TOMOGRAFIA, FC.FLG_TOMOGRAFIA, FC.DATA_RESSONANCIA, FC.FLG_RESSONANCIA, FC.CBO_PROFISSIONAL, FC.CNS_PROFISSIONAL, FC.CNS_CIDADAO, FC.CNS_RESPONSAVEL_FAMILIAR,
                                                        MED.CSI_CBO, MED.CSI_CODMED, MED.CSI_NOMMED,(MED.CSI_CODMED || ' - ' || MED.CSI_NOMMED) PROFISSIONAL, MED.CSI_CNS,
                                                        PAC.CSI_CODPAC, PAC.CSI_NOMPAC,(PAC.CSI_CODPAC || ' - ' || PAC.CSI_NOMPAC) PACIENTE,
                                                        UNI.CSI_CODUNI,  UNI.CSI_NOMUNI,(UNI.CSI_CODUNI || ' - ' || UNI.CSI_NOMUNI)UNIDADE,
                                                        (SELECT (CP.CSI_CODPAC || ' - ' || CSI_NOMPAC) FROM TSI_CADPAC CP
                                                            LEFT JOIN ESUS_FICHA_COMPLEMENTAR FC ON (FC.cns_responsavel_familiar = CP.csi_ncartao)
                                                            WHERE FC.ID = @id) AS RESPONSAVEL,
                                                        EQ.DESCRICAO, EQ.COD_INE, EQ.ID
                                                                    FROM ESUS_FICHA_COMPLEMENTAR FC
                                                                        LEFT JOIN TSI_MEDICOS MED
                                                                            ON(MED.CSI_CODMED = FC.ID_PROFISSIONAL)
                                                                        LEFT JOIN TSI_CADPAC PAC
                                                                            ON(PAC.CSI_NCARTAO = FC.CNS_CIDADAO)
                                                                        LEFT JOIN TSI_UNIDADE UNI
                                                                        ON (UNI.CSI_CODUNI = FC.ID_UNIDADE)
                                                                        LEFT JOIN ESUS_EQUIPES EQ
                                                                        ON (EQ.ID = FC.ID_EQUIPE)
                                                        WHERE FC.ID = @id";
        string IFichaComplementarCommand.GetFichaComplementarById { get => sqlGetFichaComplementarById; }

        public string sqlNewId = $@"SELECT GEN_ID(GEN_ESUS_FICHA_COMPLEMENTAR_ID, 1) AS VLR FROM RDB$DATABASE";
        string IFichaComplementarCommand.GetNewId { get => sqlNewId; }

        public string sqlInsert = $@"INSERT INTO ESUS_FICHA_COMPLEMENTAR(
                                            id, id_profissional, id_unidade, cns_profissional, cbo_profissional, cnes, ine_unidade,
                                            data, turno, cns_cidadao, cns_responsavel_familiar, flg_teste_olhinho, data_teste_olhinho, flg_exame_fundo_olho,
                                            data_exame_fundo_olho, flg_teste_orelhinha_peate, data_teste_orelhinha_peate, flg_us_transfontanela, data_us_transfontanela, flg_tomografia, data_tomografia, flg_ressonancia, data_ressonancia,
                                            id_usuario, id_equipe
                                        )
                                         VALUES (
                                             @id, @id_profissional, @id_unidade, @cns_profissional, @cbo_profissional, @cnes, @ine_unidade, @data,
                                             @turno, @cns_cidadao, @cns_responsavel_familiar, @flg_teste_olhinho, @data_teste_olhinho, @flg_exame_fundo_olho, @data_exame_fundo_olho,
                                             @flg_teste_orelhinha_peate, @data_teste_orelhinha_peate, @flg_us_transfontanela, @data_us_transfontanela, @flg_tomografia, @data_tomografia, @flg_ressonancia, @data_ressonancia,
                                             @id_usuario, @id_equipe)";

        string IFichaComplementarCommand.Insert { get => sqlInsert; }

        public string sqlUpdate = $@"UPDATE ESUS_FICHA_COMPLEMENTAR
                                        SET
                                            id_profissional = @id_profissional,
                                            id_unidade = @id_unidade,
                                            cns_profissional = @cns_profissional,
                                            cbo_profissional = @cbo_profissional,
                                            cnes = @cnes,
                                            ine_unidade = @ine_unidade,
                                            data = @data,
                                            turno = @turno,
                                            cns_cidadao = @cns_cidadao,
                                            cns_responsavel_familiar = @cns_responsavel_familiar,
                                            flg_teste_olhinho = @flg_teste_olhinho,
                                            data_teste_olhinho = @data_teste_olhinho,
                                            flg_exame_fundo_olho = @flg_exame_fundo_olho,
                                            data_exame_fundo_olho = @data_exame_fundo_olho,
                                            flg_teste_orelhinha_peate = @flg_teste_orelhinha_peate,
                                            data_teste_orelhinha_peate = @data_teste_orelhinha_peate,
                                            flg_us_transfontanela = @flg_us_transfontanela,
                                            data_us_transfontanela = @data_us_transfontanela,
                                            flg_tomografia = @flg_tomografia,
                                            data_tomografia = @data_tomografia,
                                            flg_ressonancia = @flg_ressonancia,
                                            data_ressonancia = @data_ressonancia,
                                            id_usuario = @id_usuario,
                                            id_equipe = @id_equipe
                                        WHERE id = @id";
        string IFichaComplementarCommand.Update { get => sqlUpdate; }

        public string sqlDelete = $@"DELETE FROM ESUS_FICHA_COMPLEMENTAR
                                     WHERE ID = @id";
        string IFichaComplementarCommand.Delete { get => sqlDelete; }
    }
}
