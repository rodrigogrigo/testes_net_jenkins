using RgCidadao.Domain.Commands.Cadastro;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Queries.Cadastro
{
    public class EstadoCommandText : IEstadoCommand
    {
        public string sqlGetAll = $@"SELECT CSI_CODEST, CSI_NOMEST, CSI_SIGEST
                                     FROM TSI_ESTADO  
                                     ORDER BY CSI_NOMEST";
        string IEstadoCommand.GetAll { get => sqlGetAll; }

        public string sqlGetByid = $@"SELECT CSI_CODEST, CSI_NOMEST, CSI_SIGEST
                                      FROM TSI_ESTADO
                                      WHERE CSI_CODEST = @id";
        string IEstadoCommand.GetEstadoById { get => sqlGetByid; }
    }
}
