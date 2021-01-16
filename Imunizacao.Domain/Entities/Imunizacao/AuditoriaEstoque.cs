using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Entities.Imunizacao
{
    public class AuditoriaEstoque
    {
        public int? id { get; set; }
        public DateTime? data { get; set; }
        public string operacao { get; set; }
        public string historico { get; set; }
        public int qtde { get; set; }
        public int estoque_anterior { get; set; }
        public int estoque_atual { get; set; }
    }
}
