using RgCidadao.Domain.Commands.Imunizacao;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Queries.Imunizacao
{
    public class ImunobiologicoCommandText : IImunobiologicoCommand
    {
        public string sqlGetAllImunobiologico = $@"SELECT * FROM PNI_IMUNOBIOLOGICO";
        string IImunobiologicoCommand.GetAllImunobiologico { get => sqlGetAllImunobiologico; }


    }
}
