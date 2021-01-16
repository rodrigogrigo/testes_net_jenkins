using RgCidadao.Domain.Commands.Cadastro;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Queries.Cadastro
{
    public class BairroCommandText : IBairroCommand
    {
        public string sqlGetCountAll = $@"SELECT COUNT(*)
                                          FROM TSI_BAIRRO 
                                          @filtros";
        string IBairroCommand.GetCountAll { get => sqlGetCountAll; }

        public string sqlGetAllPagination = $@"SELECT FIRST (@pagesize) SKIP (@page) CSI_CODBAI, CSI_NOMBAI, CSI_CODCID
                                               FROM TSI_BAIRRO 
                                               @filtros
                                               ORDER BY CSI_NOMBAI";
        string IBairroCommand.GetAllPagination { get => sqlGetAllPagination; }

        public string sqlBairroById = $@"SELECT CSI_CODBAI, CSI_NOMBAI, CSI_CODCID
                                        FROM TSI_BAIRRO
                                        WHERE CSI_CODBAI = @id";
        string IBairroCommand.GetBairroById { get => sqlBairroById; }

        public string sqlGetAll = $@"SELECT * FROM TSI_BAIRRO";
        string IBairroCommand.GetAll { get => sqlGetAll; }

        public string sqlGetBairroByIbge = $@"SELECT BAI.* FROM TSI_BAIRRO BAI
                                              WHERE BAI.CSI_CODCID = @ibge";
        string IBairroCommand.GetBairroByIbge { get => sqlGetBairroByIbge; }
    }
}
