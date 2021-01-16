using Dapper;
using RgCidadao.Domain.Commands.Cadastro;
using RgCidadao.Domain.Entities.Cadastro;
using RgCidadao.Domain.Repositories.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RgCidadao.Domain.Infra.Repositories.Cadastro
{
    public class EquipeRepository : IEquipeRepository
    {
        public IEquipeCommand _equipeCommand;
        public EquipeRepository(IEquipeCommand _command)
        {
            _equipeCommand = _command;
        }

        public List<Equipe> GetEquipeByBairro(string ibge, int id_equipe)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                               conn.Query<Equipe>(_equipeCommand.GetEquipesByBairro, new
                               {
                                   @id_bairro = id_equipe
                               })).ToList();
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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

        public List<Equipe> GetEquipeByPerfil(string ibge, string filtro)
        {
            try
            {
                string sql = _equipeCommand.GetEquipeByPerfil.Replace("@filtro", filtro);

                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                               conn.Query<Equipe>(sql)).ToList();
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Equipe> GetEquipeByProfissional(string ibge, int profissional)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                               conn.Query<Equipe>(_equipeCommand.GetEquipeByProfissional, new
                               {
                                   @id_profissional = profissional
                               })).ToList();
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Equipe> GetEquipeByUnidade(string ibge, int unidade)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                               conn.Query<Equipe>(_equipeCommand.GetEquipeByUnidade, new
                               {
                                   @id = unidade
                               })).ToList();
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
