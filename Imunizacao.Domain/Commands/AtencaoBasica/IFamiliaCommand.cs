using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Commands.AtencaoBasica
{
    public interface IFamiliaCommand
    {
        string GetFamiliaById { get; }
        string GetNewId { get; }
        string Insert { get; }
        string Update { get; }

        string GetProntuarioUso { get; }
        string GetQtdMaximaFamiliaMicroarea { get; }
        string AtualizaCadPacFamilia { get; }
        string GetIndividuoFamilia { get; }
        string AtualizaResponsavelFamilia { get; }

        string AtualizaFamiliaOutraArea { get; }
        string AtualizaFamiliaDomicilio { get; }

        string GetFamiliaByIndividuoResponsavel { get; }
    }
}
