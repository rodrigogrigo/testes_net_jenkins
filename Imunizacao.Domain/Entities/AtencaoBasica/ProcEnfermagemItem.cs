using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Entities.AtencaoBasica
{
    public class ProcEnfermagemItem
    {
        public int? csi_controle { get; set; }
        public string csi_codproc { get; set; }
        public int? csi_qtde { get; set; }
        public int? csi_id_producao { get; set; }
        public int? csi_idade { get; set; }
        public string csi_codcid { get; set; }
        public string csi_escuta_inicial { get; set; }
        public int? id_esus_exportacao_item { get; set; }
        public string uuid { get; set; }
        public int? id_sequencial { get; set; }
    }
}
