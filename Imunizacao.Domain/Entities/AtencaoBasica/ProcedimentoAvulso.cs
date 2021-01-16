using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Entities.AtencaoBasica
{
    public class ProcedimentoAvulso
    {
        public int? csi_controle { get; set; }
        public int? csi_codpac { get; set; }
        public string csi_nompac { get; set; }
        public int? csi_codmed { get; set; }
        public string csi_nommed { get; set; }
        public DateTime? csi_data { get; set; }
        public string csi_nomusu { get; set; }
        public DateTime? csi_datainc { get; set; }
        public string csi_cbo { get; set; }
        public int? csi_coduni { get; set; }
        public string csi_nomuni { get; set; }
    }
}
