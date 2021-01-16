using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.ViewModels.AtencaoBasica
{
    public class EstatisticaGestaoFamiliaViewModel
    {
        public int? individuo_total { get; set; }
        public int? familia_total { get; set; }
        public int? familia_visitada { get; set; }
        public int? individuo_visitado { get; set; }
        public int? diabetico_total { get; set; }
        public int? diabetico_visitado { get; set; }
        public int? hipertenso_total { get; set; }
        public int? hipertenso_visitado { get; set; }
        public int? gestante_total { get; set; }
        public int? gestante_visitado { get; set; }
        public int? crianca_total { get; set; }
        public int? crianca_visitado { get; set; }
        public int? idoso_total { get; set; }
        public int? idoso_visitado { get; set; }
    }
}
