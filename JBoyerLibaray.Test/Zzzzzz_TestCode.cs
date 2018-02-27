using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using JBoyerLibaray.Exceptions;
using JBoyerLibaray.UnitTests;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using JBoyerLibaray.Extensions;
using System.Security.Cryptography;
using JBoyerLibaray.UnitTests.Database;
using Dapper;
using Dapper.Contrib.Extensions;
using JBoyerLibaray.Database;
using Moq;
using System.Data;

namespace JBoyerLibaray
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Zzzzzz_TestCode
    {
        [TestMethod]
        public void Zzzzzz_TestMethodOne()
        {
            ReaderData readerData = new ReaderData();

            Mock<IDataReader> mockDataReader = new Mock<IDataReader>();

            readerData.Add(new DataResult(mockDataReader.Object));




            readerData.Add(new DataResult(mockDataReader.Object));

            var test = "";
            
            //var database = new FakeDatabase();
            //List<RandomTable> results = new List<RandomTable>();

            //results.Add(new RandomTable() { Test1 = 0, Test2 = "Searches", Test3 = DateTime.Now });
            //results.Add(new RandomTable() { Test1 = 1, Test2 = "Parties", Test3 = DateTime.Now.AddDays(5) });
            //results.Add(new RandomTable() { Test1 = 2, Test2 = "LockedOrders", Test3 = DateTime.Now.AddDays(-20) });

            //database.SetupTable("RandomTables", results);
            //database.SetupStoredProcedure("USP_GetRandomTables", results, new string[] { "Test" });

            //database.SetupSql("Select * From RandomTables Order By Test2", results.OrderBy(r => r.Test2).ToArray());
            //database.SetupSql("Select * From RandomTables Order By Test3", (d, p) =>
            //{
            //    return results.OrderBy(r => r.Test3);
            //});

            //database.SetupSql("Select * From RandomTables Where Test1 > @Temp Order By Test2", results.Where(r => r.Test1 > 0).OrderBy(r => r.Test2).ToArray(), new string[] { "Temp" });
            //database.SetupSql("Select * From RandomTables Where Test1 > @Temp Order By Test3", (d, p) =>
            //{
            //    return results.Where(r => r.Test1 > 0).OrderBy(r => r.Test3);
            //}, new string[] { "Temp" });

            //using (var connection = new FakeConnection(database))
            //{
            //    var tableResults = connection.GetAll<RandomTable>();
            //    var tableResult = connection.Get<RandomTable>(1);
            //    var spResults = connection.Query<RandomTable>("USP_GetRandomTables", new { Test = ":D" }, commandType: System.Data.CommandType.StoredProcedure);
            //    var sqlResult = connection.Query<RandomTable>("Select * From RandomTables Order By Test2");
            //    var sqlResult2 = connection.Query<RandomTable>("Select * From RandomTables Order By Test3");

            //    var sqlRequiredResult = connection.Query<RandomTable>("Select * From RandomTables Where Test1 > @Temp Order By Test2", new { @Temp = 0 });
            //    var sqlRequiredResult2 = connection.Query<RandomTable>("Select * From RandomTables Where Test1 > @Temp Order By Test3", new { @Temp = 0 });

            //    var count = spResults.Count();

            //    //Assert.IsTrue();
            //}
        }
    }
}
