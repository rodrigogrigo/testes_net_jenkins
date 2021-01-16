using RgCidadao.Domain.Commands.Imunizacao;

namespace RgCidadao.Domain.Queries.Imunizacao
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

        public string sqlDelete = $@"DELETE FROM PNI_ENTRADA_PRODUTO_ITEM WHERE ID = @id";
        string IEntradaProdutoItemCommand.Delete { get => sqlDelete; }

        public string sqlGetAllItensByPai = $@"SELECT EPI.ID, EPI.ID_ENTRADA_PRODUTO, EPI.ID_UNIDADE, EPI.VALIDADE, EPI.ID_APRESENTACAO,
                                                      EPI.QTDE_FRASCOS, EPI.VALOR, EPI.ID_LOTE, PLP.LOTE, PP.ID ID_PRODUTO,
                                                      PP.NOME NOME_PRODUTO, PP.ABREVIATURA, PP.SIGLA, PA.DESCRICAO FORMA_APRESENTACAO,
                                                      COALESCE(EPI.QTDE_DOSES, 0) QTDE_DOSES, PA.QUANTIDADE AS QUANTIDADE_APRESENTACAO
                                                FROM PNI_ENTRADA_PRODUTO_ITEM EPI
                                                JOIN PNI_LOTE_PRODUTO PLP ON PLP.ID = EPI.ID_LOTE
                                                JOIN PNI_PRODUTO PP ON PP.ID = PLP.ID_PRODUTO
                                                JOIN PNI_APRESENTACAO PA ON PA.id = EPI.ID_APRESENTACAO
                                                WHERE ID_ENTRADA_PRODUTO = @id";
        string IEntradaProdutoItemCommand.GetAllItensByPai { get => sqlGetAllItensByPai; }

        public string sqlDeleteByPai = $@"DELETE FROM PNI_ENTRADA_PRODUTO_ITEM  WHERE ID_ENTRADA_PRODUTO = @id";
        string IEntradaProdutoItemCommand.DeleteAllByPai { get => sqlDeleteByPai; }

        public string sqlPossuiMovimentoByEstradaItem = $@"SELECT *
                                                           FROM PNI_ENTRADA_PRODUTO_ITEM SPI
                                                           JOIN PNI_LOTE_PRODUTO L ON L.ID = SPI.ID_LOTE
                                                           JOIN PNI_ACERTO_ESTOQUE AE ON AE.LOTE = L.LOTE
                                                           WHERE SPI.ID = @id";
        string IEntradaProdutoItemCommand.PossuiMovimentoByEstradaItem { get => sqlPossuiMovimentoByEstradaItem; }


        public string sqlGetEntradaProdutoItemById = $@"SELECT EPI.ID, EPI.VALIDADE, EPI.ID_UNIDADE, LP.LOTE, LP.ID_PRODUTO, LP.ID_PRODUTOR
                                                     FROM PNI_ENTRADA_PRODUTO_ITEM EPI
                                                     JOIN PNI_LOTE_PRODUTO LP ON (EPI.ID_LOTE = LP.ID)
                                                     WHERE EPI.ID = @id";
        string IEntradaProdutoItemCommand.GetEntradaProdutoItemById { get => sqlGetEntradaProdutoItemById; }


        public string sqlGetValorUltimaEntradaLote = $@"SELECT FIRST(1) EPI.VALOR
                                                        FROM PNI_ENTRADA_PRODUTO_ITEM EPI
                                                        JOIN PNI_LOTE_PRODUTO PLP ON PLP.ID = EPI.ID_LOTE
                                                        WHERE PLP.LOTE = @lote
                                                        order BY EPI.ID";
        string IEntradaProdutoItemCommand.GetValorUltimaEntradaLote { get => sqlGetValorUltimaEntradaLote; }

        public string sqlGetUltimaEntradaItemByLote = $@"SELECT FIRST 1 EPI.*
                                                         FROM PNI_ENTRADA_PRODUTO EP
                                                         JOIN PNI_ENTRADA_PRODUTO_ITEM EPI ON EP.ID = EPI.ID_ENTRADA_PRODUTO
                                                         WHERE EPI.ID_LOTE = @id_lote
                                                         ORDER BY EP.DATA DESC;";
        string IEntradaProdutoItemCommand.GetUltimaEntradaItemByLote { get => sqlGetUltimaEntradaItemByLote; }


        public string sqlGetItensEntradaProduto = $@"SELECT EPI.ID, EPI.VALIDADE, EPI.ID_UNIDADE, LP.LOTE, LP.ID_PRODUTO, LP.ID_PRODUTOR
                                                     FROM PNI_ENTRADA_PRODUTO_ITEM EPI
                                                     JOIN PNI_LOTE_PRODUTO LP ON (EPI.ID_LOTE = LP.ID)
                                                     WHERE EPI.ID_ENTRADA_PRODUTO = @id_entrada_produto";
        string IEntradaProdutoItemCommand.GetItensEntradaProdutoByIdEntradaProduto { get => sqlGetItensEntradaProduto; }



    }
}
