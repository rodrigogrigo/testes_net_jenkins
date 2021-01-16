using FirebirdSql.Data.FirebirdClient;
using FirebirdSql.Data.Isql;
using System;

namespace RgCidadao.Domain.Infra.Helpers
{
    public static class HelperConnection
    {
        public static void ExecuteCommand(string ibge, Action<FbConnection> task)
        {
            using (var conn = new FbConnection(ibge))
            {
                conn.Open();
                task(conn);
            }
        }

        public static T ExecuteCommand<T>(string ibge, Func<FbConnection, T> task)
        {
            using (var conn = new FbConnection(ibge))
            {
                conn.Open();
                return task(conn);
            }
        }

        public static void ExecuteCommandFoto(string ibge, Action<FbConnection> task)
        {
            using (var conn = new FbConnection(ibge))
            {
                conn.Open();
                task(conn);
            }
        }

        public static T ExecuteCommandFoto<T>(string ibge, Func<FbConnection, T> task)
        {
            using (var conn = new FbConnection(ibge))
            {
                conn.Open();
                return task(conn);
            }
        }

        public static bool TestaConnection(string ibge)
        {
            try
            {
                using (var conn = new FbConnection(ibge))
                {
                    conn.Open();
                }
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //teste para bloco de codigo
        public static void ExecuteCommandBloco(string ibge, string scripttext)
        {
            using (var conn = new FbConnection(ibge))
            {
                conn.Open();
                FbScript script = new FbScript(scripttext);
                script.Parse();
                FbBatchExecution fbe = new FbBatchExecution(conn);
                fbe.AppendSqlStatements(script);
                fbe.Execute();


            }
        }
    }
}
