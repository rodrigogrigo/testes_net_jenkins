using Dapper;
using RgCidadao.Domain.Commands.Cadastro;
using RgCidadao.Domain.Entities.Cadastro;
using RgCidadao.Domain.Repositories.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RgCidadao.Domain.Infra.Repositories.Cadastro
{
    public class CidadeRepository : ICidadeRepository
    {
        private readonly ICidadeCommand _cidadecommand;

        public CidadeRepository(ICidadeCommand commandText)
        {
            _cidadecommand = commandText;
        }

        public List<Cidade> GetAll(string ibge)
        {
            try
            {
                var lista = Helpers.HelperConnection.ExecuteCommand<List<Cidade>>(ibge, conn =>
                     conn.Query<Cidade>(_cidadecommand.GetAll).ToList());

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Cidade> GetAllPagination(string ibge, int page, int pagesize, string filtro)
        {
            try
            {
                var lista = new List<Cidade>();
                if (string.IsNullOrWhiteSpace(filtro))
                {
                    var sql = _cidadecommand.GetAllPagination.Replace("@filtro", "");
                    lista = Helpers.HelperConnection.ExecuteCommand<List<Cidade>>(ibge, conn =>
                     conn.Query<Cidade>(sql, new
                     {
                         @pagesize = pagesize,
                         @page = page
                     }).ToList());
                }
                else
                {
                    var sql = _cidadecommand.GetAllPagination.Replace("@filtro", filtro);
                    lista = Helpers.HelperConnection.ExecuteCommand<List<Cidade>>(ibge, conn =>
                    conn.Query<Cidade>(sql, new
                    {
                        @pagesize = pagesize,
                        @page = page
                    }).ToList());
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Cidade GetCidadeById(string ibge, int id)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                    conn.QueryFirstOrDefault<Cidade>(_cidadecommand.GetCidadeById, new
                    {
                        @id = id
                    }));

                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetCountAll(string ibge, string filtro)
        {
            try
            {
                int count = 0;
                if (string.IsNullOrWhiteSpace(filtro))
                {
                    count = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                     conn.QueryFirstOrDefault<int>(_cidadecommand.GetCountAll.Replace("@filtro", "")));
                }
                else
                {
                    count = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                    conn.QueryFirstOrDefault<int>(_cidadecommand.GetCountAll.Replace("@filtro", filtro)));
                }
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Cidade GetCidadeByIBGE(string ibge, string codigo_ibge)
        {
            try
            {
                var str = $@" '%{codigo_ibge}%' ";
                var sql = _cidadecommand.GetCidadeByIBGE.Replace("@ibge", str);

                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                    conn.QueryFirstOrDefault<Cidade>(sql));

                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
