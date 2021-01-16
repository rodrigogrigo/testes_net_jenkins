using RgCidadao.Domain.Commands.Cadastro;

namespace RgCidadao.Domain.Queries.Cadastro
{
    public class CidadaoCommandText : ICidadaoCommand
    {
        public string sqlGetAll = $@"SELECT CSI_CODPAC, CSI_NOMPAC
                                     FROM TSI_CADPAC  
                                     @filtro
                                     ORDER BY CSI_NOMPAC";

        string ICidadaoCommand.GetAll { get => sqlGetAll; }

        public string GetCountAll = $@"SELECT count(*)
                                       FROM TSI_CADPAC C  
                                       @filtro";
        string ICidadaoCommand.GetCountAll { get => GetCountAll; }

        public string sqlGetAllPaginationWithFamilia = $@"SELECT FIRST (@pagesize) SKIP (@page) CP.CSI_CODPAC ID_INDIVIDUO,
                                                              CP.CSI_NOMPAC INDIVIDUO,
                                                              CP.CSI_SEXPAC SEXO,
                                                              (SELECT * FROM PRO_CALCULA_IDADE(CP.CSI_DTNASC, CURRENT_DATE)) IDADE,
                                                              CP.CSI_DTNASC DATA_NASCIMENTO,
                                                              FAM.ID ID_FAMILIA,
                                                              FAM.NUM_PRONTUARIO_FAMILIAR,
                                                              EST.ID ID_DOMICILIO,
                                                              EST.ID_PROFISSIONAL,
                                                              EST.ID_MICROAREA ,
                                                              CASE WHEN (CP.CSI_CODPAC = FAM.ID_RESPONSAVEL) THEN 1 ELSE 0 END AS RESPONSAVEL,
                                                              USU.USA_TABLET,
                                                              MED.CSI_NOMMED NOME_PROFISSIONAL
                                                          FROM TSI_CADPAC CP
                                                          LEFT JOIN ESUS_FAMILIA FAM ON (CP.ID_FAMILIA = FAM.ID)
                                                          LEFT JOIN VS_ESTABELECIMENTOS EST ON (FAM.ID_DOMICILIO = EST.ID)
                                                          LEFT JOIN TSI_MEDICOS MED ON (EST.ID_PROFISSIONAL = MED.CSI_CODMED)
                                                          LEFT JOIN SEG_USUARIO USU ON (MED.CSI_IDUSER = USU.ID)
                                                          @filtro
                                                          ORDER BY CP.CSI_NOMPAC";
        string ICidadaoCommand.GetAllPaginationWithFamilia { get => sqlGetAllPaginationWithFamilia; }

        public string sqlGetCountAllWithFamilia = $@"SELECT COUNT(*)
                                                     FROM (SELECT CP.CSI_CODPAC ID_INDIVIDUO, CP.CSI_NOMPAC INDIVIDUO, CP.CSI_SEXPAC SEXO,
                                                                  (SELECT *
                                                                   FROM PRO_CALCULA_IDADE(CP.CSI_DTNASC, CURRENT_DATE)) IDADE, CP.CSI_DTNASC DATA_NASCIMENTO,
                                                                  FAM.ID ID_FAMILIA, EST.ID ID_DOMICILIO, EST.ID_PROFISSIONAL, EST.ID_MICROAREA,
                                                                  CASE
                                                                    WHEN (CP.CSI_CODPAC = FAM.ID_RESPONSAVEL) THEN 1
                                                                    ELSE 0
                                                                  END AS RESPONSAVEL
                                                           FROM TSI_CADPAC CP
                                                           LEFT JOIN ESUS_FAMILIA FAM ON (CP.ID_FAMILIA = FAM.ID)
                                                           LEFT JOIN VS_ESTABELECIMENTOS EST ON (FAM.ID_DOMICILIO = EST.ID)
                                                           @filtro
                                                           ORDER BY CP.CSI_NOMPAC)";
        string ICidadaoCommand.GetCountAllWithFamilia { get => sqlGetCountAllWithFamilia; }

        public string GetAllPagination = $@"SELECT FIRST(@pagesize) SKIP(@page) C.CSI_CODPAC, C.CSI_NOMPAC, C.CSI_SEXPAC, C.CSI_DTNASC,
                                                   C.CSI_CELULAR, C.CSI_NCARTAO, C.CSI_CPFPAC, C.CSI_MAEPAC,
                                                   C.CSI_PAIPAC, MED.CSI_NOMMED ACS, E.DESCRICAO EQUIPE, C.CSI_SITUACAO,C.CSI_PESO,C.CSI_ALTURA,
                                                   NUM_PRONTUARIO_FAMILIAR AS cc_numero_prontuario 
                                            FROM TSI_CADPAC C
                                            @sql_estrutura
                                            LEFT JOIN TSI_MEDICOS MED ON D.ID_PROFISSIONAL = MED.CSI_CODMED
                                            LEFT JOIN ESUS_MICROAREA M ON D.ID_MICROAREA = M.ID
                                            LEFT JOIN ESUS_EQUIPES E ON M.ID_EQUIPE = E.ID
                                            @filtro
                                            ORDER BY C.CSI_NOMPAC";
        string ICidadaoCommand.GetAllPagination { get => GetAllPagination; }

        public string SqlGetNewId = $@"SELECT GEN_ID(GEN_TSI_CADPAC_ID, 1) AS VLR FROM RDB$DATABASE";
        string ICidadaoCommand.GetNewId { get => SqlGetNewId; }

        public string sqlExisteCidadao = $@"SELECT PAC.CSI_CODPAC, PAC.CSI_NOMPAC, PAC.CSI_CPFPAC, PAC.CSI_NCARTAO, PAC.CSI_DTNASC
                                            FROM TSI_CADPAC PAC
                                                 @filtro";
        string ICidadaoCommand.ValidaExistenciaCidadao { get => sqlExisteCidadao; }

        public string SqlInsert = $@"INSERT INTO TSI_CADPAC (CSI_CODPAC,CSI_NOMPAC,CSI_SEXPAC,CSI_CORPAC,CSI_DTNASC,CSI_CELULAR,NACIONALIDADE,
                                                            CSI_CODNAT,CSI_NCARTAO,CSI_CPFPAC,CSI_IDEPAC,CSI_ORGIDE,CSI_ESTIDE,CSI_PISPAC,EMIAL,CSI_MAEPAC,
                                                            CSI_PAIPAC,CSI_ID_NACIONALIDADE,CSI_NATURALIDADE_DATA,CSI_NATURALIDADE_PORTARIA, EXCLUIDO,
                                                            CSI_CODEND, CSI_ENDPAC,COMPLEMENTO , CSI_NUMERO_LOGRADOURO,
                                                            CSI_BAIPAC,CSI_CEPPAC,CSI_CODCID,CSI_COD_EQUIPE ,CSI_CODAGE,FORA_AREA,CSI_SITUACAO )
                                     VALUES (@csi_codpac,@csi_nompac,@csi_sexpac,@csi_corpac,@csi_dtnasc,@csi_celular,@nacionalidade,
                                             @csi_codnat,@csi_ncartao,@csi_cpfpac,@csi_idepac,@csi_orgide,@csi_estide,@csi_pispac,@emial,@csi_maepac,
                                             @csi_paipac,@csi_id_nacionalidade,@csi_naturalidade_data,@csi_naturalidade_portaria, 'F',
                                             @csi_codend, @csi_endpac,@complemento , @csi_numero_logradouro,
                                             @csi_baipac,@csi_ceppac,@csi_codcid,@csi_cod_equipe, @csi_codage, @fora_area, @csi_situacao)";
        string ICidadaoCommand.Insert { get => SqlInsert; }

        public string SqlUpdate = $@"UPDATE TSI_CADPAC
                                     SET CSI_NOMPAC = @csi_nompac,
                                         CSI_SEXPAC = @csi_sexpac,
                                         CSI_CORPAC = @csi_corpac,
                                         CSI_DTNASC = @csi_dtnasc,
                                         CSI_CELULAR = @csi_celular,
                                         NACIONALIDADE = @nacionalidade,
                                         CSI_CODNAT = @csi_codnat,
                                         CSI_NCARTAO = @csi_ncartao,
                                         CSI_CPFPAC = @csi_cpfpac,
                                         CSI_IDEPAC = @csi_idepac,
                                         CSI_ORGIDE = @csi_orgide,
                                         CSI_ESTIDE = @csi_estide,
                                         CSI_PISPAC = @csi_pispac,
                                         EMIAL = @emial,
                                         CSI_MAEPAC = @csi_maepac,
                                         CSI_PAIPAC = @csi_paipac,
                                         CSI_ID_NACIONALIDADE = @csi_id_nacionalidade,
                                         CSI_NATURALIDADE_DATA = @csi_naturalidade_data,
                                         CSI_NATURALIDADE_PORTARIA = @csi_naturalidade_portaria,
                                         EXCLUIDO = 'F',
                                         CSI_CODEND = @csi_codend,
                                         CSI_ENDPAC=@csi_endpac,
                                         COMPLEMENTO = @complemento,
                                         CSI_NUMERO_LOGRADOURO = @csi_numero_logradouro,
                                         CSI_BAIPAC = @csi_baipac,
                                         CSI_CEPPAC = @csi_ceppac,
                                         CSI_CODCID = @csi_codcid,
                                         CSI_COD_EQUIPE = @csi_cod_equipe,
                                         CSI_CODAGE = @csi_codage,
                                         FORA_AREA  = @fora_area,
                                         CSI_SITUACAO = @csi_situacao
                                     WHERE CSI_CODPAC = @csi_codpac";
        string ICidadaoCommand.Update { get => SqlUpdate; }

        public string SqlDelete = $@"UPDATE TSI_CADPAC
                                     SET EXCLUIDO = 'T'
                                     WHERE CSI_CODPAC = @csi_codpac";
        string ICidadaoCommand.Delete { get => SqlDelete; }

        public string SqlCidadaoById =
                            $@"SELECT
                                    CSI_CODPAC, CSI_NOMPAC, NOME_SOCIAL, CSI_SEXPAC, CSI_CORPAC, CSI_DTNASC, CSI_CELULAR, NACIONALIDADE, CSI_CODNAT, CSI_NCARTAO,
                                        CSI_CPFPAC, CSI_IDEPAC, CSI_ORGIDE, CSI_ESTIDE, CSI_PISPAC, EMIAL, CSI_MAEPAC, CSI_PAIPAC, CSI_ID_NACIONALIDADE,
                                        CSI_NATURALIDADE_DATA, CSI_NATURALIDADE_PORTARIA, CSI_CODGRAU, COD_AGEESUS, ESUS_CNS_RESPONSAVEL_DOMICILIO,
                                        ESUS_RESPONSAVEL_DOMICILIO, FORA_AREA, ETNIA, CSI_CODPRO, SIT_MERCADO_TRAB, CSI_ESCPAC,
                                        ESUS_CRIANCA_ADULTO, ESUS_CRIANCA_OUTRA_CRIANCA, ESUS_CRIANCA_ADOLESCENTE, ESUS_CRIANCA_SOZINHA,
                                        ESUS_CRIANCA_CRECHE, ESUS_CRIANCA_OUTRO, CSI_ESTUDANDO, FREQ_CURANDEIRO, POSSUI_PLANO_SAUDE, GRUPO_COMUNITARIO,
                                        COMUNIDADE_TRADIC, DESC_COMUNIDADE, VERIFICA_DEFICIENCIA, DEF_AUDITIVA, DEF_VISUAL, DEF_INTELECTUAL, DEF_FISICA,
                                        DEF_OUTRA, VERIFICA_IDENT_SEX, ORIENTACAO_SEXUAL, ESUS_VERIFICA_IDENT_GENERO, ESUS_IDENT_GENERO,
                                        ESUS_SAIDA_CIDADAO_CADASTRO, CSI_DATA_OBITO, ESUS_NUMERO_DO, VERIFICA_CARDIACA, INSULF_CARDIACA, CARDIACA_NSABE,
                                        CARDIACA_OUTRO, VERIFICA_RINS, RINS_INSULFICIENCIA, RINS_NSABE, RINS_OUTROS, DOENCA_RESPIRATORIA, RESP_ASMA,
                                        RESP_ENFISEMA, RESP_NSABE, RESP_OUTRO, INTERNACAO, INTERNACAO_CAUSA, PLANTAS_MEDICINAIS, QUAIS_PLANTAS,
                                        TRATAMENTO_PSIQ, SITUACAO_PESO, DOMICILIADO, ACAMADO, CANCER, FUMANTE, DROGAS, ALCOOL, DIABETES, AVC_DERRAME,
                                        HIPERTENSO, INFARTO, TUBERCULOSE, HANSENIASE, PRATICAS_COMPLEM, OUTRAS_CONDIC_01, VERIF_SITUACAO_RUA,
                                        TEMPO_SITUACAO_RUA, OUTRA_INSTITUICAO, DESC_INSTITUICAO, VISITA_FAMILIAR, GRAU_PARENTESCO, ACESSO_HIGIENTEP,
                                        BANHO, ACESSO_SANIT, HIGIENE_BUCAL, HIGIENE_OUTROS, SIT_RUA_BENEFICIO, SIT_RUA_FAMILIAR, VEZES_ALIMENTA,
                                        RESTAURANTE_POPU, DOAC_RESTAURANTE, DOAC_GRUP_RELIG, DOACAO_POPULAR, DOACAO_OUTROS, PAC.CSI_CODEND, LO.CSI_CODBAI, CSI_ENDPAC,PAC.COMPLEMENTO , CSI_NUMERO_LOGRADOURO,
                                        CSI_BAIPAC,CSI_CEPPAC,CSI_CODCID,CSI_COD_EQUIPE ,CSI_CODAGE,FORA_AREA, CSI_SITUACAO,
                                        D.NUM_PRONTUARIO_FAMILIAR AS CC_NUMERO_PRONTUARIO
                                        FROM TSI_CADPAC PAC
                                        @sql_estrutura
                                        LEFT JOIN TSI_LOGRADOURO LO ON (LO.CSI_CODEND = PAC.CSI_CODEND)
                                        WHERE CSI_CODPAC =  @csi_codpac";

        string ICidadaoCommand.GetCidadaoById { get => SqlCidadaoById; }

        public string sqlGetFotoByCidadao = $@"SELECT CSI_FOTO
                                               FROM TSI_FOTOS
                                               WHERE CSI_MATRICULA = @id";
        string ICidadaoCommand.GetFotoByCidadao { get => sqlGetFotoByCidadao; }

        public string sqlGetGrauParentesco = $@"SELECT CSI_CODGRA CODIGO, CSI_DESGRA DESCRICAO
                                                FROM TSI_GRAUP
                                                WHERE FLG_ESUS = 'T'";
        string ICidadaoCommand.GetGrauParentesco { get => sqlGetGrauParentesco; }

        public string sqlInsertCadIndividual = $@"INSERT INTO TSI_CADPAC (CSI_CODPAC, CSI_NOMPAC, NOME_SOCIAL, CSI_SEXPAC, CSI_CORPAC, CSI_DTNASC, CSI_CELULAR,
                                                                           NACIONALIDADE, CSI_CODNAT, CSI_NCARTAO, CSI_CPFPAC, CSI_IDEPAC, CSI_ORGIDE, CSI_ESTIDE,
                                                                           CSI_PISPAC, EMIAL, CSI_MAEPAC, CSI_PAIPAC, CSI_ID_NACIONALIDADE, CSI_NATURALIDADE_DATA,
                                                                           CSI_NATURALIDADE_PORTARIA, CSI_CODGRAU, COD_AGEESUS, ESUS_CNS_RESPONSAVEL_DOMICILIO,
                                                                           ESUS_RESPONSAVEL_DOMICILIO, FORA_AREA, ETNIA, CSI_CODPRO, SIT_MERCADO_TRAB,
                                                                           CSI_ESCPAC, ESUS_CRIANCA_ADULTO, ESUS_CRIANCA_OUTRA_CRIANCA, ESUS_CRIANCA_ADOLESCENTE,
                                                                           ESUS_CRIANCA_SOZINHA, ESUS_CRIANCA_CRECHE, ESUS_CRIANCA_OUTRO, CSI_ESTUDANDO, FREQ_CURANDEIRO,
                                                                           POSSUI_PLANO_SAUDE, GRUPO_COMUNITARIO, COMUNIDADE_TRADIC, DESC_COMUNIDADE, VERIFICA_DEFICIENCIA,
                                                                           DEF_AUDITIVA, DEF_VISUAL, DEF_INTELECTUAL, DEF_FISICA, DEF_OUTRA, VERIFICA_IDENT_SEX,
                                                                           ORIENTACAO_SEXUAL, ESUS_VERIFICA_IDENT_GENERO, ESUS_IDENT_GENERO, ESUS_SAIDA_CIDADAO_CADASTRO,
                                                                           CSI_DATA_OBITO, ESUS_NUMERO_DO, VERIFICA_CARDIACA, INSULF_CARDIACA, CARDIACA_NSABE,
                                                                           CARDIACA_OUTRO, VERIFICA_RINS, RINS_INSULFICIENCIA, RINS_NSABE, RINS_OUTROS,
                                                                           DOENCA_RESPIRATORIA, RESP_ASMA, RESP_ENFISEMA, RESP_NSABE, RESP_OUTRO, INTERNACAO,
                                                                           INTERNACAO_CAUSA, PLANTAS_MEDICINAIS, QUAIS_PLANTAS, TRATAMENTO_PSIQ, SITUACAO_PESO,
                                                                           DOMICILIADO, ACAMADO, CANCER, FUMANTE, DROGAS, ALCOOL, DIABETES, AVC_DERRAME, HIPERTENSO,
                                                                           INFARTO, TUBERCULOSE, HANSENIASE, PRATICAS_COMPLEM, OUTRAS_CONDIC_01, VERIF_SITUACAO_RUA,
                                                                           TEMPO_SITUACAO_RUA, OUTRA_INSTITUICAO, DESC_INSTITUICAO, VISITA_FAMILIAR, GRAU_PARENTESCO,
                                                                           ACESSO_HIGIENTEP, BANHO, ACESSO_SANIT, HIGIENE_BUCAL, HIGIENE_OUTROS, SIT_RUA_BENEFICIO,
                                                                           SIT_RUA_FAMILIAR, VEZES_ALIMENTA, RESTAURANTE_POPU, DOAC_RESTAURANTE, DOAC_GRUP_RELIG,
                                                                           DOACAO_POPULAR, EXCLUIDO, DOACAO_OUTROS, CSI_SITUACAO)
                                                   VALUES (@csi_codpac, @csi_nompac, @nome_social, @csi_sexpac, @csi_corpac, @csi_dtnasc, @csi_celular,
                                                           @nacionalidade, @csi_codnat, @csi_ncartao, @csi_cpfpac, @csi_idepac, @csi_orgide, @csi_estide,
                                                           @csi_pispac, @emial, @csi_maepac, @csi_paipac, @csi_id_nacionalidade, @csi_naturalidade_data,
                                                           @csi_naturalidade_portaria, @csi_codgrau, @cod_ageesus, @esus_cns_responsavel_domicilio,
                                                           @esus_responsavel_domicilio, @fora_area, @etnia, @csi_codpro, @sit_mercado_trab,
                                                           @csi_escpac, @esus_crianca_adulto, @esus_crianca_outra_crianca, @esus_crianca_adolescente,
                                                           @esus_crianca_sozinha, @esus_crianca_creche, @esus_crianca_outro, @csi_estudando, @freq_curandeiro,
                                                           @possui_plano_saude, @grupo_comunitario, @comunidade_tradic, @desc_comunidade, @verifica_deficiencia,
                                                           @def_auditiva, @def_visual, @def_intelectul, @def_fisica, @def_outra, @verifica_ident_sex,
                                                           @orientacao_sexual, @esus_verifica_ident_genero, @esus_ident_genero, @esus_saida_cidadao_cadastro,
                                                           @csi_data_obito, @esus_numero_do, @verifica_cardiaca, @insulf_cardiaca, @cardiaca_nsabe,
                                                           @cardiaca_outro, @verifica_rins, @rins_insulficiencia, @rins_nsabe, @rins_outros,
                                                           @doenca_respiratoria, @resp_asma, @resp_enfisema, @resp_nsabe, @resp_outro, @internacao,
                                                           @internacao_causa, @plantas_medicinais, @quais_plantas, @tratamento_psiq, @situacao_peso,
                                                           @domiciliado, @acamado, @cancer, @fumante, @drogas, @alcool, @diabetes, @avc_derrame, @hipertenso,
                                                           @infarto, @turberculose, @hanseniase, @praticas_complem, @outras_condic_01, @verif_situacao_rua,
                                                           @tempo_situacao_rua, @outra_instituicao, @desc_instituicao, @visita_familiar, @grau_parentesco,
                                                           @acesso_higientep, @banho, @acesso_sanit, @higiene_bucal, @higiene_outros, @sit_rua_beneficio,
                                                           @sit_rua_familiar, @vezes_alimenta, @restaurante_popu, @doac_restaurante, @doac_grp_relig,
                                                           @doacao_popular, 'F', @doacao_outros, @csi_situacao)";
        string ICidadaoCommand.InsertCadIndividual { get => sqlInsertCadIndividual; }



        public string sqlUpdateCadIndividual = $@"UPDATE TSI_CADPAC
                                                  SET CSI_NOMPAC =  @csi_nompac,
                                                      NOME_SOCIAL = @nome_social,
                                                      CSI_SEXPAC = @csi_sexpac,
                                                      CSI_CORPAC = @csi_corpac,
                                                      CSI_DTNASC = @csi_dtnasc,
                                                      CSI_CELULAR = @csi_celular,
                                                      NACIONALIDADE = @nacionalidade,
                                                      CSI_CODNAT = @csi_codnat,
                                                      CSI_NCARTAO = @csi_ncartao,
                                                      CSI_CPFPAC = @csi_cpfpac,
                                                      CSI_IDEPAC = @csi_idepac,
                                                      CSI_ORGIDE = @csi_orgide,
                                                      CSI_ESTIDE = @csi_estide,
                                                      CSI_PISPAC = @csi_pispac,
                                                      EMIAL = @emial,
                                                      CSI_MAEPAC = @csi_maepac,
                                                      CSI_PAIPAC = @csi_paipac,
                                                      CSI_ID_NACIONALIDADE = @csi_id_nacionalidade,
                                                      CSI_NATURALIDADE_DATA = @csi_naturalidade_data,
                                                      CSI_NATURALIDADE_PORTARIA = @csi_naturalidade_portaria,
                                                      CSI_CODGRAU = @csi_codgrau,
                                                      COD_AGEESUS = @cod_ageesus,
                                                      ESUS_CNS_RESPONSAVEL_DOMICILIO = @esus_cns_responsavel_domicilio,
                                                      ESUS_RESPONSAVEL_DOMICILIO = @esus_responsavel_domicilio,
                                                      FORA_AREA = @fora_area,
                                                      ETNIA = @etnia,
                                                      CSI_CODPRO = @csi_codpro,
                                                      SIT_MERCADO_TRAB = @sit_mercado_trab,
                                                      CSI_ESCPAC = @csi_escpac,
                                                      ESUS_CRIANCA_ADULTO = @esus_crianca_adulto,
                                                      ESUS_CRIANCA_OUTRA_CRIANCA = @esus_crianca_outra_crianca,
                                                      ESUS_CRIANCA_ADOLESCENTE = @esus_crianca_adolescente,
                                                      ESUS_CRIANCA_SOZINHA = @esus_crianca_sozinha,
                                                      ESUS_CRIANCA_CRECHE = @esus_crianca_creche,
                                                      ESUS_CRIANCA_OUTRO = @esus_crianca_outro,
                                                      CSI_ESTUDANDO = @csi_estudando,
                                                      FREQ_CURANDEIRO = @freq_curandeiro,
                                                      POSSUI_PLANO_SAUDE = @possui_plano_saude,
                                                      GRUPO_COMUNITARIO = @grupo_comunitario,
                                                      COMUNIDADE_TRADIC = @comunidade_tradic,
                                                      DESC_COMUNIDADE = @desc_comunidade,
                                                      VERIFICA_DEFICIENCIA = @verifica_deficiencia,
                                                      DEF_AUDITIVA = @def_auditiva,
                                                      DEF_VISUAL = @def_visual,
                                                      DEF_INTELECTUAL = @def_intelectual,
                                                      DEF_FISICA = @def_fisica,
                                                      DEF_OUTRA = @def_outra,
                                                      VERIFICA_IDENT_SEX = @verifica_ident_sex,
                                                      ORIENTACAO_SEXUAL= @orientacao_sexual,
                                                      ESUS_VERIFICA_IDENT_GENERO = @esus_verifica_ident_genero,
                                                      ESUS_IDENT_GENERO = @esus_ident_genero,
                                                      ESUS_SAIDA_CIDADAO_CADASTRO = @esus_saida_cidadao_cadastro,
                                                      CSI_DATA_OBITO = @csi_data_obito,
                                                      ESUS_NUMERO_DO = @esus_numero_do,
                                                      VERIFICA_CARDIACA = @verifica_cardiaca,
                                                      INSULF_CARDIACA = @insulf_cardiaca,
                                                      CARDIACA_NSABE = @cardiaca_nsabe,
                                                      CARDIACA_OUTRO = @cardiaca_outro,
                                                      VERIFICA_RINS = @verifica_rins,
                                                      RINS_INSULFICIENCIA = @rins_insulficiencia,
                                                      RINS_NSABE = @rins_nsabe,
                                                      RINS_OUTROS = @rins_outros,
                                                      DOENCA_RESPIRATORIA = @doenca_respiratoria,
                                                      RESP_ASMA = @resp_asma,
                                                      RESP_ENFISEMA = @resp_enfisema,
                                                      RESP_NSABE = @resp_nsabe,
                                                      RESP_OUTRO = @resp_outro,
                                                      INTERNACAO = @internacao,
                                                      INTERNACAO_CAUSA = @internacao_causa,
                                                      PLANTAS_MEDICINAIS = @plantas_medicinais,
                                                      QUAIS_PLANTAS = @quais_plantas,
                                                      TRATAMENTO_PSIQ = @tratamento_psiq,
                                                      SITUACAO_PESO = @situacao_peso,
                                                      DOMICILIADO = @domiciliado,
                                                      ACAMADO = @acamado,
                                                      CANCER = @cancer,
                                                      FUMANTE = @fumante,
                                                      DROGAS = @drogas,
                                                      ALCOOL = @alcool,
                                                      DIABETES = @diabetes,
                                                      AVC_DERRAME = @avc_derrame,
                                                      HIPERTENSO = @hipertenso,
                                                      INFARTO = @infarto,
                                                      TUBERCULOSE = @tuberculose ,
                                                      HANSENIASE = @hanseniase,
                                                      PRATICAS_COMPLEM = @praticas_complem,
                                                      OUTRAS_CONDIC_01 = @outras_condic_01,
                                                      VERIF_SITUACAO_RUA = @verif_situacao_rua,
                                                      TEMPO_SITUACAO_RUA = @tempo_situacao_rua,
                                                      OUTRA_INSTITUICAO = @outra_instituicao,
                                                      DESC_INSTITUICAO = @desc_instituicao,
                                                      VISITA_FAMILIAR = @visita_familiar,
                                                      GRAU_PARENTESCO = @grau_parentesco,
                                                      ACESSO_HIGIENTEP = @acesso_higientep,
                                                      BANHO = @banho,
                                                      ACESSO_SANIT = @acesso_sanit,
                                                      HIGIENE_BUCAL = @higiene_bucal,
                                                      HIGIENE_OUTROS = @higiene_outros,
                                                      SIT_RUA_BENEFICIO = @sit_rua_beneficio,
                                                      SIT_RUA_FAMILIAR = @sit_rua_familiar,
                                                      VEZES_ALIMENTA = @vezes_alimenta,
                                                      RESTAURANTE_POPU = @restaurante_popu,
                                                      DOAC_RESTAURANTE = @doac_restaurante,
                                                      DOAC_GRUP_RELIG = @doac_grup_relig,
                                                      DOACAO_POPULAR = @doacao_popular,
                                                      DOACAO_OUTROS =  @doacao_outros,  
                                                      CSI_SITUACAO = @csi_situacao
                                                  WHERE CSI_CODPAC = @csi_codpac";
        string ICidadaoCommand.UpdateCadIndividual { get => sqlUpdateCadIndividual; }

        public string sqlLoginCadSus = $@"SELECT LOGIN_WEBSERVICE_CADSUS LOGIN, SENHA_WEBSERVICE_CADSUS SENHA
                                          FROM TSI_PARAMETROS";
        string ICidadaoCommand.GetLoginCadSus { get => sqlLoginCadSus; }

        public string sqlGetIndividuosToFamilia = $@"SELECT FIRST(@pagesize) SKIP(@page)
                                                         CP.CSI_CODPAC ID_INDIVIDUO,
                                                         CP.CSI_NOMPAC INDIVIDUO,
                                                         CP.CSI_SEXPAC SEXO,
                                                         (SELECT * FROM PRO_CALCULA_IDADE(CP.CSI_DTNASC, CURRENT_DATE)) IDADE,
                                                         CP.CSI_DTNASC DATA_NASCIMENTO,
                                                         FAM.ID ID_FAMILIA,
                                                         FAM.NUM_PRONTUARIO_FAMILIAR,
                                                         EST.ID ID_DOMICILIO,
                                                         EST.ID_PROFISSIONAL,
                                                         EST.ID_MICROAREA ,
                                                         CASE WHEN (CP.CSI_CODPAC = FAM.ID_RESPONSAVEL) THEN 1 ELSE 0 END AS RESPONSAVEL,
                                                         USU.USA_TABLET,
                                                         MED.CSI_NOMMED NOME_PROFISSIONAL
                                                     FROM TSI_CADPAC CP
                                                     LEFT JOIN ESUS_FAMILIA FAM ON (CP.ID_FAMILIA = FAM.ID)
                                                     LEFT JOIN VS_ESTABELECIMENTOS EST ON (FAM.ID_DOMICILIO = EST.ID)
                                                     LEFT JOIN TSI_MEDICOS MED ON (EST.ID_PROFISSIONAL = MED.CSI_CODMED)
                                                     LEFT JOIN SEG_USUARIO USU ON (MED.CSI_IDUSER = USU.ID)
                                                      @filtro
                                                     ORDER BY CP.CSI_NOMPAC;";
        string ICidadaoCommand.GetIndividuosToFamilia { get => sqlGetIndividuosToFamilia; }

        public string sqlGetCountIndividuosToFamilia = $@"SELECT COUNT(*) FROM (SELECT CP.CSI_CODPAC ID_INDIVIDUO,
                                                                                    CP.CSI_NOMPAC INDIVIDUO,
                                                                                    CP.CSI_SEXPAC SEXO,
                                                                                    (SELECT * FROM PRO_CALCULA_IDADE(CP.CSI_DTNASC, CURRENT_DATE)) IDADE,
                                                                                    CP.CSI_DTNASC DATA_NASCIMENTO,
                                                                                    FAM.ID ID_FAMILIA,
                                                                                    FAM.NUM_PRONTUARIO_FAMILIAR,
                                                                                    EST.ID ID_DOMICILIO,
                                                                                    EST.ID_PROFISSIONAL,
                                                                                    EST.ID_MICROAREA ,
                                                                                    CASE WHEN (CP.CSI_CODPAC = FAM.ID_RESPONSAVEL) THEN 1 ELSE 0 END AS RESPONSAVEL,
                                                                                    USU.USA_TABLET,
                                                                                    MED.CSI_NOMMED NOME_PROFISSIONAL
                                                                                FROM TSI_CADPAC CP
                                                                                LEFT JOIN ESUS_FAMILIA FAM ON (CP.ID_FAMILIA = FAM.ID)
                                                                                LEFT JOIN VS_ESTABELECIMENTOS EST ON (FAM.ID_DOMICILIO = EST.ID)
                                                                                LEFT JOIN TSI_MEDICOS MED ON (EST.ID_PROFISSIONAL = MED.CSI_CODMED)
                                                                                LEFT JOIN SEG_USUARIO USU ON (MED.CSI_IDUSER = USU.ID)
                                                                                @filtro
                                                                                ORDER BY CP.CSI_NOMPAC;)";
        string ICidadaoCommand.GetCountIndividuosToFamilia { get => sqlGetCountIndividuosToFamilia; }

        public string sqlGetCidadaoByIdProntuarioIdAgente = $@"SELECT C.CSI_CODPAC, C.CSI_NOMPAC, (CASE WHEN COALESCE(F.ID_RESPONSAVEL,-1) = C.CSI_CODPAC THEN 'T' ELSE 'F' END) AS FLG_RESPONSAVEL
                                                            FROM TSI_CADPAC C
                                                            JOIN ESUS_FAMILIA F ON (C.ID_FAMILIA = F.ID)
                                                            JOIN VS_ESTABELECIMENTOS E ON (F.ID_DOMICILIO = E.ID )
                                                            JOIN ESUS_MICROAREA M ON (E.ID_MICROAREA = M.ID)
                                                            WHERE M.id_profissional = @id_profissional ";
        string ICidadaoCommand.GetCidadaoByIdProntuarioIdAgente { get => sqlGetCidadaoByIdProntuarioIdAgente; }



        public string sqlVerificaExisteEsusFamilia = $@"SELECT COUNT(*) QTDE
                                                        FROM RDB$RELATIONS
                                                        WHERE RDB$FLAGS=1 and RDB$RELATION_NAME='ESUS_FAMILIA'";
        string ICidadaoCommand.VerificaExisteEsusFamilia { get => sqlVerificaExisteEsusFamilia; }

    }


}
