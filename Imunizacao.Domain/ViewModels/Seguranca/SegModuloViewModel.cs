using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.ViewModels.Seguranca
{
    public class SegModuloViewModel
    {
        public SegModuloViewModel()
        {
            telas = new List<SegTelaViewModel>();
        }

        public int? id { get; set; }
        public string descricao { get; set; }
        public List<SegTelaViewModel> telas { get; set; }
    }

    public class SegTelaViewModel
    {
        public SegTelaViewModel()
        {
            acoes = new List<SegAcoesViewModel>();
        }
        public int? id { get; set; }
        public string descricao { get; set; }
        public string nome { get; set; }
        public int id_modulo { get; set; }
        public List<SegAcoesViewModel> acoes { get; set; }
    }

    public class SegAcoesViewModel
    {
        public int? id { get; set; }
        public string descricao { get; set; }
        public string nome { get; set; }
        public int? permissao { get; set; }
    }
}
