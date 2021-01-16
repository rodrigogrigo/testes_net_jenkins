using RgCidadao.Domain.Entities.Cadastro;
using RgCidadao.Domain.ViewModels.Cadastros;
using System.Collections.Generic;

namespace RgCidadao.Domain.Repositories.Cadastro
{
    public interface IUnidadeRepository
    {
        List<Unidade> GetAll(string ibge, string filtro);
        List<Unidade> GetUnidadesByUser(string ibge, int user);
        List<LocalAtendimentoViewModel> GetLocaisAtendimentoByUnidade(string ibge, int unidade);

    }
}
