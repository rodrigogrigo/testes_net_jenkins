using Imunizacao.Domain.Entities;
using System.Collections.Generic;

namespace Imunizacao.Domain.Repositories
{
    public interface IEntradaProdutoRepository
    {
        EntradaVacina GetEntradaById(string ibge, int id);
        int GetNewId(string ibge);
        void InsertOrUpdate(string ibge, EntradaVacina model);
        void Delete(string ibge, int id);
        void AtualizaValor(string ibge, int id, double valor);
        List<EntradaVacina> GetEntradaVacinaByUnidade(string ibge, string unidade, int page, int pagesize, string filtro);
        int GetCountEntradaVacina(string ibge, string unidade, string filtro);
    }
}
