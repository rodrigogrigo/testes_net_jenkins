namespace Imunizacao.Domain.Commands
{
    public interface IDoseCommand
    {
        string GetAll { get; }
        string GetDoseByEstrategiaAndProduto { get; }
    }
}
