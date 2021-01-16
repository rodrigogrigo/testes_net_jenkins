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
    public class UnidadeMedRepository : IUnidadeMedRepository
    {
        public IUnidadeMedCommand _command;
        public UnidadeMedRepository(IUnidadeMedCommand command)
        {
            _command = command;
        }

        public List<UnidadeMedida> GetAll(string ibge)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                       conn.Query<UnidadeMedida>(_command.GetAll).ToList());

                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
