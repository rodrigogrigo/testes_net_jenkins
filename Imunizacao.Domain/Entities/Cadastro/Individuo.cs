using System;

namespace RgCidadao.Domain.Entities.Cadastro
{
    public class Individuo
    {
        public int id_usuario { get; set; }
        public int? id_familia { get; }

        // Identificação
        public int? csi_codpac { get; set; }
        public string csi_nompac { get; set; }
        public string nome_social { get; set; }
        public string csi_sexpac { get; set; }
        public string csi_corpac { get; set; }
        public string etnia { get; set; }
        public DateTime csi_dtnasc { get; set; }
        public string emial { get; set; }
        public string csi_celular { get; set; }
        public string csi_maepac { get; set; }
        public string csi_paipac { get; set; }
        public int nacionalidade { get; set; }
        public string csi_codnat { get; set; }
        public DateTime? csi_naturalidade_data { get; set; }
        public string csi_naturalidade_portaria { get; set; }
        public int? csi_id_nacionalidade { get; set; }
        public DateTime? csi_data_entrada_pais { get; set; }
        public string csi_sanguegrupo { get; set; }
        public string csi_sanguefator { get; set; }
        public string csi_codhemoes { get; set; }
        public string csi_dsangue { get; set; }
        public string verif_situacao_rua { get; set; }

        // Documentos
        public string csi_cpfpac { get; set; }
        public string csi_ncartao { get; set; }
        public string csi_pispac { get; set; }
        public string csi_idepac { get; set; }
        public string csi_orgide { get; set; }
        public string csi_comide { get; set; }
        public DateTime? csi_expide { get; set; }
        public string csi_estide { get; set; }
        public DateTime? csi_dt_primeira_cnh { get; set; }
        public string csi_tipcer { get; set; }
        public int? numero_dnv { get; set; }
        public string csi_numcer { get; set; }
        public string csi_folivr { get; set; }
        public string csi_livcer { get; set; }
        public string csi_certidao_termo { get; set; }
        public DateTime? csi_emicer { get; set; }
        public string csi_nova_certidao { get; set; }
        public string csi_carcer { get; set; }
        public int? csi_cidcerc { get; set; }
        public string csi_titele { get; set; }
        public string csi_zontit { get; set; }
        public string csi_sectit { get; set; }
        public string csi_ctpsas { get; set; }
        public int? csi_ctps_serie { get; set; }
        public DateTime? csi_ctps_dtemis { get; set; }
        public string csi_ctps_uf { get; set; }
        public string csi_acerres { get; set; }
        public string csi_acarvac { get; set; }
        public string csi_acomres { get; set; }
        public string csi_ncerres { get; set; }
        public string num_processo_estado { get; set; }
        public string csi_cre { get; set; }

        // Endereço
        public int? csi_codend { get; set; }
        public string csi_endpac { get; set; }
        public string csi_numero_logradouro { get; set; }
        public string complemento { get; set; }
        public string csi_baipac { get; set; }
        public string csi_ceppac { get; set; }
        public string csi_codcid { get; set; }
        public int? id_estabelecimento_saude { get; set; }
        public int? cod_ageesus { get; set; }
        public string fora_area { get; set; }

        // Informações Sociodemográficas
        public string csi_estciv { get; set; }
        public string csi_situacao_familiar { get; set; }
        public int? csi_codgrau { get; set; }
        public int? csi_codpro { get; set; }
        public string csi_escpac { get; set; }
        public int? sit_mercado_trab { get; set; }
        public string comunidade_tradic { get; set; }
        public string desc_comunidade { get; set; }
        public string esus_verifica_ident_genero { get; set; }
        public int? esus_ident_genero { get; set; }
        public string verifica_ident_sex { get; set; }
        public int? orientacao_sexual { get; set; }
        public string esus_crianca_adulto { get; set; }
        public string esus_crianca_outra_crianca { get; set; }
        public string esus_crianca_outro { get; set; }
        public string esus_crianca_adolescente { get; set; }
        public string esus_crianca_sozinha { get; set; }
        public string esus_crianca_creche { get; set; }
        public int? verifica_deficiencia { get; set; }
        public string def_auditiva { get; set; }
        public string def_intelectual { get; set; }
        public string def_visual { get; set; }
        public string def_fisica { get; set; }
        public string def_outra { get; set; }
        public string csi_estudando { get; set; }
        public string freq_curandeiro { get; set; }
        public string grupo_comunitario { get; set; }
        public string possui_plano_saude { get; set; }

        // Condições de Saúde
        public int? situacao_peso { get; set; }
        public string internacao { get; set; }
        public string internacao_causa { get; set; }
        public string plantas_medicinais { get; set; }
        public string quais_plantas { get; set; }
        public string verifica_cardiaca { get; set; }
        public string insulf_cardiaca { get; set; }
        public string cardiaca_outro { get; set; }
        public string cardiaca_nsabe { get; set; }
        public int? verifica_rins { get; set; }
        public string rins_insulficiencia { get; set; }
        public string rins_outros { get; set; }
        public string rins_nsabe { get; set; }
        public string doenca_respiratoria { get; set; }
        public string resp_asma { get; set; }
        public string resp_enfisema { get; set; }
        public string resp_outro { get; set; }
        public string resp_nsabe { get; set; }
        public string fumante { get; set; }
        public string alcool { get; set; }
        public string drogas { get; set; }
        public string hipertenso { get; set; }
        public string diabetes { get; set; }
        public string avc_derrame { get; set; }
        public string infarto { get; set; }
        public string hanseniase { get; set; }
        public string tuberculose { get; set; }
        public string cancer { get; set; }
        public string tratamento_psiq { get; set; }
        public string acamado { get; set; }
        public string domiciliado { get; set; }
        public string praticas_complem { get; set; }
        public string outras_condic_01 { get; set; }
        public string outras_condic_02 { get; set; }
        public string outras_condic_03 { get; set; }

        // Situação de Rua
        public int? tempo_situacao_rua { get; set; }
        public int? vezes_alimenta { get; set; }
        public string outra_instituicao { get; set; }
        public string desc_instituicao { get; set; }
        public string grau_parentesco { get; set; }
        public string visita_familiar { get; set; }
        public string sit_rua_beneficio { get; set; }
        public string sit_rua_familiar { get; set; }
        public string banho { get; set; }
        public string higiene_bucal { get; set; }
        public string acesso_sanit { get; set; }
        public string higiene_outros { get; set; }
        public string doac_restaurante { get; set; }
        public string restaurante_popu { get; set; }
        public string doac_grup_relig { get; set; }
        public string doacao_popular { get; set; }
        public string doacao_outros { get; set; }
    }
}
