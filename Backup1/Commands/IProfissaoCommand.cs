namespace Imunizacao.Domain.Commands
{
    public interface IProfissaoCommand
    {
        string GetAll { get; }
        string GetCountAll { get; }
        string GetAllPagination { get; }
    }
}
