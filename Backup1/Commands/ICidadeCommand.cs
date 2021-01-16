namespace Imunizacao.Domain.Commands
{
    public interface ICidadeCommand
    {
        string GetAll { get; }
        string GetCountAll { get; }
        string GetAllPagination { get; }
    }
}
