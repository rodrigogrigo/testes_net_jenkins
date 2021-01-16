using RgCidadao.Domain.Entities.Imunizacao;
using System.Collections.Generic;

namespace RgCidadao.Domain.Repositories.Imunizacao
{
    public interface IEstrategiaRepository
    {
        List<Estrategia> GetEstrategiasByProduto(string ibge, int id);
        List<Estrategia> GetAll(string ibge);
    }
}
