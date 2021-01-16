using RgCidadao.Domain.Commands.Cadastro;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Queries.Cadastro
{
    public class EscolaCommandText : IEscolaCommand
    {
        public string sqlGetAll = $@"SELECT E.*, L.CSI_NOMEND LOGRADOURO FROM PSE_ESCOLA E
                                     JOIN TSI_LOGRADOURO L ON L.CSI_CODEND = E.ID_LOGRADOURO";
        string IEscolaCommand.GetAll { get => sqlGetAll; }

        public string sqlGetAllPagination = $@"SELECT FIRST (@pagesize) SKIP (@page) E.*, L.CSI_NOMEND LOGRADOURO,
                                                      BAI.CSI_NOMBAI BAIRRO, CID.CSI_NOMCID CIDADE, CID.CSI_SIGEST UF, L.CSI_CODEND CEP
                                               FROM PSE_ESCOLA E
                                               JOIN TSI_LOGRADOURO L ON L.CSI_CODEND = E.ID_LOGRADOURO
                                               JOIN TSI_BAIRRO BAI ON BAI.CSI_CODBAI = L.CSI_CODBAI
                                               JOIN TSI_CIDADE CID ON CID.CSI_CODCID = BAI.CSI_CODCID
                                               @filtros";
        string IEscolaCommand.GetAllPagination { get => sqlGetAllPagination; }

        public string sqlGetCountAll = $@"SELECT COUNT(*) FROM (SELECT E.*, L.CSI_NOMEND LOGRADOURO,
                                                                       BAI.CSI_NOMBAI BAIRRO, CID.CSI_NOMCID CIDADE, CID.CSI_SIGEST UF, L.CSI_CODEND CEP
                                                                FROM PSE_ESCOLA E
                                                                JOIN TSI_LOGRADOURO L ON L.CSI_CODEND = E.ID_LOGRADOURO
                                                                JOIN TSI_BAIRRO BAI ON BAI.CSI_CODBAI = L.CSI_CODBAI
                                                                JOIN TSI_CIDADE CID ON CID.CSI_CODCID = BAI.CSI_CODCID
                                                                @filtros)";
        string IEscolaCommand.GetCountAll { get => sqlGetCountAll; }

        public string GetEscolaById = $@"SELECT * FROM PSE_ESCOLA
                                         WHERE ID = @id";
        string IEscolaCommand.GetEscolaById { get => GetEscolaById; }

        public string sqlGetNewId = $@"SELECT GEN_ID(GEN_PSE_ESCOLA_ID, 1) AS VLR FROM RDB$DATABASE";
        string IEscolaCommand.GetNewId { get => sqlGetNewId; }

        public string sqlInsert = $@"INSERT INTO PSE_ESCOLA (ID, NOME, INEP, ID_LOGRADOURO, TELEFONE)
                                     VALUES (@id, @nome, @inep, @id_logradouro, @telefone)";
        string IEscolaCommand.Insert { get => sqlInsert; }

        public string sqlUpdate = $@"UPDATE PSE_ESCOLA
                                     SET NOME = @nome,
                                         INEP = @inep,
                                         ID_LOGRADOURO = @id_logradouro,
                                         TELEFONE = @telefone
                                     WHERE ID = @id";
        string IEscolaCommand.Update { get => sqlUpdate; }

        public string sqlDelete = $@"DELETE FROM PSE_ESCOLA
                                     WHERE ID = @id";
        string IEscolaCommand.Delete { get => sqlDelete; }
    }
}
