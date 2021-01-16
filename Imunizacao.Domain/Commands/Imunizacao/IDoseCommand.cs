namespace RgCidadao.Domain.Commands.Imunizacao
{
    public interface IDoseCommand
    {
        string GetAll { get; }
        string GetDoseByEstrategiaAndProduto { get; }
    }
}
