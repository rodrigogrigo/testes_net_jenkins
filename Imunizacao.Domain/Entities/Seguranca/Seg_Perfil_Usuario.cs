using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Entities.Seguranca
{
    public class Seg_Perfil_Usuario
    {
        public int? id { get; set; }
        public int? id_usuario { get; set; }
        public int? id_unidade { get; set; }
        public int? id_perfil { get; set; }
        public string perfil { get; set; }
        public string unidade { get; set; }
    }
}
