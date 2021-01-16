using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Entities.Prontuario
{
    public class Exame
    {
        public int? csi_codexa { get; set; }
        public string csi_nome { get; set; }
        public int? csi_dias { get; set; }
        public string csi_material { get; set; }
        public int? id_servico { get; set; }
        public int? id_classificacao { get; set; }
        public int? csi_tipo { get; set; }
        public int? cod_modelo_exame { get; set; }
        public int? cod_tipo_impressao_exame { get; set; }
        public int? id_material_exame { get; set; }
        public int? codigo_equipamento { get; set; }
        public string csi_codsus { get; set; }

        //Exames Agrupados
        public int? id_requisicao { get; set; }
        public int? id_agendamento { get; set; }
        public int? id_profissional_exame { get; set; }
        public int? quantidade { get; set; }
        public string flg_solicitado { get; set; }
        public string flg_avaliado { get; set; }
        public string flg_exame_realizado { get; set; }
        public string data_hora_solicitacao { get; set; }
        public string data_hora_avaliacao { get; set; }
        public string data_hora_resultado { get; set; }
        public string flg_cancelado { get; set; }
        public int? csi_codpac { get; set; }
        public string csi_nompac { get; set; }
        public string nome_agrupamento { get; set; }

    }
}
