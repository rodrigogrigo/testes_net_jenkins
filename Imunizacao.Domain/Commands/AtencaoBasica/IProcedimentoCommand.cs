using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Commands.AtencaoBasica
{
    public interface IProcedimentoCommand
    {
        string GetProcedimentosByCompetencia022019 { get; }
        string GetProcedimentoBycbo { get; }
    }
}
