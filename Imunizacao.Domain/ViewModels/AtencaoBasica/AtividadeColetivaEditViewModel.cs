using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.ViewModels.AtencaoBasica
{
    public class AtividadeColetivaEditViewModel
    {
        public AtividadeColetivaEditViewModel()
        {
            sigtap = new SigtapViewModel();
            profissional = new profissionalAtividadeViewModel();
        }

        public int? id { get; set; }
        public int? tipo_atividade { get; set; }
        public int? numero_inep_escola { get; set; }
        public DateTime? data_atividade { get; set; }
        public TimeSpan? hora_fim { get; set; }
        public TimeSpan? hora_inicio { get; set; }
        public string local_atividade { get; set; }
        public int? num_alteracoes { get; set; }
        public int? num_participantes { get; set; }
        public string pratica_ali_saudavel { get; set; }
        public string pratica_aplica_fluor { get; set; }
        public string pratica_acuid_visual { get; set; }
        public string pratica_autocuidado { get; set; }
        public string pratica_cidad_direitos { get; set; }
        public string pratica_saude_trabalha { get; set; }
        public string pratica_deped_quimica { get; set; }
        public string pratica_envelhecimento { get; set; }
        public string pratica_escova_dental { get; set; }
        public string pratica_planta_medici { get; set; }
        public string pratica_atividade_fisic { get; set; }
        public string pratica_corpo_mental { get; set; }
        public string pratica_corp_ment_pic { get; set; }
        public string pratica_prev_viol_cultu { get; set; }
        public string pratica_saude_ambiental { get; set; }
        public string pratica_saude_bucal { get; set; }
        public string pratica_saude_mental { get; set; }
        public string pratica_sexual_reprodut { get; set; }
        public string pratica_saude_escola { get; set; }
        public string pratica_agravo_negligen { get; set; }
        public string pratica_antropometria { get; set; }
        public string pratica_outros { get; set; }
        public int? previsao_participantes { get; set; }
        public string publico_comunidade { get; set; }
        public string publico_crian_0_3_anos { get; set; }
        public string publico_crian_4_5_anos { get; set; }
        public string publico_crian_6_11_anos { get; set; }
        public string publico_adolescente { get; set; }
        public string publico_mulher { get; set; }
        public string publico_gestante { get; set; }
        public string publico_homem { get; set; }
        public string publico_familiares { get; set; }
        public string publico_idosos { get; set; }
        public string publico_pess_doenca { get; set; }
        public string publico_usua_tabaco { get; set; }
        public string publico_usua_alcool { get; set; }
        public string publico_usua_drogas { get; set; }
        public string publico_sofrim_mental { get; set; }
        public string publico_prof_educacao { get; set; }
        public string publico_outros { get; set; }
        public int? id_lotacao_responsavel { get; set; }
        public int? situacao_envio { get; set; }
        public string tema_questo_administr { get; set; }
        public string tema_processo_trabalho { get; set; }
        public string tema_diagnostico_territ { get; set; }
        public string tema_planeja_monitoram { get; set; }
        public string tema_discussao_casa { get; set; }
        public string tema_educa_permanent { get; set; }
        public string tema_outros { get; set; }
        public string uuid { get; set; }
        public string exportado_esus { get; set; }
        public DateTime? data_alteracao_serv { get; set; }
        public string uuid_registro_mobile { get; set; }
        public string saude_auditiva { get; set; }
        public string desenvolvimento_linguagem { get; set; }
        public string verificacao_situacao_vacinal { get; set; }
        public string pnct_sessao_1 { get; set; }
        public string pnct_sessao_2 { get; set; }
        public string pnct_sessao_3 { get; set; }
        public string pnct_sessao_4 { get; set; }
        public string turno { get; set; }
        public string codigo_sigtap { get; set; }
        public string cnes { get; set; }
        public int? id_esus_exportacao_item { get; set; }
        //public int? id_profissional { get; set; }
        public int? id_equipe { get; set; }
        public int? id_unidade { get; set; }
        public string flg_atendimento_especializado { get; set; }
        public int? id_usuario { get; set; }
        public string flg_pratica_educativa { get; set; }
        public string flg_pratica_saude { get; set; }
        public int? id_controle_sincronizacao_lote { get; set; }
        public int? local_atendimento { get; set; }
        public int? id_escola { get; set; }
        public int? id_estabelecimento { get; set; }
        public string inep { get; set; }

        public string equipe { get; set; }
        public string csi_inativo_profissional { get; set; }
        public string nome_sigtap { get; set; }
        public int? csi_codmed { get; set; }
        public string csi_nommed { get; set; }
        public string descricao { get; set; }
        public string csi_cbo { get; set; }

        public SigtapViewModel sigtap { get; set; }
        public profissionalAtividadeViewModel profissional { get; set; }
    }

    public class SigtapViewModel
    {
        public string codigo { get; set; }
        public string nome { get; set; }
    }

    public class profissionalAtividadeViewModel
    {
        public string csi_cbo { get; set; }
        public int? csi_codmed { get; set; }
        public string csi_inativo_profissional { get; set; }
        public string csi_nommed { get; set; }
        public string descricao { get; set; }
        public string equipe { get; set; }
        public int? id_equipe { get; set; }
        public int? id_lotacao { get; set; }
    }
}
