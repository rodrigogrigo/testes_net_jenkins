using Imunizacao.Domain.Commands;

namespace Imunizacao.Domain.Queries
{
    public class ProfissionalCommandText : IProfissionalCommand
    {
        public string SqlGetAll = $@"SELECT MED.CSI_CODMED, MED.CSI_NOMMED, CBO.CSI_CBO
                                     FROM TSI_MEDICOS MED
                                     JOIN TSI_MEDICOS_CBO CBO ON CBO.CSI_CODMED = MED.CSI_CODMED
                                     @filtro
                                     ORDER BY MED.CSI_NOMMED";

        string IProfissionalCommand.GetAll { get => SqlGetAll; }

        public string sqlGetByUnidade = $@"SELECT MED.CSI_CODMED, MED.CSI_NOMMED
                                           FROM TSI_MEDICOS MED
                                           JOIN TSI_MEDICOS_UNIDADE MED_UNI ON MED.CSI_CODMED = MED_UNI.CSI_CODMED
                                           WHERE MED_UNI.CSI_CODUNI = @unidade
                                           ORDER BY MED.CSI_NOMMED";
        string IProfissionalCommand.GetProfissionalByUnidade { get => sqlGetByUnidade; }

        public string sqlGetcbo = $@"SELECT CBO.*
                                     FROM TSI_MEDICOS_CBO MED_CBO
                                     JOIN TSI_CBO CBO ON CBO.CODIGO = MED_CBO.CSI_CBO
                                     WHERE CSI_CODMED = @csi_codmed";
        string IProfissionalCommand.GetListaCBO { get => sqlGetcbo; }

        public string sqlProfissionalCboByUnidade = $@"SELECT
                                                            MED_UNI.CSI_CODMED,
                                                            MED.CSI_NOMMED,
                                                            TRIM(CBO.DESCRICAO) DESCRICAO_CBO,
                                                            CBO.CODIGO CBO,
                                                            MED.CSI_IDUSER
                                                        FROM TSI_MEDICOS_UNIDADE MED_UNI
                                                        JOIN TSI_MEDICOS MED ON MED.CSI_CODMED = MED_UNI.CSI_CODMED
                                                        JOIN TSI_CBO CBO ON CBO.CODIGO = MED_UNI.CSI_CBO
                                                        WHERE MED_UNI.CSI_CODUNI = @unidade AND
                                                             (MED_UNI.CSI_CBO LIKE '2235%' OR
                                                              MED_UNI.CSI_CBO LIKE '3222%') AND
                                                              MED_UNI.CSI_ATIVADO = 'T'
                                                        ORDER BY MED.CSI_NOMMED";
        string IProfissionalCommand.GetProfissionalCboByUnidade { get => sqlProfissionalCboByUnidade; }
    }
}
