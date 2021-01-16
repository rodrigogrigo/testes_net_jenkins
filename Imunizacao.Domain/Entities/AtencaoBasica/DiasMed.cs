using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Entities.AtencaoBasica
{
    public class DiasMed
    {
        public int? csi_codmed { get; set; }
        public DateTime? csi_data { get; set; }
        public string csi_horario { get; set; }
        public int? csi_qtdecon { get; set; }
        public int? csi_copdponto { get; set; }
        public string csi_procedimento { get; set; }
        public string csi_cbo { get; set; }
        public int? csi_forma_agendamento { get; set; }
        public int? csi_intervalo_agendamento { get; set; }
        public string csi_forma_faturamento { get; set; }
        public int? csi_id_prestador { get; set; }
        public DateTime? csi_data_criacao { get; set; }
        public int? id { get; set; }
        public string csi_horariofinal { get; set; }
        public int? id_grupo_procedimento_cota { get; set; }
        public int? id_controle_sincronizacao_lote { get; set; }
        public string uuid { get; set; }
    }
}
