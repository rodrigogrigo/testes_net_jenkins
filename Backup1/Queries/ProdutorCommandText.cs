using Imunizacao.Domain.Commands;

namespace Imunizacao.Domain.Queries
{
    public class ProdutorCommandText : IProdutorCommand
    {
        public string sqlInsert = $@"INSERT INTO PNI_PRODUTOR (ID, NOME, ABREVIATURA)
                                     VALUES (@id, @nome, @abreviatura)";
        string IProdutorCommand.Insert { get => sqlInsert; }

        public string sqlGetNewId = $@"SELECT MAX(ID) + 1 FROM PNI_PRODUTOR";
        string IProdutorCommand.GetNewId { get => sqlGetNewId; }

        public string sqlGetAll = $@"SELECT * FROM PNI_PRODUTOR";
        string IProdutorCommand.GetAll { get => sqlGetAll; }
    }
}
