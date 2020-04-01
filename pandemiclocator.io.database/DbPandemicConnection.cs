using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;
using pandemiclocator.io.database.abstractions;

namespace pandemiclocator.io.database
{
    public class DbPandemicConnection : IDbPandemicConnection
    {
        private NpgsqlConnection Connection { get; }

        public DbPandemicConnection(string connectionString)
        {
            Connection = new NpgsqlConnection(connectionString);
        }

        public void Open()
        {
            Connection.Open();
        }
        
        public NpgsqlCommand NewCommand()
        {
            return Connection.CreateCommand();
        }

        public void Close()
        {
            Connection.Close();
        }

        public void Dispose()
        {
            Connection?.Dispose();
        }
    }
}
