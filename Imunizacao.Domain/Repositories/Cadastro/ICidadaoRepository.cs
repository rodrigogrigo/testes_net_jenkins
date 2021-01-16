using RgCidadao.Domain.Entities.Cadastro;
using RgCidadao.Domain.Entities.Imunizacao;
using RgCidadao.Domain.ViewModels.Cadastros;
using System;
using System.Collections.Generic;

namespace RgCidadao.Domain.Repositories.Cadastro
{
    public interface ICidadaoRepository
    {
        List<Cidadao> GetAll(string ibge, string filtro);
        int GetCountAll(string ibge, string filtro);
        List<Cidadao> GetAllPagination(string ibge, string filtro, int page, int pagesize, string sql_estrutura);
        Cidadao GetCidadaoById(string ibge, int id, string sql_estrutura);
        int GetNewId(string ibge);
        Tuple<bool, int?, string> ValidaExistenciaCidadao(string ibge, string filtro);
        List<Cidadao> ValidaExistenciaCidadaoParam(string ibge, string filtro);
        void Insert(Cidadao model, string ibge);
        void Update(Cidadao model, string ibge);
        void Excluir(string ibge, int id);
        byte[] GetFotoByCidadao(string ibge, int id);
        List<GrauParentesco> GetGrauParentesco(string ibge);
        void InsertCadIndividual(string ibge, Cidadao model);
        void UpdateCadIndividual(string ibge, Cidadao model);


        //Login CadSus
        Tuple<string, string> GetLoginCadSus(string ibge);


        List<CidadaoFamiliaViewModel> GetAllPaginationWithFamilia(string ibge, int page, int pagesize, string filtro);
        int GetCountAllWithFamilia(string ibge, string filtro);

        List<CidadaoFamiliaViewModel> GetIndividuosToFamilia(string ibge, int page, int pagesize, string filtro);
        int GetCountIndividuosToFamilia(string ibge, string filtro);

        List<CidadaoViewModel> GetCidadaoByIdProntuarioIdAgente(string ibge, int? id_prontuario, int id_agente);

        bool VerificaExisteEsusFamilia(string IBGE);
    }
}
