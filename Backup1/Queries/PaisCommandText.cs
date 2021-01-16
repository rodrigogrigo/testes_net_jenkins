using Imunizacao.Domain.Commands;

namespace Imunizacao.Domain.Queries
{
    public class PaisCommandText : IPaisCommand
    {
        public string SqlGetAll = $@"SELECT CSI_ID CODIGO, CSI_DESCRICAO NOME
                                     FROM TSI_NACIONALIDADE  
                                     ORDER BY NOME";
        string IPaisCommand.GetAll { get => SqlGetAll; }
    }
}
