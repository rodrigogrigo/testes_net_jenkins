using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.ViewModels.AtencaoBasica
{
    public class TotalPorEquipeViewModel
    {
        public int? visitados { get; set; }
        public int? nao_visitados { get; set; }
        public int? ausentes_recusados { get; set; }
        public int? total { get; set; }
    }
}
