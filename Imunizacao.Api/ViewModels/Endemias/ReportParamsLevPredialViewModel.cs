using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RgCidadao.Api.ViewModels.Endemias
{
    public class ReportParamsLevPredialViewModel
    {
        public DateTime? dataInicial { get; set; }
        public DateTime? dataFinal { get; set; }
        public string bairro { get; set; }
        public string nomeBairro { get; set; }
        public int? tipo { get; set; }
        public int? unidadelogadaParam { get; set; }
        public string usuarioParam { get; set; }
        public string profissionais { get; set; }
        public string nomeProfissional { get; set; }
    }
}
