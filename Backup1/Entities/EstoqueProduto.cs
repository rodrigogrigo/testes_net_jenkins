using System;

namespace Imunizacao.Domain.Entities
{
    public class EstoqueProduto
    {
        public int? id { get; set; }
        public int? id_unidade { get; set; }
        public string lote { get; set; }
        public int? id_produto { get; set; }
        public int? id_produtor { get; set; }
        public double? qtde { get; set; }
        public string nome_produtor { get; set; }
        public DateTime? validade { get; set; }
    }
}
