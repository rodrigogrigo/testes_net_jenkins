using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Commands.Endemias
{
    public interface IVisitaCommand
    {
        string GetAllPagination { get; }
        string GetCountAll { get; }

        string GetNewIdVisita { get; }
        string InsertVisita { get; }
        string GetNewIdColeta { get; }
        string InsertColeta { get; }
        string GetVisitaById { get; }
        string GetColetaByVisita { get; }
        string UpdateVisita { get; }
        string UpdateColeta { get; }
        string DeleteVisita { get; }
        string DeleteColeta { get; }
        string GetVisitaByEstabelecimento { get; }
        string GetVisitasByCiclo { get; }

        string GetQuarteiraoEstabelecimentoByBairro { get; }
        string GetUltimaVisitaCicloByEstabelecimento { get; }
    }
}
