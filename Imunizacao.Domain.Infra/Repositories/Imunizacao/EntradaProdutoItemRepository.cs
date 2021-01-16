using Dapper;
using RgCidadao.Domain.Commands.Imunizacao;
using RgCidadao.Domain.Entities.Imunizacao;
using RgCidadao.Domain.Repositories.Imunizacao;
using RgCidadao.Domain.ViewModels.Imunizacao;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RgCidadao.Domain.Infra.Repositories.Imunizacao
{
    public class EntradaProdutoItemRepository : IEntradaProdutoItemRepository
    {
        public IEntradaProdutoItemCommand _itemCommand;

        public EntradaProdutoItemRepository(IEntradaProdutoItemCommand command)
        {
            _itemCommand = command;
        }

        public void Delete(string ibge, int id)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                    conn.Execute(_itemCommand.Delete, new { @id = id }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteAllByPai(string ibge, int id)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                conn.Execute(_itemCommand.DeleteAllByPai, new { @id = id }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EntradaVacinaItem> GetAllItensByPai(string ibge, int id)
        {
            try
            {
                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                conn.Query<dynamic>(_itemCommand.GetAllItensByPai, new { @id = id }).ToList());

                var lista = new List<EntradaVacinaItem>();
                foreach (var item in itens)
                {
                    var itemproduto = new EntradaVacinaItem
                    {
                        id = item.ID,
                        id_entrada_produto = item.ID_ENTRADA_PRODUTO,
                        id_unidade = item.ID_UNIDADE,
                        validade = item.VALIDADE,
                        id_apresentacao = item.ID_APRESENTACAO,
                        qtde_frascos = item.QTDE_FRASCOS,
                        valor = item.VALOR,
                        abreviatura = item.ABREVIATURA,
                        sigla = item.SIGLA,
                        Lote = new LoteImunobiologico
                        {
                            id = item.ID_LOTE,
                            lote = item.LOTE,
                            quantidade_apresentacao = item.QUANTIDADE_APRESENTACAO
                        },
                        produto = new Produto
                        {
                            id = item.ID_PRODUTO,
                            nome = item.NOME_PRODUTO
                        },
                        forma_apresentacao = item.FORMA_APRESENTACAO,
                        qtde_doses = item.QTDE_DOSES
                    };

                    lista.Add(itemproduto);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EntradaVacinaItem GetEntradaItemById(string ibge, int id)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                conn.QueryFirstOrDefault<dynamic>(_itemCommand.GetEntradaItemById, new { @id = id }));

                var itemproduto = new EntradaVacinaItem
                {
                    id = item.ID,
                    id_entrada_produto = item.ID_ENTRADA_PRODUTO,
                    id_unidade = item.ID_UNIDADE,
                    validade = item.VALIDADE,
                    id_apresentacao = item.ID_APRESENTACAO,
                    qtde_frascos = item.QTDE_FRASCOS,
                    valor = item.VALOR,
                    abreviatura = item.ABREVIATURA,
                    sigla = item.SIGLA,
                    Lote = new LoteImunobiologico
                    {
                        id = item.ID_LOTE,
                        lote = item.LOTE
                    },
                    produto = new Produto
                    {
                        id = item.ID_PRODUTO,
                        nome = item.NOME_PRODUTO
                    },
                    qtde_doses = item.QTDE_DOSES
                };

                return itemproduto;
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
                                    conn.QueryFirstOrDefault<int>(_itemCommand.GetNewId));
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EntradaVacinaItem GetUltimaEntradaItemByLote(string ibge, int? lote)
        {
            try
            {
                var valor = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                   conn.QueryFirstOrDefault<EntradaVacinaItem>(_itemCommand.GetUltimaEntradaItemByLote, new
                                   {
                                       @id_lote = lote
                                   }));

                return valor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public double? GetValorUltimaEntradaLote(string ibge, string lote)
        {
            try
            {
                var valor = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                   conn.QueryFirstOrDefault<double?>(_itemCommand.GetValorUltimaEntradaLote, new
                                   {
                                       @lote = lote
                                   }));

                return valor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertOrUpdate(string ibge, EntradaVacinaItem model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                    conn.Execute(_itemCommand.InsertOrUpdate, new
                                    {
                                        @id = model.id,
                                        @id_entrada_produto = model.id_entrada_produto,
                                        @id_unidade = model.id_unidade,
                                        @validade = model.validade,
                                        @id_apresentacao = model.id_apresentacao,
                                        @qtde_frascos = model.qtde_frascos,
                                        @valor = model.valor,
                                        @id_lote = model.id_lote,
                                        @qtde_doses = model.qtde_doses
                                    }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool PossuiMovimentoByEntradaItem(string ibge, int id)
        {
            try
            {
                var movimento = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                  conn.Query(_itemCommand.PossuiMovimentoByEstradaItem, new
                                  {
                                      @id = id
                                  }).ToList());

                if (movimento.Count != 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ExcluirMovimentoProdutoById(string ibge, int id_entrada_produto_item, string tabela_origem)
        {
            try
            {
                var lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.Execute(_itemCommand.Delete, new
                          {
                              @tabela_origem = tabela_origem,
                              @id_entrada_produto_item = id_entrada_produto_item
                          }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EntradaProdutoItem GetEntradaProdutoItemById(string ibge, int id_entrada_produto_item)
        {

            try
            {
                var Item = Helpers.HelperConnection.ExecuteCommand<EntradaProdutoItem>(ibge, conn =>
                                        conn.QueryFirstOrDefault<EntradaProdutoItem>(_itemCommand.GetEntradaProdutoItemById, new
                                        { @id = id_entrada_produto_item }));

                return Item;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<EntradaProdutoItem> GetItensEntradaProdutoByIdEntradaProduto(string ibge, int id_entrada_produto)
        {
            try
            {
                var lista = new List<EntradaProdutoItem>();

                lista = Helpers.HelperConnection.ExecuteCommand<List<EntradaProdutoItem>>(ibge, conn =>
                              conn.Query<EntradaProdutoItem>(_itemCommand.GetItensEntradaProdutoByIdEntradaProduto, new
                              {
                                  @id_entrada_produto = id_entrada_produto
                              }).ToList());


                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
