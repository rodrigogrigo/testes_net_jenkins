namespace RgCidadao.Domain.Commands.Cadastro
{
    public interface IProfissaoCommand
    {
        string GetAll { get; }
        string GetCountAll { get; }
        string GetAllPagination { get; }
    }
}
