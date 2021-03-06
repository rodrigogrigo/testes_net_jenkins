﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Entities.AtencaoBasica
{
    public class Consultas
    {
        public int? csi_controle { get; set; }
        public DateTime? csi_dataag { get; set; }
        public DateTime? csi_datacon { get; set; }
        public string csi_horario { get; set; }
        public string csi_nomusu { get; set; }
        public int? csi_codmed { get; set; }
        public int? csi_codponto { get; set; }
        public int? csi_codpac { get; set; }
        public int? csi_ordem { get; set; }
        public string csi_suplente { get; set; }
        public DateTime? csi_dataconf { get; set; }
        public string csi_nomusuconf { get; set; }
        public string csi_diagnostico { get; set; }
        public int? csi_codnatatend { get; set; }
        public int? csi_codpacacomp { get; set; }
        public string csi_nomeacomp { get; set; }
        public string csi_parentescoacomp { get; set; }
        public DateTime? csi_datanacacomp { get; set; }
        public string csi_docideacomp { get; set; }
        public string csi_endtrabacomp { get; set; }
        public string csi_codcidacomp { get; set; }
        public string csi_cepacomp { get; set; }
        public string csi_diagprovavel { get; set; }
        public string csi_anamnese { get; set; }
        public string csi_repatendimento { get; set; }
        public int? csi_qtdrepatend { get; set; }
        public DateTime? csi_dataalta { get; set; }
        public int? csi_codtipoalta { get; set; }
        public int? csi_codleito { get; set; }
        public int? csi_nprenatal { get; set; }
        public int? csi_codestnut { get; set; }
        public string csi_status { get; set; }
        public int? csi_codcon { get; set; }
        public string csi_alta { get; set; }
        public string csi_obsalta { get; set; }
        public string csi_modelo { get; set; }
        public string csi_modelopublico { get; set; }
        public string csi_nomemodelo { get; set; }
        public double? csi_peso { get; set; }
        public double? csi_imc { get; set; }
        public int? csi_altura { get; set; }
        public string csi_dietaobs { get; set; }
        public string csi_orientnutri { get; set; }
        public string csi_cbo { get; set; }
        public int? csi_unidade_agendamento { get; set; }
        public string csi_historico_evolutivo { get; set; }
        public int? csi_id_fila_espera { get; set; }
        public string csi_tipo_consulta { get; set; }
        public double? csi_medida_cintura { get; set; }
        public string csi_restricao_modelo { get; set; }
        public string csi_modelo_restrito_pac { get; set; }
        public string csi_hipertencao { get; set; }
        public string csi_diabetes { get; set; }
        public int? csi_pressao_art_sistolica { get; set; }
        public int? csi_pressao_art_diastolica { get; set; }
        public int? csi_glicemia { get; set; }
        public string csi_tipo_glicemia { get; set; }
        public string csi_sem_complicacoes { get; set; }
        public string csi_angina { get; set; }
        public string csi_iam { get; set; }
        public string csi_avc { get; set; }
        public string csi_pre_diabetico { get; set; }
        public string csi_amputacao_diabete { get; set; }
        public string csi_doenca_renal { get; set; }
        public string csi_retinopatia { get; set; }
        public string csi_hb_glicosilada { get; set; }
        public string csi_creatinina_serica { get; set; }
        public string csi_colesterol_total { get; set; }
        public string csi_ecg { get; set; }
        public string csi_triglicerides { get; set; }
        public string csi_urina_tipo1 { get; set; }
        public string csi_microalbuminuria { get; set; }
        public double? csi_qtde_hidroclorotiazida { get; set; }
        public double? csi_qtde_propranolol { get; set; }
        public double? csi_qtde_captopril { get; set; }
        public double? csi_qtde_glibenclamida { get; set; }
        public double? csi_qtde_metformina { get; set; }
        public int? csi_qtde_insulina { get; set; }
        public string csi_ant_fam_cardio_vascular { get; set; }
        public string csi_diabete_tipo1 { get; set; }
        public string csi_diabete_tipo2 { get; set; }
        public string csi_tabagismo { get; set; }
        public string csi_sedentarismo { get; set; }
        public string csi_sobrepeso { get; set; }
        public string csi_medicamentoso { get; set; }
        public string csi_outros_medicamentos { get; set; }
        public string csi_outras_coronariopatias { get; set; }
        public string csi_tipo_risco { get; set; }
        public string csi_status_hiperdia { get; set; }
        public string csi_outros_medicamentos_obs { get; set; }
        public int? csi_id_libexames { get; set; }
        public int? csi_codexa_padrao { get; set; }
        public int? id_diasmed { get; set; }
        public int? id_atendimento_individual { get; set; }
        public int? id_tipo_atendimento { get; set; }
        public int? id_atendimento_odontologico { get; set; }
        public string id_cid { get; set; }
        public int? id_orgao_publico { get; set; }
        public DateTime? csi_data_cancelou { get; set; }
        public string csi_nomusu_cancelou { get; set; }
        public string csi_obs { get; set; }
        public int? csi_unidade_paciente { get; set; }
        public string uuid { get; set; }
        public int? id_tipo_consulta { get; set; }
        public string id_senha { get; set; }
        public int? id_pep_exame_fisico { get; set; }
        public int? sms_enviado { get; set; }
        public string csi_obs_cancelamento { get; set; }
        public string id_controle_sincronizacao_lote { get; set; }
        public int? id_equipe { get; set; }
    }
}
