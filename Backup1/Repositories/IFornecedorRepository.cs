using Imunizacao.Domain.Entities;
using Imunizacao.Domain.ViewModels;
using System;
using System.Collections.Generic;

namespace Imunizacao.Domain.Repositories
{
    public interface IFornecedorRepository
    {
        List<Fornecedor> GetAllPagination(string ibge, string filtro, int page, int pagesize);
        List<Fornecedor> GetAll(string ibge);
        int GetCountAll(string ibge, string filtro);
        Fornecedor GetById(string ibge, int id);
        int GetNewId(string ibge);
        void Insert(FornecedorViewModel model, string ibge);
        void Update(FornecedorViewModel model, string ibge);
        void Delete(DateTime csi_dataexc, int csi_codfor, string ibge);
    }
}
