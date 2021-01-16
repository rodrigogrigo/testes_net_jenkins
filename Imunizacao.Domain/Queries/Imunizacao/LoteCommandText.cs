using RgCidadao.Domain.Commands.Imunizacao;

namespace RgCidadao.Domain.Queries.Imunizacao
{
    public class LoteCommandText : ILoteCommand
    {
        public string sqlLoteByImunobiologico = $@"SELECT DISTINCT PLP.ID, PLP.LOTE, PLP.VALIDADE, PLP.ID_PRODUTOR, PP.NOME NOME_PRODUTOR,
                                                         PA.DESCRICAO APRESENTACAO, PA.QUANTIDADE, PLP.ID_APRESENTACAO, (SELECT COUNT(*) FROM PNI_LOTE_UNIDADE_BLOQUEADO LUB
                                                                                                                         WHERE LUB.ID_LOTE = PLP.ID AND LUB.ID_UNIDADE = @id_unidade) FLG_BLOQUEADO
                                                   FROM PNI_LOTE_PRODUTO PLP
                                                   LEFT JOIN PNI_ESTOQUE_PRODUTO EP ON PLP.LOTE = EP.LOTE
                                                   JOIN PNI_PRODUTOR PP ON PP.ID = PLP.ID_PRODUTOR
                                                   JOIN PNI_APRESENTACAO PA ON PA.ID = PLP.ID_APRESENTACAO
                                                   WHERE PLP.ID_PRODUTO =@produto";
        string ILoteCommand.GetLoteByImunobiologico { get => sqlLoteByImunobiologico; }

        public string sqlGetLoteEstoqueByImunobiologico = $@"SELECT
                                                                 LP.*, P.NOME NOME_PRODUTOR, PA.DESCRICAO APRESENTACAO, PA.QUANTIDADE, (SELECT COUNT(*) FROM PNI_LOTE_UNIDADE_BLOQUEADO LUB
                                                                                                                                        WHERE LUB.ID_LOTE = LP.ID AND LUB.ID_UNIDADE = @id_unidade) FLG_BLOQUEADO
                                                             FROM PNI_LOTE_PRODUTO LP
                                                             JOIN PNI_ESTOQUE_PRODUTO EP ON (LP.LOTE = EP.LOTE AND LP.ID_PRODUTO = EP.ID_PRODUTO AND LP.ID_PRODUTOR = EP.ID_PRODUTOR)
                                                             JOIN PNI_PRODUTOR P ON LP.ID_PRODUTOR = P.ID
                                                             JOIN PNI_APRESENTACAO PA ON PA.ID = LP.ID_APRESENTACAO
                                                             WHERE @filtro";

        string ILoteCommand.GetLoteEstoqueByImunobiologico { get => sqlGetLoteEstoqueByImunobiologico; }

        public string sqlGetLoteByUnidade = $@"SELECT
                LP.ID, LP.LOTE,
                CAST(EP.QTDE AS INTEGER) QTDE_DOSES,
                CAST(CEIL(EP.QTDE / A.QUANTIDADE) AS INTEGER) QTDE_FRASCOS,
                PP.NOME NOME_PRODUTOR, LP.VALIDADE,
                (SELECT COUNT(*) FROM PNI_LOTE_UNIDADE_BLOQUEADO LUB
                WHERE LUB.ID_LOTE = LP.ID AND LUB.ID_UNIDADE = @id_unidade1) FLG_BLOQUEADO
        FROM PNI_LOTE_PRODUTO LP
        JOIN PNI_APRESENTACAO A ON (LP.ID_APRESENTACAO = A.ID)
        JOIN PNI_ESTOQUE_PRODUTO EP ON (LP.LOTE = EP.LOTE AND LP.ID_PRODUTO = EP.ID_PRODUTO AND LP.ID_PRODUTOR = EP.ID_PRODUTOR)
        JOIN PNI_PRODUTOR PP ON PP.ID = EP.ID_PRODUTOR
        WHERE LP.ID_PRODUTO = @id_produto AND
                EP.ID_UNIDADE = @id_unidade";

        string ILoteCommand.GetLoteByUnidade { get => sqlGetLoteByUnidade; }

        public string sqlGetNewId = $@"SELECT GEN_ID(GEN_PNI_LOTE_PRODUTO, 1) AS VLR FROM RDB$DATABASE";
        string ILoteCommand.GetNewId { get => sqlGetNewId; }

        public string sqlInsert = $@"INSERT INTO PNI_LOTE_PRODUTO (ID,LOTE,ID_PRODUTO,ID_PRODUTOR,INATIVO,VALIDADE,ID_APRESENTACAO)
                                     VALUES(@id, @lote, @id_produto, @id_produtor, @inativo, @validade, @id_apresentacao)";
        string ILoteCommand.Insert { get => sqlInsert; }

        public string sqlUpdate = $@"UPDATE PNI_LOTE_PRODUTO
                                     SET LOTE = @lote,
                                         ID_PRODUTO = @id_produto,
                                         ID_PRODUTOR = @id_produtor,
                                         INATIVO = @inativo,
                                         VALIDADE = @validade,
                                         ID_APRESENTACAO = @id_apresentacao
                                     WHERE ID = @id";
        string ILoteCommand.Editar { get => sqlUpdate; }

        //public string sqlAtualizaSituacao = $@"UPDATE PNI_LOTE_PRODUTO SET FLG_BLOQUEADO = @flg_bloqueado
        //                                       WHERE ID = @id";
        //string ILoteCommand.AtualizaSituacaoLote { get => sqlAtualizaSituacao; }

        public string sqlGetPrimeiroMovLote = $@"SELECT CAST(MIN(MP.DATA) AS DATE) DATA
                                                 FROM PNI_MOVIMENTO_PRODUTO MP
                                                 WHERE MP.ID_PRODUTO = @id_produto AND
                                                       MP.ID_UNIDADE = @id_unidade AND
                                                       MP.LOTE = @lote";
        string ILoteCommand.GetPrimeiroMovimentoLote { get => sqlGetPrimeiroMovLote; }

        public string sqlGetLoteById = $@"SELECT PLP.*, PA.DESCRICAO, PA.QUANTIDADE
                                          FROM PNI_LOTE_PRODUTO PLP
                                          JOIN PNI_APRESENTACAO PA ON PA.ID = PLP.ID_APRESENTACAO
                                          WHERE PLP.ID = @id";
        string ILoteCommand.GetLoteById { get => sqlGetLoteById; }

        public string sqlGetLoteByLote = $@"SELECT * from PNI_LOTE_PRODUTO
                                            WHERE LOTE = @lote AND
                                                  ID_PRODUTO = @produto AND
                                                  ID_PRODUTOR = @produtor";
        string ILoteCommand.GetLoteByLote { get => sqlGetLoteByLote; }

        public string sqlGetLoteByProdutor = $@"SELECT *
                                                FROM PNI_LOTE_PRODUTO
                                                WHERE ID_PRODUTOR = @id_produtor";
        string ILoteCommand.GetLoteByProdutor { get => sqlGetLoteByProdutor; }

        public string sqlAdicionaBloqueioUnidadeLote = $@"INSERT INTO PNI_LOTE_UNIDADE_BLOQUEADO (ID_UNIDADE, ID_LOTE)
                                                          VALUES(@id_unidade, @id_lote)";
        string ILoteCommand.AdicionaBloqueioUnidadeLote { get => sqlAdicionaBloqueioUnidadeLote; }

        public string sqlRemoveBloqueioUnidadeLote = $@"DELETE FROM PNI_LOTE_UNIDADE_BLOQUEADO
                                                        WHERE ID_UNIDADE = @id_unidade AND
                                                              ID_LOTE = @id_lote";
        string ILoteCommand.RemoveBloqueioUnidadeLote { get => sqlRemoveBloqueioUnidadeLote; }
    }
}
