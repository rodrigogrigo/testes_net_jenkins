using RgCidadao.Domain.Entities.Imunizacao;
using System.Collections.Generic;

namespace RgCidadao.Domain.Repositories.Imunizacao
{
    public interface IProdutoRepository
    {
        List<Produto> GetAll(string ibge, string filtro, int page, int pagesize);
        int GetCountAll(string ibge, string filtro);
        List<Produto> GetImunobiologico(string ibge, bool orderAbrev);
        List<Produto> GetImunobiologicoEstoqueByUnidade(string ibge, int unidade);
        EstoqueProduto GetEstoqueImunobiologicoByParams(string ibge, int produto, int unidade, int produtor, string lote);
        List<Produto> GetProdutoByEstrategia(string ibge, int estrategia);

        int? GetNewId(string ibge);
        void Insert(string ibge, Produto produto);
        void Update(string ibge, Produto produto);
        Produto GetProdutoById(string ibge, int id);
        void Delete(string ibge, int id);
    }
}
