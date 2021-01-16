namespace Imunizacao.Domain.Commands
{
    public interface IProdutoCommand
    {
        string GetAll { get; }
        string GetCountAll { get; }
        string GetImunobiologico { get; }
        string GetImunobiologicoEstoqueByUnidade { get; }
        string GetEstoqueImunobiologicoByParams { get; }
        string GetProdutoByEstrategia { get; }
    }
}
