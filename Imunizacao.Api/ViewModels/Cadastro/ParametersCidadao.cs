using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RgCidadao.Api.ViewModels.Cadastro
{
    public class ParametersCidadao
    {
        public string filtro { get; set; }
        public int page { get; set; }
        public int pagesize { get; set; }
        public string nome { get; set; }
        public string nomeMae { get; set; }
        public string cpf { get; set; }
        public string dataNascimento { get; set; }
        public int? codigo { get; set; }
        public string cns { get; set; }
        public string sexo { get; set; }
    }
}
