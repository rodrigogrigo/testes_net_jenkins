using RgCidadao.Domain.Entities.Imunizacao;
using System.Collections.Generic;

namespace RgCidadao.Domain.Repositories.Imunizacao
{
    public interface IProdutorRepository
    {
        void Insert(string ibge, Produtor model);
        void Update(string ibge, Produtor model);
        int GetCountAll(string ibge, string filtro);
        int GetNewId(string ibge);
        List<Produtor> GetAllPagination(string ibge, int pagesize, int page, string filtro);
        List<Produtor> GetAll(string ibge);
        Produtor GetProdutorById(string ibge, int id);
        void Delete(string ibge, int id);
    }
}
