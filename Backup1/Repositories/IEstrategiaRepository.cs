using Imunizacao.Domain.Entities;
using System.Collections.Generic;

namespace Imunizacao.Domain.Repositories
{
    public interface IEstrategiaRepository
    {
        List<Estrategia> GetEstrategiasByProduto(string ibge, int id);
        List<Estrategia> GetAll(string ibge);
    }
}
