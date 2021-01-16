using Imunizacao.Domain.Entities;
using System.Collections.Generic;

namespace Imunizacao.Domain.Repositories
{
    public interface IProfissaoRepository
    {
        List<Profissao> GetAll(string ibge);
        List<Profissao> GetAllPagination(string ibge, int page, int pagesize, string filtro);
        int GetCountAll(string ibge, string filtro);
    }
}
