using Imunizacao.Domain.Entities;

namespace Imunizacao.Domain.Repositories
{
    public interface IEquipeRepository
    {
        bool UsaEstruturaNova(string ibge);
        Equipe GetEquipeByCidadaoEstruturaVelha(string ibge, int paciente);
        Equipe GetEquipeByCidadaoEstruturaNova(string ibge, int paciente);
    }
}
