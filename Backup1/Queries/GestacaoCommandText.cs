using Imunizacao.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Imunizacao.Domain.Queries
{
    public class GestacaoCommandText : IGestacaoCommand
    {
        public string sqlIsGestante = $@"SELECT GI.NOME_MATERNIDADE, COUNT(GI.ID) QTD
                                         FROM GESTACAO G
                                         JOIN GESTACAO_ITEM GI ON (G.ID = GI.ID_GESTACAO)
                                         WHERE G.ID_CIDADAO = @id AND
                                               G.FLG_GESTACAO_EM_ANDAMENTO = 'T'
                                         GROUP BY GI.NOME_MATERNIDADE";
        string IGestacaoCommand.IsGestante { get => sqlIsGestante; }

        public string sqlGestacaoByCidadao = $@"SELECT *
                                                FROM GESTACAO G
                                                WHERE G.ID_CIDADAO = @id_cidadao";
        string IGestacaoCommand.GetGestacaoByCidadao { get => sqlGestacaoByCidadao; }

        public string sqlGetGestacaoItemByGestacao = $@"SELECT GI.ID, GI.DUM FROM GESTACAO_ITEM GI
                                                        WHERE GI.ID_GESTACAO = @id_gestacao AND
                                                              GI.FLG_DESFECHO = 0                  
                                                        ORDER BY GI.DUM";
        string IGestacaoCommand.GetGestacaoItensByGestacao { get => sqlGetGestacaoItemByGestacao; }

        public string sqlGetGestacaoItemUltima = $@"SELECT GI.ID, GI.DUM, GI.FLG_DESFECHO, GI.FLG_DESFECHO, GI.DATA_NASCIMENTO
                                                    FROM GESTACAO_ITEM GI
                                                    WHERE GI.ID_GESTACAO = @id_gestacao
                                                    ORDER BY GI.DUM";
        string IGestacaoCommand.GetUltimaGestacaoItem { get => sqlGetGestacaoItemUltima; }

    }
}
