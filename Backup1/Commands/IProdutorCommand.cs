namespace Imunizacao.Domain.Commands
{
    public interface IProdutorCommand
    {
        string GetAll { get; }
        string GetNewId { get; }
        string Insert { get; }
    }
}
