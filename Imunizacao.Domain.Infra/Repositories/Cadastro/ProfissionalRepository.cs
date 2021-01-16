using Dapper;
using RgCidadao.Domain.Commands.Cadastro;
using RgCidadao.Domain.Entities.Imunizacao;
using RgCidadao.Domain.Entities.Cadastro;
using RgCidadao.Domain.Repositories.Cadastro;
using RgCidadao.Domain.ViewModels.Cadastros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RgCidadao.Domain.Infra.Repositories.Cadastro
{
    public class ProfissionalRepository : IProfissionalRepository
    {
        private readonly IProfissionalCommand _profissionalcommand;

        public ProfissionalRepository(IProfissionalCommand commandText)
        {
            _profissionalcommand = commandText;
        }

        public List<ProfissionalViewModel> GetAll(string ibge, string filtro)
        {
            try
            {
                var prof = new List<ProfissionalViewModel>();
                if (string.IsNullOrWhiteSpace(filtro))
                {
                    prof = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.Query<ProfissionalViewModel>(_profissionalcommand.GetAll.Replace("@filtro", "")).ToList());
                }
                else
                {
                    prof = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.Query<ProfissionalViewModel>(_profissionalcommand.GetAll.Replace("@filtro", $" {filtro} ")).ToList());
                }

                return prof;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<CBO> GetCboProfissional(string ibge, int profissional)
        {
            try
            {
                var cbo = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                       conn.Query<CBO>(_profissionalcommand.GetListaCBO, new { @csi_codmed = profissional }).ToList());
                return cbo;
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
                    sql = _profissionalcommand.GetCountAll.Replace("@filtros", filtro);
                else
                    sql = _profissionalcommand.GetCountAll.Replace("@filtros", string.Empty);

                var prof = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                            conn.QueryFirstOrDefault<int>(sql));
                return prof;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProfissionalViewModel> GetProfissionalByUnidade(string ibge, int unidade)
        {
            try
            {
                var prof = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                       conn.Query<ProfissionalViewModel>(_profissionalcommand.GetProfissionalByUnidade, new { @unidade = unidade }).ToList());
                return prof;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProfissionalViewModel> GetProfissionalAtivoByUnidade(string ibge, int unidade)
        {
            try
            {
                var prof = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                       conn.Query<ProfissionalViewModel>(_profissionalcommand.GetProfissionalAtivoByUnidade, new { @unidade = unidade }).ToList());
                return prof;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProfissionalViewModel> GetProfissionalByUnidades(string ibge, string filtrounidade)
        {
            try
            {
                var prof = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                    conn.Query<ProfissionalViewModel>(_profissionalcommand.GetProfissionalByUnidades.Replace("@unidades", filtrounidade)).ToList());
                return prof;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProfissionalViewModel> GetProfissionalCBOByUnidade(string ibge, int unidade)
        {
            try
            {
                var prof = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                     conn.Query<ProfissionalViewModel>(_profissionalcommand.GetProfissionalCboByUnidade, new { @unidade = unidade }).ToList());
                return prof;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProfissionalViewModel> GetAllPagination(string ibge, int page, int pagesize, string filtro)
        {
            try
            {
                string sql = string.Empty;
                if (!string.IsNullOrWhiteSpace(filtro))
                    sql = _profissionalcommand.GetAllPagination.Replace("@filtros", filtro);
                else
                    sql = _profissionalcommand.GetAllPagination.Replace("@filtros", string.Empty);

                var prof = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                            conn.Query<ProfissionalViewModel>(sql, new
                            {
                                @pagesize = pagesize,
                                @page = page
                            }).ToList());
                return prof;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProfissionalViewModel> GetProfissionalByIdAndUnidade(string ibge, int unidade, int profissional)
        {
            try
            {
                var prof = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.Query<ProfissionalViewModel>(_profissionalcommand.GetProfissionalByIdAndUnidade, new
                          {
                              @unidade = unidade,
                              @id_profissional = profissional
                          }).ToList());

                return prof;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProfissionalViewModel> GetAllPaginationProfissionalWithCBO(string ibge, int page, int pagesize, string filtro)
        {
            try
            {
                string sql = string.Empty;
                if (!string.IsNullOrWhiteSpace(filtro))
                    sql = _profissionalcommand.GetAllPaginationProfissionalWithCBO.Replace("@filtro", filtro);
                else
                    sql = _profissionalcommand.GetAllPaginationProfissionalWithCBO.Replace("@filtro", string.Empty);

                var prof = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                            conn.Query<dynamic>(sql, new
                            {
                                @pagesize = pagesize,
                                @page = page
                            }).ToList());

                var groupprof = prof.GroupBy(x => new { x.CSI_CODMED, x.CSI_NOMMED })
                                    .Select(x => new
                                    {
                                        codigo = x.Select(p => p.CSI_CODMED).FirstOrDefault(),
                                        nome = x.Select(p => p.CSI_NOMMED).FirstOrDefault(),
                                        id_lotacao = x.Select(p => p.ID_LOTACAO).FirstOrDefault()
                                    })
                                    .ToList();

                var listaprofissionais = new List<ProfissionalViewModel>();
                foreach (var item in groupprof)
                {
                    var profissional = new ProfissionalViewModel();

                    profissional.csi_codmed = item.codigo;
                    profissional.csi_nommed = item.nome;
                    profissional.id_lotacao = item.id_lotacao;

                    var cbos = prof.Where(x => x.CSI_CODMED == item.codigo).Select(x => new CBO()
                    {
                        codigo = x.CSI_CBO,
                        descricao = x.DESC_CBO
                    }).ToList();

                    profissional.cbos.AddRange(cbos);

                    listaprofissionais.Add(profissional);
                }

                return listaprofissionais;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetCountAllProfissionalWithCBO(string ibge, string filtro)
        {
            try
            {
                string sql = string.Empty;
                if (!string.IsNullOrWhiteSpace(filtro))
                    sql = _profissionalcommand.GetCountAllProfissionalWithCBO.Replace("@filtro", filtro);
                else
                    sql = _profissionalcommand.GetCountAllProfissionalWithCBO.Replace("@filtro", string.Empty);

                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                            conn.QueryFirstOrDefault<int>(sql));
                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProfissionalViewModel> GetProfissionalByUnidadeWithCBO(string ibge, int unidade)
        {
            try
            {
                var prof = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                     conn.Query<ProfissionalViewModel>(_profissionalcommand.GetProfissionalByUnidadeWithCBO, new { @unidade = unidade }).ToList());

                return prof;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Profissional GetProfissionalById(string ibge, int profissional)
        {
            try
            {
                string sql = _profissionalcommand.GetProfissionalById;

                var Prof = Helpers.HelperConnection.ExecuteCommand<Profissional>(ibge, conn =>
                           conn.QueryFirstOrDefault<Profissional>(sql, new { @id_profissional = profissional }));

                return Prof;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProfissionalViewModel> GetProfissionalByEquipe(string ibge, string id)
        {
            try
            {
                var prof = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                       conn.Query<ProfissionalViewModel>(_profissionalcommand.GetProfissionalByEquipe, new { @id_equipe = id }).ToList());
                return prof;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ACSViewModel> GetACSByEstabelecimentoSaude(string ibge, int id_estabelecimento_saude)
        {
            try
            {
                var agentes_saude = Helpers.HelperConnection.ExecuteCommand(
                    ibge, conn => conn.Query<ACSViewModel>(
                        _profissionalcommand.GetACSByEstabelecimentoSaude,
                        new { @id = id_estabelecimento_saude }
                    ).ToList()
                );

                return agentes_saude;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProfissionalViewModel> GetCBOByMedicoUnidade(string ibge, int codmed, int coduni, string cbo)
        {
            try
            {
                string sql = _profissionalcommand.GetCBOByMedicoUnidade;
                string filtro = string.Empty;
                if (!string.IsNullOrWhiteSpace(cbo))
                    filtro += $@"AND MU.CSI_CBO = '{cbo}'";

                sql = sql.Replace("@filtro", filtro);
                var profissionais = Helpers.HelperConnection.ExecuteCommand(
                   ibge, conn => conn.Query<ProfissionalViewModel>(
                       sql,
                       new
                       {
                           @codmed = codmed,
                           @coduni = coduni
                       }
                   ).ToList()
               );

                return profissionais;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
