using Imunizacao.Domain.Entities;
using System.Collections.Generic;

namespace Imunizacao.Domain.Repositories
{
    public interface IACSRepository
    {
        List<ACS> GetAll(string ibge);
        List<ACS> GetAllPagination(string ibge, string filtro, int page, int pagesize);
        int GetCountAll(string ibge, string filtro);
    }
}
