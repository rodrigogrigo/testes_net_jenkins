using RgCidadao.Domain.Commands.Indicadores;

namespace RgCidadao.Domain.Queries.Indicadores
{
    public class Indicador1CommandText : IIndicador1Command
    {
        public string sqlIndicador1 = $@"@select
                                         COUNT(*) QTDE_GESTANTES,

                                         -- QUANTIDADE DE METAS (SISTEMA)
                                         SUM((CASE WHEN ((
                                         SELECT COUNT(DISTINCT PA.ID) FROM PEP_ATENDIMENTO PA
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
                                         WHERE (PA.ID_PACIENTE = G.ID_CIDADAO) AND
                                         (SUBSTRING(PA.ID_CBO FROM 1 FOR 4) IN ('2251','2252','2253','2231','2235')) AND
                                         (PA.DATA BETWEEN GI.DUM AND COALESCE(GI.DATA_NASCIMENTO, CURRENT_TIMESTAMP))) >= 6) AND
                                         ((SELECT MIN(PA.DATA) FROM PEP_ATENDIMENTO PA
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
                                         WHERE (PA.ID_PACIENTE = G.ID_CIDADAO) AND
                                         (SUBSTRING(PA.ID_CBO FROM 1 FOR 4) IN ('2251','2252','2253','2231','2235')) AND
                                         (PA.DATA BETWEEN GI.DUM AND COALESCE(GI.DATA_NASCIMENTO,CURRENT_TIMESTAMP))) <= (GI.DUM + 140)) THEN 1 ELSE 0 END)) AS QTDE_METAS,

                                         -- QUANTIDADE DE METAS (VÁLIDAS)
                                         SUM((CASE WHEN ((
                                         SELECT COUNT(DISTINCT PA.ID) FROM PEP_ATENDIMENTO PA
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
                                         WHERE (PA.ID_PACIENTE = G.ID_CIDADAO) AND
                                         (E.PROCESSADO_CRITICAS = 'T') AND
                                         ((EI.FLG_ERRO <> 'T') OR (EI.FLG_ERRO IS NULL)) AND 
                                         (SUBSTRING(PA.ID_CBO FROM 1 FOR 4) IN ('2251','2252','2253','2231','2235')) AND
                                         (PA.DATA BETWEEN GI.DUM AND COALESCE(GI.DATA_NASCIMENTO, CURRENT_TIMESTAMP))) >= 6) AND
                                         ((SELECT MIN(PA.DATA) FROM PEP_ATENDIMENTO PA
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
                                         WHERE (PA.ID_PACIENTE = G.ID_CIDADAO) AND
                                         (E.PROCESSADO_CRITICAS = 'T') AND
                                         ((EI.FLG_ERRO <> 'T') OR (EI.FLG_ERRO IS NULL)) AND 
                                         ((CP.CSI_NCARTAO IS NOT NULL AND CP.CSI_NCARTAO <> '') OR
                                         (CP.CSI_CPFPAC IS NOT NULL AND CP.CSI_CPFPAC <> '')) AND
                                         (SUBSTRING(PA.ID_CBO FROM 1 FOR 4) IN ('2251','2252','2253','2231','2235')) AND
                                         (PA.DATA BETWEEN GI.DUM AND COALESCE(GI.DATA_NASCIMENTO,CURRENT_TIMESTAMP))) <= (GI.DUM + 140)) THEN 1 ELSE 0 END)) AS QTDE_METAS_VALIDAS

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
                                         LEFT JOIN TSI_UNIDADE UN ON UN.CSI_CNES = ES.CNES
                                         WHERE 
                                         @filtros 
                                         @agrupamento";
        string IIndicador1Command.Indicador1 { get => sqlIndicador1; }

        public string sqlVerificaNovaEstrutura = $@"SELECT COUNT(*) QTDE
                                                    FROM RDB$RELATIONS
                                                    WHERE RDB$FLAGS=1 and RDB$RELATION_NAME='ESUS_FAMILIA'";
        string IIndicador1Command.VerificaNovaEstrutura { get => sqlVerificaNovaEstrutura; }

        public string sqlpublicoAlvo = $@"@select 
                                           CP.CSI_NOMPAC GESTANTE,
                                           GI.DUM,
                                           GI.DATA_NASCIMENTO GI_DATA_NASCIMENTO,
                                           CP.CSI_CODPAC ID_INDIVIDUO,

                                           (CASE WHEN GI.FLG_DESFECHO = 0 THEN
                                           ((CURRENT_DATE - CAST(GI.DUM AS DATE))/7)
                                           WHEN GI.FLG_DESFECHO > 0 THEN ((CAST(GI.DATA_NASCIMENTO AS DATE) - CAST(GI.DUM AS DATE))/7) END) AS IDADE_GESTACIONAL,

                                           (CASE WHEN GI.FLG_DESFECHO = 0 THEN 'EM ANDAMENTO'
                                           WHEN GI.FLG_DESFECHO = 1 THEN 'ABORTO'
                                           WHEN GI.FLG_DESFECHO = 2 THEN 'CONCLUÍDO'
                                           WHEN GI.FLG_DESFECHO = 3 THEN 'ÓBITO AMBOS'
                                           WHEN GI.FLG_DESFECHO = 4 THEN 'ÓBITO FETAL'
                                           WHEN GI.FLG_DESFECHO = 5 THEN 'ÓBITO MATERNO'
                                           WHEN GI.FLG_DESFECHO = 6 THEN 'MUDANÇA DE TERRITÓRIO' END) AS DESFECHO,
                                          
                                           (SELECT MIN(CAST(PA.DATA AS DATE)) FROM PEP_ATENDIMENTO PA
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
                                           WHERE (PA.ID_PACIENTE = G.ID_CIDADAO) AND
                                           (PA.DATA BETWEEN GI.DUM AND COALESCE(GI.DATA_NASCIMENTO,CURRENT_TIMESTAMP))) AS DATA_PRIMEIRO_ATENDIMENTO,

                                           (SELECT MIN(CAST(PA.DATA AS DATE)) FROM PEP_ATENDIMENTO PA
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
                                           WHERE (PA.ID_PACIENTE = G.ID_CIDADAO) AND
                                           (E.PROCESSADO_CRITICAS = 'T') AND 
                                           ((EI.FLG_ERRO <> 'T') OR (EI.FLG_ERRO IS NULL)) AND 
                                           (PA.DATA BETWEEN GI.DUM AND COALESCE(GI.DATA_NASCIMENTO,CURRENT_TIMESTAMP))) AS DATA_PRIMEIRO_ATD_VALIDO,

                                           (SELECT MAX(CAST(PA.DATA AS DATE)) FROM PEP_ATENDIMENTO PA
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
                                           WHERE (PA.ID_PACIENTE = G.ID_CIDADAO) AND
                                           (PA.DATA BETWEEN GI.DUM AND COALESCE(GI.DATA_NASCIMENTO,CURRENT_TIMESTAMP))) AS DATA_ULTIMO_ATENDIMENTO,

                                           (SELECT COUNT(DISTINCT PA.ID)
                                           FROM PEP_ATENDIMENTO PA
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
                                           WHERE (PA.ID_PACIENTE = G.ID_CIDADAO) AND
                                           (SUBSTRING(PA.ID_CBO FROM 1 FOR 4) IN ('2251','2252','2253','2231','2235')) AND
                                           (PA.DATA BETWEEN GI.DUM AND COALESCE(GI.DATA_NASCIMENTO,CURRENT_TIMESTAMP))) AS QTDE_ATENDIMENTO,

                                           (SELECT COUNT(DISTINCT PA.ID)
                                           FROM PEP_ATENDIMENTO PA
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
                                           WHERE (PA.ID_PACIENTE = G.ID_CIDADAO) AND
                                           (E.PROCESSADO_CRITICAS = 'T') AND 
                                           ((EI.FLG_ERRO <> 'T') OR (EI.FLG_ERRO IS NULL)) AND 
                                           ((CP.CSI_NCARTAO IS NOT NULL AND CP.CSI_NCARTAO <> '') OR
                                           (CP.CSI_CPFPAC IS NOT NULL AND CP.CSI_CPFPAC <> '')) AND
                                           (SUBSTRING(PA.ID_CBO FROM 1 FOR 4) IN ('2251','2252','2253','2231','2235')) AND
                                           (PA.DATA BETWEEN GI.DUM AND COALESCE(GI.DATA_NASCIMENTO,CURRENT_TIMESTAMP))) AS QTDE_ATENDIMENTO_VALIDO

                                           FROM GESTACAO G
                                           JOIN GESTACAO_ITEM GI ON (GI.ID_GESTACAO = G.ID) AND
                                           ((GI.FLG_DESFECHO > 0) AND (GI.DATA_NASCIMENTO BETWEEN
                                           (SELECT DATA_PRIMEIRO_DIA FROM PRIMEIRO_DIA((SELECT MES_ANTERIOR
                                           FROM COMPETENCIA_ANTERIOR(CURRENT_TIMESTAMP,11)))) AND CURRENT_TIMESTAMP)
                                           OR (GI.FLG_DESFECHO = 0))
                                           JOIN TSI_CADPAC CP ON (G.ID_CIDADAO = CP.CSI_CODPAC)
                                           LEFT JOIN ESUS_ESTAB_HOSPITALARES EH ON EH.ID = GI.ID_MATERNIDADE
                                           LEFT JOIN ESUS_FAMILIA FAM ON FAM.ID = CP.ID_FAMILIA 
                                           LEFT JOIN VS_ESTABELECIMENTOS EST ON EST.ID = FAM.ID_DOMICILIO 
                                           LEFT JOIN ESUS_MICROAREA MI ON MI.ID = EST.ID_MICROAREA
                                           LEFT JOIN TSI_MEDICOS ME ON ME.CSI_CODMED = MI.ID_PROFISSIONAL
                                           LEFT JOIN ESUS_EQUIPES EQ ON EQ.ID = MI.ID_EQUIPE
                                           LEFT JOIN ESUS_ESTABELECIMENTO_SAUDE ES ON ES.ID = EQ.ID_ESTABELECIMENTO
                                           LEFT JOIN TSI_UNIDADE UN ON UN.CSI_CNES = ES.CNES
                                           WHERE 
                                           @filtros
                                           ORDER BY CP.CSI_NOMPAC ";

        string IIndicador1Command.publicoAlvo { get => sqlpublicoAlvo; }

        public string sqlCountPublicoAlvo = $@"SELECT COUNT(*) FROM (
                                                SELECT CP.CSI_NOMPAC GESTANTE
                                                FROM GESTACAO G
                                                JOIN GESTACAO_ITEM GI ON (GI.ID_GESTACAO = G.ID) AND
                                                ((GI.FLG_DESFECHO > 0) AND (GI.DATA_NASCIMENTO BETWEEN
                                                (SELECT DATA_PRIMEIRO_DIA FROM PRIMEIRO_DIA((SELECT MES_ANTERIOR
                                                FROM COMPETENCIA_ANTERIOR(CURRENT_TIMESTAMP,11)))) AND CURRENT_TIMESTAMP)
                                                OR (GI.FLG_DESFECHO = 0))
                                                JOIN TSI_CADPAC CP ON (G.ID_CIDADAO = CP.CSI_CODPAC)
                                                LEFT JOIN ESUS_ESTAB_HOSPITALARES EH ON EH.ID = GI.ID_MATERNIDADE
                                                LEFT JOIN ESUS_FAMILIA FAM ON FAM.ID = CP.ID_FAMILIA 
                                                LEFT JOIN VS_ESTABELECIMENTOS EST ON EST.ID = FAM.ID_DOMICILIO 
                                                LEFT JOIN ESUS_MICROAREA MI ON MI.ID = EST.ID_MICROAREA
                                                LEFT JOIN TSI_MEDICOS ME ON ME.CSI_CODMED = MI.ID_PROFISSIONAL
                                                LEFT JOIN ESUS_EQUIPES EQ ON EQ.ID = MI.ID_EQUIPE
                                                LEFT JOIN ESUS_ESTABELECIMENTO_SAUDE ES ON ES.ID = EQ.ID_ESTABELECIMENTO
                                                LEFT JOIN TSI_UNIDADE UN ON UN.CSI_CNES = ES.CNES
                                                WHERE 
                                                @filtros )";

        string IIndicador1Command.CountPublicoAlvo { get => sqlCountPublicoAlvo; }

        public string sqlAtendimentos = $@"SELECT
                                                PA.DATA AS DATA,
                                                PROF.CSI_NOMMED AS PROFISSIONAL,
                                                PA.ID_CBO AS CBO,
                                                CBO.DESCRICAO DESCRICAO_CBO,

                                                COALESCE((SELECT 'CIAP: '||LIST(COALESCE(CA.ID_CIAP, '')) || ' | ' || 'CID: '||LIST(COALESCE(CA.ID_CID, ''))
                                                FROM PEP_CONDICAO_AVALIADA CA WHERE CA.CSI_ID_ATEND = PA.ID AND (CA.ID_CIAP IS NOT NULL OR CA.ID_CID IS NOT NULL)), '--') AS CONDICAO_AVALIADA,

                                                EQ.DSC_AREA AS EQUIPE,
                                                U.CSI_NOMUNI UNIDADE_SAUDE,
                                                CASE WHEN (COALESCE(E.PROCESSADO_CRITICAS, 'F') = 'T') THEN 1 ELSE 0 END AS PROCESSADO_CRITICAS,
                                                CASE WHEN (COALESCE(EI.FLG_ERRO, 'F') = 'T') THEN 1 ELSE 0 END AS FLG_ERRO,

                                                CASE WHEN (E.ID IS NULL) THEN 'Registro não exportado'
                                                WHEN (COALESCE(E.PROCESSADO_CRITICAS, 'F') = 'T') THEN EI.DESCRICAO_ERRO
                                                ELSE 'Arquivo de críticas não processado'
                                                END AS DESCRICAO_ERRO,

                                                (CASE WHEN ((SELECT COUNT(DISTINCT ATD.ID) FROM PEP_ATENDIMENTO ATD
                                                JOIN PEP_CONDICAO_AVALIADA CA ON CA.CSI_ID_ATEND = ATD.ID AND
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
                                                WHERE (ATD.ID = PA.ID)) > 0)
                                                THEN 1 ELSE 0 END) AS CID_CIAP_VALIDO,

                                                (CASE WHEN ((SELECT COUNT(DISTINCT ATD.ID) FROM PEP_ATENDIMENTO ATD
                                                WHERE (ATD.ID = PA.ID) AND (SUBSTRING(ATD.ID_CBO FROM 1 FOR 4) IN ('2251','2252','2253','2231','2235')) ) > 0)
                                                THEN 1 ELSE 0 END) AS CBO_VALIDO,

                                                (CASE WHEN ((SELECT COUNT(DISTINCT ATD.ID) FROM PEP_ATENDIMENTO ATD
                                                JOIN PEP_CONDICAO_AVALIADA CA ON CA.CSI_ID_ATEND = ATD.ID AND
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
                                                WHERE (ATD.ID = PA.ID) AND (SUBSTRING(ATD.ID_CBO FROM 1 FOR 4) IN ('2251','2252','2253','2231','2235')) ) > 0)
                                                THEN 1 ELSE 0 END) AS INDICADOR,

                                                (CASE WHEN ((SELECT COUNT(DISTINCT ATD.ID) FROM PEP_ATENDIMENTO ATD
                                                JOIN TSI_CADPAC CP ON ATD.ID_PACIENTE = CP.CSI_CODPAC
                                                JOIN ESUS_EXPORTACAO_ITEM EI ON EI.ID = PA.ID_ESUS_EXPORTACAO_ITEM
                                                JOIN ESUS_EXPORTACAO E ON E.ID = EI.ID_EXPORTACAO
                                                JOIN PEP_CONDICAO_AVALIADA CA ON CA.CSI_ID_ATEND = ATD.ID AND
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
                                                 WHERE (ATD.ID = PA.ID) AND
                                                ((CP.CSI_NCARTAO IS NOT NULL AND CP.CSI_NCARTAO <> '') OR
                                                (CP.CSI_CPFPAC IS NOT NULL AND CP.CSI_CPFPAC <> '')) AND
                                                (E.PROCESSADO_CRITICAS = 'T') AND
                                                ((EI.FLG_ERRO <> 'T') OR (EI.FLG_ERRO IS NULL)) AND
                                                (SUBSTRING(ATD.ID_CBO FROM 1 FOR 4) IN ('2251','2252','2253','2231','2235')) ) > 0)
                                                THEN 1 ELSE 0 END) AS INDICADOR_VALIDO,
                                                E.LOTE AS LOTE

                                           FROM PEP_ATENDIMENTO PA
                                           JOIN TSI_MEDICOS PROF ON PROF.CSI_CODMED = PA.ID_MEDICO
                                           JOIN TSI_CBO CBO ON CBO.CODIGO = PA.ID_CBO
                                           LEFT JOIN ESUS_EXPORTACAO_ITEM EI ON EI.ID = PA.ID_ESUS_EXPORTACAO_ITEM
                                           LEFT JOIN ESUS_EXPORTACAO E ON E.ID = EI.ID_EXPORTACAO
                                           LEFT JOIN ESUS_EQUIPES EQ ON EQ.ID = PA.ID_EQUIPE
                                           JOIN TSI_UNIDADE U ON PA.ID_UNIDADE = U.CSI_CODUNI
                                           WHERE PA.ID_PACIENTE = @id_individuo  AND
                                           (PA.DATA BETWEEN CAST(@data_dum AS TIMESTAMP) AND COALESCE(@gi_data_nascimento,CURRENT_TIMESTAMP))
                                           ORDER BY PA.DATA";

        string IIndicador1Command.Atendimentos { get => sqlAtendimentos; }
    }
}
