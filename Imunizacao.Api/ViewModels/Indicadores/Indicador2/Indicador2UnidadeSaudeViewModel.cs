﻿namespace RgCidadao.Api.ViewModels.Indicadores.Indicador2
{
    public class Indicador2UnidadeSaudeViewModel
    {
        public int? codigo_unidade { get; set; }
        public double? porcentagem { get; set; }
        public double? porcentagem_valida { get; set; }
        public int? qtde_gestantes { get; set; }
        public int? qtde_metas { get; set; }
        public int? qtde_metas_validas { get; set; }
        public string unidade { get; set; }
    }
}
