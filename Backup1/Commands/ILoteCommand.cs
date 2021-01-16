namespace Imunizacao.Domain.Commands
{
    public interface ILoteCommand
    {
        string GetLoteByImunobiologico { get; }
        string GetLoteEstoqueByImunobiologico { get; }
        string GetLoteByUnidade { get; }
        string GetNewId { get; }
        string Insert { get; }
        string AtualizaSituacaoLote { get; }
    }
}
