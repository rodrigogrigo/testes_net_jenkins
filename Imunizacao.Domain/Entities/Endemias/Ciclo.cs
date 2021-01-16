using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Entities.Endemias
{
    public class Ciclo
    {
        public int? id { get; set; }
        public DateTime? data_inicial { get; set; }
        public DateTime? data_final { get; set; }
        public string num_ciclo { get; set; }
        public int? situacao { get; set; }
        public DateTime? data_situacao { get; set; }
        public int? id_usuario { get; set; }

    }
}
