namespace Imunizacao.Domain.Entities
{
    public class Dose
    {
        public int? id { get; set; }
        public string descricao { get; set; }
        public string sigla { get; set; }
        public int? qtde { get; set; }
    }
}
