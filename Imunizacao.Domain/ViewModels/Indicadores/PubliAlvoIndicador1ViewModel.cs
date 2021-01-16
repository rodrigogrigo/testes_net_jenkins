using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.ViewModels.Indicadores
{
    public class PubliAlvoIndicador1ViewModel
    {
        public string gestante { get; set; }
        public int? idade_gestacional { get; set; }
        public string desfecho { get; set; }
        public DateTime? dum { get; set; }
        public DateTime? data_primeiro_atendimento { get; set; }
        public DateTime? data_primeiro_atd_valido { get; set; }
        public DateTime? data_ultimo_atendimento { get; set; }
        public int? qtde_atendimento { get; set; }
        public int? qtde_atendimento_valido { get; set; }
        public int? qtde_dias_primeiro_atendimento { get; set; }
        public int? id_individuo { get; set; }
        public DateTime? gi_data_nascimento { get; set; }
    }
}
