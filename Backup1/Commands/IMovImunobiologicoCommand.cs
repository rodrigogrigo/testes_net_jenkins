namespace Imunizacao.Domain.Commands
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
    }
}
