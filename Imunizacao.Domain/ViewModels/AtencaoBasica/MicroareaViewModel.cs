using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.ViewModels.AtencaoBasica
{
    public class MicroareaViewModel
    {
        public int? id_microarea { get; set; }
        public int? codigo_microarea { get; set; }
        public string profissional { get; set; }
        public string cod_ine { get; set; }
        public string cnes { get; set; }
        public int? id_equipe { get; set; }
        public int? id_profissional { get; set; }
        public string usa_tablet { get; set; }
    }
}
