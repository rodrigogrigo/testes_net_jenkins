using RgCidadao.Domain.Commands.Endemias;
using System;
using System.Collections.Generic;
using System.Text;

namespace RgCidadao.Domain.Queries.Endemias
{
    public class CicloCommandText : ICicloCommand
    {
        public string sqlGetCountAll = $@"SELECT COUNT(*) FROM ENDEMIAS_CICLOS
                                          @filtro";
        string ICicloCommand.GetCountAll { get => sqlGetCountAll; }

        public string sqlGetAllPagination = $@"SELECT FIRST (@pagesize) SKIP (@page) * FROM ENDEMIAS_CICLOS
                                               @filtro
                                               ORDER BY ID DESC";
        string ICicloCommand.GetAllPagination { get => sqlGetAllPagination; }

        public string sqlGetNewId = $@"SELECT GEN_ID(GEN_ENDEMIAS_CICLOS_ID, 1) AS VLR FROM RDB$DATABASE";
        string ICicloCommand.GetNewId { get => sqlGetNewId; }

        public string sqlInsert = $@"INSERT INTO ENDEMIAS_CICLOS (ID,DATA_INICIAL,DATA_FINAL,NUM_CICLO, SITUACAO, DATA_SITUACAO, ID_USUARIO)
                                     VALUES(@id, @data_inicial, @data_final, @num_ciclo, @situacao, @data_situacao, @id_usuario)";
        string ICicloCommand.Insert { get => sqlInsert; }

        public string sqlGetCicloById = $@"SELECT * FROM ENDEMIAS_CICLOS
                                           WHERE ID = @id";
        string ICicloCommand.GetCicloById { get => sqlGetCicloById; }

        public string sqlUpdate = $@"UPDATE ENDEMIAS_CICLOS
                                     SET DATA_INICIAL = @data_inicial,
                                         DATA_FINAL = @data_final,
                                         NUM_CICLO = @num_ciclo,
                                         SITUACAO = @situacao, 
                                         DATA_SITUACAO = @data_situacao,
                                         ID_USUARIO = @id_usuario
                                     WHERE ID = @id";
        string ICicloCommand.Update { get => sqlUpdate; }

        public string sqlDelete = $@"DELETE FROM ENDEMIAS_CICLOS
                                     WHERE ID = @id";
        string ICicloCommand.Delete { get => sqlDelete; }

        public string sqlGetAllCiclosAtivos = $@"SELECT * FROM ENDEMIAS_CICLOS";
        string ICicloCommand.GetAllCiclosAtivos { get => sqlGetAllCiclosAtivos; }

        public string sqlValidaExistenciaCicloPeriodo = $@"SELECT * FROM ENDEMIAS_CICLOS CI
                                                           WHERE (CI.DATA_INICIAL BETWEEN @datainicial AND @datafinal OR
                                                                  CI.DATA_FINAL BETWEEN @datainicial AND @datafinal)";
        string ICicloCommand.ValidaExistenciaCicloPeriodo { get => sqlValidaExistenciaCicloPeriodo; }

        public string sqlGetNumCiclosRestantes = $@"SELECT DISTINCT EC.NUM_CICLO, EC.DATA_FINAL FROM ENDEMIAS_CICLOS EC
                                                    WHERE EXTRACT(YEAR FROM EC.DATA_INICIAL) = EXTRACT(YEAR FROM CURRENT_DATE) AND
                                                          EC.NUM_CICLO IS NOT NULL";
        string ICicloCommand.GetNumCiclosRestantes { get => sqlGetNumCiclosRestantes; }

        public string sqlGetCicloByData = $@"SELECT * FROM ENDEMIAS_CICLOS EC
                                             WHERE @data BETWEEN EC.DATA_INICIAL AND EC.DATA_FINAL";
        string ICicloCommand.GetCicloByData { get => sqlGetCicloByData; }

        public string sqlCriarLogCiclo = $@"INSERT INTO ENDEMIAS_CICLOS_LOG (ID,ID_CICLO,SITUACAO,DATA_SITUACAO, ID_USUARIO)
                                            VALUES(@id,@id_ciclo,@situacao,@data_situacao, @id_usuario)";
        string ICicloCommand.CriarLogCiclo { get => sqlCriarLogCiclo; }

        public string sqlGetLogCicloByCiclo = $@"SELECT LOG.*, SEG.NOME USUARIO FROM ENDEMIAS_CICLOS_LOG LOG
                                                 JOIN SEG_USUARIO SEG ON SEG.ID = LOG.ID_USUARIO
                                                 WHERE ID_CICLO = @id_ciclo
                                                 ORDER BY DATA_SITUACAO DESC";
        string ICicloCommand.GetLogCicloByCiclo { get => sqlGetLogCicloByCiclo; }

        public string sqlGetLogCicloNewId = $@"SELECT GEN_ID(GEN_ENDEMIAS_CICLOS_LOG_ID, 1) AS VLR FROM RDB$DATABASE";
        string ICicloCommand.GetLogCicloNewId { get => sqlGetLogCicloNewId; }

        public string sqlExcluirLogsByCiclo = $@"DELETE FROM ENDEMIAS_CICLOS_LOG
                                                 WHERE ID_CICLO = @id_ciclo";
        string ICicloCommand.ExcluirLogsByCiclo { get => sqlExcluirLogsByCiclo; }

        public string sqlGetAll = $@"SELECT * FROM ENDEMIAS_CICLOS";
        string ICicloCommand.GetAll { get => sqlGetAll; }

        public string sqlCountVisitasCiclo = $@"SELECT COUNT(*)
                                                FROM VISITA_IMOVEL VI
                                                WHERE VI.ID_CICLO = @id_ciclo
                                                AND VI.DATA_HORA_ENTRADA NOT BETWEEN @data_inicial AND @data_final;";
        string ICicloCommand.CountVisitasCiclo { get => sqlCountVisitasCiclo; }

        public string sqlGetDataMaximaCiclo = $@"SELECT MAX(VI.DATA_HORA_ENTRADA) FROM VISITA_IMOVEL VI WHERE VI.ID_CICLO = @id_ciclo";
        string ICicloCommand.GetDataMaximaCiclo { get => sqlGetDataMaximaCiclo; }

        public string sqlGetDataMinimaCiclo = $@"SELECT MIN(VI.DATA_HORA_ENTRADA) FROM VISITA_IMOVEL VI WHERE VI.ID_CICLO = @id_ciclo";
        string ICicloCommand.GetDataMinimaCiclo { get => sqlGetDataMinimaCiclo; }
    }
}
