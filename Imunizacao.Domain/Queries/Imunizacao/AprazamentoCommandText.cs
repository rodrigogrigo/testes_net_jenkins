using RgCidadao.Domain.Commands.Imunizacao;

namespace RgCidadao.Domain.Queries.Imunizacao
{
    public class AprazamentoCommandText : IAprazamentoCommand
    {
        public string sqlGetAprazamentoByCidadao = $@"SELECT
                                                         APZ.ID ID_APRAZAMENTO, APZ.DATA_LIMITE, P.ID ID_PRODUTO, P.NOME NOME_PRODUTO, P.ABREVIATURA ABREVIATURA_PRODUTO,
                                                         P.SIGLA SIGLA_PRODUTO, D.DESCRICAO DESCRICAO_DOSE, D.SIGLA SIGLA_DOSE, V.ID ID_VACINADOS, V.LOTE LOTE,
                                                         MED.CSI_NOMMED PROFISSIONAL, V.DATA_APLICACAO, CB.IDADE_MINIMA, CB.IDADE_MAXIMA, CB.ID_DOSE, APZ.ID_CALENDARIO_BASICO,
                                                         CB.FLG_EXCLUIR_APRAZAMENTO, V.OBSERVACAO, CB.PUBLICO_ALVO, CB.ID_ESTRATEGIA, V.ID_ESUS_EXPORTACAO_ITEM 
                                                      FROM PNI_APRAZAMENTO APZ
                                                      LEFT JOIN PNI_CALENDARIO_BASICO CB ON CB.ID = APZ.ID_CALENDARIO_BASICO
                                                      JOIN PNI_PRODUTO P ON APZ.ID_PRODUTO = P.ID
                                                      JOIN PNI_DOSE D ON APZ.ID_DOSE = D.ID
                                                      LEFT JOIN PNI_VACINADOS V ON APZ.ID_VACINADOS = V.ID
                                                      LEFT JOIN TSI_MEDICOS MED ON V.ID_PROFISIONAL = MED.CSI_CODMED
                                                      WHERE APZ.ID_INDIVIDUO = @id
                                                          AND ((APZ.DATA_LIMITE - COALESCE(CB.DIAS_ANTES_APRAZAMENTO, 0)) <= CURRENT_DATE
                                                          OR APZ.ID_VACINADOS IS NOT NULL) AND 
                                                          COALESCE(V.FLG_EXCLUIDO, 0) = 0
                                                      ORDER BY P.ABREVIATURA";

        string IAprazamentoCommand.GetAprazamentoByCidadao { get => sqlGetAprazamentoByCidadao; }

        public string sqlGetAprazamentoByCalendarioBasico = $@"SELECT *
                                                               FROM PNI_APRAZAMENTO PA
                                                               WHERE PA.ID_CALENDARIO_BASICO = @calendario";
        string IAprazamentoCommand.GetAprazamentoByCalendarioBasico { get => sqlGetAprazamentoByCalendarioBasico; }

        public string sqlInsert = $@"INSERT INTO PNI_APRAZAMENTO(ID, ID_INDIVIDUO,DATA_LIMITE,ID_VACINADOS, ID_PRODUTO, ID_DOSE)
                                     VALUES(@id, @id_individuo, @data_limite, @id_vacinados, @id_produto, @id_dose)";
        string IAprazamentoCommand.Insert { get => sqlInsert; }

        public string sqlUpdateAprazamentoVacinados = $@"UPDATE PNI_APRAZAMENTO
                                                         SET ID_VACINADOS = @id_vacinados
                                                         WHERE ID = @id";
        string IAprazamentoCommand.UpdateAprazamentoVacinados { get => sqlUpdateAprazamentoVacinados; }

        public string sqlGetNewId = $@"SELECT GEN_ID(GEN_PNI_APRAZAMENTO_ID, 1) AS VLR FROM RDB$DATABASE";
        string IAprazamentoCommand.GetNewId { get => sqlGetNewId; }

        public string sqlPermiteDeletar = $@"SELECT CB.flg_excluir_aprazamento
                                             FROM PNI_APRAZAMENTO APZ
                                             LEFT JOIN PNI_CALENDARIO_BASICO CB ON CB.ID = APZ.ID_CALENDARIO_BASICO
                                             WHERE APZ.ID = @id";
        string IAprazamentoCommand.PermiteDeletar { get => sqlPermiteDeletar; }

        public string sqlDelete = $@"DELETE FROM PNI_APRAZAMENTO APZ
                                     WHERE APZ.ID = @id";
        string IAprazamentoCommand.Delete { get => sqlDelete; }


        public string sqlGeraAprazamentoPopGeralByIndividuo = $@"EXECUTE PROCEDURE PNI_GERA_APRAZAMENTO(@id_individuo, NULL, 1)";
        string IAprazamentoCommand.GeraAprazamentoPopGeralByIndividuo { get => sqlGeraAprazamentoPopGeralByIndividuo; }

        public string sqlGeraAprazamentoFemininoByIndividuo = $@"EXECUTE PROCEDURE PNI_GERA_APRAZAMENTO(@id_individuo, NULL, 3)";
        string IAprazamentoCommand.GeraAprazamentoFemininoByIndividuo { get => sqlGeraAprazamentoFemininoByIndividuo; }

        public string sqlGeraAprazamentoMasculinoByIndividuo = $@"EXECUTE PROCEDURE PNI_GERA_APRAZAMENTO(@id_individuo, NULL, 4)";
        string IAprazamentoCommand.GeraAprazamentoMasculinoByIndividuo { get => sqlGeraAprazamentoMasculinoByIndividuo; }

        public string sqlGeraAprazamentoDeficienciaByIndividuo = $@"EXECUTE PROCEDURE PNI_GERA_APRAZAMENTO(@id_individuo, NULL, 6)";
        string IAprazamentoCommand.GeraAprazamentoDeficienciaByIndividuo { get => sqlGeraAprazamentoDeficienciaByIndividuo; }

        public string sqlGeraAprazamentoGestacaoByIndividuo = $@"EXECUTE PROCEDURE PNI_GERA_APRAZAMENTO(@id_individuo, NULL, 2)";
        string IAprazamentoCommand.GeraAprazamentoGestacaoByIndividuo { get => sqlGeraAprazamentoGestacaoByIndividuo; }

        public string sqlGeraAprazamentoPuerperaByIndividuo = $@"EXECUTE PROCEDURE PNI_GERA_APRAZAMENTO(@id_individuo, NULL, 5)";
        string IAprazamentoCommand.GeraAprazamentoPuerperaByIndividuo { get => sqlGeraAprazamentoPuerperaByIndividuo; }


        public string sqlGeraAprazamentoCalendarioBasico = $@"EXECUTE PROCEDURE PNI_GERA_APRAZAMENTO(NULL, @id_calendario_basico, @publico_alvo)";
        string IAprazamentoCommand.GeraAprazamentoCalendarioBasico { get => sqlGeraAprazamentoCalendarioBasico; }


        public string sqlGeraAprazamentoPopGeral = $@"EXECUTE PROCEDURE PNI_GERA_APRAZAMENTO(NULL, NULL, 1)";
        string IAprazamentoCommand.GeraAprazamentoPopGeral { get => sqlGeraAprazamentoPopGeral; }

        public string sqlGeraAprazamentoFeminino = $@"EXECUTE PROCEDURE PNI_GERA_APRAZAMENTO(NULL, NULL, 3)";
        string IAprazamentoCommand.GeraAprazamentoFeminino { get => sqlGeraAprazamentoFeminino; }

        public string sqlGeraAprazamentoMasculino = $@"EXECUTE PROCEDURE PNI_GERA_APRAZAMENTO(NULL, NULL, 4)";
        string IAprazamentoCommand.GeraAprazamentoMasculino { get => sqlGeraAprazamentoMasculino; }

        public string sqlGeraAprazamentoDeficiencia = $@"EXECUTE PROCEDURE PNI_GERA_APRAZAMENTO(NULL, NULL, 6)";
        string IAprazamentoCommand.GeraAprazamentoDeficiencia { get => sqlGeraAprazamentoDeficiencia; }

        public string sqlGeraAprazamentoGestacao = $@"EXECUTE PROCEDURE PNI_GERA_APRAZAMENTO(NULL, NULL, 2)";
        string IAprazamentoCommand.GeraAprazamentoGestacao { get => sqlGeraAprazamentoGestacao; }

        public string sqlGeraAprazamentoPuerpera = $@"EXECUTE PROCEDURE PNI_GERA_APRAZAMENTO(NULL, NULL, 5)";
        string IAprazamentoCommand.GeraAprazamentoPuerpera { get => sqlGeraAprazamentoPuerpera; }
    }
}
