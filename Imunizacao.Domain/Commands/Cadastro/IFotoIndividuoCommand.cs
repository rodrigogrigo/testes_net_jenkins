namespace RgCidadao.Domain.Commands.Cadastro
{
    public interface IFotoIndividuoCommand
    {
        string GetNewId { get; }
        string UpdateOrInsertByIdIndividuo { get; }
        string GetByIdIndividuo { get; }
    }
}
