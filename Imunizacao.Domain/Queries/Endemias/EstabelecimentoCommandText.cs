using RgCidadao.Domain.Commands.Endemias;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Queries.Endemias
{
    public class EstabelecimentoCommandText : IEstabelecimentoCommand
    {
        public string sqlGetImovelById = $@"SELECT VS.*, BAI.CSI_NOMBAI BAIRRO, LOG.CSI_NOMEND LOGRADOURO 
                                            FROM VS_ESTABELECIMENTOS VS
                                            LEFT JOIN TSI_LOGRADOURO LOG ON VS.ID_LOGRADOURO = LOG.CSI_CODEND
                                            LEFT JOIN TSI_BAIRRO BAI ON LOG.CSI_CODBAI = BAI.CSI_CODBAI
                                            WHERE VS.ID = @id";
        string IEstabelecimentoCommand.GetImovelById { get => sqlGetImovelById; }

        public string sqlGetCountAll = $@"SELECT COUNT(*) FROM (SELECT CASE
                                                                      WHEN VS.RAZAO_SOCIAL_NOME IS NOT NULL THEN VS.RAZAO_SOCIAL_NOME
                                                                      WHEN VS.NOME_FANTASIA_APELIDO IS NOT NULL THEN VS.NOME_FANTASIA_APELIDO
                                                                      WHEN CP.CSI_NOMPAC IS NOT NULL THEN CP.CSI_NOMPAC
                                                                      ELSE 'NÃO INFORMADO'
                                                                    END AS IDENTIFICACAO_IMOVEL,
                                                                    VS.*,
                                                                    BAI.CSI_NOMBAI BAIRRO, LOG.CSI_NOMEND LOGRADOURO
                                                                FROM VS_ESTABELECIMENTOS VS
                                                                LEFT JOIN TSI_LOGRADOURO LOG ON VS.ID_LOGRADOURO = LOG.CSI_CODEND
                                                                LEFT JOIN TSI_BAIRRO BAI ON LOG.CSI_CODBAI = BAI.CSI_CODBAI
                                                                LEFT JOIN ESUS_FAMILIA FAM ON VS.ID = FAM.ID_DOMICILIO
                                                                LEFT JOIN TSI_CADPAC CP ON FAM.ID_RESPONSAVEL = CP.CSI_CODPAC
                                                                @filtro)";
        string IEstabelecimentoCommand.GetCountAll { get => sqlGetCountAll; }

        public string sqlGetAllPagination = $@"SELECT FIRST(@pagesize) SKIP(@page) 
                                                   CASE
                                                     WHEN VS.RAZAO_SOCIAL_NOME IS NOT NULL THEN VS.RAZAO_SOCIAL_NOME
                                                     WHEN VS.NOME_FANTASIA_APELIDO IS NOT NULL THEN VS.NOME_FANTASIA_APELIDO
                                                     WHEN CP.CSI_NOMPAC IS NOT NULL THEN CP.CSI_NOMPAC
                                                     ELSE 'NÃO INFORMADO'
                                                   END AS IDENTIFICACAO_IMOVEL,
                                                   VS.*,
                                                   BAI.CSI_NOMBAI BAIRRO, LOG.CSI_NOMEND LOGRADOURO
                                               FROM VS_ESTABELECIMENTOS VS
                                               LEFT JOIN TSI_LOGRADOURO LOG ON VS.ID_LOGRADOURO = LOG.CSI_CODEND
                                               LEFT JOIN TSI_BAIRRO BAI ON LOG.CSI_CODBAI = BAI.CSI_CODBAI
                                               LEFT JOIN ESUS_FAMILIA FAM ON VS.ID = FAM.ID_DOMICILIO
                                               LEFT JOIN TSI_CADPAC CP ON FAM.ID_RESPONSAVEL = CP.CSI_CODPAC
                                               @filtro
                                               ORDER BY IDENTIFICACAO_IMOVEL
                                               ";
        string IEstabelecimentoCommand.GetAllPagination { get => sqlGetAllPagination; }

        public string sqlGetAll = $@"SELECT * FROM VS_ESTABELECIMENTOS
                                     WHERE COALESCE(EXCLUIDO, 'F') = 'F'";
        string IEstabelecimentoCommand.GetAll { get => sqlGetAll; }

        public string sqlGetNewId = $@"SELECT GEN_ID(GEN_VS_ESTABELECIMENTOS_ID, 1) AS VLR FROM RDB$DATABASE";
        string IEstabelecimentoCommand.GetNewId { get => sqlGetNewId; }

        public string sqlInsert = $@"INSERT INTO VS_ESTABELECIMENTOS (ID, RAZAO_SOCIAL_NOME, NOME_FANTASIA_APELIDO, CNPJ_CPF, INSC_ESTADUAL, INSC_MUNICIPAL,
                                                                      INSC_PRODUTOR_RURAL, NATUREZA_JURIDICA, ID_LOGRADOURO, NUMERO_LOGRADOURO,
                                                                      COMPLEMENTO_LOGRADOURO, TELEFONE_FIXO, TELEFONE_MOVEL, FAX, EMAIL, WEBSITE,
                                                                      DATA_CADASTRO, SITUACAO_ATUAL, DATA_SITUACAO_ATUAL, MATRIZ_FILIAL, CNPJ_MATRIZ,
                                                                      ESFERA_ADM, NATUREZA_ORGANIZACAO, DATA_BAIXA_FECHAMENTO, DESCRICAO_CNAE,
                                                                      PROCEDIMENTO_CADASTRO, TEMPORARIO, NUMERO_VISA, CNES_ESTABELECIMENTO, ZONA, METRAGEM,
                                                                      ID_MICROAREA, NUM_PRONTUARIO_FAMILIAR, ID_USUARIO, PONTO_REFERENCIA, TIPO_DOMICILIO,
                                                                      QTD_COMODOS, TIPO_ACESSO_DOMIC, MAT_PREDOMINANTE, DISPONIB_ENERGIA, ABASTECIMENTO_AGUA,
                                                                      ESCOAMENTO_SANITA, LATITUDE, LONGITUDE, QRCODE, UUID, UUID_ALTERACAO,
                                                                      DATA_ALTERACAO_SERV, FLG_EXPORTAR_ESUS, ID_ESUS_EXPORTACAO_ITEM, TIPO_IMOVEL,
                                                                      TIPO_LOGRADOURO, CARGO_RESP_INSTIT, NOME_RESP_INSTIT, OUTROS_PROFI_INSTITUICAO,
                                                                      TEL_RESP_INSTIT, ID_PROFISSIONAL, CNS_RESP_INSTIT,
                                                                      NOME_INST_PERMANENCIA, EXCLUIDO, QUARTEIRAO_LOGRADOURO,SEQUENCIA_QUARTEIRAO,SEQUENCIA_NUMERO)
                                     VALUES (@id, @razao_social_nome, @nome_fantasia_apelido, @cnpj_cpf, @insc_estadual, @insc_municipal,
                                             @insc_produtor_rural, @natureza_juridica, @id_logradouro, @numero_logradouro, @complemento_logradouro,
                                             @telefone_fixo, @telefone_movel, @fax, @email, @website, @data_cadastro, @situacao_atual,
                                             @data_situacao_atual, @matriz_filial, @cnpj_matriz, @esfera_adm, @natureza_organizacao,
                                             @data_baixa_fechamento, @descricao_cnae, @procedimento_cadastro, @temporario, @numero_visa,
                                             @cnes_estabelecimento, @zona, @metragem, @id_microarea, @num_prontuario_familiar, @id_usuario,
                                             @ponto_referencia, @tipo_domicilio, @qtd_comodos, @tipo_acesso_domic, @mat_predominante,
                                             @disponib_energia, @abastecimento_agua, @escoamento_sanita, @latitude, @longitude, @qrcode, @uuid,
                                             @uuid_alteracao, @data_alteracao_serv, @flg_exportar_esus, @id_esus_exportacao_item, @tipo_imovel,
                                             @tipo_logradouro, @cargo_resp_instit, @nome_resp_instit, @outros_profi_instituicao, @tel_resp_instit,
                                             @id_profissional, @cns_resp_instit, @nome_inst_permanencia, @excluido, @quarteirao_logradouro,
                                             @sequencia_quarteirao, @sequencia_numero)";
        string IEstabelecimentoCommand.Insert { get => sqlInsert; }

        public string sqlUpdate = $@"UPDATE VS_ESTABELECIMENTOS
                                     SET RAZAO_SOCIAL_NOME = @razao_social_nome,
                                         NOME_FANTASIA_APELIDO = @nome_fantasia_apelido,
                                         CNPJ_CPF = @cnpj_cpf,
                                         INSC_ESTADUAL = @insc_estadual,
                                         INSC_MUNICIPAL = @insc_municipal,
                                         INSC_PRODUTOR_RURAL = @insc_produtor_rural,
                                         NATUREZA_JURIDICA = @natureza_juridica,
                                         ID_LOGRADOURO = @id_logradouro,
                                         NUMERO_LOGRADOURO = @numero_logradouro,
                                         COMPLEMENTO_LOGRADOURO = @complemento_logradouro,
                                         TELEFONE_FIXO = @telefone_fixo,
                                         TELEFONE_MOVEL = @telefone_movel,
                                         FAX = @telefone_movel,
                                         EMAIL = @email,
                                         WEBSITE = @website,
                                         DATA_CADASTRO = @data_cadastro,
                                         SITUACAO_ATUAL = @situacao_atual,
                                         DATA_SITUACAO_ATUAL = @data_situacao_atual,
                                         MATRIZ_FILIAL = @matriz_filial,
                                         CNPJ_MATRIZ = @cnpj_matriz,
                                         ESFERA_ADM = @esfera_adm,
                                         NATUREZA_ORGANIZACAO = @natureza_organizacao,
                                         DATA_BAIXA_FECHAMENTO = @data_baixa_fechamento,
                                         DESCRICAO_CNAE = @descricao_cnae,
                                         PROCEDIMENTO_CADASTRO = @procedimento_cadastro,
                                         TEMPORARIO = @temporario,
                                         NUMERO_VISA = @numero_visa,
                                         CNES_ESTABELECIMENTO = @cnes_estabelecimento,
                                         ZONA = @zona,
                                         METRAGEM = @metragem,
                                         ID_MICROAREA = @id_microarea,
                                         NUM_PRONTUARIO_FAMILIAR = @num_prontuario_familiar,
                                         ID_USUARIO = @id_usuario,
                                         PONTO_REFERENCIA = @ponto_referencia,
                                         TIPO_DOMICILIO = @tipo_domicilio,
                                         QTD_COMODOS = @qtd_comodos,
                                         TIPO_ACESSO_DOMIC = @tipo_acesso_domic,
                                         MAT_PREDOMINANTE = @mat_predominante,
                                         DISPONIB_ENERGIA = @disponib_energia,
                                         ABASTECIMENTO_AGUA = @abastecimento_agua,
                                         ESCOAMENTO_SANITA = @escoamento_sanita,
                                         LATITUDE = @latitude,
                                         LONGITUDE = @longitude,
                                         QRCODE = @qrcode,
                                         UUID = @uuid,
                                         UUID_ALTERACAO = @uuid_alteracao,
                                         DATA_ALTERACAO_SERV = @data_alteracao_serv,
                                         FLG_EXPORTAR_ESUS = @flg_exportar_esus,
                                         ID_ESUS_EXPORTACAO_ITEM = @id_esus_exportacao_item,
                                         TIPO_IMOVEL = @tipo_imovel,
                                         TIPO_LOGRADOURO = @tipo_logradouro,
                                         CARGO_RESP_INSTIT = @cargo_resp_instit,
                                         NOME_RESP_INSTIT = @nome_resp_instit,
                                         OUTROS_PROFI_INSTITUICAO = @outros_profi_instituicao,
                                         TEL_RESP_INSTIT = @tel_resp_instit,
                                         ID_PROFISSIONAL = @id_profissional,
                                         CNS_RESP_INSTIT = @cns_resp_instit,
                                         NOME_INST_PERMANENCIA = @nome_inst_permanencia,
                                         QUARTEIRAO_LOGRADOURO = @quarteirao_logradouro,
                                         SEQUENCIA_QUARTEIRAO = @sequencia_quarteirao,
                                         SEQUENCIA_NUMERO = @sequencia_numero
                                     WHERE ID = @id";
        string IEstabelecimentoCommand.Update { get => sqlUpdate; }

        public string sqlDelete = $@"UPDATE VS_ESTABELECIMENTOS SET EXCLUIDO = 'T'
                                     WHERE ID = @id";
        string IEstabelecimentoCommand.Delete { get => sqlDelete; }

        public string sqlGetEstabelecimentoByCiclo = $@"SELECT DISTINCT VE.ID,
                                                                    (SELECT FIRST (1) VI1.ID ID_VISITA
                                                                     FROM VISITA_IMOVEL VI1
                                                                     JOIN ENDEMIAS_CICLOS EC ON (VI1.DATA_HORA_ENTRADA BETWEEN EC.DATA_INICIAL AND EC.DATA_FINAL AND VI1.DATA_HORA_SAIDA BETWEEN EC.DATA_INICIAL AND EC.DATA_FINAL)
                                                                     WHERE VI1.ID_ESTABELECIMENTO = VE.ID
                                                                     ORDER BY VI1.DATA_HORA_SAIDA DESC) ID_VISITA,
                                                                     (SELECT FIRST (1) VI2.DESFECHO
                                                                     FROM VISITA_IMOVEL VI2
                                                                     JOIN ENDEMIAS_CICLOS EC ON (VI2.DATA_HORA_ENTRADA BETWEEN EC.DATA_INICIAL AND EC.DATA_FINAL AND VI2.DATA_HORA_SAIDA BETWEEN EC.DATA_INICIAL AND EC.DATA_FINAL)
                                                                     WHERE VI2.ID_ESTABELECIMENTO = VE.ID
                                                                     ORDER BY VI2.DATA_HORA_SAIDA DESC) DESFECHO
                                                        FROM VS_ESTABELECIMENTOS VE
                                                        JOIN VISITA_IMOVEL VI ON VI.ID_ESTABELECIMENTO = VE.ID
                                                        JOIN ENDEMIAS_CICLOS EC ON (VI.DATA_HORA_ENTRADA BETWEEN EC.DATA_INICIAL AND EC.DATA_FINAL AND VI.DATA_HORA_SAIDA BETWEEN EC.DATA_INICIAL AND EC.DATA_FINAL)
                                                        WHERE EC.ID = @id_ciclo ";
        string IEstabelecimentoCommand.GetEstabelecimentoByCiclo { get => sqlGetEstabelecimentoByCiclo; }
    }
}
