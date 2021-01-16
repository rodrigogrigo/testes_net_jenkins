using Imunizacao.Domain.Commands;

namespace Imunizacao.Domain.Queries
{
    public class EntradaProdutoItemCommandText : IEntradaProdutoItemCommand
    {
        public string sqlGetEntradaItemById = $@"SELECT EPI.ID, EPI.ID_ENTRADA_PRODUTO, EPI.ID_UNIDADE, EPI.VALIDADE, EPI.ID_APRESENTACAO, EPI.QTDE_FRASCOS, EPI.VALOR,
                                                      EPI.ID_LOTE, PLP.LOTE, PP.ID ID_PRODUTO, PP.NOME NOME_PRODUTO, PP.ABREVIATURA, PP.SIGLA, EPI.QTDE_DOSES
                                                 FROM PNI_ENTRADA_PRODUTO_ITEM EPI
                                                 JOIN PNI_LOTE_PRODUTO PLP ON PLP.ID = EPI.ID_LOTE
                                                 JOIN PNI_PRODUTO PP ON PP.ID = PLP.ID_PRODUTO
                                                 WHERE EPI.ID = @id";
        string IEntradaProdutoItemCommand.GetEntradaItemById { get => sqlGetEntradaItemById; }

        public string sqlGetNewId = $@"SELECT GEN_ID(GEN_PNI_ENTRADA_PRODUTO_ITEM, 1) AS VLR FROM RDB$DATABASE";
        string IEntradaProdutoItemCommand.GetNewId { get => sqlGetNewId; }

        public string sqlInsertOrUpdate = $@"UPDATE OR INSERT INTO PNI_ENTRADA_PRODUTO_ITEM (ID, ID_ENTRADA_PRODUTO, ID_UNIDADE, VALIDADE, ID_APRESENTACAO,
                                                                                             QTDE_FRASCOS, VALOR, ID_LOTE, QTDE_DOSES)
                                             VALUES (@id, @id_entrada_produto, @id_unidade, @validade, @id_apresentacao, @qtde_frascos, @valor, @id_lote, @qtde_doses)";
        string IEntradaProdutoItemCommand.InsertOrUpdate { get => sqlInsertOrUpdate; }

        public string sqlDelete = $@"DELETE FROM PNI_ENTRADA_PRODUTO_ITEM
                                     WHERE ID = @id";
        string IEntradaProdutoItemCommand.Delete { get => sqlDelete; }

        public string sqlGetAllItensByPai = $@"SELECT EPI.ID, EPI.ID_ENTRADA_PRODUTO, EPI.ID_UNIDADE, EPI.VALIDADE, EPI.ID_APRESENTACAO,
                                                      EPI.QTDE_FRASCOS, EPI.VALOR, EPI.ID_LOTE, PLP.LOTE, PP.ID ID_PRODUTO,
                                                      PP.NOME NOME_PRODUTO, PP.ABREVIATURA, PP.SIGLA, PA.DESCRICAO FORMA_APRESENTACAO,
                                                      COALESCE(EPI.QTDE_DOSES, 0) QTDE_DOSES
                                                FROM PNI_ENTRADA_PRODUTO_ITEM EPI
                                                JOIN PNI_LOTE_PRODUTO PLP ON PLP.ID = EPI.ID_LOTE
                                                JOIN PNI_PRODUTO PP ON PP.ID = PLP.ID_PRODUTO
                                                JOIN PNI_APRESENTACAO PA ON PA.id = EPI.ID_APRESENTACAO
                                                WHERE ID_ENTRADA_PRODUTO = @id";
        string IEntradaProdutoItemCommand.GetAllItensByPai { get => sqlGetAllItensByPai; }

        public string sqlDeleteByPai = $@"DELETE FROM PNI_ENTRADA_PRODUTO_ITEM
                                          WHERE ID_ENTRADA_PRODUTO = @id";
        string IEntradaProdutoItemCommand.DeleteAllByPai { get => sqlDeleteByPai; }

        public string sqlPossuiMovimentoByEstradaItem = $@"SELECT *
                                                           FROM PNI_ENTRADA_PRODUTO_ITEM SPI
                                                           JOIN PNI_LOTE_PRODUTO L ON L.ID = SPI.ID_LOTE
                                                           JOIN PNI_ACERTO_ESTOQUE AE ON AE.LOTE = L.LOTE
                                                           WHERE SPI.ID = @id";
        string IEntradaProdutoItemCommand.PossuiMovimentoByEstradaItem { get => sqlPossuiMovimentoByEstradaItem; }
    }
}
