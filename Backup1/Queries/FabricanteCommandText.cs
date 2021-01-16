using Imunizacao.Domain.Commands;

namespace Imunizacao.Domain.Queries
{
    public class FabricanteCommandText : IFabricanteCommand
    {
        public string sqlGetAll = $@"SELECT ID, NOME
                                     FROM PNI_PRODUTOR
                                     @filtro
                                     ORDER BY NOME";

        string IFabricanteCommand.GetAll { get => sqlGetAll; }

        public string sqlGetNewId = $@"SELECT MAX(ID) + 1
                                       FROM PNI_PRODUTOR";

        string IFabricanteCommand.GetNewId { get => sqlGetNewId; }

        public string sqlInserir = $@"INSERT INTO PNI_PRODUTOR (ID, NOME, ABREVIATURA)
                                      VALUES (@id, @nome, @abreviatura)";

        string IFabricanteCommand.Inserir { get => sqlInserir; }

        public string SqlAtualizar = $@"UPDATE PNI_PRODUTOR SET NOME = @nome, ABREVIATURA = @abreviatura
                                        WHERE ID = @id";
        string IFabricanteCommand.Atualizar { get => SqlAtualizar; }

        public string SqlDeletar = $@"DELETE FROM PNI_PRODUTOR
                                      WHERE ID = @id";
        string IFabricanteCommand.Deletar { get => SqlDeletar; }

        public string sqlGetProdutorByImuno = $@"SELECT DISTINCT PP.*
                                                 FROM PNI_PRODUTOR PP
                                                 JOIN PNI_LOTE_PRODUTO PLP ON PLP.ID_PRODUTOR = PP.ID
                                                 WHERE PLP.ID_PRODUTO = @produto
                                                 ORDER BY PP.NOME";
        string IFabricanteCommand.GetProdutorByImunobiologico { get => sqlGetProdutorByImuno; }
    }
}
