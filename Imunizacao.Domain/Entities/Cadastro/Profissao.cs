using System;

namespace RgCidadao.Domain.Entities.Cadastro
{
    public class Profissao
    {
        public string csi_nompro { get; set; }
        public int? csi_codpro { get; set; }
        public DateTime? csi_datinc { get; set; }
        public DateTime? csi_datalt { get; set; }
        public string csi_cbopac { get; set; }
        public DateTime? csi_datainc { get; set; }
        public DateTime? csi_dataalt { get; set; }
        public string excluido { get; set; }
        public string csi_nomusu { get; set; }
        public int? csi_codocusiab { get; set; }
        public string codigo_cbo { get; set; }
        public DateTime? data_alteracao_serv { get; set; }
    }
}
