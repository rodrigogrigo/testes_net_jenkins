using RgCidadao.Domain.Entities.Imunizacao;
using RgCidadao.Domain.ViewModels.Imunizacao;
using System.Collections.Generic;

namespace RgCidadao.Domain.Repositories.Imunizacao
{
    public interface IDashboardRepository
    {
        DashboardViewModel TotalVacinasDia(string ibge, int unidade);
        DashboardViewModel TotalVacinaVencida(string ibge);
        List<DashboardViewModel> TotalImunizadasMes(string ibge);
        List<DashboardViewModel> GetVacinas(string ibge, int unidade);
        DashboardViewModel GetPercentualPolioPenta(string ibge, int unidade);
    }
}
