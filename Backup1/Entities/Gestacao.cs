namespace Imunizacao.Domain.Entities
{
    public class Gestacao
    {
        public Gestacao()
        {
            item = new Gestacao_Item();
        }

        public string nome_maternidade { get; set; }
        public int? qtd { get; set; }

        public int? id { get; set; }
        public int? id_cidadao { get; set; }
        public int? qtd_gestacao { get; set; }
        public int? qtd_parto { get; set; }
        public int? qtd_aborto { get; set; }
        public int? qtd_parto_normal { get; set; }
        public int? qtd_nascido_vivo { get; set; }
        public string dsc_observacao { get; set; }
        public string flg_gestacao_em_andamento { get; set; }
        public string info_qtd_gestacao { get; set; }
        public string info_qtd_parto { get; set; }
        public string info_qtd_aborto { get; set; }
        public string info_qtd_parto_normal { get; set; }
        public string info_qtd_nascido_vivo { get; set; }
        public Gestacao_Item item { get; set; }
    }
}
