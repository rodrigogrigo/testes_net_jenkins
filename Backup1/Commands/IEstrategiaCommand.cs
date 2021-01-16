namespace Imunizacao.Domain.Commands
{
    public interface IEstrategiaCommand
    {
        string GetEstrategiaByProduto { get; }
        string GetAll { get; }
    }
}
