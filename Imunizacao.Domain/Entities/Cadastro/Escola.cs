using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Entities.Cadastro
{
    public class Escola
    {
        public int? id { get; set; }
        public string nome { get; set; }
        public string inep { get; set; }
        public int? id_logradouro { get; set; }
        public string telefone { get; set; }
       
        public string logradouro { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string uf { get; set; }
        public string cep { get; set; }
    }
}
