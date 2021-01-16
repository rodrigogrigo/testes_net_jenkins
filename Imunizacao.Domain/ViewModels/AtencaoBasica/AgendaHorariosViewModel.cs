using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.ViewModels.AtencaoBasica
{
    public class AgendaHorariosViewModel
    {
        public AgendaHorariosViewModel()
        {
            itens = new List<AgendaConsultaViewModel>();
        }

        public int? id_dias_med { get; set; }
        public string horario { get; set; }
        public List<AgendaConsultaViewModel> itens { get; set; }
    }
}
