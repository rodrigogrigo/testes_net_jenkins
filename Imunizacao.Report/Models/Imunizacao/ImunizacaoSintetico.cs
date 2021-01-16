using RgCidadao.Report.Interfaces.Imunizacao;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Report.Models.Imunizacao
{
    public class ImunizacaoSintetico : IImunizacaoSinteticoReport
    {
        private static string _nome_unidade;
        private static string _imunobiologico;
        private static string _dose;
        private static int _qtde;

        public string nome_unidade { get => _nome_unidade; set => _nome_unidade = value; }
        public string imunobiologico { get => _imunobiologico; set => _imunobiologico = value; }
        public string dose { get => _dose; set => _dose = value; }
        public int qtde { get => _qtde; set => _qtde = value; }
    }
}
