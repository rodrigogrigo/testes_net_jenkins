using RgCidadao.Domain.Entities.Imunizacao;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Commands.Imunizacao
{
    public interface IEnvioCommand
    {
        string GetNewIdEnvio { get; }
        string GetAllPagination { get; }
        string GetCountAll { get; }
        string GetEnvioById { get; }
        string InsertOrUpdate { get; }
        string Delete { get; }
        string UpdateStatusEnviado { get; }

        string GetNewIdItem { get; }
        string GetAllItensByPai { get; }
        string GetItemById { get; }
        string DeleteItem { get; }
        string InsertOrUpdateItens { get; }

        string ValidaEstoqueItensEnvio { get; }

        string GetTranferenciaByUnidadeDestino { get; }
    }
}
