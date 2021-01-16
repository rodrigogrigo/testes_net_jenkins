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
    public class ViaAdmRepository : IViaAdmRepository
    {
        public IViaAdmCommand _command;
        public ViaAdmRepository(IViaAdmCommand command)
        {
            _command = command;
        }

        public List<ViaAdministracao> GetAllViaAdm(string ibge, string filtro)
        {
            try
            {
                var lista = new List<ViaAdministracao>();
                if (string.IsNullOrWhiteSpace(filtro))
                {
                    lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                conn.Query<ViaAdministracao>(_command.GetAllViaAdm.Replace("@filtro", string.Empty)).ToList());
                }
                else
                {
                    lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                               conn.Query<ViaAdministracao>(_command.GetAllViaAdm.Replace("@filtro", filtro)).ToList());
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<LocalAplicacao> GetLocalAplicacaoByViaAdm(string ibge, int? id)
        {
            try
            {
                var lista = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                conn.Query<LocalAplicacao>(_command.GetAllViaAdm, new { @id_via_adm = id }).ToList());

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
