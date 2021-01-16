using RgCidadao.Domain.Entities.AtencaoBasica;
using RgCidadao.Domain.ViewModels.AtencaoBasica;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Repositories.AtencaoBasica
{
    public interface IExameFisicoRepository
    {
        List<ExameFisicoViewModel> GetAllPagination(string ibge, int? page, int? pagesize, string filtro);
        List<IProcenfermagem> GetIProcenfermagemById(string ibge, int csi_controle);
        int GetCountAll(string ibge, string filtro);

        void Insert(string ibge, ExameFisico model);

        void InsertUpdateProcenfermagem(string ibge, ExameFisico model);
        void InsertUpdateIProcenfermagem(string ibge, ExameFisico model);

        void Update(string ibge, ExameFisico model);
        int GetNewCodigoExameFisico(string ibge);
        int GetNewCodigoControle(string ibge);

        UltimoExameFisicoPac GetLastExameFisicoPac(string ibge, int id_paciente);
        List<HistoricoExameFisicoPac> GetHistoricoExameFisicoPac(string ibge, int idpaciente);

        List<HistoricoExameFisicoPac> GetHistoricoPeso(string ibge, int idpaciente);
        List<HistoricoExameFisicoPac> GetHistoricoAltura(string ibge, int idpaciente);
        List<HistoricoExameFisicoPac> GetHistoricoIMC(string ibge, int idpaciente);
        List<HistoricoExameFisicoPac> GetHistoricoTemperatura(string ibge, int idpaciente);
        List<HistoricoExameFisicoPac> GetHistoricoCircunferenciaAbdominal(string ibge, int idpaciente);
        List<HistoricoExameFisicoPac> GetHistoricoCircunferenciaToracica(string ibge, int idpaciente);
        List<HistoricoExameFisicoPac> GetHistoricoPressaoArterial(string ibge, int idpaciente);
        List<HistoricoExameFisicoPac> GetHistoricoPressaoArterialSistolica(string ibge, int idpaciente);
        List<HistoricoExameFisicoPac> GetHistoricoPressaoArterialDiastolica(string ibge, int idpaciente);
        List<HistoricoExameFisicoPac> GetHistoricoGlicemia(string ibge, int idpaciente);
        List<HistoricoExameFisicoPac> GetHistoricoSaturacao(string ibge, int idpaciente);
        List<HistoricoExameFisicoPac> GetHistoricoFrequenciaCardiaca(string ibge, int idpaciente);
        List<HistoricoExameFisicoPac> GetHistoricoFrequenciaRespiratoria(string ibge, int idpaciente);
        List<HistoricoExameFisicoPac> GetHistoricoFrequenciaCefalico(string ibge, int idpaciente);
        List<HistoricoExameFisicoPac> GetHistoricoGlassGow(string ibge, int idpaciente);
        List<HistoricoExameFisicoPac> GetHistoricoReguaDor(string ibge, int idpaciente);
        List<HistoricoExameFisicoPac> GetHistoricoProcedimentosGerados(string ibge, int idpaciente);

        List<HistoricoObservacaoViewModel> GetHistoricoObservacaoByPaciente(string ibge, int paciente);

        ExameFisico GetExameFisicoById(string ibge, int id);
       
    }
}
