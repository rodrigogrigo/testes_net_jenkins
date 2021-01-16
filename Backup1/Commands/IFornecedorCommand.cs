namespace Imunizacao.Domain.Commands
{
    public interface IFornecedorCommand
    {
        string GetAll { get; }
        string GetAllPagination { get; }
        string GetCountAll { get; }
        string GetById { get; }
        string GetNewId { get; }
        string Insert { get; }
        string Update { get; }
        string Delete { get; }
    }
}
