using RgCidadao.Domain.Entities.Imunizacao;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Repositories.Imunizacao
{
    public interface IClasseRepository
    {
        List<Classe> GetAll(string ibge);
    }
}
