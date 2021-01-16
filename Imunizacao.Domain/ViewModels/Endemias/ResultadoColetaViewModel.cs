using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.ViewModels.Endemias
{
    public class ResultadoColetaViewModel
    {
        public int? id { get; set; }
        public int? id_visita { get; set; }
        public int? deposito { get; set; }
        public int? amostra { get; set; }
        public int? id_profissional { get; set; }
        public int? qtde_larvas { get; set; }
        public string csi_nommed { get; set; }
        public int? restante { get; set; }
    }
}
