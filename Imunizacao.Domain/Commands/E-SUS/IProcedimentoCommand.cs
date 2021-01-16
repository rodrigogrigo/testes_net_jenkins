using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Commands.E_SUS
{
    public interface IProcedimentoCommand
    {
        string GetAllPagination { get; }
        string GetCountAll { get; }
        string GetProcEnfermagemById { get; }
        string GetProcEnfermagemItensByPai { get; }
        string GetProcEnfermagemItemByPaiProc { get; }
        string GetNewId { get; }
        string Inserir { get; }
        string InserirItem { get; }
        string GetNewIdItem { get; }
        string GetNewUuid { get; }
        string Editar { get; }
        string EditarItem { get; }
        string Excluir { get; }
        string ExcluirItem { get; }
    }
}
