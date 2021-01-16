using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Report.Interfaces.Imunizacao
{
    public interface ICabecalhoSinteticoReport
    {
        string img_cabecalho { get; set; }
        string filtro { get; set; }
        string impresso_por { get; set; }
        string total_geral { get; set; }
    }
}
