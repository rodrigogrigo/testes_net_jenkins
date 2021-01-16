using Imunizacao.Domain.Entities;
using System.Collections.Generic;

namespace Imunizacao.Domain.Repositories
{
    public interface IEntradaProdutoItemRepository
    {
        EntradaVacinaItem GetEntradaItemById(string ibge, int id);
        int GetNewId(string ibge);
        void InsertOrUpdate(string ibge, EntradaVacinaItem model);
        void Delete(string ibge, int id);
        List<EntradaVacinaItem> GetAllItensByPai(string ibge, int id);
        void DeleteAllByPai(string ibge, int id);
        bool PossuiMovimentoByEntradaItem(string ibge, int id);
    }
}
