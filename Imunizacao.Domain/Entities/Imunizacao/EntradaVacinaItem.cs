using System;

namespace RgCidadao.Domain.Entities.Imunizacao
{
    public class EntradaVacinaItem
    {
        public EntradaVacinaItem()
        {
            Lote = new LoteImunobiologico();
            produto = new Produto();
        }

        public int? id { get; set; }
        public int? id_entrada_produto { get; set; }
        public int? id_unidade { get; set; }
        public DateTime? validade { get; set; }
        public int? id_apresentacao { get; set; }
        public int qtde_frascos { get; set; }
        public double valor { get; set; }
        public int? id_lote { get; set; }
        public string abreviatura { get; set; }
        public string sigla { get; set; }
        public string forma_apresentacao { get; set; }
        public int? qtde_doses { get; set; }
       

        public LoteImunobiologico Lote { get; set; }
        public Produto produto { get; set; }
    }
}
