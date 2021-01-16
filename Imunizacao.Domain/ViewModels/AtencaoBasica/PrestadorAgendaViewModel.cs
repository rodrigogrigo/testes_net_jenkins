using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.ViewModels.AtencaoBasica
{
    public class PrestadorAgendaViewModel
    {
        public int? codigo { get; set; }
        public string nome { get; set; }
        public string telefone { get; set; }
        public double? liberado { get; set; }
        public string consorcio { get; set; }
        public string municipio { get; set; }
        public string tipo { get; set; }
    }
}
