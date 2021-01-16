using RgCidadao.Domain.Entities.Cadastro;
using System.Collections.Generic;

namespace RgCidadao.Domain.Repositories.Cadastro
{
    public interface ICidadeRepository
    {
        List<Cidade> GetAll(string ibge);
        int GetCountAll(string ibge, string filtro);
        List<Cidade> GetAllPagination(string ibge, int page, int pagesize, string filtro);
        Cidade GetCidadeById(string ibge, int id);
        Cidade GetCidadeByIBGE(string ibge, string codigo_ibge);
    }
}
