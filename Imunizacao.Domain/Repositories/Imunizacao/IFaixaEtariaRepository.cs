using RgCidadao.Domain.Entities.Imunizacao;
using System.Collections.Generic;

namespace RgCidadao.Domain.Repositories.Imunizacao
{
    public interface IFaixaEtariaRepository
    {
        List<FaixaEtaria> GetAll(string ibge);
    }
}
