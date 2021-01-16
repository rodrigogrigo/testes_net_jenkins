using RgCidadao.Domain.Commands.Cadastro;

namespace RgCidadao.Domain.Queries.Cadastro
{
    public class FotoIndividuoCommandText : IFotoIndividuoCommand
    {
        public string sqlGetNewId = $@"SELECT GEN_ID(GEN_TSI_FOTOS_ID,1) AS VLR FROM RDB$DATABASE";
        string IFotoIndividuoCommand.GetNewId { get => sqlGetNewId; }

        public string sqlGetFotoIndividuo = $@"SELECT F.CSI_ID, F.CSI_MATRICULA, F.CSI_TIPO, F.CSI_FOTO, F.DATA_ALTERACAO
                                            FROM TSI_FOTOS F
                                            WHERE F.CSI_MATRICULA = @id_cidadao";

        string IFotoIndividuoCommand.GetByIdIndividuo { get => sqlGetFotoIndividuo; }

        public string sqlUpdateOrInsertFotoIndividuo = $@"UPDATE OR INSERT INTO TSI_FOTOS
                (CSI_MATRICULA, CSI_TIPO, CSI_FOTO, DATA_ALTERACAO)
                VALUES (@csi_matricula, @csi_tipo, @csi_foto, @data_alteracao)
                MATCHING (CSI_MATRICULA)";

        string IFotoIndividuoCommand.UpdateOrInsertByIdIndividuo { get => sqlUpdateOrInsertFotoIndividuo; }
    }
}
