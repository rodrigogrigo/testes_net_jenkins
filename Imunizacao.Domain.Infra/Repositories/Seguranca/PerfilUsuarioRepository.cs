using Dapper;
using RgCidadao.Domain.Commands.Cadastro;
using RgCidadao.Domain.Commands.Seguranca;
using RgCidadao.Domain.Entities.Seguranca;
using RgCidadao.Domain.Repositories.Cadastro;
using RgCidadao.Domain.Repositories.Seguranca;
using RgCidadao.Domain.ViewModels.Seguranca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RgCidadao.Domain.Infra.Repositories.Seguranca
{
    public class PerfilUsuarioRepository : IPerfilUsuarioRepository
    {
        private IPerfilUsuarioCommand _command;
        private ISegUserRepository _seguserrepository;
        public PerfilUsuarioRepository(IPerfilUsuarioCommand command, ISegUserRepository seguserrepository)
        {
            _command = command;
            _seguserrepository = seguserrepository;
        }

        public void Delete(string ibge, int id)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                     conn.Execute(_command.Delete, new { @id = id }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Seg_Perfil_Usuario> GetByIdUsuario(string ibge, int id_usuario)
        {
            try
            {
                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                    conn.Query<Seg_Perfil_Usuario>(_command.GetByIdUsuario, new { @id_usuario = id_usuario }).ToList());

                return itens;
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
                                     conn.QueryFirstOrDefault<int>(_command.GetNewId));
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool GetPermissaoByUserTelaModulo(string ibge, int usuario, int unidade, string modulo, string tela, string acao)
        {
            try
            {
                bool boolpermissao = false;
                var modelusuario = _seguserrepository.GetSegUsuarioById(usuario, ibge);
                if (modelusuario.tipo_usuario == 1 || modelusuario.tipo_usuario == 2)
                    return boolpermissao = true;


                if (acao.Contains(";"))
                {
                    var contagem = 0;
                    foreach (var item in acao.Split(";")[contagem])
                    {
                        var permissao = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                   conn.QueryFirstOrDefault<int>(_command.GetPermissaoTelaModulo, new
                                   {
                                       @usuario = usuario,
                                       @unidade = unidade,
                                       @modulo = modulo,
                                       @tela = tela,
                                       @acao = item
                                   }));

                        if (permissao > 0 && !boolpermissao)
                            boolpermissao = true;
                        contagem = contagem++;
                    }
                }
                else
                {
                    var permissao = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                      conn.QueryFirstOrDefault<int>(_command.GetPermissaoTelaModulo, new
                                      {
                                          @usuario = usuario,
                                          @unidade = unidade,
                                          @modulo = modulo,
                                          @tela = tela,
                                          @acao = acao
                                      }));

                    if (permissao > 0 && !boolpermissao)
                        boolpermissao = true;
                }

                return boolpermissao;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int? GetPermissaoModuloTela(string ibge, int usuario, string unidade, string modulo, string tela)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                   conn.QueryFirstOrDefault<int>(_command.GetPermissaoModuloTela, new
                                   {
                                       @usuario = usuario,
                                       @unidade = unidade,
                                       @modulo = modulo,
                                       @tela = tela
                                   }));
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SegPermissoesUsuarioViewModel> GetPermissaoUsuarios(string ibge, int usuario, int unidade)
        {
            try
            {
                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                               conn.Query<dynamic>(_command.GetPermissaoUsuarios, new
                               {
                                   @usuario = usuario,
                                   @unidade = unidade
                               }).ToList());

                var lista = new List<SegPermissoesUsuarioViewModel>();
                var modulos = itens.Select(x => new
                {
                    descricao = x.MODULO,
                }).Distinct().ToList();

                foreach (var item in modulos)
                {
                    var mod = new SegPermissoesUsuarioViewModel();
                    mod.modulo = item.descricao;

                    var telas = itens.Where(t => t.MODULO == item.descricao)
                                   .Select(p => new
                                   {
                                       descricao = p.TELA
                                   }).Distinct().ToList();

                    var telasmodel = telas.Select(x => new SegTelasUsuarioViewModel()
                    {
                        tela = x.descricao
                    }).ToList();

                    foreach (var itemtelas in telasmodel)
                    {
                        var acoes = itens.Where(k => k.MODULO == item.descricao && k.TELA == itemtelas.tela)
                                                       .Select(b => new
                                                       {
                                                           b.ACAO
                                                       }).Distinct().ToList();
                        var listastring = new List<string>();
                        foreach (var itemacoes in acoes)
                        {
                            listastring.Add(itemacoes.ACAO.ToString());
                        }
                        itemtelas.acoes.AddRange(listastring);
                    }
                    mod.telas.AddRange(telasmodel);
                    lista.Add(mod);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SegPermissoesUsuarioViewModel> GetPermissaoUsuarioTipo1e2(string ibge, int unidade)
        {
            try
            {
                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                conn.Query<dynamic>(_command.GetPermissaoUsuarioTipo1e2, new
                                {
                                    @unidade = unidade
                                }).ToList());

                var lista = new List<SegPermissoesUsuarioViewModel>();
                var modulos = itens.Select(x => new
                {
                    descricao = x.MODULO,
                }).Distinct().ToList();

                foreach (var item in modulos)
                {
                    var mod = new SegPermissoesUsuarioViewModel();
                    mod.modulo = item.descricao;

                    var telas = itens.Where(t => t.MODULO == item.descricao)
                                   .Select(p => new
                                   {
                                       descricao = p.TELA
                                   }).Distinct().ToList();

                    var telasmodel = telas.Select(x => new SegTelasUsuarioViewModel()
                    {
                        tela = x.descricao
                    }).ToList();

                    foreach (var itemtelas in telasmodel)
                    {
                        var acoes = itens.Where(k => k.MODULO == item.descricao && k.TELA == itemtelas.tela)
                                                       .Select(b => new
                                                       {
                                                           b.ACAO
                                                       }).Distinct().ToList();
                        var listastring = new List<string>();
                        foreach (var itemacoes in acoes)
                        {
                            listastring.Add(itemacoes.ACAO.ToString());
                        }
                        itemtelas.acoes.AddRange(listastring);
                    }
                    mod.telas.AddRange(telasmodel);
                    lista.Add(mod);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int? GetUsuarioPermissaoTipo1e2(string ibge, int id_usuario)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                    conn.QueryFirstOrDefault<int>(_command.GetUsuarioPermissaoTipo1e2, new
                                    {
                                        @id_usuario = id_usuario
                                    }));
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertOrUpdate(string ibge, Seg_Perfil_Usuario model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                         conn.Execute(_command.InsertOrUpdate, new
                         {
                             @id = model.id,
                             @id_usuario = model.id_usuario,
                             @id_unidade = model.id_unidade,
                             @id_perfil = model.id_perfil
                         }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
