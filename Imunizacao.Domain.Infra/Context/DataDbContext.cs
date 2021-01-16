using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace RgCidadao.Domain.Infra.Context
{
    public class DataDbContext : IDisposable
    {
        public FbConnection connection { get; set; }

        public DataDbContext()
        {
          //  connection = new FbConnection(Shared.Settings.DefaultConnection);
            connection.Open();
        }

        public void Dispose()
        {
            if (connection.State != ConnectionState.Closed)
                connection.Close();
        }
    }
}
