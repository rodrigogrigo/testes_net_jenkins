namespace RgCidadao.Domain.Commands.Imunizacao
{
    public interface ICartaoVacinaCommand
    {
        string GetNewId { get; }
        string GetCartaoVacinaById { get; }
        string Insert { get; }
        string Update { get; }
        string Delete { get; }
        string GetCartaoVacinaByProduto { get; }
        string GetCartaoVacinaByProdutor { get; }
    }
}
