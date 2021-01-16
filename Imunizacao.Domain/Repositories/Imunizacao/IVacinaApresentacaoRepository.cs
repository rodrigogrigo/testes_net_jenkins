using RgCidadao.Domain.Entities.Imunizacao;
using System.Collections.Generic;

namespace RgCidadao.Domain.Repositories.Imunizacao
{
    public interface IVacinaApresentacaoRepository
    {
        List<VacinaApresentacao> GetAll(string ibge, string filtro);
        VacinaApresentacao GetById(string ibge, int id);
        int GetId(string ibge);
        void InserirVacinaApresentacao(string ibge, int id, string descricao, int quantidade);
        void AtualizarVacinaApresentacao(string ibge, int id, string descricao, int quantidade);
        void ExcluirVacinaApresentacao(string ibge, int id);
    }
}
