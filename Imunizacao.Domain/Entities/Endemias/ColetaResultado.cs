using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Entities.Endemias
{
    public class ColetaResultado
    {
        public int? id { get; set; }
        public int? id_visita { get; set; }
        public int? id_coleta { get; set; }
        public int? id_especime { get; set; }
        public int? qtde { get; set; }
        public int? exemplar { get; set; }
        public int? qtde_larvas { get; set; }
        public int? amostra { get; set; }
    }
}
