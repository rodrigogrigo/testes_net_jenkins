using Dapper;
using RgCidadao.Domain.Commands.Imunizacao;
using RgCidadao.Domain.Entities.Imunizacao;
using RgCidadao.Domain.Repositories.Imunizacao;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RgCidadao.Domain.Infra.Repositories.Imunizacao
{
    public class CartaoVacinaRepository : ICartaoVacinaRepository
    {
        public ICartaoVacinaCommand _command;
        public CartaoVacinaRepository(ICartaoVacinaCommand cartaocommand)
        {
            _command = cartaocommand;
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

        public CartaoVacina GetCartaoVacinaById(string ibge, int id)
        {
            try
            {
                var model = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                            conn.QueryFirstOrDefault<CartaoVacina>(_command.GetCartaoVacinaById, new { @id = id }));
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CartaoVacina> GetCartaoVacinaByProduto(string ibge, int id_produto)
        {
            try
            {
                var model = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                             conn.Query<CartaoVacina>(_command.GetCartaoVacinaByProduto, new { @id_produto = id_produto }).ToList());
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CartaoVacina> GetCartaoVacinaByProdutor(string ibge, int id_produtor)
        {
            try
            {
                var model = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                            conn.Query<CartaoVacina>(_command.GetCartaoVacinaByProdutor, new { @id_produtor = id_produtor }).ToList());
                return model;
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

        public void Insert(string ibge, CartaoVacina model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                     conn.Execute(_command.Insert, new
                     {
                         @id = model.id,
                         @id_unidade = model.id_unidade,
                         @id_profisional = model.id_profisional,
                         @id_produto = model.id_produto,
                         @lote = model.lote,
                         @vencimento = model.vencimento,
                         @data_aplicacao = model.data_aplicacao,
                         @data_aprazamento = model.data_aprazamento,
                         @data_prevista = model.data_prevista,
                         @registro_anterior = model.registro_anterior,
                         @hanseniase = model.hanseniase,
                         @gestante = model.gestante,
                         @inadvertida = model.inadvertida,
                         @usuario = model.usuario,
                         @data_hora = model.data_hora,
                         @id_paciente = model.id_paciente,
                         @cbo = model.cbo,
                         @id_dose = model.id_dose,
                         @id_estrategia = model.id_estrategia,
                         @id_grupo_atendimento = model.id_grupo_atendimento,
                         @id_produtor = model.id_produtor,
                         @id_modivo_indicacao = model.id_modivo_indicacao,
                         @exportado = model.exportado,
                         @id_gestacao = model.id_gestacao,
                         @uuid = model.uuid,
                         @id_turno = model.id_turno,
                         @id_local_atendimento = model.id_local_atendimento,
                         @flg_viajante = model.flg_viajante,
                         @flg_puerpera = model.flg_puerpera,
                         @id_esus_exportacao_item = model.id_esus_exportacao_item,
                         @data_nascimento = model.data_nascimento,
                         @id_sexo = model.id_sexo,
                         @nome_produtor = model.nome_produtor,
                         @id_usuario_exclusao = model.id_usuario_exclusao,
                         @flg_excluido = model.flg_excluido,
                         @observacao = model.observacao,
                         @id_via_adm = model.id_via_adm,
                         @id_local_aplicacao = model.id_local_aplicacao
                     }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(string ibge, CartaoVacina model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                     conn.Execute(_command.Update, new
                     {
                         @id_unidade = model.id_unidade,
                         @id_profisional = model.id_profisional,
                         @id_produto = model.id_produto,
                         @lote = model.lote,
                         @vencimento = model.vencimento,
                         @data_aplicacao = model.data_aplicacao,
                         @data_aprazamento = model.data_aprazamento,
                         @data_prevista = model.data_prevista,
                         @registro_anterior = model.registro_anterior,
                         @hanseniase = model.hanseniase,
                         @gestante = model.gestante,
                         @inadvertida = model.inadvertida,
                         @usuario = model.usuario,
                         @data_hora = model.data_hora,
                         @id_paciente = model.id_paciente,
                         @cbo = model.cbo,
                         @id_dose = model.id_dose,
                         @id_estrategia = model.id_estrategia,
                         @id_grupo_atendimento = model.id_grupo_atendimento,
                         @id_produtor = model.id_produtor,
                         @id_modivo_indicacao = model.id_modivo_indicacao,
                         @exportado = model.exportado,
                         @id_gestacao = model.id_gestacao,
                         @uuid = model.uuid,
                         @id_turno = model.id_turno,
                         @id_local_atendimento = model.id_local_atendimento,
                         @flg_viajante = model.flg_viajante,
                         @flg_puerpera = model.flg_puerpera,
                         @id_esus_exportacao_item = model.id_esus_exportacao_item,
                         @data_nascimento = model.data_nascimento,
                         @id_sexo = model.id_sexo,
                         @nome_produtor = model.nome_produtor,
                         @id_usuario_exclusao = model.id_usuario_exclusao,
                         @flg_excluido = model.flg_excluido,
                         @observacao = model.observacao,
                         @id_via_adm = model.id_via_adm,
                         @id_local_aplicacao = model.id_local_aplicacao,
                         @id = model.id
                     }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
