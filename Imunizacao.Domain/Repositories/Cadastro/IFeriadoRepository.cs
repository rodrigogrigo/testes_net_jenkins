using RgCidadao.Domain.Entities.Cadastro;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Repositories.Cadastro
{
    public interface IFeriadoRepository
    {
        List<Feriado> GetAllPagination(string ibge, int page, int pagesize, string filtro);
        List<Feriado> GetAll(string ibge);
        int GetCountAll(string ibge, string filtro);
        Feriado GetFeriadoById(string ibge, DateTime? data);
        void Inserir(string ibge, Feriado model);
        void Atualizar(string ibge, Feriado model);
        void Deletar(string ibge, DateTime? data);
    }
}
