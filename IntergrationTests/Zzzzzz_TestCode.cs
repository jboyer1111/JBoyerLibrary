using JBoyerLibaray.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;

namespace JBoyerLibaray
{

    [TestClass, ExcludeFromCodeCoverage]
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
