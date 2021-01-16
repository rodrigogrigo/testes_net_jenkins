using RgCidadao.Report.Interfaces.Imunizacao;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Report.Models.Imunizacao
{
    public class ImunizacaoImunobiologico : IImunizacaoImunobiologicoReport
    {
        public ImunizacaoImunobiologico()
        {
        }

        private static string _data_imunizacao;
        private static string _imunobiologico;
        private static string _dose;
        private static string _profissional;
        private static int _id_paciente;
        private static string _nome_paciente;
        private static int _idade;
        private static int _total_paciente;
        private static string _lote;
        private static string _nome_unidade;

        public string data_imunizacao { get => _data_imunizacao; set => _data_imunizacao = value; }
        public string imunobiologico { get => _imunobiologico; set => _imunobiologico = value; }
        public string dose { get => _dose; set => _dose = value; }
        public string profissional { get => _profissional; set => _profissional = value; }
        public int id_paciente { get => _id_paciente; set => _id_paciente = value; }
        public string nome_paciente { get => _nome_paciente; set => _nome_paciente = value; }
        public int idade { get => _idade; set => _idade = value; }
        public int total_paciente { get => _total_paciente; set => _total_paciente = value; }
        public string lote { get => _lote; set => _lote = value; }
        public string nome_unidade { get => _nome_unidade; set => _nome_unidade = value; }
    }
}
