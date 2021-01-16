namespace RgCidadao.Domain.Commands.Cadastro
{
    public interface ICidadeCommand
    {
        string GetAll { get; }
        string GetCountAll { get; }
        string GetAllPagination { get; }
        string GetCidadeById { get; }
        string GetCidadeByIBGE { get; }
    }
}
