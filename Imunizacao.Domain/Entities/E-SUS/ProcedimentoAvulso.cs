using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Entities.E_SUS
{
    public class ProcedimentoAvulso
    {
        public ProcedimentoAvulso()
        {
            itens = new List<ProcedimentoAvulsoItem>();
        }

        public int? csi_controle { get; set; }
        public int? csi_codpac { get; set; }
        public DateTime? csi_data { get; set; }
        public int? csi_codmed { get; set; }
        public string csi_nomusu { get; set; }
        public DateTime? csi_datainc { get; set; }
        public string csi_obs { get; set; }
        public string csi_cbo { get; set; }
        public int? csi_coduni { get; set; }
        public int? idtriagem { get; set; }
        public int? idestabelecimento { get; set; }
        public int? idatend_odontologico { get; set; }
        public int? idatividade_coletiva { get; set; }
        public int? idvisita_domiciliar { get; set; }
        public int? csi_local_atendimento { get; set; }
        public int? turno { get; set; }
        public int? id_denuncia { get; set; }
        public int? id_inspecao { get; set; }
        public int? id_atendimento_individual { get; set; }
        public int? id_pep_anamnese { get; set; }
        public int? id_licenca { get; set; }
        public int? id_inspecao_veiculo { get; set; }
        public int? id_denuncia_andamento { get; set; }
        public int? id_pep_exame_fisico { get; set; }
        public int? id_administrar_medicamento { get; set; }
        public List<ProcedimentoAvulsoItem> itens { get; set; }
    }
}
