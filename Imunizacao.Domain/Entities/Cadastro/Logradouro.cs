using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Entities.Cadastro
{
    public class Logradouro
    {
        public int csi_codend { get; set; }
        public string csi_nomend { get; set; }
        public string csi_cep { get; set; }
        public int csi_codbai { get; set; }
        public string csi_codcid { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string estado { get; set; }
        public string sigla_estado { get; set; }
    }
}
