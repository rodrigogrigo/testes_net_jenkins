using RgCidadao.Domain.ViewModels.Indicadores;
using System.Collections.Generic;

namespace RgCidadao.Domain.Repositories.Indicadores
{
    public interface IIndicador4Repository
    {
        List<Indicador4ViewModel> Indicador4(string ibge, string sqlSelect, string sqlFiltros, string sqlAgrupamento, string quadrimestre, int ano);
        List<PubliAlvoIndicador4ViewModel> PublicoAlvo(string ibge, string sqlSelect, string sqlFiltros, string quadrimestre, int ano);
        int? CountPublicoAlvo(string ibge, string sqlFiltros, string quadrimestre, int ano);
        List<AtendimentoIndicador4ViewModel> Atendimentos(string ibge, int id_individuo, string quadrimestre, int ano);
    }
}
