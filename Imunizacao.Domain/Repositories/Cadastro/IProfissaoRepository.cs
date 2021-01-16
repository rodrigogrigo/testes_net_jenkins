using RgCidadao.Domain.Entities.Cadastro;
using System.Collections.Generic;

namespace RgCidadao.Domain.Repositories.Cadastro
{
    public interface IProfissaoRepository
    {
        List<Profissao> GetAll(string ibge);
        List<Profissao> GetAllPagination(string ibge, int page, int pagesize, string filtro);
        int GetCountAll(string ibge, string filtro);
    }
}
