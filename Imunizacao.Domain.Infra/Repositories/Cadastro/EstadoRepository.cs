using Dapper;
using RgCidadao.Domain.Commands.Cadastro;
using RgCidadao.Domain.Entities.Cadastro;
using RgCidadao.Domain.Repositories.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RgCidadao.Domain.Infra.Repositories.Cadastro
{
    public class EstadoRepository : IEstadoRepository
    {
        private IEstadoCommand _command;
        public EstadoRepository(IEstadoCommand command)
        {
            _command = command;
        }

        public List<Estado> GetAll(string ibge)
        {
            try
            {
                var lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                         conn.Query<Estado>(_command.GetAll)).ToList();

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Estado GetEstadoById(string ibge, int id)
        {
            try
            {
                var lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                         conn.QueryFirstOrDefault<Estado>(_command.GetEstadoById, new
                         {
                             @id = id
                         }));

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
