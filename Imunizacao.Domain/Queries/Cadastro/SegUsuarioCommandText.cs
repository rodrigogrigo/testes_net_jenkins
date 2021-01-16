using RgCidadao.Domain.Commands.Cadastro;

namespace RgCidadao.Domain.Queries.Cadastro
{
    public class SegUsuarioCommandText : ISegUsuarioCommand
    {
        public string SqlGetSegUsuarioById = $@"SELECT *
                                                FROM SEG_USUARIO  
                                                WHERE ID = @ID";

        string ISegUsuarioCommand.GetSegUsuarioById { get => SqlGetSegUsuarioById; }

        public string SqlGetLogin = $@"SELECT *
                                    FROM SEG_USUARIO
                                    WHERE LOGIN = @login AND
                                          SENHA = @senha";

        string ISegUsuarioCommand.GetLogin { get => SqlGetLogin; }

        public string SqlGetUserByTelefone = $@"SELECT *
                                                FROM SEG_USUARIO
                                                WHERE LOGIN = @login";

        string ISegUsuarioCommand.GetTelefoneByUser { get => SqlGetUserByTelefone; }

        public string SqlRecuperarSenha = $@"UPDATE SEG_USUARIO
                                             SET SENHA = @senha
                                             WHERE ID = @id";

        string ISegUsuarioCommand.AtualizarSenhaProvisoria { get => SqlRecuperarSenha; }

        public string sqlAtualizarInfoUsuario = $@"UPDATE SEG_USUARIO
                                                    SET TELEFONE_1 = @telefone1,
                                                        TELEFONE_2 = @telefone2,
                                                        EMAIL_1 = @email
                                                    WHERE ID = @id";
        string ISegUsuarioCommand.AtualizarInfoUsuario { get => sqlAtualizarInfoUsuario; }

        public string sqlInsert = $@"INSERT INTO SEG_USUARIO (ID, LOGIN, SENHA, NOME, ADMINISTRADOR, CSI_ID_ULTIMA_VERSAO, CSI_EXIBIR_VERSAO, TIMEOUT,
                                                              STATUS, ID_GRUPO, EMAIL_1, EMAIL_2, TELEFONE_1, TELEFONE_2, LAYOUT_CONSULTA,
                                                              DATA_ALTERACAO_SERV, SENHA_NAO_EXPIRA, USA_TABLET, UUID_USUARIO, POSSUI_CERTIFICADO_DIGITAL,
                                                              CHAVE_PRIVADA, ACESSAR_APP_INDICADOR, EMAIL_3, TIPO_USUARIO)
                                     VALUES (@id, @login, @senha, @nome, @administrador, @csi_id_ultima_versao, @csi_exibir_versao, @timeout, @status, @id_grupo,
                                             @email_1, @email_2, @telefone_1, @telefone_2, @layout_consulta, @data_alteracao_serv, @senha_nao_expira, @usa_tablet,
                                             @uuid_usuario, @possui_certificado_digital, @chave_privada, @acessar_app_indicador, @email_3, @tipo_usuario)";
        string ISegUsuarioCommand.Insert { get => sqlInsert; }

        public string sqlUpdate = $@"UPDATE SEG_USUARIO
                                    SET LOGIN = @login,
                                        SENHA = @senha,
                                        NOME = @nome,
                                        ADMINISTRADOR = @administrador,
                                        CSI_ID_ULTIMA_VERSAO = @csi_id_ultima_versao,
                                        CSI_EXIBIR_VERSAO = @csi_exibir_versao,
                                        TIMEOUT = @timeout,
                                        STATUS = @status,
                                        ID_GRUPO = @id_grupo,
                                        EMAIL_1 = @email_1,
                                        EMAIL_2 = @email_2,
                                        TELEFONE_1 = @telefone_1,
                                        TELEFONE_2 = @telefone_2,
                                        LAYOUT_CONSULTA = @layout_consulta,
                                        DATA_ALTERACAO_SERV = @data_alteracao_serv,
                                        SENHA_NAO_EXPIRA = @senha_nao_expira,
                                        USA_TABLET = @usa_tablet,
                                        UUID_USUARIO = @uuid_usuario,
                                        POSSUI_CERTIFICADO_DIGITAL = @possui_certificado_digital,
                                        CHAVE_PRIVADA = @chave_privada,
                                        ACESSAR_APP_INDICADOR = @acessar_app_indicador,
                                        EMAIL_3 = @email_3,
                                        TIPO_USUARIO = @tipo_usuario
                                    WHERE ID = @id";
        string ISegUsuarioCommand.Update { get => sqlUpdate; }

        public string GetConfigUsuario = $@"SELECT * FROM CONFIGURACAO_USUARIO
                                            WHERE ID_USUARIO = @id";
        string ISegUsuarioCommand.GetConfigUsuario { get => GetConfigUsuario; }

        public string sqlGetNewIdConfiguration = $@"SELECT MAX(ID) + 1 FROM CONFIGURACAO_USUARIO";
        string ISegUsuarioCommand.GetNewIdConfiguration { get => sqlGetNewIdConfiguration; }

        public string sqlInsertOrUpdateConfigUsuario = $@"UPDATE OR INSERT INTO CONFIGURACAO_USUARIO (ID, ID_USUARIO, ID_ULTIMA_UNIDADE, QTDE_REGISTRO_TABELA, TIPO_MENU, BUSCA_AUTOMATICA)
                                                      VALUES (@id, @id_usuario, @id_ultima_unidade, @qtde_registro_tabela, @tipo_menu, @busca_automatica)
                                                      MATCHING (ID_USUARIO)";
        string ISegUsuarioCommand.InsertOrUpdateConfigUsuario { get => sqlInsertOrUpdateConfigUsuario; }

        public string sqltestePerformance = $@"select * from tsi_cadpac";
        string ISegUsuarioCommand.sqlTestePerformance { get => sqltestePerformance; }

        public string sqlGetPermissaoUser = $@"SELECT COUNT(*)
                                               FROM SEG_PERFIL_USUARIO PU
                                               WHERE PU.ID_USUARIO = @usuario";
        string ISegUsuarioCommand.GetPermissaoUser { get => sqlGetPermissaoUser; }

        public string sqlGetNewId = $@"SELECT GEN_ID(GEN_SEG_USUARIO, 1) AS VLR FROM RDB$DATABASE";
        string ISegUsuarioCommand.GetNewId { get => sqlGetNewId; }

        public string sqlGetAllPagination = $@"SELECT FIRST (@pagesize) SKIP (@page) SG.ID, SG.NOME, SG.LOGIN, SG.ADMINISTRADOR, SG.TIPO_USUARIO
                                               FROM SEG_USUARIO SG
                                               @filtro";
        string ISegUsuarioCommand.GetAllPagination { get => sqlGetAllPagination; }

        public string sqlGetCountAll = $@"SELECT COUNT(*)
                                          FROM SEG_USUARIO SG
                                          @filtro";
        string ISegUsuarioCommand.GetCountAll { get => sqlGetCountAll; }

        public string sqlGetReportCabecalho = $@"SELECT U.CSI_CABECALHOR
                                                 FROM TSI_UNIDADE U
                                                 WHERE CSI_CODUNI = @unidade";
        string ISegUsuarioCommand.GetReportCabecalho { get => sqlGetReportCabecalho; }

        public string sqlGetReportCabecalhoHorizontal = $@"SELECT U.CSI_CABECALHOP
                                                           FROM TSI_UNIDADE U
                                                           WHERE CSI_CODUNI = @unidade";
        string ISegUsuarioCommand.GetReportCabecalhoHorizontal { get => sqlGetReportCabecalhoHorizontal; }

        public string sqlGetTipoUsuarioById = $@"SELECT COALESCE(USU.TIPO_USUARIO, 3) FROM SEG_USUARIO USU
                                                 WHERE USU.ID = @id_usuario";
        string ISegUsuarioCommand.GetTipoUsuarioById { get => sqlGetTipoUsuarioById; }
    }
}
