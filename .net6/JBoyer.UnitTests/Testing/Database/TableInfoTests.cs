using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace JBoyer.Testing.Database
{

    [TestClass, ExcludeFromCodeCoverage]
    public class TableInfoTests
    {

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void TableInfo_Constructor_IEnumerable_ThrowsErrorWhenArgumentIsNull()
        {
            // Act
            IEnumerable<TableRow> list = null;

            // Arragne
            new TableInfo<TableRow>(list);

            // Assert
            // Throws Error
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void TableInfo_Constructor_Func_ThrowsErrorWhenArgumentIsNull()
        {
            // Act
            Func<IEnumerable<TableRow>> list = null;

            // Arragne
            new TableInfo<TableRow>(list);

            // Assert
            // Throws Error
        }

        [TestMethod]
        public void TableInfo_GetResults_ListOfObjects()
        {
            // Act
            var list = Enumerable.Empty<TableRow>();
            var tableInfo = new TableInfo<TableRow>(list);

            // Arragne
            var result = tableInfo.GetResults();

            // Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<TableRow>), "Not Correct type!");
        }

        [TestMethod]
        public void TableInfo_GetResults_ListOfObjectsFunction()
        {
            // Act
            var list = Enumerable.Empty<TableRow>();
            var tableInfo = new TableInfo<TableRow>(() => list);

            // Arragne
            var result = tableInfo.GetResults();

            // Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<TableRow>), "Not Correct type!");
        }

        [TestMethod, ExpectedException(typeof(SqlException))]
        public void TableInfo_GetResults_ThrowsException()
        {
            // Act
            Func<IEnumerable<TableRow>> resolver = () =>
            {
                throw UTObjectCreator.NewSqlException(100);
            };

            var tableInfo = new TableInfo<TableRow>(resolver);

            // Arragne
            tableInfo.GetResults();

            // Assert
            // Throws Error
        }

    }

}
