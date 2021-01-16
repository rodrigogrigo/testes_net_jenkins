using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.ViewModels.Imunizacao
{
    public class CartaoVacinaReportViewModel
    {
        public CartaoVacinaReportViewModel()
        {
            itens = new List<VacinaReportViewModel>();
        }
        public string paciente { get; set; }
        public string dataNasc { get; set; }
        public string idade { get; set; }
        public string sexo { get; set; }
        public string cns { get; set; }
        public string nomemae { get; set; }
        public string cpf { get; set; }
        public byte[] cabecalho { get; set; }
       

        public List<VacinaReportViewModel> itens { get; set; }
    }
}
