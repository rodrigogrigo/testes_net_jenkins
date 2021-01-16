using RgCidadao.Domain.Entities.Cadastro;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Repositories.Cadastro
{
    public interface ILogradouroRepository
    {
        int GetCountAll(string ibge, string filtro);
        List<Logradouro> GetAllPagination(string ibge, int page, int pagesize, string filtro);
        Logradouro GetLogradouroById(string ibge, int id);
        List<Logradouro> GetLogradouroByBairro(string ibge, int idbairro);
    }
}
