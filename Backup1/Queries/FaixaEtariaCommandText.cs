using Imunizacao.Domain.Commands;

namespace Imunizacao.Domain.Queries
{
    public class FaixaEtariaCommandText : IFaixaEtariaCommand
    {
        public string sqlGetAll = $@"SELECT * FROM PNI_FAIXA_ETARIA";
        string IFaixaEtariaCommand.GetAll { get => sqlGetAll; }
    }
}
