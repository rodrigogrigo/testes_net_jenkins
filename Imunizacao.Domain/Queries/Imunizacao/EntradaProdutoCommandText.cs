using RgCidadao.Domain.Commands.Imunizacao;

namespace RgCidadao.Domain.Queries.Imunizacao
{
    public class EntradaProdutoCommandText : IEntradaProdutoCommand
    {
        public string sqlEntradaById = $@"SELECT ID, ID_UNIDADE, NUMERO_NOTA, DATA, USUARIO, OBS, VALOR_TOTAL, ID_FORNECEDOR, ID_ENVIO
                                          FROM PNI_ENTRADA_PRODUTO
                                          WHERE ID = @id";
        string IEntradaProdutoCommand.GetEntradaById { get => sqlEntradaById; }

        public string sqlNewId = $@"SELECT GEN_ID(GEN_PNI_ENTRADA_PRODUTO, 1) AS VLR FROM RDB$DATABASE";
        string IEntradaProdutoCommand.GetNewId { get => sqlNewId; }

        public string sqlInsertOrUpdate = $@"UPDATE OR INSERT INTO PNI_ENTRADA_PRODUTO (ID, ID_UNIDADE, NUMERO_NOTA, DATA,
                                                                                    USUARIO, OBS, VALOR_TOTAL, ID_FORNECEDOR, ID_ENVIO)
                                             VALUES (@id, @id_unidade, @numero_nota, @data, @usuario, @obs, @valor_total, @id_fornecedor, @id_envio)";
        string IEntradaProdutoCommand.InsertOrUpdate { get => sqlInsertOrUpdate; }

        public string sqlDelete = $@"DELETE FROM PNI_ENTRADA_PRODUTO  WHERE ID = @id";
        string IEntradaProdutoCommand.Delete { get => sqlDelete; }

        public string sqlAtualizaValor = $@"UPDATE PNI_ENTRADA_PRODUTO
                                            SET VALOR_TOTAL = @valor
                                            WHERE ID = @id";
        string IEntradaProdutoCommand.AtualizaValor { get => sqlAtualizaValor; }

        public string SqlGetEntradaVacinaApresentacao = $@"SELECT FIRST (@pagesize) SKIP (@page) E.ID, E.DATA, E.NUMERO_NOTA NUMERO_NOTA, E.VALOR_TOTAL VALOR, E.USUARIO,
                                                                                                    UNI.CSI_NOMUNI NOME_UNIDADE, E.ID_ENVIO,
                                                                                                    CASE
                                                                                                      WHEN E.ID_FORNECEDOR IS NOT NULL THEN F.CSI_NOMFOR
                                                                                                      ELSE UNI_ENV.CSI_NOMUNI
                                                                                                    END FORNECEDOR
                                                            FROM PNI_ENTRADA_PRODUTO E
                                                            LEFT JOIN TSI_CADFOR F ON (F.CSI_CODFOR = E.ID_FORNECEDOR)
                                                            LEFT JOIN PNI_ENVIO PE ON PE.ID = E.ID_ENVIO
                                                            JOIN TSI_UNIDADE UNI ON UNI.CSI_CODUNI = E.ID_UNIDADE
                                                            LEFT JOIN TSI_UNIDADE UNI_ENV ON UNI_ENV.CSI_CODUNI = PE.ID_UNIDADE_ORIGEM
                                                            WHERE E.ID_UNIDADE = @unidade
                                                                 @filtro
                                                            ORDER BY E.ID DESC";
        string IEntradaProdutoCommand.GetEntradaVacinaByUnidade { get => SqlGetEntradaVacinaApresentacao; }

        public string SqlGetCountEntradaVacina = $@"SELECT count(*) total FROM (SELECT E.ID, E.DATA, E.NUMERO_NOTA NUMERO_NOTA, E.VALOR_TOTAL VALOR, E.USUARIO,
                                                                                                UNI.CSI_NOMUNI NOME_UNIDADE, E.ID_ENVIO,
                                                                                                CASE
                                                                                                    WHEN E.ID_FORNECEDOR IS NOT NULL THEN F.CSI_NOMFOR
                                                                                                    ELSE UNI_ENV.CSI_NOMUNI
                                                                                                END FORNECEDOR
                                                                                FROM PNI_ENTRADA_PRODUTO E
                                                                                LEFT JOIN TSI_CADFOR F ON (F.CSI_CODFOR = E.ID_FORNECEDOR)
                                                                                LEFT JOIN PNI_ENVIO PE ON PE.ID = E.ID_ENVIO
                                                                                JOIN TSI_UNIDADE UNI ON UNI.CSI_CODUNI = E.ID_UNIDADE
                                                                                LEFT JOIN TSI_UNIDADE UNI_ENV ON UNI_ENV.CSI_CODUNI = PE.ID_UNIDADE_ORIGEM
                                                                                WHERE E.ID_UNIDADE = @unidade
                                                                                     @filtro)";
        string IEntradaProdutoCommand.GetCountEntradaVacina { get => SqlGetCountEntradaVacina; }

     
    }
}
