using RgCidadao.Domain.Entities.AtencaoBasica;
using RgCidadao.Domain.ViewModels.AtencaoBasica;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Repositories.AtencaoBasica
{
    public interface IEstabelecimentoRepository
    {
        Estabelecimento GetEstabelecimentoById(string ibge, int id);
        int GetNewId(string ibge);
        void Insert(string ibge, Estabelecimento model);
        void Update(string ibge, Estabelecimento model);
        List<EstabelecimentoViewModel> GetEstabelecimentosByArea(string ibge, int page, int pagesize, string filtro, int microarea);
        int GetCountEstabelecimentosByArea(string ibge, string filtro, int microarea);
    }
}
