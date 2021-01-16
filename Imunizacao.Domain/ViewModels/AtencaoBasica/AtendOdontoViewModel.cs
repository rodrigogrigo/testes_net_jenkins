using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.ViewModels.AtencaoBasica
{
    public class AtendOdontoViewModel
    {
        public int? id { get; set; }
        public int? id_profissional { get; set; }
        public string nome_profissional { get; set; }
        public int? id_cidadao { get; set; }
        public string nome_paciente { get; set; }
        public int? id_unidade { get; set; }
        public string unidade { get; set; }
        public DateTime? data { get; set; }
    }
}
