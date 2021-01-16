namespace RgCidadao.Domain.Commands.Imunizacao
{
    public interface ILoteCommand
    {
        string GetLoteByImunobiologico { get; }
        string GetLoteEstoqueByImunobiologico { get; }
        string GetLoteByUnidade { get; }
        string GetLoteById { get; }
        string GetLoteByLote { get; }
        string GetNewId { get; }
        string Insert { get; }
        string Editar { get; }
        //string AtualizaSituacaoLote { get; }
        string GetPrimeiroMovimentoLote { get; }
        string GetLoteByProdutor { get; }

        //adiciona e remove bloqueio
        string AdicionaBloqueioUnidadeLote { get; }
        string RemoveBloqueioUnidadeLote { get; }
    }
}
