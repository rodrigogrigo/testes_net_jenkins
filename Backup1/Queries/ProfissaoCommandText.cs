using Imunizacao.Domain.Commands;

namespace Imunizacao.Domain.Queries
{
    public class ProfissaoCommandText : IProfissaoCommand
    {
        public string sqlGetAll = $@"SELECT CSI_NOMPRO, CSI_CODPRO
                                     FROM TSI_PROFIS
                                     WHERE EXCLUIDO <> 'T'";
        string IProfissaoCommand.GetAll { get => sqlGetAll; }

        public string sqlGetCountAll = $@"SELECT COUNT(*)
                                          FROM TSI_PROFIS
                                          WHERE EXCLUIDO <> 'T'
                                          @filtro";
        string IProfissaoCommand.GetCountAll { get => sqlGetCountAll; }

        public string sqlGetAllPagination = $@"SELECT FIRST(@pagesize) SKIP(@page) CSI_NOMPRO, CSI_CODPRO
                                          FROM TSI_PROFIS
                                          WHERE EXCLUIDO <> 'T'
                                          @filtro";
        string IProfissaoCommand.GetAllPagination { get => sqlGetAllPagination; }
    }
}
