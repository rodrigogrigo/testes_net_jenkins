namespace RgCidadao.Domain.Entities.Cadastro
{
    public class Unidade
    {
        public int? id { get; set; }
        public string unidade { get; set; }
        public string cnes { get; set; }
        public string endereco { get; set; }
        public string bairro { get; set; }
        public string unidade_pa { get; set; }
        public string ativo { get; set; }

        public int? id_estabelecimento_saude { get; set; }
    }
}
