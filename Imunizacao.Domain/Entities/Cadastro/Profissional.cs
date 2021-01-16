using RgCidadao.Domain.Entities.Imunizacao;
using System;
using System.Collections.Generic;

namespace RgCidadao.Domain.Entities.Cadastro
{
    public class Profissional
    {
       public int csi_codmed { get; set; }
       public string csi_nommed { get; set; }
       public string csi_endmed { get; set; }
       public string csi_baimed { get; set; }
       public string csi_cepmed { get; set; }
       public string csi_fonres { get; set; }
       public string csi_fontra { get; set; }
       public string csi_foncon { get; set; }
       public string csi_celular { get; set; }
       public string csi_endcon { get; set; }
       public string csi_baicon { get; set; }
       public string csi_cepcon { get; set; }
       public string csi_nomusu { get; set; }
       public DateTime csi_datainc { get; set; }
       public DateTime csi_dataalt { get; set; }
       public string excluido { get; set; }
       public string csi_cpf { get; set; }
       public string csi_ide { get; set; }
       public string csi_crm { get; set; }
       public string csi_pontoref { get; set; }
       public int csi_codesp { get; set; }
       public string csi_cidmed { get; set; }
       public string csi_cidcon { get; set; }
       public string csi_tipo { get; set; }
       public string csi_cbo { get; set; }
       public string csi_cns { get; set; }
       public string csi_apelido { get; set; }
       public string csi_senha { get; set; }
       public int csi_iduser { get; set; }
       public string csi_procpad { get; set; }
       public int csi_idmicroarea { get; set; }
       public string csi_inativo { get; set; }
       public DateTime csi_data_inativo { get; set; }
       public string csi_pis_pasep { get; set; }
       public string csi_status_hiperdia { get; set; }
       public DateTime csi_dtnasc { get; set; }
       public string csi_sexo { get; set; }
       public string csi_cod_cons_clase { get; set; }
       public string csi_sg_uf_emis_cons_classe { get; set; }
       public string csi_e_mail { get; set; }
       public string csi_numeroendereco { get; set; }
       public string csi_complementoend { get; set; }
       public int idhorario { get; set; }
       public DateTime data_alteracao_serv { get; set; }
       public string csi_nome_orgao_classe { get; set; }
       public string id_numero_interno { get; set; }
       public string n_matricula { get; set; }
       public string csi_naturalidade { get; set; }
       public string nome_mae { get; set; }
       public string nome_pai { get; set; }
       public int raca_cor { get; set; }
       public string flg_deficiencia { get; set; }
       public string flg_def_auditiva { get; set; }
       public string flg_def_fisica { get; set; }
       public string flg_def_motora { get; set; }
       public string flg_def_visual { get; set; }
       public string flg_def_outras { get; set; }
       public string csi_ide_uf { get; set; }
       public string csi_ide_orgao_emissor { get; set; }
       public DateTime csi_ide_data_emissao { get; set; }
       public string ctps { get; set; }
       public string ctps_serie { get; set; }
       public string ctps_uf { get; set; }
       public DateTime ctps_data_emissao { get; set; }
       public int situacao_familiar { get; set; }
       public string flg_possui_filhos { get; set; }
       public int qtd_filhos { get; set; }
       public int escolaridade { get; set; }
       public string desc_escolaridade { get; set; }
       public string freq_escola_faculdade { get; set; }
       public DateTime previsao_termino_escola { get; set; }
       public float insalubridade { get; set; }
       public string csi_crm_uf { get; set; }
       public string imagem_assinatura { get; set; }
    }
}

