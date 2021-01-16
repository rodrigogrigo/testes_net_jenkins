namespace RgCidadao.Domain.Commands.Imunizacao
{
    public interface IMovImunobiologicoCommand
    {
        string GetMovimentoByUnidade { get; }
        string GetCountAll { get; }
        string GetById { get; }
        string Inserir { get; }
        string GetNewId { get; }
        string Atualizar { get; }
        string Excluir { get; }
        string GetMovimentoByLote { get; }

        string GetMovRetiradaEstoqueByLote { get; }
        string GetMovimentoSaidaLote { get; }

        string ExcluirMovimentoProdutoById { get; }
        string GetDataMovimentoProdutoByOrigemAndId { get; }
        string GetExisteMovimentoProdutoAposEntrada { get; }


    }
}
