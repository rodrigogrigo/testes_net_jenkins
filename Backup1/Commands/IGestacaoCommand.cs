namespace Imunizacao.Domain.Commands
{
    public interface IGestacaoCommand
    {
        string IsGestante { get; }
        string GetGestacaoByCidadao { get; }
        string GetGestacaoItensByGestacao { get; }
        string GetUltimaGestacaoItem { get; }
    }
}
