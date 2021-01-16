using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Commands.Cadastro
{
    public interface IBairroCommand
    {
        string GetCountAll { get; }
        string GetAllPagination { get; }
        string GetBairroById { get; }
        string GetAll { get; }
        string GetBairroByIbge { get; }
    }
}
