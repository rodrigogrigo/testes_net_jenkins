namespace RgCidadao.Api.ViewModels.Indicadores.Indicador3
{
    public class Indicador3AgenteSaudeViewModel
    {
        public string agente { get; set; }
        public int? codigo_agente { get; set; }
        public double? porcentagem { get; set; }
        public double? porcentagem_valida { get; set; }
        public int? qtde_gestantes { get; set; }
        public int? qtde_metas { get; set; }
        public int? qtde_metas_validas { get; set; }
    }
}
