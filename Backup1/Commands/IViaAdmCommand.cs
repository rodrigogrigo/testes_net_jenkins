using System;
using System.Collections.Generic;
using System.Text;

namespace Imunizacao.Domain.Commands
{
    public interface IViaAdmCommand
    {
        string GetAllViaAdm { get; }
        string GetLocalAplicacaoByViaAdm { get; }
    }
}
