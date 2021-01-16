using Dapper;
using RgCidadao.Domain.Commands.Imunizacao;
using RgCidadao.Domain.Entities.Imunizacao;
using RgCidadao.Domain.Repositories.Imunizacao;
using RgCidadao.Domain.ViewModels.Imunizacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RgCidadao.Domain.Infra.Repositories.Imunizacao
{
    public class LoteRepository : ILoteRepository
    {
        public ILoteCommand _command;
        public LoteRepository(ILoteCommand command)
        {
            _command = command;
        }

        public void AdicionaBloqueioUnidadeLote(string ibge, int unidade, int lote)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Execute(_command.AdicionaBloqueioUnidadeLote, new
                           {
                               @id_unidade = unidade,
                               @id_lote = lote
                           }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public void AtualizarSituacaoLote(string ibge, int idLote, int situacao)
        //{
        //    try
        //    {
        //        Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
        //                   conn.Execute(_command.AtualizaSituacaoLote, new
        //                   {
        //                       @flg_bloqueado = situacao,
        //                       @id = idLote
        //                   }));
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public void Editar(string ibge, LoteImunobiologico model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.Execute(_command.Editar, new
                          {
                              @lote = model.lote.ToUpper(),
                              @id_produto = model.id_produto,
                              @id_produtor = model.id_produtor,
                              @inativo = "F",
                              @validade = model.validade,
                              @id_apresentacao = model.id_apresentacao,
                              @id = model.id
                          }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LoteImunobiologico GetLoteById(string ibge, int id)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                     conn.QueryFirstOrDefault<dynamic>(_command.GetLoteById, new
                                     {
                                         @id = id
                                     }));

                var itemfinal = new LoteImunobiologico()
                {
                    id = item.ID,
                    lote = item.LOTE,
                    id_produto = item.ID_PRODUTO,
                    id_produtor = item.ID_PRODUTOR,
                    validade = item.VALIDADE,
                    id_apresentacao = item.ID_APRESENTACAO,
                    flg_bloqueado = item.FLG_BLOQUEADO,
                    vacinaapresentacao = new VacinaApresentacao()
                    {
                        id = item.ID_APRESENTACAO,
                        descricao = item.DESCRICAO,
                        quantidade = item.QUANTIDADE
                    }
                };
                return itemfinal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<LoteImunobiologico> GetLoteByImunobiologico(string ibge, int produto, int unidade)
        {
            try
            {
                var listaprincipal = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                            conn.Query<dynamic>(_command.GetLoteByImunobiologico, new
                            {
                                @produto = produto,
                                @id_unidade = unidade
                            }).ToList());

                var lista = listaprincipal.Select(x => new LoteImunobiologico
                {
                    id = x.ID,
                    lote = x.LOTE,
                    validade = x.VALIDADE,
                    id_produtor = x.ID_PRODUTOR,
                    id_apresentacao = x.ID_APRESENTACAO,
                    apresentacao_descricao = x.APRESENTACAO,
                    quantidade = x.QUANTIDADE,
                    produtor = new Produtor
                    {
                        id = x.ID_PRODUTOR,
                        nome = x.NOME_PRODUTOR
                    },
                    flg_bloqueado = x.FLG_BLOQUEADO

                }).ToList();

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LoteImunobiologico GetLoteByLote(string ibge, string lote, int produtor, int produto)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                            conn.QueryFirstOrDefault<LoteImunobiologico>(_command.GetLoteByLote, new
                            {
                                @lote = lote,
                                @produto = produtor,
                                @produtor = produto
                            }));
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<LoteImunobiologico> GetLoteByProdutor(string ibge, int produtor)
        {
            try
            {
                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Query<LoteImunobiologico>(_command.GetLoteByProdutor, new
                           {
                               @id_produtor = produtor
                           }).ToList());

                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<LoteImunobiologico> GetLoteByUnidade(string ibge, int unidade, int produto)
        {
            try
            {
                var itens = Helpers.HelperConnection.ExecuteCommand<List<LoteImunobiologico>>(ibge, conn =>
                            conn.Query<LoteImunobiologico>(_command.GetLoteByUnidade, new
                            {
                                @id_unidade = unidade,
                                @id_produto = produto,
                                @id_unidade1 = unidade
                            }).ToList());

                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<LoteImunobiologico> GetLoteEstoqueByImunobiologico(string ibge, string filtro, int unidade)
        {
            try
            {
                var listaprincipal = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                            conn.Query<dynamic>(_command.GetLoteEstoqueByImunobiologico.Replace("@filtro", filtro), new
                            {
                                @id_unidade = unidade
                            }).ToList());

                var lista = listaprincipal.Select(x => new LoteImunobiologico
                {
                    id = x.ID,
                    lote = x.LOTE,
                    validade = x.VALIDADE,
                    id_produtor = x.ID_PRODUTOR,
                    id_apresentacao = x.ID_APRESENTACAO,
                    apresentacao_descricao = x.APRESENTACAO,
                    quantidade = x.QUANTIDADE,
                    produtor = new Produtor
                    {
                        id = x.ID_PRODUTOR,
                        nome = x.NOME_PRODUTOR
                    },
                    flg_bloqueado = x.FLG_BLOQUEADO,
                    id_produto = x.ID_PRODUTO
                }).ToList();

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetNewId(string ibge)
        {
            try
            {
                var id = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.QueryFirstOrDefault<int>(_command.GetNewId));
                return id;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MovLoteViewModel GetPrimeiroMovimentoLote(string ibge, int? id_produto, int? id_unidade, string lote)
        {
            try
            {
                var datalote = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.QueryFirstOrDefault<DateTime?>(_command.GetPrimeiroMovimentoLote, new
                           {
                               @id_produto = id_produto,
                               @id_unidade = id_unidade,
                               @lote = lote
                           }));

                var movlote = new MovLoteViewModel()
                {
                    data = datalote
                };

                return movlote;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insert(string ibge, LoteImunobiologico model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Execute(_command.Insert, new
                           {
                               @id = model.id,
                               @lote = model.lote.ToUpper(),
                               @id_produto = model.id_produto,
                               @id_produtor = model.id_produtor,
                               @inativo = "F",
                               @validade = model.validade,
                               @id_apresentacao = model.id_apresentacao
                           }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RemoveBloqueioUnidadeLote(string ibge, int unidade, int lote)
        {
            try
            {

                var sql = _command.RemoveBloqueioUnidadeLote.Replace("@id_unidade", unidade.ToString()).Replace("@id_lote", lote.ToString());

                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.Execute(sql, new
                          {
                              @id_unidade = unidade,
                              @id_lote = lote
                          }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
