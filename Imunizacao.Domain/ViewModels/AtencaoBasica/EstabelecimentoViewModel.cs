using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.ViewModels.AtencaoBasica
{
    public class EstabelecimentoViewModel
    {
        public int? id { get; set; }
        public int? tipo_imovel { get; set; }
        public string logradouro { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string sigla_estado { get; set; }
        public int? qtde_familia { get; set; }
        public string numero_logradouro { get; set; }

    }
}
