using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Commands.Imunizacao
{
    public interface IViaAdmCommand
    {
        string GetAllViaAdm { get; }
        string GetLocalAplicacaoByViaAdm { get; }
    }
}
