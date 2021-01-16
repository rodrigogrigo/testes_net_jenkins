using Imunizacao.Domain.Entities;
using System.Collections.Generic;

namespace Imunizacao.Domain.Repositories
{
    public interface IProdutoRepository
    {
        List<Produto> GetAll(string ibge, string filtro, int page, int pagesize);
        int GetCountAll(string ibge, string filtro);
        List<Produto> GetImunobiologico(string ibge);
        List<Produto> GetImunobiologicoEstoqueByUnidade(string ibge, int unidade);
        EstoqueProduto GetEstoqueImunobiologicoByParams(string ibge, int produto, int unidade, int produtor, string lote);
        List<Produto> GetProdutoByEstrategia(string ibge, int estrategia);
       
    }
}
