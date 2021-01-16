using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RgCidadao.Api.ViewModels.AtencaoBasica
{
    public class CancelaAgendaViewModel
    {
        public int id_dias_med { get; set; }
        public int ordem { get; set; }
        public string login { get; set; }
        public string obs { get; set; }
        public int? fRG_Saude_Agenda_ID { get; set; }
    }
}
