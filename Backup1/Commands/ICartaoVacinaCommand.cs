namespace Imunizacao.Domain.Commands
{
    public interface ICartaoVacinaCommand
    {
        string GetNewId { get; }
        string GetCartaoVacinaById { get; }
        string Insert { get; }
        string Update { get; }
        string Delete { get; }
        string CancelarCartaoVacina { get; }
    }
}
