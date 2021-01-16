using RgCidadao.Domain.ViewModels.Indicadores;
using System.Collections.Generic;

namespace RgCidadao.Domain.Repositories.Indicadores
{
    public interface IIndicador6Repository
    {
        List<Indicador6ViewModel> Indicador6(string ibge, string sqlSelect, string sqlFiltros, string sqlAgrupamento, string quadrimestre, int ano);
        List<PubliAlvoIndicador6ViewModel> PublicoAlvo(string ibge, string sqlSelect, string sqlFiltros, string quadrimestre, int ano);
        int? CountPublicoAlvo(string ibge, string sqlFiltros, string quadrimestre, int ano);
        List<AtendimentoIndicador6ViewModel> Atendimentos(string ibge, int id_individuo, string quadrimestre, int ano);
    }
}
