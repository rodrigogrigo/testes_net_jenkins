using Imunizacao.Domain.Commands;

namespace Imunizacao.Domain.Queries
{
    public class GrupoAtendimentoCommandText : IGrupoAtendimentoCommand
    {
        public string sqlGetAll = $@"SELECT ID, DESCRICAO
                                     FROM PNI_GRUPO_ATENDIMENTO
                                     ORDER BY DESCRICAO";

        string IGrupoAtendimentoCommand.GetAll { get => sqlGetAll; }
    }
}
