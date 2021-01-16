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
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly IProdutoCommand _vacinacommand;

        public ProdutoRepository(IProdutoCommand commandText)
        {
            _vacinacommand = commandText;
        }

        public List<Produto> GetAll(string ibge, string filtro, int page, int pagesize)
        {
            try
            {
                var vacina = new List<Produto>();
                if (string.IsNullOrWhiteSpace(filtro))
                {
                    vacina = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                conn.Query<Produto>(_vacinacommand.GetAll.Replace("@filtro", ""), new { @pagesize = pagesize, @page = page }).ToList());
                }
                else
                {
                    vacina = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                               conn.Query<Produto>(_vacinacommand.GetAll.Replace("@filtro", $" {filtro} "), new { @pagesize = pagesize, @page = page }).ToList());
                }

                return vacina;
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
                var count = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                              conn.QueryFirstOrDefault<int>(_vacinacommand.GetCountAll.Replace("@filtro", $" {filtro} ")));

                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Produto> GetImunobiologico(string ibge)
        {
            try
            {
                var lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                            conn.Query<Produto>(_vacinacommand.GetImunobiologico).ToList());

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EstoqueProduto GetEstoqueImunobiologicoByParams(string ibge, int produto, int unidade, int produtor, string lote)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                            conn.QueryFirstOrDefault<EstoqueProduto>(_vacinacommand.GetEstoqueImunobiologicoByParams, new
                            {
                                @lote = lote,
                                @id_produto = produto,
                                @id_unidade = unidade,
                                @id_produtor = produtor
                            }));

                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Produto> GetImunobiologicoEstoqueByUnidade(string ibge, int unidade)
        {
            try
            {
                var lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.Query<Produto>(_vacinacommand.GetImunobiologicoEstoqueByUnidade, new { @id_unidade = unidade }).ToList());

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Produto> GetProdutoByEstrategia(string ibge, int estrategia)
        {
            try
            {
                var lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                         conn.Query<Produto>(_vacinacommand.GetProdutoByEstrategia, new { @id_estrategia = estrategia }).ToList());

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      
    }
}
