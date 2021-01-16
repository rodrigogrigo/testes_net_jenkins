using RgCidadao.Domain.Entities.AtencaoBasica;
using RgCidadao.Domain.ViewModels.AtencaoBasica;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Repositories.AtencaoBasica
{
    public interface IConsumoAlimentarRepository
    {
        List<ConsumoAlimentarViewModel> GetAllPagination(string ibge, int page, int pagesize, string filtro);
        int GetCountAll(string ibge, string filtro);

        int GetNewId(string ibge);
        void Insert(string ibge, ConsumoAlimentar model);
        void Update(string ibge, ConsumoAlimentar model);
        ConsumoAlimentar GetConsumoAlimentarById(string ibge, int id);
        void Delete(string ibge, int id);
    }
}
