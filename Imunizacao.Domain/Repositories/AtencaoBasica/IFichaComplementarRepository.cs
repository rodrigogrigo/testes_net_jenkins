using RgCidadao.Domain.ViewModels.AtencaoBasica;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Repositories.AtencaoBasica
{
    public interface IFichaComplementarRepository
    {
        List<FichaComplementarViewModel> GetAllPagination(string ibge, string filtro, int page, int pagesize, int usuario);
        int GetCountAll(string ibge, string filtro);
        FichaComplementarViewModel GetFichaComplementarById(string ibge, int id);

        void Insert(string ibge, FichaComplementarViewModel model);

        int GetNewId(string ibge);

        void Update(string ibge, FichaComplementarViewModel model);

        void Excluir(string ibge, int id);

    }
}
