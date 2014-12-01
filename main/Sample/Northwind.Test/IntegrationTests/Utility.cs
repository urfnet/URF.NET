#region

using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace Northwind.Test.IntegrationTests
{
    public static class Utility
    {
        public static void CreateSeededTestDatabase()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["MasterDbConnection"].ConnectionString;

            var path = Environment.CurrentDirectory.Replace("bin\\Debug", "Sql\\instnwnd.sql");
            var file = new FileInfo(path);
            var script = file.OpenText().ReadToEnd();

            using (var connection = new SqlConnection(connectionString))
            {
                var server = new Server(new ServerConnection(connection));
                server.ConnectionContext.ExecuteNonQuery(script);
            }
        }
    }
}