using Dapper;
using Imunizacao.Domain.Commands;
using Imunizacao.Domain.Entities;
using Imunizacao.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Imunizacao.Domain.Infra.Repositories
{
    public class RegraVacinalRepository : IRegraVacinalRepository
    {
        public IRegraVacinalCommand _command;
        public RegraVacinalRepository(IRegraVacinalCommand command)
        {
            _command = command;
        }

        public RegraVacinal GetRegraVacinalByParams(string ibge, int id_imunobiologico, int id_estrategia, int id_dose)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.QueryFirstOrDefault<RegraVacinal>(_command.GetRegraVacinalByParams, new
                          {
                              @id_imunobiologico = id_imunobiologico,
                              @id_estrategia = id_estrategia,
                              @id_dose = id_dose
                          }));
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
