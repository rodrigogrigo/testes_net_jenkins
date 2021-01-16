namespace Imunizacao.Domain.Commands
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
    }
}
