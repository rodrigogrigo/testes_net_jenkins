using RgCidadao.Domain.Entities.Imunizacao;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Repositories.Imunizacao
{
    public interface IUnidadeMedRepository
    {
        List<UnidadeMedida> GetAll(string ibge);
    }
}
