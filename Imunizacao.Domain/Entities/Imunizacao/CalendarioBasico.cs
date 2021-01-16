using System;

namespace RgCidadao.Domain.Entities.Imunizacao
{
    public class CalendarioBasico
    {
        public int? id { get; set; }
        public int? id_faixa_etaria { get; set; }
        public int? id_produto { get; set; }
        public int? id_dose { get; set; }
        public int? publico_alvo { get; set; }
        public int? dias_antes_aprazamento { get; set; }
        public string faixa_etaria { get; set; }
        public string produto { get; set; }
        public string dose { get; set; }
        public double idade_minima { get; set; }
        public double idade_maxima { get; set; }
        public int? flg_excluir_aprazamento { get; set; }
        public DateTime? vigencia_inicio { get; set; }
        public DateTime? vigencia_fim { get; set; }
        public int? id_estrategia { get; set; }
    }
}
