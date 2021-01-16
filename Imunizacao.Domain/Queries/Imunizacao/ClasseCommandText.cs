using RgCidadao.Domain.Commands.Imunizacao;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Queries.Imunizacao
{
    public class ClasseCommandText : IClasseCommand
    {
        public string sqlGetAll = $@"SELECT * FROM PNI_CLASSE";
        string IClasseCommand.GetAll { get => sqlGetAll; }
    }
}
