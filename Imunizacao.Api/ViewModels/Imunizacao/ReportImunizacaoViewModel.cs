using RgCidadao.Domain.Entities.Imunizacao;
using RgCidadao.Domain.ViewModels.Imunizacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RgCidadao.Api.ViewModels.Imunizacao
{
    public class ReportImunizacaoViewModel
    {
        public ReportImunizacaoViewModel()
        {
            itens = new List<ImunizacaoPacienteImunobiologico>();
            Detalhamento = new List<DetalhamentoMovReportViewModel>();
            itensBoletim = new List<BoletimMovimentacao>();
        }
        public string filtro { get; set; }
        public string impresso_por { get; set; }
        public int unidade { get; set; }
        public string ibge { get; set; }
        public List<ImunizacaoPacienteImunobiologico> itens { get; set; }
        public byte[] cabecalho { get; set; }
        public List<DetalhamentoMovReportViewModel> Detalhamento { get; set; }
        public List<BoletimMovimentacao> itensBoletim { get; set; }
    }
}
