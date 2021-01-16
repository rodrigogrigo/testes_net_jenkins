using Dapper;
using Imunizacao.Domain.Commands;
using Imunizacao.Domain.Entities;
using Imunizacao.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Imunizacao.Domain.Infra.Repositories
{
    public class MovImunobiologicoRepository : IMovImunobiologicoRepository
    {
        private readonly IMovImunobiologicoCommand _movcommand;

        public MovImunobiologicoRepository(IMovImunobiologicoCommand commandText)
        {
            _movcommand = commandText;
        }

        public int GetCountAll(string ibge, string filtro, int unidade)
        {
            try
            {
                int mov = 0;

                if (string.IsNullOrWhiteSpace(filtro))
                {
                    mov = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.QueryFirstOrDefault<int>(_movcommand.GetCountAll.Replace("@filtro", ""), new { @unidade = unidade }));

                }
                else
                {
                    mov = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.QueryFirstOrDefault<int>(_movcommand.GetCountAll.Replace("@filtro", filtro), new { @unidade = unidade }));
                }

                return mov;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MovimentoImunobiologico GetMovimentoById(string ibge, int id)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand<MovimentoImunobiologico>(ibge, conn =>
                        conn.QueryFirstOrDefault<MovimentoImunobiologico>(_movcommand.GetById, new { @id = id }));

                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MovimentoImunobiologico> GetMovimentoByUnidade(string ibge, int unidade, string filtro, int page, int pagesize)
        {
            try
            {
                var lista = new List<MovimentoImunobiologico>();

                if (string.IsNullOrWhiteSpace(filtro))
                {
                    lista = Helpers.HelperConnection.ExecuteCommand<List<MovimentoImunobiologico>>(ibge, conn =>
                              conn.Query<MovimentoImunobiologico>(_movcommand.GetMovimentoByUnidade.Replace("@filtro", ""), new
                              {
                                  @pagesize = pagesize,
                                  @page = page,
                                  @unidade = unidade
                              }).ToList());

                }
                else
                {
                    lista = Helpers.HelperConnection.ExecuteCommand<List<MovimentoImunobiologico>>(ibge, conn =>
                             conn.Query<MovimentoImunobiologico>(_movcommand.GetMovimentoByUnidade.Replace("@filtro", filtro), new
                             {
                                 @pagesize = pagesize,
                                 @page = page,
                                 @unidade = unidade
                             }).ToList());
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Inserir(string ibge, MovimentoImunobiologico model)
        {
            try
            {
                var lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.Execute(_movcommand.Inserir, new
                          {
                              @id = model.id,
                              @ano_apuracao = model.ano_apuracao,
                              @mes_apuracao = model.mes_apuracao,
                              @id_unidade = model.id_unidade,
                              @lote = model.lote,
                              @id_produto = model.id_produto,
                              @id_produtor = model.id_produtor,
                              @id_apresentacao = model.id_apresentacao,
                              @qtde = model.qtde,
                              @usuario = model.id_usuario,
                              @data = model.data,
                              @id_fornecedor = model.id_fornecedor,
                              @observacao = model.observacao,
                              @tipo_lancamento = model.tipo_lancamento,
                              @tipo_perca = model.tipo_perca,
                              @qtde_frascos = model.qtde_frascos,
                              @id_unidade_envio = model.id_unidade_envio
                          }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Atualizar(string ibge, MovimentoImunobiologico model)
        {
            try
            {
                var lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.Execute(_movcommand.Atualizar, new
                          {
                              @qtde = model.qtde,
                              @usuario = model.id_usuario,
                              @data = model.data,
                              @id_fornecedor = model.id_fornecedor,
                              @observacao = model.observacao,
                              @tipo_lancamento = model.tipo_lancamento,
                              @tipo_perca = model.tipo_perca,
                              @qtde_frascos = model.qtde_frascos,
                              @id_unidade_envio = model.id_unidade_envio,
                              @id = model.id
                          }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Excluir(string ibge, int id)
        {
            try
            {
                var lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.Execute(_movcommand.Excluir, new
                          {
                              @id = id
                          }));
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
                          conn.QueryFirstOrDefault<int>(_movcommand.GetNewId));
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
