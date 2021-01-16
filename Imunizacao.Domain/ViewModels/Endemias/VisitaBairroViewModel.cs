using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.ViewModels.Endemias
{
    public class VisitaBairroViewModel
    {
        public VisitaBairroViewModel()
        {
            imoveis = new List<VisitaEstabelecimentoViewModel>();
        }

        public string quarteirao_logradouro { get; set; }
        public string quarteirao{ get; set; }
        public int? sequencia_quarteirao { get; set; }
        public string numero_logradouro { get; set; }
        public string sequencia_numero { get; set; }
        public List<VisitaEstabelecimentoViewModel> imoveis { get; set; }
    }

    public class VisitaEstabelecimentoViewModel
    {
        public int? id { get; set; }
        public string identificacao_imovel { get; set; }
        public int? tipo_imovel { get; set; }
        public string ciclo { get; set; }
        public DateTime? data_ultima_visita { get; set; }
        public int? tipo_visita { get; set; }
        public int? desfecho_visita { get; set; }
        public string agente_visita { get; set; }
        public string numero_logradouro { get; set; }
        public int? sequencia_numero { get; set; }
        public string logradouro { get; set; }
        public string bairro { get; set; }
        public string complemento_logradouro { get; set; }
        public string possuiColeta { get; set; }
    }
}
