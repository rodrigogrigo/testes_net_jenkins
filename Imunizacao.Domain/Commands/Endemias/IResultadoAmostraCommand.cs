using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Commands.Endemias
{
    public interface IResultadoAmostraCommand
    {
        string GetAllPagination { get; }
        string GetCountAll { get; }
        string GetResultadoColetaByProfissional { get; }
        string GetColetaResultadoByVisita { get; }
        string UpdateColetaResultado { get; }
        string InsertColetaResultado { get; }
        string DeleteColetaResultado { get; }
        string GetColetaResultadoByColeta { get; }
        string DeleteColetaResultadoByColeta { get; }
        string GetResultadoAmostraNewId { get; }
        string GetResultadoAmostraByVisita { get; }
        string GetResultadoAmostraByProfissional { get; }
        string PendenteVisitaGetAllPagination { get; }
        string LancadaVisitaGetAllPagination { get; }
    }
}
