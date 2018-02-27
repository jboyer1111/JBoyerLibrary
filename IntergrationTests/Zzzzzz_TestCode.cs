using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Web.XmlTransform;
using System.IO;
using System.Diagnostics.CodeAnalysis;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;
using JBoyerLibaray.Database;

namespace JBoyerLibaray
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Zzzzzz_TestCode
    {
        [TestMethod]
        public void Zzzzzz_TestMethod()
        {
            var connectionSetting = ConfigurationManager.ConnectionStrings["JBoyerDB"];
            if (connectionSetting == null)
            {
                Assert.Fail("Missing connection setting in app.config");
            }

            using (var connection = new SqlConnection(connectionSetting.ConnectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "Select * From People";

                var result = command.ExecuteReaderToData();
            }
        }

    }
}
