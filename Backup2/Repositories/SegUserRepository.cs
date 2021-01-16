using Imunizacao.Domain.Commands;
using Imunizacao.Domain.Entities;
using Imunizacao.Domain.Repositories;
using Dapper;
using System;
using System.Linq;

namespace Imunizacao.Domain.Infra.Repositories
{
    public class SegUserRepository : ISegUserRepository
    {
        private readonly ISegUsuarioCommand _segusercommand;

        public SegUserRepository(ISegUsuarioCommand commandText)
        {
            _segusercommand = commandText;
        }

        public Seg_Usuario GetSegUsuarioById(int ID, string ibge)
        {
            try
            {
                var user = Helpers.HelperConnection.ExecuteCommand<Seg_Usuario>(ibge, conn =>
                            conn.QueryFirstOrDefault<Seg_Usuario>(_segusercommand.GetSegUsuarioById, new { @id = ID }));

                return user;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public Seg_Usuario GetLogin(string ibge, string login, string senha)
        {
            try
            {
                var user = Helpers.HelperConnection.ExecuteCommand<Seg_Usuario>(ibge, conn =>
                             conn.QueryFirstOrDefault<Seg_Usuario>(_segusercommand.GetLogin, new { @login = login, @senha = senha }));

                return user;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }

        public Seg_Usuario GetTelefoneByUser(string ibge, string login)
        {
            try
            {
                var user = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.QueryFirstOrDefault<Seg_Usuario>(_segusercommand.GetTelefoneByUser, new { @login = login }));

                return user;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public void AtualizarSenhaProvisoria(string ibge, int id, string senha)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.Execute(_segusercommand.AtualizarSenhaProvisoria, new { @senha = senha, @id = id }));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public void Update(string ibge, int id, string telefone1, string telefone2, string email)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                         conn.Execute(_segusercommand.AtualizarInfoUsuario, new
                         {
                             @telefone1 = telefone1,
                             @telefone2 = telefone2,
                             @email = email,
                             @id = id
                         }));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public Configuracao_Usuario GetConfigUsuario(string ibge, int id)
        {
            try
            {
                var model = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.QueryFirstOrDefault<Configuracao_Usuario>(_segusercommand.GetConfigUsuario, new { @id = id }));
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertOrUpdateConfigUsuario(string ibge, Configuracao_Usuario model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                         conn.Execute(_segusercommand.InsertOrUpdateConfigUsuario, new
                         {
                             @id = model.id,
                             @id_usuario = model.id_usuario,
                             @id_ultima_unidade = model.id_ultima_unidade,
                             @qtde_registro_tabela = model.qtde_registro_tabela,
                             @tipo_menu = model.tipo_menu
                         }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetNewIdConfiguration(string ibge)
        {
            try
            {
                var id = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                         conn.QueryFirstOrDefault<int?>(_segusercommand.GetNewIdConfiguration));
                if (id == null)
                    id = 1;

                return (int)id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public System.Collections.Generic.List<dynamic> GetTestePerformance(string ibge)
        {
            try
            {
                var lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                         conn.Query<dynamic>(_segusercommand.sqlTestePerformance)).ToList();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
