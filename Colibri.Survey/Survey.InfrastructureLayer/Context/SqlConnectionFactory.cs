using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Survey.InfrastructureLayer.Context
{
    public static class SqlConnectionFactory
    {
        public static string ConnectionString { get; set; }

        public static SqlConnection Create()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
