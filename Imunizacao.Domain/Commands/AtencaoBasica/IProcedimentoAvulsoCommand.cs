using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Commands.AtencaoBasica
{
    public interface IProcedimentoAvulsoCommand
    {
        string GetAllPagination { get; }
        string GetCountAll { get; }
        string GetProcedimentosById { get; }
        string GetProcedimentosItensByPai { get; }
        string GetNewId { get; }
        string Insert { get; }
        string Update { get; }
        string Delete { get; }

        string GetNewIdItem { get; }
        string InsertItens { get; }
        string ExcluirItens { get; }

        string GetProcedimentosConsolidados { get; }
        string GetProcedimentosIndividualizados { get; }
    }
}
