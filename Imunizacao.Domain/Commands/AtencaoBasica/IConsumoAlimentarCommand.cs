using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Commands.AtencaoBasica
{
    public interface IConsumoAlimentarCommand
    {
        string GetAllPagination { get; }
        string GetCountAll { get; }
        string GetNewId { get; }
        string Insert { get; }
        string Update { get; }
        string GetConsumoAlimentarById { get; }
        string Delete { get; }
    }
}
