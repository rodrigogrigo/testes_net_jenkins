using RgCidadao.Domain.Entities.Prontuario;
using System.Collections.Generic;

namespace RgCidadao.Domain.Repositories.Prontuario
{
    public interface IExameRepository
    {
        List<Exame> GetExamesComuns(string ibge);
        List<Exame> GetExamesAltoCustos(string ibge);
        List<Exame> GetHistoricoSolicitacoesExameByPaciente(string ibge, int id_paciente, int agrupamento);
        List<Exame> GetHistoricoResultadoExameByPaciente(string ibge, int id_exame);
        List<AgrupamentoExames> GetListAgrupamentosExames(string ibge);
        List<Cid> GetCid(string ibge);

    }
}
