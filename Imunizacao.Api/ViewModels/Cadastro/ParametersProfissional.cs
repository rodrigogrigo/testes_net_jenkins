using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RgCidadao.Api.ViewModels.Cadastro
{
    public class ParametersProfissional
    {
        public int? page { get; set; }
        public int? pagesize { get; set; }
        public string codigo { get; set; }
        public string nome { get; set; }
        public string cbo { get; set; }
        public int? unidade { get; set; }
        public string cpf { get; set; }
    }
}
