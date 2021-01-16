using Dapper;
using Imunizacao.Domain.Commands;
using Imunizacao.Domain.Entities;
using Imunizacao.Domain.Repositories;
using Imunizacao.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Imunizacao.Domain.Infra.Repositories
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
