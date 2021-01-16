using RgCidadao.Domain.ViewModels.Endemias;
using System.Collections.Generic;

namespace RgCidadao.Domain.Repositories.Endemias
{
    public interface IReportEndemiasRepository
    {
        List<InfestacaoPredialViewModel> GetInfestacaoPredialReport(string ibge, string filtro);
        List<AntivetorialAnaliticoViewModel> GetAntivetorialAnatico(string ibge, string filtro);
        AntivetorialTotalizadorViewModel GetAntivetorialTotalizador(string ibge, string filtro1, string filtro2);
        AntivetorialCampoTotaisViewModel GetAntivetorialTrabalhoCampoTotais(string ibge,string filtro);
        List<AntivetorialResumoLabViewModel> GetAntivetorialResumoLab(string ibge, string filtro, string filtro2);
        List<AntivetorialInfectadosViewModel> GetAntivetorialInfectados(string ibge, string filtro);
    }
}
