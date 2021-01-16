using RgCidadao.Domain.Entities.Imunizacao;
using System.Collections.Generic;

namespace RgCidadao.Domain.Repositories.Imunizacao
{
    public interface IDoseRepository
    {
        List<Dose> GetAll(string ibge);
        List<Dose> GetDoseByEstrategiaAndProduto(string ibge, int estrategia, int produto);
    }
}
