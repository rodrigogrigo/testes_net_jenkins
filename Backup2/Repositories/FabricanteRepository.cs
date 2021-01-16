using Dapper;
using Imunizacao.Domain.Commands;
using Imunizacao.Domain.Entities;
using Imunizacao.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Imunizacao.Domain.Infra.Repositories
{
    public class FabricanteRepository : IFabricanteRepository
    {
        private readonly IFabricanteCommand _fabricantecommand;

        public FabricanteRepository(IFabricanteCommand commandText)
        {
            _fabricantecommand = commandText;
        }

        public List<Fabricante> GetAll(string ibge, string filtro)
        {
            try
            {
                var fabri = new List<Fabricante>();
                if (string.IsNullOrWhiteSpace(filtro))
                {
                    fabri = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.Query<Fabricante>(_fabricantecommand.GetAll.Replace("@filtro", "")).ToList());
                }
                else
                {
                    fabri = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.Query<Fabricante>(_fabricantecommand.GetAll.Replace("@filtro", $" {filtro} ")).ToList());
                }

                return fabri;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Atualizar(string ibge, Fabricante model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Execute(_fabricantecommand.Atualizar, new
                           {
                               @nome = model.nome,
                               @abreviatura = model.abreviatura,
                               @id = model.id
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
                          conn.Execute(_fabricantecommand.Deletar, new
                          {
                              @id = id
                          }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //retorna um novo id
        public int GetNewId(string ibge)
        {
            try
            {
                int id = Helpers.HelperConnection.ExecuteCommand<int>(ibge, conn =>
                        conn.QueryFirstOrDefault<int>(_fabricantecommand.GetNewId));

                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Inserir(string ibge, Fabricante model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Execute(_fabricantecommand.Inserir, new
                           {
                               @id = model.id,
                               @nome = model.nome,
                               @abreviatura = model.abreviatura
                           }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Fabricante> GetProdutorByImunobiologico(string ibge, int imuno)
        {
            try
            {
                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.Query<Fabricante>(_fabricantecommand.GetProdutorByImunobiologico, new
                          {
                              @produto = imuno
                          }).ToList());

                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
