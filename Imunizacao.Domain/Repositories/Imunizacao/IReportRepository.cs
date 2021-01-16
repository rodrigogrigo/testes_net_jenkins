using RgCidadao.Domain.Entities.Imunizacao;
using RgCidadao.Domain.ViewModels.Imunizacao;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Repositories.Imunizacao
{
    public interface IReportRepository
    {
        List<ImunizacaoPacienteImunobiologico> GetReportImunizacao(string ibge, DateTime? dataini, DateTime? datafim, string filtros, string filtroini);

        List<DetalhamentoMovReportViewModel> GetReportMovimento(string ibge, string unidade, string produto, DateTime? datafinal, DateTime? datainicial);

        CartaoVacinaReportViewModel GetCartaoVacinaByIndividuo(string ibge, int id, string sql_estrutura);

        List<BoletimMovimentacao> GetBoletimMovimentacao(string ibge, string unidade, string produto, DateTime? datafinal, DateTime? datainicial);
    }
}
