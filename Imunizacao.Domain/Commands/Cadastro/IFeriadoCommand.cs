using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Commands.Cadastro
{
    public interface IFeriadoCommand
    {
        string GetAllPagination { get; }
        string GetAll { get; }
        string GetCountAll { get; }
        string GetFeriadoById { get; }
        string Insert { get; }
        string Update { get; }
        string Delete { get; }
    }
}
