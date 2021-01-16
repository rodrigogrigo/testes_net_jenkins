using Dapper;
using Imunizacao.Domain.Commands;
using Imunizacao.Domain.Entities;
using Imunizacao.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Imunizacao.Domain.Infra.Repositories
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
                    lista = Helpers.HelperConnection.ExecuteCommand<List<Cidade>>(ibge, conn =>
                     conn.Query<Cidade>(_cidadecommand.GetAllPagination.Replace("@filtro", ""), new
                     {
                         @pagesize = pagesize,
                         @page = page
                     }).ToList());
                }
                else
                {
                    lista = Helpers.HelperConnection.ExecuteCommand<List<Cidade>>(ibge, conn =>
                    conn.Query<Cidade>(_cidadecommand.GetAllPagination.Replace("@filtro", filtro), new
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
    }
}
