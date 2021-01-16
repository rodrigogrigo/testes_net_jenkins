namespace RgCidadao.Domain.Commands.Cadastro
{
    public interface IEquipeCommand
    {
        string UsaEstruturaNova { get; }
        string GetEquipeByCidadaoEstruturaVelha { get; }
        string GetEquipeByCidadaoEstruturaNova { get; }
        string GetEquipesByBairro { get; }
        string GetEquipeByProfissional { get; }
        string GetEquipeByUnidade { get; }

        string GetEquipeByPerfil { get; }
    }
}
