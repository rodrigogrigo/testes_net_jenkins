using RgCidadao.Domain.Commands.Imunizacao;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Queries.Imunizacao
{
    public class UnidadeMedCommandText : IUnidadeMedCommand
    {
        public string sqlGetAll = $@"SELECT * FROM PNI_UNIDADE";
        string IUnidadeMedCommand.GetAll { get => sqlGetAll; }
    }
}
