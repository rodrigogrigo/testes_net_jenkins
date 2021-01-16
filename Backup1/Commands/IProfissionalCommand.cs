namespace Imunizacao.Domain.Commands
{
    public interface IProfissionalCommand
    {
        string GetAll { get; }
        string GetProfissionalByUnidade { get; }
        string GetListaCBO { get; }
        string GetProfissionalCboByUnidade { get; }
    }
}
