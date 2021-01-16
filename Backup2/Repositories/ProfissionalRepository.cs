using Dapper;
using Imunizacao.Domain.Commands;
using Imunizacao.Domain.Entities;
using Imunizacao.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Imunizacao.Domain.Infra.Repositories
{
    public class ProfissionalRepository : IProfissionalRepository
    {
        private readonly IProfissionalCommand _profissionalcommand;

        public ProfissionalRepository(IProfissionalCommand commandText)
        {
            _profissionalcommand = commandText;
        }

        public List<Profissional> GetAll(string ibge, string filt)
        {
            try
            {
                var prof = new List<Profissional>();
                if (string.IsNullOrWhiteSpace(filt))
                {
                    prof = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.Query<Profissional>(_profissionalcommand.GetAll.Replace("@filtro", "")).ToList());
                }
                else
                {
                    prof = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                          conn.Query<Profissional>(_profissionalcommand.GetAll.Replace("@filtro", $" {filt} ")).ToList());
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

        public List<Profissional> GetProfissionalByUnidade(string ibge, int unidade)
        {
            try
            {
                var prof = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                       conn.Query<Profissional>(_profissionalcommand.GetProfissionalByUnidade, new { @unidade = unidade }).ToList());
                return prof;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Profissional> GetProfissionalCBOByUnidade(string ibge, int unidade)
        {
            try
            {
                var prof = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                     conn.Query<Profissional>(_profissionalcommand.GetProfissionalCboByUnidade, new { @unidade = unidade }).ToList());
                return prof;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
