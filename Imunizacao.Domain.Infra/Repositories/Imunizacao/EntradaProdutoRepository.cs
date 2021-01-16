using Dapper;
using RgCidadao.Domain.Commands.Imunizacao;
using RgCidadao.Domain.Entities.Imunizacao;
using RgCidadao.Domain.Repositories.Imunizacao;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RgCidadao.Domain.Infra.Repositories.Imunizacao
{
    public class EntradaProdutoRepository : IEntradaProdutoRepository
    {
        public IEntradaProdutoCommand _entradaCommand;
        public EntradaProdutoRepository(IEntradaProdutoCommand command)
        {
            _entradaCommand = command;
        }

        public void AtualizaValor(string ibge, int id, double valor)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                    conn.Execute(_entradaCommand.AtualizaValor, new
                    {
                        @valor = valor,
                        @id = id
                    }));
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
                     conn.Execute(_entradaCommand.Delete, new { @id = id }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EntradaVacina GetEntradaById(string ibge, int id)
        {
            try
            {
                var model = new EntradaVacina();
                model = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                    conn.QueryFirstOrDefault<EntradaVacina>(_entradaCommand.GetEntradaById, new { @id = id }));

                return model;
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
                int id = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                            conn.QueryFirstOrDefault<int>(_entradaCommand.GetNewId));

                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertOrUpdate(string ibge, EntradaVacina model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                            conn.Execute(_entradaCommand.InsertOrUpdate, new
                            {
                                @id = model.id,
                                @id_unidade = model.id_unidade,
                                @numero_nota = model.numero_nota,
                                @data = model.data,
                                @usuario = model.usuario,
                                @obs = model.obs,
                                @valor_total = model.valor_total,
                                @id_fornecedor = model.id_fornecedor,
                                @id_envio = model.id_envio
                            }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetCountEntradaVacina(string ibge, string unidade, string filtro)
        {
            try
            {
                var count = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                               conn.QueryFirstOrDefault<int>(_entradaCommand.GetCountEntradaVacina.Replace("@filtro", $" {filtro} "), new { @unidade = unidade }));

                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EntradaVacina> GetEntradaVacinaByUnidade(string ibge, string unidade, int page, int pagesize, string filtro)
        {
            try
            {
                var lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                conn.Query<EntradaVacina>(_entradaCommand.GetEntradaVacinaByUnidade.Replace("@filtro", $" {filtro} "),
                                                                    new { @unidade = unidade, @page = page, @pagesize = pagesize }).ToList());

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }    
    }
}
