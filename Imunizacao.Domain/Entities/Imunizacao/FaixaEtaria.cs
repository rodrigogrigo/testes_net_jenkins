namespace RgCidadao.Domain.Entities.Imunizacao
{
    public class FaixaEtaria
    {
        public int? id { get; set; }
        public int? nu_inicio_ano { get; set; }
        public int? nu_fim_ano { get; set; }
        public int? nu_inicio_mes { get; set; }
        public int? nu_fim_mes { get; set; }
        public int? nu_inicio_dia { get; set; }
        public int? nu_fim_dia { get; set; }
        public string descricao { get; set; }
    }
}
