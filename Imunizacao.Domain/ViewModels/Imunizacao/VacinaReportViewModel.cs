using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.ViewModels.Imunizacao
{
    public class VacinaReportViewModel
    {
        public DateTime? data_hora { get; set; }
        public string produto { get; set; }
        public string dose { get; set; }
        public string laboratorio { get; set; }
        public string lote { get; set; }
        public string dsc_equipe { get; set; }
        public string estrategia { get; set; }
        public string sigla { get; set; }
    }
}
