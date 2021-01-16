using RgCidadao.Domain.Commands.Imunizacao;

namespace RgCidadao.Domain.Queries.Imunizacao
{
    public class ProdutoCommandText : IProdutoCommand
    {
        public string sqlGetAll = $@"SELECT FIRST(@pagesize) SKIP(@page) PP.ID, PP.NOME, PP.SIGLA,
                                             PU.NOME UNIDADE_MEDIDA, PC.NOME CLASSE, PP.ABREVIATURA, PP.ID_VIA_ADM
                                     FROM PNI_PRODUTO PP
                                     JOIN PNI_UNIDADE PU ON PU.ID = PP.ID_UNIDADE
                                     JOIN PNI_CLASSE PC ON PC.ID = PP.ID_CLASSE
                                     @filtro
                                     ORDER BY PP.NOME";
        string IProdutoCommand.GetAll { get => sqlGetAll; }

        public string sqlGetCountAll = $@"SELECT count(*)
                                          FROM (SELECT PP.ID, PP.NOME, PP.SIGLA,
                                                         PU.NOME UNIDADE_MEDIDA, PC.NOME CLASSE, PP.ABREVIATURA
                                                 FROM PNI_PRODUTO PP
                                                 JOIN PNI_UNIDADE PU ON PU.ID = PP.ID_UNIDADE
                                                 JOIN PNI_CLASSE PC ON PC.ID = PP.ID_CLASSE
                                                 @filtro)";
        string IProdutoCommand.GetCountAll { get => sqlGetCountAll; }

        public string sqlImunobiologico = $@"SELECT * FROM PNI_PRODUTO";
        string IProdutoCommand.GetImunobiologico { get => sqlImunobiologico; }

        public string sqlImunobiologicoEstoqueUnidade = $@"SELECT DISTINCT P.*
                                                           FROM PNI_PRODUTO P
                                                           JOIN PNI_ESTOQUE_PRODUTO EP ON P.ID = EP.ID_PRODUTO
                                                           WHERE EP.ID_UNIDADE = @id_unidade
                                                           AND EP.QTDE > 0
                                                           ORDER BY P.NOME";
        string IProdutoCommand.GetImunobiologicoEstoqueByUnidade { get => sqlImunobiologicoEstoqueUnidade; }

        public string sqlEstoqueImunobiologicoByParams = $@"SELECT EP.*, PP.NOME NOME_PRODUTOR, LP.VALIDADE
                                                            FROM PNI_ESTOQUE_PRODUTO EP
                                                            LEFT JOIN PNI_PRODUTOR PP ON PP.ID = EP.ID_PRODUTOR
                                                            LEFT JOIN PNI_LOTE_PRODUTO LP ON LP.LOTE = EP.LOTE
                                                            WHERE EP.LOTE = @lote AND
                                                                  ep.id_produto = @id_produto AND
                                                                  EP.id_unidade = @id_unidade AND
                                                                  EP.id_produtor = @id_produtor";
        string IProdutoCommand.GetEstoqueImunobiologicoByParams { get => sqlEstoqueImunobiologicoByParams; }

        public string sqlGetProdutoByEstrategia = $@"SELECT DISTINCT P.ID, P.NOME, P.ABREVIATURA, P.SIGLA
                                                     FROM PNI_PRODUTO P
                                                     JOIN PNI_IMUNOBIOLOGICO IMB ON (P.ID_IMUNOBIOLOGICO = IMB.ID)
                                                     JOIN PNI_REGRA_VACINAL RV ON (IMB.ID = RV.ID_IMUNOBIOLOGICO)
                                                     JOIN PNI_ESTRATEGIA E ON (RV.ID_ESTRATEGIA = E.ID)
                                                     JOIN PNI_DOSE D ON (RV.ID_DOSE = D.ID)
                                                     WHERE E.ID = @id_estrategia";
        string IProdutoCommand.GetProdutoByEstrategia { get => sqlGetProdutoByEstrategia; }

        public string sqlGetNewId = $@"SELECT GEN_ID(GEN_PNI_PRODUTO, 1) AS VLR FROM RDB$DATABASE";
        string IProdutoCommand.GetNewId { get => sqlGetNewId; }

        public string sqlInsert = $@"INSERT INTO PNI_PRODUTO (ID, NOME, ABREVIATURA, SIGLA, ID_UNIDADE, ID_CLASSE, ID_IMUNOBIOLOGICO, ID_VIA_ADM)
                                     VALUES (@id, @nome, @abreviatura, @sigla, @id_unidade, @id_classe, @id_imunobiologico, @id_via_adm)";
        string IProdutoCommand.Insert { get => sqlInsert; }

        public string sqlUpdate = $@"UPDATE PNI_PRODUTO
                                     SET NOME = @nome,
                                         ABREVIATURA = @abreviatura,
                                         SIGLA = @sigla,
                                         ID_UNIDADE = @id_unidade,
                                         ID_CLASSE = @id_classe,
                                         ID_IMUNOBIOLOGICO = @id_imunobiologico,
                                         ID_VIA_ADM = @id_via_adm
                                     WHERE ID = @id";
        string IProdutoCommand.Update { get => sqlUpdate; }

        public string sqlGetProdutoById = $@"SELECT *
                                             FROM PNI_PRODUTO
                                             WHERE ID = @id";
        string IProdutoCommand.GetProdutoById { get => sqlGetProdutoById; }

        public string sqlDelete = $@"DELETE FROM PNI_PRODUTO
                                     WHERE ID = @id";
        string IProdutoCommand.Delete { get => sqlDelete; }
    }
}
