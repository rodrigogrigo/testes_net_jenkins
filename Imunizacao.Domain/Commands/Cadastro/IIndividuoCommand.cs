namespace RgCidadao.Domain.Commands.Cadastro
{
    public interface IIndividuoCommand
    {
        string GetNewId { get; }
        string Insert { get; }
        string Update { get;  }
        string GetById { get; }
        string GetUltimoLog { get; }
        string CheckIndividuoExiste { get; }
        string CheckIndividuoMesmoNome { get; }
        string CheckVinculoMicroarea { get; }
    }
}
