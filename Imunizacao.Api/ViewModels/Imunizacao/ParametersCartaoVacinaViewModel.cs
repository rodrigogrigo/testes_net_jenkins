using RgCidadao.Domain.Entities;
using RgCidadao.Domain.Entities.Imunizacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RgCidadao.Api.ViewModels.Imunizacao
{
    public class ParametersCartaoVacinaViewModel
    {
        public ParametersCartaoVacinaViewModel()
        {
            CartaoVacina = new CartaoVacina();
        }

        public CartaoVacina CartaoVacina { get; set; }
        public int? IdAprazamento { get; set; }
    }
}
