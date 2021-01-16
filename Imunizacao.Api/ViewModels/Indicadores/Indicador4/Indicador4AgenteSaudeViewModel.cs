namespace RgCidadao.Api.ViewModels.Indicadores.Indicador4
{
    public class Indicador4AgenteSaudeViewModel
    {
        public string agente_saude { get; set; }
        public int? codigo_agente { get; set; }
        public double? porcentagem { get; set; }
        public double? porcentagem_valida { get; set; }
        public int? qtde_individuos { get; set; }
        public int? qtde_metas { get; set; }
        public int? qtde_metas_validas { get; set; }
    }
}
