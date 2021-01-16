using Imunizacao.Domain.Entities;
using System.Collections.Generic;

namespace Imunizacao.Domain.Repositories
{
    public interface IFabricanteRepository
    {
        List<Fabricante> GetAll(string ibge, string filtro);
        int GetNewId(string ibge);
        void Inserir(string ibge, Fabricante model);
        void Atualizar(string ibge, Fabricante model);
        void Delete(string ibge, int id);
        List<Fabricante> GetProdutorByImunobiologico(string ibge, int imuno);
    }
}
