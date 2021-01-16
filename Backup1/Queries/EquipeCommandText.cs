using Imunizacao.Domain.Commands;

namespace Imunizacao.Domain.Queries
{
    public class EquipeCommandText : IEquipeCommand
    {
        public string sqlUsaEstruturaNova = $@"SELECT COUNT(*) QTDE
                                               FROM RDB$RELATIONS
                                               WHERE RDB$FLAGS=1 and RDB$RELATION_NAME='ESUS_FAMILIA'";

        string IEquipeCommand.UsaEstruturaNova { get => sqlUsaEstruturaNova; }

        public string sqlGetEquipeByCidadaoEstruturaVelha = $@"SELECT EQ.*, MED.CSI_NOMMED ACS
                                                               FROM TSI_CADPAC CP
                                                               JOIN ESUS_CADDOMICILIAR CAD ON CP.ID_ESUS_CADDOMICILIAR = CAD.ID
                                                               JOIN ESUS_MICROAREA MIC ON CAD.ID_MICROAREA = MIC.ID
                                                               JOIN ESUS_EQUIPES EQ ON MIC.ID_EQUIPE = EQ.ID
                                                               JOIN TSI_MEDICOS MED ON MED.CSI_CODMED = CAD.ID_PROFISSIONAL
                                                               WHERE CP.CSI_CODPAC = @id AND 
                                                                     MED.CSI_TIPO = 'Agente Comunitário'";

        string IEquipeCommand.GetEquipeByCidadaoEstruturaVelha { get => sqlGetEquipeByCidadaoEstruturaVelha; }

        public string sqlGetEquipeByCidadaoEstruturanova = $@"SELECT EQ.*, MED.CSI_NOMMED ACS
                                                              FROM TSI_CADPAC CP
                                                              JOIN ESUS_FAMILIA FAM ON CP.ID_FAMILIA = FAM.ID
                                                              JOIN VS_ESTABELECIMENTOS EST ON FAM.ID_DOMICILIO = EST.ID
                                                              JOIN ESUS_MICROAREA MIC ON EST.ID_MICROAREA = MIC.ID
                                                              JOIN ESUS_EQUIPES EQ ON MIC.ID_EQUIPE = EQ.ID
                                                              JOIN TSI_MEDICOS MED ON MED.CSI_CODMED = EST.ID_PROFISSIONAL
                                                              WHERE CP.CSI_CODPAC = @id AND 
                                                                    MED.CSI_TIPO = 'Agente Comunitário'";

        string IEquipeCommand.GetEquipeByCidadaoEstruturaNova { get => sqlGetEquipeByCidadaoEstruturanova; }
    }
}
