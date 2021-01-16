using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.ViewModels.AtencaoBasica
{
    public class AtividadeColetivaViewModel
    {
        public int? id { get; set; }
        public DateTime? data_atividade { get; set; }
        public TimeSpan? hora_inicio { get; set; }
        public TimeSpan? hora_fim { get; set; }
        public int? numero_inep_escola { get; set; }
        public string equipe { get; set; }
        public string profissional { get; set; }
        public string unidade { get; set; }
        public string cbo { get; set; }
    }
}
