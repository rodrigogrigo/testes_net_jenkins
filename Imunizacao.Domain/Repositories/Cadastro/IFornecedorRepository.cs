using RgCidadao.Domain.Entities.Cadastro;
using RgCidadao.Domain.ViewModels.AtencaoBasica;
using RgCidadao.Domain.ViewModels.Imunizacao;
using System;
using System.Collections.Generic;

namespace RgCidadao.Domain.Repositories.Cadastro
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
        bool ExisteCPFCNPJ(string ibge, string cpfcnpj);
        List<PrestadorAgendaViewModel> GetPrestadoresVigencia(string ibge, int codmed, int coduni, string data_ini, string data_fim);
    }
}
