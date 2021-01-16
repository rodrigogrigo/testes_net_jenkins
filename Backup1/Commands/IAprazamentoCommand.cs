namespace Imunizacao.Domain.Commands
{
    public interface IAprazamentoCommand
    {
        string GetAprazamentoByCidadao { get; }
        string GetNewId { get; }
        string GetAprazamentoByCalendarioBasico { get; }
        string GeraAprazamento { get; }
        string Insert { get; }
        string UpdateAprazamentoVacinados { get; }
        string PermiteDeletar { get; }
        string Delete { get; }
    }
}
