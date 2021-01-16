using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.ViewModels.Imunizacao
{
    public class DetalhamentoMovReportViewModel
    {
        public string produto { get; set; }
        public string sigla { get; set; }
        public string fabricante { get; set; }
        public int? entrada { get; set; }
        public int? saida { get; set; }
        public int? frascos_transferidos { get; set; }
        public int? quebra_frascos { get; set; }
        public int? falta_energia { get; set; }
        public int? falta_equipamento { get; set; }
        public int? validade_vencida { get; set; }
        public int? procedimento_inadequado { get; set; }
        public int? falha_transporte { get; set; }
        public int? outros_motivos { get; set; }
        public int? saldo_inicial { get; set; }
        public int? saldo_final { get; set; }
        public int? id_produto { get; set; }
        public int? id_produtor { get; set; }
        public string lote { get; set; }
    }
}
