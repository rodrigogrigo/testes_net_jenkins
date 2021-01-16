using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Entities.Prontuario
{
    public class Cid
    {
        public string codigo { get; set; }
        public string descricao { get; set; }
        public string sexo { get; set; }
        public string agravo { get; set; }
        public int id_competencia { get; set; }
    }
}
