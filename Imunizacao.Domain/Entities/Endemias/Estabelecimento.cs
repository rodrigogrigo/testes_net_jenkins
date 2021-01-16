using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Entities.Endemias
{
    public class Estabelecimento
    {
        public int? id { get; set; }
        public string razao_social_nome { get; set; }
        public string nome_fantasia_apelido { get; set; }
        public string cnpj_cpf { get; set; }
        public string insc_estadual { get; set; }
        public string insc_municipal { get; set; }
        public string insc_produtor_rural { get; set; }
        public int? natureza_juridica { get; set; }
        public int? id_logradouro { get; set; }
        public string numero_logradouro { get; set; }
        public string complemento_logradouro { get; set; }
        public string telefone_fixo { get; set; }
        public string telefone_movel { get; set; }
        public string fax { get; set; }
        public string email { get; set; }
        public string website { get; set; }
        public DateTime? data_cadastro { get; set; }
        public int? situacao_atual { get; set; }
        public DateTime? data_situacao_atual { get; set; }
        public int? matriz_filial { get; set; }
        public string cnpj_matriz { get; set; }
        public int? esfera_adm { get; set; }
        public int? natureza_organizacao { get; set; }
        public DateTime? data_baixa_fechamento { get; set; }
        public string descricao_cnae { get; set; }
        public string procedimento_cadastro { get; set; }
        public string temporario { get; set; }
        public int? numero_visa { get; set; }
        public int? cnes_estabelecimento { get; set; }
        public int? zona { get; set; }
        public int? metragem { get; set; }
        public string quarteirao_logradouro { get; set; }
        public int? sequencia_quarteirao { get; set; }
        public int? sequencia_numero { get; set; }
        public int? id_microarea { get; set; }
        public int? id_usuario { get; set; }
        public int? num_prontuario_familiar { get; set; }
        public string ponto_referencia { get; set; }
        public int? tipo_domicilio { get; set; }
        public int? qtd_comodos { get; set; }
        public int? tipo_acesso_domic { get; set; }
        public int? mat_predominante { get; set; }
        public string disponib_energia { get; set; }
        public int? abastecimento_agua { get; set; }
        public int? escoamento_sanita { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string qrcode { get; set; }
        public string uuid { get; set; }
        public string uuid_alteracao { get; set; }
        public DateTime? data_alteracao_serv { get; set; }
        public string flg_exportar_esus { get; set; }
        public int? id_esus_exportacao_item { get; set; }
        public int? tipo_imovel { get; set; }
        public string tipo_logradouro { get; set; }
        public string cargo_resp_instit { get; set; }
        public string nome_resp_instit { get; set; }
        public int? outros_profi_instituicao { get; set; }
        public string tel_resp_instit { get; set; }
        public int? id_profissional { get; set; }
        public string cns_resp_instit { get; set; }
        public string uuid_registro_mobile { get; set; }
        public string nome_inst_permanencia { get; set; }

        public string bairro { get; set; }
        public string logradouro { get; set; }

        public int? id_visita { get; set; }
        public int? desfecho { get; set; }
        public string identificacao_imovel { get; set; }
    }
}
