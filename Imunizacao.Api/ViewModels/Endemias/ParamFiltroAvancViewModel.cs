using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RgCidadao.Api.ViewModels.Endemias
{
    public class ParamFiltroEstabelecimentoViewModel
    {
        public int? id { get; set; }
        public int? tipoImovel { get; set; }
        public string bairro { get; set; }
        public string logradouro { get; set; }
        public string quarteirao { get; set; }
        public int? sequencia_numero { get; set; }
        public int? sequencia_quarteirao { get; set; }
        public int? numero { get; set; }
        public int page { get; set; }
        public int pagesize { get; set; }

        public string razao_social_nome { get; set; }
    }
}
