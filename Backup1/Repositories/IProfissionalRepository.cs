using Imunizacao.Domain.Entities;
using System.Collections.Generic;

namespace Imunizacao.Domain.Repositories
{
    public interface IProfissionalRepository
    {
        List<Profissional> GetAll(string ibge, string filtro);
        List<Profissional> GetProfissionalByUnidade(string ibge, int unidade);
        List<CBO> GetCboProfissional(string ibge, int profissional);
        List<Profissional> GetProfissionalCBOByUnidade(string ibge, int unidade);
    }
}
