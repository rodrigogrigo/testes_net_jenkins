using System;

namespace Imunizacao.Domain.Entities
{
    public class Seg_Usuario
    {
        public int ?id { get; set; }
        public string login { get; set; }
        public string senha { get; set; }
        public string nome { get; set; }
        public string administrador { get; set; }
        public int? csi_id_ultima_versao { get; set; }
        public string csi_exibir_versao { get; set; }
        public int? timeout { get; set; }
        public string status { get; set; }
        public int? id_grupo { get; set; }
        public string email_1 { get; set; }
        public string email_2 { get; set; }
        public string telefone_1 { get; set; }
        public string telefone_2 { get; set; }
        public int? layout_consulta { get; set; }
        public DateTime? data_alteracao_serv { get; set; }
        public string senha_nao_expira { get; set; }
        public string usa_tablet { get; set; }
        public string uuid_usuario { get; set; }
        public string possui_certificado_digital { get; set; }
        public string chave_privada { get; set; }
        public string acessar_app_indicador { get; set; }
        public string Token { get; set; }
    }
}
