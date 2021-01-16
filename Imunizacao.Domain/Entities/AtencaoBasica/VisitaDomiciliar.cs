using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Entities.AtencaoBasica
{

    public class VisitaDomiciliar
    {
        public int? id { get; set; }
        public int? id_profissional { get; set; }
        public int? turno { get; set; }
        public string competencia { get; set; }
        public DateTime? data_visita { get; set; }
        public int? id_domicilio { get; set; }
        public string visita_compartilhada { get; set; }
        public string visita_periodica { get; set; }
        public string ba_consulta { get; set; }
        public string ba_exame { get; set; }
        public string ba_vacina { get; set; }
        public string ba_bolsafamilia { get; set; }
        public string mv_gestante { get; set; }
        public string mv_puerpera { get; set; }
        public string mv_recem_nascido { get; set; }
        public string mv_crianca { get; set; }
        public string mv_desnutricao { get; set; }
        public string mv_reabilitacao_deficiencia { get; set; }
        public string mv_hipertencao { get; set; }
        public string mv_diabetes { get; set; }
        public string mv_asma { get; set; }
        public string mv_dpoc { get; set; }
        public string mv_cancer { get; set; }
        public string mv_doenca_cronica { get; set; }
        public string mv_hanseniase { get; set; }
        public string mv_tuberculose { get; set; }
        public string mv_domiciliado_acamado { get; set; }
        public string mv_vulnerabilidade_social { get; set; }
        public string mv_bolsa_familia { get; set; }
        public string mv_saude_mental { get; set; }
        public string mv_alcool { get; set; }
        public string mv_outras_drogas { get; set; }
        public string mv_internacao { get; set; }
        public string mv_controle_ambientes { get; set; }
        public string mv_atv_coletiva { get; set; }
        public string mv_orientacao_prevencao { get; set; }
        public string mv_outros { get; set; }
        public string desfecho { get; set; }
        public string cadastramento_atualizacao { get; set; }
        public int? id_cidadao { get; set; }
        public string uuid { get; set; }
        public string exportado_esus { get; set; }
        public DateTime? data_alteracao_serv { get; set; }
        public string uuid_registro_mobile { get; set; }
        public string csi_nomusualter { get; set; }
        public string mv_sint_respiratorios { get; set; }
        public string mv_tabagista { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public int? codigo_microarea { get; set; }
        public string tipo_imovel { get; set; }
        public string mv_acao_educativa { get; set; }
        public string mv_imovel_foco { get; set; }
        public string mv_acao_mecanica { get; set; }
        public string mv_trat_focal { get; set; }
        public double? peso { get; set; }
        public int? altura { get; set; }
        public string fora_area { get; set; }
        public int? id_esus_exportacao_item { get; set; }
        public int? id_sexo { get; set; }
        public DateTime? data_nascimento { get; set; }
        public int? id_unidade { get; set; }
        public int? id_usuario { get; set; }
        public int? id_estabelecimento { get; set; }
        public int? id_equipe { get; set; }
        public int? id_controle_sincronizacao_lote { get; set; }
    }
}
