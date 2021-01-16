using System;

namespace RgCidadao.Domain.ViewModels.Indicadores
{
    public class PubliAlvoIndicador3ViewModel
    {
        public string gestante { get; set; }
        public int? idade_gestacional { get; set; }
        public string desfecho { get; set; }
        public DateTime? dum { get; set; }
        public int? atendido { get; set; }
        public int? atendido_valido { get; set; }
        public int? id_individuo { get; set; }
        public DateTime? gi_data_nascimento { get; set; }
    }
}
