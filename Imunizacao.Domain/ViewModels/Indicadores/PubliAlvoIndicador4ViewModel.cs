using System;

namespace RgCidadao.Domain.ViewModels.Indicadores
{
    public class PubliAlvoIndicador4ViewModel
    {
        public int id_individuo { get; set; }
        public string individuo { get; set; }
        public DateTime? data_nascimento { get; set; }
        public int? idade { get; set; }
        public DateTime? data_ultimo_atendimento { get; set; }
        public DateTime? data_ultimo_atd_valido { get; set; }
        public int atd_ultimos_3anos { get; set; }
        public int atd_valido_ultimos_3anos { get; set; }
    }
}
