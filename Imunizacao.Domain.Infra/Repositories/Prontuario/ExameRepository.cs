using Dapper;
using System;
using System.Linq;
using System.Collections.Generic;

using RgCidadao.Domain.Commands.Prontuario;
using RgCidadao.Domain.Entities.Prontuario;
using RgCidadao.Domain.Repositories.Prontuario;

namespace RgCidadao.Domain.Infra.Repositories.Prontuario
{
    public class ExameRepository : IExameRepository
    {
        private readonly IExameCommand _examecommand;

        public ExameRepository(IExameCommand commandText)
        {
            _examecommand = commandText;
        }

        public List<Exame> GetExamesComuns(string ibge)
        {
            try
            {
                var exames = new List<Exame>();
                var sql = _examecommand.GetExamesComuns;
                    exames = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.Query<Exame>(sql).ToList());

                return exames;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Exame> GetExamesAltoCustos(string ibge)
        {
            try
            {
                var exames = new List<Exame>();
                var sql = _examecommand.GetExamesAltoCustos;
                exames = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                      conn.Query<Exame>(sql).ToList());

                return exames;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Exame> GetHistoricoSolicitacoesExameByPaciente(string ibge, int id_paciente, int agrupamento)
        {
            try
            {
                var exames = new List<Exame>();
                var sql = _examecommand.GetHistoricoSolicitacoesExameByPaciente;
                sql = sql.Replace("@id_paciente", $@"{id_paciente}");
                sql = sql.Replace("@agrupamento", $@"{agrupamento}");


                exames = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                      conn.Query<Exame>(sql).ToList());

                return exames;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Exame> GetHistoricoResultadoExameByPaciente(string ibge, int id_exame)
        {
            try
            {
                var exames = new List<Exame>();
                var sql = _examecommand.GetHistoricoResultadoExameByPaciente;
                sql = sql.Replace("@id_exame", $@"{id_exame}");

                exames = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                      conn.Query<Exame>(sql).ToList());

                return exames;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AgrupamentoExames> GetListAgrupamentosExames(string ibge)
        {
            try
            {
                var agrupamentos = new List<AgrupamentoExames>();
                var sql = _examecommand.GetListAgrupamentosExames;

                agrupamentos = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                      conn.Query<AgrupamentoExames>(sql).ToList());

                return agrupamentos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Cid> GetCid(string ibge)
        {
            try
            {
                var cids = new List<Cid>();
                var sql = _examecommand.GetCid;

                cids = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                      conn.Query<Cid>(sql).ToList());

                return cids;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
