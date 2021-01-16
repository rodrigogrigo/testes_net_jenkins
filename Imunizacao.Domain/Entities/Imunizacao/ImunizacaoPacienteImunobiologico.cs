using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Entities.Imunizacao
{
    public class ImunizacaoPacienteImunobiologico
    {
        public int? id { get; set; }
        public DateTime? data { get; set; }
        public int? id_paciente { get; set; }
        public string paciente { get; set; }
        public DateTime? data_nascimento { get; set; }
        public string profissional { get; set; }
        public int? id_profissional { get; set; }
        public int? id_produto { get; set; }
        public string produto { get; set; }
        public string unidade { get; set; }
        public string sigla { get; set; }
        public int? id_unidade { get; set; }
        public int? id_dose { get; set; }
        public string dose { get; set; }
        public string lote { get; set; }
        public int qtd { get; set; }
    }
}
