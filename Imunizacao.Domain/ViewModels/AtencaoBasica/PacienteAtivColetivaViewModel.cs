using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.ViewModels.AtencaoBasica
{
    public class PacienteAtivColetivaViewModel
    {
        public int? id { get; set; }
        public int? csi_codpac { get; set; }
        public string csi_nompac { get; set; }
        public string csi_sexpac { get; set; }
        public string csi_ncartao { get; set; }
        public double? csi_peso { get; set; }
        public double? csi_data_peso { get; set; }
        public double? csi_altura { get; set; }
        public double? csi_data_altura { get; set; }
        public string cessou_habito_fumar { get; set; }
        public string avalia_alterada { get; set; }
        public string csi_dtnasc { get; set; }
        public DateTime? dtnascimento { get; set; }
        public string csi_cpfpac { get; set; }
    }
}
