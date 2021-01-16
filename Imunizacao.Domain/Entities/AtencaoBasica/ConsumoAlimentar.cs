using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Entities.AtencaoBasica
{
    public class ConsumoAlimentar
    {
        public int? id { get; set; }
        public int? id_profissional { get; set; }
        public DateTime? data_atendimento { get; set; }
        public int? id_cidadao { get; set; }
        public string num_cartao_sus { get; set; }
        public string nome_cidadao { get; set; }
        public DateTime? data_nascimento { get; set; }
        public int? sexo { get; set; }
        public int? local_atendimento { get; set; }
        public int? flg_m6_leite_peito { get; set; }
        public int? flg_m6_mingau { get; set; }
        public int? flg_m6_agua_cha { get; set; }
        public int? flg_m6_leite_vaca { get; set; }
        public int? flg_m6_formula_infantil { get; set; }
        public int? flg_m6_suco_fruta { get; set; }
        public int? flg_m6_fruta { get; set; }
        public int? flg_m6_comida_sal { get; set; }
        public int? flg_m6_outros_alimentos { get; set; }
        public int? flg_d6a23_leite_peito { get; set; }
        public int? flg_d6a23_fruta { get; set; }
        public int? flg_d6a23_fruta_qtd { get; set; }
        public int? flg_d6a23_comida_sal { get; set; }
        public int? flg_d6a23_comida_sal_qtd { get; set; }
        public int? flg_d6a23_comida_sal_oferecida { get; set; }
        public int? flg_d6a23_outro_leite { get; set; }
        public int? flg_d6a23_mingau_leite { get; set; }
        public int? flg_d6a23_iorgute { get; set; }
        public int? flg_d6a23_legumes { get; set; }
        public int? flg_d6a23_vegetal { get; set; }
        public int? flg_d6a23_verdura { get; set; }
        public int? flg_d6a23_carne { get; set; }
        public int? flg_d6a23_figado { get; set; }
        public int? flg_d6a23_feijao { get; set; }
        public int? flg_d6a23_arroz { get; set; }
        public int? flg_d6a23_hamburguer { get; set; }
        public int? flg_d6a23_bebida_adocada { get; set; }
        public int? flg_d6a23_macarrao_instantaneo { get; set; }
        public int? flg_d6a23_biscoito_recheado { get; set; }
        public int? flg_m24_refeicao_tv { get; set; }
        public int? flg_m24_cafe_manha { get; set; }
        public int? flg_m24_lanche_manha { get; set; }
        public int? flg_m24_almoco { get; set; }
        public int? flg_m24_lanche_tarde { get; set; }
        public int? flg_m24_jantar { get; set; }
        public int? flg_m24_ceia { get; set; }
        public int? flg_m24_feijao { get; set; }
        public int? flg_m24_fruta { get; set; }
        public int? flg_m24_verdura { get; set; }
        public int? flg_m24_hamburguer { get; set; }
        public int? flg_m24_bebidas_adocada { get; set; }
        public int? flg_m24_macarrao_instantaneo { get; set; }
        public int? flg_m24_biscoito_recheado { get; set; }
        public string uuid { get; set; }
        public DateTime? data_alteracao_serv { get; set; }
        public int? id_esus_exportacao_item { get; set; }
        public int? id_unidade { get; set; }
        public int? id_usuario { get; set; }
        public int? id_equipe { get; set; }
        public int? id_controle_sincronizacao_lote { get; set; }
    }
}
