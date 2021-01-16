using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.ViewModels.Seguranca
{
    public class SegPermissoesUsuarioViewModel
    {
        public SegPermissoesUsuarioViewModel()
        {
            telas = new List<SegTelasUsuarioViewModel>();
        }

        public string modulo { get; set; }
        public List<SegTelasUsuarioViewModel> telas { get; set; }
    }

    public class SegTelasUsuarioViewModel
    {
        public SegTelasUsuarioViewModel()
        {
            acoes = new List<string>();
        }
        public string tela { get; set; }
        public List<string> acoes { get; set; }
    }
}
