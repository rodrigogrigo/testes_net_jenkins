using Dapper;
using Imunizacao.Domain.Commands;
using Imunizacao.Domain.Entities;
using Imunizacao.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Imunizacao.Domain.Infra.Repositories
{
    public class EstrategiaRepository : IEstrategiaRepository
    {
        public IEstrategiaCommand _command;
        public EstrategiaRepository(IEstrategiaCommand command)
        {
            _command = command;
        }

        public List<Estrategia> GetEstrategiasByProduto(string ibge, int id)
        {
            try
            {
                var lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                            conn.Query<Estrategia>(_command.GetEstrategiaByProduto, new { @id = id }).ToList());

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Estrategia> GetAll(string ibge)
        {
            try
            {
                var lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Query<Estrategia>(_command.GetAll).ToList());

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
