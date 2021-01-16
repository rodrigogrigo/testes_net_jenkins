using System;

namespace Imunizacao.Domain.Entities
{
    public class LoteImunobiologico
    {
        public LoteImunobiologico()
        {
            produtor = new Produtor();
        }

        public int? id { get; set; }
        public string lote { get; set; }
        public DateTime? validade { get; set; }
        public int? id_produto { get; set; }
        public int? id_produtor { get; set; }
        public Produtor produtor { get; set; }
        public int? id_apresentacao { get; set; }
        public string apresentacao { get; set; }
        public int? quantidade { get; set; }
        public double quantidade_lote { get; set; }
        public int? flg_bloqueado { get; set; }
        public string nome_produtor { get; set; }
    }
}
