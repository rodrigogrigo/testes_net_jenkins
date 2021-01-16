using RgCidadao.Domain.Commands.Cadastro;

namespace RgCidadao.Domain.Queries.Cadastro
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
                                               ORDER BY CSI_NOMCID";
        string ICidadeCommand.GetAllPagination { get => sqlGetAllPagination; }

        public string sqlGetById = $@"SELECT CSI_CODCID CODIGO,
                                             (CSI_NOMCID || ' - ' || CSI_SIGEST) NOME,
                                             CSI_CODEST COD_ESTADO
                                      FROM TSI_CIDADE
                                      WHERE CSI_CODCID = @id ";
        string ICidadeCommand.GetCidadeById { get => sqlGetById; }

        public string sqlGetByIBGE = $@"SELECT CSI_CODCID CODIGO,
                                             (CSI_NOMCID || ' - ' || CSI_SIGEST) NOME,
                                             CSI_CODEST COD_ESTADO
                                      FROM TSI_CIDADE CID
                                      WHERE CID.CSI_CODIBGE LIKE @ibge ";
        string ICidadeCommand.GetCidadeByIBGE { get => sqlGetByIBGE; }

    }
}
