using System;

namespace RgCidadao.Domain.Entities.Imunizacao
{
    public class MovimentoImunobiologico
    {
        public int? id { get; set; }
        public string produto { get; set; }
        public string lote { get; set; }
        public string produtor { get; set; }
        public string apresentacao { get; set; }
        public int? ano_apuracao { get; set; }
        public int? mes_apuracao { get; set; }
        public int? id_unidade { get; set; }
        public int? id_produtor { get; set; }
        public int? id_produto { get; set; }
        public int? id_apresentacao { get; set; }
        public int? qtde { get; set; }
        public int? qtde_frascos { get; set; }
        public int? id_usuario { get; set; }
        //public string data { get; set; }
        public DateTime? data { get; set; }
        public int? tipo_lancamento { get; set; }
        public int? id_fornecedor { get; set; }
        public string observacao { get; set; }
        public string abreviatura { get; set; }
        public string sigla { get; set; }
        public int? tipo_perca { get; set; }
        public string usuario { get; set; }
        public int? id_unidade_envio { get; set; }
        public int? id_envio_item { get; set; }
    }
}
