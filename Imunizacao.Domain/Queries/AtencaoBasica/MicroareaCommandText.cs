using RgCidadao.Domain.Commands.AtencaoBasica;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Queries.AtencaoBasica
{
    public class MicroareaCommandText : IMicroareaCommand
    {
        public string sqlGetMicroareasByUnidade = $@"SELECT
                                                     MIC.ID ID_MICROAREA,
                                                     MIC.CODIGO CODIGO_MICROAREA,
                                                     MED.CSI_NOMMED PROFISSIONAL,
                                                     EQ.COD_INE, ES.CNES,
                                                     MED.CSI_CODMED ID_PROFISSIONAL,
                                                     EQ.ID ID_EQUIPE,
                                                     COALESCE(USU.USA_TABLET, 'F') USA_TABLET
                                                   FROM TSI_UNIDADE U
                                                   JOIN ESUS_ESTABELECIMENTO_SAUDE ES ON (U.CSI_CNES = ES.CNES)
                                                   JOIN ESUS_EQUIPES EQ ON (ES.ID = EQ.ID_ESTABELECIMENTO)
                                                   JOIN ESUS_MICROAREA MIC ON (EQ.ID = MIC.ID_EQUIPE)
                                                   JOIN TSI_MEDICOS MED ON (MIC.ID_PROFISSIONAL = MED.CSI_CODMED)
                                                   JOIN SEG_USUARIO USU ON (MED.CSI_IDUSER = USU.ID)
                                                   WHERE U.CSI_CODUNI = @id_unidade
                                                   AND COALESCE(MED.CSI_INATIVO, 'False') = 'False';";
        string IMicroareaCommand.GetMicroareasByUnidade { get => sqlGetMicroareasByUnidade; }

        public string sqlGetMicroareas = $@"SELECT
                                              MIC.ID ID_MICROAREA,
                                              MIC.CODIGO CODIGO_MICROAREA,
                                              MED.CSI_NOMMED PROFISSIONAL,
                                              EQ.COD_INE, ES.CNES,
                                              MED.CSI_CODMED ID_PROFISSIONAL,
                                              EQ.ID ID_EQUIPE,
                                              COALESCE(USU.USA_TABLET, 'F') USA_TABLET
                                            FROM TSI_UNIDADE U
                                            JOIN ESUS_ESTABELECIMENTO_SAUDE ES ON (U.CSI_CNES = ES.CNES)
                                            JOIN ESUS_EQUIPES EQ ON (ES.ID = EQ.ID_ESTABELECIMENTO)
                                            JOIN ESUS_MICROAREA MIC ON (EQ.ID = MIC.ID_EQUIPE)
                                            JOIN TSI_MEDICOS MED ON (MIC.ID_PROFISSIONAL = MED.CSI_CODMED)
                                            JOIN SEG_USUARIO USU ON (MED.CSI_IDUSER = USU.ID)
                                            AND COALESCE(MED.CSI_INATIVO, 'False') = 'False';";
        string IMicroareaCommand.GetMicroareas { get => sqlGetMicroareas; }

    }
}
