using Dapper;
using RgCidadao.Domain.Commands.Endemias;
using RgCidadao.Domain.Entities.Endemias;
using RgCidadao.Domain.Repositories.Endemias;
using RgCidadao.Domain.ViewModels.Endemias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RgCidadao.Domain.Infra.Repositories.Endemias
{
    public class CicloRepository : ICicloRepository
    {
        private ICicloCommand _command;
        public CicloRepository(ICicloCommand command)
        {
            _command = command;
        }

        public void Excluir(string ibge, int id)
        {
            try
            {
                //exclui logs existentes antes de excluir os ciclos cadastrados
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                  conn.Execute(_command.ExcluirLogsByCiclo, new { @id_ciclo = id }));

                //exclui registro de ciclo
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                   conn.Execute(_command.Delete, new { @id = id }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Ciclo> GetAll(string ibge)
        {
            try
            {
                var lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                         conn.Query<Ciclo>(_command.GetAll)).ToList();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Ciclo> GetAllCiclosAtivos(string ibge)
        {
            try
            {
                var lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                         conn.Query<Ciclo>(_command.GetAllCiclosAtivos)).ToList();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Ciclo> GetAllPagination(string ibge, string filtro, int page, int pagesize)
        {
            try
            {
                string sql = _command.GetAllPagination;
                if (!string.IsNullOrWhiteSpace(filtro))
                    sql = sql.Replace("@filtro", filtro);
                else
                    sql = sql.Replace("@filtro", string.Empty);


                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                        conn.Query<Ciclo>(sql, new
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

        public List<Ciclo> GetCicloByData(string ibge, DateTime? data)
        {
            try
            {
                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                   conn.Query<Ciclo>(_command.GetCicloByData, new
                                   {
                                       @data = data
                                   })).ToList();
                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Ciclo GetCicloById(string ibge, int id)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                   conn.QueryFirstOrDefault<Ciclo>(_command.GetCicloById, new { @id = id }));
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CiclosUtilizadosViewModel> GetciclosUtilizados(string ibge)
        {
            try
            {
                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                            conn.Query<CiclosUtilizadosViewModel>(_command.GetNumCiclosRestantes)).ToList();

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

        public int CountVisitasCiclo(string ibge, int id_ciclo, DateTime? data_inicial, DateTime? data_final)
        {
            try
            {
                var contagem = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                    conn.QueryFirstOrDefault<int>(_command.CountVisitasCiclo, new
                                    {
                                        @id_ciclo = id_ciclo,
                                        @data_inicial = data_inicial,
                                        @data_final = data_final
                                    }));

                return contagem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DateTime? GetDataMaximaCiclo(string ibge, int id_ciclo)
        {
            try
            {
                var data = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                   conn.QueryFirstOrDefault<DateTime?>(_command.GetDataMaximaCiclo, new
                                   {
                                       @id_ciclo = id_ciclo
                                   }));

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DateTime? GetDataMinimaCiclo(string ibge, int id_ciclo)
        {
            try
            {
                var data = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                 conn.QueryFirstOrDefault<DateTime?>(_command.GetDataMinimaCiclo, new
                                 {
                                     @id_ciclo = id_ciclo
                                 }));

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CicloLog> GetLogCicloByCiclo(string ibge, int id_ciclo)
        {
            try
            {
                var ciclos = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                conn.Query<CicloLog>(_command.GetLogCicloByCiclo, new
                                {
                                    @id_ciclo = id_ciclo
                                }).ToList());

                return ciclos;
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

        public void InserirLogCiclo(string ibge, CicloLog model)
        {
            try
            {
                var id = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                conn.QueryFirstOrDefault<int?>(_command.GetLogCicloNewId));

                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                               conn.Execute(_command.CriarLogCiclo, new
                               {
                                   @id = id,
                                   @id_ciclo = model.id_ciclo,
                                   @situacao = model.situacao,
                                   @data_situacao = DateTime.Now,
                                   id_usuario = model.id_usuario
                               }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insert(string ibge, Ciclo model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                   conn.Execute(_command.Insert, new
                   {
                       @id = model.id,
                       @data_inicial = model.data_inicial,
                       @data_final = model.data_final,
                       @num_ciclo = model.num_ciclo,
                       @situacao = model.situacao,
                       @data_situacao = model.data_situacao,
                       @id_usuario = model.id_usuario
                   }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(string ibge, Ciclo model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                  conn.Execute(_command.Update, new
                  {
                      @data_inicial = model.data_inicial,
                      @data_final = model.data_final,
                      @num_ciclo = model.num_ciclo,
                      @situacao = model.situacao,
                      @data_situacao = model.data_situacao,
                      @id_usuario = model.id_usuario,
                      @id = model.id,
                  }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Ciclo> ValidaExistenciaCicloPeriodo(string ibge, DateTime? datainicial, DateTime? datafinal)
        {
            try
            {
                //VALIDA EXISTENCIA DE CICLO ANTES DE CADASTRAR NOVO
                var itenscicloperiodo = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                         conn.Query<Ciclo>(_command.ValidaExistenciaCicloPeriodo, new
                                         {
                                             @datainicial = datainicial,
                                             @datafinal = datafinal
                                         })).ToList();
                return itenscicloperiodo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
