using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Commands.Imunizacao
{
    public interface IReportCommand
    {
        string GetReportImunizacao { get; }      

        string GetReportMovimento { get; }
        string GetReportDetalhamento { get; }

        string GetReportCartaoVacina { get; }

        //sql novo
        string GetReportBoletimMovimento { get; }
    }
}
