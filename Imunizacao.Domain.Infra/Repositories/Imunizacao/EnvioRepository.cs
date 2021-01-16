using Dapper;
using RgCidadao.Domain.Commands.Imunizacao;
using RgCidadao.Domain.Entities.Imunizacao;
using RgCidadao.Domain.Repositories.Imunizacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RgCidadao.Domain.Infra.Repositories.Imunizacao
{
    public class EnvioRepository : IEnvioRepository
    {
        private IEnvioCommand _command;
        private IEntradaProdutoItemCommand _commandentradaitem;
        public EnvioRepository(IEnvioCommand command, IEntradaProdutoItemCommand commandentradaitem)
        {
            _command = command;
            _commandentradaitem = commandentradaitem;
        }

        public void Delete(string ibge, int id)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.Execute(_command.Delete, new
                          {
                              @id = id
                          }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteItem(string ibge, int iditem)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.Execute(_command.DeleteItem, new
                          {
                              @id = iditem
                          }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EnvioItem> GetAllItensByPai(string ibge, int idpai)
        {
            try
            {
                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                  conn.Query<EnvioItem>(_command.GetAllItensByPai, new
                                  {
                                      @id = idpai
                                  }).ToList());
                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Envio> GetAllPagination(string ibge, int page, int pagesize, string filtro)
        {
            try
            {
                var sql = _command.GetAllPagination;
                if (!string.IsNullOrWhiteSpace(filtro))
                    sql = sql.Replace("@filtro", filtro);
                else
                    sql = sql.Replace("@filtro", string.Empty);

                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                conn.Query<Envio>(sql, new
                                {
                                    @pagesize = pagesize,
                                    @page = page
                                }).ToList());
                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetCountAll(string ibge, string filtro)
        {
            try
            {
                var sql = _command.GetCountAll;
                if (!string.IsNullOrWhiteSpace(filtro))
                    sql = sql.Replace("@filtro", filtro);
                else
                    sql = sql.Replace("@filtro", string.Empty);

                var contagem = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                conn.QueryFirstOrDefault<int>(sql));
                return contagem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Envio GetEnvioById(string ibge, int id)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                conn.QueryFirstOrDefault<Envio>(_command.GetEnvioById, new
                                {
                                    @id = id
                                }));
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EnvioItem GetItemById(string ibge, int iditem)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                               conn.QueryFirstOrDefault<EnvioItem>(_command.GetItemById, new
                               {
                                   @id = iditem
                               }));
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int? GetNewIdEnvio(string ibge)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                               conn.QueryFirstOrDefault<int>(_command.GetNewIdEnvio));
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Envio> GetTranferenciaByUnidadeDestino(string ibge, int id)
        {
            try
            {
                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                              conn.Query<Envio>(_command.GetTranferenciaByUnidadeDestino, new { @id = id })).ToList();
                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Envio InsertOrUpdate(string ibge, Envio model)
        {
            try
            {
                if (model.id == null)
                    model.id = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                             conn.QueryFirstOrDefault<int>(_command.GetNewIdEnvio));

                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                              conn.Execute(_command.InsertOrUpdate, new
                              {
                                  @id = model.id,
                                  @id_unidade_origem = model.id_unidade_origem,
                                  @id_unidade_destino = model.id_unidade_destino,
                                  @data_envio = model.data_envio,
                                  @id_usuario = model.id_usuario,
                                  @status = model.status,
                                  @observacao = model.observacao
                              }));

                foreach (var item in model.Itens)
                {
                    if (item.id == null)
                        item.id = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                            conn.QueryFirstOrDefault<int>(_command.GetNewIdItem));

                    item.id_envio = model.id;

                    //recupera o valor do ultimo lote inserido

                    var valorultimaentrada = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.QueryFirstOrDefault<dynamic>(_commandentradaitem.GetUltimaEntradaItemByLote, new
                          {
                              @id_lote = item.id_lote
                          }));
                    item.valor = valorultimaentrada.VALOR;

                    Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                             conn.Execute(_command.InsertOrUpdateItens, new
                             {
                                 @id = item.id,
                                 @id_envio = item.id_envio,
                                 @id_lote = item.id_lote,
                                 @qtde_frascos = item.qtde_frascos,
                                 @id_apresentacao = item.id_apresentacao,
                                 @valor = item.valor
                             }));
                }

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateStatusEnviado(string ibge, int id)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                             conn.Execute(_command.UpdateStatusEnviado, new { @id = id }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ValidaEstoqueItensEnvio(string ibge, int id, int unidade)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                            conn.QueryFirstOrDefault<int>(_command.ValidaEstoqueItensEnvio, new
                            {
                                @id = id,
                                @id_unidade = unidade
                            }));
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
