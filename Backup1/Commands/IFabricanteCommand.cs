namespace Imunizacao.Domain.Commands
{
    public interface IFabricanteCommand
    {
        string GetAll { get; }
        string GetNewId { get; }
        string Inserir { get; }
        string Atualizar { get; }
        string Deletar { get; }
        string GetProdutorByImunobiologico { get; }
    }
}
