using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RgCidadao.Api.ViewModels.Imunizacao
{
    public class ParametersVacina
    {
        public string unidade { get; set; }
        public int? id_cidadao { get; set; }
        public int? id_unidade { get; set; }
    }
}
