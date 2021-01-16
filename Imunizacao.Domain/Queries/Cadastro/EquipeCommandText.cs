using RgCidadao.Domain.Commands.Cadastro;

namespace RgCidadao.Domain.Queries.Cadastro
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

        public string sqlGetEquipesByBairro = $@"SELECT EQ.ID, EQ.NOME_REFERENCIA
                                                 FROM ESUS_BAIRRO_EQUIPE BE
                                                 JOIN ESUS_EQUIPES EQ ON (EQ.ID = BE.ID_EQUIPE)
                                                 WHERE BE.ID_BAIRRO = @id_bairro
                                                 GROUP BY EQ.ID, EQ.NOME_REFERENCIA";
        string IEquipeCommand.GetEquipesByBairro { get => sqlGetEquipesByBairro; }

        public string sqlGetEquipeByProfissional = $@"SELECT EQ.ID, EQ.NOME_REFERENCIA
                                                      FROM ESUS_EQUIPES EQ
                                                      WHERE EQ.ID_PROFISSIONAL = @id_profissional and
                                                            COALESCE(EQ.EXCLUIDO,'F') = 'F'";
        string IEquipeCommand.GetEquipeByProfissional { get => sqlGetEquipeByProfissional; }

        public string sqlGetEquipeByUnidade = $@"SELECT E.ID, E.SIGLA, E.DESCRICAO, E.COD_INE
                                                    FROM ESUS_ESTABELECIMENTO_SAUDE ES
                                                    JOIN TSI_UNIDADE U ON (ES.CNES = U.CSI_CNES)
                                                    JOIN ESUS_EQUIPES E ON (ES.ID = E.ID_ESTABELECIMENTO)
                                                    WHERE E.EXCLUIDO = 'F' AND U.CSI_CODUNI = @id";
        string IEquipeCommand.GetEquipeByUnidade { get => sqlGetEquipeByUnidade; }

        public string sqlGetEquipeByPerfil = $@" SELECT
                EQ.ID, EQ.DSC_AREA || ' - ' || EQ.COD_INE DESCRICAO_EQUIPE,
                EQ.DSC_AREA, EQ.SIGLA, EQ.TIPO, EQ.DESCRICAO
            FROM ESUS_EQUIPES EQ
            JOIN ESUS_ESTABELECIMENTO_SAUDE ES ON (EQ.ID_ESTABELECIMENTO = ES.ID)
            JOIN TSI_UNIDADE UN ON (ES.CNES = UN.CSI_CNES)
            @filtro
            ORDER BY DESCRICAO_EQUIPE";
        string IEquipeCommand.GetEquipeByPerfil { get => sqlGetEquipeByPerfil; }
    }
}
