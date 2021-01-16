using RgCidadao.Domain.Entities.Imunizacao;
using System.Collections.Generic;

namespace RgCidadao.Domain.Repositories.Imunizacao
{
    public interface ICartaoVacinaRepository
    {
        int GetNewId(string ibge);
        CartaoVacina GetCartaoVacinaById(string ibge, int id);
        void Insert(string ibge, CartaoVacina model);
        void Update(string ibge, CartaoVacina model);
        void Delete(string ibge, int id);
        List<CartaoVacina> GetCartaoVacinaByProduto(string ibge, int id_produto);
        List<CartaoVacina> GetCartaoVacinaByProdutor(string ibge, int id_produtor);
    }
}
