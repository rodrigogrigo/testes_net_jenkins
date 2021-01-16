using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Entities.Imunizacao
{
    public class BoletimMovimentacao
    {
        public int saldo_inicial { get; set; }
        public string lote { get; set; }
        public string nome_fabricante { get; set; }
        public string produto { get; set; }
        public string sigla { get; set; }
        public int entrada { get; set; }
        public int entrada_transferencia { get; set; }
        public int vacinado { get; set; }
        public int quebra { get; set; }
        public int falta_energia { get; set; }
        public int falha_equipamento { get; set; }
        public int vencimento { get; set; }
        public int transporte { get; set; }
        public int outros_motivos { get; set; }
        public int doacao { get; set; }
        public int transferencia_saida { get; set; }
        public int saida { get; set; }
        public int saldo_final { get; set; }
    }
}
