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
    public class GrupoAtendimentoRepository : IGrupoAtendimentoRepository
    {
        public IGrupoAtendimentoCommand _command;
        public GrupoAtendimentoRepository(IGrupoAtendimentoCommand repository)
        {
            _command = repository;
        }

        public List<GrupoAtendimento> GetAll(string ibge)
        {
            try
            {
                var lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                               conn.Query<GrupoAtendimento>(_command.GetAll)).ToList();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
