using RgCidadao.Domain.Commands.AtencaoBasica;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Queries.AtencaoBasica
{
    public class FamiliaCommandText : IFamiliaCommand
    {
        public string sqlGetFamiliaById = $@"SELECT EF.*, PAC.CSI_NOMPAC NOME_RESPONSAVEL
                                             FROM ESUS_FAMILIA EF
                                             JOIN TSI_CADPAC PAC ON PAC.CSI_CODPAC = EF.ID_RESPONSAVEL
                                             WHERE EF.ID = @id";
        string IFamiliaCommand.GetFamiliaById { get => sqlGetFamiliaById; }

        public string sqlGetNewId = $"SELECT GEN_ID(GEN_ESUS_FAMILIA_ID, 1) AS VLR FROM RDB$DATABASE";
        string IFamiliaCommand.GetNewId { get => sqlGetNewId; }

        public string sqlGetProntuarioUso = $@"SELECT FAM.NUM_PRONTUARIO_FAMILIAR
                                               FROM VS_ESTABELECIMENTOS EST
                                               JOIN ESUS_FAMILIA FAM ON (EST.ID = FAM.ID_DOMICILIO)
                                               JOIN ESUS_MICROAREA M ON (EST.ID_MICROAREA = M.ID)
                                               WHERE M.ID_PROFISSIONAL = @id_profissional AND
                                                     FAM.NUM_PRONTUARIO_FAMILIAR IS NOT NULL
                                               ORDER BY FAM.NUM_PRONTUARIO_FAMILIAR  ";
        string IFamiliaCommand.GetProntuarioUso { get => sqlGetProntuarioUso; }

        public string sqlGetQtdMaximaFamiliaMicroarea = $@"SELECT FIRST 1 P.CSI_QTDEFAM_MICROAREA FROM TSI_PARAMETROS P";
        string IFamiliaCommand.GetQtdMaximaFamiliaMicroarea { get => sqlGetQtdMaximaFamiliaMicroarea; }

        public string sqlInsert = $@"INSERT INTO ESUS_FAMILIA (ID, NUM_PRONTUARIO_FAMILIAR, DATA_INCLUSAO, SITUACAO_CADASTRO, ID_DOMICILIO, ID_RESPONSAVEL,
                                                               ID_USUARIO, RENDA_FAMILIAR_SAL_MIN, SITUACAO_MORADIA, AREA_PROD_RURAL, RESIDE_DESDE, TRAT_AGUA,
                                                               DESTINO_LIXO, TEL_RES, TEL_REF, GATO, CACHORRO, DE_CRIACAO, OUTROS, PASSARO, QTD_ANIMAIS)
                                     
                                     VALUES(@id, @num_prontuario_familiar, @data_inclusao, @situacao_cadastro, @id_domicilio, @id_responsavel, @id_usuario,
                                             @renda_familiar_sal_min, @situacao_moradia, @area_prod_rural, @reside_desde, @trat_agua, @destino_lixo, @tel_res,
                                             @tel_ref, @gato, @cachorro, @de_criacao, @outros, @passaro, @qtd_animais)";
        string IFamiliaCommand.Insert { get => sqlInsert; }

        public string sqlUpdate = $@"UPDATE ESUS_FAMILIA
                                     SET NUM_PRONTUARIO_FAMILIAR = @num_prontuario_familiar,
                                         DATA_INCLUSAO = @data_inclusao,
                                         SITUACAO_CADASTRO = @situacao_cadastro,
                                         ID_DOMICILIO = @id_domicilio,
                                         ID_RESPONSAVEL = @id_responsavel,
                                         ID_USUARIO = @id_usuario,
                                         RENDA_FAMILIAR_SAL_MIN = @renda_familiar_sal_min,
                                         SITUACAO_MORADIA = @situacao_moradia,
                                         AREA_PROD_RURAL = @area_prod_rural,
                                         RESIDE_DESDE = @reside_desde,
                                         TRAT_AGUA = @trat_agua,
                                         DESTINO_LIXO = @destino_lixo,
                                         TEL_RES = @tel_res,
                                         TEL_REF = @tel_ref,
                                         GATO = @gato,
                                         CACHORRO = @cachorro,
                                         DE_CRIACAO = @de_criacao,
                                         OUTROS = @outros,
                                         PASSARO = @passaro,
                                         QTD_ANIMAIS = @qtd_animais
                                     WHERE ID = @id";
        string IFamiliaCommand.Update { get => sqlUpdate; }

        public string sqlAtualizaCadPacFamilia = $@"UPDATE TSI_CADPAC SET ID_FAMILIA = @id_familia
                                                    WHERE CSI_CODPAC = @id";
        string IFamiliaCommand.AtualizaCadPacFamilia { get => sqlAtualizaCadPacFamilia; }

        public string sqlGetIndividuoFamilia = $@"SELECT ID_FAMILIA
                                                  FROM TSI_CADPAC
                                                  WHERE CSI_CODPAC = @id";
        string IFamiliaCommand.GetIndividuoFamilia { get => sqlGetIndividuoFamilia; }

        public string sqlAtualizaResponsavelFamilia = $@"UPDATE ESUS_FAMILIA
                                                         SET ID_RESPONSAVEL = @responsavel
                                                         WHERE ID = @id";
        string IFamiliaCommand.AtualizaResponsavelFamilia { get => sqlAtualizaResponsavelFamilia; }

        public string sqlAtualizaFamiliaOutraArea = $@"UPDATE ESUS_FAMILIA
                                                       SET ID_DOMICILIO = null,
                                                           NUM_PRONTUARIO_FAMILIAR = null,
                                                           SITUACAO_CADASTRO = 2
                                                       WHERE ID = @id";
        string IFamiliaCommand.AtualizaFamiliaOutraArea { get => sqlAtualizaFamiliaOutraArea; }

        public string sqlAtualizaFamiliaDomicilio = $@"UPDATE ESUS_FAMILIA
                                                       SET ID_DOMICILIO = @id_domicilio,
                                                           SITUACAO_CADASTRO = 1
                                                       WHERE ID = @id";
        string IFamiliaCommand.AtualizaFamiliaDomicilio { get => sqlAtualizaFamiliaDomicilio; }

        public string sqlGetFamiliaByIndividuoResponsavel = $@"SELECT * FROM ESUS_FAMILIA
                                                               WHERE ID_RESPONSAVEL = @responsavel";
        string IFamiliaCommand.GetFamiliaByIndividuoResponsavel { get => sqlGetFamiliaByIndividuoResponsavel; }
    }
}
