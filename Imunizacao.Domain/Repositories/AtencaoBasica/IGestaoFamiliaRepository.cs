using RgCidadao.Domain.ViewModels;
using RgCidadao.Domain.ViewModels.AtencaoBasica;
using System.Collections.Generic;

namespace RgCidadao.Domain.Repositories.AtencaoBasica
{
    public interface IGestaoFamiliaRepository
    {
        List<GestaoFamiliaViewModel> GetGestaoFamilia(string ibge, int competencia, int microarea);
        EstatisticaGestaoFamiliaViewModel GetEstatisticasByMicroarea(string ibge, int id_microarea, int competencia);
        EstatisticaGestaoFamiliaViewModel GetEstatisticasByEquipes(string ibge, string sqlFiltros, int competencia);
        List<MembroFamiliaViewModel> GetMembrosByFamilia(string ibge, int id_familia);

        List<GrupoIndividuosEquipeViewModel> GetDiabeticosByEquipe(string ibge, string sqlSelect, string filtro1, int competencia, string filtro2);
        List<GrupoIndividuosEquipeViewModel> GetHipertensosByEquipe(string ibge, string sqlSelect, string filtro1, int competencia, string filtro2);
        List<GrupoIndividuosEquipeViewModel> GetGestantesByEquipe(string ibge, string sqlSelect, string filtro1, int competencia, string filtro2);
        List<GrupoIndividuosEquipeViewModel> GetCriancasByEquipe(string ibge, string sqlSelect, string filtro1, int competencia, string filtro2);
        List<GrupoIndividuosEquipeViewModel> GetIdososByEquipe(string ibge, string sqlSelect, string filtro1, int competencia, string filtro2);

        TotalPorEquipeViewModel GetTotaisDiabeticoByEquipe(string ibge, string sqlfiltros, int competencia);
        TotalPorEquipeViewModel GetTotaisHipertensoByEquipe(string ibge, string sqlfiltros, int competencia);
        TotalPorEquipeViewModel GetTotaisGestanteByEquipe(string ibge, string sqlfiltros, int competencia);
        TotalPorEquipeViewModel GetTotaisCriancasByEquipe(string ibge, string sqlfiltros, int competencia);
        TotalPorEquipeViewModel GetTotaisIdososByEquipe(string ibge, string sqlfiltros, int competencia);
    }
}
