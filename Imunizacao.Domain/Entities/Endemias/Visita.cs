using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Entities.Endemias
{
    public class Visita
    {
        public Visita()
        {
            itens = new List<Coleta>();
        }

        public int? id { get; set; }
        public int? id_visita { get; set; }
        public int? id_visita_imovel { get; set; }
        public string uuid_registro_mobile { get; set; }
        public int? id_imovel { get; set; }
        public string quarteirao_logradouro { get; set; }
        public DateTime? data_hora_entrada { get; set; }
        public DateTime? data_hora_saida { get; set; }
        public DateTime? data_hora_registro { get; set; }
        public string competencia { get; set; }
        public int? desfecho { get; set; }
        public int? atividade { get; set; }
        public int? tipo_visita { get; set; }
        public int? encontrou_foco { get; set; }
        public string numero_logradouro { get; set; }
        public string lado { get; set; }
        public int? deposito_inspecionado_a1 { get; set; }
        public int? deposito_inspecionado_a2 { get; set; }
        public string complemento_logradouro { get; set; }
        public int? deposito_inspecionado_b { get; set; }
        public int? deposito_inspecionado_c { get; set; }
        public string csi_nombai { get; set; }
        public int? deposito_inspecionado_d1 { get; set; }
        public int? deposito_inspecionado_d2 { get; set; }
        public string sequencia_quarteirao { get; set; }
        public int? sequencia_numero { get; set; }
        public int? deposito_inspecionado_e { get; set; }
        public int? deposito_eliminado { get; set; }
        public string pendencia_descricao { get; set; }
        public string trabalho_educativo { get; set; }
        public string trabalho_mecanico { get; set; }
        public string trabalho_quimico { get; set; }
        public int? trat_focal_larvi1_tipo { get; set; }
        public double? trat_focal_larvi1_qtd_gramas { get; set; }
        public double? trat_focal_larvi1_qtd_dep_trat { get; set; }
        //public int? trat_focal_larvi2_tipo { get; set; }
        //public double? trat_focal_larvi2_qtd_gramas { get; set; }
        //public int? trat_focal_larvi2_qtd_dep_trat { get; set; }
        public int? trat_perifocal_adult_tipo { get; set; }
        public int? trat_perifocal_adult_qtd_carga { get; set; }
        public string latitude_cadastro { get; set; }
        public string longitude_cadastro { get; set; }
        public string latitude_foco { get; set; }
        public string longitude_foco { get; set; }
        public int? id_profissional { get; set; }
        public DateTime? data_alteracao_serv { get; set; }
        public string csi_nommed { get; set; }
        public int? numero_tubito { get; set; }
        public int? id_esus_exportacao_item { get; set; }
        public int? id_estabelecimento { get; set; }
        public List<Coleta> itens { get; set; }
        public string csi_nomend { get; set; }
        public string num_ciclo { get; set; }
        public string nome_fantasia_apelido { get; set; }
        public string razao_social_nome { get; set; }
        public int? id_ciclo { get; set; }
        public string logradouro { get; set; }
    }
}
