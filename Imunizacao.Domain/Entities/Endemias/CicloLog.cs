using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Entities.Endemias
{
    public class CicloLog
    {
        public int? id { get; set; }
        public int? id_ciclo { get; set; }
        public int? situacao { get; set; }
        public DateTime? data_situacao { get; set; }
        public int? id_usuario { get; set; }
        public string usuario { get; set; }
    }
}
