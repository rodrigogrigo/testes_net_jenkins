namespace Imunizacao.Domain.Commands
{
    public interface IEquipeCommand
    {
        string UsaEstruturaNova { get; }
        string GetEquipeByCidadaoEstruturaVelha { get; }
        string GetEquipeByCidadaoEstruturaNova { get; }
    }
}
