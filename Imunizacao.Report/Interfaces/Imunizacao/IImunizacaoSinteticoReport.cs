using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Report.Interfaces.Imunizacao
{
    public interface IImunizacaoSinteticoReport
    {
        string nome_unidade { get; set; }
        string imunobiologico { get; set; }
        string dose { get; set; }
        int qtde { get; set; }
    }
}
