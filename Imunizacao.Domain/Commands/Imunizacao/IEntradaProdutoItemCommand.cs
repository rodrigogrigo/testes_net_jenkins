namespace RgCidadao.Domain.Commands.Imunizacao
{
    public interface IEntradaProdutoItemCommand
    {
        string GetEntradaItemById { get; }
        string GetNewId { get; }
        string InsertOrUpdate { get; }
        string Delete { get; }
        string GetAllItensByPai { get; }
        string DeleteAllByPai { get; }
        string PossuiMovimentoByEstradaItem { get; }
        string GetValorUltimaEntradaLote { get; }
        string GetUltimaEntradaItemByLote { get; }
        string GetEntradaProdutoItemById { get; }
        string GetItensEntradaProdutoByIdEntradaProduto { get; }
    }
}
