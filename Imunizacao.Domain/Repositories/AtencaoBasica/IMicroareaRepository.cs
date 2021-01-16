using RgCidadao.Domain.ViewModels.AtencaoBasica;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Repositories.AtencaoBasica
{
    public interface IMicroareaRepository
    {
        List<MicroareaViewModel> GetMicroareas(string ibge);
        List<MicroareaViewModel> GetMicroareasByUnidade(string ibge, int id_unidade);
    }
}
