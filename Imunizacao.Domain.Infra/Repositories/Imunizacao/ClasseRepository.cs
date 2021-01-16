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
    public class ClasseRepository : IClasseRepository
    {
        public IClasseCommand _command;
        public ClasseRepository(IClasseCommand command)
        {
            _command = command;
        }

        public List<Classe> GetAll(string ibge)
        {
            try
            {
                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                     conn.Query<Classe>(_command.GetAll).ToList());

                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
