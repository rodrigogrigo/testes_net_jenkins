using RgCidadao.Domain.Entities.AtencaoBasica;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Repositories.AtencaoBasica
{
    public interface IProcedimentoRepository
    {
        List<Procedimento> GetProcedimentosByCompetencia022019(string ibge);
        List<Procedimento> GetProcedimentoBycbo(string ibge, string cbo);
    }
}
