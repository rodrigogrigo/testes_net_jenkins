using RgCidadao.Domain.ViewModels.Indicadores;
using System;
using System.Collections.Generic;
namespace RgCidadao.Domain.Repositories.Indicadores
{
    public interface IIndicador5Repository
    {
        List<Indicador5ViewModel> Indicador5(string ibge, string sqlSelect, string sqlFiltros, string sqlAgrupamento, string quadrimestre, int ano);
        List<PubliAlvoIndicador5ViewModel> PublicoAlvo(string ibge, string sqlSelect, string sqlFiltros, string quadrimestre, int ano);
        int? CountPublicoAlvo(string ibge, string sqlFiltros, string quadrimestre, int ano);
        List<AtendimentoIndicador5ViewModel> Atendimentos(string ibge, int id_individuo);
    }
}
