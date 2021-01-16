using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Entities.Imunizacao
{
    public class EnvioItem
    {
        public int? id { get; set; }
        public int? id_envio { get; set; }
        public int? id_produto { get; set; }
        public int? id_produtor { get; set; }
        public string lote { get; set; }
        public int qtde_frascos { get; set; }
        public int? id_apresentacao { get; set; }
        public double? valor { get; set; }
        public string abreviatura { get; set; }
        public string sigla { get; set; }
        public string produto { get; set; }
        public int? id_lote { get; set; }
        public string lote_produtor { get; set; }
    }
}
