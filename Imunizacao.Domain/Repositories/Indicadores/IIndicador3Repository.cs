using RgCidadao.Domain.ViewModels.Indicadores;
using System.Collections.Generic;

namespace RgCidadao.Domain.Repositories.Indicadores
{
    public interface IIndicador3Repository
    {
        List<Indicador3ViewModel> Indicador3(string ibge, string sqlSelect, string sqlFiltros, string sqlAgrupamento);
        List<PubliAlvoIndicador3ViewModel> PublicoAlvo(string ibge, string sqlSelect, string sqlFiltros, string filtro_valido);
        int? CountPublicoAlvo(string ibge, string sqlFiltros, string filtro_valido);
        List<AtendimentoIndicador3ViewModel> Atendimentos(string ibge, int id_individuo, string dum, string gi_dt_nascimento);
    }
}
