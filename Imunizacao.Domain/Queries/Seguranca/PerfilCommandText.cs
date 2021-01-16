using RgCidadao.Domain.Commands.Seguranca;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Queries.Seguranca
{
    public class PerfilCommandText : IPerfilCommand
    {
        #region Perfil de Acesso
        public string sqlGetAllPerfis = $@"SELECT * FROM SEG_PERFIL_ACESSO SPA
                                            @filtro";
        string IPerfilCommand.GetAllPerfis { get => sqlGetAllPerfis; }

        public string sqlInsertSegPerfilAcesso = $@"INSERT INTO SEG_PERFIL_ACESSO(ID, DESCRICAO)
                                                    VALUES(@id, @descricao)";
        string IPerfilCommand.InsertSegPerfilAcesso { get => sqlInsertSegPerfilAcesso; }

        public string sqlUpdateSegPerfilAcesso = $@"UPDATE SEG_PERFIL_ACESSO SET DESCRICAO = @descricao
                                                    WHERE ID = @id";
        string IPerfilCommand.UpdateSegPerfilAcesso { get => sqlUpdateSegPerfilAcesso; }

        public string sqlGetPerfilById = $@"SELECT * FROM SEG_PERFIL_ACESSO
                                            WHERE ID = @id";
        string IPerfilCommand.GetPerfilById { get => sqlGetPerfilById; }

        public string sqlGetPerfilNewId = $@"SELECT GEN_ID(GEN_SEG_PERFIL_ACESSO_ID, 1) AS VLR FROM RDB$DATABASE";
        string IPerfilCommand.GetPerfilNewId { get => sqlGetPerfilNewId; }

        public string sqlGetPerfilByDescricao = $@"SELECT * FROM SEG_PERFIL_ACESSO
                                                   WHERE DESCRICAO CONTAINING @descricao";
        string IPerfilCommand.GetPerfilByDescricao { get => sqlGetPerfilByDescricao; }

        public string sqlGetPerfilPagination = $@"SELECT FIRST(@pagesize) SKIP(@page) * FROM SEG_PERFIL_ACESSO SPA
                                                  @filtro";
        string IPerfilCommand.GetPerfilPagination { get => sqlGetPerfilPagination; }

        public string sqlGetCountAll = $@"SELECT COUNT(*) FROM SEG_PERFIL_ACESSO SPA
                                          @filtro";
        string IPerfilCommand.GetCountAll { get => sqlGetCountAll; }
        #endregion

        #region Modulos de Segurança
        public string sqlGetModulosByPerfil = $@"SELECT M.ID ID_MODULO, M.DESCRICAO AS MODULO,TEL.ID ID_TELA, TEL.DESCRICAO DESCRICAO_TELA,TEL.NOME NOME_TELA,
                                                        SA.NOME NOME_ACAO,SA.DESCRICAO DESCRICAO_ACAO,PP.PERMISSAO, PP.ID ID_PERMISSAO_PERFIL
                                                 FROM SEG_MODULOS M
                                                 JOIN SEG_TELAS TEL ON TEL.ID_MODULO = M.ID
                                                 JOIN SEG_TELAS_ACOES TC ON TC.ID_TELA = TEL.ID
                                                 JOIN SEG_ACOES SA ON SA.ID = TC.ID_ACAO
                                                 JOIN SEG_PERMISSOES_PERFIL PP ON PP.ID_TELA_ACAO = TC.ID
                                                 WHERE PP.ID_PERFIL = @id_perfil";
        string IPerfilCommand.GetModulosByPerfil { get => sqlGetModulosByPerfil; }

        public string sqlAtualizaPermissaoPerfil = $@"UPDATE SEG_PERMISSOES_PERFIL SET PERMISSAO = @permissao
                                                      WHERE ID = @id";
        string IPerfilCommand.AtualizaPermissaoPerfil { get => sqlAtualizaPermissaoPerfil; }

        #endregion
    }
}
