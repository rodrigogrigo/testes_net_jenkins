using Dapper;
using RgCidadao.Domain.Commands.Imunizacao;
using RgCidadao.Domain.Entities.Imunizacao;
using RgCidadao.Domain.Repositories.Imunizacao;
using RgCidadao.Domain.ViewModels;
using RgCidadao.Domain.ViewModels.Imunizacao;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RgCidadao.Domain.Infra.Repositories.Imunizacao
{
    public class DashboardRepository : IDashboardRepository
    {
        public IDashboardCommand _command;
        public DashboardRepository(IDashboardCommand command)
        {
            _command = command;
        }

        public DashboardViewModel GetPercentualPolioPenta(string ibge, int unidade)
        {
            try
            {
                var dashboard = new DashboardViewModel();
                var valores = Helpers.HelperConnection.ExecuteCommand<dynamic>(ibge, conn =>
                                              conn.QueryFirstOrDefault<dynamic>(_command.GetPercentualPolioPenta, new { @unidade = unidade }));
                var valortotal = valores.QTDE_INDIVIDUOS;
                var indicador = valores.INDICADOR;
                //calcula percentual
                var total = (indicador * 100) / valortotal;

                dashboard.PercentualPolioPenta = total;
                return dashboard;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DashboardViewModel> GetVacinas(string ibge, int unidade)
        {
            try
            {
                var lista = Helpers.HelperConnection.ExecuteCommand<List<DashboardViewModel>>(ibge, conn =>
                                              conn.Query<DashboardViewModel>(_command.GetVacinas, new { @unidade = unidade }).ToList());
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DashboardViewModel> TotalImunizadasMes(string ibge)
        {
            try
            {
                var contagem = Helpers.HelperConnection.ExecuteCommand<List<dynamic>>(ibge, conn =>
                                            conn.Query<dynamic>(_command.TotalImunizadasMes).ToList());

                var lista = new List<DashboardViewModel>();
                foreach (var item in contagem)
                {
                    var modelitem = new DashboardViewModel();
                    modelitem.mesaplicacao = item.MES_APLICACAO;
                    modelitem.imunizadasmes = item.COUNT;

                    lista.Add(modelitem);
                }
                return lista;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DashboardViewModel TotalVacinasDia(string ibge, int unidade)
        {
            try
            {
                var contagem = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                            conn.QueryFirstOrDefault<int>(_command.TotalVacinasDia, new { @id = unidade }));
                var model = new DashboardViewModel()
                {
                    vacinasdia = contagem
                };
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DashboardViewModel TotalVacinaVencida(string ibge)
        {
            try
            {
                var contagem = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                          conn.QueryFirstOrDefault<int>(_command.TotalVacinaVencida));
                var model = new DashboardViewModel()
                {
                    vacinasvencidas = contagem
                };
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
