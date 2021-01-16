using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RgCidadao.Api.ViewModels.Cadastro
{
    public class ResponseViewModel
    {
        public bool erro { get; set; }
        public string message { get; set; }
        public DateTime? datainicial { get; set; }
        public DateTime? datafinal { get; set; }
    }
}
