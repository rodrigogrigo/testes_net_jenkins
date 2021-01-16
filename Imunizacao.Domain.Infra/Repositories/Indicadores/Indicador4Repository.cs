using Dapper;
using RgCidadao.Domain.Commands.Indicadores;
using RgCidadao.Domain.Repositories.Indicadores;
using RgCidadao.Domain.ViewModels.Indicadores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RgCidadao.Domain.Infra.Repositories.Indicadores
{
    public class Indicador4Repository : IIndicador4Repository
    {
        private IIndicador4Command _command;
        public Indicador4Repository(IIndicador4Command command)
        {
            _command = command;
        }

        public List<Indicador4ViewModel> Indicador4(string ibge, string sqlSelect, string sqlFiltros,
                                                            string sqlAgrupamento, string quadrimestre, int ano)
        {
            try
            {
                string sql = _command.Indicador4;
                sql = sql.Replace("@select", $"{sqlSelect}");
                sql = sql.Replace("@filtros", $"{sqlFiltros}");
                sql = sql.Replace("@quadrimestre", $"{quadrimestre}");
                sql = sql.Replace("@agrupamento", $"{sqlAgrupamento}");
                sql = sql.Replace("@ano", $"{ano}");


                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                            conn.Query<Indicador4ViewModel>(sql)).ToList();

                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PubliAlvoIndicador4ViewModel> PublicoAlvo(string ibge, string sqlSelect, string sqlFiltros, string quadrimestre, int ano)
        {
            try
            {
                string sql = _command.PublicoAlvo;

                sql = sql.Replace("@select", $"{sqlSelect}");
                sql = sql.Replace("@quadrimestre", $"{quadrimestre}");
                sql = sql.Replace("@filtros", $"{sqlFiltros}");
                sql = sql.Replace("@ano", $"{ano}");

                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Query<PubliAlvoIndicador4ViewModel>(sql)).ToList();

                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int? CountPublicoAlvo(string ibge, string sqlFiltros, string quadrimestre, int ano)
        {
            try
            {
                string sql = _command.CountPublicoAlvo;

                sql = sql.Replace("@filtros", $"{sqlFiltros}");
                sql = sql.Replace("@quadrimestre", $"{quadrimestre}");
                sql = sql.Replace("@ano", $"{ano}");

                var count = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.QueryFirstOrDefault<int?>(sql));

                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AtendimentoIndicador4ViewModel> Atendimentos(string ibge, int id_individuo, string quadrimestre, int ano)
        {
            try
            {
                string sql = _command.Atendimentos;

                sql = sql.Replace(@"@id_individuo", id_individuo.ToString());
                sql = sql.Replace(@"@quadrimestre", quadrimestre);
                sql = sql.Replace("@ano", $"{ano}");

                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Query<AtendimentoIndicador4ViewModel>(sql)).ToList();

                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
