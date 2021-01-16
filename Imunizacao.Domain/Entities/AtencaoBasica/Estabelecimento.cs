using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Entities.AtencaoBasica
{
    public class Estabelecimento
    {
        public int? id { get; set; }
        public int? id_profissional { get; set; }
        public int? id_microarea { get; set; }
        public int? id_logradouro { get; set; }
        public int? tipo_imovel { get; set; }
        public string numero_logradouro { get; set; }
        public string complemento_logradouro { get; set; }
        public string telefone_fixo { get; set; }
        public string telefone_movel { get; set; }
        public int? zona { get; set; }
        public int? tipo_domicilio { get; set; }
        public int? qtd_comodos { get; set; }
        public int? tipo_acesso_domic { get; set; }
        public int? mat_predominante { get; set; }
        public int? abastecimento_agua { get; set; }
        public int? escoamento_sanita { get; set; }
        public string disponib_energia { get; set; }
        public string nome_inst_permanencia { get; set; }
        public int? outros_profi_instituicao { get; set; }
        public string nome_resp_instit { get; set; }
        public string cns_resp_instit { get; set; }
        public string cargo_resp_instit { get; set; }
        public string tel_resp_instit { get; set; }
        public int? id_usuario { get; set; }
        public DateTime? data_cadastro { get; set; }

    }
}
