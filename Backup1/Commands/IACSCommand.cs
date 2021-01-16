namespace Imunizacao.Domain.Commands
{
    public interface IACSCommand
    {
        string GetAll { get; }
        string GetAllPagination { get; }
        string GetCountAll { get; }
    }
}
