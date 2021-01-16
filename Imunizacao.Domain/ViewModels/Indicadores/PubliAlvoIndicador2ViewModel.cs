using System;

namespace RgCidadao.Domain.ViewModels.Indicadores
{
    public class PubliAlvoIndicador2ViewModel
    {
        public string gestante { get; set; }
        public int? idade_gestacional { get; set; }
        public string desfecho { get; set; }
        public DateTime? dum { get; set; }
        public DateTime? data_primeiro_atendimento { get; set; }
        public DateTime? data_ultimo_atendimento { get; set; }
        public int? qtde_atendimento { get; set; }
        public int? sifilis { get; set; }
        public int? sifilis_valido { get; set; }
        public int? hiv { get; set; }
        public int? hiv_valido { get; set; }
        public int? id_individuo { get; set; }
        public DateTime? gi_data_nascimento { get; set; }
    }
}
