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
    public class EscolaRepository : IEscolaRepository
    {
        private IEscolaCommand _command;
        public EscolaRepository(IEscolaCommand command)
        {
            _command = command;
        }

        public void Delete(string ibge, int id)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                   conn.Execute(_command.Delete, new
                                   {
                                       @id = id
                                   }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Escola> GetAll(string ibge)
        {
            try
            {
                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                    conn.Query<Escola>(_command.GetAll).ToList());
                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Escola> GetAllPagination(string ibge, int page, int pagesize, string filtro)
        {
            try
            {
                var sql = string.Empty;
                if (!string.IsNullOrWhiteSpace(filtro))
                    sql = _command.GetAllPagination.Replace("@filtros", filtro);
                else
                    sql = _command.GetAllPagination.Replace("@filtros", string.Empty);

                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                    conn.Query<Escola>(sql, new
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
                var sql = string.Empty;
                if (!string.IsNullOrWhiteSpace(filtro))
                    sql = _command.GetCountAll.Replace("@filtros", filtro);
                else
                    sql = _command.GetCountAll.Replace("@filtros", string.Empty);

                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                    conn.QueryFirstOrDefault<int>(sql));
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Escola GetEscolaById(string ibge, int id)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                    conn.QueryFirstOrDefault<Escola>(_command.GetEscolaById, new
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

        public int GetNewId(string ibge)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                    conn.QueryFirstOrDefault<int>(_command.GetNewId));
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insert(string ibge, Escola model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                    conn.Execute(_command.Insert, new
                                    {
                                        @id = model.id,
                                        @nome = model.nome,
                                        @inep = model.inep,
                                        @id_logradouro = model.id_logradouro,
                                        @telefone = model.telefone
                                    }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(string ibge, Escola model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                    conn.Execute(_command.Update, new
                                    {
                                        @nome = model.nome,
                                        @inep = model.inep,
                                        @id_logradouro = model.id_logradouro,
                                        @telefone = model.telefone,
                                        @id = model.id
                                    }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
