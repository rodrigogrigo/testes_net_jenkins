using System;

namespace RgCidadao.Domain.ViewModels.Indicadores
{
    public class AtendimentoIndicador3ViewModel
    {
        public string tipo_atendimento { get; set; }
        public DateTime data { get; set; }
        public string profissional { get; set; }
        public string cbo { get; set; }
        public string descricao_cbo { get; set; }
        public string condicao_avaliada { get; set; }
        public string procedimento { get; set; }
        public string equipe { get; set; }
        public string unidade_saude { get; set; }
        public int processado_criticas { get; set; }
        public int flg_erro { get; set; }
        public string descricao_erro { get; set; }
        public int indicador { get; set; }
        public int indicador_valido { get; set; }
        public int cbo_valido { get; set; }
        public int cid_ciap_valido { get; set; }
        public int lote { get; set; }
    }
}
