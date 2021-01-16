using Imunizacao.Domain.Commands;

namespace Imunizacao.Domain.Queries
{
    public class CartaoVacinaCommandText : ICartaoVacinaCommand
    {
        public string sqlGetNewId = $@"SELECT GEN_ID(GEN_PNI_VACINADOS_ID, 1) AS VLR FROM RDB$DATABASE";
        string ICartaoVacinaCommand.GetNewId { get => sqlGetNewId; }

        public string sqlInsert = $@"INSERT INTO PNI_VACINADOS (ID, ID_UNIDADE, ID_PROFISIONAL, ID_PRODUTO, LOTE, VENCIMENTO, DATA_APLICACAO,
                                                                DATA_APRAZAMENTO, DATA_PREVISTA, REGISTRO_ANTERIOR, HANSENIASE, GESTANTE, INADVERTIDA,
                                                                USUARIO, DATA_HORA, ID_PACIENTE, CBO, ID_DOSE, ID_ESTRATEGIA, ID_GRUPO_ATENDIMENTO,
                                                                ID_PRODUTOR, ID_MODIVO_INDICACAO, EXPORTADO, ID_GESTACAO, UUID, ID_TURNO,
                                                                ID_LOCAL_ATENDIMENTO, FLG_VIAJANTE, FLG_PUERPERA, ID_ESUS_EXPORTACAO_ITEM, DATA_NASCIMENTO,
                                                                ID_SEXO, NOME_PRODUTOR, ID_USUARIO_EXCLUSAO, FLG_EXCLUIDO,OBSERVACAO, ID_VIA_ADM, ID_LOCAL_APLICACAO)
                                     VALUES (@id, @id_unidade, @id_profisional, @id_produto, @lote, @vencimento, @data_aplicacao,
                                             @data_aprazamento, @data_prevista, @registro_anterior, @hanseniase, @gestante, @inadvertida,
                                             @usuario, @data_hora, @id_paciente, @cbo, @id_dose, @id_estrategia, @id_grupo_atendimento,
                                             @id_produtor, @id_modivo_indicacao, @exportado, @id_gestacao, @uuid, @id_turno,
                                             @id_local_atendimento, @flg_viajante, @flg_puerpera, @id_esus_exportacao_item, @data_nascimento,
                                             @id_sexo, @nome_produtor, @id_usuario_exclusao, @flg_excluido, @observacao, @id_via_adm, @id_local_aplicacao)";
        string ICartaoVacinaCommand.Insert { get => sqlInsert; }

        public string sqlUpdate = $@"UPDATE PNI_VACINADOS
                                     SET ID_UNIDADE = @id_unidade,
                                         ID_PROFISIONAL = @id_profisional,
                                         ID_PRODUTO = @id_produto,
                                         LOTE = @lote,
                                         VENCIMENTO = @vencimento,
                                         DATA_APLICACAO = @data_aplicacao,
                                         DATA_APRAZAMENTO = @data_aprazamento,
                                         DATA_PREVISTA = @data_prevista,
                                         REGISTRO_ANTERIOR = @registro_anterior,
                                         HANSENIASE = @hanseniase,
                                         GESTANTE = @gestante,
                                         INADVERTIDA = @inadvertida,
                                         USUARIO = @usuario,
                                         DATA_HORA= @data_hora,
                                         ID_PACIENTE = @id_paciente,
                                         CBO= @cbo,
                                         ID_DOSE = @id_dose,
                                         ID_ESTRATEGIA = @id_estrategia,
                                         ID_GRUPO_ATENDIMENTO = @id_grupo_atendimento,
                                         ID_PRODUTOR = @id_produtor,
                                         ID_MODIVO_INDICACAO = @id_modivo_indicacao,
                                         EXPORTADO = @exportado,
                                         ID_GESTACAO = @id_gestacao,
                                         UUID = @uuid,
                                         ID_TURNO = @id_turno,
                                         ID_LOCAL_ATENDIMENTO = @id_local_atendimento,
                                         FLG_VIAJANTE = @flg_viajante,
                                         FLG_PUERPERA = @flg_puerpera,
                                         ID_ESUS_EXPORTACAO_ITEM = @id_esus_exportacao_item,
                                         DATA_NASCIMENTO = @data_nascimento,
                                         ID_SEXO = @id_sexo,
                                         NOME_PRODUTOR = @nome_produtor,
                                         ID_USUARIO_EXCLUSAO = @id_usuario_exclusao,
                                         FLG_EXCLUIDO = @flg_excluido,
                                         OBSERVACAO = @observacao,
                                         ID_VIA_ADM = @id_via_adm,
                                         ID_LOCAL_APLICACAO = @id_local_aplicacao
                                     WHERE ID = @id";
        string ICartaoVacinaCommand.Update { get => sqlUpdate; }

        public string sqlDelete = $@"UPDATE PNI_VACINADOS
                                     SET FLG_EXCLUIDO = 1
                                     WHERE ID = @id";
        string ICartaoVacinaCommand.Delete { get => sqlDelete; }

        public string sqlGetCartaoVacinaById = $@"SELECT *
                                                  FROM PNI_VACINADOS
                                                  WHERE ID = @id";
        string ICartaoVacinaCommand.GetCartaoVacinaById { get => sqlGetCartaoVacinaById; }

        public string sqlCancelarCartaoVacina = $@"";
        string ICartaoVacinaCommand.CancelarCartaoVacina { get => sqlCancelarCartaoVacina; }
    }
}
