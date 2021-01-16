using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RgCidadao.Api.ViewModels.Endemias
{
    public class FiltroAvancVisitaViewModel
    {
        public string bairro { get; set; }
        public string logradouro { get; set; }
        public string quarteirao_logradouro { get; set; }
        public DateTime? datainicial { get; set; }
        public DateTime? datafinal { get; set; }
        public int? agente { get; set; }
        public int? atividade { get; set; }
        public int? tipoImovel { get; set; }
        public int? tipo_visita { get; set; }
        public int? desfecho { get; set; }
        public int? encontroufoco { get; set; }
        public int? page { get; set; }
        public int? pagesize { get; set; }
        public string nomefantasia { get; set; }
        public string razaosocial { get; set; }
        public string num_ciclo { get; set; }
    }
}
