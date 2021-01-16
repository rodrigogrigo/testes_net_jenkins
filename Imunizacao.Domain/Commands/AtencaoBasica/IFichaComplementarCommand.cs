using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Commands.AtencaoBasica
{
    public interface IFichaComplementarCommand
    {
        string GetAllPagination { get; }
        string GetCountAll { get; }
        string GetFichaComplementarById { get; }

        string Insert { get; }
        string Update { get; }
        string GetNewId { get; }
        string Delete { get; }

    }
}
