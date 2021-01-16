using Imunizacao.Domain.Entities;
using System.Collections.Generic;

namespace Imunizacao.Domain.Repositories
{
    public interface IMovImunobiologicoRepository
    {
        List<MovimentoImunobiologico> GetMovimentoByUnidade(string ibge, int unidade, string filtro, int page, int pagesize);
        MovimentoImunobiologico GetMovimentoById(string ibge, int id);
        int GetNewId(string ibge);
        void Inserir(string ibge, MovimentoImunobiologico model);
        void Atualizar(string ibge, MovimentoImunobiologico model);
        void Excluir(string ibge, int id);
        int GetCountAll(string ibge, string filtro, int unidade);
    }
}
