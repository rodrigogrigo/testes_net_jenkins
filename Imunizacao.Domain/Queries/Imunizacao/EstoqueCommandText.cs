using RgCidadao.Domain.Commands.Imunizacao;

namespace RgCidadao.Domain.Queries.Imunizacao
{
    public class EstoqueCommandText : IEstoqueCommand
    {
        public string sqlGetAllUnidadeWithEstoque = $@"SELECT DISTINCT UN.CSI_CODUNI ID, UN.CSI_NOMUNI UNIDADE, UN.CSI_CNES CNES,
                                                                       (SELECT SUM(PER.QTDE) FROM PNI_ESTOQUE_PRODUTO PER
                                                                       WHERE PER.ID_UNIDADE = UN.CSI_CODUNI
                                                                       AND PER.ID_PRODUTO = @produto) AS QTDE
                                                       FROM TSI_UNIDADE UN
                                                       JOIN SEG_PERFIL_USUARIO PU ON (UN.CSI_CODUNI = PU.ID_UNIDADE)
                                                       WHERE COALESCE(UN.EXCLUIDO, 'F') = 'F'
                                                       AND PU.ID_USUARIO = @user
                                                       AND (SELECT SUM(PER.QTDE) FROM PNI_ESTOQUE_PRODUTO PER
                                                            WHERE PER.ID_UNIDADE = UN.CSI_CODUNI AND PER.ID_PRODUTO = @produto) > 0
                                                       ORDER BY UN.CSI_NOMUNI;";
        string IEstoqueCommand.GetAllUnidadeWithEstoque { get => sqlGetAllUnidadeWithEstoque; }

        public string sqlGetEstoqueLoteByUnidadeAndProduto = $@"SELECT DISTINCT EP.*, PP.NOME NOME_PRODUTOR, LP.VALIDADE
                                                                FROM PNI_ESTOQUE_PRODUTO EP
                                                                JOIN PNI_PRODUTOR PP ON PP.ID = EP.ID_PRODUTOR
                                                                JOIN PNI_LOTE_PRODUTO LP ON LP.LOTE = EP.LOTE
                                                                WHERE ep.id_produto = @id_produto AND
                                                                      EP.id_unidade = @id_unidade";
        string IEstoqueCommand.GetEstoqueLoteByUnidadeAndProduto { get => sqlGetEstoqueLoteByUnidadeAndProduto; }

        public string sqlGetAuditoria = $@" SELECT
                                            FIRST(@pagesize) SKIP(@page)
                                            MP.ID,
                                            MP.DATA,
                                            CASE MP.OPERACAO
                                            WHEN 0 THEN 'ENTRADA'
                                            WHEN 1 THEN 'SAÍDA'
                                            ELSE NULL
                                            END AS OPERACAO,
                                            CASE MP.TABELA_ORIGEM
                                            WHEN 'PNI_ENTRADA_PRODUTO_ITEM' THEN
                                                (SELECT
                                                'Entrada no Estoque Nº ' || EPI.ID_ENTRADA_PRODUTO
                                                || ' | Data: ' || CAST(EP.DATA AS DATE) || ' | Forn.: ' ||  CASE
                                                                                                            WHEN EP.ID_FORNECEDOR IS NOT NULL THEN CF.CSI_NOMFOR
                                                                                                            ELSE UNI_ENV.CSI_NOMUNI
                                                                                                            END  || ' | Usuário: ' || EP.USUARIO
                                                FROM PNI_ENTRADA_PRODUTO_ITEM EPI
                                                LEFT JOIN PNI_ENTRADA_PRODUTO EP ON EP.ID = EPI.ID_ENTRADA_PRODUTO
                                                LEFT JOIN TSI_CADFOR CF ON CF.CSI_CODFOR = EP.ID_FORNECEDOR
                                                LEFT JOIN PNI_ENVIO PE ON PE.ID = EP.ID_ENVIO
                                                LEFT JOIN TSI_UNIDADE UNI_ENV ON UNI_ENV.CSI_CODUNI = PE.ID_UNIDADE_ORIGEM
                                                WHERE EPI.ID = MP.ID_ORIGEM)
                                            WHEN 'PNI_ACERTO_ESTOQUE' THEN
                                                (SELECT CASE AE.TIPO_LANCAMENTO
                                                WHEN 2 THEN 'Perca por ' ||
                                                    CASE AE.TIPO_PERCA
                                                    WHEN 1 THEN 'Quebra'
                                                    WHEN 2 THEN 'Falta de Energia'
                                                    WHEN 3 THEN 'Falha Equipamento'
                                                    WHEN 4 THEN 'Vencimento'
                                                    WHEN 5 THEN 'Transporte'
                                                    WHEN 6 THEN 'Outros Motivos'
                                                    END 
                                                WHEN 3 THEN 'Doação para ' || F.CSI_NOMFOR || ' | Usuário: ' || MP.USUARIO
                                                WHEN 4 THEN 'Envio para ' || U.CSI_NOMUNI || ' | Usuário: ' || MP.USUARIO
                                                ELSE ''
                                                END || ' | N° ' || AE.ID || ' | Data: ' || CAST(AE.DATA AS DATE) || ' | Usuário: ' || MP.USUARIO
                                            FROM PNI_ACERTO_ESTOQUE AE
                                            LEFT JOIN TSI_UNIDADE U ON U.CSI_CODUNI = AE.ID_UNIDADE_ENVIO
                                            LEFT JOIN TSI_CADFOR F ON F.CSI_CODFOR = AE.ID_FORNECEDOR
                                            WHERE AE.ID = MP.ID_ORIGEM)
                                            WHEN 'PNI_VACINADOS' THEN
                                                (SELECT 'Dose Aplicada | Pront.: ' || CP.CSI_CODPAC || ' | Nome: ' || CP.CSI_NOMPAC || ' | Usuário: ' || MP.USUARIO
                                                FROM PNI_VACINADOS V
                                                JOIN TSI_CADPAC CP ON CP.CSI_CODPAC = V.ID_PACIENTE
                                                WHERE V.ID = MP.ID_ORIGEM)
                                            WHEN 'PNI_ENVIO_ITEM' THEN
                                               (SELECT 'Envio para ' || U.CSI_NOMUNI || ' | Usuário: ' || MP.USUARIO FROM PNI_ENVIO_ITEM EI
                                               JOIN PNI_ENVIO E ON EI.ID_ENVIO = E.ID
                                                JOIN TSI_UNIDADE U ON E.ID_UNIDADE_DESTINO = U.CSI_CODUNI
                                                WHERE EI.ID = MP.ID_ORIGEM)
                                            ELSE MP.TABELA_ORIGEM
                                            END AS HISTORICO,
                                             CASE MP.OPERACAO
                                                  WHEN 1 THEN MP.QTDE * -1
                                                  ELSE MP.QTDE
                                                  END AS QTDE,
                                            COALESCE(MP.ESTOQUE_ANTERIOR, 0) ESTOQUE_ANTERIOR,
                                            CASE MP.OPERACAO
                                              WHEN 1 THEN COALESCE(MP.ESTOQUE_ANTERIOR, 0) - MP.QTDE
                                              ELSE COALESCE(MP.ESTOQUE_ANTERIOR, 0) + MP.QTDE
                                            END AS ESTOQUE_ATUAL
                                        FROM PNI_MOVIMENTO_PRODUTO MP
                                        WHERE MP.ID_PRODUTO = @id_produto -- OBRIGATÓRIO
                                        AND (CAST(MP.DATA AS DATE) BETWEEN @data_inicial AND @data_final) -- OBRIGATÓRIO
                                        @filtro
                                        ORDER BY MP.ID";
        string IEstoqueCommand.GetAuditoria { get => sqlGetAuditoria; }

        public string GetCountAuditoria = $@"SELECT COUNT(*) FROM (
                                               SELECT
                                                    MP.ID,
                                                    MP.DATA,
                                                    CASE MP.OPERACAO
                                                    WHEN 0 THEN 'ENTRADA'
                                                    WHEN 1 THEN 'SAÍDA'
                                                    ELSE NULL
                                                    END AS OPERACAO,
                                                    CASE MP.TABELA_ORIGEM
                                                    WHEN 'PNI_ENTRADA_PRODUTO_ITEM' THEN
                                                        (SELECT
                                                        'Entrada no Estoque Nº ' || EPI.ID_ENTRADA_PRODUTO
                                                        || ' | Data: ' || CAST(EP.DATA AS DATE) || ' | Forn.: ' || CF.CSI_NOMFOR || ' | Usuário: ' || MP.USUARIO
                                                        FROM PNI_ENTRADA_PRODUTO_ITEM EPI
                                                        LEFT JOIN PNI_ENTRADA_PRODUTO EP ON EP.ID = EPI.ID_ENTRADA_PRODUTO
                                                        LEFT JOIN TSI_CADFOR CF ON CF.CSI_CODFOR = EP.ID_FORNECEDOR
                                                        WHERE EPI.ID = MP.ID_ORIGEM)
                                                    WHEN 'PNI_ACERTO_ESTOQUE' THEN
                                                        (SELECT CASE AE.TIPO_LANCAMENTO
                                                        WHEN 2 THEN 'Perca por ' ||
                                                            CASE AE.TIPO_PERCA
                                                            WHEN 1 THEN 'Quebra'
                                                            WHEN 2 THEN 'Falta de Energia'
                                                            WHEN 3 THEN 'Falha Equipamento'
                                                            WHEN 4 THEN 'Vencimento'
                                                            WHEN 5 THEN 'Transporte'
                                                            WHEN 6 THEN 'Outros Motivos'
                                                            END || ' | Usuário: ' || MP.USUARIO
                                                        WHEN 3 THEN 'Doação para ' || F.CSI_NOMFOR || ' | Usuário: ' || MP.USUARIO
                                                        WHEN 4 THEN 'Envio para ' || U.CSI_NOMUNI || ' | Usuário: ' || MP.USUARIO
                                                        ELSE ''
                                                        END || ' | N° ' || AE.ID || ' | Data: ' || CAST(AE.DATA AS DATE)
                                                    FROM PNI_ACERTO_ESTOQUE AE
                                                    LEFT JOIN TSI_UNIDADE U ON U.CSI_CODUNI = AE.ID_UNIDADE_ENVIO
                                                    LEFT JOIN TSI_CADFOR F ON F.CSI_CODFOR = AE.ID_FORNECEDOR
                                                    WHERE AE.ID = MP.ID_ORIGEM)
                                                    WHEN 'PNI_VACINADOS' THEN
                                                        (SELECT 'Dose Aplicada | Pront.: ' || CP.CSI_CODPAC || ' | Nome: ' || CP.CSI_NOMPAC || ' | Usuário: ' || MP.USUARIO
                                                        FROM PNI_VACINADOS V
                                                        JOIN TSI_CADPAC CP ON CP.CSI_CODPAC = V.ID_PACIENTE
                                                        WHERE V.ID = MP.ID_ORIGEM)
                                                    WHEN 'PNI_ENVIO_ITEM' THEN
                                                        (SELECT 'Envio para ' || U.CSI_NOMUNI || ' | Usuário: ' || MP.USUARIO FROM PNI_ENVIO_ITEM EI
                                                        JOIN PNI_ENVIO E ON EI.ID_ENVIO = E.ID
                                                        JOIN TSI_UNIDADE U ON E.ID_UNIDADE_DESTINO = U.CSI_CODUNI
                                                        WHERE EI.ID = MP.ID_ORIGEM)
                                                    ELSE MP.TABELA_ORIGEM
                                                    END AS HISTORICO,
                                                     CASE MP.OPERACAO
                                                          WHEN 1 THEN MP.QTDE * -1
                                                          ELSE MP.QTDE
                                                          END AS QTDE,
                                                    COALESCE(MP.ESTOQUE_ANTERIOR, 0) ESTOQUE_ANTERIOR,
                                                    CASE MP.OPERACAO
                                                      WHEN 1 THEN COALESCE(MP.ESTOQUE_ANTERIOR, 0) - MP.QTDE
                                                      ELSE COALESCE(MP.ESTOQUE_ANTERIOR, 0) + MP.QTDE
                                                    END AS ESTOQUE_ATUAL
                                                FROM PNI_MOVIMENTO_PRODUTO MP
                                                WHERE MP.ID_PRODUTO = @id_produto -- OBRIGATÓRIO
                                                AND (CAST(MP.DATA AS DATE) BETWEEN @data_inicial AND @data_final) -- OBRIGATÓRIO
                                                @filtro
                                                ORDER BY MP.ID)";
        string IEstoqueCommand.GetCountAuditoria { get => GetCountAuditoria; }
    }
}
