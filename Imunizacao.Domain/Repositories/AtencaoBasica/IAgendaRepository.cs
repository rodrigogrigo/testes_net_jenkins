using RgCidadao.Domain.Entities.AtencaoBasica;
using RgCidadao.Domain.ViewModels.AtencaoBasica;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Repositories.AtencaoBasica
{
    public interface IAgendaRepository
    {
        List<AgendaHorariosViewModel> GetAll(string ibge, DateTime data, int id_profissional);
        int GetCountAll(string ibge, int id_dia_med);
        List<AgendaDiasViewModel> GetAgendaDias(string ibge, int id_profissional, DateTime? datainicial, DateTime? datafinal);
        void ExcluiDiasMed(string ibge, int id_dias_med);

        #region Configuração de Agenda
        List<ConfiguraAgendaViewModel> GetConfiguracaoProjetoByData(string ibge, int codmed, DateTime data);
        DiasMed GetConfigProjetoById(string ibge, int id);
        void GravarAgendaDiasMed(string ibge, DiasMed model);
        int GetNewIdDiasMed(string ibge);
        void InserirConsultasItens(string ibge, TimeSpan horaini, TimeSpan horafim, int qtdeVagas, int id_dias_med, int intervalo);
        void ExcluirConsultasItemByDiasMed(string ibge, int id_dias_med);

        List<ConsultasItem> GetConsultasItensByDiasMed(string ibge, int id_dias_med);
        ConsultasItem GetItensByDiasMedOrdem(string ibge, int id_diasmed, int ordem);
        void CancelaReservaConsultaItem(string ibge, int dias_med, int ordem);

        int GetNextOrdemConsultaItem(string ibge, int id_diasmed);
        #endregion

        #region Agendamento de Consultas
        void InserirConsulta(string ibge, Consultas model);
        int GetNewIdConsulta(string ibge);
        Consultas GetConsultaByDiasMedOrdem(string ibge, int id_dias_med, int ordem);
        void CancelarConsulta(string ibge, string status, string usuario, DateTime data, int id_controle, string obs);
        void ConfirmaAgendaConsulta(string ibge, string status, DateTime data, string nome_usu, int id_controle);

        List<AgendaDiasViewModel> GetAgendadosByDiasMed(string ibge, int id_diasmed);

        void RemanejaOrdemConsulta(string ibge, int ordem, string horario, int csi_controle);
        #endregion

        #region RG Saude Agenda
        void AtualizaRGSaudeAgenda(string ibge, int id_consulta, int fRG_Saude_Agenda_ID);
        #endregion
    }
}
