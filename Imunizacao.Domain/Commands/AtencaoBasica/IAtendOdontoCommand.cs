using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Commands.AtencaoBasica
{
    public interface IAtendOdontoCommand
    {
        string GetCountAll { get; }
        string GetAllPagination { get; }
        string GetAtendOdontoById { get; }
        string GetAtendOdontoItensByPai { get; }
        string InsertOrUpdate { get; }
        string InsertOrUpdateItens { get; }
        string GetNewId { get; }
        string GetNewIdItem { get; }
        string ExcluirItemPai { get; }
        string ExcluirItensByPai { get; }
        string ExcluirItemById { get; }

        string GetAtendOdontoItemById { get; }

        string GetProcOdontoIndividualizado { get; }
        string GetCountProcOdontoIndividualizado { get; }
    }
}
