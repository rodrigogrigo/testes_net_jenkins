namespace RgCidadao.Domain.Entities.Imunizacao
{
    public class Produto
    {
        public int? id { get; set; }
        public string nome { get; set; }
        public string abreviatura { get; set; }
        public string sigla { get; set; }
        public int id_unidade { get; set; }
        public int? id_classe { get; set; }
        public int? id_imunobiologico { get; set; }
        public string unidade_medida { get; set; }
        public string classe { get; set; }
        public int id_via_adm { get; set; }
    }
}
