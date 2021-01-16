using Imunizacao.Domain.Entities;
using System.Collections.Generic;

namespace Imunizacao.Domain.Repositories
{
    public interface ICalendarioBasicoRepository
    {
        List<CalendarioBasico> GetAllPagination(string ibge, int page, int pagesize, string filtro);
        int GetCountAll(string ibge, string filtro);
        int GetNewId(string ibge);
        void Insert(string ibge, CalendarioBasico model);
        CalendarioBasico GetById(string ibge, int id);
        void Update(string ibge, CalendarioBasico model);
        void Delete(string ibge, int id);
        void UpdateInativo(string ibge, int id);
    }
}
