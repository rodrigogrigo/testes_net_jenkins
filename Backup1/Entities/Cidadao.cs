﻿using System;

namespace Imunizacao.Domain.Entities
{
    public class Cidadao
    {
        public int? csi_codpac { get; set; }
        public string csi_nompac { get; set; }
        public string csi_sexpac { get; set; }
        public DateTime? csi_dtnasc { get; set; }
        public string csi_celular { get; set; }
        public string csi_corpac { get; set; }
        public int? nacionalidade { get; set; }
        public string csi_codnat { get; set; }
        public string csi_cpfpac { get; set; }
        public string csi_idepac { get; set; }
        public string csi_ncartao { get; set; }
        public string csi_orgide { get; set; }
        public string csi_estide { get; set; }
        public string csi_pispac { get; set; }
        public string emial { get; set; }
        public string csi_maepac { get; set; }
        public string csi_paipac { get; set; }
        public int? csi_id_nacionalidade { get; set; }
        public DateTime? csi_naturalidade_data { get; set; }
        public string csi_naturalidade_portaria { get; set; }
        public string acs { get; set; }
        public string equipe { get; set; }
        public string nome_social { get; set; }
        public int? csi_codgrau { get; set; }
        public int? cod_ageesus { get; set; }
        public string esus_cns_responsavel_domicilio { get; set; }
        public string esus_responsavel_domicilio { get; set; }
        public string fora_area { get; set; }
        public string microarea { get; set; }
        public string etnia { get; set; }
        public int? csi_codpro { get; set; }
        public int? sit_mercado_trab { get; set; }
        public string csi_escpac { get; set; }
        public string esus_crianca_adulto { get; set; }
        public string esus_crianca_outra_crianca { get; set; }
        public string esus_crianca_adolescente { get; set; }
        public string esus_crianca_sozinha { get; set; }
        public string esus_crianca_creche { get; set; }
        public string esus_crianca_outro { get; set; }
        public string csi_estudando { get; set; }
        public string freq_curandeiro { get; set; }
        public string possui_plano_saude { get; set; }
        public string grupo_comunitario { get; set; }
        public string comunidade_tradic { get; set; }
        public string desc_comunidade { get; set; }
        public int? verifica_deficiencia { get; set; }
        public string def_auditiva { get; set; }
        public string def_visual { get; set; }
        public string def_intelectual { get; set; }
        public string def_fisica { get; set; }
        public string def_outra { get; set; }
        public string verifica_ident_sex { get; set; }
        public int? orientacao_sexual { get; set; }
        public string esus_verifica_ident_genero { get; set; }//
        public int? esus_ident_genero { get; set; }
        public int? esus_saida_cidadao_cadastro { get; set; }//
        public DateTime? csi_data_obito { get; set; }
        public string esus_numero_do { get; set; }
        public string verifica_cardiaca { get; set; }
        public string insulf_cardiaca { get; set; }
        public string cardiaca_nsabe { get; set; }
        public string cardiaca_outro { get; set; }
        public int? verifica_rins { get; set; }
        public string rins_insulficiencia { get; set; }
        public string rins_nsabe { get; set; }
        public string rins_outros { get; set; }
        public string doenca_respiratoria { get; set; }
        public string resp_asma { get; set; }
        public string resp_enfisema { get; set; }
        public string resp_nsabe { get; set; }
        public string resp_outro { get; set; }
        public string internacao { get; set; }
        public string internacao_causa { get; set; }
        public string plantas_medicinais { get; set; }
        public string quais_plantas { get; set; }
        public string tratamento_psiq { get; set; }
        public int? situacao_peso { get; set; }
        public string domiciliado { get; set; }
        public string acamado { get; set; }
        public string cancer { get; set; }
        public string fumante { get; set; }
        public string drogas { get; set; }
        public string alcool { get; set; }
        public string diabetes { get; set; }
        public string avc_derrame { get; set; }
        public string hipertenso { get; set; }
        public string infarto { get; set; }
        public string tuberculose { get; set; }
        public string hanseniase { get; set; }
        public string praticas_complem { get; set; }
        public string outras_condic_01 { get; set; }
        public string verif_situacao_rua { get; set; }
        public int? tempo_situacao_rua { get; set; }
        public string outra_instituicao { get; set; }
        public string desc_instituicao { get; set; }
        public string visita_familiar { get; set; }
        public string grau_parentesco { get; set; }
        public string acesso_higientep { get; set; }
        public string banho { get; set; }
        public string acesso_sanit { get; set; }
        public string higiene_bucal { get; set; }
        public string higiene_outros { get; set; }
        public string sit_rua_beneficio { get; set; }
        public string sit_rua_familiar { get; set; }
        public int? vezes_alimenta { get; set; }
        public string restaurante_popu { get; set; }
        public string doac_restaurante { get; set; }
        public string doac_grup_relig { get; set; }
        public string doacao_popular { get; set; }
        public string doacao_outros { get; set; }
    }
}