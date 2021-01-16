using RgCidadao.Domain.Commands.Cadastro;
using RgCidadao.Domain.Commands.Imunizacao;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Queries.Imunizacao
{
    public class ReportCommandText : IReportCommand
    {
        public string sqlGetImunizacao = $@"@filtroini
                                            SELECT V.ID,
                                                   CAST(V.DATA_APLICACAO AS DATE) DATA,
                                                   COALESCE(V.ID_PACIENTE,0) ID_PACIENTE,
                                                   COALESCE(C.CSI_NOMPAC,'CIDADÃO NÃO INFORMADO') PACIENTE,
                                                   CAST(C.CSI_DTNASC AS DATE) DATA_NASCIMENTO,
                                                   COALESCE(M.CSI_NOMMED,'PROFISSIONAL NÃO INFORMADO') PROFISSIONAL,
                                                   COALESCE(V.ID_PROFISIONAL,0) ID_PROFISSIONAL,
                                                   V.ID_PRODUTO,
                                                   P.ABREVIATURA PRODUTO,
                                                   P.SIGLA,
                                                   COALESCE(U.CSI_NOMUNI, 'UNIDADE NÃO INFORMADA') UNIDADE,
                                                   COALESCE(V.ID_UNIDADE,0) ID_UNIDADE,
                                                   V.ID_DOSE, D.DESCRICAO DOSE, V.LOTE
                                            FROM PNI_VACINADOS V
                                            LEFT JOIN TSI_CADPAC C ON V.ID_PACIENTE = C.CSI_CODPAC
                                            LEFT JOIN TSI_MEDICOS M ON V.ID_PROFISIONAL = M.CSI_CODMED
                                            JOIN PNI_PRODUTO P ON V.ID_PRODUTO = P.ID
                                            LEFT JOIN TSI_UNIDADE U ON V.ID_UNIDADE = U.CSI_CODUNI
                                            JOIN PNI_DOSE D ON V.ID_DOSE = D.ID
                                            WHERE 1 = 1 AND CAST(V.DATA_APLICACAO AS DATE) BETWEEN @datainicio AND @datafim
                                                  AND COALESCE(V.FLG_EXCLUIDO,0) = 0
                                                    @filtro";
        string IReportCommand.GetReportImunizacao { get => sqlGetImunizacao; }

        //@unidade,@produto,@datafinal
        public string sqlGetReportMovimento = $@"SELECT TAB.PRODUTO,TAB.SIGLA, TAB.FABRICANTE, CAST(0 AS INTEGER) ENTRADA, CAST(0 AS INTEGER) SAIDA,
                                                 CAST(0 AS INTEGER) FRASCOS_TRANSFERIDOS, CAST(0 AS INTEGER) QUEBRA_FRASCOS, CAST(0 AS INTEGER) FALTA_ENERGIA,
                                                 CAST(0 AS INTEGER) FALTA_EQUIPAMENTO, CAST(0 AS INTEGER) VALIDADE_VENCIDA,
                                                 CAST(0 AS INTEGER) PROCEDIMENTO_INADEQUADO, CAST(0 AS INTEGER) FALHA_TRANSPORTE,
                                                 CAST(0 AS INTEGER) OUTROS_MOTIVOS,
                                                 CAST(SUM(TAB.ENTRADA) - SUM(SAIDA) - SUM(FRASCOS_TRANSFERIDOS) - SUM(QUEBRA_FRASCOS) - SUM(FALTA_ENERGIA) - SUM(FALTA_EQUIPAMENTO) - SUM(VALIDADE_VENCIDA) - SUM(PROCEDIMENTO_INADEQUADO) - SUM(FALHA_TRANSPORTE) - SUM(OUTROS_MOTIVOS) AS INTEGER) SALDO_INICIAL,
                                                 CAST(SUM(SALDO_FINAL) AS INTEGER) SALDO_FINAL, ID_PRODUTO, ID_PRODUTOR, LOTE
                                                 FROM (SELECT P.ABREVIATURA PRODUTO, P.SIGLA, F.NOME FABRICANTE, A.DESCRICAO, SUM(EI.QTDE_FRASCOS) ENTRADA, 0 AS SAIDA,
                                                      0 FRASCOS_TRANSFERIDOS, 0 QUEBRA_FRASCOS, 0 FALTA_ENERGIA, 0 FALTA_EQUIPAMENTO, 0 VALIDADE_VENCIDA,
                                                      0 PROCEDIMENTO_INADEQUADO, 0 FALHA_TRANSPORTE, 0 OUTROS_MOTIVOS, 0 SALDO_INICIAL, 0 SALDO_FINAL,
                                                      E.ID_UNIDADE, PLP.ID_PRODUTO, EI.ID_APRESENTACAO, PLP.ID_PRODUTOR, PLP.LOTE
                                                 FROM PNI_ENTRADA_PRODUTO E
                                                 JOIN PNI_ENTRADA_PRODUTO_ITEM EI ON E.ID = EI.ID_ENTRADA_PRODUTO
                                                 JOIN PNI_LOTE_PRODUTO PLP ON PLP.ID = EI.ID_LOTE
                                                 JOIN PNI_PRODUTO P ON PLP.ID_PRODUTO = P.ID
                                                 JOIN PNI_PRODUTOR F ON PLP.ID_PRODUTOR = F.ID
                                                 JOIN PNI_APRESENTACAO A ON EI.ID_APRESENTACAO = A.ID
                                                 WHERE 1 = 1 AND
                                                    @filtro1
                                                     CAST(E.DATA AS DATE) < @datafinal
                                                 GROUP BY P.ABREVIATURA,P.SIGLA, F.NOME, A.DESCRICAO,EI.QTDE_FRASCOS,
                                                          E.ID_UNIDADE, PLP.ID_PRODUTO, EI.ID_APRESENTACAO, PLP.ID_PRODUTOR, PLP.LOTE
                                                 UNION
                                                 SELECT P.ABREVIATURA PRODUTO,P.SIGLA, F.NOME FABRICANTE, D.DESCRICAO, 0 ENTRADA, COUNT(V.ID) SAIDA,
                                                      0 FRASCOS_TRANSFERIDOS, 0 QUEBRA_FRASCOS, 0 FALTA_ENERGIA, 0 FALTA_EQUIPAMENTO, 0 VALIDADE_VENCIDA,
                                                      0 PROCEDIMENTO_INADEQUADO, 0 FALHA_TRANSPORTE, 0 OUTROS_MOTIVOS, 0 SALDO_INICIAL, 0 SALDO_FINAL,
                                                      V.ID_UNIDADE, V.ID_PRODUTO, V.ID_DOSE APLICACAO, V.ID_PRODUTOR, V.LOTE
                                                 FROM PNI_VACINADOS V
                                                 JOIN PNI_PRODUTO P ON V.ID_PRODUTO = P.ID
                                                 JOIN PNI_PRODUTOR F ON V.ID_PRODUTOR = F.ID
                                                 JOIN PNI_DOSE D ON V.ID_DOSE = D.ID
                                                 WHERE 1 = 1 AND
                                                     @filtro2
                                                     CAST(V.DATA_APLICACAO AS DATE) < @datafinal
                                                 GROUP BY P.ABREVIATURA,P.SIGLA, F.NOME, D.DESCRICAO, V.ID_UNIDADE, V.ID_PRODUTO, V.ID_DOSE, V.ID_PRODUTOR, V.LOTE
                                                 UNION
                                                 SELECT P.ABREVIATURA PRODUTO,P.SIGLA, F.NOME FABRICANTE, A.DESCRICAO, 0 ENTRADA, 0 AS SAIDA, SUM(
                                                      CASE
                                                        WHEN UPPER(MP.TIPO_LANCAMENTO) = 'FTO' THEN MP.QTDE
                                                      END) FRASCOS_TRANSFERIDOS,
                                                      SUM(
                                                      CASE
                                                        WHEN UPPER(MP.TIPO_LANCAMENTO) = 'QF' THEN MP.QTDE
                                                      END) QUEBRA_FRASCOS,
                                                      SUM(
                                                      CASE
                                                        WHEN UPPER(MP.TIPO_LANCAMENTO) = 'FEA' THEN MP.QTDE
                                                      END) FALTA_ENERGIA,
                                                      SUM(
                                                      CASE
                                                        WHEN UPPER(MP.TIPO_LANCAMENTO) = 'FEO' THEN MP.QTDE
                                                      END) FALTA_EQUIPAMENTO,
                                                      SUM(
                                                      CASE
                                                        WHEN UPPER(MP.TIPO_LANCAMENTO) = 'VV' THEN MP.QTDE
                                                      END) VALIDADE_VENCIDA,
                                                      SUM(
                                                      CASE
                                                        WHEN UPPER(MP.TIPO_LANCAMENTO) = 'PI' THEN MP.QTDE
                                                      END) PROCEDIMENTO_INADEQUADO,
                                                      SUM(
                                                      CASE
                                                        WHEN UPPER(MP.TIPO_LANCAMENTO) = 'FTE' THEN MP.QTDE
                                                      END) FALHA_TRANSPORTE,
                                                      SUM(
                                                      CASE
                                                        WHEN UPPER(MP.TIPO_LANCAMENTO) = 'OM' THEN MP.QTDE
                                                      END) OUTROS_MOTIVOS,
                                                      0 SALDO_INICIAL, 0 SALDO_FINAL, MP.ID_UNIDADE, MP.ID_PRODUTO, MP.ID_APRESENTACAO, MP.ID_PRODUTOR, MP.LOTE
                                                 FROM PNI_ACERTO_ESTOQUE MP
                                                 JOIN PNI_PRODUTO P ON MP.ID_PRODUTO = P.ID
                                                 JOIN PNI_PRODUTOR F ON MP.ID_PRODUTOR = F.ID
                                                 JOIN PNI_APRESENTACAO A ON MP.ID_APRESENTACAO = A.ID
                                                 WHERE 1 = 1 AND
                                                      @filtro3
                                                     CAST(MP.DATA AS DATE) < @datafinal
                                                 GROUP BY P.ABREVIATURA,P.SIGLA, F.NOME, A.DESCRICAO, MP.ID_UNIDADE, MP.ID_PRODUTO, MP.ID_APRESENTACAO, MP.ID_PRODUTOR, MP.LOTE) TAB
                                                 GROUP BY TAB.PRODUTO,TAB.SIGLA, TAB.FABRICANTE, ID_PRODUTO, ID_PRODUTOR, LOTE
                                                 ORDER BY TAB.PRODUTO, TAB.FABRICANTE";
        string IReportCommand.GetReportMovimento { get => sqlGetReportMovimento; }

        //@unidade,@produto,@datainicial, @datafinal
        public string sqlGetReportDetalhamento = $@"SELECT TAB.PRODUTO, TAB.FABRICANTE, CAST(SUM(TAB.ENTRADA) AS INTEGER) ENTRADA, CAST(SUM(SAIDA) AS INTEGER) SAIDA,
                                                    CAST(SUM(FRASCOS_TRANSFERIDOS) AS INTEGER) FRASCOS_TRANSFERIDOS,
                                                    CAST(SUM(QUEBRA_FRASCOS) AS INTEGER) QUEBRA_FRASCOS, CAST(SUM(FALTA_ENERGIA) AS INTEGER) FALTA_ENERGIA,
                                                    CAST(SUM(FALTA_EQUIPAMENTO) AS INTEGER) FALTA_EQUIPAMENTO,
                                                    CAST(SUM(VALIDADE_VENCIDA) AS INTEGER) VALIDADE_VENCIDA,
                                                    CAST(SUM(PROCEDIMENTO_INADEQUADO) AS INTEGER) PROCEDIMENTO_INADEQUADO,
                                                    CAST(SUM(FALHA_TRANSPORTE) AS INTEGER) FALHA_TRANSPORTE, CAST(SUM(OUTROS_MOTIVOS) AS INTEGER) OUTROS_MOTIVOS,
                                                    CAST(SUM(SALDO_INICIAL) AS INTEGER) SALDO_INICIAL, CAST(SUM(SALDO_FINAL) AS INTEGER) SALDO_FINAL, ID_PRODUTO,
                                                    ID_PRODUTOR, LOTE
                                                    FROM (SELECT P.ABREVIATURA PRODUTO, F.NOME FABRICANTE, A.DESCRICAO, SUM(EI.QTDE_FRASCOS) ENTRADA, 0 AS SAIDA,
                                                            0 FRASCOS_TRANSFERIDOS, 0 QUEBRA_FRASCOS, 0 FALTA_ENERGIA, 0 FALTA_EQUIPAMENTO, 0 VALIDADE_VENCIDA,
                                                            0 PROCEDIMENTO_INADEQUADO, 0 FALHA_TRANSPORTE, 0 OUTROS_MOTIVOS, 0 SALDO_INICIAL, 0 SALDO_FINAL,
                                                            E.ID_UNIDADE, PLP.ID_PRODUTO, EI.ID_APRESENTACAO, PLP.ID_PRODUTOR, PLP.LOTE
                                                    FROM PNI_ENTRADA_PRODUTO E
                                                    JOIN PNI_ENTRADA_PRODUTO_ITEM EI ON E.ID = EI.ID_ENTRADA_PRODUTO
                                                    JOIN PNI_LOTE_PRODUTO PLP ON PLP.ID = EI.ID_LOTE
                                                    JOIN PNI_PRODUTO P ON PLP.ID_PRODUTO = P.ID
                                                    JOIN PNI_PRODUTOR F ON PLP.ID_PRODUTOR = F.ID
                                                    JOIN PNI_APRESENTACAO A ON EI.ID_APRESENTACAO = A.ID
                                                    WHERE 1 = 1 AND
                                                        @filtro1 
                                                        CAST(E.DATA AS DATE) BETWEEN @datainicial AND @datafinal
                                                    GROUP BY P.ABREVIATURA,P.SIGLA, F.NOME, A.DESCRICAO,EI.QTDE_FRASCOS,
                                                             E.ID_UNIDADE, PLP.ID_PRODUTO, EI.ID_APRESENTACAO, PLP.ID_PRODUTOR, PLP.LOTE
                                                    UNION
                                                    SELECT P.ABREVIATURA PRODUTO, F.NOME FABRICANTE, D.DESCRICAO, 0 ENTRADA, COUNT(V.ID) SAIDA,
                                                            0 FRASCOS_TRANSFERIDOS, 0 QUEBRA_FRASCOS, 0 FALTA_ENERGIA, 0 FALTA_EQUIPAMENTO, 0 VALIDADE_VENCIDA,
                                                            0 PROCEDIMENTO_INADEQUADO, 0 FALHA_TRANSPORTE, 0 OUTROS_MOTIVOS, 0 SALDO_INICIAL, 0 SALDO_FINAL,
                                                            V.ID_UNIDADE, V.ID_PRODUTO, V.ID_DOSE APLICACAO, V.ID_PRODUTOR, V.LOTE
                                                    FROM PNI_VACINADOS V
                                                    JOIN PNI_PRODUTO P ON V.ID_PRODUTO = P.ID
                                                    JOIN PNI_PRODUTOR F ON V.ID_PRODUTOR = F.ID
                                                    JOIN PNI_DOSE D ON V.ID_DOSE = D.ID
                                                    WHERE 1 = 1 AND
                                                          @filtro2 
                                                        CAST(V.DATA_APLICACAO AS DATE) BETWEEN @datainicial AND @datafinal
                                                    GROUP BY P.ABREVIATURA,P.SIGLA, F.NOME, D.DESCRICAO, V.ID_UNIDADE, V.ID_PRODUTO, V.ID_DOSE, V.ID_PRODUTOR, V.LOTE
                                                    UNION
                                                    SELECT P.ABREVIATURA PRODUTO, F.NOME FABRICANTE, A.DESCRICAO, 0 ENTRADA, 0 AS SAIDA, SUM(
                                                            CASE
                                                            WHEN UPPER(MP.TIPO_LANCAMENTO) = 'FTO' THEN MP.QTDE
                                                            END) FRASCOS_TRANSFERIDOS,
                                                            SUM(
                                                            CASE
                                                            WHEN UPPER(MP.TIPO_LANCAMENTO) = 'QF' THEN MP.QTDE
                                                            END) QUEBRA_FRASCOS,
                                                            SUM(
                                                            CASE
                                                            WHEN UPPER(MP.TIPO_LANCAMENTO) = 'FEA' THEN MP.QTDE
                                                            END) FALTA_ENERGIA,
                                                            SUM(
                                                            CASE
                                                            WHEN UPPER(MP.TIPO_LANCAMENTO) = 'FEO' THEN MP.QTDE
                                                            END) FALTA_EQUIPAMENTO,
                                                            SUM(
                                                            CASE
                                                            WHEN UPPER(MP.TIPO_LANCAMENTO) = 'VV' THEN MP.QTDE
                                                            END) VALIDADE_VENCIDA,
                                                            SUM(
                                                            CASE
                                                            WHEN UPPER(MP.TIPO_LANCAMENTO) = 'PI' THEN MP.QTDE
                                                            END) PROCEDIMENTO_INADEQUADO,
                                                            SUM(
                                                            CASE
                                                            WHEN UPPER(MP.TIPO_LANCAMENTO) = 'FTE' THEN MP.QTDE
                                                            END) FALHA_TRANSPORTE,
                                                            SUM(
                                                            CASE
                                                            WHEN UPPER(MP.TIPO_LANCAMENTO) = 'OM' THEN MP.QTDE
                                                            END) OUTROS_MOTIVOS,
                                                            0 SALDO_INICIAL, 0 SALDO_FINAL, MP.ID_UNIDADE, MP.ID_PRODUTO, MP.ID_APRESENTACAO, MP.ID_PRODUTOR, MP.LOTE
                                                    FROM PNI_ACERTO_ESTOQUE MP
                                                    JOIN PNI_PRODUTO P ON MP.ID_PRODUTO = P.ID
                                                    JOIN PNI_PRODUTOR F ON MP.ID_PRODUTOR = F.ID
                                                    JOIN PNI_APRESENTACAO A ON MP.ID_APRESENTACAO = A.ID
                                                    WHERE 1 = 1 AND
                                                          @filtro3 
                                                        CAST(MP.DATA AS DATE) BETWEEN @datainicial AND @datafinal
                                                    GROUP BY P.ABREVIATURA,P.SIGLA, F.NOME, A.DESCRICAO, MP.ID_UNIDADE, MP.ID_PRODUTO, MP.ID_APRESENTACAO, MP.ID_PRODUTOR, MP.LOTE) TAB
                                                    GROUP BY TAB.PRODUTO, TAB.FABRICANTE, ID_PRODUTO, ID_PRODUTOR, LOTE
                                                    ORDER BY TAB.PRODUTO, TAB.FABRICANTE  ";
        string IReportCommand.GetReportDetalhamento { get => sqlGetReportDetalhamento; }


        //Impressão do cartão de vacina 
        public string sqlGetCartaoVacinaByCidadao = $@"SELECT DISTINCT V.DATA_APLICACAO DATA_HORA, PE.DESCRICAO ESTRATEGIA, P.ABREVIATURA PRODUTO, P.SIGLA, D.DESCRICAO DOSE,
                                                                       COALESCE(PROD.ABREVIATURA, '') LABORATORIO, COALESCE(V.LOTE, '') LOTE,
                                                                       COALESCE(E.DESCRICAO, '') DSC_EQUIPE
                                                       FROM PNI_VACINADOS V
                                                       JOIN PNI_ESTRATEGIA PE ON PE.ID = V.ID_ESTRATEGIA
                                                       JOIN PNI_PRODUTO P ON P.ID = V.ID_PRODUTO
                                                       JOIN PNI_DOSE D ON D.ID = V.ID_DOSE
                                                       LEFT JOIN PNI_PRODUTOR PROD ON PROD.ID = V.ID_PRODUTOR
                                                       LEFT JOIN TSI_MEDICOS M ON M.CSI_CODMED = V.ID_PROFISIONAL
                                                       LEFT JOIN ESUS_LOTACAO_PROFISSIONAIS LP ON (LP.ID_PROFISSIONAL = M.CSI_CODMED AND LP.ID_EQUIPE IS NOT NULL)
                                                       LEFT JOIN ESUS_EQUIPES E ON (LP.ID_EQUIPE = E.ID)
                                                       WHERE V.ID_PACIENTE = @id ";

        string IReportCommand.GetReportCartaoVacina { get => sqlGetCartaoVacinaByCidadao; }

        public string sqlGetReportBoletimMovimento = $@"SELECT DISTINCT TAB.DATA,TAB.SALDO_INICIAL, TAB.LOTE, TAB.NOME_FABRICANTE, TAB.PRODUTO, TAB.SIGLA,
                                                                 CAST(SUM(COALESCE(TAB.ENTRADA,0)) + SUM(COALESCE(TAB.ENTRADA_EDICAO,0)) AS INTEGER) ENTRADA,
                                                                SUM(COALESCE(TAB.ENTRADA_TRANSFERENCIA,0)) ENTRADA_TRANSFERENCIA, SUM(COALESCE(TAB.VACINADO,0)) VACINADO,
                                                                SUM(COALESCE(TAB.QUEBRA,0)) QUEBRA, SUM(COALESCE(TAB.FALTA_ENERGIA,0)) FALTA_ENERGIA,
                                                                SUM(COALESCE(TAB.FALHA_EQUIPAMENTO,0)) FALHA_EQUIPAMENTO,
                                                                SUM(COALESCE(TAB.VENCIMENTO,0)) VENCIMENTO, SUM(COALESCE(TAB.TRANSPORTE,0)) TRANSPORTE,
                                                                SUM(COALESCE(TAB.OUTROS_MOTIVOS,0)) OUTROS_MOTIVOS, SUM(COALESCE(TAB.DOACAO,0)) DOACAO,
                                                                SUM(COALESCE(TAB.TRANFERENCIA_SAIDA,0)) TRANFERENCIA_SAIDA, SUM(COALESCE(TAB.SAIDA,0)) SAIDA,

                                                                CAST(((TAB.SALDO_INICIAL) + SUM(COALESCE(TAB.ENTRADA,0)) + SUM(COALESCE(TAB.ENTRADA_TRANSFERENCIA,0)) + SUM(COALESCE(TAB.ENTRADA_EDICAO,0))) -
                                                                SUM(COALESCE(TAB.VACINADO,0)) - SUM(COALESCE(TAB.QUEBRA,0)) - SUM(COALESCE(TAB.FALTA_ENERGIA,0)) -
                                                                SUM(COALESCE(TAB.FALHA_EQUIPAMENTO,0)) - SUM(COALESCE(TAB.VENCIMENTO,0)) - SUM(COALESCE(TAB.TRANSPORTE,0)) -
                                                                SUM(COALESCE(TAB.OUTROS_MOTIVOS,0)) - SUM(COALESCE(TAB.DOACAO,0)) - SUM(COALESCE(TAB.TRANFERENCIA_SAIDA,0)) AS INTEGER) SALDO_FINAL
                                                        FROM (SELECT MP.DATA ,MP.LOTE, F.NOME NOME_FABRICANTE, PP.ABREVIATURA PRODUTO, PP.SIGLA,
                                                             (SELECT FIRST(1) SALD.ESTOQUE_ANTERIOR
                                                              FROM PNI_MOVIMENTO_PRODUTO SALD
                                                              WHERE SALD.ID = MP.ID AND
                                                                    CAST(SALD.DATA AS DATE) <= @datafinal
                                                              ORDER BY SALD.DATA DESC) SALDO_INICIAL,
                                                             (SELECT SUM(ENTR.QTDE)
                                                              FROM PNI_MOVIMENTO_PRODUTO ENTR
                                                              JOIN PNI_ENTRADA_PRODUTO_ITEM EPI ON EPI.ID = ENTR.ID_ORIGEM
                                                              JOIN PNI_ENTRADA_PRODUTO EP ON EP.ID = EPI.ID_ENTRADA_PRODUTO
                                                              WHERE ENTR.TABELA_ORIGEM = 'PNI_ENTRADA_PRODUTO_ITEM' AND
                                                                    ENTR.ID = MP.ID AND
                                                                    ENTR.TIPO_MOVIMENTO = 0 AND
                                                                    EP.ID_ENVIO IS NULL) ENTRADA,
                                                                    (SELECT SUM(VAC.QTDE)
                                                              FROM PNI_MOVIMENTO_PRODUTO VAC
                                                              JOIN PNI_VACINADOS ENV ON ENV.ID = VAC.ID_ORIGEM
                                                              WHERE VAC.TABELA_ORIGEM = 'PNI_VACINADOS' AND
                                                                    VAC.ID = MP.ID AND
                                                                    VAC.OPERACAO = 0) ENTRADA_EDICAO,
                                                             (SELECT SUM(TRANF_ENTR.QTDE)
                                                              FROM PNI_MOVIMENTO_PRODUTO TRANF_ENTR
                                                              JOIN PNI_ENTRADA_PRODUTO_ITEM EPI ON EPI.ID = TRANF_ENTR.ID_ORIGEM
                                                              JOIN PNI_ENTRADA_PRODUTO EP ON EP.ID = EPI.ID_ENTRADA_PRODUTO
                                                              WHERE TRANF_ENTR.TABELA_ORIGEM = 'PNI_ENTRADA_PRODUTO_ITEM' AND
                                                                    TRANF_ENTR.ID = MP.ID AND
                                                                    TRANF_ENTR.TIPO_MOVIMENTO = 0 AND
                                                                    EP.ID_ENVIO IS NOT NULL) ENTRADA_TRANSFERENCIA,
                                                             (SELECT SUM(MPVAC.QTDE)
                                                              FROM PNI_MOVIMENTO_PRODUTO MPVAC
                                                              WHERE MPVAC.TIPO_MOVIMENTO = 1 AND
                                                                    MPVAC.ID = MP.ID AND
                                                                    MPVAC.OPERACAO = 1) VACINADO,
                                                             (SELECT SUM(MPPERCA1.QTDE)
                                                              FROM PNI_MOVIMENTO_PRODUTO MPPERCA1
                                                              JOIN PNI_ACERTO_ESTOQUE PAE ON PAE.ID = MPPERCA1.ID_ORIGEM
                                                              WHERE MPPERCA1.TABELA_ORIGEM = 'PNI_ACERTO_ESTOQUE' AND
                                                                    PAE.TIPO_PERCA = 1 AND
                                                                    MPPERCA1.ID = MP.ID) QUEBRA,
                                                             (SELECT SUM(MPPERCA2.QTDE)
                                                              FROM PNI_MOVIMENTO_PRODUTO MPPERCA2
                                                              JOIN PNI_ACERTO_ESTOQUE PAE ON PAE.ID = MPPERCA2.ID_ORIGEM
                                                              WHERE MPPERCA2.TABELA_ORIGEM = 'PNI_ACERTO_ESTOQUE' AND
                                                                    PAE.TIPO_PERCA = 2 AND
                                                                    MPPERCA2.ID = MP.ID) FALTA_ENERGIA,
                                                             (SELECT SUM(MPPERCA3.QTDE)
                                                              FROM PNI_MOVIMENTO_PRODUTO MPPERCA3
                                                              JOIN PNI_ACERTO_ESTOQUE PAE ON PAE.ID = MPPERCA3.ID_ORIGEM
                                                              WHERE MPPERCA3.TABELA_ORIGEM = 'PNI_ACERTO_ESTOQUE' AND
                                                                    PAE.TIPO_PERCA = 3 AND
                                                                    MPPERCA3.ID = MP.ID) FALHA_EQUIPAMENTO,
                                                             (SELECT SUM(MPPERCA4.QTDE)
                                                              FROM PNI_MOVIMENTO_PRODUTO MPPERCA4
                                                              JOIN PNI_ACERTO_ESTOQUE PAE ON PAE.ID = MPPERCA4.ID_ORIGEM
                                                              WHERE MPPERCA4.TABELA_ORIGEM = 'PNI_ACERTO_ESTOQUE' AND
                                                                    PAE.TIPO_PERCA = 4 AND
                                                                    MPPERCA4.ID = MP.ID) VENCIMENTO,
                                                             (SELECT SUM(MPPERCA5.QTDE)
                                                              FROM PNI_MOVIMENTO_PRODUTO MPPERCA5
                                                              JOIN PNI_ACERTO_ESTOQUE PAE ON PAE.ID = MPPERCA5.ID_ORIGEM
                                                              WHERE MPPERCA5.TABELA_ORIGEM = 'PNI_ACERTO_ESTOQUE' AND
                                                                    PAE.TIPO_PERCA = 5 AND
                                                                    MPPERCA5.ID = MP.ID) TRANSPORTE,
                                                             (SELECT SUM(MPPERCA6.QTDE)
                                                              FROM PNI_MOVIMENTO_PRODUTO MPPERCA6
                                                              JOIN PNI_ACERTO_ESTOQUE PAE ON PAE.ID = MPPERCA6.ID_ORIGEM
                                                              WHERE MPPERCA6.TABELA_ORIGEM = 'PNI_ACERTO_ESTOQUE' AND
                                                                    PAE.TIPO_PERCA = 6 AND
                                                                    MPPERCA6.ID = MP.ID) OUTROS_MOTIVOS,
                                                             (SELECT SUM(MPVAC.QTDE)
                                                              FROM PNI_MOVIMENTO_PRODUTO MPVAC
                                                              WHERE MPVAC.TIPO_MOVIMENTO = 3 AND
                                                                    MPVAC.ID = MP.ID) DOACAO,
                                                             (SELECT SUM(MPPERCA1.QTDE)
                                                              FROM PNI_MOVIMENTO_PRODUTO MPPERCA1
                                                              JOIN PNI_ENVIO_ITEM ENV ON ENV.ID = MPPERCA1.ID_ORIGEM
                                                              WHERE MPPERCA1.TABELA_ORIGEM = 'PNI_ENVIO_ITEM' AND
                                                                    MPPERCA1.ID = MP.ID) TRANFERENCIA_SAIDA,
                                                             (SELECT SUM(VAC.QTDE)
                                                              FROM PNI_MOVIMENTO_PRODUTO VAC
                                                              JOIN PNI_VACINADOS ENV ON ENV.ID = VAC.ID_ORIGEM
                                                              WHERE VAC.TABELA_ORIGEM = 'PNI_VACINADOS' AND
                                                                    VAC.ID = MP.ID AND
                                                                    VAC.OPERACAO = 1) SAIDA
                                                        FROM PNI_MOVIMENTO_PRODUTO MP
                                                        JOIN PNI_PRODUTOR F ON MP.ID_PRODUTOR = F.ID
                                                        JOIN PNI_PRODUTO PP ON PP.ID = MP.ID_PRODUTO
                                                        WHERE CAST(MP.DATA AS DATE) BETWEEN @datainicial AND @datafinal
                                                               @filtro) TAB
                                                        GROUP BY TAB.DATA, TAB.LOTE, TAB.SALDO_INICIAL,TAB.NOME_FABRICANTE, TAB.PRODUTO, TAB.SIGLA
                                                        ORDER BY TAB.PRODUTO, TAB.NOME_FABRICANTE,TAB.LOTE, TAB.DATA";
        string IReportCommand.GetReportBoletimMovimento { get => sqlGetReportBoletimMovimento; }
    }
}
