using RgCidadao.Domain.ViewModels.Indicadores;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Repositories.Indicadores
{
    public interface IIndicador2Repository
    {
        List<Indicador2ViewModel> Indicador2(string ibge, string sqlSelect, string sqlFiltros, string sqlAgrupamento);
        List<PubliAlvoIndicador2ViewModel> PublicoAlvo(string ibge, string sqlSelect, string sqlFiltros);
        int? CountPublicoAlvo(string ibge, string sqlFiltros);
        List<AtendimentoIndicador2ViewModel> Atendimentos(string ibge, int id_individuo, string dum, string gi_dt_nascimento);
    }
}
