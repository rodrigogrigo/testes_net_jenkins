using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Entities.AtencaoBasica
{
    public class ConsultasItem
    {
        public int? id { get; set; }
        public int? id_diasmed { get; set; }
        public int? id_consultas { get; set; }
        public TimeSpan horario { get; set; }
        public int? ordem { get; set; }
        public string flg_reservado { get; set; }
    }
}
