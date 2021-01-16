namespace RgCidadao.Domain.Commands.Imunizacao
{
    public interface IEstoqueCommand
    {
        string GetAllUnidadeWithEstoque { get; }
        string GetEstoqueLoteByUnidadeAndProduto { get; }
        string GetCountAuditoria { get; }
        string GetAuditoria { get; }
    }
}
