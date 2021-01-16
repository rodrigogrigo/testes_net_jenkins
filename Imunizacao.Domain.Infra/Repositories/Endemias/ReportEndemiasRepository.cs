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
    public class ReportEndemiasRepository : IReportEndemiasRepository
    {
        private IReportEndemiasCommand _command;
        public ReportEndemiasRepository(IReportEndemiasCommand command)
        {
            _command = command;
        }

        public List<AntivetorialAnaliticoViewModel> GetAntivetorialAnatico(string ibge, string filtro)
        {
            try
            {
                string sql = string.Empty;
                if (!string.IsNullOrWhiteSpace(filtro))
                    sql = _command.ServicoAntivetorialAnalitico.Replace("@filtro", filtro);
                else
                    sql = _command.ServicoAntivetorialAnalitico.Replace("@filtro", string.Empty);

                var lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Query<AntivetorialAnaliticoViewModel>(sql)).ToList();

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AntivetorialInfectadosViewModel> GetAntivetorialInfectados(string ibge, string filtro)
        {
            try
            {
                var lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                        conn.Query<AntivetorialInfectadosViewModel>(_command.ServAntivetorialInfectados.Replace("@filtro", $"{filtro}"))).ToList();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AntivetorialResumoLabViewModel> GetAntivetorialResumoLab(string ibge, string filtro, string filtro2)
        {
            try
            {
                var lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                        conn.Query<AntivetorialResumoLabViewModel>(_command.ServAntivetorialResumoLab
                                                                           .Replace("@filtro1", $"{filtro}")
                                                                           .Replace("@filtro2", $"{filtro2}"))).ToList();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AntivetorialTotalizadorViewModel GetAntivetorialTotalizador(string ibge, string filtro1, string filtro2)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                        conn.QueryFirstOrDefault<AntivetorialTotalizadorViewModel>(_command.ServicoAntivetorialTotalizador
                                                                             .Replace("@filtro1", $"{filtro1}")
                                                                             .Replace("@filtro2", $"{filtro2}")));
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AntivetorialCampoTotaisViewModel GetAntivetorialTrabalhoCampoTotais(string ibge, string filtro)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                        conn.QueryFirstOrDefault<AntivetorialCampoTotaisViewModel>(_command.ServAntivetorialTrabalhoCampoTotais
                                                                            .Replace("@filtro", $"{filtro}")));
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<InfestacaoPredialViewModel> GetInfestacaoPredialReport(string ibge, string filtro)
        {
            try
            {
                string sql = string.Empty;
                if (!string.IsNullOrWhiteSpace(filtro))
                    sql = _command.InfestacaoPredialSintetico.Replace("@filtro", filtro);
                else
                    sql = _command.InfestacaoPredialSintetico.Replace("@filtro", string.Empty);

                var lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Query<dynamic>(sql)).ToList();

                var listainfestacao = new List<InfestacaoPredialViewModel>();
                foreach (var item in lista)
                {
                    var iteminfestacao = new InfestacaoPredialViewModel();
                    iteminfestacao.bairro = item.BAIRRO;
                    iteminfestacao.imoveis_pesquisados = item.IMOVEIS_PESQUISADOS;
                    iteminfestacao.total_positivos_aegypti = item.TOTAL_POSITIVOS_AEGYPTI;
                    iteminfestacao.ind_aegypti_pred = item.IND_AEGYPTI_PRED;
                    iteminfestacao.total_positivos_albopictus = item.TOTAL_POSITIVOS_ALBOPICTUS;
                    iteminfestacao.ind_albopictus_pred = item.IND_ALBOPICTUS_PRED;
                    iteminfestacao.dep_aegypti_pos = item.DEP_AEGYPTI_POS;
                    iteminfestacao.dep_albopictus_pos = item.DEP_ALBOPICTUS_POS;
                    iteminfestacao.ind_aegypti_bret = item.IND_AEGYPTI_BRET;
                    iteminfestacao.ind_albopictus_bret = item.IND_ALBOPICTUS_BRET;
                    iteminfestacao.tot_rec_pos_aegypti = item.TOT_REC_POS_AEGYPTI;
                    iteminfestacao.a1_aegypti = item.A1_AEGYPTI_COUNT;
                    iteminfestacao.a2_aegypti = item.A2_AEGYPTI_COUNT;
                    iteminfestacao.b_aegypti = item.B_AEGYPTI_COUNT;
                    iteminfestacao.c_aegypti = item.C_AEGYPTI_COUNT;
                    iteminfestacao.d1_aegypti = item.D1_AEGYPTI_COUNT;
                    iteminfestacao.d2_aegypti = item.D2_AEGYPTI_COUNT;
                    iteminfestacao.e_aegypti = item.E_AEGYPTI_COUNT;

                    iteminfestacao.tot_rec_pos_albopictus = item.TOT_REC_POS_ALBOPICTUS;
                    iteminfestacao.a1_albopictus = item.A1_ALBOPICTUS_COUNT;
                    iteminfestacao.a2_albopictus = item.A2_ALBOPICTUS_COUNT;
                    iteminfestacao.b_albopictus = item.B_ALBOPICTUS_COUNT;
                    iteminfestacao.c_albopictus = item.C_ALBOPICTUS_COUNT;
                    iteminfestacao.d1_albopictus = item.D1_ALBOPICTUS_COUNT;
                    iteminfestacao.d2_albopictus = item.D2_ALBOPICTUS_COUNT;
                    iteminfestacao.e_albopictus = item.E_ALBOPICTUS_COUNT;

                    iteminfestacao.a1_tot = item.A1_TOT;
                    iteminfestacao.a2_tot = item.A2_TOT;
                    iteminfestacao.b_tot = item.B_TOT;
                    iteminfestacao.c_tot = item.C_TOT;
                    iteminfestacao.d1_tot = item.D1_TOT;
                    iteminfestacao.d2_tot = item.D2_TOT;
                    iteminfestacao.e_tot = item.E_TOT;

                    listainfestacao.Add(iteminfestacao);
                }
                return listainfestacao;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
