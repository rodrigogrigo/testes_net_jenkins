using Dapper;
using RgCidadao.Domain.Commands.Imunizacao;
using RgCidadao.Domain.Entities.Imunizacao;
using RgCidadao.Domain.Repositories.Imunizacao;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RgCidadao.Domain.Infra.Repositories.Imunizacao
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

        public void GeraAprazamento(string ibge)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                        conn.Execute(_command.GeraAprazamentoPopGeral));

                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                        conn.Execute(_command.GeraAprazamentoFeminino));

                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                        conn.Execute(_command.GeraAprazamentoMasculino));

                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                        conn.Execute(_command.GeraAprazamentoDeficiencia));

                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                        conn.Execute(_command.GeraAprazamentoGestacao));

                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                        conn.Execute(_command.GeraAprazamentoPuerpera));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GeraAprazamentoCalendarioBasico(string ibge, int id_calendario, int publico_alvo)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                      conn.Execute(_command.GeraAprazamentoCalendarioBasico, new
                      {
                          @id_calendario_basico = id_calendario,
                          @publico_alvo = publico_alvo
                      }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GeraAprazamentoDeficienciaByIndividuo(string ibge, int id)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                     conn.Execute(_command.GeraAprazamentoDeficienciaByIndividuo, new
                     {
                         @id_individuo = id
                     }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GeraAprazamentoFemininoByIndividuo(string ibge, int id)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                   conn.Execute(_command.GeraAprazamentoFemininoByIndividuo, new
                   {
                       @id_individuo = id
                   }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GeraAprazamentoGestacaoByIndividuo(string ibge, int id)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                  conn.Execute(_command.GeraAprazamentoGestacaoByIndividuo, new
                  {
                      @id_individuo = id
                  }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GeraAprazamentoMasculinoByIndividuo(string ibge, int id)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                 conn.Execute(_command.GeraAprazamentoMasculinoByIndividuo, new
                 {
                     @id_individuo = id
                 }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GeraAprazamentoPopGeralByIndividuo(string ibge, int id)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                conn.Execute(_command.GeraAprazamentoPopGeralByIndividuo, new
                {
                    @id_individuo = id
                }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GeraAprazamentoPuerperaByIndividuo(string ibge, int id)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
              conn.Execute(_command.GeraAprazamentoPuerperaByIndividuo, new
              {
                  @id_individuo = id
              }));
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
