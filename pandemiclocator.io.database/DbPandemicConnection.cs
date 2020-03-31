using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;
using pandemiclocator.io.database.abstractions;

namespace pandemiclocator.io.database
{
    public class DbPandemicConnection : IDbPandemicConnection
    {
        public DbPandemicConnection(string connectionString)
        {
            Connection = new NpgsqlConnection(connectionString);
        }

        public NpgsqlConnection Connection { get; }

        public void Dispose()
        {
            Connection?.Dispose();
        }
    }
}
