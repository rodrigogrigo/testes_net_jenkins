namespace RgCidadao.Api.ViewModels.Indicadores.Indicador3
{
    public class ParamsQueryIndicador3
    {
        public bool? flg_em_andamento { get; set; }
        public bool? flg_pn_particular { get; set; }
        public int? quadrimestre { get; set; }
        public int? ano { get; set; }
        public int? idade_gestacional_minima { get; set; }
        public int? idade_gestacional_maxima { get; set; }
        public string equipes { get; set; }
        public int? agente_saude { get; set; }
        public int? page { get; set; }
        public int? pagesize { get; set; }
        public int? sistema { get; set; }
        public int? valido { get; set; }
    }
}
