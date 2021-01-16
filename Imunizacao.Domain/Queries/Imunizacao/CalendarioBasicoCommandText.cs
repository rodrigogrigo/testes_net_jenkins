using RgCidadao.Domain.Commands.Imunizacao;

namespace RgCidadao.Domain.Queries.Imunizacao
{
    public class CalendarioBasicoCommandText : ICalendarioBasicoCommand
    {
        public string GetAll = $@"SELECT FIRST(@pagesize) SKIP(@page) 
                                  CB.*,
                                  ' Entre '|| round(CB.IDADE_MINIMA,2) || ' e ' || round(CB.IDADE_MAXIMA,2) FAIXA_ETARIA,
                                  P.ABREVIATURA ||' - '|| P.SIGLA PRODUTO,
                                  D.DESCRICAO DOSE
                                  FROM PNI_CALENDARIO_BASICO CB
                                  JOIN PNI_PRODUTO P ON CB.ID_PRODUTO = P.ID
                                  JOIN PNI_DOSE D ON CB.ID_DOSE = D.ID
                                  WHERE CB.FLG_INATIVO = 0 
                                  @filtro";
        string ICalendarioBasicoCommand.GetAll { get => GetAll; }

        public string GetCountAll = $@"SELECT COUNT(*)
                                       FROM (SELECT CB.*, ' Entre '|| round(CB.IDADE_MINIMA, 2) || ' e ' || round(CB.IDADE_MAXIMA, 2) FAIXA_ETARIA, P.NOME PRODUTO, D.DESCRICAO DOSE
                                             FROM PNI_CALENDARIO_BASICO CB
                                             JOIN PNI_PRODUTO P ON CB.ID_PRODUTO = P.ID
                                             JOIN PNI_DOSE D ON CB.ID_DOSE = D.ID
                                             WHERE CB.FLG_INATIVO = 0 
                                             @filtro)";
        string ICalendarioBasicoCommand.GetCountAll { get => GetCountAll; }

        public string sqlGetnewId = $@"SELECT GEN_ID(GEN_PNI_CALENDARIO_BASICO_ID, 1) AS VLR FROM RDB$DATABASE";
        string ICalendarioBasicoCommand.GetNewId { get => sqlGetnewId; }

        public string sqlInsert = $@"INSERT INTO PNI_CALENDARIO_BASICO (ID, ID_PRODUTO, ID_DOSE, PUBLICO_ALVO, DIAS_ANTES_APRAZAMENTO, IDADE_MINIMA, 
                                                                        IDADE_MAXIMA, FLG_EXCLUIR_APRAZAMENTO, VIGENCIA_INICIO, VIGENCIA_FIM, ID_ESTRATEGIA)
                                     VALUES (@id, @id_produto, @id_dose, @publico_alvo, @dias_antes_aprazamento, @idade_minima, @idade_maxima, @flg_excluir_aprazamento,
                                                            @vigencia_inicio, @vigencia_fim, @id_estrategia)";
        string ICalendarioBasicoCommand.Insert { get => sqlInsert; }

        public string sqlGetById = $@"SELECT * FROM PNI_CALENDARIO_BASICO
                                      WHERE ID = @id";
        string ICalendarioBasicoCommand.GetById { get => sqlGetById; }

        public string sqlUpdate = $@"UPDATE PNI_CALENDARIO_BASICO
                                     SET ID_PRODUTO = @id_produto,
                                         ID_DOSE = @id_dose,
                                         PUBLICO_ALVO = @publico_alvo,
                                         DIAS_ANTES_APRAZAMENTO = @dias_antes_aprazamento,
                                         IDADE_MINIMA = @idade_minima,
                                         IDADE_MAXIMA = @idade_maxima,
                                         FLG_EXCLUIR_APRAZAMENTO = @flg_excluir_aprazamento,
                                         VIGENCIA_INICIO =  @vigencia_inicio,
                                         VIGENCIA_FIM = @vigencia_fim,
                                         ID_ESTRATEGIA = @id_estrategia
                                     WHERE ID = @id";
        string ICalendarioBasicoCommand.Update { get => sqlUpdate; }

        public string sqlDelete = $@"DELETE FROM PNI_CALENDARIO_BASICO
                                     WHERE ID = @id";
        string ICalendarioBasicoCommand.Delete { get => sqlDelete; }

        public string sqlUpdateInativo = $@"UPDATE PNI_CALENDARIO_BASICO
                                            SET FLG_INATIVO = 1
                                            WHERE ID = @id";
        string ICalendarioBasicoCommand.UpdateInativo { get => sqlUpdateInativo; }

        public string sqlGetCalendarioBasicoByProduto = $@"SELECT *
                                                           FROM PNI_CALENDARIO_BASICO
                                                           WHERE ID_PRODUTO = @id_produto";
        string ICalendarioBasicoCommand.GetCalendarioBasicoByProduto { get => sqlGetCalendarioBasicoByProduto; }
    }
}
