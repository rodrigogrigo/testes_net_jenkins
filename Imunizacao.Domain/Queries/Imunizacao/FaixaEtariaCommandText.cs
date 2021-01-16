using RgCidadao.Domain.Commands.Imunizacao;

namespace RgCidadao.Domain.Queries.Imunizacao
{
    public class FaixaEtariaCommandText : IFaixaEtariaCommand
    {
        public string sqlGetAll = $@"SELECT * FROM PNI_FAIXA_ETARIA";
        string IFaixaEtariaCommand.GetAll { get => sqlGetAll; }
    }
}
