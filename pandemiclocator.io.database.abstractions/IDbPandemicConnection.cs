using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;

namespace pandemiclocator.io.database.abstractions
{
    public interface IDbPandemicConnection : IDisposable
    {
        void Open();
        NpgsqlCommand NewCommand();
        void Close();
    }
}
