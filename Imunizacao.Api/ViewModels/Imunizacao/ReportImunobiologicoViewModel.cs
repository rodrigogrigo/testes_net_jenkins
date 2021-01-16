using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RgCidadao.Api.ViewModels.Imunizacao
{
    public class ReportImunobiologicoViewModel
    {
        [JsonProperty("unidade")]
        public string unidade { get; set; }

        [JsonProperty("nom_unidade")]
        public string nom_unidade { get; set; }

        [JsonProperty("count_unidades")]
        public int? count_unidades { get; set; }

        [JsonProperty("profissional")]
        public string profissional { get; set; }

        [JsonProperty("count_profissional")]
        public int? count_profissional { get; set; }

        [JsonProperty("nom_profissional")]
        public string nom_profissional { get; set; }

        [JsonProperty("produto")]
        public string produto { get; set; }

        [JsonProperty("count_produto")]
        public int? count_produto { get; set; }

        [JsonProperty("nom_produto")]
        public string nom_produto { get; set; }

        [JsonProperty("datainicio")]
        public string datainicio { get; set; }

        [JsonProperty("datafim")]
        public string datafim { get; set; }

        [JsonProperty("id_paciente")]
        public int? id_paciente { get; set; }

        [JsonProperty("nome_paciente")]
        public string nome_paciente { get; set; }

        [JsonProperty("gp_atendimento")]
        public int? gp_atendimento { get; set; }

        [JsonProperty("agrupamento")]
        public int? agrupamento { get; set; }

        [JsonProperty("unidadelogadaParam")]
        public int? unidadelogadaParam { get; set; }

        [JsonProperty("usuarioParam")]
        public string usuarioParam { get; set; }

        [JsonProperty("estrategia")]
        public string estrategia { get; set; }

        [JsonProperty("count_estrategias")]
        public int? count_estrategias { get; set; }

        [JsonProperty("nom_estrategia")]
        public string nom_estrategia { get; set; }
    }
}
