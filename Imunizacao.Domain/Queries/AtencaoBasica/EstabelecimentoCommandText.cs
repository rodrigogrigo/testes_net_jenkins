using RgCidadao.Domain.Commands.AtencaoBasica;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Queries.AtencaoBasica
{
    public class EstabelecimentoCommandText : IEstabelecimentoCommand
    {
        public string sqlGetEstabelecimentoById = $@"SELECT VS.*, BAI.CSI_NOMBAI BAIRRO, LOG.CSI_NOMEND LOGRADOURO 
                                                     FROM VS_ESTABELECIMENTOS VS
                                                     LEFT JOIN TSI_LOGRADOURO LOG ON VS.ID_LOGRADOURO = LOG.CSI_CODEND
                                                     LEFT JOIN TSI_BAIRRO BAI ON LOG.CSI_CODBAI = BAI.CSI_CODBAI
                                                     WHERE VS.ID = @id";
        string IEstabelecimentoCommand.GetEstabelecimentoById { get => sqlGetEstabelecimentoById; }

        public string sqlGetNewId = $@"SELECT GEN_ID(GEN_VS_ESTABELECIMENTOS_ID, 1) AS VLR FROM RDB$DATABASE";
        string IEstabelecimentoCommand.GetNewId { get => sqlGetNewId; }

        public string sqlInsert = $@"INSERT INTO VS_ESTABELECIMENTOS (ID, ID_PROFISSIONAL, ID_MICROAREA, ID_LOGRADOURO, TIPO_IMOVEL, NUMERO_LOGRADOURO,
                                                                      COMPLEMENTO_LOGRADOURO, TELEFONE_FIXO, TELEFONE_MOVEL, ZONA, TIPO_DOMICILIO,
                                                                      QTD_COMODOS, TIPO_ACESSO_DOMIC, MAT_PREDOMINANTE, ABASTECIMENTO_AGUA,
                                                                      ESCOAMENTO_SANITA, DISPONIB_ENERGIA, NOME_INST_PERMANENCIA, OUTROS_PROFI_INSTITUICAO,
                                                                      NOME_RESP_INSTIT, CNS_RESP_INSTIT, CARGO_RESP_INSTIT, TEL_RESP_INSTIT, DATA_CADASTRO, ID_USUARIO)
                                     VALUES (@id, @id_profissional, @id_microarea, @id_logradouro, @tipo_imovel, @numero_logradouro, @complemento_logradouro,
                                             @telefone_fixo, @telefone_movel, @zona, @tipo_domicilio, @qtd_comodos, @tipo_acesso_domic, @mat_predominante,
                                             @abastecimento_agua, @escoamento_sanita, @disponib_energia, @nome_inst_permanencia, @outros_profi_instituicao,
                                             @nome_resp_instit, @cns_resp_instit, @cargo_resp_instit, @tel_resp_instit, @data_cadastro, @id_usuario)";
        string IEstabelecimentoCommand.Insert { get => sqlInsert; }

        public string sqlUpdate = $@"UPDATE VS_ESTABELECIMENTOS
                                     SET ID_PROFISSIONAL = @id_profissional,
                                         ID_MICROAREA = @id_microarea,
                                         ID_LOGRADOURO = @id_logradouro,
                                         TIPO_IMOVEL = @tipo_imovel,
                                         NUMERO_LOGRADOURO = @numero_logradouro,
                                         COMPLEMENTO_LOGRADOURO = @complemento_logradouro,
                                         TELEFONE_FIXO = @telefone_fixo,
                                         TELEFONE_MOVEL = @telefone_movel,
                                         ZONA = @zona,
                                         TIPO_DOMICILIO = @tipo_domicilio,
                                         QTD_COMODOS = @qtd_comodos,
                                         TIPO_ACESSO_DOMIC = @tipo_acesso_domic,
                                         MAT_PREDOMINANTE = @mat_predominante,
                                         ABASTECIMENTO_AGUA = @abastecimento_agua,
                                         ESCOAMENTO_SANITA = @escoamento_sanita,
                                         DISPONIB_ENERGIA = @disponib_energia,
                                         NOME_INST_PERMANENCIA = @nome_inst_permanencia,
                                         OUTROS_PROFI_INSTITUICAO = @outros_profi_instituicao,
                                         NOME_RESP_INSTIT = @nome_resp_instit,
                                         CNS_RESP_INSTIT = @cns_resp_instit,
                                         CARGO_RESP_INSTIT = @cargo_resp_instit,
                                         TEL_RESP_INSTIT = @tel_resp_instit
                                     WHERE ID = @id";
        string IEstabelecimentoCommand.Update { get => sqlUpdate; }

        public string sqlGetEstabelecimentosByArea = $@"SELECT FIRST (@pagesize) SKIP (@page)
                                                            EST.ID,
                                                            EST.TIPO_IMOVEL,
                                                            EST.NUMERO_LOGRADOURO,
                                                            LOG.CSI_NOMEND LOGRADOURO,
                                                            BAI.CSI_NOMBAI BAIRRO,
                                                            CID.CSI_NOMCID CIDADE,
                                                            CID.CSI_SIGEST SIGLA_ESTADO,
                                                            (SELECT COUNT(*) FROM ESUS_FAMILIA FAM WHERE FAM.ID_DOMICILIO = EST.ID) QTDE_FAMILIA
                                                        FROM VS_ESTABELECIMENTOS EST
                                                        JOIN TSI_LOGRADOURO LOG ON (EST.ID_LOGRADOURO = LOG.CSI_CODEND)
                                                        JOIN TSI_BAIRRO BAI ON (LOG.CSI_CODBAI = BAI.CSI_CODBAI)
                                                        JOIN TSI_CIDADE CID ON (BAI.CSI_CODCID = CID.CSI_CODCID)
                                                        WHERE COALESCE(EST.TIPO_IMOVEL, 0) IN (0, 6, 7)
                                                        AND EST.ID_MICROAREA = @id_microarea 
                                                        @filtros";
        string IEstabelecimentoCommand.GetEstabelecimentosByArea { get => sqlGetEstabelecimentosByArea; }

        public string sqlGetCountEstabelecimentosByArea = $@"SELECT COUNT(*) FROM (SELECT
                                                                                       EST.ID,
                                                                                       EST.TIPO_IMOVEL,
                                                                                       EST.NUMERO_LOGRADOURO,
                                                                                       LOG.CSI_NOMEND LOGRADOURO,
                                                                                       BAI.CSI_NOMBAI BAIRRO,
                                                                                       CID.CSI_NOMCID CIDADE,
                                                                                       CID.CSI_SIGEST SIGLA_ESTADO,
                                                                                       (SELECT COUNT(*) FROM ESUS_FAMILIA FAM WHERE FAM.ID_DOMICILIO = EST.ID) QTDE_FAMILIA
                                                                                   FROM VS_ESTABELECIMENTOS EST
                                                                                   JOIN TSI_LOGRADOURO LOG ON (EST.ID_LOGRADOURO = LOG.CSI_CODEND)
                                                                                   JOIN TSI_BAIRRO BAI ON (LOG.CSI_CODBAI = BAI.CSI_CODBAI)
                                                                                   JOIN TSI_CIDADE CID ON (BAI.CSI_CODCID = CID.CSI_CODCID)
                                                                                   WHERE COALESCE(EST.TIPO_IMOVEL, 0) IN (0, 6, 7)
                                                                                   AND EST.ID_MICROAREA = @id_microarea
                                                                                    @filtros)";
        string IEstabelecimentoCommand.GetCountEstabelecimentosByArea { get => sqlGetCountEstabelecimentosByArea; }
    }
}
