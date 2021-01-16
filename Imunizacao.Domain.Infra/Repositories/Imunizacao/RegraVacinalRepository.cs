using Dapper;
using RgCidadao.Domain.Commands.Imunizacao;
using RgCidadao.Domain.Entities.Imunizacao;
using RgCidadao.Domain.Repositories.Imunizacao;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Infra.Repositories.Imunizacao
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
