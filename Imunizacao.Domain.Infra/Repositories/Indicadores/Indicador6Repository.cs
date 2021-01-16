using Dapper;
using RgCidadao.Domain.Commands.Indicadores;
using RgCidadao.Domain.Repositories.Indicadores;
using RgCidadao.Domain.ViewModels.Indicadores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RgCidadao.Domain.Infra.Repositories.Indicadores
{
    public class Indicador6Repository : IIndicador6Repository
    {
        private IIndicador6Command _command;
        public Indicador6Repository(IIndicador6Command command)
        {
            _command = command;
        }

        public List<Indicador6ViewModel> Indicador6(string ibge, string sqlSelect, string sqlFiltros, string sqlAgrupamento, string quadrimestre, int ano)
        {
            try
            {
                string sql = _command.Indicador6;
                sql = sql.Replace("@select", $"{sqlSelect}");
                sql = sql.Replace("@filtros", $"{sqlFiltros}");
                sql = sql.Replace("@quadrimestre", $"{quadrimestre}");
                sql = sql.Replace("@agrupamento", $"{sqlAgrupamento}");
                sql = sql.Replace("@ano", $"{ano}");

                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                            conn.Query<Indicador6ViewModel>(sql)).ToList();

                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PubliAlvoIndicador6ViewModel> PublicoAlvo(string ibge, string sqlSelect, string sqlFiltros, string quadrimestre, int ano)
        {
            try
            {
                string sql = _command.PublicoAlvo;

                sql = sql.Replace("@select", $"{sqlSelect}");
                sql = sql.Replace("@quadrimestre", $"{quadrimestre}");
                sql = sql.Replace("@filtros", $"{sqlFiltros}");
                sql = sql.Replace("@ano", $"{ano}");

                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Query<PubliAlvoIndicador6ViewModel>(sql)).ToList();

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

        public List<AtendimentoIndicador6ViewModel> Atendimentos(string ibge, int id_individuo, string quadrimestre, int ano)
        {
            try
            {
                string sql = _command.Atendimentos;

                sql = sql.Replace(@"@id_individuo", id_individuo.ToString());
                sql = sql.Replace(@"@quadrimestre", quadrimestre);
                sql = sql.Replace("@ano", $"{ano}");

                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Query<AtendimentoIndicador6ViewModel>(sql)).ToList();

                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
