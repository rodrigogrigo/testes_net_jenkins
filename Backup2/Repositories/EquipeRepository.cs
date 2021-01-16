using Dapper;
using Imunizacao.Domain.Commands;
using Imunizacao.Domain.Entities;
using Imunizacao.Domain.Repositories;
using System;

namespace Imunizacao.Domain.Infra.Repositories
{
    public class EquipeRepository : IEquipeRepository
    {
        public IEquipeCommand _equipeCommand;
        public EquipeRepository(IEquipeCommand _command)
        {
            _equipeCommand = _command;
        }

        public Equipe GetEquipeByCidadaoEstruturaNova(string ibge, int paciente)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                 conn.QueryFirstOrDefault<Equipe>(_equipeCommand.GetEquipeByCidadaoEstruturaNova, new
                                 {
                                     @id = paciente
                                 }));
                return item;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Equipe GetEquipeByCidadaoEstruturaVelha(string ibge, int paciente)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                conn.QueryFirstOrDefault<Equipe>(_equipeCommand.GetEquipeByCidadaoEstruturaVelha, new
                                {
                                    @id = paciente
                                }));
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UsaEstruturaNova(string ibge)
        {
            try
            {
                var count = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                 conn.QueryFirstOrDefault<int>(_equipeCommand.UsaEstruturaNova));
                if (count > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
