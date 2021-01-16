using RgCidadao.Domain.Commands.Cadastro;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Queries.Cadastro
{
    public class LogradouroCommandText : ILogradouroCommand
    {
        public string sqlGetAllPagination = $@"SELECT FIRST(@pagesize) SKIP(@page) LOGR.CSI_CODEND, LOGR.CSI_NOMEND,
                                                      LOGR.CSI_CEP, LOGR.CSI_CODBAI, BAI.CSI_NOMBAI BAIRRO, CID.CSI_NOMCID CIDADE,
                                                      CID.CSI_CODCID, EST.CSI_NOMEST ESTADO
                                               FROM TSI_LOGRADOURO LOGR
                                               JOIN TSI_BAIRRO BAI ON BAI.CSI_CODBAI = LOGR.CSI_CODBAI
                                               JOIN TSI_CIDADE CID ON CID.CSI_CODCID = BAI.CSI_CODCID
                                               JOIN TSI_ESTADO EST ON EST.CSI_CODEST = CID.CSI_CODEST
                                               @filtros
                                               ORDER BY LOGR.CSI_NOMEND";
        string ILogradouroCommand.GetAllPagination { get => sqlGetAllPagination; }

        public string sqlGetCountAll = $@"SELECT COUNT(*)
                                          FROM (SELECT LOGR.CSI_CODEND, LOGR.CSI_NOMEND,
                                                      LOGR.CSI_CEP, LOGR.CSI_CODBAI, BAI.CSI_NOMBAI BAIRRO, CID.CSI_NOMCID CIDADE,
                                                      EST.CSI_NOMEST ESTADO
                                               FROM TSI_LOGRADOURO LOGR
                                               JOIN TSI_BAIRRO BAI ON BAI.CSI_CODBAI = LOGR.CSI_CODBAI
                                               JOIN TSI_CIDADE CID ON CID.CSI_CODCID = BAI.CSI_CODCID
                                               JOIN TSI_ESTADO EST ON EST.CSI_CODEST = CID.CSI_CODEST
                                               @filtros)";
        string ILogradouroCommand.GetCountAll { get => sqlGetCountAll; }

        public string sqlGetLogradouroById = $@"SELECT L.CSI_CODEND, L.CSI_NOMEND, L.CSI_CEP, L.CSI_CODBAI, B.CSI_NOMBAI BAIRRO,
                                                       CID.CSI_NOMCID CIDADE, CID.CSI_SIGEST SIGLA_ESTADO
                                                FROM TSI_LOGRADOURO L
                                                JOIN TSI_BAIRRO B ON B.CSI_CODBAI = L.CSI_CODBAI
                                                JOIN TSI_CIDADE CID ON CID.CSI_CODCID = B.CSI_CODCID
                                                WHERE L.CSI_CODEND =  @id";
        string ILogradouroCommand.GetLogradouroById { get => sqlGetLogradouroById; }

        public string sqlGetLogradouroByBairro = $@"SELECT CSI_CODEND, CSI_NOMEND FROM TSI_LOGRADOURO
                                                    WHERE CSI_CODBAI = @bairro";
        string ILogradouroCommand.GetLogradouroByBairro { get => sqlGetLogradouroByBairro; }


    }
}
