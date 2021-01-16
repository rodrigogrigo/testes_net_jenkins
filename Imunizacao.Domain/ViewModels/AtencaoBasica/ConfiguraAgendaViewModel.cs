using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.ViewModels.AtencaoBasica
{
    public class ConfiguraAgendaViewModel
    {
        public int? id { get; set; }
        public string unidade { get; set; }
        public string csi_cbo { get; set; }
        public TimeSpan? horarioini { get; set; }
        public TimeSpan? horariofinal { get; set; }
        public int? csi_qtdecon { get; set; }
        public int? csi_intervalo_agendamento { get; set; }

    }
}
