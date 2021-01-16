using Imunizacao.Domain.Commands;

namespace Imunizacao.Domain.Queries
{
    public class ACSCommandText : IACSCommand
    {
        public string sqlGetAll = $@"SELECT CSI_NOMMED, CSI_CODMED
                                     FROM TSI_MEDICOS
                                     WHERE EXCLUIDO <> 'F' AND
                                           CSI_TIPO = 'Agente Comunitário'";
        string IACSCommand.GetAll { get => sqlGetAll; }

        public string sqlGetAllPagination = $@"SELECT FIRST(@pagesize) SKIP(@page) MED.CSI_NOMMED,
                                                      MED.CSI_CODMED, M.DESCRICAO MICROAREA
                                               FROM TSI_MEDICOS MED
                                               LEFT JOIN ESUS_MICROAREA M ON M.ID_PROFISSIONAL = MED.CSI_CODMED
                                               WHERE MED.EXCLUIDO <> 'F' AND
                                                     MED.CSI_TIPO = 'Agente Comunitário'
                                               @filtro";
        string IACSCommand.GetAllPagination { get => sqlGetAllPagination; }

        public string sqlGetCountAll = $@"SELECT COUNT(*)
                                          FROM (SELECT MED.CSI_NOMMED, MED.CSI_CODMED, M.DESCRICAO
                                                FROM TSI_MEDICOS MED
                                                LEFT JOIN ESUS_MICROAREA M ON M.ID_PROFISSIONAL = MED.CSI_CODMED
                                                WHERE MED.EXCLUIDO <> 'F' AND
                                                      MED.CSI_TIPO = 'Agente Comunitário'
                                                      @filtro)";
        string IACSCommand.GetCountAll { get => sqlGetCountAll; }
    }
}
