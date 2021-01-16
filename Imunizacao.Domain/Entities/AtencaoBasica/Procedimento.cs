using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Entities.AtencaoBasica
{
    public class Procedimento
    {
        public string codigo { get; set; }
        public string nome { get; set; }
        public string cod_grupo { get; set; }
        public string cod_subgrupo { get; set; }
        public string cod_forma_org { get; set; }
        public string cod_origem { get; set; }
        public string cod_pesq_1 { get; set; }
        public string cod_pesq_2 { get; set; }
        public string mod_atendimento { get; set; }
        public string complexidade { get; set; }
        public int? dias_permanencia { get; set; }
        public string ptos_ato { get; set; }
        public int? qtde_maxima { get; set; }
        public double? vlr_sa { get; set; }
        public double? vlr_sh { get; set; }
        public double? vlr_sp { get; set; }
        public double? vlr_sadt { get; set; }
        public double? vlr_toth { get; set; }
        public double? vlr_tota { get; set; }
        public string num_port_ini { get; set; }
        public DateTime? dt_vig_ini { get; set; }
        public string num_port_fim { get; set; }
        public DateTime? dt_vig_fim { get; set; }
        public int? idade_min { get; set; }
        public int? idade_max { get; set; }
        public string tp_financiamento { get; set; }
        public string usuario_inc { get; set; }
        public DateTime? datahora_inc { get; set; }
        public string sexo { get; set; }
        public string permanencia { get; set; }
        public string descricao { get; set; }
        public string codproc { get; set; }
        public string urgencia { get; set; }
        public string valida_idade { get; set; }
        public string codigo_pai { get; set; }
        public string csi_gerar_producao { get; set; }
        public int? csi_codmed { get; set; }
        public string csi_prof_pad_producao { get; set; }
        public string csi_cbo { get; set; }
        public int? csi_coduni { get; set; }
        public int? cod_tipo_financiamento { get; set; }
        public string flg_proc_cbo { get; set; }
        public int? id_competencia { get; set; }
    }
}
