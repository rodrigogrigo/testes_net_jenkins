using RgCidadao.Domain.Entities.Cadastro;
using RgCidadao.Domain.Entities.Imunizacao;
using RgCidadao.Domain.ViewModels.Cadastros;
using System;
using System.Collections.Generic;

namespace RgCidadao.Domain.Repositories.Cadastro
{
    public interface IIndividuoRepository
    {
        int GetNewId(string ibge);
        Individuo GetById(string ibge, int id);
        void Insert(string ibge, Individuo model);
        void Update(string ibge, Individuo model);
        dynamic GetUltimoLog(string ibge, int id_individuo);
        Tuple<bool, int?, string> CheckIndividuoExiste(string ibge, string filtro);
        List<Individuo> CheckIndividuoMesmoNome(string ibge, string filtro);
        bool CheckVinculoMicroarea(string ibge, int id, string sql_estrutura);
    }
}
