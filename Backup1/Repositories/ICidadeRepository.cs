using Imunizacao.Domain.Entities;
using System.Collections.Generic;

namespace Imunizacao.Domain.Repositories
{
    public interface ICidadeRepository
    {
        List<Cidade> GetAll(string ibge);
        int GetCountAll(string ibge, string filtro);
        List<Cidade> GetAllPagination(string ibge, int page, int pagesize, string filtro);
    }
}
