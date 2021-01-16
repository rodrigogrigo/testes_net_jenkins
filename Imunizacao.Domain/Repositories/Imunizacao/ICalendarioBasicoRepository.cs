using RgCidadao.Domain.Entities.Imunizacao;
using System.Collections.Generic;

namespace RgCidadao.Domain.Repositories.Imunizacao
{
    public interface ICalendarioBasicoRepository
    {
        List<CalendarioBasico> GetAllPagination(string ibge, int page, int pagesize, string filtro, int? publico_alvo);
        int GetCountAll(string ibge, string filtro, int? publico_alvo);
        int GetNewId(string ibge);
        void Insert(string ibge, CalendarioBasico model);
        CalendarioBasico GetById(string ibge, int id);
        void Update(string ibge, CalendarioBasico model);
        void Delete(string ibge, int id);
        void UpdateInativo(string ibge, int id);
        List<CalendarioBasico> GetCalendarioByProduto(string ibge, int idproduto);
    }
}
