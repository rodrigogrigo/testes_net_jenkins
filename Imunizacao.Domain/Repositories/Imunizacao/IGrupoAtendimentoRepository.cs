using RgCidadao.Domain.Entities.Imunizacao;
using System.Collections.Generic;

namespace RgCidadao.Domain.Repositories.Imunizacao
{
    public interface IGrupoAtendimentoRepository
    {
        List<GrupoAtendimento> GetAll(string ibge);
    }
}
