using RgCidadao.Domain.Entities.Cadastro;
using System.Collections.Generic;

namespace RgCidadao.Domain.Repositories.Cadastro
{
    public interface IEquipeRepository
    {
        bool UsaEstruturaNova(string ibge);
        Equipe GetEquipeByCidadaoEstruturaVelha(string ibge, int paciente);
        Equipe GetEquipeByCidadaoEstruturaNova(string ibge, int paciente);
        List<Equipe> GetEquipeByBairro(string ibge, int id_equipe);
        List<Equipe> GetEquipeByProfissional(string ibge, int profissional);
        List<Equipe> GetEquipeByUnidade(string ibge, int unidade);
        List<Equipe> GetEquipeByPerfil(string ibge, string filtro);
    }
}
