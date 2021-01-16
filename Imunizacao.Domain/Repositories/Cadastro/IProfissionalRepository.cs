using RgCidadao.Domain.Entities.Cadastro;
using RgCidadao.Domain.ViewModels.Cadastros;
using RgCidadao.Domain.Entities.Imunizacao;
using System.Collections.Generic;

namespace RgCidadao.Domain.Repositories.Cadastro
{
    public interface IProfissionalRepository
    {
        List<ProfissionalViewModel> GetAll(string ibge, string filtro);
        List<ProfissionalViewModel> GetProfissionalByUnidade(string ibge, int unidade);
        List<ProfissionalViewModel> GetProfissionalAtivoByUnidade(string ibge, int unidade);
        List<CBO> GetCboProfissional(string ibge, int profissional);
        List<ProfissionalViewModel> GetProfissionalCBOByUnidade(string ibge, int unidade);
        List<ProfissionalViewModel> GetProfissionalByUnidades(string ibge, string unidade);
        List<ProfissionalViewModel> GetProfissionalByEquipe(string ibge, string id);
        int GetCountAll(string ibge, string filtro);
        List<ProfissionalViewModel> GetAllPagination(string ibge, int page, int pagesize, string filtro);
        List<ProfissionalViewModel> GetProfissionalByIdAndUnidade(string ibge, int unidade, int profissional);

        List<ProfissionalViewModel> GetAllPaginationProfissionalWithCBO(string ibge, int page, int pagesize, string filtro);
        int GetCountAllProfissionalWithCBO(string ibge, string filtro);

        List<ProfissionalViewModel> GetProfissionalByUnidadeWithCBO(string ibge, int unidade);

        Profissional GetProfissionalById(string ibge, int profissional);
        List<ACSViewModel> GetACSByEstabelecimentoSaude(string ibge, int id_estabelecimento_saude);
        List<ProfissionalViewModel> GetCBOByMedicoUnidade(string ibge, int codmed, int coduni, string cbo);
    }
}
