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
    public class UnidadeRepository : IUnidadeRepository
    {
        private readonly IUnidadeCommand _unidadecommand;

        public UnidadeRepository(IUnidadeCommand commandText)
        {
            _unidadecommand = commandText;
        }

        public List<Unidade> GetAll(string ibge, string filtro)
        {
            try
            {
                var unidade = new List<Unidade>();
                if (string.IsNullOrWhiteSpace(filtro))
                {
                    unidade = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Query<Unidade>(_unidadecommand.GetAll.Replace("@filtro", "")).ToList());
                }
                else
                {
                    unidade = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Query<Unidade>(_unidadecommand.GetAll.Replace("@filtro", $" {filtro} ")).ToList());
                }

                return unidade;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<Unidade> GetUnidadesByUser(string ibge, int user)
        {
            try
            {
                List<Unidade> unidades = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                            conn.Query<Unidade>(_unidadecommand.GetUnidadesByUser, new { @user = user }).ToList());

                return unidades;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
