using System;
using System.Collections.Generic;
using System.Text;

namespace Imunizacao.Domain.ViewModels
{
    public class FornecedorViewModel
    {
        public int csi_codfor { get; set; }
        public string csi_nomfan { get; set; }
        public string csi_pessoa { get; set; }
        public DateTime csi_datcad { get; set; }
        public string csi_endfor { get; set; }
        public string csi_baifor { get; set; }
        public string csi_cepfor { get; set; }
        public string csi_telfor { get; set; }
        public string csi_contat { get; set; }
        public string csi_emafor { get; set; }
        public int? csi_sitfor { get; set; }
        public string csi_cgcfor { get; set; }
        public string csi_insfor { get; set; }
        public string csi_nomusu { get; set; }
        public string csi_codcid { get; set; }
        public string csi_nomfor { get; set; }
        public string csi_fabricante { get; set; }
        public DateTime? csi_datainc { get; set; }
        public DateTime? csi_dataalt { get; set; }
        public string csi_codforseller { get; set; }
        public double csi_saldo { get; set; }
        public string csi_tipfor { get; set; }
        public string csi_impvlrreq { get; set; }
        public string csi_srv_consorcio { get; set; }
        public int csi_port_consorcio { get; set; }
        public string csi_user_consorcio { get; set; }
        public string csi_pwd_consorcio { get; set; }
        public int? csi_id_consorcio { get; set; }
        public int? csi_id_prestador_consorcio { get; set; }
        public int? csi_id_municipio_consorcio { get; set; }
        public string csi_gerar_producao { get; set; }
        public int? csi_id_usuario { get; set; }
        public int? csi_expirar_requisicoes { get; set; }
        public string num_cnes { get; set; }
        public string codigo_interno_arquivo_cnes { get; set; }
    }
}
