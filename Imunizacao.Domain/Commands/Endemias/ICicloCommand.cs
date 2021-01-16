using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Commands.Endemias
{
    public interface ICicloCommand
    {
        string GetCountAll { get; }
        string GetAllPagination { get; }
        string GetNewId { get; }
        string Insert { get; }
        string GetCicloById { get; }
        string Update { get; }
        string Delete { get; }
        string GetAllCiclosAtivos { get; }
        string ValidaExistenciaCicloPeriodo { get; }
        string GetNumCiclosRestantes { get; }
        string GetCicloByData { get; }
        string CriarLogCiclo { get; }
        string GetLogCicloByCiclo { get; }
        string GetLogCicloNewId { get; }
        string ExcluirLogsByCiclo { get; }

        string CountVisitasCiclo { get; }
        string GetDataMaximaCiclo { get; }
        string GetDataMinimaCiclo { get; }

        string GetAll { get; }
    }
}
