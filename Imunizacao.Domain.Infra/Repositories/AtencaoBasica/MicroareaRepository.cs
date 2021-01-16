using Dapper;
using RgCidadao.Domain.Commands.AtencaoBasica;
using RgCidadao.Domain.Repositories.AtencaoBasica;
using RgCidadao.Domain.ViewModels.AtencaoBasica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RgCidadao.Domain.Infra.Repositories.AtencaoBasica
{
    public class MicroareaRepository : IMicroareaRepository
    {
        private IMicroareaCommand _command;
        public MicroareaRepository(IMicroareaCommand command)
        {
            _command = command;
        }

        public List<MicroareaViewModel> GetMicroareas(string ibge)
        {
            try
            {
                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                         conn.Query<MicroareaViewModel>(_command.GetMicroareas).ToList());

                return itens;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MicroareaViewModel> GetMicroareasByUnidade(string ibge, int id_unidade)
        {
            try
            {
                var itens = Helpers.HelperConnection.ExecuteCommand(ibge, conn =>
                         conn.Query<MicroareaViewModel>(_command.GetMicroareasByUnidade, new
                         {
                             @id_unidade = id_unidade,
                         }).ToList());

                return itens;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
