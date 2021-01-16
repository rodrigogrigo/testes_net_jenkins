namespace RgCidadao.Domain.Commands.Imunizacao
{
    public interface IProfissaoCommand
    {
        string GetAll { get; }
        string GetCountAll { get; }
        string GetAllPagination { get; }
    }
}
