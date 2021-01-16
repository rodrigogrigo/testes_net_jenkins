using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Entities.AtencaoBasica
{
    public class Familia
    {
        public Familia()
        {
            domicilio = new Estabelecimento();
        }

        public int? id { get; set; }
        public int? id_domicilio { get; set; }
        public int? id_responsavel { get; set; }
        public int? id_usuario { get; set; }
        public int? area_prod_rural { get; set; }
        public int? trat_agua { get; set; }
        public int? destino_lixo { get; set; }
        public string tel_res { get; set; }
        public string tel_ref { get; set; }
        public int situacao_moradia { get; set; }
        public string gato { get; set; }
        public string cachorro { get; set; }
        public string passaro { get; set; }
        public string de_criacao { get; set; }
        public string outros { get; set; }
        public int? qtd_animais { get; set; }
        public int? situacao_cadastro { get; set; }
        public string renda_familiar_sal_min { get; set; }
        public string reside_desde { get; set; }
        public DateTime? data_inclusao { get; set; }
        public string uuid_registro_mobile { get; set; }
        public DateTime? data_alteracao_serv { get; set; }
        public string uuid_alteracao { get; set; }
        public string flg_exportar_esus { get; set; }
        public string uuid { get; set; }
        public int? id_velho_domicilio { get; set; }
        public int? num_prontuario_familiar { get; set; }

        public Estabelecimento domicilio { get; set; }
        public string nome_responsavel { get; set; }
    }
}
