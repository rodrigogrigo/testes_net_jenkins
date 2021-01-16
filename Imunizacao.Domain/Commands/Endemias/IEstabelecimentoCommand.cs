using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Commands.Endemias
{
    public interface IEstabelecimentoCommand
    {
        string GetImovelById { get; }
        string GetCountAll { get; }
        string GetAllPagination { get; }
        string GetAll { get; }
        string GetNewId { get; }
        string Insert { get; }
        string Update { get; }
        string Delete { get; }
        string GetEstabelecimentoByCiclo { get; }
    }
}
