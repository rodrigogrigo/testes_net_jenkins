namespace RgCidadao.Domain.Commands.AtencaoBasica
{
    public interface IGestaoFamiliaCommand
    {
        string GetEstabelecimentoByMicroarea { get; }
        string GetFamiliaByEstabelecimento { get; }
        string GetVisitaByEstabelecimento { get; }
        string GetEstatisticasByMicroarea { get; }
        string GetEstatisticasByEquipes { get; }
        string GetMembrosByFamilia { get; }

        string GetDiabeticosByEquipe { get; }
        string GetHipertensosByEquipe { get; }
        string GetGestantesByEquipe { get; }
        string GetCriancasByEquipe { get; }
        string GetIdososByEquipe { get; }

        string GetTotaisDiabeticoByEquipe { get; }
        string GetTotaisHipertensoByEquipe { get; }
        string GetTotaisGestanteByEquipe { get; }
        string GetTotaisCriancasByEquipe { get; }
        string GetTotaisIdososByEquipe { get; }

    }
}
