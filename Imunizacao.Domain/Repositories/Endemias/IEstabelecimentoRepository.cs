using RgCidadao.Domain.Entities.Endemias;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Repositories.Endemias
{
    public interface IEstabelecimentoRepository
    {
        Estabelecimento GetEstabelecimentoById(string ibge, int id);
        int GetCountAll(string ibge, string filtro);
        List<Estabelecimento> GetAllPagination(string ibge, string filtro, int page, int pagesize);
        List<Estabelecimento> GetAll(string ibge);
        int GetNewId(string ibge);
        void Insert(string ibge, Estabelecimento model);
        void Update(string ibge, Estabelecimento model);
        void Delete(string ibge, int id);
        List<Estabelecimento> GetEstabelecimentoByCiclo(string ibge, int id_ciclo);
    }
}
