using Imunizacao.Domain.Entities;
using Imunizacao.Domain.ViewModels;
using System.Collections.Generic;

namespace Imunizacao.Domain.Repositories
{
    public interface IEstoqueRepository
    {
        List<UnidadeEstoqueViewModel> GetAllUnidadeWithEstoque(string ibge, int produto, int usuario);
        List<EstoqueProduto> GetEstoqueLoteByUnidadeAndProduto(string ibge, int unidade, int produto);
    }
}
