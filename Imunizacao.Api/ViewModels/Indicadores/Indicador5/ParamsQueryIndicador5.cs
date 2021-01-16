namespace RgCidadao.Api.ViewModels.Indicadores.Indicador5
{
    public class ParamsQueryIndicador5
    {
        public int? quadrimestre { get; set; }
        public string equipes { get; set; }
        public int? agente_saude { get; set; }
        public int? page { get; set; }
        public int? pagesize { get; set; }
        public int? ano { get; set; }
        public double? idade_inicial { get; set; }
        public double? idade_final { get; set; }
    }
}
