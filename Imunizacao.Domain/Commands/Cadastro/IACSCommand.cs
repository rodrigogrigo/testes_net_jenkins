namespace RgCidadao.Domain.Commands.Cadastro
{
    public interface IACSCommand
    {
        string GetAll { get; }
        string GetAllPagination { get; }
        string GetCountAll { get; }
        string GetAcsByEquipe { get; }
    }
}
