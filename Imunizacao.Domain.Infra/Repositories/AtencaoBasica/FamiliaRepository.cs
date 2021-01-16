using Dapper;
using RgCidadao.Domain.Commands.AtencaoBasica;
using RgCidadao.Domain.Entities.AtencaoBasica;
using RgCidadao.Domain.Repositories.AtencaoBasica;
using RgCidadao.Domain.ViewModels.AtencaoBasica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RgCidadao.Domain.Infra.Repositories.AtencaoBasica
{
    public class FamiliaRepository : IFamiliaRepository
    {
        private IFamiliaCommand _command;
        public FamiliaRepository(IFamiliaCommand command)
        {
            _command = command;
        }

        public void AtualizaCadPacFamilia(string ibge, int? id_familia, int id_responsavel)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.Execute(_command.AtualizaCadPacFamilia, new
                          {
                              @id_familia = id_familia,
                              @id = id_responsavel
                          }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AtualizaResponsavelFamilia(string ibge, int id, int responsavel)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                         conn.Execute(_command.AtualizaResponsavelFamilia, new
                         {
                             @responsavel = responsavel,
                             @id = id
                         }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FamiliaProntuarioQtdeViewModel FamiliaProntuarioQtde(string ibge, int profissional)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.Query<int>(_command.GetProntuarioUso, new
                           {
                               @id_profissional = profissional
                           }).ToList());

                var qtdeprontuario = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                     conn.QueryFirstOrDefault<int>(_command.GetQtdMaximaFamiliaMicroarea));
                var familia = new FamiliaProntuarioQtdeViewModel();
                familia.max_familias = qtdeprontuario;
                familia.prontuarios_em_uso = item;

                return familia;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Familia GetFamiliaById(string ibge, int id)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                    conn.QueryFirstOrDefault<Familia>(_command.GetFamiliaById, new
                    {
                        @id = id
                    }));

                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetIndividuoFamilia(string ibge, int id)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.QueryFirstOrDefault<int>(_command.GetIndividuoFamilia, new
                           {
                               @id = id
                           }));
                return item;
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
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                            conn.QueryFirstOrDefault<int>(_command.GetNewId));

                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Insert(string ibge, Familia model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.Execute(_command.Insert, new
                          {
                              @id = model.id,
                              @num_prontuario_familiar = model.num_prontuario_familiar,
                              @data_inclusao = model.data_inclusao,
                              @situacao_cadastro = model.situacao_cadastro,
                              @id_domicilio = model.id_domicilio,
                              @id_responsavel = model.id_responsavel,
                              @id_usuario = model.id_usuario,
                              @renda_familiar_sal_min = model.renda_familiar_sal_min,
                              @situacao_moradia = model.situacao_moradia,
                              @area_prod_rural = model.area_prod_rural,
                              @reside_desde = model.reside_desde,
                              @trat_agua = model.trat_agua,
                              @destino_lixo = model.destino_lixo,
                              @tel_res = model.tel_res,
                              @tel_ref = model.tel_ref,
                              @gato = model.gato,
                              @cachorro = model.cachorro,
                              @de_criacao = model.de_criacao,
                              @outros = model.outros,
                              @passaro = model.passaro,
                              @qtd_animais = model.qtd_animais
                          }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(string ibge, Familia model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                         conn.Execute(_command.Update, new
                         {
                             @num_prontuario_familiar = model.num_prontuario_familiar,
                             @data_inclusao = model.data_inclusao,
                             @situacao_cadastro = model.situacao_cadastro,
                             @id_domicilio = model.id_domicilio,
                             @id_responsavel = model.id_responsavel,
                             @id_usuario = model.id_usuario,
                             @renda_familiar_sal_min = model.renda_familiar_sal_min,
                             @situacao_moradia = model.situacao_moradia,
                             @area_prod_rural = model.area_prod_rural,
                             @reside_desde = model.reside_desde,
                             @trat_agua = model.trat_agua,
                             @destino_lixo = model.destino_lixo,
                             @tel_res = model.tel_res,
                             @tel_ref = model.tel_ref,
                             @gato = model.gato,
                             @cachorro = model.cachorro,
                             @de_criacao = model.de_criacao,
                             @outros = model.outros,
                             @passaro = model.passaro,
                             @qtd_animais = model.qtd_animais,
                             @id = model.id
                         }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AtualizaFamiliaOutraArea(string ibge, int id)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                        conn.Execute(_command.AtualizaFamiliaOutraArea, new
                        {
                            @id = id
                        }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AtualizaFamiliaDomicilio(string ibge, int familia, int domicilio)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                      conn.Execute(_command.AtualizaFamiliaDomicilio, new
                      {
                          @id_domicilio = domicilio,
                          @id = familia
                      }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Familia> GetFamiliaByIndividuoResponsavel(string ibge, int responsavel)
        {
            try
            {
                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                      conn.Query<Familia>(_command.GetFamiliaByIndividuoResponsavel, new
                      {
                          @responsavel = responsavel
                      }).ToList());

                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
