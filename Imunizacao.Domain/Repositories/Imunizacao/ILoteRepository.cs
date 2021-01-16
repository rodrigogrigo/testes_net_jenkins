
using RgCidadao.Domain.Entities.Imunizacao;
using RgCidadao.Domain.ViewModels.Imunizacao;
using System.Collections.Generic;

namespace RgCidadao.Domain.Repositories.Imunizacao
{
    public interface ILoteRepository
    {
        List<LoteImunobiologico> GetLoteByImunobiologico(string ibge, int produto, int unidade);
        List<LoteImunobiologico> GetLoteEstoqueByImunobiologico(string ibge, string filtro, int unidade);
        int GetNewId(string ibge);
        LoteImunobiologico GetLoteById(string ibge, int id);
        void Insert(string ibge, LoteImunobiologico model);
        void Editar(string ibge, LoteImunobiologico model);
        //void AtualizarSituacaoLote(string ibge, int idLote, int situacao);
        List<LoteImunobiologico> GetLoteByUnidade(string ibge, int unidade, int produto);
        MovLoteViewModel GetPrimeiroMovimentoLote(string ibge, int? id_produto, int? id_unidade, string lote);
        LoteImunobiologico GetLoteByLote(string ibge, string lote, int produtor, int produto);
        List<LoteImunobiologico> GetLoteByProdutor(string ibge, int produtor);

        void AdicionaBloqueioUnidadeLote(string ibge, int unidade, int lote);
        void RemoveBloqueioUnidadeLote(string ibge, int unidade, int lote);
    }
}
