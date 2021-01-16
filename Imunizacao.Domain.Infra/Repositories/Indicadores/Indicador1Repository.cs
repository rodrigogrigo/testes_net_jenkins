using Dapper;
using RgCidadao.Domain.Commands.Indicadores;
using RgCidadao.Domain.Repositories.Indicadores;
using RgCidadao.Domain.ViewModels.Indicadores;
using System;
using System.Collections.Generic;
using System.Linq;


namespace RgCidadao.Domain.Infra.Repositories.Indicadores
{
    public class Indicador1Repository : IIndicador1Repository
    {
        private IIndicador1Command _command;
        public Indicador1Repository(IIndicador1Command command)
        {
            _command = command;
        }

        public List<Indicador1ViewModel> Indicador1(string ibge, string sqlSelect, string sqlFiltros, string sqlAgrupamento)
        {
            try
            {
                string sql = _command.Indicador1;
                sql = sql.Replace("@select", $"{sqlSelect}");
                sql = sql.Replace("@filtros", $"{sqlFiltros}");
                sql = sql.Replace("@agrupamento", $"{sqlAgrupamento}");

                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                            conn.Query<Indicador1ViewModel>(sql)).ToList();

                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PubliAlvoIndicador1ViewModel> PublicoAlvo(string ibge, string sqlSelect, string sqlFiltros)
        {
            try
            {
                string sql = _command.publicoAlvo;
                
                sql = sql.Replace("@select", $"{sqlSelect}");
                sql = sql.Replace("@filtros", $"{sqlFiltros}");

                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Query<PubliAlvoIndicador1ViewModel>(sql)).ToList();

                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int? CountPublicoAlvo(string ibge, string sqlFiltros)
        {
            try
            {
                string sql = _command.CountPublicoAlvo;

                sql = sql.Replace("@filtros", $"{sqlFiltros}");

                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.QueryFirstOrDefault<int?>(sql));

                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AtendimentoIndicador1ViewModel> Atendimentos(string ibge, int id_individuo, string dum, string gi_dt_nascimento)
        {
            try
            {
                string sql = _command.Atendimentos;

                sql = sql.Replace(@"@id_individuo", id_individuo.ToString());
                sql = sql.Replace(@"@data_dum", $"'{dum}'");
                sql = sql.Replace(@"@gi_data_nascimento", gi_dt_nascimento);

                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Query<AtendimentoIndicador1ViewModel>(sql)).ToList();

                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool VerificaNovaEstrutura(string ibge)
        {
            try
            {
                var count = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                        conn.QueryFirstOrDefault<int>(_command.VerificaNovaEstrutura));

                if (count <= 0)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
