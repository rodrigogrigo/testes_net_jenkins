using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Commands.E_SUS
{
    public interface IConsumoAlimentarCommand
    {
        string GetAllPagination { get; }
        string GetCountAll { get; }
        string GetNewId { get; }
        string Insert { get; }
        string Editar { get; }
        string GetById { get; }
        string Delete { get; }

    }
}
