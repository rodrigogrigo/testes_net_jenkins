using Dapper;
using RgCidadao.Domain.Commands.AtencaoBasica;
using RgCidadao.Domain.Infra.Helpers;
using RgCidadao.Domain.Repositories.AtencaoBasica;
using RgCidadao.Domain.ViewModels;
using RgCidadao.Domain.ViewModels.AtencaoBasica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RgCidadao.Domain.Infra.Repositories.AtencaoBasica
{
    public class GestaoFamiliaRepository : IGestaoFamiliaRepository
    {
        private IGestaoFamiliaCommand _command;
        public GestaoFamiliaRepository(IGestaoFamiliaCommand command)
        {
            _command = command;
        }

        public EstatisticaGestaoFamiliaViewModel GetEstatisticasByMicroarea(string ibge, int id_microarea, int competencia)
        {
            try
            {
                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                       conn.QueryFirstOrDefault<EstatisticaGestaoFamiliaViewModel>(_command.GetEstatisticasByMicroarea, new
                       {
                           @id_microarea = id_microarea,
                           @competencia = competencia
                       }));

                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EstatisticaGestaoFamiliaViewModel GetEstatisticasByEquipes(string ibge, string sqlFiltros, int competencia)
        {
            try
            {
                string sql = _command.GetEstatisticasByEquipes;

                sql = sql.Replace(@"@filtros", sqlFiltros);
                sql = sql.Replace(@"@competencia", competencia.ToString());

                var item = HelperConnection.ExecuteCommand(ibge, conn =>
                                conn.QueryFirstOrDefault<EstatisticaGestaoFamiliaViewModel>(sql)
                            );

                return item;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GestaoFamiliaViewModel> GetGestaoFamilia(string ibge, int competencia, int microarea)
        {
            try
            {
                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                       conn.Query<dynamic>(_command.GetEstabelecimentoByMicroarea, new
                       {
                           @id_microarea = microarea,
                       }).ToList());

                var lista = new List<GestaoFamiliaViewModel>();
                var logradourosgroup = itens.GroupBy(x => new { x.ID_LOGRADOURO, x.LOGRADOURO, x.BAIRRO })
                                            .Select(x => new
                                            {
                                                id_logradouro = x.Select(p => p.ID_LOGRADOURO).FirstOrDefault(),
                                                nome_logradouro = x.Select(p => p.LOGRADOURO).FirstOrDefault(),
                                                bairro = x.Select(p => p.BAIRRO).FirstOrDefault(),
                                                cidade = x.Select(p => p.CIDADE).FirstOrDefault(),
                                                sigla_estado = x.Select(p => p.SIGLA_ESTADO).FirstOrDefault(),
                                                tipo_imovel = x.Select(p => p.TIPO_IMOVEL).FirstOrDefault(),
                                                latitude = x.Select(p => p.LATITUDE).FirstOrDefault(),
                                                longitude = x.Select(p => p.LONGITUDE).FirstOrDefault(),
                                            })
                                            .ToList();
                foreach (var item in logradourosgroup)
                {
                    var model = new GestaoFamiliaViewModel();

                    model.id = item.id_logradouro;
                    model.logradouro = item.nome_logradouro;
                    model.bairro = item.bairro;
                    model.cidade = item.cidade;
                    model.sigla_estado = item.sigla_estado;
                    model.tipo_imovel = item.tipo_imovel;
                    model.latitude = item.latitude;
                    model.longitude = item.longitude;

                    var imoveisgroup = itens.Where(x => x.ID_LOGRADOURO == item.id_logradouro)
                                            .GroupBy(x => new
                                            {
                                                x.ID,
                                                x.NUMERO_LOGRADOURO,
                                                x.TIPO_IMOVEL
                                            })
                                            .Select(x => new
                                            {
                                                id = x.Select(p => p.ID).FirstOrDefault(),
                                                numero_logradouro = x.Select(p => p.NUMERO_LOGRADOURO).FirstOrDefault(),
                                                tipo_imovel = x.Select(p => p.TIPO_IMOVEL).FirstOrDefault()
                                            }).ToList();

                    var listaimovel = new List<ImoveisEstabelecimento>();
                    foreach (var itemimovel in imoveisgroup)
                    {
                        var modelimovel = new ImoveisEstabelecimento();
                        modelimovel.id = itemimovel.id;
                        modelimovel.numero_logradouro = itemimovel.numero_logradouro;
                        modelimovel.tipo_imovel = itemimovel.tipo_imovel;
                        modelimovel.familias = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                                   conn.Query<FamiliaEstabelecimento>(_command.GetFamiliaByEstabelecimento, new
                                                   {
                                                       @id_estabelecimento = itemimovel.id,
                                                   }).ToList());

                        var visitas = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                                   conn.QueryFirstOrDefault<VisitaEstabelecimento>(_command.GetVisitaByEstabelecimento, new
                                                   {
                                                       @id_estabelecimento = itemimovel.id,
                                                       @competencia = competencia
                                                   }));

                        if (visitas != null)
                            modelimovel.visita = visitas;
                        else
                            modelimovel.visita = new VisitaEstabelecimento();

                        listaimovel.Add(modelimovel);
                    }
                    model.imoveis.AddRange(listaimovel);
                    lista.Add(model);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MembroFamiliaViewModel> GetMembrosByFamilia(string ibge, int id_familia)
        {
            try
            {
                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                     conn.Query<MembroFamiliaViewModel>(_command.GetMembrosByFamilia, new
                     {
                         @id_familia = id_familia,
                     }).ToList());

                foreach (var item in itens)
                {
                    var anos_idade = Helper.CalculaIdade(Convert.ToDateTime(item.csi_dtnasc));
                    item.idade_calc = $@"{anos_idade.Item1} Anos {anos_idade.Item2} Meses e {anos_idade.Item3} dias";
                }

                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GrupoIndividuosEquipeViewModel> GetDiabeticosByEquipe(string ibge, string sqlSelect, string filtro1, int competencia, string filtro2)
        {
            try
            {
                var sql = _command.GetDiabeticosByEquipe;
                sql = sql.Replace("@select", sqlSelect);
                sql = sql.Replace("@filtro1", filtro1);
                sql = sql.Replace("@filtro2", filtro2);
                sql = sql.Replace("@competencia", competencia.ToString());

                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                            conn.Query<GrupoIndividuosEquipeViewModel>(sql).ToList());

                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GrupoIndividuosEquipeViewModel> GetHipertensosByEquipe(string ibge, string sqlSelect, string filtro1, int competencia, string filtro2)
        {
            try
            {
                var sql = _command.GetHipertensosByEquipe;
                sql = sql.Replace("@select", sqlSelect);
                sql = sql.Replace("@filtro1", filtro1);
                sql = sql.Replace("@filtro2", filtro2);
                sql = sql.Replace("@competencia", competencia.ToString());

                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                            conn.Query<GrupoIndividuosEquipeViewModel>(sql).ToList());

                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GrupoIndividuosEquipeViewModel> GetGestantesByEquipe(string ibge, string sqlSelect, string filtro1, int competencia, string filtro2)
        {
            try
            {
                var sql = _command.GetGestantesByEquipe;
                sql = sql.Replace("@select", sqlSelect);
                sql = sql.Replace("@filtro1", filtro1);
                sql = sql.Replace("@filtro2", filtro2);
                sql = sql.Replace("@competencia", competencia.ToString());

                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                            conn.Query<GrupoIndividuosEquipeViewModel>(sql).ToList());

                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GrupoIndividuosEquipeViewModel> GetCriancasByEquipe(string ibge, string sqlSelect, string filtro1, int competencia, string filtro2)
        {
            try
            {
                var sql = _command.GetCriancasByEquipe;
                sql = sql.Replace("@select", sqlSelect);
                sql = sql.Replace("@filtro1", filtro1);
                sql = sql.Replace("@filtro2", filtro2);
                sql = sql.Replace("@competencia", competencia.ToString());

                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                            conn.Query<GrupoIndividuosEquipeViewModel>(sql).ToList());

                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GrupoIndividuosEquipeViewModel> GetIdososByEquipe(string ibge, string sqlSelect, string filtro1, int competencia, string filtro2)
        {
            try
            {
                var sql = _command.GetIdososByEquipe;
                sql = sql.Replace("@select", sqlSelect);
                sql = sql.Replace("@filtro1", filtro1);
                sql = sql.Replace("@filtro2", filtro2);
                sql = sql.Replace("@competencia", competencia.ToString());

                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                            conn.Query<GrupoIndividuosEquipeViewModel>(sql).ToList());

                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }  

        public TotalPorEquipeViewModel GetTotaisDiabeticoByEquipe(string ibge, string sqlfiltros, int competencia)
        {
            try
            {
                string sql = _command.GetTotaisDiabeticoByEquipe;

                sql = sql.Replace("@competencia", competencia.ToString());
                sql = sql.Replace("@filtros", sqlfiltros);

                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.QueryFirstOrDefault<TotalPorEquipeViewModel>(sql));

                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TotalPorEquipeViewModel GetTotaisHipertensoByEquipe(string ibge, string sqlfiltros, int competencia)
        {
            try
            {
                string sql = _command.GetTotaisHipertensoByEquipe;

                sql = sql.Replace("@competencia", competencia.ToString());
                sql = sql.Replace("@filtros", sqlfiltros);

                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.QueryFirstOrDefault<TotalPorEquipeViewModel>(sql));

                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TotalPorEquipeViewModel GetTotaisGestanteByEquipe(string ibge, string sqlfiltros, int competencia)
        {
            try
            {
                string sql = _command.GetTotaisGestanteByEquipe;

                sql = sql.Replace("@competencia", competencia.ToString());
                sql = sql.Replace("@filtros", sqlfiltros);

                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.QueryFirstOrDefault<TotalPorEquipeViewModel>(sql));

                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TotalPorEquipeViewModel GetTotaisCriancasByEquipe(string ibge, string sqlfiltros, int competencia)
        {
            try
            {
                string sql = _command.GetTotaisCriancasByEquipe;

                sql = sql.Replace("@competencia", competencia.ToString());
                sql = sql.Replace("@filtros", sqlfiltros);

                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.QueryFirstOrDefault<TotalPorEquipeViewModel>(sql));

                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TotalPorEquipeViewModel GetTotaisIdososByEquipe(string ibge, string sqlfiltros, int competencia)
        {
            try
            {
                string sql = _command.GetTotaisIdososByEquipe;

                sql = sql.Replace("@competencia", competencia.ToString());
                sql = sql.Replace("@filtros", sqlfiltros);

                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.QueryFirstOrDefault<TotalPorEquipeViewModel>(sql));

                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
