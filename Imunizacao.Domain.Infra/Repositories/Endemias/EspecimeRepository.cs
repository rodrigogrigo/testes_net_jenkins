using Dapper;
using RgCidadao.Domain.Commands.Endemias;
using RgCidadao.Domain.Entities.Endemias;
using RgCidadao.Domain.Repositories.Endemias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RgCidadao.Domain.Infra.Repositories.Endemias
{
    public class EspecimeRepository : IEspecimeRepository
    {
        private IEspecimeCommand _command;
        public EspecimeRepository(IEspecimeCommand command)
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

        public List<Especime> GetAllEspecime(string ibge)
        {
            try
            {

                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                        conn.Query<Especime>(_command.GetAllEspecime)).ToList();
                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Especime> GetAllPagination(string ibge, int page, int pagesize, string filtro)
        {
            try
            {
                string sql = _command.GetAllPagination;
                if (!string.IsNullOrWhiteSpace(filtro))
                    sql = sql.Replace("@filtro", filtro);
                else
                    sql = sql.Replace("@filtro", string.Empty);

                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                        conn.Query<Especime>(sql, new
                        {
                            @pagesize = pagesize,
                            @page = page
                        })).ToList();
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

        public Especime GetEspecimeById(string ibge, int id)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                        conn.QueryFirstOrDefault<Especime>(_command.GetEspecimeById, new
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

        public int GetEspecimeNewId(string ibge)
        {
            try
            {
                var id = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                    conn.QueryFirstOrDefault<int>(_command.GetEspecimeNewId));
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insert(string ibge, Especime model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                     conn.Execute(_command.Insert, new
                     {
                         @id = model.id,
                         @especime = model.especime,
                         @tipo_especime = model.tipo_especime
                     }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(string ibge, Especime model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                     conn.Execute(_command.Update, new
                     {
                         @especime = model.especime,
                         @tipo_especime = model.tipo_especime,
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
