using RgCidadao.Report.Interfaces.Imunizacao;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Report.Models.Imunizacao
{
    public class CabecalhoSintetico : ICabecalhoSinteticoReport
    {
        private static string _filtro;
        private static string _ImpressoPor;
        private static string _img_cabecalho;
        private static string _total_geral;

        public string filtro { get => _filtro; set => _filtro = value; }
        public string impresso_por { get => _ImpressoPor; set => _ImpressoPor = value; }
        public string img_cabecalho { get => _img_cabecalho; set => _img_cabecalho = value; }
        public string total_geral { get => _total_geral; set => _total_geral = value; }
    }
}
