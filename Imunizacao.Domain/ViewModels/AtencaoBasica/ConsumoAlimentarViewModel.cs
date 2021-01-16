using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.ViewModels.AtencaoBasica
{
    public class ConsumoAlimentarViewModel
    {
        public int? id { get; set; }
        public int? id_profissional { get; set; }
        public string profissional { get; set; }
        public string paciente { get; set; }
        public DateTime? data_atendimento { get; set; }
        public string equipe { get; set; }
    }
}
