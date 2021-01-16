using Dapper;
using RgCidadao.Domain.Commands.Cadastro;
using RgCidadao.Domain.Entities.Cadastro;
using RgCidadao.Domain.Repositories.Cadastro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RgCidadao.Domain.Infra.Repositories.Cadastro
{
    public class ProfissaoRepository : IProfissaoRepository
    {
        public IProfissaoCommand _profissao;
        public ProfissaoRepository(IProfissaoCommand prof)
        {
            _profissao = prof;
        }

        public List<Profissao> GetAll(string ibge)
        {
            try
            {
                var profissoes = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                            conn.Query<Profissao>(_profissao.GetAll).ToList());
                return profissoes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Profissao> GetAllPagination(string ibge, int page, int pagesize, string filtro)
        {
            try
            {
                var profissoes = new List<Profissao>();
                if (string.IsNullOrWhiteSpace(filtro))
                {
                    profissoes = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                            conn.Query<Profissao>(_profissao.GetAllPagination.Replace("@filtro", ""), new
                                            {
                                                @pagesize = pagesize,
                                                @page = page
                                            }).ToList());
                }
                else
                {
                    profissoes = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                          conn.Query<Profissao>(_profissao.GetAllPagination.Replace("@filtro", filtro), new
                                          {
                                              @pagesize = pagesize,
                                              @page = page
                                          }).ToList());
                }
                return profissoes;
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
                int count = 0;
                if (string.IsNullOrWhiteSpace(filtro))
                {
                    count = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                            conn.QueryFirstOrDefault<int>(_profissao.GetCountAll.Replace("@filtro", "")));
                }
                else
                {
                    count = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                          conn.QueryFirstOrDefault<int>(_profissao.GetCountAll.Replace("@filtro", filtro)));
                }
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
