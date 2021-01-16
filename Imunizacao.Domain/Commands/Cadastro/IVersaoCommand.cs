using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Commands.Cadastro
{
    public interface IVersaoCommand
    {
        string GetUltimoCodigoScript { get; }
        string UpdateCodigoScript { get; }
        string VerificaTabelaVersaoExiste { get; }
        string VerificaExisteRegistroTabelaVersao { get; }
        string InserePrimeiroRegTabVersao { get; }
        string CriaTabelaVersao { get; }
        string CriaContraintTabelaVersao { get; }
    }
}
