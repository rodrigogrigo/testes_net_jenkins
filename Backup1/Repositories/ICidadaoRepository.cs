using Imunizacao.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Imunizacao.Domain.Repositories
{
    public interface ICidadaoRepository
    {
        List<Cidadao> GetAll(string ibge, string filtro);
        int GetCountAll(string ibge, string filtro);
        List<Cidadao> GetAllPagination(string ibge, string filtro, int page, int pagesize);
        Cidadao GetCidadaoById(string ibge, int id);
        int GetNewId(string ibge);
        Tuple<bool, int?, string> ValidaExistenciaCidadao(string ibge, string filtro);
        void Insert(Cidadao model, string ibge);
        void Update(Cidadao model, string ibge);
        void Excluir(string ibge, int id);
        byte[] GetFotoByCidadao(string ibge, int id);
        List<GrauParentesco> GetGrauParentesco(string ibge);
        void InsertCadIndividual(string ibge, Cidadao model);
        void UpdateCadIndividual(string ibge, Cidadao model);

        //Login CadSus
        Tuple<string, string> GetLoginCadSus(string ibge);
    }
}
