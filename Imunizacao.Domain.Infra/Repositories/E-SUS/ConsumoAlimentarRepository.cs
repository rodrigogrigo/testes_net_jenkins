using Dapper;
using RgCidadao.Domain.Commands.E_SUS;
using RgCidadao.Domain.Entities.E_SUS;
using RgCidadao.Domain.Repositories.E_SUS;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Infra.Repositories.E_SUS
{
    public class ConsumoAlimentarRepository : IConsumoAlimentarRepository
    {
        public IConsumoAlimentarCommand _command;
        public ConsumoAlimentarRepository(IConsumoAlimentarCommand command)
        {
            _command = command;
        }

        //public void Editar(string ibge, ConsumoAlimentar model)
        //{
        //    try
        //    {
        //        Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
        //                       conn.Execute(_command.Editar, new
        //                       {
        //                           @id_profissional = model.id_profissional,
        //                           @data_atendimento = model.data_atendimento,
        //                           @id_cidadao = model.id_cidadao,
        //                           @num_cartao_sus = model.num_cartao_sus,
        //                           @nome_cidadao = model.nome_cidadao,
        //                           @data_nascimento = model.data_nascimento,
        //                           @sexo = model.sexo,
        //                           @local_atendimento = model.local_atendimento,
        //                           @flg_m6_leite_peito = model.flg_m6_leite_peito,
        //                           @flg_m6_mingau = model.flg_m6_mingau,
        //                           @flg_m6_agua_cha = model.flg_m6_agua_cha,
        //                           @flg_m6_leite_vaca = model.flg_m6_leite_vaca,
        //                           @flg_m6_formula_infantil = model.flg_m6_formula_infantil,
        //                           @flg_m6_suco_fruta = model.flg_m6_suco_fruta,
        //                           @flg_m6_fruta = model.flg_m6_fruta,
        //                           @flg_m6_comida_sal = model.flg_m6_comida_sal,
        //                           @flg_m6_outros_alimentos = model.flg_m6_outros_alimentos,
        //                           @flg_d6a23_leite_peito = model.flg_d6a23_leite_peito,
        //                           @flg_d6a23_fruta = model.flg_d6a23_fruta,
        //                           @flg_d6a23_fruta_qtd = model.flg_d6a23_fruta_qtd,
        //                           @flg_d6a23_comida_sal = model.flg_d6a23_comida_sal,
        //                           @flg_d6a23_comida_sal_qtd = model.flg_d6a23_comida_sal_qtd,
        //                           @flg_d6a23_comida_sal_oferecida = model.flg_d6a23_comida_sal_oferecida,
        //                           @flg_d6a23_outro_leite = model.flg_d6a23_outro_leite,
        //                           @flg_d6a23_mingau_leite = model.flg_d6a23_mingau_leite,
        //                           @flg_d6a23_iorgute = model.flg_d6a23_iorgute,
        //                           @flg_d6a23_legumes = model.flg_d6a23_legumes,
        //                           @flg_d6a23_vegetal = model.flg_d6a23_vegetal,
        //                           @flg_d6a23_verdura = model.flg_d6a23_verdura,
        //                           @flg_d6a23_carne = model.flg_d6a23_carne,
        //                           @flg_d6a23_figado = model.flg_d6a23_figado,
        //                           @flg_d6a23_feijao = model.flg_d6a23_feijao,
        //                           @flg_d6a23_arroz = model.flg_d6a23_arroz,
        //                           @flg_d6a23_hamburguer = model.flg_d6a23_hamburguer,
        //                           @flg_d6a23_bebida_adocada = model.flg_d6a23_bebida_adocada,
        //                           @flg_d6a23_macarrao_instantaneo = model.flg_d6a23_macarrao_instantaneo,
        //                           @flg_d6a23_biscoito_recheado = model.flg_d6a23_biscoito_recheado,
        //                           @flg_m24_refeicao_tv = model.flg_m24_refeicao_tv,
        //                           @flg_m24_cafe_manha = model.flg_m24_cafe_manha,
        //                           @flg_m24_lanche_manha = model.flg_m24_lanche_manha,
        //                           @flg_m24_almoco = model.flg_m24_almoco,
        //                           @flg_m24_lanche_tarde = model.flg_m24_lanche_tarde,
        //                           @flg_m24_jantar = model.flg_m24_jantar,
        //                           @flg_m24_ceia = model.flg_m24_ceia,
        //                           @flg_m24_feijao = model.flg_m24_feijao,
        //                           @flg_m24_fruta = model.flg_m24_fruta,
        //                           @flg_m24_verdura = model.flg_m24_verdura,
        //                           @flg_m24_hamburguer = model.flg_m24_hamburguer,
        //                           @flg_m24_bebidas_adocada = model.flg_m24_bebidas_adocada,
        //                           @flg_m24_macarrao_instantaneo = model.flg_m24_macarrao_instantaneo,
        //                           @flg_m24_biscoito_recheado = model.flg_m24_biscoito_recheado,
        //                           @uuid = model.uuid,
        //                           @data_alteracao_serv = model.data_alteracao_serv,
        //                           @id_esus_exportacao_item = model.id_esus_exportacao_item,
        //                           @id_unidade = model.id_unidade,
        //                           @id_usuario = model.id_usuario,
        //                           @id = model.id
        //                       }));
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public void Excluir(string ibge, int id)
        //{
        //    try
        //    {
        //        Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
        //                       conn.Execute(_command.Delete, new
        //                       {
        //                           @id = id
        //                       }));
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public List<ConsumoAlimentar> GetAllPagination(string ibge, string filtro, int page, int pagesize)
        //{
        //    throw new NotImplementedException();
        //}

        //public ConsumoAlimentar GetById(string ibge, int id)
        //{
        //    try
        //    {
        //        var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
        //                       conn.QueryFirstOrDefault<ConsumoAlimentar>(_command.GetById, new
        //                       {
        //                           @id = id
        //                       }));
        //        return item;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public int GetCountAll(string ibge, string filtro)
        //{
        //    try
        //    {
        //        int count = 0;
        //        if (!string.IsNullOrWhiteSpace(filtro))
        //        {
        //            count = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
        //                       conn.QueryFirstOrDefault<int>(_command.GetCountAll.Replace("@filtro", filtro)));
        //        }
        //        else
        //        {
        //            count = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
        //                     conn.QueryFirstOrDefault<int>(_command.GetCountAll.Replace("@filtro", string.Empty)));
        //        }

        //        return count;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public int GetNewId(string ibge)
        //{
        //    try
        //    {
        //        var id = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
        //                       conn.QueryFirstOrDefault<int>(_command.GetNewId));

        //        return id;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public void Inserir(string ibge, ConsumoAlimentar model)
        //{
        //    try
        //    {
        //        Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
        //                       conn.Execute(_command.Insert, new
        //                       {
        //                           @id = model.id,
        //                           @id_profissional = model.id_profissional,
        //                           @data_atendimento = model.data_atendimento,
        //                           @id_cidadao = model.id_cidadao,
        //                           @num_cartao_sus = model.num_cartao_sus,
        //                           @nome_cidadao = model.nome_cidadao,
        //                           @data_nascimento = model.data_nascimento,
        //                           @sexo = model.sexo,
        //                           @local_atendimento = model.local_atendimento,
        //                           @flg_m6_leite_peito = model.flg_m6_leite_peito,
        //                           @flg_m6_mingau = model.flg_m6_mingau,
        //                           @flg_m6_agua_cha = model.flg_m6_agua_cha,
        //                           @flg_m6_leite_vaca = model.flg_m6_leite_vaca,
        //                           @flg_m6_formula_infantil = model.flg_m6_formula_infantil,
        //                           @flg_m6_suco_fruta = model.flg_m6_suco_fruta,
        //                           @flg_m6_fruta = model.flg_m6_fruta,
        //                           @flg_m6_comida_sal = model.flg_m6_comida_sal,
        //                           @flg_m6_outros_alimentos = model.flg_m6_outros_alimentos,
        //                           @flg_d6a23_leite_peito = model.flg_d6a23_leite_peito,
        //                           @flg_d6a23_fruta = model.flg_d6a23_fruta,
        //                           @flg_d6a23_fruta_qtd = model.flg_d6a23_fruta_qtd,
        //                           @flg_d6a23_comida_sal = model.flg_d6a23_comida_sal,
        //                           @flg_d6a23_comida_sal_qtd = model.flg_d6a23_comida_sal_qtd,
        //                           @flg_d6a23_comida_sal_oferecida = model.flg_d6a23_comida_sal_oferecida,
        //                           @flg_d6a23_outro_leite = model.flg_d6a23_outro_leite,
        //                           @flg_d6a23_mingau_leite = model.flg_d6a23_mingau_leite,
        //                           @flg_d6a23_iorgute = model.flg_d6a23_iorgute,
        //                           @flg_d6a23_legumes = model.flg_d6a23_legumes,
        //                           @flg_d6a23_vegetal = model.flg_d6a23_vegetal,
        //                           @flg_d6a23_verdura = model.flg_d6a23_verdura,
        //                           @flg_d6a23_carne = model.flg_d6a23_carne,
        //                           @flg_d6a23_figado = model.flg_d6a23_figado,
        //                           @flg_d6a23_feijao = model.flg_d6a23_feijao,
        //                           @flg_d6a23_arroz = model.flg_d6a23_arroz,
        //                           @flg_d6a23_hamburguer = model.flg_d6a23_hamburguer,
        //                           @flg_d6a23_bebida_adocada = model.flg_d6a23_bebida_adocada,
        //                           @flg_d6a23_macarrao_instantaneo = model.flg_d6a23_macarrao_instantaneo,
        //                           @flg_d6a23_biscoito_recheado = model.flg_d6a23_biscoito_recheado,
        //                           @flg_m24_refeicao_tv = model.flg_m24_refeicao_tv,
        //                           @flg_m24_cafe_manha = model.flg_m24_cafe_manha,
        //                           @flg_m24_lanche_manha = model.flg_m24_lanche_manha,
        //                           @flg_m24_almoco = model.flg_m24_almoco,
        //                           @flg_m24_lanche_tarde = model.flg_m24_lanche_tarde,
        //                           @flg_m24_jantar = model.flg_m24_jantar,
        //                           @flg_m24_ceia = model.flg_m24_ceia,
        //                           @flg_m24_feijao = model.flg_m24_feijao,
        //                           @flg_m24_fruta = model.flg_m24_fruta,
        //                           @flg_m24_verdura = model.flg_m24_verdura,
        //                           @flg_m24_hamburguer = model.flg_m24_hamburguer,
        //                           @flg_m24_bebidas_adocada = model.flg_m24_bebidas_adocada,
        //                           @flg_m24_macarrao_instantaneo = model.flg_m24_macarrao_instantaneo,
        //                           @flg_m24_biscoito_recheado = model.flg_m24_biscoito_recheado,
        //                           @uuid = model.uuid,
        //                           @data_alteracao_serv = model.data_alteracao_serv,
        //                           @id_esus_exportacao_item = model.id_esus_exportacao_item,
        //                           @id_unidade = model.id_unidade,
        //                           @id_usuario = model.id_usuario
        //                       }));
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
