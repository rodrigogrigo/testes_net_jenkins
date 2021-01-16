using RgCidadao.Domain.Commands.Cadastro;

namespace RgCidadao.Domain.Queries.Cadastro
{
    public class IndividuoCommandText : IIndividuoCommand
    {
       
        public string sqlGetNewId = $@"SELECT GEN_ID(GEN_TSI_CADPAC_ID, 1) AS VLR FROM RDB$DATABASE";
        string IIndividuoCommand.GetNewId { get => sqlGetNewId; }

        public string sqlInsert = $@"INSERT INTO TSI_CADPAC (
                ID_USUARIO, CSI_CODPAC, CSI_NOMPAC, NOME_SOCIAL, CSI_SEXPAC, CSI_CORPAC, ETNIA, CSI_DTNASC, EMIAL, CSI_CELULAR,
                CSI_MAEPAC, CSI_PAIPAC, NACIONALIDADE, CSI_CODNAT, CSI_NATURALIDADE_DATA, CSI_NATURALIDADE_PORTARIA,
                CSI_ID_NACIONALIDADE, CSI_DATA_ENTRADA_PAIS, CSI_SANGUEGRUPO, CSI_SANGUEFATOR, CSI_CODHEMOES,
                CSI_DSANGUE, VERIF_SITUACAO_RUA, CSI_CPFPAC, CSI_NCARTAO, CSI_PISPAC, CSI_IDEPAC, CSI_ORGIDE,
                CSI_COMIDE, CSI_EXPIDE, CSI_ESTIDE, CSI_DT_PRIMEIRA_CNH, CSI_TIPCER, NUMERO_DNV, CSI_NUMCER,
                CSI_FOLIVR, CSI_LIVCER, CSI_CERTIDAO_TERMO, CSI_EMICER, CSI_NOVA_CERTIDAO, CSI_CARCER, CSI_CIDCERC,
                CSI_TITELE, CSI_ZONTIT, CSI_SECTIT, CSI_CTPSAS, CSI_CTPS_SERIE, CSI_CTPS_DTEMIS, CSI_CTPS_UF,
                CSI_ACERRES, CSI_ACARVAC, CSI_ACOMRES, CSI_NCERRES, NUM_PROCESSO_ESTADO, CSI_CRE, CSI_CODEND,
                CSI_ENDPAC, CSI_NUMERO_LOGRADOURO, COMPLEMENTO, CSI_BAIPAC, CSI_CEPPAC, CSI_CODCID,
                ID_ESTABELECIMENTO_SAUDE, COD_AGEESUS, FORA_AREA,
                CSI_ESTCIV, CSI_SITUACAO_FAMILIAR, CSI_CODGRAU, CSI_CODPRO, CSI_ESCPAC, SIT_MERCADO_TRAB,
                COMUNIDADE_TRADIC, DESC_COMUNIDADE, ESUS_VERIFICA_IDENT_GENERO, ESUS_IDENT_GENERO, VERIFICA_IDENT_SEX,
                ORIENTACAO_SEXUAL, ESUS_CRIANCA_ADULTO, ESUS_CRIANCA_OUTRA_CRIANCA, ESUS_CRIANCA_OUTRO,
                ESUS_CRIANCA_ADOLESCENTE, ESUS_CRIANCA_SOZINHA, ESUS_CRIANCA_CRECHE, VERIFICA_DEFICIENCIA,
                DEF_AUDITIVA, DEF_INTELECTUAL, DEF_VISUAL, DEF_FISICA, DEF_OUTRA, CSI_ESTUDANDO, FREQ_CURANDEIRO,
                GRUPO_COMUNITARIO, POSSUI_PLANO_SAUDE, SITUACAO_PESO, INTERNACAO, INTERNACAO_CAUSA, PLANTAS_MEDICINAIS,
                QUAIS_PLANTAS, VERIFICA_CARDIACA, INSULF_CARDIACA, CARDIACA_OUTRO, CARDIACA_NSABE, VERIFICA_RINS,
                RINS_INSULFICIENCIA, RINS_OUTROS, RINS_NSABE, DOENCA_RESPIRATORIA, RESP_ASMA, RESP_ENFISEMA,
                RESP_OUTRO, RESP_NSABE, FUMANTE, ALCOOL, DROGAS, HIPERTENSO, DIABETES, AVC_DERRAME, INFARTO,
                HANSENIASE, TUBERCULOSE, CANCER, TRATAMENTO_PSIQ, ACAMADO, DOMICILIADO, PRATICAS_COMPLEM,
                OUTRAS_CONDIC_01, OUTRAS_CONDIC_02, OUTRAS_CONDIC_03, TEMPO_SITUACAO_RUA, VEZES_ALIMENTA,
                OUTRA_INSTITUICAO, DESC_INSTITUICAO, GRAU_PARENTESCO, VISITA_FAMILIAR, SIT_RUA_BENEFICIO,
                SIT_RUA_FAMILIAR, BANHO, HIGIENE_BUCAL, ACESSO_SANIT, HIGIENE_OUTROS, DOAC_RESTAURANTE,
                RESTAURANTE_POPU, DOAC_GRUP_RELIG, DOACAO_POPULAR, DOACAO_OUTROS
            )
            VALUES (
                @id_usuario, @csi_codpac, @csi_nompac, @nome_social, @csi_sexpac, @csi_corpac, @etnia, @csi_dtnasc, @emial, @csi_celular,
                @csi_maepac, @csi_paipac, @nacionalidade, @csi_codnat, @csi_naturalidade_data, @csi_naturalidade_portaria,
                @csi_id_nacionalidade, @csi_data_entrada_pais, @csi_sanguegrupo, @csi_sanguefator, @csi_codhemoes,
                @csi_dsangue, @verif_situacao_rua, @csi_cpfpac, @csi_ncartao, @csi_pispac, @csi_idepac, @csi_orgide,
                @csi_comide, @csi_expide, @csi_estide, @csi_dt_primeira_cnh, @csi_tipcer, @numero_dnv, @csi_numcer,
                @csi_folivr, @csi_livcer, @csi_certidao_termo, @csi_emicer, @csi_nova_certidao, @csi_carcer, @csi_cidcerc,
                @csi_titele, @csi_zontit, @csi_sectit, @csi_ctpsas, @csi_ctps_serie, @csi_ctps_dtemis, @csi_ctps_uf,
                @csi_acerres, @csi_acarvac, @csi_acomres, @csi_ncerres, @num_processo_estado, @csi_cre, @csi_codend,
                @csi_endpac, @csi_numero_logradouro, @complemento, @csi_baipac, @csi_ceppac, @csi_codcid,
                @id_estabelecimento_saude, @cod_ageesus, @fora_area,
                @csi_estciv, @csi_situacao_familiar, @csi_codgrau, @csi_codpro, @csi_escpac, @sit_mercado_trab,
                @comunidade_tradic, @desc_comunidade, @esus_verifica_ident_genero, @esus_ident_genero, @verifica_ident_sex,
                @orientacao_sexual, @esus_crianca_adulto, @esus_crianca_outra_crianca, @esus_crianca_outro,
                @esus_crianca_adolescente, @esus_crianca_sozinha, @esus_crianca_creche, @verifica_deficiencia,
                @def_auditiva, @def_intelectual, @def_visual, @def_fisica, @def_outra, @csi_estudando, @freq_curandeiro,
                @grupo_comunitario, @possui_plano_saude, @situacao_peso, @internacao, @internacao_causa, @plantas_medicinais,
                @quais_plantas, @verifica_cardiaca, @insulf_cardiaca, @cardiaca_outro, @cardiaca_nsabe, @verifica_rins,
                @rins_insulficiencia, @rins_outros, @rins_nsabe, @doenca_respiratoria, @resp_asma, @resp_enfisema,
                @resp_outro, @resp_nsabe, @fumante, @alcool, @drogas, @hipertenso, @diabetes, @avc_derrame, @infarto,
                @hanseniase, @tuberculose, @cancer, @tratamento_psiq, @acamado, @domiciliado, @praticas_complem,
                @outras_condic_01, @outras_condic_02, @outras_condic_03, @tempo_situacao_rua, @vezes_alimenta,
                @outra_instituicao, @desc_instituicao, @grau_parentesco, @visita_familiar, @sit_rua_beneficio,
                @sit_rua_familiar, @banho, @higiene_bucal, @acesso_sanit, @higiene_outros, @doac_restaurante,
                @restaurante_popu, @doac_grup_relig, @doacao_popular, @doacao_outros
        )";

        string IIndividuoCommand.Insert { get => sqlInsert; }

        public string sqlUpdate = $@"UPDATE TSI_CADPAC SET 
                ID_USUARIO = @id_usuario,
                CSI_NOMPAC = @csi_nompac, 
                NOME_SOCIAL = @nome_social, 
                CSI_SEXPAC = @csi_sexpac, 
                CSI_CORPAC = @csi_corpac, 
                ETNIA = @etnia, 
                CSI_DTNASC = @csi_dtnasc, 
                EMIAL = @emial, 
                CSI_CELULAR = @csi_celular,
                CSI_MAEPAC = @csi_maepac, 
                CSI_PAIPAC = @csi_paipac, 
                NACIONALIDADE = @nacionalidade, 
                CSI_CODNAT = @csi_codnat, 
                CSI_NATURALIDADE_DATA = @csi_naturalidade_data, 
                CSI_NATURALIDADE_PORTARIA = @csi_naturalidade_portaria,
                CSI_ID_NACIONALIDADE = @csi_id_nacionalidade, 
                CSI_DATA_ENTRADA_PAIS = @csi_data_entrada_pais, 
                CSI_SANGUEGRUPO = @csi_sanguegrupo, 
                CSI_SANGUEFATOR = @csi_sanguefator, 
                CSI_CODHEMOES = @csi_codhemoes,
                CSI_DSANGUE = @csi_dsangue, 
                VERIF_SITUACAO_RUA = @verif_situacao_rua, 
                CSI_CPFPAC = @csi_cpfpac, 
                CSI_NCARTAO = @csi_ncartao, 
                CSI_PISPAC = @csi_pispac, 
                CSI_IDEPAC = @csi_idepac, 
                CSI_ORGIDE = @csi_orgide,
                CSI_COMIDE = @csi_comide, 
                CSI_EXPIDE = @csi_expide, 
                CSI_ESTIDE = @csi_estide, 
                CSI_DT_PRIMEIRA_CNH = @csi_dt_primeira_cnh, 
                CSI_TIPCER = @csi_tipcer, 
                NUMERO_DNV = @numero_dnv, 
                CSI_NUMCER = @csi_numcer,
                CSI_FOLIVR = @csi_folivr, 
                CSI_LIVCER = @csi_livcer, 
                CSI_CERTIDAO_TERMO = @csi_certidao_termo, 
                CSI_EMICER = @csi_emicer, 
                CSI_NOVA_CERTIDAO = @csi_nova_certidao, 
                CSI_CARCER = @csi_carcer, 
                CSI_CIDCERC = @csi_cidcerc,
                CSI_TITELE = @csi_titele, 
                CSI_ZONTIT = @csi_zontit, 
                CSI_SECTIT = @csi_sectit, 
                CSI_CTPSAS = @csi_ctpsas, 
                CSI_CTPS_SERIE = @csi_ctps_serie, 
                CSI_CTPS_DTEMIS = @csi_ctps_dtemis, 
                CSI_CTPS_UF = @csi_ctps_uf,
                CSI_ACERRES = @csi_acerres, 
                CSI_ACARVAC = @csi_acarvac, 
                CSI_ACOMRES = @csi_acomres, 
                CSI_NCERRES = @csi_ncerres, 
                NUM_PROCESSO_ESTADO = @num_processo_estado, 
                CSI_CRE = @csi_cre, 
                CSI_CODEND = @csi_codend,
                CSI_ENDPAC = @csi_endpac, 
                CSI_NUMERO_LOGRADOURO = @csi_numero_logradouro, 
                COMPLEMENTO = @complemento, 
                CSI_BAIPAC = @csi_baipac, 
                CSI_CEPPAC = @csi_ceppac, 
                CSI_CODCID = @csi_codcid, 
                ID_ESTABELECIMENTO_SAUDE = @id_estabelecimento_saude,
                COD_AGEESUS = @cod_ageesus,
                FORA_AREA = @fora_area,
                CSI_ESTCIV = @csi_estciv, 
                CSI_SITUACAO_FAMILIAR = @csi_situacao_familiar, 
                CSI_CODGRAU = @csi_codgrau, 
                CSI_CODPRO = @csi_codpro, 
                CSI_ESCPAC = @csi_escpac, 
                SIT_MERCADO_TRAB = @sit_mercado_trab,
                COMUNIDADE_TRADIC = @comunidade_tradic, 
                DESC_COMUNIDADE = @desc_comunidade, 
                ESUS_VERIFICA_IDENT_GENERO = @esus_verifica_ident_genero, 
                ESUS_IDENT_GENERO = @esus_ident_genero, 
                VERIFICA_IDENT_SEX = @verifica_ident_sex,
                ORIENTACAO_SEXUAL = @orientacao_sexual, 
                ESUS_CRIANCA_ADULTO = @esus_crianca_adulto, 
                ESUS_CRIANCA_OUTRA_CRIANCA = @esus_crianca_outra_crianca, 
                ESUS_CRIANCA_OUTRO = @esus_crianca_outro,
                ESUS_CRIANCA_ADOLESCENTE = @esus_crianca_adolescente, 
                ESUS_CRIANCA_SOZINHA = @esus_crianca_sozinha, 
                ESUS_CRIANCA_CRECHE = @esus_crianca_creche, 
                VERIFICA_DEFICIENCIA = @verifica_deficiencia,
                DEF_AUDITIVA = @def_auditiva, 
                DEF_INTELECTUAL = @def_intelectual, 
                DEF_VISUAL = @def_visual, 
                DEF_FISICA = @def_fisica, 
                DEF_OUTRA = @def_outra, 
                CSI_ESTUDANDO = @csi_estudando, 
                FREQ_CURANDEIRO = @freq_curandeiro,
                GRUPO_COMUNITARIO = @grupo_comunitario, 
                POSSUI_PLANO_SAUDE = @possui_plano_saude, 
                SITUACAO_PESO = @situacao_peso, 
                INTERNACAO = @internacao, 
                INTERNACAO_CAUSA = @internacao_causa, 
                PLANTAS_MEDICINAIS = @plantas_medicinais,
                QUAIS_PLANTAS = @quais_plantas, 
                VERIFICA_CARDIACA = @verifica_cardiaca, 
                INSULF_CARDIACA = @insulf_cardiaca, 
                CARDIACA_OUTRO = @cardiaca_outro, 
                CARDIACA_NSABE = @cardiaca_nsabe, 
                VERIFICA_RINS = @verifica_rins,
                RINS_INSULFICIENCIA = @rins_insulficiencia, 
                RINS_OUTROS = @rins_outros, 
                RINS_NSABE = @rins_nsabe, 
                DOENCA_RESPIRATORIA = @doenca_respiratoria, 
                RESP_ASMA = @resp_asma, 
                RESP_ENFISEMA = @resp_enfisema,
                RESP_OUTRO = @resp_outro, 
                RESP_NSABE = @resp_nsabe, 
                FUMANTE = @fumante, 
                ALCOOL = @alcool, 
                DROGAS = @drogas, 
                HIPERTENSO = @hipertenso, 
                DIABETES = @diabetes, 
                AVC_DERRAME = @avc_derrame, 
                INFARTO = @infarto,
                HANSENIASE = @hanseniase, 
                TUBERCULOSE = @tuberculose, 
                CANCER = @cancer, 
                TRATAMENTO_PSIQ = @tratamento_psiq, 
                ACAMADO = @acamado, 
                DOMICILIADO = @domiciliado, 
                PRATICAS_COMPLEM = @praticas_complem,
                OUTRAS_CONDIC_01 = @outras_condic_01, 
                OUTRAS_CONDIC_02 = @outras_condic_02, 
                OUTRAS_CONDIC_03 = @outras_condic_03, 
                TEMPO_SITUACAO_RUA = @tempo_situacao_rua, 
                VEZES_ALIMENTA = @vezes_alimenta,
                OUTRA_INSTITUICAO = @outra_instituicao, 
                DESC_INSTITUICAO = @desc_instituicao, 
                GRAU_PARENTESCO = @grau_parentesco, 
                VISITA_FAMILIAR = @visita_familiar, 
                SIT_RUA_BENEFICIO = @sit_rua_beneficio,
                SIT_RUA_FAMILIAR = @sit_rua_familiar, 
                BANHO = @banho, 
                HIGIENE_BUCAL = @higiene_bucal, 
                ACESSO_SANIT = @acesso_sanit, 
                HIGIENE_OUTROS = @higiene_outros, 
                DOAC_RESTAURANTE = @doac_restaurante,
                RESTAURANTE_POPU = @restaurante_popu, 
                DOAC_GRUP_RELIG = @doac_grup_relig, 
                DOACAO_POPULAR = @doacao_popular, 
                DOACAO_OUTROS = @doacao_outros
            WHERE CSI_CODPAC = @csi_codpac";

        string IIndividuoCommand.Update { get => sqlUpdate; }

        public string sqlGetById = $@"SELECT 
                ID_USUARIO, CSI_CODPAC, CSI_NOMPAC, NOME_SOCIAL, CSI_SEXPAC, CSI_CORPAC, ETNIA, CSI_DTNASC, EMIAL, CSI_CELULAR,
                CSI_MAEPAC, CSI_PAIPAC, NACIONALIDADE, CSI_CODNAT, CSI_NATURALIDADE_DATA, CSI_NATURALIDADE_PORTARIA,
                CSI_ID_NACIONALIDADE, CSI_DATA_ENTRADA_PAIS, CSI_SANGUEGRUPO, CSI_SANGUEFATOR, CSI_CODHEMOES,
                CSI_DSANGUE, VERIF_SITUACAO_RUA, CSI_CPFPAC, CSI_NCARTAO, CSI_PISPAC, CSI_IDEPAC, CSI_ORGIDE,
                CSI_COMIDE, CSI_EXPIDE, CSI_ESTIDE, CSI_DT_PRIMEIRA_CNH, CSI_TIPCER, NUMERO_DNV, CSI_NUMCER,
                CSI_FOLIVR, CSI_LIVCER, CSI_CERTIDAO_TERMO, CSI_EMICER, CSI_NOVA_CERTIDAO, CSI_CARCER, CSI_CIDCERC,
                CSI_TITELE, CSI_ZONTIT, CSI_SECTIT, CSI_CTPSAS, CSI_CTPS_SERIE, CSI_CTPS_DTEMIS, CSI_CTPS_UF,
                CSI_ACERRES, CSI_ACARVAC, CSI_ACOMRES, CSI_NCERRES, NUM_PROCESSO_ESTADO, CSI_CRE, CSI_CODEND,
                CSI_ENDPAC, CSI_NUMERO_LOGRADOURO, COMPLEMENTO, CSI_BAIPAC, CSI_CEPPAC, CSI_CODCID,
                ID_ESTABELECIMENTO_SAUDE, COD_AGEESUS, FORA_AREA,
                CSI_ESTCIV, CSI_SITUACAO_FAMILIAR, CSI_CODGRAU, CSI_CODPRO, CSI_ESCPAC, SIT_MERCADO_TRAB,
                COMUNIDADE_TRADIC, DESC_COMUNIDADE, ESUS_VERIFICA_IDENT_GENERO, ESUS_IDENT_GENERO, VERIFICA_IDENT_SEX,
                ORIENTACAO_SEXUAL, ESUS_CRIANCA_ADULTO, ESUS_CRIANCA_OUTRA_CRIANCA, ESUS_CRIANCA_OUTRO,
                ESUS_CRIANCA_ADOLESCENTE, ESUS_CRIANCA_SOZINHA, ESUS_CRIANCA_CRECHE, VERIFICA_DEFICIENCIA,
                DEF_AUDITIVA, DEF_INTELECTUAL, DEF_VISUAL, DEF_FISICA, DEF_OUTRA, CSI_ESTUDANDO, FREQ_CURANDEIRO,
                GRUPO_COMUNITARIO, POSSUI_PLANO_SAUDE, SITUACAO_PESO, INTERNACAO, INTERNACAO_CAUSA, PLANTAS_MEDICINAIS,
                QUAIS_PLANTAS, VERIFICA_CARDIACA, INSULF_CARDIACA, CARDIACA_OUTRO, CARDIACA_NSABE, VERIFICA_RINS,
                RINS_INSULFICIENCIA, RINS_OUTROS, RINS_NSABE, DOENCA_RESPIRATORIA, RESP_ASMA, RESP_ENFISEMA,
                RESP_OUTRO, RESP_NSABE, FUMANTE, ALCOOL, DROGAS, HIPERTENSO, DIABETES, AVC_DERRAME, INFARTO,
                HANSENIASE, TUBERCULOSE, CANCER, TRATAMENTO_PSIQ, ACAMADO, DOMICILIADO, PRATICAS_COMPLEM,
                OUTRAS_CONDIC_01, OUTRAS_CONDIC_02, OUTRAS_CONDIC_03, TEMPO_SITUACAO_RUA, VEZES_ALIMENTA,
                OUTRA_INSTITUICAO, DESC_INSTITUICAO, GRAU_PARENTESCO, VISITA_FAMILIAR, SIT_RUA_BENEFICIO,
                SIT_RUA_FAMILIAR, BANHO, HIGIENE_BUCAL, ACESSO_SANIT, HIGIENE_OUTROS, DOAC_RESTAURANTE,
                RESTAURANTE_POPU, DOAC_GRUP_RELIG, DOACAO_POPULAR, DOACAO_OUTROS, ID_FAMILIA
            FROM TSI_CADPAC
            WHERE CSI_CODPAC = @csi_codpac";

        string IIndividuoCommand.GetById{ get => sqlGetById; }

        public string sqlGetUltimoLog = $@"SELECT FIRST(1) USU.LOGIN, USU.NOME,
                                            L.DATA, L.TIPO
                                        FROM LOG_EVENTOS L
                                        LEFT JOIN SEG_USUARIO USU ON (L.USUARIO = USU.ID)
                                        WHERE L.TABELA = 'TSI_CADPAC'
                                        AND L.CODIGO = @id_individuo 
                                        ORDER BY L.DATA DESC;";

        string IIndividuoCommand.GetUltimoLog{ get => sqlGetUltimoLog; }

        public string sqlCheckIndividuoExiste = $@"SELECT
                                                PAC.CSI_CODPAC, PAC.CSI_NOMPAC, PAC.CSI_CPFPAC,
                                                PAC.CSI_NCARTAO, PAC.CSI_DTNASC
                                                FROM TSI_CADPAC PAC
                                                @filtro";
        string IIndividuoCommand.CheckIndividuoExiste { get => sqlCheckIndividuoExiste; }

        public string sqlCheckIndividuoMesmoNome = $@"SELECT
                                                PAC.CSI_CODPAC, PAC.CSI_NOMPAC, PAC.CSI_CPFPAC,
                                                PAC.CSI_NCARTAO, PAC.CSI_DTNASC
                                                FROM TSI_CADPAC PAC
                                                @filtro";
        string IIndividuoCommand.CheckIndividuoMesmoNome { get => sqlCheckIndividuoMesmoNome; }

        public string sqlCheckVinculoMicroarea = $@"SELECT  CP.CSI_CODPAC, CP.CSI_NOMPAC,
                                            FAM.ID ID_FAMILIA, EST.ID ID_DOMICILIO, EST.ID_MICROAREA
                                            FROM TSI_CADPAC CP
                                            @sql_estrutura
                                            WHERE CP.CSI_CODPAC = @csi_codpac
                                            AND EST.ID_MICROAREA IS NOT NULL";
        string IIndividuoCommand.CheckVinculoMicroarea { get => sqlCheckVinculoMicroarea; }
    }
}
