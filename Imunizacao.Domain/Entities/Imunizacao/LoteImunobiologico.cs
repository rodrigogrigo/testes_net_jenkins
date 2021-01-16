using System;

namespace RgCidadao.Domain.Entities.Imunizacao
{
    public class LoteImunobiologico
    {
        public LoteImunobiologico()
        {
            produtor = new Produtor();
            vacinaapresentacao = new VacinaApresentacao();
        }

        public int? id { get; set; }
        public string lote { get; set; }
        public DateTime? validade { get; set; }
        public int? id_produto { get; set; }
        public int? id_produtor { get; set; }
        public Produtor produtor { get; set; }
        public VacinaApresentacao vacinaapresentacao { get; set; }
        public int? id_apresentacao { get; set; }
        public string apresentacao_descricao { get; set; }
        public int? quantidade { get; set; }
        public double quantidade_lote { get; set; }
        public long? flg_bloqueado { get; set; }
        public string nome_produtor { get; set; }
        public int? quantidade_apresentacao { get; set; }
        public int qtde_doses { get; set; }
        public int qtde_frascos { get; set; }
    }
}
