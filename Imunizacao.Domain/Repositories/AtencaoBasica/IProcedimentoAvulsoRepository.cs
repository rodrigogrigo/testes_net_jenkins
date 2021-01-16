using RgCidadao.Domain.Entities.AtencaoBasica;
using RgCidadao.Domain.ViewModels.AtencaoBasica;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Repositories.AtencaoBasica
{
    public interface IProcedimentoAvulsoRepository
    {
        List<ProcedimentoAvulso> GetAllPagination(string ibge, string filtro, int page, int pagesize, int? usuario);
        int GetCountAll(string ibge, string filtro, int? usuario);
        int GetNewId(string ibge);
        void Insert(string ibge, ProcEnfermagem model);
        void Update(string ibge, ProcEnfermagem model);
        void Delete(string ibge, int id);
        ProcEnfermagem GetProcEnfermagemById(string ibge, int id);

        int GetNewIdItem(string ibge);
        void InsertItem(string ibge, ProcEnfermagemItem model);
        void DeleteItem(string ibge, int id_controle, int id_codproc);

        List<ProcedimentosConsolidadosViewModel> GetProcedimentosConsolidados(string ibge, string cbo, string filtro);
        List<ProcedimentosConsolidadosViewModel> GetProcedimentosIndividualizado(string ibge, string cbo, string filtroSIGTAP, string filtroESUS);
    }
}
