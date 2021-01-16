using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Commands.Cadastro
{
    public interface IEstadoCommand
    {
        string GetAll { get; }
        string GetEstadoById { get; }
    }
}
