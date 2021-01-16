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

        public List<Produto> GetImunobiologico(string ibge, bool orderAbrev)
        {
            try
            {
                var lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                            conn.Query<Produto>(_vacinacommand.GetImunobiologico).ToList());

                if (orderAbrev)
                    lista = lista.OrderBy(x => x.abreviatura).ToList();
                else
                    lista = lista.OrderBy(x => x.nome).ToList();

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

        public int? GetNewId(string ibge)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                         conn.QueryFirstOrDefault<int>(_vacinacommand.GetNewId));

                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insert(string ibge, Produto produto)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                        conn.Execute(_vacinacommand.Insert, new
                        {
                            @id = produto.id,
                            @nome = produto.nome,
                            @abreviatura = produto.abreviatura,
                            @sigla = produto.sigla,
                            @id_unidade = produto.id_unidade,
                            @id_classe = produto.id_classe,
                            @id_imunobiologico = produto.id_imunobiologico,
                            @id_via_adm = produto.id_via_adm
                        }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(string ibge, Produto produto)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                        conn.Execute(_vacinacommand.Update, new
                        {
                            @nome = produto.nome,
                            @abreviatura = produto.abreviatura,
                            @sigla = produto.sigla,
                            @id_unidade = produto.id_unidade,
                            @id_classe = produto.id_classe,
                            @id_imunobiologico = produto.id_imunobiologico,
                            @id_via_adm = produto.id_via_adm,
                            @id = produto.id
                        }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Produto GetProdutoById(string ibge, int id)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                         conn.QueryFirstOrDefault<Produto>(_vacinacommand.GetProdutoById, new { @id = id }));

                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(string ibge, int id)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                         conn.Execute(_vacinacommand.Delete, new { @id = id }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
