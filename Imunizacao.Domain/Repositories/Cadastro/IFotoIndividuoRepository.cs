using RgCidadao.Domain.Entities.Cadastro;

namespace RgCidadao.Domain.Repositories.Cadastro
{
    public interface IFotoIndividuoRepository
    {
        int GetNewId(string ibge);
        void UpdateOrInsertByIdIndividuo(string ibge, FotoIndividuo model);
        FotoIndividuo GetByIdIndividuo(string ibge, int id_cidadao);
    }
}
