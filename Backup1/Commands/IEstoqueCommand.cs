namespace Imunizacao.Domain.Commands
{
    public interface IEstoqueCommand
    {
        string GetAllUnidadeWithEstoque { get; }
        string GetEstoqueLoteByUnidadeAndProduto { get; }
    }
}
