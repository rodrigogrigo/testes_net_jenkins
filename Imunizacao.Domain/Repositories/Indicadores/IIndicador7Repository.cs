using RgCidadao.Domain.ViewModels.Indicadores;
using System.Collections.Generic;

namespace RgCidadao.Domain.Repositories.Indicadores
{
    public interface IIndicador7Repository
    {
        List<Indicador7ViewModel> Indicador7(string ibge, string sqlSelect, string sqlFiltros, string sqlAgrupamento, string quadrimestre, int ano);
        List<PubliAlvoIndicador7ViewModel> PublicoAlvo(string ibge, string sqlSelect, string sqlFiltros, string quadrimestre, int ano);
        int? CountPublicoAlvo(string ibge, string sqlFiltros, string quadrimestre);
        List<AtendimentoIndicador7ViewModel> Atendimentos(string ibge, int id_individuo, string quadrimestre, int ano);
    }
}
