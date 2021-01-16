using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Entities.AtencaoBasica
{
    public class ExameFisico
    {
        //EXAME FISICO
        public int? codigo_exame_fisico { get; set; }
        public int? codigo_atendimento { get; set; }
        public DateTime? data_exame_fisico { get; set; }
        public decimal? valor_saturacao { get; set; }
        public decimal? valor_cefalico { get; set; }
        public decimal? valor_circ_abdominal { get; set; }
        public decimal? valor_pa1 { get; set; }
        public decimal? valor_pa2 { get; set; }
        public decimal? valor_freq_cardiaca { get; set; }
        public decimal? valor_freq_respiratoria { get; set; }
        public decimal? valor_temperatura { get; set; }
        public decimal? valor_glicemia { get; set; }
        public string observacao { get; set; }
        public string origem_dados { get; set; }
        public int? codigo_triagem { get; set; }
        public decimal? valor_peso { get; set; }
        public decimal? valor_altura { get; set; }
        public decimal? valor_imc { get; set; }
        public decimal? valor_circ_toracica { get; set; }
        public int? codigo_consulta { get; set; }
        public int? momento_coleta { get; set; }
        public int? origem { get; set; }
        public int? glasgow { get; set; }
        public int? regua_dor { get; set; }
        public int? id_classificacao_triagem { get; set; }
        public int? id_grupo_triagem { get; set; }
        public int? id_subgrupo_triagem { get; set; }
        public string descricao_sintoma_triagem { get; set; }
        public int? turno { get; set; }

        //USUÁRIO
        public string csi_nomusu {get; set;}

        //PROFISSIONAL
        public int? codigo_profissional { get; set; }
        public int? id_profissional_clas_triagem { get; set; }
        public string csi_nommed { get; set; }
        public int? csi_codmed { get; set; }
        public string csi_cbo { get; set; }

        //PACIENTE
        public int? id_paciente { get; set; }
        public int? csi_codpac { get; set; }
        public string csi_nompac { get; set; }
        public int? csi_idade { get; set; }

        //UNIDADE
        public int? csi_coduni { get; set; }
        public string csi_local_atendimento { get; set; }

        //PROCENFERMAGEM
        public int csi_controle { get; set; }

        public List<IProcenfermagem> iprocenfermagem { get; set; }

        public ExameFisico()
        {
            this.iprocenfermagem = new List<IProcenfermagem>();
        }
    }

    public class IProcenfermagem
    {
        public string csi_codproc { get; set; }
        public int? csi_qtde { get; set; }

        public IProcenfermagem(string csi_codproc, int? csi_qtde)
        {
            this.csi_codproc = csi_codproc;
            this.csi_qtde = csi_qtde;
        }
    }

}
