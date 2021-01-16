namespace Imunizacao.Domain.Commands
{
    public interface IUnidadeCommand
    {
        string GetAll { get; }
        string GetUnidadesByUser { get; }
    }
}
