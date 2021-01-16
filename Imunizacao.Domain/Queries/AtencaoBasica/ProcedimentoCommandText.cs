using RgCidadao.Domain.Commands.AtencaoBasica;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Queries.AtencaoBasica
{
    public class ProcedimentoCommandText : IProcedimentoCommand
    {
        public string sqlGetProcedimentosByCompetencia022019 = $@"SELECT *
                                                                  FROM TSI_PROCEDIMENTO
                                                                  WHERE CODIGO IN ('0101010010', '0101010028', '0101020023', '0101020040', '0101020082', '0101050011', '0101050020', '0101050046', '0101050054', '0101050062',
                                                                                   '0101050070', '0101050089', '0101050100', '0101050119', '0101050127', '0101050135', '0102020027') ";
        string IProcedimentoCommand.GetProcedimentosByCompetencia022019 { get => sqlGetProcedimentosByCompetencia022019; }

        public string sqlGetProcedimentoBycbo = $@"SELECT DISTINCT P.CODIGO, P.NOME 
                                                   FROM TSI_PROCEDIMENTO P
                                                   JOIN TSI_PROCEDIMENTO_CBO PCBO ON (PCBO.COD_PROCEDIMENTO = P.CODIGO)
                                                   WHERE PCBO.COD_CBO = @cbo AND
                                                         PCBO.COMPETENCIA = (SELECT MAX(COMPETENCIA)
                                                                             FROM COMPETENCIA_SIGTAP
                                                                             WHERE ATIVO = 'T')";
        string IProcedimentoCommand.GetProcedimentoBycbo { get => sqlGetProcedimentoBycbo; }
    }
}
