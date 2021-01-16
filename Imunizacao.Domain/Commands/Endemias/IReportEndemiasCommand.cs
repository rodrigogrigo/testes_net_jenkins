using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Commands.Endemias
{
    public interface IReportEndemiasCommand
    {
        string InfestacaoPredialSintetico { get; }
        string ServicoAntivetorialAnalitico { get; }
        string ServicoAntivetorialTotalizador { get; }
        string ServAntivetorialTrabalhoCampoTotais { get; }
        string ServAntivetorialResumoLab { get; }
        string ServAntivetorialInfectados { get; }
    }
}
