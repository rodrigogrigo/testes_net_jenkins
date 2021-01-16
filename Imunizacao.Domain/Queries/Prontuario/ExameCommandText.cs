using RgCidadao.Domain.Commands.Prontuario;

namespace RgCidadao.Domain.Queries.Prontuario
{
    public class ExameCommandText : IExameCommand
    {
        public string sqlGetExamesComuns = $@"SELECT CE.*
                                              FROM TSI_CADEXAMES CE
                                              JOIN TSI_PROCEDIMENTO P ON (CE.CSI_CODSUS = P.CODIGO)
                                              WHERE P.COD_GRUPO = '02' AND P.COMPLEXIDADE IN (1,2) AND CE.FLG_ATIVO = 'True'
                                              ORDER BY CE.CSI_NOME";
        string IExameCommand.GetExamesComuns { get => sqlGetExamesComuns; }

        public string sqlGetExamesAltoCustos = $@"SELECT CE.*
                                                FROM TSI_CADEXAMES CE
                                                JOIN TSI_PROCEDIMENTO P ON (CE.CSI_CODSUS = P.CODIGO)
                                                WHERE P.COD_GRUPO = '02' AND P.COMPLEXIDADE IN (3) AND CE.FLG_ATIVO = 'True'
                                                ORDER BY CE.CSI_NOME";
        string IExameCommand.GetExamesAltoCustos { get => sqlGetExamesAltoCustos; }

        public string sqlGetHistoricoSolicitacoesExameByPaciente = $@"SELECT
                                                                    REQ_EXA.ID AS ID_REQUISICAO,
                                                                    PEP_ATEN.ID_AGENDAMENTO,
                                                                    CAD_EXA.CSI_CODEXA,
                                                                    CAD_EXA.CSI_NOME,
                                                                    REQ_EXA.ID_PROFISSIONAL_EXAME,
                                                                    REQ_EXA.QUANTIDADE,
                                                                    REQ_EXA.FLG_SOLICITADO,
                                                                    REQ_EXA.FLG_AVALIADO,
                                                                    REQ_EXA.FLG_EXAME_REALIZADO,
                                                                    REQ_EXA.DATA_HORA_SOLICITACAO,
                                                                    REQ_EXA.DATA_HORA_AVALIADO,
                                                                    REQ_EXA.DATA_HORA_RESULTADO,
                                                                    REQ_EXA.FLG_CANCELADO,
                                                                    PAC.CSI_CODPAC,
                                                                    PAC.CSI_NOMPAC,
                                                                    PROC.CSI_NOME AS NOME_AGRUPAMENTO
                                                                    FROM PEP_REQUISICAO_EXAME REQ_EXA
                                                                    JOIN TSI_CADEXAMES CAD_EXA ON (CAD_EXA.CSI_CODEXA = REQ_EXA.ID_EXAME)
                                                                    JOIN PEP_ATENDIMENTO PEP_ATEN ON (PEP_ATEN.ID = REQ_EXA.ID_ATENDIMENTO)
                                                                    JOIN TSI_CADPAC PAC ON (PAC.CSI_CODPAC = PEP_ATEN.ID_PACIENTE)
                                                                    JOIN TSI_PROCEDIMENTO_SUB_GRUPO PROC ON ((PROC.CSI_CODIGO_GRUPO||PROC.CSI_CODIGO) = LEFT(CAD_EXA.CSI_CODSUS,4))
                                                                    WHERE PAC.CSI_CODPAC = @id_paciente
                                                                    AND LEFT(CAD_EXA.CSI_CODSUS,4) = @agrupamento
                                                                    ORDER BY REQ_EXA.DATA_HORA_SOLICITACAO DESC";

        string IExameCommand.GetHistoricoSolicitacoesExameByPaciente { get => sqlGetHistoricoSolicitacoesExameByPaciente; }

        public string sqlGetHistoricoResultadoExameByPaciente = $@"SELECT CE.*
                                                FROM TSI_CADEXAMES CE
                                                JOIN TSI_PROCEDIMENTO P ON (CE.CSI_CODSUS = P.CODIGO)
                                                WHERE P.COD_GRUPO = '02' AND P.COMPLEXIDADE IN (3) AND CE.FLG_ATIVO = 'True'";

        string IExameCommand.GetHistoricoResultadoExameByPaciente { get => sqlGetHistoricoResultadoExameByPaciente; }

        public string sqlGetListAgrupamentosExames = $@"SELECT
                                                        (PROC.CSI_CODIGO_GRUPO||PROC.CSI_CODIGO) AS CODIGO_AGRUPAMENTO,
                                                        PROC.CSI_NOME AS NOME_AGRUPAMENTO
                                                        FROM TSI_PROCEDIMENTO_SUB_GRUPO PROC
                                                        WHERE PROC.CSI_CODIGO_GRUPO = 02";

        string IExameCommand.GetListAgrupamentosExames { get => sqlGetListAgrupamentosExames; }

        public string sqlGetCid = $@"SELECT * FROM TSI_CID";

        string IExameCommand.GetCid { get => sqlGetCid; }
    }
}
