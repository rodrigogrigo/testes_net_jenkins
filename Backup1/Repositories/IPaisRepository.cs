using Imunizacao.Domain.Entities;
using System.Collections.Generic;

namespace Imunizacao.Domain.Repositories
{
    public interface IPaisRepository
    {
        List<Pais> GetAll(string ibge);
    }
}
