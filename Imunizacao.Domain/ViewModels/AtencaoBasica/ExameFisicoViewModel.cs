using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.ViewModels.AtencaoBasica
{
    public class ExameFisicoViewModel
    {
        public int? codigo_exame_fisico { get; set; }
        public int? csi_codpac { get; set; }
        public string csi_nompac { get; set; }
        public int? csi_codmed { get; set; }
        public string csi_nommed { get; set; }
        public string data_exame_fisico { get; set; }
    }

    public class IProcenfermagemViewModel
    {
        public int? csi_codproc { get; set; }
        public int? csi_qtde { get; set; }

    }
}
