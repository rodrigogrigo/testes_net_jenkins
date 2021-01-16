using System;

namespace RgCidadao.Domain.Entities.Cadastro
{
    public class Equipe
    {
        public int? id { get; set; }
        public int? tipo { get; set; }
        public string sigla { get; set; }
        public string descricao { get; set; }
        public string cod_ine { get; set; }
        public int? cod_area { get; set; }
        public string dsc_area { get; set; }
        public string nome_referencia { get; set; }
        public DateTime? data_desativacao { get; set; }
        public int? id_estabelecimento { get; set; }
        public string excluido { get; set; }
        public int? id_usuario { get; set; }
        public DateTime? data_alteracao_serv { get; set; }
        public string descricao_exibir { get; set; }
        public string acs { get; set; }
        public string descricao_equipe { get; set; }
    }
}
