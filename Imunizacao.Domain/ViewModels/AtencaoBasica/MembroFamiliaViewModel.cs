using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.ViewModels.AtencaoBasica
{
    public class MembroFamiliaViewModel
    {
        public int? csi_codpac { get; set; }
        public string csi_nompac { get; set; }
        public DateTime? csi_dtnasc { get; set; }
        public string hipertenso { get; set; }
        public string diabetico { get; set; }
        public string csi_ncartao { get; set; }
        public string csi_cpfpac { get; set; }
        public string gestante { get; set; }
        public int? idade_anos { get; set; }
        public int? vacinas_atrasadas { get; set; }
        public string csi_sexpac { get; set; }

        public string idade_calc { get; set; }
    }
}
