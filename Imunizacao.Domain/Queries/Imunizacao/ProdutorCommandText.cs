using RgCidadao.Domain.Commands.Imunizacao;

namespace RgCidadao.Domain.Queries.Imunizacao
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

        public string sqlGetById = $@"SELECT *
                                      FROM PNI_PRODUTOR
                                      WHERE ID = @id";
        string IProdutorCommand.GetById { get => sqlGetById; }

        public string sqlUpdate = $@"UPDATE PNI_PRODUTOR
                                     SET NOME = @nome,
                                         ABREVIATURA = @abreviatura
                                     WHERE ID = @id";
        string IProdutorCommand.Update { get => sqlUpdate; }

        public string sqlGetCountAll = $@"SELECT COUNT(*)
                                          FROM PNI_PRODUTOR
                                          @filtro";
        string IProdutorCommand.GetCountAll { get => sqlGetCountAll; }

        public string sqlGetAllPagination = $@"SELECT FIRST(@pagesize) SKIP(@page) *
                                               FROM PNI_PRODUTOR
                                               @filtro
                                              ORDER BY NOME";
        string IProdutorCommand.GetAllPagination { get => sqlGetAllPagination; }

        public string sqlExcluir = $@"DELETE FROM PNI_PRODUTOR
                                      WHERE ID = @id";
        string IProdutorCommand.Delete { get => sqlExcluir; }
    }
}
