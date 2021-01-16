using Dapper;
using Imunizacao.Domain.Commands;
using Imunizacao.Domain.Entities;
using Imunizacao.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Imunizacao.Domain.Infra.Repositories
{
    public class FaixaEtariaRepository : IFaixaEtariaRepository
    {
        public IFaixaEtariaCommand _faixaCommand;
        public FaixaEtariaRepository(IFaixaEtariaCommand _command)
        {
            _faixaCommand = _command;
        }

        public List<FaixaEtaria> GetAll(string ibge)
        {
            try
            {
                var lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                         conn.Query<FaixaEtaria>(_faixaCommand.GetAll).ToList());
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
