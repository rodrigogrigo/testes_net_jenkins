using Dapper;
using RgCidadao.Domain.Commands.Cadastro;
using RgCidadao.Domain.Entities.Cadastro;
using RgCidadao.Domain.Repositories.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RgCidadao.Domain.Infra.Repositories.Cadastro
{
    public class LogradouroRepository : ILogradouroRepository
    {
        private ILogradouroCommand _command;
        public LogradouroRepository(ILogradouroCommand command)
        {
            _command = command;
        }

        public List<Logradouro> GetAllPagination(string ibge, int page, int pagesize, string filtro)
        {
            try
            {
                string sql = _command.GetAllPagination;
                if (!string.IsNullOrWhiteSpace(filtro))
                    sql = sql.Replace("@filtros", filtro);
                else
                    sql = sql.Replace("@filtros", string.Empty);

                var itens = Helpers.HelperConnection.ExecuteCommand<List<Logradouro>>(ibge, conn =>
                         conn.Query<Logradouro>(sql, new
                         {
                             @pagesize = pagesize,
                             @page = page
                         }).ToList());

                return itens;
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
                string sql = _command.GetCountAll;
                if (!string.IsNullOrWhiteSpace(filtro))
                    sql = sql.Replace("@filtros", filtro);
                else
                    sql = sql.Replace("@filtros", string.Empty);

                int count = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                         conn.QueryFirstOrDefault<int>(sql));

                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Logradouro> GetLogradouroByBairro(string ibge, int idbairro)
        {
            try
            {
                List<Logradouro> logradouros = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                               conn.Query<Logradouro>(_command.GetLogradouroByBairro, new { @bairro = idbairro })
                                                   .ToList());

                return logradouros;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Logradouro GetLogradouroById(string ibge, int id)
        {
            try
            {
                var model = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                         conn.QueryFirstOrDefault<Logradouro>(_command.GetLogradouroById, new { @id = id }));

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}
