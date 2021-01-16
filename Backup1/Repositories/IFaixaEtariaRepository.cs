using Imunizacao.Domain.Entities;
using System.Collections.Generic;

namespace Imunizacao.Domain.Repositories
{
    public interface IFaixaEtariaRepository
    {
        List<FaixaEtaria> GetAll(string ibge);
    }
}
