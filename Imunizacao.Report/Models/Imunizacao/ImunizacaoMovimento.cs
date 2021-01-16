using RgCidadao.Report.Interfaces.Imunizacao;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Report.Models.Imunizacao
{
    public class ImunizacaoMovimento : IImunizacaoMovimentoReport
    {
        private static string _produto;
        private static string _fabricante;
        private static int _entrada;
        private static int _saida;
        private static int _frascos_transferidos;
        private static int _quebra_frascos;
        private static int _falta_energia;
        private static int _falta_equipamento;
        private static int _validade_vencida;
        private static int _procedimento_inadequado;
        private static int _falha_transporte;
        private static int _outros_motivos;
        private static int _saldo_inicial;
        private static int _saldo_final;
        private static int _id_produto;
        private static int _id_produtor;
        private static string _lote;

        public string produto { get => _produto; set => _produto = value; }
        public string fabricante { get => _fabricante; set => _fabricante = value; }
        public int entrada { get => _entrada; set => _entrada = value; }
        public int saida { get => _saida; set => _saida = value; }
        public int frascos_transferidos { get => _frascos_transferidos; set => _frascos_transferidos = value; }
        public int quebra_frascos { get => _quebra_frascos; set => _quebra_frascos = value; }
        public int falta_energia { get => _falta_energia; set => _falta_energia = value; }
        public int falta_equipamento { get => _falta_equipamento; set => _falta_equipamento = value; }
        public int validade_vencida { get => _validade_vencida; set => _validade_vencida = value; }
        public int procedimento_inadequado { get => _procedimento_inadequado; set => _procedimento_inadequado = value; }
        public int falha_transporte { get => _falha_transporte; set => _falha_transporte = value; }
        public int outros_motivos { get => _outros_motivos; set => _outros_motivos = value; }
        public int saldo_inicial { get => _saldo_inicial; set => _saldo_inicial = value; }
        public int saldo_final { get => _saldo_final; set => _saldo_final = value; }
        public int id_produto { get => _id_produto; set => _id_produto = value; }
        public int id_produtor { get => _id_produtor; set => _id_produtor = value; }
        public string lote { get => _lote; set => _lote = value; }
    }
}
