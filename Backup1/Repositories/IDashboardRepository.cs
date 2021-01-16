using Imunizacao.Domain.Entities;
using Imunizacao.Domain.ViewModels;
using System.Collections.Generic;

namespace Imunizacao.Domain.Repositories
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
