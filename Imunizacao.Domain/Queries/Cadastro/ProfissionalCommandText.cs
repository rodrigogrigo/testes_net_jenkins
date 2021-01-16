using RgCidadao.Domain.Commands;
using RgCidadao.Domain.Commands.Cadastro;

namespace RgCidadao.Domain.Queries.Cadastro
{
    public class ProfissionalCommandText : IProfissionalCommand
    {
        public string SqlGetAll = $@"SELECT DISTINCT MED.CSI_CODMED, MED.CSI_NOMMED, CBO.CSI_CBO, CBO.CSI_ATIVO
                                     FROM TSI_MEDICOS MED
                                     JOIN TSI_MEDICOS_CBO CBO ON CBO.CSI_CODMED = MED.CSI_CODMED
                                     @filtro
                                     ORDER BY MED.CSI_NOMMED";

        string IProfissionalCommand.GetAll { get => SqlGetAll; }

        public string sqlGetByUnidade = $@"SELECT MED.CSI_CODMED, MED.CSI_NOMMED, MED_UNI.CSI_CBO, MED.CSI_CNS, EE.ID ID_EQUIPE, EE.SIGLA||'-'||EE.DESCRICAO AS EQUIPE, 
                                           MED.CSI_CPF, MED.CSI_INATIVO AS CSI_INATIVO_PROFISSIONAL, 
                                           MED_UNI.CSI_ATIVADO AS CSI_ATIVO
                                           FROM TSI_MEDICOS MED
                                           JOIN TSI_MEDICOS_UNIDADE MED_UNI ON MED.CSI_CODMED = MED_UNI.CSI_CODMED
                                           LEFT JOIN ESUS_MICROAREA MI ON (MI.ID_PROFISSIONAL = MED.CSI_CODMED)
                                           LEFT JOIN ESUS_EQUIPES EE ON (MI.ID_EQUIPE = EE.ID)
                                           WHERE MED_UNI.CSI_CODUNI = @unidade
                                           ORDER BY MED.CSI_NOMMED";
        string IProfissionalCommand.GetProfissionalByUnidade { get => sqlGetByUnidade; }

        public string sqlGetProfissionalAtivoByUnidade = $@"SELECT MED.CSI_CODMED, MED.CSI_NOMMED, MED_UNI.CSI_CBO, MED.CSI_CNS,
                                                               MED.CSI_CPF, MED.CSI_INATIVO AS CSI_INATIVO_PROFISSIONAL, 
                                                               MED_UNI.CSI_ATIVADO AS CSI_ATIVO
                                                            FROM TSI_MEDICOS MED
                                                                LEFT JOIN TSI_MEDICOS_UNIDADE MED_UNI ON(MED_UNI.CSI_CODMED = MED.CSI_CODMED)
                                                                LEFT JOIN TSI_UNIDADE UNI ON(UNI.CSI_CODUNI = MED_UNI.CSI_CODUNI)
                                                            WHERE UNI.CSI_CODUNI = @unidade AND MED_UNI.CSI_ATIVADO = 'T'
                                                            ORDER BY MED.CSI_NOMMED";
        string IProfissionalCommand.GetProfissionalAtivoByUnidade { get => sqlGetProfissionalAtivoByUnidade; }

        public string sqlGetcbo = $@"SELECT CBO.*
                                     FROM TSI_MEDICOS_CBO MED_CBO
                                     JOIN TSI_CBO CBO ON CBO.CODIGO = MED_CBO.CSI_CBO
                                     WHERE CSI_CODMED = @csi_codmed";
        string IProfissionalCommand.GetListaCBO { get => sqlGetcbo; }

        public string sqlProfissionalCboByUnidade = $@"SELECT
                                                            MED_UNI.CSI_CODMED,
                                                            MED.CSI_NOMMED,
                                                            TRIM(CBO.DESCRICAO) DESCRICAO_CBO,
                                                            CBO.CODIGO CBO,
                                                            MED.CSI_IDUSER
                                                        FROM TSI_MEDICOS_UNIDADE MED_UNI
                                                        JOIN TSI_MEDICOS MED ON MED.CSI_CODMED = MED_UNI.CSI_CODMED
                                                        JOIN TSI_CBO CBO ON CBO.CODIGO = MED_UNI.CSI_CBO
                                                        WHERE MED_UNI.CSI_CODUNI = @unidade AND
                                                             (MED_UNI.CSI_CBO LIKE '2235%' OR
                                                              MED_UNI.CSI_CBO LIKE '3222%') AND
                                                              MED_UNI.CSI_ATIVADO = 'T'
                                                        ORDER BY MED.CSI_NOMMED";
        string IProfissionalCommand.GetProfissionalCboByUnidade { get => sqlProfissionalCboByUnidade; }


        public string sqlGetByUnidades = $@"SELECT MED.CSI_CODMED, MED.CSI_NOMMED, MED_UNI.CSI_CBO
                                           FROM TSI_MEDICOS MED
                                           JOIN TSI_MEDICOS_UNIDADE MED_UNI ON MED.CSI_CODMED = MED_UNI.CSI_CODMED
                                           WHERE MED_UNI.CSI_CODUNI IN (@unidades)
                                           ORDER BY MED.CSI_NOMMED";
        string IProfissionalCommand.GetProfissionalByUnidades { get => sqlGetByUnidades; }

        public string sqlGetCountAll = $@"SELECT COUNT(*)
                                          FROM TSI_MEDICOS MED
                                          JOIN TSI_MEDICOS_UNIDADE MED_UNI ON MED.CSI_CODMED = MED_UNI.CSI_CODMED
                                          @filtros";
        string IProfissionalCommand.GetCountAll { get => sqlGetCountAll; }

        public string sqlGetAllPagination = $@"SELECT FIRST (@pagesize) SKIP (@page) DISTINCT MED.CSI_CODMED, MED.CSI_NOMMED, MED.CSI_CPF, MED.CSI_CBO
                                               FROM TSI_MEDICOS MED
                                               JOIN TSI_MEDICOS_UNIDADE MED_UNI ON MED.CSI_CODMED = MED_UNI.CSI_CODMED
                                               @filtros
                                               ORDER BY MED.CSI_NOMMED";
        string IProfissionalCommand.GetAllPagination { get => sqlGetAllPagination; }

        public string sqlGetProfissionalByIdAndUnidade = $@"SELECT MED.CSI_CODMED, MED.CSI_NOMMED, MED_UNI.CSI_CBO
                                                            FROM TSI_MEDICOS MED
                                                            JOIN TSI_MEDICOS_UNIDADE MED_UNI ON MED.CSI_CODMED = MED_UNI.CSI_CODMED
                                                            WHERE MED_UNI.CSI_CODUNI = @unidade
                                                                  MED.CSI_CODMED = @id_profissional
                                                            ORDER BY MED.CSI_NOMMED";
        string IProfissionalCommand.GetProfissionalByIdAndUnidade { get => sqlGetProfissionalByIdAndUnidade; }

        public string sqlGetAllPaginationProfissionalWithCBO = $@"SELECT FIRST (@pagesize) SKIP (@page) DISTINCT MED.CSI_CODMED, MED.CSI_NOMMED, CBO.CSI_CBO, CB.DESCRICAO DESC_CBO, 
                                                                                                        ELP.ID ID_LOTACAO, EE.ID ID_EQUIPE
                                                                  FROM TSI_MEDICOS MED
                                                                  JOIN TSI_MEDICOS_CBO CBO ON CBO.CSI_CODMED = MED.CSI_CODMED
                                                                  JOIN TSI_CBO CB ON CB.CODIGO = CBO.CSI_CBO  
                                                                  LEFT JOIN TSI_MEDICOS_UNIDADE MED_UNI ON MED.CSI_CODMED = MED_UNI.CSI_CODMED
                                                                  LEFT JOIN ESUS_EQUIPES EE ON EE.ID_PROFISSIONAL = MED.CSI_CODMED
                                                                  LEFT JOIN ESUS_LOTACAO_PROFISSIONAIS ELP ON (ELP.ID_PROFISSIONAL = MED.CSI_CODMED AND
                                                                                                               ELP.ID_EQUIPE = EE.ID AND
                                                                                                               ELP.ID_ESTABELECIMENTO = EE.ID_ESTABELECIMENTO)
                                                                  WHERE CBO.CSI_CBO IS NOT NULL AND
                                                                        CBO.CSI_ATIVO <> 'F' AND
                                                                        CBO.CSI_ATIVO <> 'False' 
                                                                        @filtro
                                                                  ORDER BY MED.CSI_NOMMED";
        string IProfissionalCommand.GetAllPaginationProfissionalWithCBO { get => sqlGetAllPaginationProfissionalWithCBO; }

        public string sqlGetCountAllProfissionalWithCBO = $@"SELECT COUNT(*) FROM (SELECT DISTINCT MED.CSI_CODMED, MED.CSI_NOMMED
                                                                                   FROM TSI_MEDICOS MED
                                                                                   JOIN TSI_MEDICOS_CBO CBO ON CBO.CSI_CODMED = MED.CSI_CODMED
                                                                                   WHERE CBO.CSI_CBO IS NOT NULL AND
                                                                                         CBO.CSI_ATIVO <> 'F' AND
                                                                                         CBO.CSI_ATIVO <> 'False' 
                                                                                         @filtro
                                                                                   ORDER BY MED.CSI_NOMMED)";
        string IProfissionalCommand.GetCountAllProfissionalWithCBO { get => sqlGetCountAllProfissionalWithCBO; }


        public string sqlGetProfissionalByUnidadeWithCBO = $@"SELECT DISTINCT MED.CSI_CODMED, MED.CSI_NOMMED, MED_UNI.CSI_CBO, EE.SIGLA || ' - ' || EE.NOME_REFERENCIA || ' - ' || EE.COD_INE EQUIPE, CBO.CSI_ATIVO, ELP.ID ID_LOTACAO, 
                                                                     EE.ID ID_EQUIPE, MED.CSI_INATIVO CSI_INATIVO_PROFISSIONAL
                                                              FROM TSI_MEDICOS MED
                                                              JOIN TSI_MEDICOS_UNIDADE MED_UNI ON MED.CSI_CODMED = MED_UNI.CSI_CODMED
                                                              JOIN ESUS_EQUIPES EE ON EE.ID_PROFISSIONAL = MED.CSI_CODMED
                                                              JOIN TSI_MEDICOS_CBO CBO ON CBO.CSI_CODMED = MED.CSI_CODMED
                                                              LEFT JOIN ESUS_LOTACAO_PROFISSIONAIS ELP ON (ELP.ID_PROFISSIONAL = MED.CSI_CODMED AND
                                                                                                           ELP.ID_EQUIPE = EE.ID AND
                                                                                                           ELP.ID_ESTABELECIMENTO = EE.ID_ESTABELECIMENTO)
                                                              WHERE MED_UNI.CSI_CODUNI = @unidade AND
                                                                    MED_UNI.CSI_CBO IS NOT NULL AND
                                                                    CBO.CSI_ATIVO = 'True' AND
                                                                    COALESCE(MED_UNI.CSI_ATIVADO,'T') = 'T'
                                                              ORDER BY MED.CSI_NOMMED";
        string IProfissionalCommand.GetProfissionalByUnidadeWithCBO { get => sqlGetProfissionalByUnidadeWithCBO; }


        public string sqlGetProfissionalById = $@"SELECT CSI_CODMED, CSI_NOMMED, CSI_ENDMED, CSI_BAIMED, CSI_CEPMED, CSI_FONRES, CSI_FONTRA, CSI_FONCON,
                                                 CSI_CELULAR, CSI_ENDCON, CSI_BAICON, CSI_CEPCON, CSI_NOMUSU, CSI_DATAINC, CSI_DATAALT,
                                                 EXCLUIDO, CSI_CPF, CSI_IDE, CSI_CRM, CSI_PONTOREF, CSI_CODESP, CSI_CIDMED, CSI_CIDCON,
                                                 CSI_TIPO, CSI_CBO, CSI_CNS, CSI_APELIDO, CSI_SENHA, CSI_IDUSER, CSI_PROCPAD, CSI_IDMICROAREA,
                                                 CSI_INATIVO, CSI_DATA_INATIVO, CSI_PIS_PASEP, CSI_STATUS_HIPERDIA, CSI_DTNASC, CSI_SEXO,
                                                 CSI_COD_CONS_CLASE, CSI_SG_UF_EMIS_CONS_CLASSE, CSI_E_MAIL, CSI_NUMEROENDERECO,
                                                 CSI_COMPLEMENTOEND, IDHORARIO, DATA_ALTERACAO_SERV, CSI_NOME_ORGAO_CLASSE, ID_NUMERO_INTERNO,
                                                 N_MATRICULA, CSI_NATURALIDADE, NOME_MAE, NOME_PAI, RACA_COR, FLG_DEFICIENCIA, FLG_DEF_AUDITIVA,
                                                 FLG_DEF_FISICA, FLG_DEF_MOTORA, FLG_DEF_VISUAL, FLG_DEF_OUTRAS, CSI_IDE_UF,
                                                 CSI_IDE_ORGAO_EMISSOR, CSI_IDE_DATA_EMISSAO, CTPS, CTPS_SERIE, CTPS_UF, CTPS_DATA_EMISSAO,
                                                 SITUACAO_FAMILIAR, FLG_POSSUI_FILHOS, QTD_FILHOS, ESCOLARIDADE, DESC_ESCOLARIDADE,
                                                 FREQ_ESCOLA_FACULDADE, PREVISAO_TERMINO_ESCOLA, INSALUBRIDADE, CSI_CRM_UF, IMAGEM_ASSINATURA FROM TSI_MEDICOS WHERE CSI_CODMED = @id_profissional";
        string IProfissionalCommand.GetProfissionalById { get => sqlGetProfissionalById; }

        public string sqlGetByEquipe = $@"SELECT M.CSI_CODMED, M.CSI_NOMMED, M.CSI_CNS
                                            FROM ESUS_EQUIPES E
                                            JOIN ESUS_MICROAREA MI ON (MI.ID_EQUIPE = E.ID)
                                            JOIN TSI_MEDICOS M ON (MI.ID_PROFISSIONAL = M.CSI_CODMED)
                                            WHERE E.ID = @id_equipe";
        string IProfissionalCommand.GetProfissionalByEquipe { get => sqlGetByEquipe; }

        public string sqlGetACSByEstabelecimentoSaude = $@"SELECT M.CSI_CODMED, M.CSI_NOMMED, LP.COD_CBO
                                                        FROM ESUS_ESTABELECIMENTO_SAUDE ES
                                                        JOIN ESUS_EQUIPES E ON (ES.ID = E.ID_ESTABELECIMENTO)
                                                        JOIN ESUS_MICROAREA MI ON (MI.ID_EQUIPE = E.ID)
                                                        JOIN TSI_MEDICOS M ON (MI.ID_PROFISSIONAL = M.CSI_CODMED)
                                                        JOIN ESUS_LOTACAO_PROFISSIONAIS LP ON (LP.ID_ESTABELECIMENTO = ES.ID
                                                        AND LP.ID_EQUIPE = E.ID AND LP.ID_PROFISSIONAL = M.CSI_CODMED)
                                                        WHERE ES.ID = @id";
        string IProfissionalCommand.GetACSByEstabelecimentoSaude { get => sqlGetACSByEstabelecimentoSaude; }

        #region Agendamento de Consulta
        public string sqlGetCBOByMedicoUnidade = $@"SELECT MU.CSI_CBO, C.DESCRICAO, M.CSI_CNS, M.CSI_NOMMED
                                                    FROM TSI_MEDICOS_UNIDADE MU
                                                    JOIN TSI_MEDICOS M ON M.CSI_CODMED = MU.CSI_CODMED
                                                    JOIN TSI_CBO C ON (C.CODIGO = MU.CSI_CBO)
                                                    WHERE MU.CSI_CODMED = @codmed AND
                                                          MU.CSI_CODUNI = @coduni
                                                          @filtro AND
                                                          TRIM(MU.CSI_ATIVADO) = 'T'";
        string IProfissionalCommand.GetCBOByMedicoUnidade { get => sqlGetCBOByMedicoUnidade; }
        #endregion
    }
}
