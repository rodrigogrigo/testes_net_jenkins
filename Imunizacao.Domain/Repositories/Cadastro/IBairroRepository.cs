using RgCidadao.Domain.Entities.Cadastro;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Repositories.Cadastro
{
    public interface IBairroRepository
    {
        int GetCountAll(string ibge, string filtro);
        List<Bairro> GetAllPagination(string ibge, string filtro, int page, int pagesize);
        Bairro GetById(string ibge, int id);
        List<Bairro> GetAll(string ibge);
        List<Bairro> GetBairroByIbge(string ibge, string codibge);
    }
}
