using Imunizacao.Domain.Commands;

namespace Imunizacao.Domain.Queries
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

        public string GetConfigUsuario = $@"SELECT * FROM CONFIGURACAO_USUARIO
                                            WHERE ID_USUARIO = @id";
        string ISegUsuarioCommand.GetConfigUsuario { get => GetConfigUsuario; }

        public string sqlGetNewIdConfiguration = $@"SELECT MAX(ID) + 1 FROM CONFIGURACAO_USUARIO";
        string ISegUsuarioCommand.GetNewIdConfiguration { get => sqlGetNewIdConfiguration; }

        public string sqlInsertOrUpdateConfigUsuario = $@"UPDATE OR INSERT INTO CONFIGURACAO_USUARIO (ID, ID_USUARIO, ID_ULTIMA_UNIDADE, QTDE_REGISTRO_TABELA, TIPO_MENU)
                                                      VALUES (@id, @id_usuario, @id_ultima_unidade, @qtde_registro_tabela, @tipo_menu)
                                                      MATCHING (ID_USUARIO)";
        string ISegUsuarioCommand.InsertOrUpdateConfigUsuario { get => sqlInsertOrUpdateConfigUsuario; }


        public string sqltestePerformance = $@"select * from tsi_cadpac";
        string ISegUsuarioCommand.sqlTestePerformance { get => sqltestePerformance; }


    }
}
