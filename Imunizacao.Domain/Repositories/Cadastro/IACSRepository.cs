using RgCidadao.Domain.Entities.Cadastro;
using System.Collections.Generic;

namespace RgCidadao.Domain.Repositories.Cadastro
{
    public interface IACSRepository
    {
        List<ACS> GetAll(string ibge);
        List<ACS> GetAllPagination(string ibge, string filtro, int page, int pagesize);
        int GetCountAll(string ibge, string filtro);
        List<ACS> GetAcsByEquipe(string ibge, int id);
    }
}
