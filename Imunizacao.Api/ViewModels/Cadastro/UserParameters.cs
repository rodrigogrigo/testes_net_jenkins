using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RgCidadao.Api.ViewModels.Cadastro
{
    public class UserParameters
    {
        public int id { get; set; }
        public string login { get; set; }
        public string senha { get; set; }
        public string telefone { get; set; }
        public string telefone_1 { get; set; }
        public string telefone_2 { get; set; }
        public string email_1 { get; set; }
    }
}
