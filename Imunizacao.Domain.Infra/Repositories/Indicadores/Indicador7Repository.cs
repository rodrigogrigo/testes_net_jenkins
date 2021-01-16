using Dapper;
using RgCidadao.Domain.Commands.Indicadores;
using RgCidadao.Domain.Repositories.Indicadores;
using RgCidadao.Domain.ViewModels.Indicadores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RgCidadao.Domain.Infra.Repositories.Indicadores
{
    public class Indicador7Repository : IIndicador7Repository
    {
        private IIndicador7Command _command;
        public Indicador7Repository(IIndicador7Command command)
        {
            _command = command;
        }

        public List<Indicador7ViewModel> Indicador7(string ibge, string sqlSelect, string sqlFiltros, string sqlAgrupamento, string quadrimestre, int ano)
        {
            try
            {
                string sql = _command.Indicador7;
                sql = sql.Replace("@select", $"{sqlSelect}");
                sql = sql.Replace("@filtros", $"{sqlFiltros}");
                sql = sql.Replace("@quadrimestre", $"{quadrimestre}");
                sql = sql.Replace("@agrupamento", $"{sqlAgrupamento}");
                sql = sql.Replace("@ano", $"{ano}");

                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                            conn.Query<Indicador7ViewModel>(sql)).ToList();

                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PubliAlvoIndicador7ViewModel> PublicoAlvo(string ibge, string sqlSelect, string sqlFiltros, string quadrimestre, int ano)
        {
            try
            {
                string sql = _command.PublicoAlvo;

                sql = sql.Replace("@select", $"{sqlSelect}");
                sql = sql.Replace("@quadrimestre", $"{quadrimestre}");
                sql = sql.Replace("@filtros", $"{sqlFiltros}");
                sql = sql.Replace("@ano", $"{ano}");

                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Query<PubliAlvoIndicador7ViewModel>(sql)).ToList();

                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int? CountPublicoAlvo(string ibge, string sqlFiltros, string quadrimestre)
        {
            try
            {
                string sql = _command.CountPublicoAlvo;

                sql = sql.Replace("@filtros", $"{sqlFiltros}");
                sql = sql.Replace("@quadrimestre", $"{quadrimestre}");

                var count = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.QueryFirstOrDefault<int?>(sql));

                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AtendimentoIndicador7ViewModel> Atendimentos(string ibge, int id_individuo, string quadrimestre, int ano)
        {
            try
            {
                string sql = _command.Atendimentos;

                sql = sql.Replace(@"@id_individuo", id_individuo.ToString());
                sql = sql.Replace(@"@quadrimestre", quadrimestre);
                sql = sql.Replace("@ano", $"{ano}");

                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Query<AtendimentoIndicador7ViewModel>(sql)).ToList();

                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
