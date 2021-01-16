using Imunizacao.Domain.Entities;
using System.Collections.Generic;

namespace Imunizacao.Domain.Repositories
{
    public interface IAprazamentoRepository
    {
        List<Aprazamento> GetAprazamentoByCidadao(string ibge, int id);
        List<Aprazamento> GetAprazamentoByCalendarioBasico(string ibge, int id);
        void Insert(string ibge, Aprazamento model);
        void UpdateVacinados(string ibge, int? id_vacinados, int id);
        int GetNewId(string ibge);
        bool PermiteExcluirAprazamento(string ibge, int id);
        void Delete(string ibge, int id);
        void GeraAprazamento(string ibge, int id, int publicoAlvo);
    }
}
