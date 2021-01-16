using RgCidadao.Domain.Entities.Imunizacao;

namespace RgCidadao.Domain.Repositories.Imunizacao
{
    public interface IRegraVacinalRepository
    {
        RegraVacinal GetRegraVacinalByParams(string ibge, int id_imunobiologico, int id_estrategia, int id_dose);
    }
}
