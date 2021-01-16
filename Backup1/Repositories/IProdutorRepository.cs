using Imunizacao.Domain.Entities;
using System.Collections.Generic;

namespace Imunizacao.Domain.Repositories
{
    public interface IProdutorRepository
    {
        void Insert(string ibge, Produtor model);
        int GetNewId(string ibge);
        List<Produtor> GetAll(string ibge);
    }
}
