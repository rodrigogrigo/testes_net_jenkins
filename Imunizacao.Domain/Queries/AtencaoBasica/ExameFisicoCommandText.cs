using RgCidadao.Domain.Commands.AtencaoBasica;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Queries.AtencaoBasica
{
    public class ExameFisicoCommandText : IExameFisicoCommand
    {
        public string sqlGetAllPagination = $@"SELECT FIRST(@pagesize) SKIP(@page) 
                                                                EF.CODIGO_EXAME_FISICO,
                                                                MED.CSI_CODMED,
                                                                MED.CSI_NOMMED,
                                                                PAC.CSI_CODPAC,
                                                                PAC.CSI_NOMPAC,
                                                                EF.DATA_EXAME_FISICO 
                                                                FROM PEP_EXAME_FISICO EF
                                                                LEFT JOIN TSI_MEDICOS MED
                                                            ON MED.CSI_CODMED = EF.CODIGO_PROFISSIONAL
                                                            OR MED.CSI_CODMED = EF.ID_PROFISSIONAL_CLAS_TRIAGEM
                                                                LEFT JOIN TSI_CADPAC PAC
                                                            ON PAC.CSI_CODPAC = EF.ID_PACIENTE @filtro 
                                                            ORDER BY EF.CODIGO_EXAME_FISICO DESC";
        string IExameFisicoCommand.GetAllPagination { get => sqlGetAllPagination; }

        public string sqlNewCodigoExameFisico = $@"SELECT GEN_ID(GEN_PEP_EXAME_FISICO_ID, 1) AS VLR FROM RDB$DATABASE";
        string IExameFisicoCommand.GetNewCodigoExameFisico { get => sqlNewCodigoExameFisico; }

        public string sqlInsert = $@"INSERT INTO PEP_EXAME_FISICO (CODIGO_EXAME_FISICO, CODIGO_ATENDIMENTO, DATA_EXAME_FISICO, VALOR_SATURACAO,
                                                VALOR_CEFALICO, VALOR_CIRC_ABDOMINAL, VALOR_PA1, VALOR_PA2, VALOR_FREQ_CARDIACA,
                                                VALOR_FREQ_RESPIRATORIA, VALOR_TEMPERATURA, VALOR_GLICEMIA, OBSERVACAO, CODIGO_PROFISSIONAL,
                                                ORIGEM_DADOS, CODIGO_TRIAGEM, VALOR_PESO, VALOR_ALTURA, VALOR_IMC, ID_PACIENTE, CODIGO_CONSULTA, MOMENTO_COLETA,
                                                ORIGEM, GLASGOW, REGUA_DOR, ID_CLASSIFICACAO_TRIAGEM, ID_PROFISSIONAL_CLAS_TRIAGEM, ID_GRUPO_TRIAGEM,
                                                ID_SUBGRUPO_TRIAGEM, DESCRICAO_SINTOMA_TRIAGEM, VALOR_CIRC_TORACICA )
                                    VALUES (@codigo_exame_fisico, @codigo_atendimento, @data_exame_fisico, @valor_saturacao, @valor_cefalico, @valor_circ_abdominal, @valor_pa1, @valor_pa2,
                                                @valor_freq_cardiaca, @valor_freq_respiratoria, @valor_temperatura, @valor_glicemia, @observacao, @codigo_profissional, @origem_dados,
                                                @codigo_triagem, @valor_peso, @valor_altura, @valor_imc, @id_paciente, @codigo_consulta, @momento_coleta, @origem, @glasgow, @regua_dor, @id_classificacao_triagem,
                                                @id_profissional_clas_triagem, @id_grupo_triagem, @id_subgrupo_triagem, @descricao_sintoma_triagem, @valor_circ_toracica)";

        string IExameFisicoCommand.Insert { get => sqlInsert; }

        public string sqlNewCodigoControle = $@"SELECT GEN_ID(GEN_TSI_PROCENFERMAGEM, 1) AS VLR FROM RDB$DATABASE";
        string IExameFisicoCommand.GetNewCodigoControle { get => sqlNewCodigoControle; }

        public string sqlInsertUpdateProcenfermagem = $@"UPDATE OR INSERT INTO TSI_PROCENFERMAGEM(
                                                                CSI_CONTROLE, CSI_DATA, CSI_CODMED, CSI_NOMUSU, CSI_DATAINC,
                                                                CSI_CBO, CSI_CODUNI, ID_PEP_EXAME_FISICO, CSI_LOCAL_ATENDIMENTO, TURNO, CSI_CODPAC
                                                            ) VALUES (
                                                               @csi_controle, current_timestamp, @csi_codmed, @csi_nomusu, current_timestamp, 
                                                                @csi_cbo, @csi_coduni, @id_pep_exame_fisico, @csi_local_atendimento, @turno, @csi_codpac
                                                            ) MATCHING (
                                                                CSI_CONTROLE
                                                            )";

        string IExameFisicoCommand.InsertUpdateProcenfermagem { get => sqlInsertUpdateProcenfermagem; }

        public string sqlInsertUpdateIProcenfermagem = $@"UPDATE OR INSERT INTO TSI_IPROCENFERMAGEM (
                                                                CSI_CONTROLE, CSI_CODPROC, CSI_QTDE, CSI_IDADE, CSI_CODCID, CSI_ESCUTA_INICIAL
                                                            )VALUES (
                                                                @csi_controle, @csi_codproc, @csi_qtde, @csi_idade, NULL, 'F'
                                                            ) MATCHING (
                                                                CSI_CONTROLE,
                                                                CSI_CODPROC
                                                            );";

        string IExameFisicoCommand.InsertUpdateIProcenfermagem { get => sqlInsertUpdateIProcenfermagem; }


        public string sqlUpdate = $@"UPDATE PEP_EXAME_FISICO SET CODIGO_ATENDIMENTO = @CODIGO_ATENDIMENTO,
                                        DATA_EXAME_FISICO = @DATA_EXAME_FISICO, VALOR_SATURACAO = @VALOR_SATURACAO,
                                        VALOR_CEFALICO = @VALOR_CEFALICO, VALOR_CIRC_ABDOMINAL = @VALOR_CIRC_ABDOMINAL,
                                        VALOR_PA1 = @VALOR_PA1, VALOR_PA2 = @VALOR_PA2,
                                        VALOR_FREQ_CARDIACA = @VALOR_FREQ_CARDIACA, VALOR_FREQ_RESPIRATORIA = @VALOR_FREQ_RESPIRATORIA,
                                        VALOR_TEMPERATURA = @VALOR_TEMPERATURA,  VALOR_GLICEMIA = @VALOR_GLICEMIA,
                                        OBSERVACAO = @OBSERVACAO, CODIGO_PROFISSIONAL = @CODIGO_PROFISSIONAL,
                                        ORIGEM_DADOS = @ORIGEM_DADOS, CODIGO_TRIAGEM = @CODIGO_TRIAGEM,
                                        VALOR_PESO = @VALOR_PESO, VALOR_ALTURA = @VALOR_ALTURA,
                                        VALOR_IMC = @VALOR_IMC, ID_PACIENTE = @ID_PACIENTE, 
                                        CODIGO_CONSULTA = @CODIGO_CONSULTA,
                                        MOMENTO_COLETA = @MOMENTO_COLETA, ORIGEM = @ORIGEM,
                                        GLASGOW = @GLASGOW, REGUA_DOR = @REGUA_DOR,
                                        ID_CLASSIFICACAO_TRIAGEM = @ID_CLASSIFICACAO_TRIAGEM, ID_PROFISSIONAL_CLAS_TRIAGEM = @ID_PROFISSIONAL_CLAS_TRIAGEM,
                                        ID_GRUPO_TRIAGEM = @ID_GRUPO_TRIAGEM, ID_SUBGRUPO_TRIAGEM = @ID_SUBGRUPO_TRIAGEM,
                                        DESCRICAO_SINTOMA_TRIAGEM = @DESCRICAO_SINTOMA_TRIAGEM, VALOR_CIRC_TORACICA = @VALOR_CIRC_TORACICA
                                    WHERE CODIGO_EXAME_FISICO = @CODIGO_EXAME_FISICO";

        string IExameFisicoCommand.Update { get => sqlUpdate; }

        public string sqlGetCountAll = $@"SELECT COUNT(*) 
                                          FROM (SELECT 
                                                    EF.CODIGO_EXAME_FISICO,
                                                    MED.CSI_CODMED,
                                                    MED.CSI_NOMMED,
                                                    PAC.CSI_CODPAC,
                                                    PAC.CSI_NOMPAC,
                                                    EF.DATA_EXAME_FISICO
                                                    FROM PEP_EXAME_FISICO EF
                                                    LEFT JOIN TSI_MEDICOS MED
                                                ON MED.CSI_CODMED = EF.CODIGO_PROFISSIONAL
                                                OR MED.CSI_CODMED = EF.ID_PROFISSIONAL_CLAS_TRIAGEM
                                                    LEFT JOIN TSI_CADPAC PAC
                                                ON PAC.CSI_CODPAC = EF.ID_PACIENTE @filtro)";
        string IExameFisicoCommand.GetCountAll { get => sqlGetCountAll; }

        public string sqlGetHistoricoObservacaoByPaciente = $@"SELECT EF.DATA_EXAME_FISICO DATA, EF.OBSERVACAO DESCRICAO
                                                               FROM PEP_EXAME_FISICO EF
                                                               WHERE EF.ID_PACIENTE = @id_paciente AND
                                                                     EF.OBSERVACAO IS NOT NULL
                                                               ORDER BY EF.DATA_EXAME_FISICO DESC ";
        string IExameFisicoCommand.GetHistoricoObservacaoByPaciente { get => sqlGetHistoricoObservacaoByPaciente; }

        //PEGANDO ULTIMOS DADOS DE CADA PACIENTE
        public string sqlGetLastPesoByPaciente = $@"SELECT FIRST(1) EF.DATA_EXAME_FISICO DATA_PESO, EF.VALOR_PESO PESO FROM PEP_EXAME_FISICO EF
                                                    WHERE EF.ID_PACIENTE = @id_paciente AND
                                                          COALESCE(EF.VALOR_PESO,0) != 0
                                                    ORDER BY EF.DATA_EXAME_FISICO DESC";
        string IExameFisicoCommand.GetLastPesoByPaciente { get => sqlGetLastPesoByPaciente; }

        public string sqlGetLastAlturaPaciente = $@"SELECT FIRST(1) EF.DATA_EXAME_FISICO DATA_ALTURA, EF.VALOR_ALTURA ALTURA FROM PEP_EXAME_FISICO EF
                                                    WHERE EF.ID_PACIENTE = @id_paciente AND
                                                          COALESCE(EF.VALOR_ALTURA,0) != 0
                                                    ORDER BY EF.DATA_EXAME_FISICO DESC";
        string IExameFisicoCommand.GetLastAlturaPaciente { get => sqlGetLastAlturaPaciente; }

        public string sqlGetLastImcPaciente = $@"SELECT FIRST(1) EF.DATA_EXAME_FISICO DATA_IMC, EF.VALOR_IMC IMC FROM PEP_EXAME_FISICO EF
                                                    WHERE EF.ID_PACIENTE = @id_paciente AND
                                                          COALESCE(EF.VALOR_IMC,0) != 0
                                                    ORDER BY EF.DATA_EXAME_FISICO DESC";
        string IExameFisicoCommand.GetLastImcPaciente { get => sqlGetLastImcPaciente; }

        public string sqlGetLastTemperaturaPaciente = $@"SELECT FIRST(1) EF.DATA_EXAME_FISICO DATA, EF.VALOR_TEMPERATURA VALOR
                                                         FROM PEP_EXAME_FISICO EF
                                                         WHERE EF.ID_PACIENTE = @id_paciente AND
                                                            COALESCE(EF.VALOR_TEMPERATURA,0) != 0
                                                         ORDER BY EF.DATA_EXAME_FISICO DESC";
        string IExameFisicoCommand.GetLastTemperaturaPaciente { get => sqlGetLastTemperaturaPaciente; }

        public string sqlGetLastCircunferenciaAbdominalPaciente = $@"SELECT FIRST(1) EF.DATA_EXAME_FISICO DATA, EF.VALOR_CIRC_ABDOMINAL VALOR
                                                                     FROM PEP_EXAME_FISICO EF
                                                                     WHERE EF.ID_PACIENTE = @id_paciente AND
                                                                           COALESCE(EF.VALOR_CIRC_ABDOMINAL,0) != 0
                                                                     ORDER BY EF.DATA_EXAME_FISICO DESC";
        string IExameFisicoCommand.GetLastCircunferenciaAbdominalPaciente { get => sqlGetLastCircunferenciaAbdominalPaciente; }

        public string sqlGetLastCircunferenciaToracicaPaciente = $@"SELECT FIRST(1) EF.DATA_EXAME_FISICO DATA, EF.VALOR_CIRC_TORACICA VALOR
                                                                    FROM PEP_EXAME_FISICO EF
                                                                    WHERE EF.ID_PACIENTE = @id_paciente AND
                                                                          COALESCE(EF.VALOR_CIRC_TORACICA,0) != 0
                                                                    ORDER BY EF.DATA_EXAME_FISICO DESC";
        string IExameFisicoCommand.GetLastCircunferenciaToracicaPaciente { get => sqlGetLastCircunferenciaToracicaPaciente; }

        public string sqlGetLastPressaoArterialPaciente = $@"SELECT FIRST(1) EF.DATA_EXAME_FISICO DATA, CAST(EF.VALOR_PA1 AS NUMERIC(15,1)) || ' / ' || CAST(EF.VALOR_PA2 AS NUMERIC(15,1)) VALOR
                                                             FROM PEP_EXAME_FISICO EF
                                                             WHERE EF.ID_PACIENTE = @id_paciente AND
                                                                   COALESCE(EF.VALOR_PA1,0) != 0 AND
                                                                   COALESCE(EF.VALOR_PA2,0) != 0
                                                             ORDER BY EF.DATA_EXAME_FISICO DESC";
        string IExameFisicoCommand.GetLastPressaoArterialPaciente { get => sqlGetLastPressaoArterialPaciente; }

        public string sqlGetLastPressaoArterialSistolicaPaciente = $@"SELECT FIRST(1) EF.DATA_EXAME_FISICO DATA, CAST(EF.VALOR_PA1 AS NUMERIC(15,2))  VALOR
                                                             FROM PEP_EXAME_FISICO EF
                                                             WHERE EF.ID_PACIENTE = @id_paciente AND
                                                                   COALESCE(EF.VALOR_PA1,0) != 0
                                                             ORDER BY EF.DATA_EXAME_FISICO DESC";
        string IExameFisicoCommand.GetLastPressaoArterialSistolicaPaciente { get => sqlGetLastPressaoArterialSistolicaPaciente; }

        public string sqlGetLastPressaoArterialDiastolicaPaciente = $@"SELECT FIRST(1) EF.DATA_EXAME_FISICO DATA, CAST(EF.VALOR_PA2 AS NUMERIC(15,2)) VALOR
                                                             FROM PEP_EXAME_FISICO EF
                                                             WHERE EF.ID_PACIENTE = @id_paciente AND
                                                                   COALESCE(EF.VALOR_PA2,0) != 0
                                                             ORDER BY EF.DATA_EXAME_FISICO DESC";
        string IExameFisicoCommand.GetLastPressaoArterialDiastolicaPaciente { get => sqlGetLastPressaoArterialDiastolicaPaciente; }

        public string sqlGetLastGlicemiaPaciente = $@"SELECT FIRST(1) EF.DATA_EXAME_FISICO DATA, EF.VALOR_GLICEMIA VALOR
                                                      FROM PEP_EXAME_FISICO EF
                                                      WHERE EF.ID_PACIENTE = @id_paciente AND
                                                            COALESCE(EF.VALOR_GLICEMIA,0) != 0
                                                      ORDER BY EF.DATA_EXAME_FISICO DESC";
        string IExameFisicoCommand.GetLastGlicemiaPaciente { get => sqlGetLastGlicemiaPaciente; }

        public string sqlGetLastSaturacaoPaciente = $@"SELECT FIRST(1) EF.DATA_EXAME_FISICO DATA, EF.VALOR_SATURACAO VALOR
                                                       FROM PEP_EXAME_FISICO EF
                                                       WHERE EF.ID_PACIENTE = @id_paciente AND
                                                           COALESCE(EF.VALOR_SATURACAO,0) != 0
                                                       ORDER BY EF.DATA_EXAME_FISICO DESC";
        string IExameFisicoCommand.GetLastSaturacaoPaciente { get => sqlGetLastSaturacaoPaciente; }

        public string sqlGetLastFrequenciaCardiacaPaciente = $@"SELECT FIRST(1) EF.DATA_EXAME_FISICO DATA, EF.VALOR_FREQ_CARDIACA VALOR
                                                                FROM PEP_EXAME_FISICO EF
                                                                WHERE EF.ID_PACIENTE = @id_paciente AND
                                                                      COALESCE(EF.VALOR_FREQ_CARDIACA,0) != 0
                                                                ORDER BY EF.DATA_EXAME_FISICO DESC";
        string IExameFisicoCommand.GetLastFrequenciaCardiacaPaciente { get => sqlGetLastFrequenciaCardiacaPaciente; }

        public string sqlGetLastFrequenciaRespiratoriaPaciente = $@"SELECT FIRST(1) EF.DATA_EXAME_FISICO DATA, EF.VALOR_FREQ_RESPIRATORIA VALOR
                                                                    FROM PEP_EXAME_FISICO EF
                                                                    WHERE EF.ID_PACIENTE = @id_paciente AND
                                                                          COALESCE(EF.VALOR_FREQ_RESPIRATORIA,0) != 0
                                                                    ORDER BY EF.DATA_EXAME_FISICO DESC";
        string IExameFisicoCommand.GetLastFrequenciaRespiratoriaPaciente { get => sqlGetLastFrequenciaRespiratoriaPaciente; }

        public string sqlGetLastFrequenciaCefalicoPaciente = $@"SELECT FIRST(1) EF.DATA_EXAME_FISICO DATA, EF.VALOR_CEFALICO VALOR
                                                                FROM PEP_EXAME_FISICO EF
                                                                WHERE EF.ID_PACIENTE = @id_paciente AND
                                                                      COALESCE(EF.VALOR_CEFALICO,0) != 0
                                                                ORDER BY EF.DATA_EXAME_FISICO DESC";
        string IExameFisicoCommand.GetLastFrequenciaCefalicoPaciente { get => sqlGetLastFrequenciaCefalicoPaciente; }

        public string sqlGetLastGlassGowPaciente = $@"SELECT FIRST(1) EF.DATA_EXAME_FISICO DATA, EF.GLASGOW VALOR
                                                      FROM PEP_EXAME_FISICO EF
                                                      WHERE EF.ID_PACIENTE = @id_paciente AND
                                                            COALESCE(EF.GLASGOW,0) != 0
                                                      ORDER BY EF.DATA_EXAME_FISICO DESC";
        string IExameFisicoCommand.GetLastGlassGowPaciente { get => sqlGetLastGlassGowPaciente; }

        public string sqlGetLastReguaDorPaciente = $@"SELECT FIRST(1) EF.DATA_EXAME_FISICO DATA, EF.REGUA_DOR VALOR
                                                      FROM PEP_EXAME_FISICO EF 
                                                      WHERE EF.ID_PACIENTE = @id_paciente AND
                                                            COALESCE(EF.REGUA_DOR,0) != 0
                                                      ORDER BY EF.DATA_EXAME_FISICO DESC";
        string IExameFisicoCommand.GetLastReguaDorPaciente { get => sqlGetLastReguaDorPaciente; }

        public string sqlGetLastProcedimentosGeradosPaciente = $@"SELECT FIRST(1) EF.DATA_EXAME_FISICO DATA, (SELECT COUNT(*) FROM TSI_PROCENFERMAGEM
                                                                  WHERE ID_PEP_EXAME_FISICO = EF.CODIGO_EXAME_FISICO) VALOR
                                                                  FROM PEP_EXAME_FISICO EF
                                                                  WHERE EF.ID_PACIENTE = @id_paciente
                                                                  ORDER BY EF.DATA_EXAME_FISICO DESC";
        string IExameFisicoCommand.GetLastProcedimentosGeradosPaciente { get => sqlGetLastProcedimentosGeradosPaciente; }

        //PEGANDO HISTÓRICO DE DADOS DE CADA PACIENTE
        public string sqlGetListaPesoByPaciente = $@"SELECT EF.DATA_EXAME_FISICO DATA, EF.VALOR_PESO VALOR, 
                                                            MED.CSI_CODMED ID_PROFISSIONAL, MED.CSI_NOMMED PROFISSIONAL
                                                     FROM PEP_EXAME_FISICO EF
                                                     JOIN TSI_MEDICOS MED ON MED.CSI_CODMED = EF.CODIGO_PROFISSIONAL
                                                     WHERE EF.ID_PACIENTE = @id_paciente AND
                                                           COALESCE(EF.VALOR_PESO, 0) != 0
                                                     ORDER BY EF.DATA_EXAME_FISICO  ";
        string IExameFisicoCommand.GetListaPesoByPaciente { get => sqlGetListaPesoByPaciente; }

        public string sqlGetListaAlturaByPaciente = $@"SELECT EF.DATA_EXAME_FISICO DATA, EF.VALOR_ALTURA VALOR, 
                                                              MED.CSI_CODMED ID_PROFISSIONAL, MED.CSI_NOMMED PROFISSIONAL
                                                       FROM PEP_EXAME_FISICO EF
                                                       JOIN TSI_MEDICOS MED ON MED.CSI_CODMED = EF.CODIGO_PROFISSIONAL
                                                       WHERE EF.ID_PACIENTE = @id_paciente AND
                                                             COALESCE(EF.VALOR_ALTURA, 0) != 0
                                                       ORDER BY EF.DATA_EXAME_FISICO ";
        string IExameFisicoCommand.GetListaAlturaByPaciente { get => sqlGetListaAlturaByPaciente; }


        public string sqlGetListaIMCByPaciente = $@"SELECT EF.DATA_EXAME_FISICO DATA, EF.VALOR_IMC VALOR,
                                                           MED.CSI_CODMED ID_PROFISSIONAL, MED.CSI_NOMMED PROFISSIONAL
                                                    FROM PEP_EXAME_FISICO EF
                                                    JOIN TSI_MEDICOS MED ON MED.CSI_CODMED = EF.CODIGO_PROFISSIONAL
                                                    WHERE EF.ID_PACIENTE = @id_paciente AND
                                                          COALESCE(EF.VALOR_PESO,0) != 0 AND
                                                          COALESCE(EF.VALOR_ALTURA,0) != 0
                                                    ORDER BY EF.DATA_EXAME_FISICO";
        string IExameFisicoCommand.GetListaIMCByPaciente { get => sqlGetListaIMCByPaciente; }

        public string sqlGetListaTemperaturaByPaciente = $@"SELECT EF.DATA_EXAME_FISICO DATA, EF.VALOR_TEMPERATURA VALOR,
                                                                   MED.CSI_CODMED ID_PROFISSIONAL, MED.CSI_NOMMED PROFISSIONAL
                                                            FROM PEP_EXAME_FISICO EF
                                                            JOIN TSI_MEDICOS MED ON MED.CSI_CODMED = EF.CODIGO_PROFISSIONAL
                                                            WHERE EF.ID_PACIENTE = @id_paciente AND
                                                                 COALESCE(EF.VALOR_TEMPERATURA, 0) != 0
                                                            ORDER BY EF.DATA_EXAME_FISICO";
        string IExameFisicoCommand.GetListaTemperaturaByPaciente { get => sqlGetListaTemperaturaByPaciente; }

        public string sqlGetListaCircunferenciaAbdominalByPaciente = $@"SELECT EF.DATA_EXAME_FISICO DATA, EF.VALOR_CIRC_ABDOMINAL VALOR, 
                                                                               MED.CSI_CODMED ID_PROFISSIONAL, MED.CSI_NOMMED PROFISSIONAL
                                                                        FROM PEP_EXAME_FISICO EF
                                                                        JOIN TSI_MEDICOS MED ON MED.CSI_CODMED = EF.CODIGO_PROFISSIONAL
                                                                        WHERE EF.ID_PACIENTE = @id_paciente AND
                                                                             COALESCE(EF.VALOR_CIRC_ABDOMINAL, 0) != 0
                                                                        ORDER BY EF.DATA_EXAME_FISICO";
        string IExameFisicoCommand.GetListaCircunferenciaAbdominalByPaciente { get => sqlGetListaCircunferenciaAbdominalByPaciente; }

        public string sqlGetListaCircunferenciaToracicaByPaciente = $@"SELECT EF.DATA_EXAME_FISICO DATA, EF.VALOR_CIRC_TORACICA VALOR, 
                                                                              MED.CSI_CODMED ID_PROFISSIONAL, MED.CSI_NOMMED PROFISSIONAL
                                                                       FROM PEP_EXAME_FISICO EF
                                                                       JOIN TSI_MEDICOS MED ON MED.CSI_CODMED = EF.CODIGO_PROFISSIONAL
                                                                       WHERE EF.ID_PACIENTE = @id_paciente AND
                                                                            COALESCE(EF.VALOR_CIRC_TORACICA, 0) != 0
                                                                       ORDER BY EF.DATA_EXAME_FISICO";
        string IExameFisicoCommand.GetListaCircunferenciaToracicaByPaciente { get => sqlGetListaCircunferenciaToracicaByPaciente; }

        public string sqlGetListaPressaoArterialByPaciente = $@"SELECT EF.DATA_EXAME_FISICO DATA, CAST(EF.VALOR_PA1 AS NUMERIC(15,2)) || ' : ' || CAST(EF.VALOR_PA2 AS NUMERIC(15,2)) PRESSAOARTERIAL,
                                                                               MED.CSI_CODMED ID_PROFISSIONAL, MED.CSI_NOMMED PROFISSIONAL
                                                                        FROM PEP_EXAME_FISICO EF
                                                                        JOIN TSI_MEDICOS MED ON MED.CSI_CODMED = EF.CODIGO_PROFISSIONAL
                                                                        WHERE EF.ID_PACIENTE = @id_paciente AND
                                                                              COALESCE(EF.VALOR_PA1,0) != 0 AND
                                                                              COALESCE(EF.VALOR_PA2,0) != 0
                                                                        ORDER BY EF.DATA_EXAME_FISICO";
        string IExameFisicoCommand.GetListaPressaoArterialByPaciente { get => sqlGetListaPressaoArterialByPaciente; }

        public string sqlGetListaPressaoArterialSistolicaByPaciente = $@"SELECT EF.DATA_EXAME_FISICO DATA, EF.VALOR_PA1 VALOR,
                                                                                    MED.CSI_CODMED ID_PROFISSIONAL, MED.CSI_NOMMED PROFISSIONAL
                                                                                FROM PEP_EXAME_FISICO EF
                                                                                JOIN TSI_MEDICOS MED ON MED.CSI_CODMED = EF.CODIGO_PROFISSIONAL
                                                                                WHERE EF.ID_PACIENTE = @id_paciente AND
                                                                                    COALESCE(EF.VALOR_PA1, 0) != 0
                                                                                ORDER BY EF.DATA_EXAME_FISICO";
        string IExameFisicoCommand.GetListaPressaoArterialSistolicaByPaciente { get => sqlGetListaPressaoArterialSistolicaByPaciente; }

        public string sqlGetListaPressaoArterialDiastolicaByPaciente = $@"SELECT EF.DATA_EXAME_FISICO DATA, EF.VALOR_PA2 VALOR,
                                                                                    MED.CSI_CODMED ID_PROFISSIONAL, MED.CSI_NOMMED PROFISSIONAL
                                                                                FROM PEP_EXAME_FISICO EF
                                                                                JOIN TSI_MEDICOS MED ON MED.CSI_CODMED = EF.CODIGO_PROFISSIONAL
                                                                                WHERE EF.ID_PACIENTE = @id_paciente AND
                                                                                    COALESCE(EF.VALOR_PA2, 0) != 0
                                                                                ORDER BY EF.DATA_EXAME_FISICO";
        string IExameFisicoCommand.GetListaPressaoArterialDiastolicaByPaciente { get => sqlGetListaPressaoArterialDiastolicaByPaciente; }

        public string sqlGetListaGlicemiaByPaciente = $@"SELECT EF.DATA_EXAME_FISICO DATA,EF.CODIGO_EXAME_FISICO ID, EF.VALOR_GLICEMIA VALOR, 
                                                                MED.CSI_CODMED ID_PROFISSIONAL, MED.CSI_NOMMED PROFISSIONAL
                                                         FROM PEP_EXAME_FISICO EF
                                                         JOIN TSI_MEDICOS MED ON MED.CSI_CODMED = EF.CODIGO_PROFISSIONAL
                                                         WHERE EF.ID_PACIENTE = @id_paciente AND
                                                              COALESCE(EF.VALOR_GLICEMIA, 0) != 0
                                                         ORDER BY EF.DATA_EXAME_FISICO";
        string IExameFisicoCommand.GetListaGlicemiaByPaciente { get => sqlGetListaGlicemiaByPaciente; }

        public string sqlGetListaSaturacaoByPaciente = $@"SELECT EF.DATA_EXAME_FISICO DATA, EF.VALOR_SATURACAO VALOR, 
                                                                 MED.CSI_CODMED ID_PROFISSIONAL, MED.CSI_NOMMED PROFISSIONAL
                                                          FROM PEP_EXAME_FISICO EF
                                                          JOIN TSI_MEDICOS MED ON MED.CSI_CODMED = EF.CODIGO_PROFISSIONAL 
                                                          WHERE EF.ID_PACIENTE = @id_paciente AND
                                                               COALESCE(EF.VALOR_SATURACAO, 0) != 0
                                                          ORDER BY EF.DATA_EXAME_FISICO";
        string IExameFisicoCommand.GetListaSaturacaoByPaciente { get => sqlGetListaSaturacaoByPaciente; }

        public string sqlGetListaFrequenciaCardiacaByPaciente = $@"SELECT EF.DATA_EXAME_FISICO DATA, EF.VALOR_FREQ_CARDIACA VALOR, 
                                                                          MED.CSI_CODMED ID_PROFISSIONAL, MED.CSI_NOMMED PROFISSIONAL
                                                                   FROM PEP_EXAME_FISICO EF
                                                                   JOIN TSI_MEDICOS MED ON MED.CSI_CODMED = EF.CODIGO_PROFISSIONAL
                                                                   WHERE EF.ID_PACIENTE = @id_paciente AND
                                                                        COALESCE(EF.VALOR_FREQ_CARDIACA, 0) != 0
                                                                   ORDER BY EF.DATA_EXAME_FISICO";
        string IExameFisicoCommand.GetListaFrequenciaCardiacaByPaciente { get => sqlGetListaFrequenciaCardiacaByPaciente; }

        public string sqlGetListaFrequenciaRespiratoriaByPaciente = $@"SELECT EF.DATA_EXAME_FISICO DATA, EF.VALOR_FREQ_RESPIRATORIA VALOR, 
                                                                              MED.CSI_CODMED ID_PROFISSIONAL, MED.CSI_NOMMED PROFISSIONAL
                                                                       FROM PEP_EXAME_FISICO EF
                                                                       JOIN TSI_MEDICOS MED ON MED.CSI_CODMED = EF.CODIGO_PROFISSIONAL
                                                                       WHERE EF.ID_PACIENTE = @id_paciente AND
                                                                            COALESCE(EF.VALOR_FREQ_RESPIRATORIA, 0) != 0
                                                                       ORDER BY EF.DATA_EXAME_FISICO";
        string IExameFisicoCommand.GetListaFrequenciaRespiratoriaByPaciente { get => sqlGetListaFrequenciaRespiratoriaByPaciente; }

        public string sqlGetListaFrequenciaCefalicoByPaciente = $@"SELECT EF.DATA_EXAME_FISICO DATA, EF.VALOR_CEFALICO VALOR, 
                                                                          MED.CSI_CODMED ID_PROFISSIONAL, MED.CSI_NOMMED PROFISSIONAL
                                                                   FROM PEP_EXAME_FISICO EF
                                                                   JOIN TSI_MEDICOS MED ON MED.CSI_CODMED = EF.CODIGO_PROFISSIONAL
                                                                   WHERE EF.ID_PACIENTE = @id_paciente AND
                                                                        COALESCE(EF.VALOR_CEFALICO, 0) != 0
                                                                   ORDER BY EF.DATA_EXAME_FISICO";
        string IExameFisicoCommand.GetListaFrequenciaCefalicoByPaciente { get => sqlGetListaFrequenciaCefalicoByPaciente; }

        public string sqlGetListaGlassGowByPaciente = $@"SELECT EF.DATA_EXAME_FISICO DATA, EF.GLASGOW VALOR, 
                                                                MED.CSI_CODMED ID_PROFISSIONAL, MED.CSI_NOMMED PROFISSIONAL
                                                         FROM PEP_EXAME_FISICO EF
                                                         JOIN TSI_MEDICOS MED ON MED.CSI_CODMED = EF.CODIGO_PROFISSIONAL
                                                         WHERE EF.ID_PACIENTE = @id_paciente AND
                                                              COALESCE(EF.GLASGOW, 0) != 0
                                                         ORDER BY EF.DATA_EXAME_FISICO";
        string IExameFisicoCommand.GetListaGlassGowByPaciente { get => sqlGetListaGlassGowByPaciente; }

        public string sqlGetListaReguaDorByPaciente = $@"SELECT EF.DATA_EXAME_FISICO DATA, EF.REGUA_DOR VALOR, 
                                                                MED.CSI_CODMED ID_PROFISSIONAL, MED.CSI_NOMMED PROFISSIONAL
                                                         FROM PEP_EXAME_FISICO EF
                                                         JOIN TSI_MEDICOS MED ON MED.CSI_CODMED = EF.CODIGO_PROFISSIONAL
                                                         WHERE EF.ID_PACIENTE = @id_paciente AND
                                                              COALESCE(EF.REGUA_DOR, 0) != 0
                                                         ORDER BY EF.DATA_EXAME_FISICO";
        string IExameFisicoCommand.GetListaReguaDorByPaciente { get => sqlGetListaReguaDorByPaciente; }

        public string sqlGetProcedimentosGeradosByPaciente = $@"SELECT EF.DATA_EXAME_FISICO DATA, (SELECT COUNT(*) FROM TSI_PROCENFERMAGEM
                                                                                                   WHERE ID_PEP_EXAME_FISICO = EF.CODIGO_EXAME_FISICO) VALOR,
                                                                    MED.CSI_CODMED ID_PROFISSIONAL, MED.CSI_NOMMED PROFISSIONAL
                                                                FROM PEP_EXAME_FISICO EF
                                                                JOIN TSI_MEDICOS MED ON MED.CSI_CODMED = EF.CODIGO_PROFISSIONAL
                                                                WHERE EF.ID_PACIENTE = @id_paciente
                                                                ORDER BY EF.DATA_EXAME_FISICO";
        string IExameFisicoCommand.GetProcedimentosGeradosByPaciente { get => sqlGetProcedimentosGeradosByPaciente; }

        public string sqlGetExameFisicoById = $@"SELECT PEP.*, PROC.CSI_CONTROLE, PAC.CSI_CODPAC, PAC.CSI_NOMPAC, MED.CSI_CODMED, MED.CSI_NOMMED, PROC.CSI_CBO FROM PEP_EXAME_FISICO PEP
                                                        LEFT JOIN TSI_PROCENFERMAGEM PROC
                                                            ON (PROC.ID_PEP_EXAME_FISICO = PEP.CODIGO_EXAME_FISICO)
                                                        LEFT JOIN TSI_MEDICOS MED
                                                            ON (MED.CSI_CODMED = PEP.CODIGO_PROFISSIONAL OR MED.CSI_CODMED = PEP.ID_PROFISSIONAL_CLAS_TRIAGEM)
                                                        LEFT JOIN TSI_CADPAC PAC
                                                            ON PAC.CSI_CODPAC = PEP.ID_PACIENTE
                                                        WHERE PEP.CODIGO_EXAME_FISICO = @id";
        string IExameFisicoCommand.GetExameFisicoById { get => sqlGetExameFisicoById; }

        public string sqlGetIprocenfermagemById = $@"SELECT CSI_CODPROC, CSI_QTDE FROM TSI_IPROCENFERMAGEM WHERE CSI_CONTROLE = @csi_controle";
        string IExameFisicoCommand.GetIprocenfermagemById { get => sqlGetIprocenfermagemById; }
    }
}
