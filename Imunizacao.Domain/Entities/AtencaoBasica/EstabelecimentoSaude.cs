using System;

namespace RgCidadao.Domain.Entities.AtencaoBasica
{
    public class EstabelecimentoSaude
    {
        public int id { get; set; }
        public string nome_fantasia { get; set; }
        public string cnpj { get; set; }
        public string cnes { get; set; }
        public string cod_esf_adm { get; set; }
        public string cod_tipo_unid { get; set; }
        public string telefone1 { get; set; }
        public string telefone2 { get; set; }
        public string fax { get; set; }
        public string e_mail { get; set; }
        public string numero { get; set; }
        public string complemento { get; set; }
        public string ponto_ref { get; set; }
        public string excluido { get; set; }
        public string complexidade { get; set; }
        public int id_usuario { get; set; }
        public int? id_logradouro { get; set; }
    }
}
