using Imunizacao.Domain.Entities;
using System.Collections.Generic;

namespace Imunizacao.Domain.Repositories
{
    public interface ILoteRepository
    {
        List<LoteImunobiologico> GetLoteByImunobiologico(string ibge, int produto);
        List<LoteImunobiologico> GetLoteEstoqueByImunobiologico(string ibge, string filtro);
        int GetNewId(string ibge);
        void Insert(string ibge, LoteImunobiologico model);
        void AtualizarSituacaoLote(string ibge, int idLote, int situacao);
        List<LoteImunobiologico> GetLoteByUnidade(string ibge, int unidade, int produto);
    }
}
