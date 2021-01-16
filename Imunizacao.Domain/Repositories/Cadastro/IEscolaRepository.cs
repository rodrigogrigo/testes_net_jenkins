using RgCidadao.Domain.Entities.Cadastro;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Repositories.Cadastro
{
    public interface IEscolaRepository
    {
        List<Escola> GetAll(string ibge);
        List<Escola> GetAllPagination(string ibge, int page, int pagesize, string filtro);
        int GetCountAll(string ibge, string filtro);
        Escola GetEscolaById(string ibge, int id);
        int GetNewId(string ibge);
        void Insert(string ibge, Escola model);
        void Update(string ibge, Escola model);
        void Delete(string ibge, int id);
    }
}
