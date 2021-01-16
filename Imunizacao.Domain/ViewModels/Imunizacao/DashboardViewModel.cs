using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.ViewModels.Imunizacao
{
    public class DashboardViewModel
    {
        public int? vacinasdia { get; set; }
        public int? vacinasvencidas { get; set; }
        public long? imunizadasmes { get; set; }
        public int? mesaplicacao { get; set; }
        public double? PercentualPolioPenta { get; set; }
        public int? hora { get; set; }
        public int? qtde_doses { get; set; }
    }
}
