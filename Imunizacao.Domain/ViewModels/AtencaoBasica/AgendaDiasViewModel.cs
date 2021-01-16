using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.ViewModels.AtencaoBasica
{
    public class AgendaDiasViewModel
    {
        public DateTime? Data { get; set; }
        public int? tipo { get; set; }
        public int? vagas { get; set; }

        public int? diasmed { get; set; }
        public int? id { get; set; }
        public int? ordem { get; set; }
        public string flg_reservado { get; set; }
        public int? ordem_pac { get; set; }
        public int? csi_controle { get; set; }
        public string csi_horario { get; set; }
    }
}
