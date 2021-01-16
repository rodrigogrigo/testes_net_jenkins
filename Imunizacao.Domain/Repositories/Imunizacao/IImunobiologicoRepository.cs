using RgCidadao.Domain.Entities.Imunizacao;
using System.Collections.Generic;

namespace RgCidadao.Domain.Repositories.Imunizacao
{
    public interface IImunobiologicoRepository
    {
        List<Imunobiologico> GetAllImunobiologico(string ibge);
    }
}
