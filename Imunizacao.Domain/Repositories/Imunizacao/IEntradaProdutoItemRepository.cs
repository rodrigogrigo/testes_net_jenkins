using RgCidadao.Domain.Entities.Imunizacao;
using System.Collections.Generic;
using RgCidadao.Domain.ViewModels.Imunizacao;

namespace RgCidadao.Domain.Repositories.Imunizacao
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
        double? GetValorUltimaEntradaLote(string ibge, string lote);
        EntradaVacinaItem GetUltimaEntradaItemByLote(string ibge, int? lote);
        EntradaProdutoItem GetEntradaProdutoItemById(string ibge, int id_entrada_produto_item);
        List<EntradaProdutoItem> GetItensEntradaProdutoByIdEntradaProduto(string ibge, int id_entrada_produto);
    }
}
