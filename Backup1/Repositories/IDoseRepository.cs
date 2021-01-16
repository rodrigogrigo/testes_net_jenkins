using Imunizacao.Domain.Entities;
using System.Collections.Generic;

namespace Imunizacao.Domain.Repositories
{
    public interface IDoseRepository
    {
        List<Dose> GetAll(string ibge);
        List<Dose> GetDoseByEstrategiaAndProduto(string ibge, int estrategia, int produto);
    }
}
