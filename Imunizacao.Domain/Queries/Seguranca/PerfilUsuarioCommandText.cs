using RgCidadao.Domain.Commands.Seguranca;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Queries.Seguranca
{
    public class PerfilUsuarioCommandText : IPerfilUsuarioCommand
    {
        public string sqlInsertOrUpdate = $@"UPDATE OR INSERT INTO SEG_PERFIL_USUARIO(ID,ID_USUARIO,ID_UNIDADE,ID_PERFIL)
                                             VALUES(@id, @id_usuario, @id_unidade, @id_perfil)";
        string IPerfilUsuarioCommand.InsertOrUpdate { get => sqlInsertOrUpdate; }

        public string sqlGetNewId = $@"SELECT GEN_ID(GEN_SEG_PERFIL_USUARIO_ID, 1) AS VLR FROM RDB$DATABASE";
        string IPerfilUsuarioCommand.GetNewId { get => sqlGetNewId; }

        public string sqlDelete = $@"DELETE FROM SEG_PERFIL_USUARIO
                                     WHERE ID = @id";
        string IPerfilUsuarioCommand.Delete { get => sqlDelete; }

        public string sqlGetByIdUsuario = $@"SELECT PU.*, U.CSI_NOMUNI UNIDADE, PA.DESCRICAO PERFIL
                                             FROM SEG_PERFIL_USUARIO PU
                                             JOIN TSI_UNIDADE U ON PU.ID_UNIDADE = U.CSI_CODUNI
                                             JOIN SEG_PERFIL_ACESSO PA ON PU.ID_PERFIL = PA.ID
                                             WHERE PU.ID_USUARIO = @id_usuario;";
        string IPerfilUsuarioCommand.GetByIdUsuario { get => sqlGetByIdUsuario; }

        public string sqlGetPermissaoTelaModulo = $@"SELECT COUNT(PU.ID)
                                                     FROM SEG_PERFIL_USUARIO PU
                                                     JOIN SEG_PERFIL_ACESSO PA ON (PU.ID_PERFIL = PA.ID)
                                                     JOIN SEG_PERMISSOES_PERFIL PP ON(PA.ID = PU.ID_PERFIL)
                                                     JOIN SEG_TELAS_ACOES TA ON(PP.ID_TELA_ACAO = TA.ID)
                                                     JOIN SEG_ACOES A ON(TA.ID_ACAO = A.ID)
                                                     JOIN SEG_TELAS T ON(TA.ID_TELA = T.ID)
                                                     JOIN SEG_MODULOS M ON(T.ID_MODULO = M.ID)
                                                     WHERE PU.ID_USUARIO = @usuario AND
                                                           PU.ID_UNIDADE = @unidade AND
                                                           M.NOME = @modulo AND
                                                           T.NOME = @tela AND
                                                           A.NOME = @acao AND
                                                           PP.PERMISSAO = 1;";
        string IPerfilUsuarioCommand.GetPermissaoTelaModulo { get => sqlGetPermissaoTelaModulo; }

        public string GetPermissaoUsuarios = $@"SELECT
                                                    M.NOME MODULO, T.NOME TELA, A.NOME ACAO
                                                FROM SEG_PERMISSOES_PERFIL PP
                                                JOIN SEG_PERFIL_ACESSO PA ON (PP.ID_PERFIL = PA.ID)
                                                JOIN SEG_PERFIL_USUARIO PU ON (PU.ID_PERFIL = PA.ID)
                                                JOIN SEG_TELAS_ACOES TA ON (PP.ID_TELA_ACAO = TA.ID)
                                                JOIN SEG_TELAS T ON (TA.ID_TELA = T.ID)
                                                JOIN SEG_ACOES A ON (TA.ID_ACAO = A.ID)
                                                JOIN SEG_MODULOS M ON (T.ID_MODULO = M.ID)
                                                WHERE PP.PERMISSAO = 1
                                                      AND PU.ID_USUARIO = @usuario
                                                      AND PU.ID_UNIDADE = @unidade;";
        string IPerfilUsuarioCommand.GetPermissaoUsuarios { get => GetPermissaoUsuarios; }

        public string sqlGetPermissaoUsuarioTipo1e2 = $@"SELECT DISTINCT
                                                             M.NOME MODULO, T.NOME TELA, A.NOME ACAO
                                                         FROM SEG_PERMISSOES_PERFIL PP
                                                         JOIN SEG_PERFIL_ACESSO PA ON (PP.ID_PERFIL = PA.ID)
                                                         JOIN SEG_PERFIL_USUARIO PU ON (PU.ID_PERFIL = PA.ID)
                                                         JOIN SEG_TELAS_ACOES TA ON (PP.ID_TELA_ACAO = TA.ID)
                                                         JOIN SEG_TELAS T ON (TA.ID_TELA = T.ID)
                                                         JOIN SEG_ACOES A ON (TA.ID_ACAO = A.ID)
                                                         JOIN SEG_MODULOS M ON (T.ID_MODULO = M.ID)
                                                         WHERE PU.ID_UNIDADE = @unidade";
        string IPerfilUsuarioCommand.GetPermissaoUsuarioTipo1e2 { get => sqlGetPermissaoUsuarioTipo1e2; }

        public string sqlGetUsuarioPermissaoTipo1e2 = $@"SELECT TIPO_USUARIO FROM SEG_USUARIO
                                                         WHERE ID = @id_usuario";
        string IPerfilUsuarioCommand.GetUsuarioPermissaoTipo1e2 { get => sqlGetUsuarioPermissaoTipo1e2; }

        public string sqlGetPermissaoModuloTela = $@"SELECT COUNT(PU.ID)
                                                     FROM SEG_PERFIL_USUARIO PU
                                                     JOIN SEG_PERFIL_ACESSO PA ON (PU.ID_PERFIL = PA.ID)
                                                     JOIN SEG_PERMISSOES_PERFIL PP ON(PA.ID = PU.ID_PERFIL)
                                                     JOIN SEG_TELAS_ACOES TA ON(PP.ID_TELA_ACAO = TA.ID)
                                                     JOIN SEG_ACOES A ON(TA.ID_ACAO = A.ID)
                                                     JOIN SEG_TELAS T ON(TA.ID_TELA = T.ID)
                                                     JOIN SEG_MODULOS M ON(T.ID_MODULO = M.ID)
                                                     WHERE PU.ID_USUARIO = @usuario AND
                                                           PU.ID_UNIDADE = @unidade AND
                                                           M.NOME = @modulo AND
                                                           T.NOME = @tela AND
                                                           PP.PERMISSAO = 1;";
        string IPerfilUsuarioCommand.GetPermissaoModuloTela { get => sqlGetPermissaoModuloTela; }

    }
}
