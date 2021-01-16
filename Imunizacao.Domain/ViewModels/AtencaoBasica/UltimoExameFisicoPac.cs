using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.ViewModels.AtencaoBasica
{
    public class UltimoExameFisicoPac
    {
        public DateTime? data_peso { get; set; }
        public double? peso { get; set; }
        public DateTime? data_altura { get; set; }
        public double? altura { get; set; }
    }
}
