using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Report.Interfaces.Imunizacao
{
    public interface IImunizacaoMovimentoReport
    {
        string produto { get; set; }
        string fabricante { get; set; }
        int entrada { get; set; }
        int saida { get; set; }
        int frascos_transferidos { get; set; }
        int quebra_frascos { get; set; }
        int falta_energia { get; set; }
        int falta_equipamento { get; set; }
        int validade_vencida { get; set; }
        int procedimento_inadequado { get; set; }
        int falha_transporte { get; set; }
        int outros_motivos { get; set; }
        int saldo_inicial { get; set; }
        int saldo_final { get; set; }
        int id_produto { get; set; }
        int id_produtor { get; set; }
        string lote { get; set; }
    }
}