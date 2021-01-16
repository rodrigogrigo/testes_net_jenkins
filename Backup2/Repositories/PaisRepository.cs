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
    public class PaisRepository : IPaisRepository
    {
        private readonly IPaisCommand _paiscommand;

        public PaisRepository(IPaisCommand commandText)
        {
            _paiscommand = commandText;
        }

        public List<Pais> GetAll(string ibge)
        {
            try
            {
                var lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.Query<Pais>(_paiscommand.GetAll).ToList());

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
