using Dapper;
using Imunizacao.Domain.Commands;
using Imunizacao.Domain.Entities;
using Imunizacao.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Imunizacao.Domain.Infra.Repositories
{
    public class AprazamentoRepository : IAprazamentoRepository
    {
        public IAprazamentoCommand _command;
        public AprazamentoRepository(IAprazamentoCommand command)
        {
            _command = command;
        }

        public void Delete(string ibge, int id)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                            conn.Execute(_command.Delete, new { @id = id }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GeraAprazamento(string ibge, int id, int publicoAlvo)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                        conn.Query<Aprazamento>(_command.GeraAprazamento, new
                        {
                            @id_individuo = id,
                            @publico_alvo = publicoAlvo
                        })).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Aprazamento> GetAprazamentoByCalendarioBasico(string ibge, int id)
        {
            try
            {
                var lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                         conn.Query<Aprazamento>(_command.GetAprazamentoByCalendarioBasico, new
                         {
                             @calendario = id
                         })).ToList();

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Aprazamento> GetAprazamentoByCidadao(string ibge, int id)
        {
            try
            {
                var lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Query<Aprazamento>(_command.GetAprazamentoByCidadao, new { @id = id })).ToList();

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetNewId(string ibge)
        {
            try
            {
                var id = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.QueryFirstOrDefault<int>(_command.GetNewId));
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insert(string ibge, Aprazamento model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Execute(_command.Insert, new
                           {
                               @id = model.id_aprazamento,
                               @id_individuo = model.id_individuo,
                               @data_limite = model.data_limite,
                               @id_vacinados = model.id_vacinados,
                               @id_produto = model.id_produto,
                               @id_dose = model.id_dose
                           }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool PermiteExcluirAprazamento(string ibge, int id)
        {
            try
            {
                var permite = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                            conn.QueryFirstOrDefault<int?>(_command.PermiteDeletar, new { @id = id }));
                if (permite == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateVacinados(string ibge, int? id_vacinados, int id)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                         conn.Execute(_command.UpdateAprazamentoVacinados, new
                         {
                             @id_vacinados = id_vacinados,
                             @id = id
                         }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
