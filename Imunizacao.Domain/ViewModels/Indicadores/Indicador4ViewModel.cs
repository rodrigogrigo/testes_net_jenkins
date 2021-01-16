using System;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.ViewModels.Indicadores
{
    public class Indicador4ViewModel
    {
        public int? codigo_unidade { get; set; }
        public string unidade_saude { get; set; }
        public int? codigo_equipe { get; set; }
        public string equipe { get; set; }
        public int? codigo_agente { get; set; }
        public string agente_saude { get; set; }
        public int qtde_individuos { get; set; }
        public int qtde_metas { get; set; }
        public int qtde_metas_validas { get; set; }
    }
}
