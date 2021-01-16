using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.ViewModels.AtencaoBasica
{
    public class FichaComplementarViewModel
    {
        public int? id { get; set; }

        //PROFISSIONAL
        public int? id_profissional { get; set; }
        public int? csi_codmed { get; set; }
        public string csi_nommed { get; set; }
        public string profissional { get; set; }
        public string csi_cbo { get; set; }

        //PACIENTE
        public int? csi_codpac { get; set; }
        public string csi_nompac { get; set; }
        public string paciente { get; set; }
        public string cns_responsavel { get; set; }

        //UNIDADE
        public int? id_unidade { get; set; }
        public int? csi_coduni { get; set; }
        public string csi_nomuni { get; set; }
        public string unidade { get; set; }

        public string cns_profissional { get; set; }
        public string cbo_profissional { get; set; }
        public string cnes { get; set; }
        public string ine_unidade { get; set; }
        public DateTime? data { get; set; }
        public string turno { get; set; }
        public string cns_cidadao { get; set; }
        public string cns_responsavel_familiar { get; set; }
        public string flg_teste_olhinho { get; set; }
        public DateTime? data_teste_olhinho { get; set; }
        public string flg_exame_fundo_olho { get; set; }
        public DateTime? data_exame_fundo_olho { get; set; }
        public string flg_teste_orelhinha_peate { get; set; }
        public DateTime? data_teste_orelhinha_peate { get; set; }
        public string flg_us_transfontanela { get; set; }
        public DateTime? data_us_transfontanela { get; set; }
        public string flg_tomografia { get; set; }
        public DateTime? data_tomografia { get; set; }
        public string flg_ressonancia { get; set; }
        public DateTime? data_ressonancia { get; set; }
        public int? id_usuario { get; set; }
        public int? id_equipe { get; set; }

        public string cod_ine { get; set; } 
        public string descricao { get; set; }
        public string responsavel { get; set; }

    }
}
