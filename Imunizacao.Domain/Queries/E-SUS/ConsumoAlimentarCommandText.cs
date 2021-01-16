using RgCidadao.Domain.Commands.E_SUS;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Queries.E_SUS
{
    public class ConsumoAlimentarCommandText : IConsumoAlimentarCommand
    {
        public string sqlGetAllPagination = $@"SELECT FIRST(@pagesize) SKIP(@page) CA.ID, M.CSI_NOMMED, CA.DATA_ATENDIMENTO, CA.NUM_CARTAO_SUS, CA.NOME_CIDADAO, CA.DATA_NASCIMENTO,
                                                      (
                                                      CASE
                                                        WHEN CA.SEXO = 0 THEN 'Masculino'
                                                        ELSE 'Feminino'
                                                      END) AS SEXO,
                                                      U.CSI_NOMUNI AS UNIDADE
                                               FROM ESUS_CONSUMO_ALIMENTAR CA
                                               JOIN TSI_MEDICOS M ON (CA.ID_PROFISSIONAL = M.CSI_CODMED)
                                               JOIN TSI_UNIDADE U ON (U.CSI_CODUNI = CA.ID_UNIDADE)
                                               JOIN TSI_MEDICOS_UNIDADE MU ON (MU.CSI_CODMED = M.CSI_CODMED AND MU.CSI_CODUNI = U.CSI_CODUNI AND MU.CSI_ATIVADO = 'T')  
                                               @filtro
                                               ORDER BY CA.ID, CA.DATA_ATENDIMENTO";
        string IConsumoAlimentarCommand.GetAllPagination { get => sqlGetAllPagination; }

        public string sqlGetCountAll = $@"SELECT count(*)
                                          FROM (SELECT CA.ID, M.CSI_NOMMED, CA.DATA_ATENDIMENTO, CA.NUM_CARTAO_SUS, CA.NOME_CIDADAO, CA.DATA_NASCIMENTO,
                                                      (
                                                      CASE
                                                        WHEN CA.SEXO = 0 THEN 'Masculino'
                                                        ELSE 'Feminino'
                                                      END) AS SEXO,
                                                      U.CSI_NOMUNI AS UNIDADE
                                               FROM ESUS_CONSUMO_ALIMENTAR CA
                                               JOIN TSI_MEDICOS M ON (CA.ID_PROFISSIONAL = M.CSI_CODMED)
                                               JOIN TSI_UNIDADE U ON (U.CSI_CODUNI = CA.ID_UNIDADE)
                                               JOIN TSI_MEDICOS_UNIDADE MU ON (MU.CSI_CODMED = M.CSI_CODMED AND MU.CSI_CODUNI = U.CSI_CODUNI AND MU.CSI_ATIVADO = 'T')  
                                               @filtro
                                               ORDER BY CA.ID, CA.DATA_ATENDIMENTO)";
        string IConsumoAlimentarCommand.GetCountAll { get => sqlGetCountAll; }

        public string sqlGetnewid = $@"SELECT GEN_ID(GEN_ESUS_CONSUMO_ALIMENTAR_ID, 1) AS VLR FROM RDB$DATABASE";
        string IConsumoAlimentarCommand.GetNewId { get => sqlGetnewid; }

        public string sqlInsert = $@"INSERT ESUS_CONSUMO_ALIMENTAR INTO(ID, ID_PROFISSIONAL, DATA_ATENDIMENTO, ID_CIDADAO, NUM_CARTAO_SUS, NOME_CIDADAO,
                                                                    DATA_NASCIMENTO, SEXO, LOCAL_ATENDIMENTO, FLG_M6_LEITE_PEITO, FLG_M6_MINGAU,
                                                                    FLG_M6_AGUA_CHA, FLG_M6_LEITE_VACA, FLG_M6_FORMULA_INFANTIL, FLG_M6_SUCO_FRUTA,
                                                                    FLG_M6_FRUTA, FLG_M6_COMIDA_SAL, FLG_M6_OUTROS_ALIMENTOS, FLG_D6A23_LEITE_PEITO,
                                                                    FLG_D6A23_FRUTA, FLG_D6A23_FRUTA_QTD, FLG_D6A23_COMIDA_SAL, FLG_D6A23_COMIDA_SAL_QTD,
                                                                    FLG_D6A23_COMIDA_SAL_OFERECIDA, FLG_D6A23_OUTRO_LEITE, FLG_D6A23_MINGAU_LEITE,
                                                                    FLG_D6A23_IORGUTE, FLG_D6A23_LEGUMES, FLG_D6A23_VEGETAL, FLG_D6A23_VERDURA,
                                                                    FLG_D6A23_CARNE, FLG_D6A23_FIGADO, FLG_D6A23_FEIJAO, FLG_D6A23_ARROZ,
                                                                    FLG_D6A23_HAMBURGUER, FLG_D6A23_BEBIDA_ADOCADA, FLG_D6A23_MACARRAO_INSTANTANEO,
                                                                    FLG_D6A23_BISCOITO_RECHEADO, FLG_M24_REFEICAO_TV, FLG_M24_CAFE_MANHA,
                                                                    FLG_M24_LANCHE_MANHA, FLG_M24_ALMOCO, FLG_M24_LANCHE_TARDE, FLG_M24_JANTAR,
                                                                    FLG_M24_CEIA, FLG_M24_FEIJAO, FLG_M24_FRUTA, FLG_M24_VERDURA, FLG_M24_HAMBURGUER,
                                                                    FLG_M24_BEBIDAS_ADOCADA, FLG_M24_MACARRAO_INSTANTANEO, FLG_M24_BISCOITO_RECHEADO,
                                                                    UUID, DATA_ALTERACAO_SERV, ID_ESUS_EXPORTACAO_ITEM, ID_UNIDADE, ID_USUARIO)
                                     VALUES (@id, @id_profissional, @data_atendimento, @id_cidadao, @num_cartao_sus, @nome_cidadao, @data_nascimento,
                                             @sexo, @local_atendimento, @flg_m6_leite_peito, @flg_m6_mingau, @flg_m6_agua_cha, @flg_m6_leite_vaca,
                                             @flg_m6_formula_infantil, @flg_m6_suco_fruta, @flg_m6_fruta, @flg_m6_comida_sal, @flg_m6_outros_alimentos,
                                             @flg_d6a23_leite_peito, @flg_d6a23_fruta, @flg_d6a23_fruta_qtd, @flg_d6a23_comida_sal,
                                             @flg_d6a23_comida_sal_qtd, @flg_d6a23_comida_sal_oferecida, @flg_d6a23_outro_leite, @flg_d6a23_mingau_leite,
                                             @flg_d6a23_iorgute, @flg_d6a23_legumes, @flg_d6a23_vegetal, @flg_d6a23_verdura, @flg_d6a23_carne,
                                             @flg_d6a23_figado, @flg_d6a23_feijao, @flg_d6a23_arroz, @flg_d6a23_hamburguer, @flg_d6a23_bebida_adocada,
                                             @flg_d6a23_macarrao_instantaneo, @flg_d6a23_biscoito_recheado, @flg_m24_refeicao_tv, @flg_m24_cafe_manha,
                                             @flg_m24_lanche_manha, @flg_m24_almoco, @flg_m24_lanche_tarde, @flg_m24_jantar, @flg_m24_ceia,
                                             @flg_m24_feijao, @flg_m24_fruta, @flg_m24_verdura, @flg_m24_hamburguer, @flg_m24_bebidas_adocada,
                                             @flg_m24_macarrao_instantaneo, @flg_m24_biscoito_recheado, @uuid, @data_alteracao_serv,
                                             @id_esus_exportacao_item, @id_unidade, @id_usuario)";
        string IConsumoAlimentarCommand.Insert { get => sqlInsert; }

        public string sqlGetById = $@"SELECT *
                                      FROM ESUS_CONSUMO_ALIMENTAR
                                      WHERE ID = @id";
        string IConsumoAlimentarCommand.GetById { get => sqlGetById; }

        public string sqlEditar = $@"UPDATE ESUS_CONSUMO_ALIMENTAR
                                     SET ID_PROFISSIONAL = @id_profissional,
                                         DATA_ATENDIMENTO = @data_atendimento,
                                         ID_CIDADAO = @id_cidadao,
                                         NUM_CARTAO_SUS = @num_cartao_sus,
                                         NOME_CIDADAO = @nome_cidadao,
                                         DATA_NASCIMENTO = @data_nascimento,
                                         SEXO = @sexo,
                                         LOCAL_ATENDIMENTO = @local_atendimento,
                                         FLG_M6_LEITE_PEITO = @flg_m6_leite_peito,
                                         FLG_M6_MINGAU = @flg_m6_mingau,
                                         FLG_M6_AGUA_CHA = @flg_m6_agua_cha,
                                         FLG_M6_LEITE_VACA = @flg_m6_leite_vaca,
                                         FLG_M6_FORMULA_INFANTIL = @flg_m6_formula_infantil,
                                         FLG_M6_SUCO_FRUTA = @flg_m6_suco_fruta,
                                         FLG_M6_FRUTA = @flg_m6_fruta,
                                         FLG_M6_COMIDA_SAL = @flg_m6_comida_sal,
                                         FLG_M6_OUTROS_ALIMENTOS = @flg_m6_outros_alimentos,
                                         FLG_D6A23_LEITE_PEITO = @flg_d6a23_leite_peito,
                                         FLG_D6A23_FRUTA = @flg_d6a23_fruta,
                                         FLG_D6A23_FRUTA_QTD = @flg_d6a23_fruta_qtd,
                                         FLG_D6A23_COMIDA_SAL = @flg_d6a23_comida_sal,
                                         FLG_D6A23_COMIDA_SAL_QTD = @flg_d6a23_comida_sal_qtd,
                                         FLG_D6A23_COMIDA_SAL_OFERECIDA = @flg_d6a23_comida_sal_oferecida,
                                         FLG_D6A23_OUTRO_LEITE = @flg_d6a23_outro_leite,
                                         FLG_D6A23_MINGAU_LEITE = @flg_d6a23_mingau_leite,
                                         FLG_D6A23_IORGUTE = @flg_d6a23_iorgute,
                                         FLG_D6A23_LEGUMES = @flg_d6a23_legumes,
                                         FLG_D6A23_VEGETAL = @flg_d6a23_vegetal,
                                         FLG_D6A23_VERDURA = @flg_d6a23_verdura,
                                         FLG_D6A23_CARNE = @flg_d6a23_carne,
                                         FLG_D6A23_FIGADO = @flg_d6a23_figado,
                                         FLG_D6A23_FEIJAO = @flg_d6a23_feijao,
                                         FLG_D6A23_ARROZ = @flg_d6a23_arroz,
                                         FLG_D6A23_HAMBURGUER = @flg_d6a23_hamburguer,
                                         FLG_D6A23_BEBIDA_ADOCADA = @flg_d6a23_bebida_adocada,
                                         FLG_D6A23_MACARRAO_INSTANTANEO = @flg_d6a23_macarrao_instantaneo,
                                         FLG_D6A23_BISCOITO_RECHEADO = @flg_d6a23_biscoito_recheado,
                                         FLG_M24_REFEICAO_TV = @flg_m24_refeicao_tv,
                                         FLG_M24_CAFE_MANHA = @flg_m24_cafe_manha,
                                         FLG_M24_LANCHE_MANHA = @flg_m24_lanche_manha,
                                         FLG_M24_ALMOCO = @flg_m24_almoco,
                                         FLG_M24_LANCHE_TARDE = @flg_m24_lanche_tarde,
                                         FLG_M24_JANTAR = @flg_m24_jantar,
                                         FLG_M24_CEIA = @flg_m24_ceia,
                                         FLG_M24_FEIJAO = @flg_m24_feijao,
                                         FLG_M24_FRUTA = @flg_m24_fruta,
                                         FLG_M24_VERDURA = @flg_m24_verdura,
                                         FLG_M24_HAMBURGUER = @flg_m24_hamburguer,
                                         FLG_M24_BEBIDAS_ADOCADA = @flg_m24_bebidas_adocada,
                                         FLG_M24_MACARRAO_INSTANTANEO = @flg_m24_macarrao_instantaneo,
                                         FLG_M24_BISCOITO_RECHEADO = @flg_m24_biscoito_recheado,
                                         UUID = @uuid,
                                         DATA_ALTERACAO_SERV = @data_alteracao_serv,
                                         ID_ESUS_EXPORTACAO_ITEM = @id_esus_exportacao_item,
                                         ID_UNIDADE = @id_unidade,
                                         ID_USUARIO = @id_usuario
                                     WHERE ID = @id";
        string IConsumoAlimentarCommand.Editar { get => sqlEditar; }

        public string sqlExcluir = $@"DELETE
                                      FROM ESUS_CONSUMO_ALIMENTAR
                                      WHERE ID = @id";
        string IConsumoAlimentarCommand.Delete { get => sqlExcluir; }
    }
}
