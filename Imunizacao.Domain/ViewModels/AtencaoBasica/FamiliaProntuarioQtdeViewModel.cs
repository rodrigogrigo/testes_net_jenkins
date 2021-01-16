using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.ViewModels.AtencaoBasica
{
    public class FamiliaProntuarioQtdeViewModel
    {
        public List<int> prontuarios_em_uso { get; set; }
        public int max_familias { get; set; }
    }
}
