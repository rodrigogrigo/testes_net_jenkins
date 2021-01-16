using RgCidadao.Domain.Entities.AtencaoBasica;
using RgCidadao.Domain.ViewModels.AtencaoBasica;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Repositories.AtencaoBasica
{
    public interface IVisitaDomiciliarRepository
    {
        List<VisitaDomiciliarViewModel> GetAllPagination(string ibge, int? page, int? pagesize, string filtro);
                
        int GetCountAll(string ibge, string filtro);

        VisitaDomiciliar GetById(string ibge, int Id);

        void Insert(string ibge, VisitaDomiciliar model);
        void Update(string ibge, VisitaDomiciliar model);
        void Delete(string ibge, int? Id);

        int GetNewId(string ibge);

    }
}
