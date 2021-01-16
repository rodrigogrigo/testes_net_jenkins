using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.ViewModels.AtencaoBasica
{
    public class VisitaDomiciliarViewModel
    {
      public int? id { get; set; }
      public DateTime? data_visita  { get; set; }
      public int? csi_codmed   { get; set; }
      public string csi_nommed   { get; set; }
      public int? csi_codpac   { get; set; }
      public string csi_nompac   { get; set; }
      public int? csi_coduni   { get; set; }
      public string csi_nomuni   { get; set; }

    }
}
