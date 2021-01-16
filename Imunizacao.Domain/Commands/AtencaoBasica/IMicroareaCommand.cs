using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Commands.AtencaoBasica
{
    public interface IMicroareaCommand
    {
        string GetMicroareas { get; }
        string GetMicroareasByUnidade { get; }
    }
}
