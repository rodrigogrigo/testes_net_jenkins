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
    public class BairroRepository : IBairroRepository
    {
        private IBairroCommand _command;
        public BairroRepository(IBairroCommand command)
        {
            _command = command;
        }

        public List<Bairro> GetAll(string ibge)
        {
            try
            {
                var itens = Helpers.HelperConnection.ExecuteCommand<List<Bairro>>(ibge, conn =>
                        conn.Query<Bairro>(_command.GetAll).ToList());

                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Bairro> GetAllPagination(string ibge, string filtro, int page, int pagesize)
        {
            try
            {
                string sql = _command.GetAllPagination;
                if (!string.IsNullOrWhiteSpace(filtro))
                    sql = sql.Replace("@filtros", filtro);
                else
                    sql = sql.Replace("@filtros", string.Empty);

                var itens = Helpers.HelperConnection.ExecuteCommand<List<Bairro>>(ibge, conn =>
                           conn.Query<Bairro>(_command.GetAllPagination, new
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

        public List<Bairro> GetBairroByIbge(string ibge, string codibge)
        {
            try
            {
                var itens = Helpers.HelperConnection.ExecuteCommand<List<Bairro>>(ibge, conn =>
                        conn.Query<Bairro>(_command.GetBairroByIbge, new
                        {
                            @ibge = codibge
                        }).ToList());

                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Bairro GetById(string ibge, int id)
        {
            try
            {
                var bairro = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                        conn.QueryFirstOrDefault<Bairro>(_command.GetBairroById, new
                        {
                            @id = id
                        }));

                return bairro;
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
                string sql = _command.GetAllPagination;
                if (!string.IsNullOrWhiteSpace(filtro))
                    sql = sql.Replace("@filtros", filtro);
                else
                    sql = sql.Replace("@filtros", string.Empty);

                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.QueryFirstOrDefault<int>(_command.GetCountAll));

                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
