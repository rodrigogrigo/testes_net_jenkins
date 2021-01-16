using RgCidadao.Domain.Entities.E_SUS;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Repositories.E_SUS
{
    public interface IProcedimentoRepository
    {
        List<ProcedimentoAvulso> GetAllPagination(string ibge, string filtro, int page, int pagesize);
        int GetCountAll(string ibge, string filtro);
        ProcedimentoAvulso GetProcEnfermagemById(string ibge, int id);
        List<ProcedimentoAvulsoItem> GetProcEnfermagemItensByPai(string ibge, int id);
        ProcedimentoAvulsoItem GetProcEnfermagemItemByPaiProc(string ibge, int id, int proc);
        int GetNewId(string ibge);
        void Inserir(string ibge, ProcedimentoAvulso model);
        int GetNewIdItem(string ibge);
        string GetNewUuid(string ibge);
        void InserirItem(string ibge, ProcedimentoAvulsoItem model);
        void Editar(string ibge, ProcedimentoAvulso model);
        void EditarItem(string ibge, ProcedimentoAvulsoItem model);
        void Excluir(string ibge, int id);
        void ExcluirItem(string ibge, int id, int idproc);
    }
}
