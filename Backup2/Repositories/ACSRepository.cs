using Dapper;
using Imunizacao.Domain.Commands;
using Imunizacao.Domain.Entities;
using Imunizacao.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Imunizacao.Domain.Infra.Repositories
{
    public class ACSRepository : IACSRepository
    {
        public IACSCommand _command;
        public ACSRepository(IACSCommand command)
        {
            _command = command;
        }

        public List<ACS> GetAll(string ibge)
        {
            try
            {
                var lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                            conn.Query<ACS>(_command.GetAll)).ToList();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ACS> GetAllPagination(string ibge, string filtro, int page, int pagesize)
        {
            try
            {
                var lista = new List<ACS>();
                if (string.IsNullOrWhiteSpace(filtro))
                {
                    lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Query<ACS>(_command.GetAllPagination.Replace("@filtro", ""), new
                           {
                               @pagesize = pagesize,
                               @page = page
                           })).ToList();
                }
                else
                {
                    lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.Query<ACS>(_command.GetAllPagination.Replace("@filtro", filtro), new
                          {
                              @pagesize = pagesize,
                              @page = page
                          })).ToList();
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
                            conn.QueryFirstOrDefault<int>(_command.GetCountAll.Replace("@filtro", "")));
                }
                else
                {
                    count = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                            conn.QueryFirstOrDefault<int>(_command.GetCountAll.Replace("@filtro", filtro)));
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
