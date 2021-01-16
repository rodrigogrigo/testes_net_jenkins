using Imunizacao.Domain.Commands;

namespace Imunizacao.Domain.Queries
{
    public class MovImunobiologicoCommandText : IMovImunobiologicoCommand
    {
        public string sqlGetMovByUnidade = $@"SELECT FIRST(@pagesize) SKIP(@page) MP.ID, PDT.NOME PRODUTO, MP.LOTE, PDTR.NOME PRODUTOR,
                                                   A.DESCRICAO APRESENTACAO, MP.TIPO_LANCAMENTO, PDT.ABREVIATURA, PDT.SIGLA, MP.DATA, MP.QTDE,
                                                   US.NOME USUARIO, MP.QTDE_FRASCOS
                                              FROM PNI_ACERTO_ESTOQUE MP
                                              INNER JOIN PNI_PRODUTO PDT ON (PDT.ID = MP.ID_PRODUTO)
                                              INNER JOIN PNI_PRODUTOR PDTR ON (PDTR.ID = MP.ID_PRODUTOR)
                                              INNER JOIN PNI_APRESENTACAO A ON (A.ID = MP.ID_APRESENTACAO)
                                              INNER JOIN SEG_USUARIO US ON US.ID = MP.ID_USUARIO
                                              WHERE MP.ID_UNIDADE = @unidade
                                              @filtro";

        string IMovImunobiologicoCommand.GetMovimentoByUnidade { get => sqlGetMovByUnidade; }

        public string SqlGetById = $@"SELECT * FROM PNI_ACERTO_ESTOQUE
                                      WHERE ID = @id";
        string IMovImunobiologicoCommand.GetById { get => SqlGetById; }

        public string SqlNewId = $@"SELECT GEN_ID(GEN_PNI_ACERTO_ESTOQUE_ID, 1) AS VLR FROM RDB$DATABASE";
        string IMovImunobiologicoCommand.GetNewId { get => SqlNewId; }

        public string sqlInserir = $@"INSERT INTO PNI_ACERTO_ESTOQUE (ID, ANO_APURACAO, MES_APURACAO, ID_UNIDADE, LOTE, ID_PRODUTO, ID_PRODUTOR,
                                                                         ID_APRESENTACAO, QTDE, ID_USUARIO, DATA, ID_FORNECEDOR, OBSERVACAO, TIPO_LANCAMENTO, TIPO_PERCA, QTDE_FRASCOS, ID_UNIDADE_ENVIO)
                                      VALUES (@id, @ano_apuracao, @mes_apuracao, @id_unidade, @lote, @id_produto, @id_produtor, @id_apresentacao,
                                              @qtde, @usuario, @data, @id_fornecedor, @observacao, @tipo_lancamento, @tipo_perca, @qtde_frascos, @id_unidade_envio)";
        string IMovImunobiologicoCommand.Inserir { get => sqlInserir; }

        public string sqlAtualizar = $@"UPDATE PNI_ACERTO_ESTOQUE
                                        SET QTDE = @qtde,
                                            ID_USUARIO = @usuario,
                                            DATA = @data,
                                            ID_FORNECEDOR = @id_fornecedor,
                                            OBSERVACAO = @observacao,
                                            TIPO_LANCAMENTO = @tipo_lancamento,
                                            TIPO_PERCA = @tipo_perca,
                                            QTDE_FRASCOS=@qtde_frascos,
                                            ID_UNIDADE_ENVIO =  @id_unidade_envio
                                        WHERE ID = @id";
        string IMovImunobiologicoCommand.Atualizar { get => sqlAtualizar; }

        public string sqlExcluir = $@"DELETE FROM PNI_ACERTO_ESTOQUE
                                      WHERE ID = @id";
        string IMovImunobiologicoCommand.Excluir { get => sqlExcluir; }

        public string sqlGetCountAll = $@"SELECT COUNT(*)
                                          FROM (SELECT MP.ID, PDT.NOME PRODUTO, MP.LOTE, PDTR.NOME PRODUTOR,
                                                   A.DESCRICAO APRESENTACAO, MP.TIPO_LANCAMENTO, PDT.ABREVIATURA, PDT.SIGLA, MP.DATA, MP.QTDE,
                                                   US.NOME USUARIO, MP.QTDE_FRASCOS
                                              FROM PNI_ACERTO_ESTOQUE MP
                                              INNER JOIN PNI_PRODUTO PDT ON (PDT.ID = MP.ID_PRODUTO)
                                              INNER JOIN PNI_PRODUTOR PDTR ON (PDTR.ID = MP.ID_PRODUTOR)
                                              INNER JOIN PNI_APRESENTACAO A ON (A.ID = MP.ID_APRESENTACAO)
                                              INNER JOIN SEG_USUARIO US ON US.ID = MP.ID_USUARIO
                                              WHERE MP.ID_UNIDADE = @unidade
                                                      @filtro)";
        string IMovImunobiologicoCommand.GetCountAll { get => sqlGetCountAll; }
    }
}
