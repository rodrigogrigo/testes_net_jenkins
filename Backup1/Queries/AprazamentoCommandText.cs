using Imunizacao.Domain.Commands;

namespace Imunizacao.Domain.Queries
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

        public string sqlGeraAprazamento = $@"EXECUTE PROCEDURE PNI_GERA_APRAZAMENTO(NULL, NULL, @publico_alvo, @id_individuo)";
        string IAprazamentoCommand.GeraAprazamento { get => sqlGeraAprazamento; }
    }
}
