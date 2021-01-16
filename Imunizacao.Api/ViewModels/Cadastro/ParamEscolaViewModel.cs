using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RgCidadao.Api.ViewModels.Cadastro
{
    public class ParamEscolaViewModel
    {
        public int? page { get; set; }
        public int? pagesize { get; set; }
        public int? id { get; set; }
        public string nome { get; set; }
        public string inep { get; set; }
        public string uf { get; set; }
        public string cidade { get; set; }
        public string logradouro { get; set; }
        public string cep { get; set; }
        public string telefone { get; set; }
    }
}
