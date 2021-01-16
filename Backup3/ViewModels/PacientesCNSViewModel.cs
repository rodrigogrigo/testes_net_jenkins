using Newtonsoft.Json;
using System.Collections.Generic;

namespace AgendamentoRedeCuidar.ViewModels
{
    public class PacientesCNSViewModel
    {
        public PacientesCNSViewModel()
        {
            contatos = new contatos();
            endereco = new endereco();
            cns = new List<cns>();
            rg = new rgCidadao();
            cnh = new cnh();
        }

        [JsonProperty("contatos")]
        public contatos contatos { get; set; }

        [JsonProperty("endereco")]
        public endereco endereco { get; set; }

        [JsonProperty("cidadeNascimento")]
        public string cidadeNascimento { get; set; }

        [JsonProperty("paisNascimento")]
        public string paisNascimento { get; set; }

        [JsonProperty("cns")]
        public List<cns> cns { get; set; }

        [JsonProperty("nome")]
        public string nome { get; set; }

        [JsonProperty("passaporte")]
        public string passaporte { get; set; }

        [JsonProperty("racaCor")]
        public string racaCor { get; set; }

        [JsonProperty("rg")]
        public rgCidadao rg { get; set; }

        [JsonProperty("cpf")]
        public string cpf { get; set; }

        [JsonProperty("nomePai")]
        public string nomePai { get; set; }

        [JsonProperty("ctps")]
        public string ctps { get; set; }

        [JsonProperty("tituloEleitor")]
        public string tituloEleitor { get; set; }

        [JsonProperty("sexo")]
        public string sexo { get; set; }

        [JsonProperty("dataNascimento")]
        public string dataNascimento { get; set; }

        [JsonProperty("cnh")]
        public cnh cnh { get; set; }

        [JsonProperty("nomeMae")]
        public string nomeMae { get; set; }
    }

    public class contatos
    {
        [JsonProperty("contato0")]
        public string contato0 { get; set; }
    }

    public class endereco
    {
        [JsonProperty("uf")]
        public string uf { get; set; }

        [JsonProperty("cidade")]
        public string cidade { get; set; }

        [JsonProperty("nomeRua")]
        public string nomeRua { get; set; }

        [JsonProperty("nCasa")]
        public string nCasa { get; set; }

        [JsonProperty("bairro")]
        public string bairro { get; set; }

        [JsonProperty("tipoRua")]
        public string tipoRua { get; set; }

        [JsonProperty("cep")]
        public string cep { get; set; }

        [JsonProperty("pais")]
        public string pais { get; set; }
    }

    public class cns
    {
        [JsonProperty("Tipo")]
        public string Tipo { get; set; }

        [JsonProperty("CNS")]
        public string CNS { get; set; }
    }

    public class rgCidadao
    {
        public string rg { get; set; }
        public string ufemissaorg { get; set; }
        public string orgaoemissorrg { get; set; }
        public string dataEmissaoRg { get; set; }
    }

    public class cnh
    {
        public string uf { get; set; }
    }
}
