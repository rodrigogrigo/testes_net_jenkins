using Imunizacao.Domain.Commands;

namespace Imunizacao.Domain.Queries
{
    public class UnidadeCommandText : IUnidadeCommand
    {
        public string sqlGetAll = $@"SELECT CSI_CODUNI ID, CSI_NOMUNI UNIDADE
                                     FROM TSI_UNIDADE
                                     @filtro
                                     ORDER BY CSI_NOMUNI";
        string IUnidadeCommand.GetAll { get => sqlGetAll; }

        public string sqlGetUnidadeByUser = $@"SELECT UN.CSI_CODUNI ID, UN.CSI_NOMUNI UNIDADE, UN.CSI_CNES CNES,
                                               UN.CSI_ENDUNI ENDERECO, UN.CSI_BAIUNI BAIRRO, UN.FLG_UNIDADE_PA UNIDADE_PA, UU.CSI_ATIVO ATIVO
                                               FROM TSI_UNIDADE UN 
                                               INNER JOIN TSI_USERUNIDADE UU ON (UU.CSI_CODUNI = UN.CSI_CODUNI)
                                               WHERE ((UN.EXCLUIDO = 'F') OR (UN.EXCLUIDO IS NULL))
                                               AND UU.CSI_IDUSER = @user
                                               ORDER BY UN.CSI_NOMUNI";

        string IUnidadeCommand.GetUnidadesByUser { get => sqlGetUnidadeByUser; }
    }
}
