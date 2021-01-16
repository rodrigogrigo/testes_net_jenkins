using System;
using System.Collections.Generic;
using System.Text;

namespace Imunizacao.Domain.ViewModels
{
    public class UnidadeEstoqueViewModel
    {
        public int id { get; set; }
        public string unidade { get; set; }
        public string cnes { get; set; }
        public int? qtde { get; set; }
    }
}
