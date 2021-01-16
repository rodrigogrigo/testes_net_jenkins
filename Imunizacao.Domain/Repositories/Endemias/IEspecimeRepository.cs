using RgCidadao.Domain.Entities.Endemias;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Repositories.Endemias
{
    public interface IEspecimeRepository
    {
        List<Especime> GetAllPagination(string ibge, int page, int pagesize, string filtro);
        Especime GetEspecimeById(string ibge, int id);
        int GetCountAll(string ibge, string filtro);
        void Insert(string ibge, Especime model);
        void Update(string ibge, Especime model);
        void Delete(string ibge, int id);
        int GetEspecimeNewId(string ibge);
        List<Especime> GetAllEspecime(string ibge);
    }
}
