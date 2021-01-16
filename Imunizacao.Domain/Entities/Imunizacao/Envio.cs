using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Entities.Imunizacao
{
    public class Envio
    {
        public Envio()
        {
            Itens = new List<EnvioItem>();
        }
        public int? id { get; set; }
        public int? id_unidade_origem { get; set; }
        public int? id_unidade_destino { get; set; }
        public DateTime? data_envio { get; set; }
        public int? id_usuario { get; set; }
        public int? status { get; set; }
        public string observacao { get; set; }
        public List<EnvioItem> Itens { get; set; }
        public string unidade_destino { get; set; }
        public string unidade_origem { get; set; }
        public string usuario { get; set; }
    }
}
