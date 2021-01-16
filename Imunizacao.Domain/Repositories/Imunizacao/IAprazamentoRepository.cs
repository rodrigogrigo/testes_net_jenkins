using RgCidadao.Domain.Entities.Imunizacao;
using System.Collections.Generic;

namespace RgCidadao.Domain.Repositories.Imunizacao
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

        void GeraAprazamentoPopGeralByIndividuo(string ibge, int id);
        void GeraAprazamentoFemininoByIndividuo(string ibge, int id);
        void GeraAprazamentoMasculinoByIndividuo(string ibge, int id);
        void GeraAprazamentoDeficienciaByIndividuo(string ibge, int id);
        void GeraAprazamentoGestacaoByIndividuo(string ibge, int id);
        void GeraAprazamentoPuerperaByIndividuo(string ibge, int id);
        void GeraAprazamentoCalendarioBasico(string ibge, int id_calendario, int publico_alvo);
        void GeraAprazamento(string ibge);
    }
}
