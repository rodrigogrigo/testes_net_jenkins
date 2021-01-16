using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RgCidadao.Api.ViewModels.Endemias
{
    public class ParamResultAmostraViewModel
    {
        public string agente { get; set; }
        public DateTime? data_inicial { get; set; }
        public DateTime? data_final { get; set; }
        public int? amostra { get; set; }
        public int? numero_tubito { get; set; }
        public string bairro { get; set; }
        public string ciclo { get; set; }
        public string unidade { get; set; }
        public int page { get; set; }
        public int pagesize { get; set; }
    }
}
