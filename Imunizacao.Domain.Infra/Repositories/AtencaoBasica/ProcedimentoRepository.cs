using Dapper;
using RgCidadao.Domain.Commands.AtencaoBasica;
using RgCidadao.Domain.Entities.AtencaoBasica;
using RgCidadao.Domain.Repositories.AtencaoBasica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RgCidadao.Domain.Infra.Repositories.AtencaoBasica
{
    public class ProcedimentoRepository : IProcedimentoRepository
    {
        private IProcedimentoCommand _command;
        public ProcedimentoRepository(IProcedimentoCommand command)
        {
            _command = command;
        }

        public List<Procedimento> GetProcedimentoBycbo(string ibge, string cbo)
        {
            try
            {
                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                   conn.Query<Procedimento>(_command.GetProcedimentoBycbo, new
                   {
                       @cbo = cbo
                   }).ToList());

                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Procedimento> GetProcedimentosByCompetencia022019(string ibge)
        {
            try
            {
                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                     conn.Query<Procedimento>(_command.GetProcedimentosByCompetencia022019).ToList());

                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
