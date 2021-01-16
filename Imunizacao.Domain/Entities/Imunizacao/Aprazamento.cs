using System;

namespace RgCidadao.Domain.Entities.Imunizacao
{
    public class Aprazamento
    {
        public int? id_aprazamento { get; set; }
        public DateTime? data_limite { get; set; }
        public int? id_produto { get; set; }
        public int? id_dose { get; set; }
        public string nome_produto { get; set; }
        public string abreviatura_produto { get; set; }
        public string sigla_produto { get; set; }
        public string descricao_dose { get; set; }
        public string sigla_dose { get; set; }
        public int? id_vacinados { get; set; }
        public string lote { get; set; }
        public string profissional { get; set; }
        public DateTime? data_aplicacao { get; set; }
        public double? idade_minima { get; set; }
        public double? idade_maxima { get; set; }
        public int? id_individuo { get; set; }
        public int? id_calendario_basico { get; set; }
        public int? flg_excluir_aprazamento { get; set; }
        public string observacao { get; set; }
        public int? publico_alvo { get; set; }
        public int? id_estrategia { get; set; }
        public int? id_esus_exportacao_item { get; set; }
    }
}
