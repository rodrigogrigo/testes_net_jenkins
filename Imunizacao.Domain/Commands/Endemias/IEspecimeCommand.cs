using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Commands.Endemias
{
    public interface IEspecimeCommand
    {
        string Insert { get; }
        string GetEspecimeById { get; }
        string Update { get; }
        string Delete { get; }
        string GetAllPagination { get; }
        string GetCountAll { get; }
        string GetEspecimeNewId { get; }
        string GetAllEspecime { get; }
    }
}
