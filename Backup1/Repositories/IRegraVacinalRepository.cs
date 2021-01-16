using Imunizacao.Domain.Entities;

namespace Imunizacao.Domain.Repositories
{
    public interface IRegraVacinalRepository
    {
        RegraVacinal GetRegraVacinalByParams(string ibge, int id_imunobiologico, int id_estrategia, int id_dose);
    }
}
