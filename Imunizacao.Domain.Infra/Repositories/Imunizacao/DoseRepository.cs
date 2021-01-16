using Dapper;
using RgCidadao.Domain.Commands.Imunizacao;
using RgCidadao.Domain.Entities.Imunizacao;
using RgCidadao.Domain.Repositories.Imunizacao;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RgCidadao.Domain.Infra.Repositories.Imunizacao
{
    public class DoseRepository : IDoseRepository
    {
        public IDoseCommand _doseCommand;
        public DoseRepository(IDoseCommand _command)
        {
            _doseCommand = _command;
        }

        public List<Dose> GetAll(string ibge)
        {
            try
            {
                var lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                               conn.Query<Dose>(_doseCommand.GetAll)).ToList();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Dose> GetDoseByEstrategiaAndProduto(string ibge, int estrategia, int produto)
        {
            try
            {
                var lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                               conn.Query<Dose>(_doseCommand.GetDoseByEstrategiaAndProduto, new
                               {
                                   @id_imunobiologico = produto,
                                   @id_estrategia = estrategia
                               })).ToList();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
