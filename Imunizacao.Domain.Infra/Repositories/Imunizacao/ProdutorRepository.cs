using Dapper;
using RgCidadao.Domain.Commands.Imunizacao;
using RgCidadao.Domain.Entities.Imunizacao;
using RgCidadao.Domain.Repositories.Imunizacao;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RgCidadao.Domain.Infra.Repositories.Imunizacao
{
    public class ProdutorRepository : IProdutorRepository
    {
        public IProdutorCommand _command;
        public ProdutorRepository(IProdutorCommand command)
        {
            _command = command;
        }

        public void Delete(string ibge, int id)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                         conn.Execute(_command.Delete, new { @id = id }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Produtor> GetAll(string ibge)
        {
            try
            {
                var lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                         conn.Query<Produtor>(_command.GetAll)).ToList();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Produtor> GetAllPagination(string ibge, int pagesize, int page, string filtro)
        {
            try
            {
                var sql = _command.GetAllPagination;
                if (!string.IsNullOrWhiteSpace(filtro))
                    sql = sql.Replace("@filtro", filtro);
                else
                    sql = sql.Replace("@filtro", string.Empty);

                var lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                         conn.Query<Produtor>(sql, new
                         {
                             @pagesize = pagesize,
                             @page = page
                         })).ToList();
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
                var sql = _command.GetCountAll;
                if (!string.IsNullOrWhiteSpace(filtro))
                    sql = sql.Replace("@filtro", filtro);
                else
                    sql = sql.Replace("@filtro", string.Empty);

                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                         conn.QueryFirstOrDefault<int>(sql));
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
                var id = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                        conn.QueryFirstOrDefault<int?>(_command.GetNewId));
                if (id == null)
                    id = 1;
                return (int)id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Produtor GetProdutorById(string ibge, int id)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                        conn.QueryFirstOrDefault<Produtor>(_command.GetById, new { @id = id }));

                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insert(string ibge, Produtor model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                        conn.Execute(_command.Insert, new
                        {
                            @id = model.id,
                            @nome = model.nome,
                            @abreviatura = model.abreviatura
                        }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(string ibge, Produtor model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                        conn.Execute(_command.Update, new
                        {
                            @nome = model.nome,
                            @abreviatura = model.abreviatura,
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
