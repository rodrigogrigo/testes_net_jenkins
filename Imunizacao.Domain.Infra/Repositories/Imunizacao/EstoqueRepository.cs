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
    public class EstoqueRepository : IEstoqueRepository
    {
        public IEstoqueCommand _command;
        public EstoqueRepository(IEstoqueCommand command)
        {
            _command = command;
        }

        public List<UnidadeEstoqueViewModel> GetAllUnidadeWithEstoque(string ibge, int produto, int usuario)

        {
            try
            {
                var lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.Query<UnidadeEstoqueViewModel>(_command.GetAllUnidadeWithEstoque, new
                          {
                              @user = usuario,
                              @produto = produto
                          }).ToList());

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AuditoriaEstoque> GetAuditoria(string ibge, int id_produto, DateTime? datainicial, DateTime? datafinal, string lote, int? unidade, int page, int pagesize, int? produtor)
        {
            try
            {
                var lista = new List<AuditoriaEstoque>();

                string filtro = string.Empty;
                if (!string.IsNullOrWhiteSpace(lote))
                    filtro += $@"AND MP.LOTE = '{lote}' AND MP.ID_PRODUTOR = {produtor}";

                if (unidade != null)
                    filtro += $"AND MP.ID_UNIDADE = {unidade}";

                if (string.IsNullOrWhiteSpace(filtro))
                {
                    lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                       conn.Query<AuditoriaEstoque>(_command.GetAuditoria.Replace("@filtro", string.Empty), new
                                       {
                                           @id_produto = id_produto,
                                           @data_inicial = datainicial,
                                           @data_final = datafinal,
                                           @pagesize = pagesize,
                                           @page = page
                                       }).ToList());
                }
                else
                {
                    lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                       conn.Query<AuditoriaEstoque>(_command.GetAuditoria.Replace("@filtro", filtro), new
                                       {
                                           @id_produto = id_produto,
                                           @data_inicial = datainicial,
                                           @data_final = datafinal,
                                           @pagesize = pagesize,
                                           @page = page
                                       }).ToList());
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetCountAuditoria(string ibge, int id_produto, DateTime? datainicial, DateTime? datafinal, string lote, int? unidade, int? produtor)
        {
            try
            {
                int count = 0;

                string filtro = string.Empty;
                if (!string.IsNullOrWhiteSpace(lote))
                    filtro += $@"AND MP.LOTE = '{lote}' AND MP.ID_PRODUTOR = {produtor}";


                

                if (unidade != null)
                    filtro += $"AND MP.ID_UNIDADE = {unidade}";



                if (string.IsNullOrWhiteSpace(filtro))
                {
                    count = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                       conn.QueryFirstOrDefault<int>(_command.GetCountAuditoria.Replace("@filtro", string.Empty), new
                                       {
                                           @id_produto = id_produto,
                                           @data_inicial = datainicial,
                                           @data_final = datafinal
                                       }));
                }
                else
                {
                    count = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                       conn.QueryFirstOrDefault<int>(_command.GetCountAuditoria.Replace("@filtro", filtro), new
                                       {
                                           @id_produto = id_produto,
                                           @data_inicial = datainicial,
                                           @data_final = datafinal
                                       }));
                }
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EstoqueProduto> GetEstoqueLoteByUnidadeAndProduto(string ibge, int unidade, int produto)
        {
            try
            {
                var lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                        conn.Query<EstoqueProduto>(_command.GetEstoqueLoteByUnidadeAndProduto, new
                        {
                            @id_produto = produto,
                            @id_unidade = unidade
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
