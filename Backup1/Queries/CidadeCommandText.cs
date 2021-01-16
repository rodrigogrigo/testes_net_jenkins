using Imunizacao.Domain.Commands;

namespace Imunizacao.Domain.Queries
{
    public class CidadeCommandText : ICidadeCommand
    {
        public string sqlGetAll = $@"SELECT CSI_CODCID CODIGO,
                                            (CSI_NOMCID || ' - ' || CSI_SIGEST) NOME
                                     FROM TSI_CIDADE
                                     WHERE EXCLUIDO <> 'F'
                                     ORDER BY NOME";
        string ICidadeCommand.GetAll { get => sqlGetAll; }

        public string sqlGetCountAll = $@"SELECT CSI_CODCID CODIGO,
                                                 (CSI_NOMCID || ' - ' || CSI_SIGEST) NOME
                                          FROM TSI_CIDADE
                                          @filtro";
        string ICidadeCommand.GetCountAll { get => sqlGetCountAll; }

        public string sqlGetAllPagination = $@"SELECT FIRST(@pagesize) SKIP(@page) CSI_CODCID CODIGO,
                                                      (CSI_NOMCID || ' - ' || CSI_SIGEST) NOME
                                               FROM TSI_CIDADE
                                               @filtro
                                               ORDER BY NOME";
        string ICidadeCommand.GetAllPagination{ get => sqlGetAllPagination; }

    }
}
