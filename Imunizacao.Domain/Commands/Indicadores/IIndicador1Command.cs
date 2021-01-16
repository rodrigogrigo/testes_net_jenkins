namespace RgCidadao.Domain.Commands.Indicadores
{
    public interface IIndicador1Command
    {
        string Indicador1 { get; }
        string VerificaNovaEstrutura { get; }
        string publicoAlvo { get; }
        string CountPublicoAlvo { get; }
        string Atendimentos { get; }
    }
}
