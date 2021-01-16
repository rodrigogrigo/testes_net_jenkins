using Imunizacao.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Imunizacao.Domain.Repositories
{
    public interface IViaAdmRepository
    {
        List<ViaAdministracao> GetAllViaAdm(string ibge, string filtro);
        List<LocalAplicacao> GetLocalAplicacaoByViaAdm(string ibge, int? id);
    }
}
