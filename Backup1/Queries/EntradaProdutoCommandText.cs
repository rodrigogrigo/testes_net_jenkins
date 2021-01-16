using Imunizacao.Domain.Commands;

namespace Imunizacao.Domain.Queries
{
    public class EntradaProdutoCommandText : IEntradaProdutoCommand
    {
        public string sqlEntradaById = $@"SELECT ID, ID_UNIDADE, NUMERO_NOTA, DATA, USUARIO, OBS, VALOR_TOTAL, ID_FORNECEDOR
                                          FROM PNI_ENTRADA_PRODUTO
                                          WHERE ID = @id";
        string IEntradaProdutoCommand.GetEntradaById { get => sqlEntradaById; }

        public string sqlNewId = $@"SELECT GEN_ID(GEN_PNI_ENTRADA_PRODUTO, 1) AS VLR FROM RDB$DATABASE";
        string IEntradaProdutoCommand.GetNewId { get => sqlNewId; }

        public string sqlInsertOrUpdate = $@"UPDATE OR INSERT INTO PNI_ENTRADA_PRODUTO (ID, ID_UNIDADE, NUMERO_NOTA, DATA,
                                                                                    USUARIO, OBS, VALOR_TOTAL, ID_FORNECEDOR)
                                             VALUES (@id, @id_unidade, @numero_nota, @data, @usuario, @obs, @valor_total, @id_fornecedor)";
        string IEntradaProdutoCommand.InsertOrUpdate { get => sqlInsertOrUpdate; }

        public string sqlDelete = $@"DELETE FROM PNI_ENTRADA_PRODUTO
                                     WHERE ID = @id";
        string IEntradaProdutoCommand.Delete { get => sqlDelete; }

        public string sqlAtualizaValor = $@"UPDATE PNI_ENTRADA_PRODUTO
                                            SET VALOR_TOTAL = @valor
                                            WHERE ID = @id";
        string IEntradaProdutoCommand.AtualizaValor { get => sqlAtualizaValor; }

        public string SqlGetEntradaVacinaApresentacao = $@"SELECT FIRST(@pagesize) SKIP(@page) E.ID, E.DATA, F.CSI_NOMFOR FORNECEDOR,
                                                                  E.NUMERO_NOTA NUMERO_NOTA, E.VALOR_TOTAL VALOR, E.USUARIO
                                                           FROM PNI_ENTRADA_PRODUTO E
                                                           INNER JOIN TSI_CADFOR F ON (F.CSI_CODFOR = E.ID_FORNECEDOR)
                                                           WHERE E.ID_UNIDADE = @unidade
                                                                 @filtro
                                                           ORDER BY E.ID DESC";
        string IEntradaProdutoCommand.GetEntradaVacinaByUnidade { get => SqlGetEntradaVacinaApresentacao; }

        public string SqlGetCountEntradaVacina = $@"SELECT count(*) total
                                                           FROM PNI_ENTRADA_PRODUTO E
                                                           INNER JOIN TSI_CADFOR F ON (F.CSI_CODFOR = E.ID_FORNECEDOR)
                                                           WHERE E.ID_UNIDADE = @unidade
                                                                 @filtro";
        string IEntradaProdutoCommand.GetCountEntradaVacina { get => SqlGetCountEntradaVacina; }

     
    }
}
