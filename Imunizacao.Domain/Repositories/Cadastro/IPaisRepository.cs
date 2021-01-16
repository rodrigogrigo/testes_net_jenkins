using RgCidadao.Domain.Entities.Cadastro;
using System.Collections.Generic;

namespace RgCidadao.Domain.Repositories.Cadastro
{
    public interface IPaisRepository
    {
        List<Pais> GetAll(string ibge);
    }
}
