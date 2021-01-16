using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Commands.Cadastro
{
    public interface ILogradouroCommand
    {
        string GetAllPagination { get; }
        string GetCountAll { get; }
        string GetLogradouroById { get; }
        string GetLogradouroByBairro { get; }
    }
}
