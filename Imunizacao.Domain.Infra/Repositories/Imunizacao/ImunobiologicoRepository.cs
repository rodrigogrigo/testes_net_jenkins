using Dapper;
using RgCidadao.Domain.Commands.Imunizacao;
using RgCidadao.Domain.Entities.Imunizacao;
using RgCidadao.Domain.Repositories.Imunizacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RgCidadao.Domain.Infra.Repositories.Imunizacao
{
    public class ImunobiologicoRepository : IImunobiologicoRepository
    {
        private readonly IImunobiologicoCommand _imunobiologicocommand;
        public ImunobiologicoRepository(IImunobiologicoCommand commandText)
        {
            _imunobiologicocommand = commandText;
        }

        public List<Imunobiologico> GetAllImunobiologico(string ibge)
        {
            try
            {
                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                                    conn.Query<Imunobiologico>(_imunobiologicocommand.GetAllImunobiologico).ToList());

                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
