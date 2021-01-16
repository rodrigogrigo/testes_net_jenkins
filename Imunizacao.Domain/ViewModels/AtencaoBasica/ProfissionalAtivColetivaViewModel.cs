using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.ViewModels.AtencaoBasica
{
    public class ProfissionalAtivColetivaViewModel
    {
        public int? id { get; set; }
        public int? csi_codmed { get; set; }
        public string csi_nommed { get; set; }
        public string csi_cbo { get; set; }
    }
}
