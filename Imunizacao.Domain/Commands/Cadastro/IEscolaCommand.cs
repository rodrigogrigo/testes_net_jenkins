using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Commands.Cadastro
{
    public interface IEscolaCommand
    {
        string GetAll { get; }
        string GetNewId { get; }
        string GetAllPagination { get; }
        string GetCountAll { get; }
        string GetEscolaById { get; }
        string Insert { get; }
        string Update { get; }
        string Delete { get; }
    }
}
