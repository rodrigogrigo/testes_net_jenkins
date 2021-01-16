using RgCidadao.Domain.Commands.AtencaoBasica;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Queries.AtencaoBasica
{
    public class VisitaDomiciliarCommandText : IVisitaDomiciliarCommand
    {
        #region SQL retorna todos registros 
        public string sqlGetAllPagination = $@"SELECT FIRST (@pagesize) SKIP (@page) VD.ID, VD.DATA_VISITA, M.CSI_CODMED, M.CSI_NOMMED,
                                            C.CSI_CODPAC, C.CSI_NOMPAC, U.CSI_CODUNI, U.CSI_NOMUNI
                                            FROM ESUS_VISITA_DOMICILIAR VD
                                            JOIN TSI_MEDICOS M ON (VD.ID_PROFISSIONAL = M.CSI_CODMED)
                                            JOIN TSI_CADPAC C ON (VD.ID_CIDADAO = C.CSI_CODPAC)
                                            JOIN TSI_UNIDADE U ON (VD.ID_UNIDADE = U.CSI_CODUNI) @filtro
                                            ORDER BY VD.DATA_VISITA";
        string IVisitaDomiciliarCommand.getAllPagination { get => sqlGetAllPagination; }
        #endregion

        #region SQL Retorna quantidade de registro
        public string sqlGetCountAll = $@"SELECT COUNT(VD.ID)
                                        FROM ESUS_VISITA_DOMICILIAR VD
                                        JOIN TSI_MEDICOS M ON (VD.ID_PROFISSIONAL = M.CSI_CODMED)
                                        JOIN TSI_CADPAC C ON (VD.ID_CIDADAO = C.CSI_CODPAC)
                                        JOIN TSI_UNIDADE U ON (VD.ID_UNIDADE = U.CSI_CODUNI) @filtro";

        string IVisitaDomiciliarCommand.getCountAll { get => sqlGetCountAll; }
        #endregion

        #region SQL Retorna apenas um
        public string sqlGetById = $@"SELECT VD.* FROM ESUS_VISITA_DOMICILIAR VD WHERE VD.ID = @id ";

        string IVisitaDomiciliarCommand.getById { get => sqlGetById; }
        #endregion
        
        # region SQL Excluir apenas um
        public string sqlDelete = $@"DELETE FROM ESUS_VISITA_DOMICILIAR WHERE ID = @id";

        string IVisitaDomiciliarCommand.Delete { get => sqlDelete; }
        #endregion

        #region SQL Inserir
        public string sqlInsertUpdate = $@"UPDATE OR INSERT INTO ESUS_VISITA_DOMICILIAR (ID, ID_PROFISSIONAL, TURNO, COMPETENCIA, DATA_VISITA, ID_DOMICILIO,
                                    VISITA_COMPARTILHADA, VISITA_PERIODICA, BA_CONSULTA, BA_EXAME, BA_VACINA,
                                    BA_BOLSAFAMILIA, MV_GESTANTE, MV_PUERPERA, MV_RECEM_NASCIDO, MV_CRIANCA,
                                    MV_DESNUTRICAO, MV_REABILITACAO_DEFICIENCIA, MV_HIPERTENCAO, MV_DIABETES, MV_ASMA,
                                    MV_DPOC, MV_CANCER, MV_DOENCA_CRONICA, MV_HANSENIASE, MV_TUBERCULOSE,
                                    MV_DOMICILIADO_ACAMADO, MV_VULNERABILIDADE_SOCIAL, MV_BOLSA_FAMILIA,
                                    MV_SAUDE_MENTAL, MV_ALCOOL, MV_OUTRAS_DROGAS, MV_INTERNACAO, MV_CONTROLE_AMBIENTES,
                                    MV_ATV_COLETIVA, MV_ORIENTACAO_PREVENCAO, MV_OUTROS, DESFECHO,
                                    CADASTRAMENTO_ATUALIZACAO, ID_CIDADAO, UUID, EXPORTADO_ESUS, DATA_ALTERACAO_SERV,
                                    UUID_REGISTRO_MOBILE, CSI_NOMUSUALTER, MV_SINT_RESPIRATORIOS, MV_TABAGISTA,
                                    LATITUDE, LONGITUDE, CODIGO_MICROAREA, TIPO_IMOVEL, MV_ACAO_EDUCATIVA,
                                    MV_IMOVEL_FOCO, MV_ACAO_MECANICA, MV_TRAT_FOCAL, PESO, ALTURA, FORA_AREA,
                                    ID_ESUS_EXPORTACAO_ITEM, ID_SEXO, DATA_NASCIMENTO, ID_UNIDADE, ID_USUARIO,
                                    ID_ESTABELECIMENTO, ID_EQUIPE, ID_CONTROLE_SINCRONIZACAO_LOTE)
                             VALUES (@id, @id_profissional, @turno, @competencia, @data_visita, @id_domicilio,
                                    @visita_compartilhada, @visita_periodica, @ba_consulta, @ba_exame, @ba_vacina,
                                    @ba_bolsafamilia, @mv_gestante, @mv_puerpera, @mv_recem_nascido, @mv_crianca,
                                    @mv_desnutricao, @mv_reabilitacao_deficiencia, @mv_hipertencao, @mv_diabetes, @mv_asma,
                                    @mv_dpoc, @mv_cancer, @mv_doenca_cronica, @mv_hanseniase, @mv_tuberculose,
                                    @mv_domiciliado_acamado, @mv_vulnerabilidade_social, @mv_bolsa_familia,
                                    @mv_saude_mental, @mv_alcool, @mv_outras_drogas, @mv_internacao, @mv_controle_ambientes,
                                    @mv_atv_coletiva, @mv_orientacao_prevencao, @mv_outros, @desfecho,
                                    @cadastramento_atualizacao, @id_cidadao, @uuid, @exportado_esus, @data_alteracao_serv,
                                    @uuid_registro_mobile, @csi_nomusualter, @mv_sint_respiratorios, @mv_tabagista,
                                    @latitude, @longitude, @codigo_microarea, @tipo_imovel, @mv_acao_educativa,
                                    @mv_imovel_foco, @mv_acao_mecanica, @mv_trat_focal, @peso, @altura, @fora_area,
                                    @id_esus_exportacao_item, @id_sexo, @data_nascimento, @id_unidade, @id_usuario,
                                    @id_estabelecimento, @id_equipe, @id_controle_sincronizacao_lote) MATCHING(ID);";
        string IVisitaDomiciliarCommand.Insert { get => sqlInsertUpdate; }
        #endregion

        #region SQL Alterar
        string IVisitaDomiciliarCommand.Update { get => sqlInsertUpdate; }
        #endregion

        #region SQL Retorna próximo ID
        public string sqlNewId = $@"SELECT GEN_ID(GEN_ESUS_VISITA_DOMICILIAR_ID, 1) AS VLR FROM RDB$DATABASE";
        string IVisitaDomiciliarCommand.GetNewId { get => sqlNewId; }
        #endregion

    }
}
