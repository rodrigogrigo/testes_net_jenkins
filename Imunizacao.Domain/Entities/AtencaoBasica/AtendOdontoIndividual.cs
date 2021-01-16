using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Entities.AtencaoBasica
{
    public class AtendOdontoIndividual
    {
        public AtendOdontoIndividual()
        {
            itens = new List<AtendOdontoIndividualItem>();
        }

        public int? id { get; set; }
        public int? id_profissional { get; set; }
        public int? id_unidade { get; set; }
        public string turno { get; set; }
        public DateTime? data { get; set; }
        public int? id_cidadao { get; set; }
        public int? id_local_atendimento { get; set; }
        public int? id_tipo_atendimento { get; set; }
        public int? id_tipo_consulta { get; set; }
        public string flg_vig_abscesso_dento { get; set; }
        public string flg_vig_alteracao_tecidos { get; set; }
        public string flg_vig_dor_dente { get; set; }
        public string flg_vig_fendas_fissuras { get; set; }
        public string flg_vig_fluorose_dentaria { get; set; }
        public string flg_vig_traumalismo { get; set; }
        public string flg_vig_nao_identificado { get; set; }
        public string id_fornecimento { get; set; }
        public string flg_conduta_ret_consulta { get; set; }
        public string flg_conduta_outros_prof { get; set; }
        public string flg_conduta_agenda_nasf { get; set; }
        public string flg_conduta_agenda_grupo { get; set; }
        public string flg_tratamento_concluido { get; set; }
        public string flg_enc_conduta_nec_especiais { get; set; }
        public string flg_enc_conduta_cirur_bmf { get; set; }
        public string flg_enc_conduta_endodontia { get; set; }
        public string flg_enc_conduta_estomatologia { get; set; }
        public string flg_enc_conduta_implantodontia { get; set; }
        public string flg_enc_conduta_odontopediatria { get; set; }
        public string flg_enc_conduta_ortodontia { get; set; }
        public string flg_enc_conduta_periodontia { get; set; }
        public string flg_enc_conduta_prot_dentaria { get; set; }
        public string flg_enc_conduta_radiologia { get; set; }
        public string flg_enc_conduta_outros { get; set; }
        public string flg_for_escova_dental { get; set; }
        public string flg_for_creme_dental { get; set; }
        public string flg_for_fio_dental { get; set; }
        public int? id_profissional2 { get; set; }
        public int? id_profissional3 { get; set; }
        public string flg_atend_gestante { get; set; }
        public string flg_atend_nescecidade_especial { get; set; }
        public string flg_conduta_alta_epsodio { get; set; }
        public string id_cbo { get; set; }
        public string uuid { get; set; }
        public int? id_usuario { get; set; }
        public DateTime? data_fim_atendimento { get; set; }
        public int? id_equipe { get; set; }

        public List<AtendOdontoIndividualItem> itens { get; set; }
    }
}
