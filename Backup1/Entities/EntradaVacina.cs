using System;
using System.Collections.Generic;

namespace Imunizacao.Domain.Entities
{
    public class EntradaVacina
    {
        public EntradaVacina()
        {
            EntradaProdutoItem = new List<EntradaVacinaItem>();
        }

        public int? id { get; set; }
        public DateTime data { get; set; }
        public string fornecedor { get; set; }
        public int? num_nota { get; set; }
        public double valor { get; set; }
        public string usuario { get; set; }
        public int? id_unidade { get; set; }
        public string obs { get; set; }
        public double? valor_total { get; set; }
        public int? id_fornecedor { get; set; }
        public string numero_nota { get; set; }
        public List<EntradaVacinaItem> EntradaProdutoItem { get; set; }   
    }
}
