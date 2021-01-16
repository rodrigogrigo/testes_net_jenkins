using RgCidadao.Domain.Commands.Cadastro;
using RgCidadao.Domain.Entities.Cadastro;
using RgCidadao.Domain.Repositories.Cadastro;
using Dapper;
using System;
using System.Linq;
using RgCidadao.Domain.Entities.Imunizacao;
using System.Collections.Generic;

namespace RgCidadao.Domain.Infra.Repositories.Cadastro
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
                             @tipo_menu = model.tipo_menu,
                             @busca_automatica = model.busca_automatica,
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

        public int GetPermissaoUser(string ibge, int id)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                         conn.QueryFirstOrDefault<int>(_segusercommand.GetPermissaoUser, new { @usuario = id }));
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertUser(string ibge, Seg_Usuario model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                        conn.Execute(_segusercommand.Insert, new
                        {
                            @id = model.id,
                            @login = model.login,
                            @senha = model.senha,
                            @nome = model.nome,
                            @administrador = model.administrador,
                            @csi_id_ultima_versao = model.csi_id_ultima_versao,
                            @csi_exibir_versao = model.csi_exibir_versao,
                            @timeout = model.timeout,
                            @status = model.status,
                            @id_grupo = model.id_grupo,
                            @email_1 = model.email_1,
                            @email_2 = model.email_2,
                            @telefone_1 = model.telefone_1,
                            @telefone_2 = model.telefone_2,
                            @layout_consulta = model.layout_consulta,
                            @data_alteracao_serv = model.data_alteracao_serv,
                            @senha_nao_expira = model.senha_nao_expira,
                            @usa_tablet = model.usa_tablet,
                            @uuid_usuario = model.uuid_usuario,
                            @possui_certificado_digital = model.possui_certificado_digital,
                            @chave_privada = model.chave_privada,
                            @acessar_app_indicador = model.acessar_app_indicador,
                            @email_3 = model.email_3,
                            @tipo_usuario = model.tipo_usuario
                        }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdatetUser(string ibge, Seg_Usuario model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                        conn.Execute(_segusercommand.Update, new
                        {
                            @login = model.login,
                            @senha = model.senha,
                            @nome = model.nome,
                            @administrador = model.administrador,
                            @csi_id_ultima_versao = model.csi_id_ultima_versao,
                            @csi_exibir_versao = model.csi_exibir_versao,
                            @timeout = model.timeout,
                            @status = model.status,
                            @id_grupo = model.id_grupo,
                            @email_1 = model.email_1,
                            @email_2 = model.email_2,
                            @telefone_1 = model.telefone_1,
                            @telefone_2 = model.telefone_2,
                            @layout_consulta = model.layout_consulta,
                            @data_alteracao_serv = model.data_alteracao_serv,
                            @senha_nao_expira = model.senha_nao_expira,
                            @usa_tablet = model.usa_tablet,
                            @uuid_usuario = model.uuid_usuario,
                            @possui_certificado_digital = model.possui_certificado_digital,
                            @chave_privada = model.chave_privada,
                            @acessar_app_indicador = model.acessar_app_indicador,
                            @email_3 = model.email_3,
                            @tipo_usuario = model.tipo_usuario,
                            @id = model.id
                        }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetNewId(string ibge)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                       conn.QueryFirstOrDefault<int>(_segusercommand.GetNewId));
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetCountAll(string ibge, string filtro)
        {
            try
            {
                string sql = string.Empty;
                if (!string.IsNullOrWhiteSpace(filtro))
                    sql = _segusercommand.GetCountAll.Replace("@filtro", filtro);
                else
                    sql = _segusercommand.GetCountAll.Replace("@filtro", string.Empty);

                int contagem = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                      conn.QueryFirstOrDefault<int>(sql));

                return contagem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Seg_Usuario> GetAllPagination(string ibge, int pagesize, int page, string filtro)
        {
            try
            {
                string sql = string.Empty;
                if (!string.IsNullOrWhiteSpace(filtro))
                    sql = _segusercommand.GetAllPagination.Replace("@filtro", filtro);
                else
                    sql = _segusercommand.GetAllPagination.Replace("@filtro", string.Empty);

                List<Seg_Usuario> lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                                            conn.Query<Seg_Usuario>(sql, new
                                                            {
                                                                @pagesize = pagesize,
                                                                @page = page
                                                            })).ToList();

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public byte[] GetCabecalhoPaisagem(string ibge, int unidade)
        {
            try
            {
                var imagem = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.QueryFirstOrDefault<byte[]>(_segusercommand.GetReportCabecalhoHorizontal, new { @unidade = unidade }));

                if (imagem == null)
                {
                    imagem = new Byte[0];
                }

                return imagem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public byte[] GetCabecalhoRetrato(string ibge, int unidade)
        {
            try
            {
                var imagem = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                           conn.QueryFirstOrDefault<byte[]>(_segusercommand.GetReportCabecalho, new { @unidade = unidade }));

                if (imagem == null)
                {
                    imagem = new Byte[0];
                }
                return imagem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int? GetTipoUsuarioById(string ibge, int id)
        {
            try
            {
                int contagem = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                   conn.QueryFirstOrDefault<int>(_segusercommand.GetTipoUsuarioById, new
                   {
                       @id_usuario = id
                   }));

                return contagem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
