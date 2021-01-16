using RgCidadao.Domain.Entities.AtencaoBasica;
using RgCidadao.Domain.ViewModels.AtencaoBasica;
using System;
using System.Collections.Generic;

namespace RgCidadao.Domain.Repositories.AtencaoBasica
{
    public interface IEstabelecimentoSaudeRepository
    {
        EstabelecimentoSaude GetById(string ibge, int id);
        List<EstabelecimentoSaude> GetAll(string ibge);
    }
}
