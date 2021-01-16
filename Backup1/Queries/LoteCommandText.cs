using Imunizacao.Domain.Commands;

namespace Imunizacao.Domain.Queries
{
    public class LoteCommandText : ILoteCommand
    {
        public string sqlLoteByImunobiologico = $@"SELECT PLP.ID, PLP.LOTE, PLP.VALIDADE, PLP.ID_PRODUTOR, PP.NOME NOME_PRODUTOR,
                                                         PA.DESCRICAO APRESENTACAO, PA.QUANTIDADE, PLP.ID_APRESENTACAO, PLP.FLG_BLOQUEADO
                                                   FROM PNI_LOTE_PRODUTO PLP
                                                   LEFT JOIN PNI_ESTOQUE_PRODUTO EP ON PLP.LOTE = EP.LOTE
                                                   JOIN PNI_PRODUTOR PP ON PP.ID = PLP.ID_PRODUTOR
                                                   JOIN PNI_APRESENTACAO PA ON PA.ID = PLP.ID_APRESENTACAO
                                                   WHERE PLP.ID_PRODUTO =@produto AND
                                                        PLP.INATIVO != 'T' AND
                                                        COALESCE(PLP.FLG_BLOQUEADO, 0) = 0 --AND
                                                        --EP.QTDE > 0 ";
        string ILoteCommand.GetLoteByImunobiologico { get => sqlLoteByImunobiologico; }

        public string sqlGetLoteEstoqueByImunobiologico = $@"SELECT LP.*, P.NOME NOME_PRODUTOR, PA.DESCRICAO APRESENTACAO, PA.QUANTIDADE, LP.FLG_BLOQUEADO
                                                             FROM PNI_LOTE_PRODUTO LP
                                                             JOIN PNI_ESTOQUE_PRODUTO EP ON LP.LOTE = EP.LOTE
                                                             JOIN PNI_PRODUTOR P ON LP.ID_PRODUTOR = P.ID
                                                             JOIN PNI_APRESENTACAO PA ON PA.ID = LP.ID_APRESENTACAO
                                                             WHERE @filtro";

        string ILoteCommand.GetLoteEstoqueByImunobiologico { get => sqlGetLoteEstoqueByImunobiologico; }

        public string sqlGetLoteByUnidade = $@"SELECT LP.ID, LP.LOTE, EP.QTDE QUANTIDADE_LOTE, LP.FLG_BLOQUEADO, PP.NOME NOME_PRODUTOR, LP.VALIDADE
                                               FROM PNI_LOTE_PRODUTO LP
                                               JOIN PNI_ESTOQUE_PRODUTO EP ON LP.LOTE = EP.LOTE
                                               JOIN PNI_PRODUTOR PP ON PP.ID = EP.ID_PRODUTOR
                                               WHERE LP.ID_PRODUTO = @id_produto AND
                                                     EP.ID_UNIDADE = @id_unidade";
        string ILoteCommand.GetLoteByUnidade { get => sqlGetLoteByUnidade; }

        public string sqlGetNewId = $@"SELECT GEN_ID(GEN_PNI_LOTE_PRODUTO, 1) AS VLR FROM RDB$DATABASE";
        string ILoteCommand.GetNewId { get => sqlGetNewId; }

        public string sqlInsert = $@"INSERT INTO PNI_LOTE_PRODUTO (ID,LOTE,ID_PRODUTO,ID_PRODUTOR,INATIVO,VALIDADE,ID_APRESENTACAO)
                                     VALUES(@id, @lote, @id_produto, @id_produtor, @inativo, @validade, @id_apresentacao)";
        string ILoteCommand.Insert { get => sqlInsert; }

        public string sqlAtualizaSituacao = $@"UPDATE PNI_LOTE_PRODUTO SET FLG_BLOQUEADO = @flg_bloqueado
                                               WHERE ID = @id";
        string ILoteCommand.AtualizaSituacaoLote { get => sqlAtualizaSituacao; }
    }
}
