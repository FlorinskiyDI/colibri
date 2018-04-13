using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Webapi
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
