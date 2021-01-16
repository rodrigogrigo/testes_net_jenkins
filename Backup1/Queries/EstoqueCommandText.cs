using Imunizacao.Domain.Commands;

namespace Imunizacao.Domain.Queries
{
    public class EstoqueCommandText : IEstoqueCommand
    {
        public string sqlGetAllUnidadeWithEstoque = $@"SELECT DISTINCT UN.CSI_CODUNI ID, UN.CSI_NOMUNI UNIDADE, UN.CSI_CNES CNES,
                                                           (SELECT SUM(PER.QTDE)
                                                           FROM PNI_ESTOQUE_PRODUTO PER
                                                           WHERE PER.ID_UNIDADE = UN.CSI_CODUNI AND
                                                                   PER.ID_PRODUTO = @produto) AS QTDE
                                                       FROM TSI_UNIDADE UN
                                                       INNER JOIN TSI_USERUNIDADE UU ON (UU.CSI_CODUNI = UN.CSI_CODUNI)
                                                       WHERE ((UN.EXCLUIDO = 'F') OR (UN.EXCLUIDO IS NULL)) AND
                                                            UU.CSI_IDUSER = @user AND
                                                           (SELECT SUM(PER.QTDE)
                                                           FROM PNI_ESTOQUE_PRODUTO PER
                                                           WHERE PER.ID_UNIDADE = UN.CSI_CODUNI AND
                                                                 PER.ID_PRODUTO = @produto) > 0
                                                       ORDER BY UN.CSI_NOMUNI";
        string IEstoqueCommand.GetAllUnidadeWithEstoque { get => sqlGetAllUnidadeWithEstoque; }

        public string sqlGetEstoqueLoteByUnidadeAndProduto = $@"SELECT DISTINCT EP.*, PP.NOME NOME_PRODUTOR, LP.VALIDADE
                                                                FROM PNI_ESTOQUE_PRODUTO EP
                                                                JOIN PNI_PRODUTOR PP ON PP.ID = EP.ID_PRODUTOR
                                                                JOIN PNI_LOTE_PRODUTO LP ON LP.LOTE = EP.LOTE
                                                                WHERE ep.id_produto = @id_produto AND
                                                                      EP.id_unidade = @id_unidade";
        string IEstoqueCommand.GetEstoqueLoteByUnidadeAndProduto { get => sqlGetEstoqueLoteByUnidadeAndProduto; }
    }
}
