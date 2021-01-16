using Dapper;
using RgCidadao.Domain.Commands.Indicadores;
using RgCidadao.Domain.Repositories.Indicadores;
using RgCidadao.Domain.ViewModels.Indicadores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RgCidadao.Domain.Infra.Repositories.Indicadores
{
   public class Indicador3Repository : IIndicador3Repository
    {
        private IIndicador3Command _command;
        public Indicador3Repository(IIndicador3Command command)
        {
            _command = command;
        }

        public List<Indicador3ViewModel> Indicador3(string ibge, string sqlSelect, string sqlFiltros, string sqlAgrupamento)
        {
            try
            {
                string sql = _command.Indicador3;
                sql = sql.Replace("@select", $"{sqlSelect}");
                sql = sql.Replace("@filtros", $"{sqlFiltros}");
                sql = sql.Replace("@agrupamento", $"{sqlAgrupamento}");

                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                            conn.Query<Indicador3ViewModel>(sql)).ToList();

                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PubliAlvoIndicador3ViewModel> PublicoAlvo(string ibge, string sqlSelect, string sqlFiltros, string filtro_valido)
        {
            try
            {
                string sql = _command.PublicoAlvo;

                sql = sql.Replace("@select", $"{sqlSelect}");
                sql = sql.Replace("@filtros", $"{sqlFiltros}");
                sql = sql.Replace("@filtro_valido", $"{filtro_valido}");

                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Query<PubliAlvoIndicador3ViewModel>(sql)).ToList();

                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int? CountPublicoAlvo(string ibge, string sqlFiltros, string filtro_valido)
        {
            try
            {
                string sql = _command.CountPublicoAlvo;

                sql = sql.Replace("@filtros", $"{sqlFiltros}");
                sql = sql.Replace("@filtro_valido", $"{filtro_valido}");

                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.QueryFirstOrDefault<int?>(sql));

                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AtendimentoIndicador3ViewModel> Atendimentos(string ibge, int id_individuo, string dum, string gi_dt_nascimento)
        {
            try
            {
                string sql = _command.Atendimentos;

                sql = sql.Replace(@"@id_individuo", id_individuo.ToString());
                sql = sql.Replace(@"@data_dum", $"'{dum}'");
                sql = sql.Replace(@"@gi_data_nascimento", gi_dt_nascimento);

                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Query<AtendimentoIndicador3ViewModel>(sql)).ToList();

                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
