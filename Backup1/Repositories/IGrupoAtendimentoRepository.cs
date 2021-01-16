using Imunizacao.Domain.Entities;
using System.Collections.Generic;

namespace Imunizacao.Domain.Repositories
{
    public interface IGrupoAtendimentoRepository
    {
        List<GrupoAtendimento> GetAll(string ibge);
    }
}
