using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.ViewModels.Endemias
{
    public class AntivetorialAnaliticoViewModel
    {
        public int? id_profissional { get; set; }
        public string csi_nommed { get; set; }
        public int? csi_codcid { get; set; }
        public string csi_nomcid { get; set; }
        public int? csi_codbai { get; set; }
        public string csi_nombai { get; set; }
        public string categoria { get; set; }
        public string quarteirao_logradouro { get; set; }
        public int? sequencia_quarteirao { get; set; }
        public int? sequencia_numero { get; set; }
        public string csi_nomend { get; set; }
        public string numero_logradouro { get; set; }
        public string complemento_logradouro { get; set; }
        public int? numero_tubito { get; set; }
        public int? deposito_eliminado { get; set; }
        public int? desfecho { get; set; }
        public string tipo_imovel { get; set; }
        public string tempendencia { get; set; }
        public DateTime? data_hora_entrada { get; set; }
        public int? tipo_visita { get; set; }
        public int? di_a1 { get; set; }
        public int? di_a2 { get; set; }
        public int? di_b { get; set; }
        public int? di_c { get; set; }
        public int? di_d1 { get; set; }
        public int? di_d2 { get; set; }
        public int? di_e { get; set; }
        public int? trat_focal_larvi1_tipo { get; set; }
        public int? trat_focal_larvi2_tipo { get; set; }
        public int? trat_perifocal_adult_tipo { get; set; }
        public double? trat_focal_larvi1_qtd_gramas { get; set; }
        public double? trat_focal_larvi2_qtd_gramas { get; set; }
        public double? trat_focal_larvi1_qtd_dep_trat { get; set; }
        public double? trat_focal_larvi2_qtd_dep_trat { get; set; }
        public double? trat_perifocal_adult_qtd_carga { get; set; }
        public string imovel_trat { get; set; }
        public int? countimov_trat { get; set; }
        public string imovel_inspc { get; set; }
        public int? countimov_insp { get; set; }
        public string tipo_visitastr { get; set; }
        public int? a_inicial { get; set; }
        public int? a_final { get; set; }
        public int? a_total { get; set; }
    }
}
