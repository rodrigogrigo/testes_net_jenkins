using Imunizacao.Domain.Entities;
using System.Collections.Generic;

namespace Imunizacao.Domain.Repositories
{
    public interface IGestacaoRepository
    {
        Gestacao IsGestante(string ibge, int id);
        List<Gestacao> GetGestacaoByCidadao(string ibge, int id);
        List<Gestacao_Item> GetGestacaoItensByGestacao(string ibge, int id);
        Gestacao GetUltimaGestacao(string ibge, int id);
    }
}
