using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.ViewModels.AtencaoBasica
{
    public class AgendaConsultaViewModel
    {
        public int? csi_controle { get; set; }
        public DateTime? csi_dataag { get; set; }
        public string csi_horario { get; set; }
        public int? csi_codpac { get; set; }
        public string csi_nompac { get; set; }
        public string csi_tipo_consulta { get; set; }
        public string csi_descnatatend { get; set; }
        public string csi_status { get; set; }
        public string csi_obs { get; set; }
        public string csi_obs_cancelamento { get; set; }
        public int? cod_ponto { get; set; }
        public int? id { get; set; }
        public TimeSpan? horarioini { get; set; }
        public TimeSpan? horariofinal { get; set; }
        public int? csi_intervalo_agendamento { get; set; }
        public int? csi_qtdecon { get; set; }
        public int? id_diasmed { get; set; }
        public DateTime? csi_dataconf { get; set; }
        public int? csi_ordem { get; set; }
        public int? triado { get; set; }
        public int? csi_codnatatend { get; set; }
        public DateTime? csi_datacon { get; set; }
        public string csi_cbo { get; set; }
        public int? csi_id_libexames { get; set; }
        public string csi_forma_faturamento { get; set; }
        public int? csi_forma_agendamento { get; set; }
        public int? csi_id_prestador { get; set; }
        public int? csi_id_prestador_consorcio { get; set; }
        public string nome_procedimento { get; set; }
    }
}
