using Dapper;
using RgCidadao.Domain.Commands.Seguranca;
using RgCidadao.Domain.Entities.Seguranca;
using RgCidadao.Domain.Repositories.Seguranca;
using RgCidadao.Domain.ViewModels.Seguranca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RgCidadao.Domain.Infra.Repositories.Seguranca
{
    public class PerfilRepository : IPerfilRepository
    {
        private IPerfilCommand _command;
        public PerfilRepository(IPerfilCommand command)
        {
            _command = command;
        }

        public List<Seg_Perfil_Acesso> GetAllPerfis(string ibge, string filtro)
        {
            try
            {
                string sql = string.Empty;
                if (!string.IsNullOrWhiteSpace(filtro))
                    sql = _command.GetAllPerfis.Replace("@filtro", filtro);
                else
                    sql = _command.GetAllPerfis.Replace("@filtro", string.Empty);

                List<Seg_Perfil_Acesso> lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                                          conn.Query<Seg_Perfil_Acesso>(sql)).ToList();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SegModuloViewModel> GetModulosByPerfil(string ibge, int id_perfil)
        {
            try

            {
                var lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.Query<dynamic>(_command.GetModulosByPerfil, new { @id_perfil = id_perfil }));

                var modulos = lista.Select(x => new
                {
                    id = x.ID_MODULO,
                    descricao = x.MODULO,
                }).Distinct().ToList();

                var listamodulos = new List<SegModuloViewModel>();
                foreach (var item in modulos)
                {
                    var mod = new SegModuloViewModel();
                    mod.id = item.id;
                    mod.descricao = item.descricao;

                    var telas = lista.Where(t => t.ID_MODULO == item.id)
                                   .Select(p => new
                                   {
                                       id = p.ID_TELA,
                                       descricao = p.DESCRICAO_TELA,
                                       nome = p.NOMETELA,
                                       id_modulo = item.id
                                   }).Distinct().ToList();

                    var telasmodel = telas.Select(x => new SegTelaViewModel()
                    {
                        id = x.id,
                        descricao = x.descricao,
                        nome = x.nome,
                        id_modulo = x.id_modulo
                    }).ToList();

                    foreach (var itemtelas in telasmodel)
                    {
                        var acoes = lista.Where(k => k.ID_MODULO == item.id && k.ID_TELA == itemtelas.id)
                                                       .Select(b => new SegAcoesViewModel()
                                                       {
                                                           id = b.ID_PERMISSAO_PERFIL,
                                                           descricao = b.DESCRICAO_ACAO,
                                                           nome = b.NOME_ACAO,
                                                           permissao = b.PERMISSAO
                                                       }).Distinct().ToList();
                        itemtelas.acoes.AddRange(acoes);
                    }
                    mod.telas.AddRange(telasmodel);
                    listamodulos.Add(mod);
                }

                return listamodulos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AtualizaPermissaoPerfil(string ibge, int permissao, int idpermissao)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.Execute(_command.AtualizaPermissaoPerfil, new
                          {
                              @id = idpermissao,
                              @permissao = permissao
                          }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertSegPerfilAcesso(string ibge, Seg_Perfil_Acesso model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.Execute(_command.InsertSegPerfilAcesso, new
                          {
                              @id = model.id,
                              @descricao = model.descricao
                          }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateSegPerfilAcesso(string ibge, Seg_Perfil_Acesso model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.Execute(_command.UpdateSegPerfilAcesso, new
                          {
                              @descricao = model.descricao,
                              @id = model.id
                          }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Seg_Perfil_Acesso GetPerfilById(string ibge, int id)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                 conn.QueryFirstOrDefault<Seg_Perfil_Acesso>(_command.GetPerfilById, new
                                 {
                                     @id = id
                                 }));
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int? GetPerfilNewId(string ibge)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                conn.QueryFirstOrDefault<int>(_command.GetPerfilNewId));
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Seg_Perfil_Acesso GetPerfilByDescricao(string ibge, string descricao)
        {
            try
            {
                var item = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                               conn.QueryFirstOrDefault<Seg_Perfil_Acesso>(_command.GetPerfilByDescricao, new
                               {
                                   @descricao = descricao
                               }));
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Seg_Perfil_Acesso> GetAllPagination(string ibge, int page, int pagesize, string filtro)
        {
            try
            {
                var lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                         conn.Query<Seg_Perfil_Acesso>(_command.GetPerfilPagination.Replace("@filtro", $"{filtro}"), new
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

        public int GetCountAll(string ibge, string filtro)
        {
            try
            {
                var lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                         conn.QueryFirstOrDefault<int>(_command.GetCountAll.Replace("@filtro", $"{filtro}")));
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
