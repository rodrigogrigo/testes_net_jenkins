using RgCidadao.Domain.Entities.Endemias;
using RgCidadao.Domain.ViewModels.Endemias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RgCidadao.Api.ViewModels.Endemias
{
    public class ReportEndemiasViewModel
    {
        public ReportEndemiasViewModel()
        {
            itens = new List<InfestacaoPredialViewModel>();
            AntivetorialAnalitico = new List<AntivetorialAnaliticoViewModel>();
            AntivetorialTotalizador = new AntivetorialTotalizadorViewModel();
            AntivetorialCamposTotais = new AntivetorialCampoTotaisViewModel();
            AntivetorialResumoLab = new List<AntivetorialResumoLabViewModel>();
            AntivetorialInfectados = new List<AntivetorialInfectadosViewModel>();
        }
        public string filtro { get; set; }
        public string impresso_por { get; set; }
        public int unidade { get; set; }
        public string ibge { get; set; }
        public List<InfestacaoPredialViewModel> itens { get; set; }
        public List<AntivetorialAnaliticoViewModel> AntivetorialAnalitico { get; set; }
        public byte[] cabecalho { get; set; }
        public DateTime? datainicial { get; set; }
        public DateTime? datafinal { get; set; }
        public string ciclo { get; set; }

        public AntivetorialTotalizadorViewModel AntivetorialTotalizador { get; set; }
        public AntivetorialCampoTotaisViewModel AntivetorialCamposTotais { get; set; }
        public List<AntivetorialResumoLabViewModel> AntivetorialResumoLab { get; set; }
        public List<AntivetorialInfectadosViewModel> AntivetorialInfectados { get; set; } //lista mesmo
    }
}
