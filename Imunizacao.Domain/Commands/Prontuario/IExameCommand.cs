namespace RgCidadao.Domain.Commands.Prontuario
{
    public interface IExameCommand
    {
        string GetExamesComuns { get; }
        string GetExamesAltoCustos { get; }
        string GetHistoricoSolicitacoesExameByPaciente {get;}
        string GetHistoricoResultadoExameByPaciente { get; } 
        string GetListAgrupamentosExames { get; }
        string GetCid { get; }
    }
}
