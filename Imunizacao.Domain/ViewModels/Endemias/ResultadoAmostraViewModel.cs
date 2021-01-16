using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.ViewModels.Endemias
{
    public class ResultadoAmostraViewModel
    {
        public string agente { get; set; }
        public DateTime? data_inicial { get; set; }
        public DateTime? data_final { get; set; }
        public int? amostra { get; set; }
        public int? numero_tubito { get; set; }
        public string bairro { get; set; }
        public int? id_profissional { get; set; }
        public int? id_visita { get; set; }
        public int? pendente { get; set; }
        public int? lancada { get; set; }
        public int? id_estabelecimento { get; set; }
    }
}
