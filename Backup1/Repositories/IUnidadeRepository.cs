using Imunizacao.Domain.Entities;
using System.Collections.Generic;

namespace Imunizacao.Domain.Repositories
{
    public interface IUnidadeRepository
    {
        List<Unidade> GetAll(string ibge, string filtro);
        List<Unidade> GetUnidadesByUser(string ibge, int user);
    }
}
