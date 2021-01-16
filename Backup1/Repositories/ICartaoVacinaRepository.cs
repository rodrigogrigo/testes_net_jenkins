using Imunizacao.Domain.Entities;

namespace Imunizacao.Domain.Repositories
{
    public interface ICartaoVacinaRepository
    {
        int GetNewId(string ibge);
        CartaoVacina GetCartaoVacinaById(string ibge, int id);
        void Insert(string ibge, CartaoVacina model);
        void Update(string ibge, CartaoVacina model);
        void Delete(string ibge, int id);
    }
}
