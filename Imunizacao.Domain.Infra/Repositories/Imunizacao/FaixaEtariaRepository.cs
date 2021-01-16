using Dapper;
using RgCidadao.Domain.Commands.Imunizacao;
using RgCidadao.Domain.Entities.Imunizacao;
using RgCidadao.Domain.Repositories.Imunizacao;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RgCidadao.Domain.Infra.Repositories.Imunizacao
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
