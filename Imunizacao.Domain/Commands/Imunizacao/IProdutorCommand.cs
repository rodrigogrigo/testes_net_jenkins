namespace RgCidadao.Domain.Commands.Imunizacao
{
    public interface IProdutorCommand
    {
        string GetAll { get; }
        string GetCountAll { get; }
        string GetAllPagination { get; }
        string GetNewId { get; }
        string Insert { get; }
        string GetById { get; }
        string Update { get; }
        string Delete { get; }
    }
}
