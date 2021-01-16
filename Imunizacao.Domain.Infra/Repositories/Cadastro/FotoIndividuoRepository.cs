using Dapper;
using RgCidadao.Domain.Commands.Cadastro;
using RgCidadao.Domain.Repositories.Cadastro;
using System;
using RgCidadao.Domain.Entities.Cadastro;

namespace RgCidadao.Domain.Infra.Repositories.Cadastro
{
    public class FotoIndividuoRepository : IFotoIndividuoRepository
    {
        private IFotoIndividuoCommand _command;
        public FotoIndividuoRepository(IFotoIndividuoCommand command)
        {
            _command = command;
        }

        public int GetNewId(string ibge)
        {
            try
            {
                int id = Helpers.HelperConnection.ExecuteCommandFoto(ibge, conn =>
                         conn.QueryFirstOrDefault<int>(_command.GetNewId));

                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateOrInsertByIdIndividuo(string ibge, FotoIndividuo model)
        {
            try
            {
                Helpers.HelperConnection.ExecuteCommandFoto(ibge, conn =>
                        conn.Execute(_command.UpdateOrInsertByIdIndividuo, new
                        {
                            @csi_matricula = model.csi_matricula,
                            @csi_tipo = model.csi_tipo,
                            @csi_foto = model.csi_foto,
                            @data_alteracao = model.data_alteracao,
                        }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FotoIndividuo GetByIdIndividuo(string ibge, int id_cidadao)
        {
            try
            {
                var foto = Helpers.HelperConnection.ExecuteCommandFoto(ibge, conn =>
                          conn.QueryFirstOrDefault<FotoIndividuo>(
                              _command.GetByIdIndividuo,
                              new { @id_cidadao = id_cidadao })
                          );

                return foto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
