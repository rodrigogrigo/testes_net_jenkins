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
    public class ResultadoAmostraRepository : IResultadoAmostraRepository
    {
        private IResultadoAmostraCommand _command;
        public ResultadoAmostraRepository(IResultadoAmostraCommand command)
        {
            _command = command;
        }

        public void DeleteColetaResultado(string ibge, int id)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                 conn.Execute(_command.DeleteColetaResultado, new { @id = id }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteColetaResultadoByColeta(string ibge, int coleta)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                 conn.Execute(_command.DeleteColetaResultadoByColeta, new { @id_coleta = coleta }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ResultadoAmostraViewModel> GetAllPagination(string ibge, int? page, int? pagesize, string filtro, string filtroAmostras)
        {
            try
            {
                var lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                 conn.Query<ResultadoAmostraViewModel>(_command.GetAllPagination.Replace("@filtro", $"{filtro}"), new
                 {
                     @pagesize = pagesize,
                     @page = page
                 })).ToList();

                var sqlPendente = _command.PendenteVisitaGetAllPagination;
                sqlPendente = sqlPendente.Replace("@filtroamostra", filtroAmostras);

                var sqlLancada = _command.LancadaVisitaGetAllPagination;
                sqlLancada = sqlLancada.Replace("@filtroamostra", filtroAmostras);

                foreach (var item in lista)
                {
                    item.pendente = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                       conn.QueryFirstOrDefault<int>(sqlPendente, new
                                       {
                                           @profissional = item.id_profissional
                                       }));

                    item.lancada = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                       conn.QueryFirstOrDefault<int>(sqlLancada, new
                                       {
                                           @profissional = item.id_profissional
                                       }));

                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ColetaResultado> GetColetaResultadoByColeta(string ibge, int coleta)
        {
            try
            {
                var lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                    conn.Query<ColetaResultado>(_command.GetColetaResultadoByColeta, new { @id_coleta = coleta })).ToList();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetCountResultadoAmostra(string ibge, string filtro)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                    conn.QueryFirstOrDefault<int>(_command.GetCountAll.Replace("@filtro", $"{filtro}")));
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetNewIdResultadoAmostra(string ibge)
        {
            try
            {
                var id = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                    conn.QueryFirstOrDefault<int>(_command.GetResultadoAmostraNewId));

                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Coleta> GetResultadoAmostraByProfissional(string ibge, int id_profissional, int? id_ciclo, DateTime? data_inicial, DateTime? data_final)
        {
            try
            {
                string sqlFilter = string.Empty;

                if (data_inicial != null)
                {
                    sqlFilter+= $@" AND CAST(VI.DATA_HORA_ENTRADA AS DATE) >= '{data_inicial?.ToString("dd.MM.yyyy")}'";
                }

                if (data_final != null)
                {
                    sqlFilter += $@" AND CAST(VI.DATA_HORA_SAIDA AS DATE) <= '{data_final?.ToString("dd.MM.yyyy")}'";
                }

                if (id_ciclo != null)
                {
                    sqlFilter += $@" AND CIC.ID = {id_ciclo}";
                }

                string sql = _command.GetResultadoAmostraByProfissional;

                sql = sql.Replace("@filtro", sqlFilter);

                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                               conn.Query<dynamic>(sql, new
                               {
                                   @profissional = id_profissional
                               })).ToList();

                var lista = new List<Coleta>();
                var listacoleta = itens.Select(x => new
                {
                    id_coleta = x.ID_COLETA,
                    id_visita = x.ID_VISITA,
                    amostra = x.AMOSTRA,
                    deposito = x.DEPOSITO,
                    id_profissional = x.ID_PROFISSIONAL,
                    qtde_larvas_coleta = x.QTDE_LARVAS_COLETA
                }).Distinct().ToList();

                foreach (var item in listacoleta)
                {
                    var coleta = new Coleta();
                    coleta.id = item.id_coleta;
                    coleta.id_visita = item.id_visita;
                    coleta.amostra = item.amostra;
                    coleta.deposito = item.deposito;
                    coleta.id_profissional = item.id_profissional;
                    coleta.qtde_larvas = item.qtde_larvas_coleta;

                    var itensresultado = itens.Where(x => x.ID_COLETA == item.id_coleta)
                                              .Select(x => new ColetaResultado
                                              {
                                                  id = x.ID,
                                                  id_coleta = x.ID_COLETA,
                                                  id_visita = x.ID_VISITA,
                                                  id_especime = x.ID_ESPECIME,
                                                  qtde = x.QTDE_LARVAS,
                                                  exemplar = x.EXEMPLAR
                                              }).ToList();
                    coleta.itens.AddRange(itensresultado);
                    lista.Add(coleta);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Coleta> GetResultadoAmostraByVisita(string ibge, int? id_visita)
        {
            try
            {
                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                 conn.Query<dynamic>(_command.GetResultadoAmostraByVisita, new
                                 {
                                     @id_visita = id_visita
                                 })).ToList();

                var lista = new List<Coleta>();
                var listacoleta = itens.Select(x => new
                {
                    id_coleta = x.ID_COLETA,
                    amostra = x.AMOSTRA,
                    deposito = x.DEPOSITO,
                    id_profissional = x.ID_PROFISSIONAL,
                    qtde_larvas_coleta = x.QTDE_LARVAS_COLETA
                }).Distinct().ToList();

                foreach (var item in listacoleta)
                {
                    var coleta = new Coleta();
                    coleta.id = item.id_coleta;
                    coleta.amostra = item.amostra;
                    coleta.deposito = item.deposito;
                    coleta.id_profissional = item.id_profissional;
                    coleta.qtde_larvas = item.qtde_larvas_coleta;

                    var itensresultado = itens.Where(x => x.ID_COLETA == item.id_coleta)
                                              .Select(x => new ColetaResultado
                                              {
                                                  id = x.ID,
                                                  id_coleta = x.ID_COLETA,
                                                  id_especime = x.ID_ESPECIME,
                                                  qtde = x.QTDE_LARVAS,
                                                  exemplar = x.EXEMPLAR
                                              }).ToList();
                    coleta.itens.AddRange(itensresultado);
                    lista.Add(coleta);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ResultadoColetaViewModel> GetResultadoColetaByProfissional(string ibge, int? id_profissional)
        {
            try
            {
                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                   conn.Query<ResultadoColetaViewModel>(_command.GetResultadoColetaByProfissional, new { @id_profissional = id_profissional })).ToList();
                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ResultadoColetaViewModel> GetResultadoColetaByVisita(string ibge, int? id_visita)
        {
            try
            {
                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                   conn.Query<ResultadoColetaViewModel>(_command.GetColetaResultadoByVisita, new
                                   {
                                       @visita = id_visita
                                   })).ToList();
                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertColetaResultado(string ibge, ColetaResultado model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                               conn.Execute(_command.InsertColetaResultado, new
                               {
                                   @id = model.id,
                                   @id_coleta = model.id_coleta,
                                   @id_especime = model.id_especime,
                                   @qtde = model.qtde,
                                   @exemplar = model.exemplar
                               }));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void UpdateColetaResultado(string ibge, ColetaResultado model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                   conn.Execute(_command.UpdateColetaResultado, new
                                   {
                                       @id_especime = model.id_especime,
                                       @qtde = model.qtde,
                                       @exemplar = model.exemplar,
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
