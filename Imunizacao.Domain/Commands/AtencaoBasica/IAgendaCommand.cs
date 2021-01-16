using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Commands.AtencaoBasica
{
    public interface IAgendaCommand
    {
        string GetAllPagination { get; }
        string GetCountAll { get; }
        string DiasMedByData { get; }

        string GetAgendaDias { get; }
        string ExcluiDiasMed { get; }

        #region Configuração de Agenda
        string GetConfiguracaoProjetoByData { get; }
        string GetConfigProjetoById { get; }
        string GetConsultaItemByDiasMed { get; }

        string GravarAgendaDiasMed { get; }
        string GetNewIdDiasMed { get; }

        string InserirConsultasItens { get; }
        string ExcluirConsultasItemByDiasMed { get; }

        string GetItensByDiasMedOrdem { get; }
        string GetNextOrdemConsultaItem { get; }

        string CancelaReservaConsultaItem { get; }
        #endregion

        #region Agendamento de Consultas
        string GetNewIdConsulta { get; }
        string InserirConsulta { get; }
        string GetConsultaByDiasMedOrdem { get; }
        string CancelarConsulta { get; }
        string ConfirmaAgendaConsulta { get; }

        string GetAgendadosByDiasMed { get; }
        string RemanejaOrdemConsulta { get; }
        #endregion

        #region RG Saude Agenda
        string AtualizaRGSaudeAgenda { get; }

        #endregion
    }
}
