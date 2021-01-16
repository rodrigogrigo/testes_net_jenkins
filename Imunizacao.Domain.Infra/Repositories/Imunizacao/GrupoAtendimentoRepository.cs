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
