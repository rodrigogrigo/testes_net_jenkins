using RgCidadao.Domain.Commands.Indicadores;

namespace RgCidadao.Domain.Queries.Indicadores
{
    public class Indicador3CommandText : IIndicador3Command
    {
        public string sqlIndicador3 = $@"@select
                                           COUNT(*) QTDE_GESTANTES,

                                           -- METAS (SISTEMA)
                                           SUM(CASE WHEN ( (
                                            --VERIFICA SE EXISTE ATENDIMENTO ODONTOLOGICO
                                           ((SELECT COUNT(DISTINCT AOI.ID)
                                           FROM ESUS_ATEND_ODONT_INDIVIDUAL AOI
                                           WHERE AOI.ID_CIDADAO = G.ID_CIDADAO AND
                                           (SUBSTRING(AOI.ID_CBO FROM 1 FOR 4) IN ('2232')) AND
                                           (AOI.DATA BETWEEN GI.DUM AND COALESCE(GI.DATA_NASCIMENTO,CURRENT_TIMESTAMP))) > 0)
                                           OR
                                           ((SELECT COUNT(DISTINCT O.CSI_ID)
                                           FROM TSI_ODONTOGRAMA O
                                           JOIN TSI_CONSULTAS C ON (C.CSI_CONTROLE = O.CSI_CONTROLE)
                                           WHERE C.CSI_STATUS = 'Consultado' AND
                                           C.CSI_CODPAC = G.ID_CIDADAO AND
                                           (SUBSTRING(C.CSI_CBO FROM 1 FOR 4) IN ('2232')) AND
                                           (C.CSI_DATACON BETWEEN GI.DUM AND COALESCE(GI.DATA_NASCIMENTO,CURRENT_TIMESTAMP))) > 0)
                                           OR
                                           ((SELECT COUNT(DISTINCT A.ID) FROM PEP_ATENDIMENTO A
                                           JOIN ESUS_IATEND_ODONT_INDIVIDUAL IOI ON ((A.ID = IOI.ID_ATEND_PRONTUARIO) OR (A.ID = IOI.ID_ATEND_PRONTUARIO_REALIZADO))
                                           WHERE A.ID_PACIENTE = G.ID_CIDADAO AND
                                           (SUBSTRING(A.ID_CBO FROM 1 FOR 4) IN ('2232')) AND
                                           (A.DATA BETWEEN GI.DUM AND COALESCE(GI.DATA_NASCIMENTO,CURRENT_TIMESTAMP))) > 0) )
                                           AND
                                           --VERIFICA SE EXISTE ATENDIMENTO INDIVIDUAL DE PN
                                           ((SELECT COUNT(DISTINCT PA.ID) FROM PEP_ATENDIMENTO PA
                                           JOIN PEP_CONDICAO_AVALIADA CA ON CA.CSI_ID_ATEND = PA.ID AND
                                           ((CA.ID_CIAP IN ('W03', 'W05', 'W29', 'W71', 'W78', 'W79', 'W80', 'W81', 'W84', 'W85')) OR
                                           (CA.ID_CID  IN ('O11', 'O120','0121','O122','O13', 'O140','O141','O149','O150','0151',
                                           'O159','O16', 'O200','O208','O209','O210','O211','O212','O218','O219',
                                           'O220','O221','O222','O223','O224','O225','O228','O229','O230','O231',
                                           'O232','O233','O234','O235','O239','O299','O300','O301','O302','O308',
                                           'O309','O311','O312','O318','O320','O321','O322','O323','O324','O325',
                                           'O326','O328','O329','O330','O331','O332','O333','O334','O335','O336',
                                           'O337','O338','O752','O753','O990','O991','O992','O993','O994','O240',
                                           'O241','O242','O243','O244','O249','O25', 'O260','O261','O263','O264',
                                           'O265','O268','O269','O280','O281','O282','O283','O284','O285','O288',
                                           'O289','O290','O291','O292','O293','O294','O295','O296','O298','O009',
                                           'O339','O340','O341','O342','O343','O344','O345','O346','O347','O348',
                                           'O349','O350','O351','O352','O353','O354','O355','O356','O357','O358',
                                           'O359','O360','O361','O362','O363','O365','O366','O367','O368','O369',
                                           'O40', 'O410','O411','O418','O419','O430','O431','O438','O439','O440',
                                           'O441','O460','O468','O469','O470','O471','O479','O48', 'O995','O996',
                                           'O997','Z640','O00', 'O10', 'O12', 'O14', 'O15', 'O20', 'O21', 'O22',
                                           'O23', 'O24', 'O26', 'O28', 'O29', 'O30', 'O31', 'O32', 'O33', 'O34',
                                           'O35', 'O36', 'O41', 'O43', 'O44', 'O46', 'O47', 'O98', 'Z34', 'Z35',
                                           'Z36', 'Z321','Z33', 'Z340','Z348','Z349','Z350','Z351','Z352','Z353',
                                           'Z354','Z357','Z358','Z359')))
                                           WHERE PA.ID_PACIENTE = G.ID_CIDADAO AND 
                                           (SUBSTRING(PA.ID_CBO FROM 1 FOR 4) IN ('2251','2252','2253','2231','2235')) AND
                                           (PA.DATA BETWEEN GI.DUM AND COALESCE(GI.DATA_NASCIMENTO, CURRENT_TIMESTAMP)) ) > 0)) THEN 1 ELSE 0 END) AS QTDE_METAS,

                                           -- METAS (VALIDAS)
                                           SUM(CASE WHEN ((
                                           ((SELECT COUNT(DISTINCT AOI.ID)
                                           FROM ESUS_ATEND_ODONT_INDIVIDUAL AOI
                                           JOIN ESUS_IATEND_ODONT_INDIVIDUAL IAOI ON (AOI.ID = IAOI.ID_ATEND_ODONT)
                                           JOIN ESUS_EXPORTACAO_ITEM EI ON EI.ID = IAOI.ID_ESUS_EXPORTACAO_ITEM
                                           JOIN ESUS_EXPORTACAO E ON E.ID = EI.ID_EXPORTACAO
                                           WHERE AOI.ID_CIDADAO = G.ID_CIDADAO AND
                                           (E.PROCESSADO_CRITICAS = 'T') AND
                                           ((EI.FLG_ERRO <> 'T') OR (EI.FLG_ERRO IS NULL)) AND
                                           ((CP.CSI_NCARTAO IS NOT NULL AND CP.CSI_NCARTAO <> '') OR
                                           (CP.CSI_CPFPAC IS NOT NULL AND CP.CSI_CPFPAC <> '')) AND
                                           (SUBSTRING(AOI.ID_CBO FROM 1 FOR 4) IN ('2232')) AND
                                           (AOI.DATA BETWEEN GI.DUM AND COALESCE(GI.DATA_NASCIMENTO,CURRENT_TIMESTAMP))) > 0)
                                           OR
                                           ((SELECT COUNT(DISTINCT O.CSI_ID)
                                           FROM TSI_ODONTOGRAMA O
                                           JOIN ESUS_EXPORTACAO_ITEM EI ON EI.ID = O.ID_ESUS_EXPORTACAO_ITEM
                                           JOIN ESUS_EXPORTACAO E ON E.ID = EI.ID_EXPORTACAO
                                           JOIN TSI_CONSULTAS C ON (C.CSI_CONTROLE = O.CSI_CONTROLE)
                                           WHERE C.CSI_STATUS = 'Consultado' AND
                                           C.CSI_CODPAC = G.ID_CIDADAO AND
                                           (E.PROCESSADO_CRITICAS = 'T') AND
                                           ((EI.FLG_ERRO <> 'T') OR (EI.FLG_ERRO IS NULL)) AND
                                           ((CP.CSI_NCARTAO IS NOT NULL AND CP.CSI_NCARTAO <> '') OR
                                           (CP.CSI_CPFPAC IS NOT NULL AND CP.CSI_CPFPAC <> '')) AND
                                           (SUBSTRING(C.CSI_CBO FROM 1 FOR 4) IN ('2232')) AND
                                           (C.CSI_DATACON BETWEEN GI.DUM AND COALESCE(GI.DATA_NASCIMENTO,CURRENT_TIMESTAMP))) > 0)
                                           OR
                                           ((SELECT COUNT(DISTINCT A.ID) FROM PEP_ATENDIMENTO A
                                           JOIN ESUS_EXPORTACAO_ITEM EI ON EI.ID = A.ID_ESUS_EXPORTACAO_ITEM
                                           JOIN ESUS_EXPORTACAO E ON E.ID = EI.ID_EXPORTACAO
                                           JOIN ESUS_IATEND_ODONT_INDIVIDUAL IOI ON ((A.ID = IOI.ID_ATEND_PRONTUARIO) OR (A.ID = IOI.ID_ATEND_PRONTUARIO_REALIZADO))
                                           WHERE A.ID_PACIENTE = G.ID_CIDADAO AND
                                           (E.PROCESSADO_CRITICAS = 'T') AND
                                           ((EI.FLG_ERRO <> 'T') OR (EI.FLG_ERRO IS NULL)) AND
                                           ((CP.CSI_NCARTAO IS NOT NULL AND CP.CSI_NCARTAO <> '') OR
                                           (CP.CSI_CPFPAC IS NOT NULL AND CP.CSI_CPFPAC <> '')) AND
                                           (SUBSTRING(A.ID_CBO FROM 1 FOR 4) IN ('2232')) AND
                                           (A.DATA BETWEEN GI.DUM AND COALESCE(GI.DATA_NASCIMENTO,CURRENT_TIMESTAMP))) > 0))
                                           AND
                                           ((SELECT COUNT(DISTINCT PA.ID) FROM PEP_ATENDIMENTO PA
                                           JOIN ESUS_EXPORTACAO_ITEM EI ON EI.ID = PA.ID_ESUS_EXPORTACAO_ITEM
                                           JOIN ESUS_EXPORTACAO E ON E.ID = EI.ID_EXPORTACAO
                                           JOIN PEP_CONDICAO_AVALIADA CA ON CA.CSI_ID_ATEND = PA.ID AND
                                           ((CA.ID_CIAP IN ('W03', 'W05', 'W29', 'W71', 'W78', 'W79', 'W80', 'W81', 'W84', 'W85')) OR
                                           (CA.ID_CID  IN ('O11', 'O120','0121','O122','O13', 'O140','O141','O149','O150','0151',
                                           'O159','O16', 'O200','O208','O209','O210','O211','O212','O218','O219',
                                           'O220','O221','O222','O223','O224','O225','O228','O229','O230','O231',
                                           'O232','O233','O234','O235','O239','O299','O300','O301','O302','O308',
                                           'O309','O311','O312','O318','O320','O321','O322','O323','O324','O325',
                                           'O326','O328','O329','O330','O331','O332','O333','O334','O335','O336',
                                           'O337','O338','O752','O753','O990','O991','O992','O993','O994','O240',
                                           'O241','O242','O243','O244','O249','O25', 'O260','O261','O263','O264',
                                           'O265','O268','O269','O280','O281','O282','O283','O284','O285','O288',
                                           'O289','O290','O291','O292','O293','O294','O295','O296','O298','O009',
                                           'O339','O340','O341','O342','O343','O344','O345','O346','O347','O348',
                                           'O349','O350','O351','O352','O353','O354','O355','O356','O357','O358',
                                           'O359','O360','O361','O362','O363','O365','O366','O367','O368','O369',
                                           'O40', 'O410','O411','O418','O419','O430','O431','O438','O439','O440',
                                           'O441','O460','O468','O469','O470','O471','O479','O48', 'O995','O996',
                                           'O997','Z640','O00', 'O10', 'O12', 'O14', 'O15', 'O20', 'O21', 'O22',
                                           'O23', 'O24', 'O26', 'O28', 'O29', 'O30', 'O31', 'O32', 'O33', 'O34',
                                           'O35', 'O36', 'O41', 'O43', 'O44', 'O46', 'O47', 'O98', 'Z34', 'Z35',
                                           'Z36', 'Z321','Z33', 'Z340','Z348','Z349','Z350','Z351','Z352','Z353',
                                           'Z354','Z357','Z358','Z359')))
                                           WHERE PA.ID_PACIENTE = G.ID_CIDADAO AND
                                           (E.PROCESSADO_CRITICAS = 'T') AND
                                           ((EI.FLG_ERRO <> 'T') OR (EI.FLG_ERRO IS NULL)) AND
                                           (SUBSTRING(PA.ID_CBO FROM 1 FOR 4) IN ('2251','2252','2253','2231','2235')) AND
                                           (PA.DATA BETWEEN GI.DUM AND COALESCE(GI.DATA_NASCIMENTO, CURRENT_TIMESTAMP))  ) > 0)) THEN 1 ELSE 0 END) AS QTDE_METAS_VALIDAS

                                           FROM GESTACAO G
                                           JOIN GESTACAO_ITEM GI ON (GI.ID_GESTACAO = G.ID) AND
                                           ((GI.FLG_DESFECHO > 0) AND (GI.DATA_NASCIMENTO BETWEEN
                                           (SELECT DATA_PRIMEIRO_DIA FROM PRIMEIRO_DIA((SELECT MES_ANTERIOR
                                           FROM COMPETENCIA_ANTERIOR(CURRENT_TIMESTAMP,11))))
                                           AND CURRENT_TIMESTAMP)
                                           OR (GI.FLG_DESFECHO = 0))
                                           JOIN TSI_CADPAC CP ON (G.ID_CIDADAO = CP.CSI_CODPAC)
                                           LEFT JOIN ESUS_ESTAB_HOSPITALARES EH ON EH.ID = GI.ID_MATERNIDADE
                                           LEFT JOIN ESUS_FAMILIA FAM ON FAM.ID = CP.ID_FAMILIA
                                           LEFT JOIN VS_ESTABELECIMENTOS EST ON EST.ID = FAM.ID_DOMICILIO
                                           LEFT JOIN ESUS_MICROAREA MI ON MI.ID = EST.ID_MICROAREA
                                           LEFT JOIN TSI_MEDICOS ME ON ME.CSI_CODMED = MI.ID_PROFISSIONAL
                                           LEFT JOIN ESUS_EQUIPES EQ ON EQ.ID = MI.ID_EQUIPE
                                           LEFT JOIN ESUS_ESTABELECIMENTO_SAUDE ES ON ES.ID = EQ.ID_ESTABELECIMENTO
                                           WHERE   
                                           @filtros
                                           @agrupamento";
        string IIndicador3Command.Indicador3 { get => sqlIndicador3; }

        public string sqlPublicoALvo = $@"@select TAB.GESTANTE,
                                                   TAB.DUM,
                                                   TAB.GI_DATA_NASCIMENTO,
                                                   TAB.ID_INDIVIDUO,
                                                   TAB.IDADE_GESTACIONAL,
                                                   TAB.DESFECHO,
                                                   TAB.ATENDIDO,
                                                   TAB.ATENDIDO_VALIDO
                                           FROM ( SELECT
                                           CP.CSI_NOMPAC AS GESTANTE,
                                           GI.DUM,
                                           GI.DATA_NASCIMENTO GI_DATA_NASCIMENTO,
                                           CP.CSI_CODPAC ID_INDIVIDUO,

                                           (CASE WHEN GI.FLG_DESFECHO = 0 THEN ((CURRENT_DATE - CAST(GI.DUM AS DATE))/7)
                                           WHEN GI.FLG_DESFECHO > 0 THEN ((CAST(GI.DATA_NASCIMENTO AS DATE) - CAST(GI.DUM AS DATE))/7) END) AS IDADE_GESTACIONAL,

                                           (CASE WHEN GI.FLG_DESFECHO = 0 THEN 'EM ANDAMENTO'
                                           WHEN GI.FLG_DESFECHO = 1 THEN 'ABORTO'
                                           WHEN GI.FLG_DESFECHO = 2 THEN 'CONCLUÍDO'
                                           WHEN GI.FLG_DESFECHO = 3 THEN 'ÓBITO AMBOS'
                                           WHEN GI.FLG_DESFECHO = 4 THEN 'ÓBITO FETAL'
                                           WHEN GI.FLG_DESFECHO = 5 THEN 'ÓBITO MATERNO'
                                           WHEN GI.FLG_DESFECHO = 6 THEN 'MUDANÇA DE TERRITÓRIO' END) AS DESFECHO,

                                           -- ATENDIMENTO (SISTEMA)
                                           CASE WHEN ((((SELECT COUNT(DISTINCT AOI.ID)
                                           FROM ESUS_ATEND_ODONT_INDIVIDUAL AOI
                                           WHERE AOI.ID_CIDADAO = G.ID_CIDADAO AND
                                           (SUBSTRING(AOI.ID_CBO FROM 1 FOR 4) IN ('2232')) AND
                                           (AOI.DATA BETWEEN GI.DUM AND COALESCE(GI.DATA_NASCIMENTO,CURRENT_TIMESTAMP))) > 0)
                                           OR
                                           ((SELECT COUNT(DISTINCT O.CSI_ID) FROM TSI_ODONTOGRAMA O
                                           JOIN TSI_CONSULTAS C ON (C.CSI_CONTROLE = O.CSI_CONTROLE)
                                           WHERE C.CSI_STATUS = 'Consultado' AND
                                           C.CSI_CODPAC = G.ID_CIDADAO AND
                                           (SUBSTRING(C.CSI_CBO FROM 1 FOR 4) IN ('2232')) AND
                                           (C.CSI_DATACON BETWEEN GI.DUM AND COALESCE(GI.DATA_NASCIMENTO,CURRENT_TIMESTAMP))) > 0)
                                           OR
                                           ((SELECT COUNT(DISTINCT A.ID) FROM PEP_ATENDIMENTO A
                                           JOIN ESUS_IATEND_ODONT_INDIVIDUAL IOI ON ((A.ID = IOI.ID_ATEND_PRONTUARIO) OR (A.ID = IOI.ID_ATEND_PRONTUARIO_REALIZADO))
                                           WHERE A.ID_PACIENTE = G.ID_CIDADAO AND
                                           (SUBSTRING(A.ID_CBO FROM 1 FOR 4) IN ('2232')) AND
                                           (A.DATA BETWEEN GI.DUM AND COALESCE(GI.DATA_NASCIMENTO,CURRENT_TIMESTAMP))) > 0))
                                           AND
                                           ((SELECT COUNT(DISTINCT PA.ID) FROM PEP_ATENDIMENTO PA
                                           JOIN PEP_CONDICAO_AVALIADA CA ON CA.CSI_ID_ATEND = PA.ID AND
                                           ((CA.ID_CIAP IN ('W03', 'W05', 'W29', 'W71', 'W78', 'W79', 'W80', 'W81', 'W84', 'W85')) OR
                                           (CA.ID_CID  IN ('O11', 'O120','0121','O122','O13', 'O140','O141','O149','O150','0151',
                                           'O159','O16', 'O200','O208','O209','O210','O211','O212','O218','O219',
                                           'O220','O221','O222','O223','O224','O225','O228','O229','O230','O231',
                                           'O232','O233','O234','O235','O239','O299','O300','O301','O302','O308',
                                           'O309','O311','O312','O318','O320','O321','O322','O323','O324','O325',
                                           'O326','O328','O329','O330','O331','O332','O333','O334','O335','O336',
                                           'O337','O338','O752','O753','O990','O991','O992','O993','O994','O240',
                                           'O241','O242','O243','O244','O249','O25', 'O260','O261','O263','O264',
                                           'O265','O268','O269','O280','O281','O282','O283','O284','O285','O288',
                                           'O289','O290','O291','O292','O293','O294','O295','O296','O298','O009',
                                           'O339','O340','O341','O342','O343','O344','O345','O346','O347','O348',
                                           'O349','O350','O351','O352','O353','O354','O355','O356','O357','O358',
                                           'O359','O360','O361','O362','O363','O365','O366','O367','O368','O369',
                                           'O40', 'O410','O411','O418','O419','O430','O431','O438','O439','O440',
                                           'O441','O460','O468','O469','O470','O471','O479','O48', 'O995','O996',
                                           'O997','Z640','O00', 'O10', 'O12', 'O14', 'O15', 'O20', 'O21', 'O22',
                                           'O23', 'O24', 'O26', 'O28', 'O29', 'O30', 'O31', 'O32', 'O33', 'O34',
                                           'O35', 'O36', 'O41', 'O43', 'O44', 'O46', 'O47', 'O98', 'Z34', 'Z35',
                                           'Z36', 'Z321','Z33', 'Z340','Z348','Z349','Z350','Z351','Z352','Z353',
                                           'Z354','Z357','Z358','Z359')))
                                           WHERE PA.ID_PACIENTE = G.ID_CIDADAO) > 0)) THEN 1 ELSE 0 END AS ATENDIDO,

                                           -- ATENDIMENTO (VALIDO)
                                           CASE WHEN ((((SELECT COUNT(DISTINCT AOI.ID)
                                           FROM ESUS_ATEND_ODONT_INDIVIDUAL AOI
                                           JOIN ESUS_IATEND_ODONT_INDIVIDUAL IAOI ON (AOI.ID = IAOI.ID_ATEND_ODONT)
                                           JOIN ESUS_EXPORTACAO_ITEM EI ON EI.ID = IAOI.ID_ESUS_EXPORTACAO_ITEM
                                           JOIN ESUS_EXPORTACAO E ON E.ID = EI.ID_EXPORTACAO
                                           WHERE AOI.ID_CIDADAO = G.ID_CIDADAO AND
                                           (E.PROCESSADO_CRITICAS = 'T') AND
                                           ((EI.FLG_ERRO <> 'T') OR (EI.FLG_ERRO IS NULL)) AND
                                           ((CP.CSI_NCARTAO IS NOT NULL AND CP.CSI_NCARTAO <> '') OR
                                           (CP.CSI_CPFPAC IS NOT NULL AND CP.CSI_CPFPAC <> '')) AND
                                           (SUBSTRING(AOI.ID_CBO FROM 1 FOR 4) IN ('2232')) AND
                                           (AOI.DATA BETWEEN GI.DUM AND COALESCE(GI.DATA_NASCIMENTO,CURRENT_TIMESTAMP))) > 0)
                                           OR
                                           ((SELECT COUNT(DISTINCT O.CSI_ID) FROM TSI_ODONTOGRAMA O
                                           JOIN ESUS_EXPORTACAO_ITEM EI ON EI.ID = O.ID_ESUS_EXPORTACAO_ITEM
                                           JOIN ESUS_EXPORTACAO E ON E.ID = EI.ID_EXPORTACAO
                                           JOIN TSI_CONSULTAS C ON (C.CSI_CONTROLE = O.CSI_CONTROLE)
                                           WHERE C.CSI_STATUS = 'Consultado' AND
                                           C.CSI_CODPAC = G.ID_CIDADAO AND
                                           (E.PROCESSADO_CRITICAS = 'T') AND
                                           ((EI.FLG_ERRO <> 'T') OR (EI.FLG_ERRO IS NULL)) AND
                                           ((CP.CSI_NCARTAO IS NOT NULL AND CP.CSI_NCARTAO <> '') OR
                                           (CP.CSI_CPFPAC IS NOT NULL AND CP.CSI_CPFPAC <> '')) AND
                                           (SUBSTRING(C.CSI_CBO FROM 1 FOR 4) IN ('2232')) AND
                                           (C.CSI_DATACON BETWEEN GI.DUM AND COALESCE(GI.DATA_NASCIMENTO,CURRENT_TIMESTAMP))) > 0)
                                           OR
                                           ((SELECT COUNT(DISTINCT A.ID) FROM PEP_ATENDIMENTO A
                                           JOIN ESUS_EXPORTACAO_ITEM EI ON EI.ID = A.ID_ESUS_EXPORTACAO_ITEM
                                           JOIN ESUS_EXPORTACAO E ON E.ID = EI.ID_EXPORTACAO
                                           JOIN ESUS_IATEND_ODONT_INDIVIDUAL IOI ON ((A.ID = IOI.ID_ATEND_PRONTUARIO) OR (A.ID = IOI.ID_ATEND_PRONTUARIO_REALIZADO))
                                           WHERE A.ID_PACIENTE = G.ID_CIDADAO AND
                                           (E.PROCESSADO_CRITICAS = 'T') AND
                                           ((EI.FLG_ERRO <> 'T') OR (EI.FLG_ERRO IS NULL)) AND
                                           ((CP.CSI_NCARTAO IS NOT NULL AND CP.CSI_NCARTAO <> '') OR
                                           (CP.CSI_CPFPAC IS NOT NULL AND CP.CSI_CPFPAC <> '')) AND
                                           (SUBSTRING(A.ID_CBO FROM 1 FOR 4) IN ('2232')) AND
                                           (A.DATA BETWEEN GI.DUM AND COALESCE(GI.DATA_NASCIMENTO,CURRENT_TIMESTAMP))) > 0))
                                           AND
                                           ((SELECT COUNT(DISTINCT PA.ID) FROM PEP_ATENDIMENTO PA
                                           JOIN ESUS_EXPORTACAO_ITEM EI ON EI.ID = PA.ID_ESUS_EXPORTACAO_ITEM
                                           JOIN ESUS_EXPORTACAO E ON E.ID = EI.ID_EXPORTACAO
                                           JOIN PEP_CONDICAO_AVALIADA CA ON CA.CSI_ID_ATEND = PA.ID AND
                                           ((CA.ID_CIAP IN ('W03', 'W05', 'W29', 'W71', 'W78', 'W79', 'W80', 'W81', 'W84', 'W85')) OR
                                           (CA.ID_CID  IN ('O11', 'O120','0121','O122','O13', 'O140','O141','O149','O150','0151',
                                           'O159','O16', 'O200','O208','O209','O210','O211','O212','O218','O219',
                                           'O220','O221','O222','O223','O224','O225','O228','O229','O230','O231',
                                           'O232','O233','O234','O235','O239','O299','O300','O301','O302','O308',
                                           'O309','O311','O312','O318','O320','O321','O322','O323','O324','O325',
                                           'O326','O328','O329','O330','O331','O332','O333','O334','O335','O336',
                                           'O337','O338','O752','O753','O990','O991','O992','O993','O994','O240',
                                           'O241','O242','O243','O244','O249','O25', 'O260','O261','O263','O264',
                                           'O265','O268','O269','O280','O281','O282','O283','O284','O285','O288',
                                           'O289','O290','O291','O292','O293','O294','O295','O296','O298','O009',
                                           'O339','O340','O341','O342','O343','O344','O345','O346','O347','O348',
                                           'O349','O350','O351','O352','O353','O354','O355','O356','O357','O358',
                                           'O359','O360','O361','O362','O363','O365','O366','O367','O368','O369',
                                           'O40', 'O410','O411','O418','O419','O430','O431','O438','O439','O440',
                                           'O441','O460','O468','O469','O470','O471','O479','O48', 'O995','O996',
                                           'O997','Z640','O00', 'O10', 'O12', 'O14', 'O15', 'O20', 'O21', 'O22',
                                           'O23', 'O24', 'O26', 'O28', 'O29', 'O30', 'O31', 'O32', 'O33', 'O34',
                                           'O35', 'O36', 'O41', 'O43', 'O44', 'O46', 'O47', 'O98', 'Z34', 'Z35',
                                           'Z36', 'Z321','Z33', 'Z340','Z348','Z349','Z350','Z351','Z352','Z353',
                                           'Z354','Z357','Z358','Z359')))
                                           WHERE PA.ID_PACIENTE = G.ID_CIDADAO AND
                                           (E.PROCESSADO_CRITICAS = 'T') AND
                                           ((EI.FLG_ERRO <> 'T') OR (EI.FLG_ERRO IS NULL))
                                           ) > 0)) THEN 1 ELSE 0 END AS ATENDIDO_VALIDO

                                           FROM GESTACAO G
                                           JOIN GESTACAO_ITEM GI ON (GI.ID_GESTACAO = G.ID) AND
                                           ((GI.FLG_DESFECHO > 0) AND (GI.DATA_NASCIMENTO BETWEEN
                                           (SELECT DATA_PRIMEIRO_DIA FROM PRIMEIRO_DIA((SELECT MES_ANTERIOR
                                           FROM COMPETENCIA_ANTERIOR(CURRENT_TIMESTAMP,11))))
                                           AND CURRENT_TIMESTAMP)
                                           OR (GI.FLG_DESFECHO = 0))
                                           JOIN TSI_CADPAC CP ON (G.ID_CIDADAO = CP.CSI_CODPAC)
                                           LEFT JOIN ESUS_ESTAB_HOSPITALARES EH ON EH.ID = GI.ID_MATERNIDADE
                                           LEFT JOIN ESUS_FAMILIA FAM ON FAM.ID = CP.ID_FAMILIA
                                           LEFT JOIN VS_ESTABELECIMENTOS EST ON EST.ID = FAM.ID_DOMICILIO
                                           LEFT JOIN ESUS_MICROAREA MI ON MI.ID = EST.ID_MICROAREA
                                           LEFT JOIN TSI_MEDICOS ME ON ME.CSI_CODMED = MI.ID_PROFISSIONAL
                                           LEFT JOIN ESUS_EQUIPES EQ ON EQ.ID = MI.ID_EQUIPE
                                           LEFT JOIN ESUS_ESTABELECIMENTO_SAUDE ES ON ES.ID = EQ.ID_ESTABELECIMENTO
                                           WHERE
                                           @filtros
                                           ORDER BY CP.CSI_NOMPAC)TAB
                                           @filtro_valido";
        string IIndicador3Command.PublicoAlvo { get => sqlPublicoALvo; }

        public string sqlCountPublicoALvo = $@"SELECT COUNT(*) FROM(
                                               SELECT TAB.CSI_NOMPAC, TAB.ATENDIDO_VALIDO, TAB.ATENDIDO FROM(
                                               SELECT CP.CSI_NOMPAC,
                                               -- ATENDIMENTO (SISTEMA)
                                               CASE WHEN ((((SELECT COUNT(DISTINCT AOI.ID)
                                               FROM ESUS_ATEND_ODONT_INDIVIDUAL AOI
                                               WHERE AOI.ID_CIDADAO = G.ID_CIDADAO AND
                                               (SUBSTRING(AOI.ID_CBO FROM 1 FOR 4) IN ('2232')) AND
                                               (AOI.DATA BETWEEN GI.DUM AND COALESCE(GI.DATA_NASCIMENTO,CURRENT_TIMESTAMP))) > 0)
                                               OR
                                               ((SELECT COUNT(DISTINCT O.CSI_ID) FROM TSI_ODONTOGRAMA O
                                               JOIN TSI_CONSULTAS C ON (C.CSI_CONTROLE = O.CSI_CONTROLE)
                                               WHERE C.CSI_STATUS = 'Consultado' AND
                                               C.CSI_CODPAC = G.ID_CIDADAO AND
                                               (SUBSTRING(C.CSI_CBO FROM 1 FOR 4) IN ('2232')) AND
                                               (C.CSI_DATACON BETWEEN GI.DUM AND COALESCE(GI.DATA_NASCIMENTO,CURRENT_TIMESTAMP))) > 0)
                                               OR
                                               ((SELECT COUNT(DISTINCT A.ID) FROM PEP_ATENDIMENTO A
                                               JOIN ESUS_IATEND_ODONT_INDIVIDUAL IOI ON ((A.ID = IOI.ID_ATEND_PRONTUARIO) OR (A.ID = IOI.ID_ATEND_PRONTUARIO_REALIZADO))
                                               WHERE A.ID_PACIENTE = G.ID_CIDADAO AND
                                               (SUBSTRING(A.ID_CBO FROM 1 FOR 4) IN ('2232')) AND
                                               (A.DATA BETWEEN GI.DUM AND COALESCE(GI.DATA_NASCIMENTO,CURRENT_TIMESTAMP))) > 0))
                                               AND
                                               ((SELECT COUNT(DISTINCT PA.ID) FROM PEP_ATENDIMENTO PA
                                               JOIN PEP_CONDICAO_AVALIADA CA ON CA.CSI_ID_ATEND = PA.ID AND
                                               ((CA.ID_CIAP IN ('W03', 'W05', 'W29', 'W71', 'W78', 'W79', 'W80', 'W81', 'W84', 'W85')) OR
                                               (CA.ID_CID  IN ('O11', 'O120','0121','O122','O13', 'O140','O141','O149','O150','0151',
                                               'O159','O16', 'O200','O208','O209','O210','O211','O212','O218','O219',
                                               'O220','O221','O222','O223','O224','O225','O228','O229','O230','O231',
                                               'O232','O233','O234','O235','O239','O299','O300','O301','O302','O308',
                                               'O309','O311','O312','O318','O320','O321','O322','O323','O324','O325',
                                               'O326','O328','O329','O330','O331','O332','O333','O334','O335','O336',
                                               'O337','O338','O752','O753','O990','O991','O992','O993','O994','O240',
                                               'O241','O242','O243','O244','O249','O25', 'O260','O261','O263','O264',
                                               'O265','O268','O269','O280','O281','O282','O283','O284','O285','O288',
                                               'O289','O290','O291','O292','O293','O294','O295','O296','O298','O009',
                                               'O339','O340','O341','O342','O343','O344','O345','O346','O347','O348',
                                               'O349','O350','O351','O352','O353','O354','O355','O356','O357','O358',
                                               'O359','O360','O361','O362','O363','O365','O366','O367','O368','O369',
                                               'O40', 'O410','O411','O418','O419','O430','O431','O438','O439','O440',
                                               'O441','O460','O468','O469','O470','O471','O479','O48', 'O995','O996',
                                               'O997','Z640','O00', 'O10', 'O12', 'O14', 'O15', 'O20', 'O21', 'O22',
                                               'O23', 'O24', 'O26', 'O28', 'O29', 'O30', 'O31', 'O32', 'O33', 'O34',
                                               'O35', 'O36', 'O41', 'O43', 'O44', 'O46', 'O47', 'O98', 'Z34', 'Z35',
                                               'Z36', 'Z321','Z33', 'Z340','Z348','Z349','Z350','Z351','Z352','Z353',
                                               'Z354','Z357','Z358','Z359')))
                                               WHERE PA.ID_PACIENTE = G.ID_CIDADAO) > 0)) THEN 1 ELSE 0 END AS ATENDIDO,

                                               CASE WHEN ((((SELECT COUNT(DISTINCT AOI.ID)
                                               FROM ESUS_ATEND_ODONT_INDIVIDUAL AOI
                                               JOIN ESUS_IATEND_ODONT_INDIVIDUAL IAOI ON (AOI.ID = IAOI.ID_ATEND_ODONT)
                                               JOIN ESUS_EXPORTACAO_ITEM EI ON EI.ID = IAOI.ID_ESUS_EXPORTACAO_ITEM
                                               JOIN ESUS_EXPORTACAO E ON E.ID = EI.ID_EXPORTACAO
                                               WHERE AOI.ID_CIDADAO = G.ID_CIDADAO AND
                                               (E.PROCESSADO_CRITICAS = 'T') AND
                                               ((EI.FLG_ERRO <> 'T') OR (EI.FLG_ERRO IS NULL)) AND
                                               ((CP.CSI_NCARTAO IS NOT NULL AND CP.CSI_NCARTAO <> '') OR
                                               (CP.CSI_CPFPAC IS NOT NULL AND CP.CSI_CPFPAC <> '')) AND
                                               (SUBSTRING(AOI.ID_CBO FROM 1 FOR 4) IN ('2232')) AND
                                               (AOI.DATA BETWEEN GI.DUM AND COALESCE(GI.DATA_NASCIMENTO,CURRENT_TIMESTAMP))) > 0)
                                               OR
                                               ((SELECT COUNT(DISTINCT O.CSI_ID) FROM TSI_ODONTOGRAMA O
                                               JOIN ESUS_EXPORTACAO_ITEM EI ON EI.ID = O.ID_ESUS_EXPORTACAO_ITEM
                                               JOIN ESUS_EXPORTACAO E ON E.ID = EI.ID_EXPORTACAO
                                               JOIN TSI_CONSULTAS C ON (C.CSI_CONTROLE = O.CSI_CONTROLE)
                                               WHERE C.CSI_STATUS = 'Consultado' AND
                                               C.CSI_CODPAC = G.ID_CIDADAO AND
                                               (E.PROCESSADO_CRITICAS = 'T') AND
                                               ((EI.FLG_ERRO <> 'T') OR (EI.FLG_ERRO IS NULL)) AND
                                               ((CP.CSI_NCARTAO IS NOT NULL AND CP.CSI_NCARTAO <> '') OR
                                               (CP.CSI_CPFPAC IS NOT NULL AND CP.CSI_CPFPAC <> '')) AND
                                               (SUBSTRING(C.CSI_CBO FROM 1 FOR 4) IN ('2232')) AND
                                               (C.CSI_DATACON BETWEEN GI.DUM AND COALESCE(GI.DATA_NASCIMENTO,CURRENT_TIMESTAMP))) > 0)
                                               OR
                                               ((SELECT COUNT(DISTINCT A.ID) FROM PEP_ATENDIMENTO A
                                               JOIN ESUS_EXPORTACAO_ITEM EI ON EI.ID = A.ID_ESUS_EXPORTACAO_ITEM
                                               JOIN ESUS_EXPORTACAO E ON E.ID = EI.ID_EXPORTACAO
                                               JOIN ESUS_IATEND_ODONT_INDIVIDUAL IOI ON ((A.ID = IOI.ID_ATEND_PRONTUARIO) OR (A.ID = IOI.ID_ATEND_PRONTUARIO_REALIZADO))
                                               WHERE A.ID_PACIENTE = G.ID_CIDADAO AND
                                               (E.PROCESSADO_CRITICAS = 'T') AND
                                               ((EI.FLG_ERRO <> 'T') OR (EI.FLG_ERRO IS NULL)) AND
                                               ((CP.CSI_NCARTAO IS NOT NULL AND CP.CSI_NCARTAO <> '') OR
                                               (CP.CSI_CPFPAC IS NOT NULL AND CP.CSI_CPFPAC <> '')) AND
                                               (SUBSTRING(A.ID_CBO FROM 1 FOR 4) IN ('2232')) AND
                                               (A.DATA BETWEEN GI.DUM AND COALESCE(GI.DATA_NASCIMENTO,CURRENT_TIMESTAMP))) > 0))
                                               AND
                                               ((SELECT COUNT(DISTINCT PA.ID) FROM PEP_ATENDIMENTO PA
                                               JOIN ESUS_EXPORTACAO_ITEM EI ON EI.ID = PA.ID_ESUS_EXPORTACAO_ITEM
                                               JOIN ESUS_EXPORTACAO E ON E.ID = EI.ID_EXPORTACAO
                                               JOIN PEP_CONDICAO_AVALIADA CA ON CA.CSI_ID_ATEND = PA.ID AND
                                               ((CA.ID_CIAP IN ('W03', 'W05', 'W29', 'W71', 'W78', 'W79', 'W80', 'W81', 'W84', 'W85')) OR
                                               (CA.ID_CID  IN ('O11', 'O120','0121','O122','O13', 'O140','O141','O149','O150','0151',
                                               'O159','O16', 'O200','O208','O209','O210','O211','O212','O218','O219',
                                               'O220','O221','O222','O223','O224','O225','O228','O229','O230','O231',
                                               'O232','O233','O234','O235','O239','O299','O300','O301','O302','O308',
                                               'O309','O311','O312','O318','O320','O321','O322','O323','O324','O325',
                                               'O326','O328','O329','O330','O331','O332','O333','O334','O335','O336',
                                               'O337','O338','O752','O753','O990','O991','O992','O993','O994','O240',
                                               'O241','O242','O243','O244','O249','O25', 'O260','O261','O263','O264',
                                               'O265','O268','O269','O280','O281','O282','O283','O284','O285','O288',
                                               'O289','O290','O291','O292','O293','O294','O295','O296','O298','O009',
                                               'O339','O340','O341','O342','O343','O344','O345','O346','O347','O348',
                                               'O349','O350','O351','O352','O353','O354','O355','O356','O357','O358',
                                               'O359','O360','O361','O362','O363','O365','O366','O367','O368','O369',
                                               'O40', 'O410','O411','O418','O419','O430','O431','O438','O439','O440',
                                               'O441','O460','O468','O469','O470','O471','O479','O48', 'O995','O996',
                                               'O997','Z640','O00', 'O10', 'O12', 'O14', 'O15', 'O20', 'O21', 'O22',
                                               'O23', 'O24', 'O26', 'O28', 'O29', 'O30', 'O31', 'O32', 'O33', 'O34',
                                               'O35', 'O36', 'O41', 'O43', 'O44', 'O46', 'O47', 'O98', 'Z34', 'Z35',
                                               'Z36', 'Z321','Z33', 'Z340','Z348','Z349','Z350','Z351','Z352','Z353',
                                               'Z354','Z357','Z358','Z359')))
                                               WHERE PA.ID_PACIENTE = G.ID_CIDADAO AND
                                               (E.PROCESSADO_CRITICAS = 'T') AND
                                               ((EI.FLG_ERRO <> 'T') OR (EI.FLG_ERRO IS NULL))
                                               ) > 0)) THEN 1 ELSE 0 END AS ATENDIDO_VALIDO 

                                               FROM GESTACAO G
                                               JOIN GESTACAO_ITEM GI ON (GI.ID_GESTACAO = G.ID) AND
                                               ((GI.FLG_DESFECHO > 0) AND (GI.DATA_NASCIMENTO BETWEEN
                                               (SELECT DATA_PRIMEIRO_DIA FROM PRIMEIRO_DIA((SELECT MES_ANTERIOR
                                               FROM COMPETENCIA_ANTERIOR(CURRENT_TIMESTAMP,11))))
                                               AND CURRENT_TIMESTAMP)
                                               OR (GI.FLG_DESFECHO = 0))
                                               JOIN TSI_CADPAC CP ON (G.ID_CIDADAO = CP.CSI_CODPAC)
                                               LEFT JOIN ESUS_ESTAB_HOSPITALARES EH ON EH.ID = GI.ID_MATERNIDADE
                                               LEFT JOIN ESUS_FAMILIA FAM ON FAM.ID = CP.ID_FAMILIA
                                               LEFT JOIN VS_ESTABELECIMENTOS EST ON EST.ID = FAM.ID_DOMICILIO
                                               LEFT JOIN ESUS_MICROAREA MI ON MI.ID = EST.ID_MICROAREA
                                               LEFT JOIN TSI_MEDICOS ME ON ME.CSI_CODMED = MI.ID_PROFISSIONAL
                                               LEFT JOIN ESUS_EQUIPES EQ ON EQ.ID = MI.ID_EQUIPE
                                               LEFT JOIN ESUS_ESTABELECIMENTO_SAUDE ES ON ES.ID = EQ.ID_ESTABELECIMENTO
                                               WHERE @filtros) TAB
                                               @filtro_valido)";
        string IIndicador3Command.CountPublicoAlvo { get => sqlCountPublicoALvo; }

        public string sqlAtendimentos = $@"SELECT * FROM( SELECT
                                             TRIM('odontologico') TIPO_ATENDIMENTO,
                                             AOI.DATA,
                                             MED.CSI_NOMMED PROFISSIONAL,
                                             AOI.ID_CBO CBO,
                                             CBO.DESCRICAO DESCRICAO_CBO,
                                             '--' CONDICAO_AVALIADA,
                                             PROC.NOME PROCEDIMENTO,
                                             EQ.DSC_AREA EQUIPE,
                                             U.CSI_NOMUNI UNIDADE_SAUDE,
                                             CASE WHEN (COALESCE(E.PROCESSADO_CRITICAS, 'F') = 'T') THEN 1 ELSE 0 END AS PROCESSADO_CRITICAS,
                                             CASE WHEN (COALESCE(EI.FLG_ERRO, 'F') = 'T') THEN 1 ELSE 0 END AS FLG_ERRO,
                                             
                                             CASE WHEN (E.ID IS NULL) THEN 'Registro não exportado'
                                             WHEN (COALESCE(E.PROCESSADO_CRITICAS, 'F') = 'T') THEN EI.DESCRICAO_ERRO
                                             ELSE 'Arquivo de críticas não processado'
                                             END AS DESCRICAO_ERRO,
                                         
                                             CASE WHEN (SUBSTRING(AOI.ID_CBO FROM 1 FOR 4) IN ('2232')) THEN 1 ELSE 0 END AS CBO_VALIDO,
                                             1 AS CID_CIAP_VALIDO,
                                         
                                             CASE WHEN ((SELECT COUNT(DISTINCT AUX_1.ID)
                                             FROM ESUS_ATEND_ODONT_INDIVIDUAL AUX_1
                                             WHERE AUX_1.ID = AOI.ID AND
                                             (SUBSTRING(AUX_1.ID_CBO FROM 1 FOR 4) IN ('2232'))) > 0)
                                             THEN 1 ELSE 0 END AS INDICADOR,
                                         
                                             CASE WHEN ((SELECT COUNT(DISTINCT AUX_1.ID)
                                             FROM ESUS_ATEND_ODONT_INDIVIDUAL AUX_1
                                             JOIN ESUS_IATEND_ODONT_INDIVIDUAL AUX_2 ON (AUX_1.ID = AUX_2.ID_ATEND_ODONT)
                                             LEFT JOIN ESUS_EXPORTACAO_ITEM EI ON EI.ID = AUX_2.ID_ESUS_EXPORTACAO_ITEM
                                             LEFT JOIN ESUS_EXPORTACAO E ON E.ID = EI.ID_EXPORTACAO
                                             WHERE AUX_1.ID = AOI.ID AND
                                             (E.PROCESSADO_CRITICAS = 'T') AND
                                             ((EI.FLG_ERRO <> 'T') OR (EI.FLG_ERRO IS NULL)) AND
                                             (SUBSTRING(AUX_1.ID_CBO FROM 1 FOR 4) IN ('2232'))) > 0)
                                             THEN 1 ELSE 0 END AS INDICADOR_VALIDO,
                                             E.LOTE AS LOTE
                                         
                                         FROM ESUS_ATEND_ODONT_INDIVIDUAL AOI
                                         JOIN ESUS_IATEND_ODONT_INDIVIDUAL IAOI ON (AOI.ID = IAOI.ID_ATEND_ODONT)
                                         JOIN TSI_PROCEDIMENTO PROC ON (IAOI.ID_PROCEDIMENTO = PROC.CODIGO)
                                         JOIN TSI_MEDICOS MED ON (AOI.ID_PROFISSIONAL = MED.CSI_CODMED)
                                         JOIN TSI_CBO CBO ON (AOI.ID_CBO = CBO.CODIGO)
                                         LEFT JOIN ESUS_EQUIPES EQ ON (AOI.ID_EQUIPE = EQ.ID)
                                         LEFT JOIN TSI_UNIDADE U ON (AOI.ID_UNIDADE = U.CSI_CODUNI)
                                         LEFT JOIN ESUS_EXPORTACAO_ITEM EI ON EI.ID = IAOI.ID_ESUS_EXPORTACAO_ITEM
                                         LEFT JOIN ESUS_EXPORTACAO E ON E.ID = EI.ID_EXPORTACAO
                                         WHERE AOI.ID_CIDADAO = @id_individuo AND
                                         (AOI.DATA BETWEEN @data_dum AND COALESCE(@gi_data_nascimento,CURRENT_TIMESTAMP))
                                         
                                         UNION ALL
                                         
                                         SELECT
                                             TRIM('odontologico') TIPO_ATENDIMENTO,
                                             C.CSI_DATACON DATA,
                                             MED.CSI_NOMMED PROFISSIONAL,
                                             C.CSI_CBO CBO,
                                             CBO.DESCRICAO DESCRICAO_CBO,
                                             '--' CONDICAO_AVALIADA,
                                             PROC.NOME PROCEDIMENTO,
                                             NULL AS EQUIPE,
                                             U.CSI_NOMUNI UNIDADE_SAUDE,
                                             CASE WHEN (COALESCE(E.PROCESSADO_CRITICAS, 'F') = 'T') THEN 1 ELSE 0 END AS PROCESSADO_CRITICAS,
                                             CASE WHEN (COALESCE(EI.FLG_ERRO, 'F') = 'T') THEN 1 ELSE 0 END AS FLG_ERRO,
                                             
                                             CASE WHEN (E.ID IS NULL) THEN 'Registro não exportado'
                                             WHEN (COALESCE(E.PROCESSADO_CRITICAS, 'F') = 'T') THEN EI.DESCRICAO_ERRO
                                             ELSE 'Arquivo de críticas não processado'
                                             END AS DESCRICAO_ERRO,
                                         
                                             CASE WHEN (SUBSTRING(C.CSI_CBO FROM 1 FOR 4) IN ('2232')) THEN 1 ELSE 0 END AS CBO_VALIDO,
                                             1 AS CID_CIAP_VALIDO,
                                         
                                             CASE WHEN ((SELECT COUNT(DISTINCT AUX.CSI_ID)
                                             FROM TSI_ODONTOGRAMA AUX
                                             JOIN TSI_CONSULTAS C ON (C.CSI_CONTROLE = O.CSI_CONTROLE)
                                             WHERE C.CSI_STATUS = 'Consultado' AND
                                             AUX.CSI_ID = O.CSI_ID AND
                                             (SUBSTRING(C.CSI_CBO FROM 1 FOR 4) IN ('2232'))) > 0) THEN 1 ELSE 0 END AS INDICADOR,
                                         
                                             CASE WHEN ((SELECT COUNT(DISTINCT AUX.CSI_ID)
                                             FROM TSI_ODONTOGRAMA AUX
                                             JOIN TSI_CONSULTAS C ON (C.CSI_CONTROLE = O.CSI_CONTROLE)
                                             LEFT JOIN ESUS_EXPORTACAO_ITEM EI ON EI.ID = AUX.ID_ESUS_EXPORTACAO_ITEM
                                             LEFT JOIN ESUS_EXPORTACAO E ON E.ID = EI.ID_EXPORTACAO
                                             WHERE C.CSI_STATUS = 'Consultado' AND
                                             (E.PROCESSADO_CRITICAS = 'T') AND
                                             ((EI.FLG_ERRO <> 'T') OR (EI.FLG_ERRO IS NULL)) AND
                                             AUX.CSI_ID = O.CSI_ID AND
                                             (SUBSTRING(C.CSI_CBO FROM 1 FOR 4) IN ('2232'))) > 0) THEN 1 ELSE 0 END AS INDICADOR_VALIDO,
                                             E.LOTE AS LOTE
                                         
                                         FROM TSI_ODONTOGRAMA O
                                         JOIN TSI_CONSULTAS C ON (C.CSI_CONTROLE = O.CSI_CONTROLE)
                                         JOIN TSI_PROCEDIMENTO PROC ON (O.CSI_PROCEDIMENTO = PROC.CODIGO)
                                         JOIN TSI_MEDICOS MED ON (C.CSI_CODMED = MED.CSI_CODMED)
                                         JOIN TSI_CBO CBO ON (C.CSI_CBO = CBO.CODIGO)
                                         LEFT JOIN TSI_UNIDADE U ON (C.CSI_UNIDADE_AGENDAMENTO = U.CSI_CODUNI)
                                         LEFT JOIN ESUS_EXPORTACAO_ITEM EI ON EI.ID = O.ID_ESUS_EXPORTACAO_ITEM
                                         LEFT JOIN ESUS_EXPORTACAO E ON E.ID = EI.ID_EXPORTACAO
                                         WHERE C.CSI_STATUS = 'Consultado' AND
                                         C.CSI_CODPAC = @id_individuo AND
                                         (C.CSI_DATACON BETWEEN @data_dum AND COALESCE(@gi_data_nascimento,CURRENT_TIMESTAMP))
                                         
                                         UNION ALL
                                         
                                         SELECT
                                             TRIM('individual') TIPO_ATENDIMENTO,
                                             PA.DATA,
                                             MED.CSI_NOMMED PROFISSIONAL,
                                             PA.ID_CBO CBO,
                                             CBO.DESCRICAO DESCRICAO_CBO,
                                         
                                             COALESCE((SELECT 'CIAP: '||LIST(COALESCE(CA.ID_CIAP, '')) || ' | ' || 'CID: '||LIST(COALESCE(CA.ID_CID, ''))
                                             FROM PEP_CONDICAO_AVALIADA CA WHERE CA.CSI_ID_ATEND = PA.ID AND (CA.ID_CIAP IS NOT NULL OR CA.ID_CID IS NOT NULL)), '--') AS CONDICAO_AVALIADA,
                                         
                                             'ATENDIMENTO INDIVIDUAL' PROCEDIMENTO,
                                             EQ.DSC_AREA EQUIPE,
                                             U.CSI_NOMUNI UNIDADE_SAUDE,
                                             CASE WHEN (COALESCE(E.PROCESSADO_CRITICAS, 'F') = 'T') THEN 1 ELSE 0 END AS PROCESSADO_CRITICAS,
                                             CASE WHEN (COALESCE(EI.FLG_ERRO, 'F') = 'T') THEN 1 ELSE 0 END AS FLG_ERRO,
                                             
                                             CASE WHEN (COALESCE(E.PROCESSADO_CRITICAS, 'F') = 'T')
                                             THEN EI.DESCRICAO_ERRO ELSE 'Arquivo de críticas não processado'
                                             END AS DESCRICAO_ERRO,
                                         
                                             CASE WHEN (SUBSTRING(PA.ID_CBO FROM 1 FOR 4) IN ('2251','2252','2253','2231','2235'))
                                             THEN 1 ELSE 0 END AS CBO_VALIDO,
                                         
                                             CASE WHEN ((SELECT COUNT(*) FROM PEP_ATENDIMENTO AUX_PA
                                             JOIN PEP_CONDICAO_AVALIADA CA ON CA.CSI_ID_ATEND = AUX_PA.ID AND
                                             ((CA.ID_CIAP IN ('W03', 'W05', 'W29', 'W71', 'W78', 'W79', 'W80', 'W81', 'W84', 'W85')) OR
                                             (CA.ID_CID  IN ('O11', 'O120','0121','O122','O13', 'O140','O141','O149','O150','0151',
                                             'O159','O16', 'O200','O208','O209','O210','O211','O212','O218','O219',
                                             'O220','O221','O222','O223','O224','O225','O228','O229','O230','O231',
                                             'O232','O233','O234','O235','O239','O299','O300','O301','O302','O308',
                                             'O309','O311','O312','O318','O320','O321','O322','O323','O324','O325',
                                             'O326','O328','O329','O330','O331','O332','O333','O334','O335','O336',
                                             'O337','O338','O752','O753','O990','O991','O992','O993','O994','O240',
                                             'O241','O242','O243','O244','O249','O25', 'O260','O261','O263','O264',
                                             'O265','O268','O269','O280','O281','O282','O283','O284','O285','O288',
                                             'O289','O290','O291','O292','O293','O294','O295','O296','O298','O009',
                                             'O339','O340','O341','O342','O343','O344','O345','O346','O347','O348',
                                             'O349','O350','O351','O352','O353','O354','O355','O356','O357','O358',
                                             'O359','O360','O361','O362','O363','O365','O366','O367','O368','O369',
                                             'O40', 'O410','O411','O418','O419','O430','O431','O438','O439','O440',
                                             'O441','O460','O468','O469','O470','O471','O479','O48', 'O995','O996',
                                             'O997','Z640','O00', 'O10', 'O12', 'O14', 'O15', 'O20', 'O21', 'O22',
                                             'O23', 'O24', 'O26', 'O28', 'O29', 'O30', 'O31', 'O32', 'O33', 'O34',
                                             'O35', 'O36', 'O41', 'O43', 'O44', 'O46', 'O47', 'O98', 'Z34', 'Z35',
                                             'Z36', 'Z321','Z33', 'Z340','Z348','Z349','Z350','Z351','Z352','Z353',
                                             'Z354','Z357','Z358','Z359')))
                                             WHERE AUX_PA.ID = PA.ID) > 0)
                                             THEN 1 ELSE 0 END AS CID_CIAP_VALIDO,
                                         
                                             CASE WHEN ((SELECT COUNT(*) FROM PEP_ATENDIMENTO AUX_PA
                                             JOIN PEP_CONDICAO_AVALIADA CA ON CA.CSI_ID_ATEND = AUX_PA.ID AND
                                             ((CA.ID_CIAP IN ('W03', 'W05', 'W29', 'W71', 'W78', 'W79', 'W80', 'W81', 'W84', 'W85')) OR
                                             (CA.ID_CID  IN ('O11', 'O120','0121','O122','O13', 'O140','O141','O149','O150','0151',
                                             'O159','O16', 'O200','O208','O209','O210','O211','O212','O218','O219',
                                             'O220','O221','O222','O223','O224','O225','O228','O229','O230','O231',
                                             'O232','O233','O234','O235','O239','O299','O300','O301','O302','O308',
                                             'O309','O311','O312','O318','O320','O321','O322','O323','O324','O325',
                                             'O326','O328','O329','O330','O331','O332','O333','O334','O335','O336',
                                             'O337','O338','O752','O753','O990','O991','O992','O993','O994','O240',
                                             'O241','O242','O243','O244','O249','O25', 'O260','O261','O263','O264',
                                             'O265','O268','O269','O280','O281','O282','O283','O284','O285','O288',
                                             'O289','O290','O291','O292','O293','O294','O295','O296','O298','O009',
                                             'O339','O340','O341','O342','O343','O344','O345','O346','O347','O348',
                                             'O349','O350','O351','O352','O353','O354','O355','O356','O357','O358',
                                             'O359','O360','O361','O362','O363','O365','O366','O367','O368','O369',
                                             'O40', 'O410','O411','O418','O419','O430','O431','O438','O439','O440',
                                             'O441','O460','O468','O469','O470','O471','O479','O48', 'O995','O996',
                                             'O997','Z640','O00', 'O10', 'O12', 'O14', 'O15', 'O20', 'O21', 'O22',
                                             'O23', 'O24', 'O26', 'O28', 'O29', 'O30', 'O31', 'O32', 'O33', 'O34',
                                             'O35', 'O36', 'O41', 'O43', 'O44', 'O46', 'O47', 'O98', 'Z34', 'Z35',
                                             'Z36', 'Z321','Z33', 'Z340','Z348','Z349','Z350','Z351','Z352','Z353',
                                             'Z354','Z357','Z358','Z359')))
                                             WHERE AUX_PA.ID = PA.ID AND
                                             (SUBSTRING(PA.ID_CBO FROM 1 FOR 4) IN ('2251','2252','2253','2231','2235'))) > 0)
                                             THEN 1 ELSE 0 END AS INDICADOR,
                                         
                                             CASE WHEN ((SELECT COUNT(*) FROM PEP_ATENDIMENTO AUX_PA
                                             JOIN ESUS_EXPORTACAO_ITEM EI ON (EI.ID = AUX_PA.ID_ESUS_EXPORTACAO_ITEM)
                                             JOIN ESUS_EXPORTACAO E ON (E.ID = EI.ID_EXPORTACAO)
                                             JOIN PEP_CONDICAO_AVALIADA CA ON CA.CSI_ID_ATEND = AUX_PA.ID AND
                                             ((CA.ID_CIAP IN ('W03', 'W05', 'W29', 'W71', 'W78', 'W79', 'W80', 'W81', 'W84', 'W85')) OR
                                             (CA.ID_CID  IN ('O11', 'O120','0121','O122','O13', 'O140','O141','O149','O150','0151',
                                             'O159','O16', 'O200','O208','O209','O210','O211','O212','O218','O219',
                                             'O220','O221','O222','O223','O224','O225','O228','O229','O230','O231',
                                             'O232','O233','O234','O235','O239','O299','O300','O301','O302','O308',
                                             'O309','O311','O312','O318','O320','O321','O322','O323','O324','O325',
                                             'O326','O328','O329','O330','O331','O332','O333','O334','O335','O336',
                                             'O337','O338','O752','O753','O990','O991','O992','O993','O994','O240',
                                             'O241','O242','O243','O244','O249','O25', 'O260','O261','O263','O264',
                                             'O265','O268','O269','O280','O281','O282','O283','O284','O285','O288',
                                             'O289','O290','O291','O292','O293','O294','O295','O296','O298','O009',
                                             'O339','O340','O341','O342','O343','O344','O345','O346','O347','O348',
                                             'O349','O350','O351','O352','O353','O354','O355','O356','O357','O358',
                                             'O359','O360','O361','O362','O363','O365','O366','O367','O368','O369',
                                             'O40', 'O410','O411','O418','O419','O430','O431','O438','O439','O440',
                                             'O441','O460','O468','O469','O470','O471','O479','O48', 'O995','O996',
                                             'O997','Z640','O00', 'O10', 'O12', 'O14', 'O15', 'O20', 'O21', 'O22',
                                             'O23', 'O24', 'O26', 'O28', 'O29', 'O30', 'O31', 'O32', 'O33', 'O34',
                                             'O35', 'O36', 'O41', 'O43', 'O44', 'O46', 'O47', 'O98', 'Z34', 'Z35',
                                             'Z36', 'Z321','Z33', 'Z340','Z348','Z349','Z350','Z351','Z352','Z353',
                                             'Z354','Z357','Z358','Z359')))
                                             WHERE AUX_PA.ID = PA.ID AND
                                             (E.PROCESSADO_CRITICAS = 'T') AND
                                             ((EI.FLG_ERRO <> 'T') OR (EI.FLG_ERRO IS NULL)) AND
                                             (SUBSTRING(PA.ID_CBO FROM 1 FOR 4) IN ('2251','2252','2253','2231','2235'))) > 0)
                                             THEN 1 ELSE 0 END AS INDICADOR_VALIDO,
                                             E.LOTE AS LOTE
                                         
                                         FROM PEP_ATENDIMENTO PA
                                         JOIN TSI_MEDICOS MED ON (PA.ID_MEDICO = MED.CSI_CODMED)
                                         JOIN TSI_CBO CBO ON (PA.ID_CBO = CBO.CODIGO)
                                         LEFT JOIN ESUS_EQUIPES EQ ON (PA.ID_EQUIPE = EQ.ID)
                                         LEFT JOIN TSI_UNIDADE U ON (PA.ID_UNIDADE = U.CSI_CODUNI)
                                         LEFT JOIN ESUS_EXPORTACAO_ITEM EI ON (EI.ID = PA.ID_ESUS_EXPORTACAO_ITEM)
                                         LEFT JOIN ESUS_EXPORTACAO E ON (E.ID = EI.ID_EXPORTACAO)
                                         WHERE PA.ID_PACIENTE = @id_individuo AND
                                         (PA.DATA BETWEEN @data_dum AND COALESCE(@gi_data_nascimento, CURRENT_TIMESTAMP)))
                                         ORDER BY DATA";

        string IIndicador3Command.Atendimentos { get => sqlAtendimentos; }
    }
}
