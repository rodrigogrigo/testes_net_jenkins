namespace Imunizacao.Domain.Commands
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
    }
}
