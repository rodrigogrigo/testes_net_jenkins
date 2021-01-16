using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Commands.AtencaoBasica
{
    public interface IExameFisicoCommand
    {
        string GetAllPagination { get; }
        string GetIprocenfermagemById { get; }
        string GetCountAll { get; }

        string Insert { get; }

        string Update { get; }

        string InsertUpdateProcenfermagem { get; }
        string InsertUpdateIProcenfermagem { get; }

        string GetNewCodigoExameFisico { get; }
        string GetNewCodigoControle { get; }

        string GetLastPesoByPaciente { get; }
        string GetLastAlturaPaciente { get; }
        string GetLastImcPaciente { get; }
        string GetLastTemperaturaPaciente { get; }
        string GetLastCircunferenciaAbdominalPaciente { get; }
        string GetLastCircunferenciaToracicaPaciente { get; }
        string GetLastPressaoArterialPaciente { get; }

        string GetLastPressaoArterialSistolicaPaciente { get; }

        string GetLastPressaoArterialDiastolicaPaciente { get; }

        string GetLastGlicemiaPaciente { get; }
        string GetLastSaturacaoPaciente { get; }
        string GetLastFrequenciaCardiacaPaciente { get; }
        string GetLastFrequenciaRespiratoriaPaciente { get; }
        string GetLastFrequenciaCefalicoPaciente { get; }
        string GetLastGlassGowPaciente { get; }
        string GetLastReguaDorPaciente { get; }
        string GetLastProcedimentosGeradosPaciente { get; }

        string GetListaPesoByPaciente { get; }
        string GetListaAlturaByPaciente { get; }
        string GetListaIMCByPaciente { get; }
        string GetListaTemperaturaByPaciente { get; }
        string GetListaCircunferenciaAbdominalByPaciente { get; }
        string GetListaCircunferenciaToracicaByPaciente { get; }

        string GetListaPressaoArterialByPaciente { get; }
        string GetListaPressaoArterialSistolicaByPaciente { get; }
        string GetListaPressaoArterialDiastolicaByPaciente { get; }

        string GetListaGlicemiaByPaciente { get; }
        string GetListaSaturacaoByPaciente { get; }
        string GetListaFrequenciaCardiacaByPaciente { get; }
        string GetListaFrequenciaRespiratoriaByPaciente { get; }
        string GetListaFrequenciaCefalicoByPaciente { get; }
        string GetListaGlassGowByPaciente { get; }
        string GetListaReguaDorByPaciente { get; }
        string GetProcedimentosGeradosByPaciente { get; }

        string GetHistoricoObservacaoByPaciente { get; }

        string GetExameFisicoById { get; }
    }
}
