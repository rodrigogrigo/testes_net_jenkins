using System;
using RgCidadao.Domain.ViewModels.Indicadores;
using System.Collections.Generic;

namespace RgCidadao.Domain.Repositories.Indicadores
{
    public interface IIndicador1Repository
    {
        List<Indicador1ViewModel> Indicador1(string ibge, string sqlSelect, string sqlFiltros, string sqlAgrupamento);
        bool VerificaNovaEstrutura(string ibge);
        int? CountPublicoAlvo(string ibge, string sqlFiltros);
        List<PubliAlvoIndicador1ViewModel> PublicoAlvo(string ibge, string sqlSelect, string sqlFiltros);
        List<AtendimentoIndicador1ViewModel> Atendimentos(string ibge, int id_individuo, string dum, string gi_dt_nascimento);
    }
}
