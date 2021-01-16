using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Entities.AtencaoBasica
{
    public class AtendOdontoIndividualItem
    {
        public int? id { get; set; }
        public int? id_atend_odont { get; set; }
        public string id_procedimento { get; set; }
        public int? quantidade_procedimento { get; set; }
        public int? id_producao { get; set; }
        public string uuid { get; set; }
        public int? id_esus_exportacao_item { get; set; }
        public int? id_atend_odont_realizado { get; set; }
        public int? id_dente { get; set; }
        public int? id_local_procedimento_odonto { get; set; }
        public string id_lista_regiao_procedimento { get; set; }
        public string id_classe { get; set; }
        public int? id_atend_prontuario { get; set; }
        public int? id_atend_prontuario_realizado { get; set; }
        public string observacao { get; set; }
        public DateTime? data_realizar { get; set; }
        public DateTime? data_realizado { get; set; }
        public int? id_controle_sincronizacao_lote { get; set; }

        public string nome { get; set; }
    }
}
