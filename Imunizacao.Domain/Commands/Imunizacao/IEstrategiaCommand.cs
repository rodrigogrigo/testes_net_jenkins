namespace RgCidadao.Domain.Commands.Imunizacao
{
    public interface IEstrategiaCommand
    {
        string GetEstrategiaByProduto { get; }
        string GetAll { get; }
    }
}
