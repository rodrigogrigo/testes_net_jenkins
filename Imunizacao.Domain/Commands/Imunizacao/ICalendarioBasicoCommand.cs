namespace RgCidadao.Domain.Commands.Imunizacao
{
    public interface ICalendarioBasicoCommand
    {
        string GetAll { get; }
        string GetCountAll { get; }
        string GetNewId { get; }
        string Insert { get; }
        string GetById { get; }
        string Update { get; }
        string Delete { get; }
        string UpdateInativo { get; }
        string GetCalendarioBasicoByProduto { get; }
    }
}
