using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Commands.AtencaoBasica
{
    public interface IEstabelecimentoCommand
    {
        string GetEstabelecimentoById { get; }
        string GetNewId { get; }
        string Insert { get; }
        string Update { get; }
        string GetEstabelecimentosByArea { get; }
        string GetCountEstabelecimentosByArea { get; }        
    }
}
