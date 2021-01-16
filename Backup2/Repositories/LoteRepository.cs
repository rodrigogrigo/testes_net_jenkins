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
    public class LoteRepository : ILoteRepository
    {
        public ILoteCommand _command;
        public LoteRepository(ILoteCommand command)
        {
            _command = command;
        }

        public void AtualizarSituacaoLote(string ibge, int idLote, int situacao)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Execute(_command.AtualizaSituacaoLote, new
                           {
                               @flg_bloqueado = situacao,
                               @id = idLote
                           }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<LoteImunobiologico> GetLoteByImunobiologico(string ibge, int produto)
        {
            try
            {
                var listaprincipal = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                            conn.Query<dynamic>(_command.GetLoteByImunobiologico, new { @produto = produto }).ToList());

                var lista = listaprincipal.Select(x => new LoteImunobiologico
                {
                    id = x.ID,
                    lote = x.LOTE,
                    validade = x.VALIDADE,
                    id_produtor = x.ID_PRODUTOR,
                    id_apresentacao = x.ID_APRESENTACAO,
                    apresentacao = x.APRESENTACAO,
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

        public List<LoteImunobiologico> GetLoteByUnidade(string ibge, int unidade, int produto)
        {
            try
            {
                var itens = Helpers.HelperConnection.ExecuteCommand<List<LoteImunobiologico>>(ibge, conn =>
                            conn.Query<LoteImunobiologico>(_command.GetLoteByUnidade, new
                            {
                                @id_unidade = unidade,
                                @id_produto = produto
                            }).ToList());

                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<LoteImunobiologico> GetLoteEstoqueByImunobiologico(string ibge, string filtro)
        {
            try
            {
                var listaprincipal = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                            conn.Query<dynamic>(_command.GetLoteEstoqueByImunobiologico.Replace("@filtro", filtro)).ToList());

                var lista = listaprincipal.Select(x => new LoteImunobiologico
                {
                    id = x.ID,
                    lote = x.LOTE,
                    validade = x.VALIDADE,
                    id_produtor = x.ID_PRODUTOR,
                    id_apresentacao = x.ID_APRESENTACAO,
                    apresentacao = x.APRESENTACAO,
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
    }
}
