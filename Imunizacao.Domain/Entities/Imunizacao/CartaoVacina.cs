using System;

namespace RgCidadao.Domain.Entities.Imunizacao
{
    public class CartaoVacina
    {
        public int? id { get; set; }
        public int? id_unidade { get; set; }
        public int? id_profisional { get; set; }
        public int? id_produto { get; set; }
        public string lote { get; set; }
        public DateTime? vencimento { get; set; }
        public DateTime? data_aplicacao { get; set; }
        public DateTime? data_aprazamento { get; set; }
        public DateTime? data_prevista { get; set; }
        public string registro_anterior { get; set; }
        public string hanseniase { get; set; }
        public string gestante { get; set; }
        public string inadvertida { get; set; }
        public string usuario { get; set; }
        public DateTime? data_hora { get; set; }
        public int? id_paciente { get; set; }
        public string cbo { get; set; }
        public int? id_dose { get; set; }
        public int? id_estrategia { get; set; }
        public int? id_grupo_atendimento { get; set; }
        public int? id_produtor { get; set; }
        public int? id_modivo_indicacao { get; set; }
        public string exportado { get; set; }
        public int? id_gestacao { get; set; }
        public string uuid { get; set; }
        public int? id_turno { get; set; }
        public int? id_local_atendimento { get; set; }
        public string flg_viajante { get; set; }
        public string flg_puerpera { get; set; }
        public int? id_esus_exportacao_item { get; set; }
        public DateTime? data_nascimento { get; set; }
        public int? id_sexo { get; set; }
        public string nome_produtor { get; set; }
        public int? id_usuario_exclusao { get; set; }
        public int? flg_excluido { get; set; }
        public string observacao { get; set; }
        public int? id_via_adm { get; set; }
        public int? id_local_aplicacao { get; set; }
    }
}
