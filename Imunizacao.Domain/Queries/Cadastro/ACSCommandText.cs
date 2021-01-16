using RgCidadao.Domain.Commands.Cadastro;

namespace RgCidadao.Domain.Queries.Cadastro
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

        public string sqlGetAcsByEquipe = $@"SELECT M.CSI_NOMMED, M.CSI_CODMED
                                             FROM TSI_MEDICOS M
                                             JOIN ESUS_LOTACAO_PROFISSIONAIS LP ON LP.ID_PROFISSIONAL = M.CSI_CODMED
                                             JOIN ESUS_EQUIPES E ON (LP.ID_EQUIPE = E.ID)
                                             WHERE COALESCE(M.EXCLUIDO, 'F') <> 'T' AND
                                                   M.CSI_TIPO = 'Agente Comunitário' AND
                                                   E.ID = @id";
        string IACSCommand.GetAcsByEquipe { get => sqlGetAcsByEquipe; }
    }
}
