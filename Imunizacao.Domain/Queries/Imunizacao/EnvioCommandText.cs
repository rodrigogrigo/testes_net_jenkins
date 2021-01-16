using RgCidadao.Domain.Commands.Imunizacao;
using RgCidadao.Domain.Entities.Imunizacao;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Queries.Imunizacao
{
    public class EnvioCommandText : IEnvioCommand
    {
        public string sqlGetNewIdEnvio = $@"SELECT GEN_ID(GEN_PNI_ENVIO_ID, 1) AS VLR FROM RDB$DATABASE";
        string IEnvioCommand.GetNewIdEnvio { get => sqlGetNewIdEnvio; }

        public string sqlGetAllPagination = $@"SELECT FIRST(@pagesize) SKIP(@page) E.*, UDEST.CSI_NOMUNI UNIDADE_DESTINO, SEG.LOGIN USUARIO
                                               FROM PNI_ENVIO E
                                               JOIN TSI_UNIDADE UDEST ON E.ID_UNIDADE_DESTINO = UDEST.CSI_CODUNI
                                               JOIN SEG_USUARIO SEG ON E.ID_USUARIO = SEG.ID
                                                @filtro";
        string IEnvioCommand.GetAllPagination { get => sqlGetAllPagination; }

        public string sqlGetCountAll = $@"SELECT COUNT(*)
                                          FROM (SELECT E.*, UDEST.CSI_NOMUNI UNIDADE_DESTINO, SEG.LOGIN
                                                FROM PNI_ENVIO E
                                                JOIN TSI_UNIDADE UDEST ON E.ID_UNIDADE_DESTINO = UDEST.CSI_CODUNI
                                                JOIN SEG_USUARIO SEG ON E.ID_USUARIO = SEG.ID
                                                 @filtro)";
        string IEnvioCommand.GetCountAll { get => sqlGetCountAll; }

        public string GetEnvioById = $@"SELECT * FROM PNI_ENVIO
                                        WHERE ID = @id";
        string IEnvioCommand.GetEnvioById { get => GetEnvioById; }

        public string sqlInsertOrUpdate = $@"UPDATE OR INSERT INTO PNI_ENVIO(ID, ID_UNIDADE_ORIGEM, ID_UNIDADE_DESTINO, DATA_ENVIO, ID_USUARIO, STATUS, OBSERVACAO)
                                             VALUES (@id, @id_unidade_origem, @id_unidade_destino, @data_envio, @id_usuario, @status, @observacao)";
        string IEnvioCommand.InsertOrUpdate { get => sqlInsertOrUpdate; }

        public string sqlDelete = $@"DELETE FROM PNI_ENVIO
                                    WHERE ID = @id";
        string IEnvioCommand.Delete { get => sqlDelete; }

        public string sqlGetAllItensByPai = $@"SELECT PEI.*, PP.ABREVIATURA || ' (' || PP.SIGLA || ' )' PRODUTO,
                                                      PLP.LOTE || ' - ' || PPR.NOME || ' (' || EXTRACT(DAY FROM PLP.VALIDADE) || '/' ||
                                                     EXTRACT(MONTH FROM PLP.VALIDADE) || '/' || EXTRACT(YEAR FROM PLP.VALIDADE)|| ')' LOTE_PRODUTOR, PLP.ID_PRODUTO
                                               FROM PNI_ENVIO_ITEM PEI
                                               JOIN PNI_LOTE_PRODUTO PLP ON PLP.ID = PEI.ID_LOTE
                                               JOIN PNI_PRODUTOR PPR ON PPR.ID = PLP.ID_PRODUTOR
                                               JOIN PNI_PRODUTO PP ON PLP.ID_PRODUTO = PP.ID
                                               WHERE ID_ENVIO = @id";
        string IEnvioCommand.GetAllItensByPai { get => sqlGetAllItensByPai; }

        public string sqlGetItemById = $@"SELECT * FROM PNI_ENVIO_ITEM
                                          WHERE ID = @id";
        string IEnvioCommand.GetItemById { get => sqlGetItemById; }

        public string sqlDeleteItem = $@"DELETE FROM PNI_ENVIO_ITEM
                                         WHERE ID = @id";
        string IEnvioCommand.DeleteItem { get => sqlDeleteItem; }

        public string sqlInsertOrUpdateItens = $@"UPDATE OR INSERT INTO PNI_ENVIO_ITEM (ID, ID_ENVIO, ID_LOTE, QTDE_FRASCOS, VALOR)
                                                  VALUES ( @id,@id_envio, @id_lote ,@qtde_frascos,  @valor)";
        string IEnvioCommand.InsertOrUpdateItens { get => sqlInsertOrUpdateItens; }

        public string sqlGetNewIdItem = $@"SELECT GEN_ID(GEN_PNI_ENVIO_ITEM_ID, 1) AS VLR FROM RDB$DATABASE";
        string IEnvioCommand.GetNewIdItem { get => sqlGetNewIdItem; }

        public string sqlUpdateStatusEnvio = $@"UPDATE PNI_ENVIO
                                                SET STATUS = 1
                                                WHERE ID = @id";
        string IEnvioCommand.UpdateStatusEnviado { get => sqlUpdateStatusEnvio; }

        public string sqlValidaEstoqueItensEnvio = $@"SELECT
                                                          SUM(
                                                          CASE WHEN (SELECT COUNT(*) FROM PNI_LOTE_UNIDADE_BLOQUEADO LUB
                                                                     WHERE LUB.ID_LOTE = LP.ID AND LUB.ID_UNIDADE = @id_unidade) = 1 THEN 1
                                                          WHEN
                                                              ((SELECT EST.QTDE FROM PNI_ESTOQUE_PRODUTO EST
                                                              WHERE EST.LOTE = LP.LOTE
                                                              AND EST.ID_PRODUTO = LP.ID_PRODUTO
                                                              AND EST.ID_PRODUTOR = LP.ID_PRODUTOR
                                                              AND EST.ID_UNIDADE = E.ID_UNIDADE_ORIGEM) -
                                                              (EI.QTDE_FRASCOS * A.QUANTIDADE)) < 0
                                                          THEN 1
                                                          ELSE 0
                                                          END) AS INDISPONIVEL
                                                      FROM PNI_ENVIO E
                                                      JOIN PNI_ENVIO_ITEM EI ON E.ID = EI.ID_ENVIO
                                                      JOIN PNI_LOTE_PRODUTO LP ON EI.ID_LOTE = LP.ID
                                                      JOIN PNI_APRESENTACAO A ON LP.ID_APRESENTACAO = A.ID
                                                      WHERE E.ID = @id";
        string IEnvioCommand.ValidaEstoqueItensEnvio { get => sqlValidaEstoqueItensEnvio; }

        public string sqlGetTranferenciaByUnidadeDestino = $@"SELECT ENV.*, UNI.CSI_NOMUNI UNIDADE_ORIGEM, SU.LOGIN USUARIO
                                                              FROM PNI_ENVIO ENV
                                                              JOIN TSI_UNIDADE UNI ON UNI.CSI_CODUNI = ENV.ID_UNIDADE_ORIGEM
                                                              JOIN SEG_USUARIO SU ON SU.ID = ENV.ID_USUARIO
                                                              WHERE ENV.ID_UNIDADE_DESTINO = @id AND
                                                                    ENV.STATUS = 1";
        string IEnvioCommand.GetTranferenciaByUnidadeDestino { get => sqlGetTranferenciaByUnidadeDestino; }
    }
}
