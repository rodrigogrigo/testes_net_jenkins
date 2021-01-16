namespace RgCidadao.Domain.Commands.Imunizacao
{
    public interface IDashboardCommand
    {
        string TotalVacinasDia { get; }
        string TotalVacinaVencida { get; }
        string TotalImunizadasMes { get; }
        string GetVacinas { get; }
        string GetPercentualPolioPenta { get; }
    }
}
