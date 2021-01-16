using Dapper;
using RgCidadao.Domain.Commands.AtencaoBasica;
using RgCidadao.Domain.Entities.AtencaoBasica;
using RgCidadao.Domain.Repositories.AtencaoBasica;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RgCidadao.Domain.Infra.Repositories.AtencaoBasica
{
    public class EstabelecimentoSaudeRepository : IEstabelecimentoSaudeRepository
    {
        private IEstabelecimentoSaudeCommand _command;
        public EstabelecimentoSaudeRepository(IEstabelecimentoSaudeCommand command)
        {
            _command = command;
        }

        public EstabelecimentoSaude GetById(string ibge, int id)
        {
            try
            {
                var estabelecimentoSaude = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                   conn.QueryFirstOrDefault<EstabelecimentoSaude>(_command.GetById, new
                   {
                       @id = id
                   }));

                return estabelecimentoSaude;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EstabelecimentoSaude> GetAll(string ibge)
        {
            try
            {
                var estabelecimentosSaude = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                    conn.Query<EstabelecimentoSaude>(_command.GetAll).ToList()
                );

                return estabelecimentosSaude;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
