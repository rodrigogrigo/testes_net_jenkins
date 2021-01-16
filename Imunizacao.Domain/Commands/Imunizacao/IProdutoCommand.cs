namespace RgCidadao.Domain.Commands.Imunizacao
{
    public interface IProdutoCommand
    {
        string GetAll { get; }
        string GetCountAll { get; }
        string GetImunobiologico { get; }
        string GetImunobiologicoEstoqueByUnidade { get; }
        string GetEstoqueImunobiologicoByParams { get; }
        string GetProdutoByEstrategia { get; }

        string GetNewId { get; }
        string Insert { get; }
        string Update { get; }
        string GetProdutoById { get; }
        string Delete { get; }
    }
}
