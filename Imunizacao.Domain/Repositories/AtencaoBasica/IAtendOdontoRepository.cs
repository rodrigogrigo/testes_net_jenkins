using RgCidadao.Domain.Entities.AtencaoBasica;
using RgCidadao.Domain.ViewModels.AtencaoBasica;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Repositories.AtencaoBasica
{
    public interface IAtendOdontoRepository
    {
        List<AtendOdontoViewModel> GetAllPagination(string ibge, int page, int pagesize, string filtro);
        int GetCountAll(string ibge, string filtro);
        AtendOdontoIndividual GetAtendOdontoById(string ibge, int id);
        void UpdateOrInsert(string ibge, AtendOdontoIndividual model);
        int GetNewId(string ibge);
        int GetNewIdItem(string ibge);
        void ExcluirItemPai(string ibge, int id);
        void ExcluirItensByPai(string ibge, int id);
        void ExcluirItemById(string ibge, int id);
        AtendOdontoIndividualItem GetAtendOdontoItemById(string ibge, int id);
        List<AtendOdontoIndividualItem> GetAtendOdontoItensByPai(string ibge, int id);
        List<AtendOdontoProcedimentoViewModel> GetProcOdontoIndividualizado(string ibge, string filtros, string cbo, int page, int pagesize);
        int GetCountProcOdontoIndividualizado(string ibge, string filtros, string cbo);
    }
}
