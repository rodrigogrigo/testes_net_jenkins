using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Commands.AtencaoBasica
{
   public interface IVisitaDomiciliarCommand
    {
        string getAllPagination { get; }
        string getCountAll { get; }
        string getById { get; }

        string Insert { get; }
        string Update { get; }
        string GetNewId { get; }
        string Delete { get; }
    }
}
