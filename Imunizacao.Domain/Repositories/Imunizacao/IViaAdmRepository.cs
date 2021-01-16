using RgCidadao.Domain.Entities.Imunizacao;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Repositories.Imunizacao
{
    public interface IViaAdmRepository
    {
        List<ViaAdministracao> GetAllViaAdm(string ibge, string filtro);
        List<LocalAplicacao> GetLocalAplicacaoByViaAdm(string ibge, int? id);
    }
}
