using JBoyerLibaray.HelperClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace JBoyerLibaray.UnitTests.Database
{

    [TestClass, ExcludeFromCodeCoverage]
    public class StoredProcedureInfoTests
    {

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentNullException), "Value cannot be null.\r\nParameter name: result")]
        public void StoredProcedureInfo_Constructor_T_ThrowsErrorWhenResultIsNull()
        {
            // Arrange
            TableRow obj = null;

            // Act
            new StoredProcedureInfo<TableRow>(obj, new string[] { });

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentNullException), "Value cannot be null.\r\nParameter name: expectedParameters")]
        public void StoredProcedureInfo_Constructor_T_ThrowsErrorWhenExpectedParametersIsNull()
        {
            // Arrange

            // Act
            new StoredProcedureInfo<TableRow>(new TableRow(1), null);

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentNullException), "Value cannot be null.\r\nParameter name: results")]
        public void StoredProcedureInfo_Constructor_IEnumerable_ThrowsErrorWhenResultIsNull()
        {
            // Arrange
            IEnumerable<TableRow> obj = null;

            // Act
            new StoredProcedureInfo<TableRow>(obj, new string[] { });

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentNullException), "Value cannot be null.\r\nParameter name: expectedParameters")]
        public void StoredProcedureInfo_Constructor_IEnumerable_ThrowsErrorWhenExpectedParametersIsNull()
        {
            // Arrange

            // Act
            new StoredProcedureInfo<TableRow>(new TableRow[] { }, null);

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentNullException), "Value cannot be null.\r\nParameter name: storedProcedureResultResolver")]
        public void StoredProcedureInfo_Constructor_Func_ThrowsErrorWhenResultIsNull()
        {
            // Arrange
            Func<FakeDatabase, IDataParameterCollection, IEnumerable<TableRow>> obj = null;

            // Act
            new StoredProcedureInfo<TableRow>(obj, new string[] { });

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentNullException), "Value cannot be null.\r\nParameter name: expectedParameters")]
        public void StoredProcedureInfo_Constructor_Func_ThrowsErrorWhenExpectedParametersIsNull()
        {
            // Arrange

            // Act
            new StoredProcedureInfo<TableRow>((d, p) => new TableRow[] { }, null);

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentNullException), "Value cannot be null.\r\nParameter name: multiResultSetResolver")]
        public void StoredProcedureInfo_Constructor_Multi_ThrowsErrorWhenResultIsNull()
        {
            // Arrange
            Func<FakeDatabase, IDataParameterCollection, MultiResultSet> obj = null;

            // Act
            new StoredProcedureInfoMulti(obj, new string[] { });

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(ArgumentNullException), "Value cannot be null.\r\nParameter name: expectedParameters")]
        public void StoredProcedureInfo_Constructor_Multi_ThrowsErrorWhenExpectedParametersIsNull()
        {
            // Arrange

            // Act
            new StoredProcedureInfoMulti((d, p) => new MultiResultSet(), null);

            // Assert
        }

        [TestMethod]
        public void StoredProcedureInfo_GetResults_ListOfObjectsResult()
        {
            // Arrange
            var spInfo = new StoredProcedureInfo<TableRow>(new TableRow(1), new string[] { });

            // Act
            var result = spInfo.GetResults(new FakeDatabase(), new FakeParameterCollection());

            // Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<TableRow>), "Not Correct type!");
        }

        [TestMethod]
        public void StoredProcedureInfo_GetResults_ListOfObjectsResults()
        {
            // Arrange
            var spInfo = new StoredProcedureInfo<TableRow>(new TableRow[] { }, new string[] { });

            // Act
            var result = spInfo.GetResults(new FakeDatabase(), new FakeParameterCollection());

            // Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<TableRow>), "Not Correct type!");
        }

        [TestMethod]
        public void StoredProcedureInfo_GetResults_ListOfObjectsFunction()
        {
            // Arrange
            var spInfo = new StoredProcedureInfo<TableRow>((d, p) => new TableRow[] { }, new string[] { });

            // Act
            var result = spInfo.GetResults(new FakeDatabase(), new FakeParameterCollection());

            // Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<TableRow>), "Not Correct type!");
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(Exception), "Stored Procedure is setup as a Mutilple Result set. You need to call GetStoredProcedureMultiResults.")]
        public void StoredProcedureInfo_GetResults_MulitResultSets()
        {
            // Arrange
            var spInfo = new StoredProcedureInfoMulti((d, p) => new MultiResultSet(), new string[] { });

            // Act
            var result = spInfo.GetResults(new FakeDatabase(), new FakeParameterCollection());

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(InvalidOperationException), "Stored Procedure is missing a required Parameter with the name \"Missing\".")]
        public void StoredProcedureInfo_GetResults_ThrowsExceptionWhenMissAParameter()
        {
            // Act

            var tableInfo = new StoredProcedureInfo<TableRow>(new TableRow(1), new string[] { "Missing" });

            // Arragne
            tableInfo.GetResults(new FakeDatabase(), new FakeParameterCollection());

            // Assert
            // Throws Error
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(InvalidOperationException), "Stored Procedure was passed an extra Parameter with the name \"Extra\".")]
        public void StoredProcedureInfo_GetResults_ThrowsExceptionWhenHasAnExtraParameter()
        {
            // Act
            var tableInfo = new StoredProcedureInfo<TableRow>(new TableRow(1), new string[] { });
            var fakeParameterCollection = new FakeParameterCollection();
            fakeParameterCollection.Add(new FakeParameter("Extra", "Value"));

            // Arragne
            tableInfo.GetResults(new FakeDatabase(), fakeParameterCollection);

            // Assert
            // Throws Error
        }

        [TestMethod, ExpectedException(typeof(InvalidCastException))]
        public void StoredProcedureInfo_GetResults_ThrowsExceptionWhenUnableToCastToIDataParameter()
        {
            // Act
            var parameterCollection = new List<TableRow>() { new TableRow(1) };

            var tableInfo = new StoredProcedureInfo<TableRow>(new TableRow(1), new string[] { });
            Mock<IDataParameterCollection> mockParameterCollection = new Mock<IDataParameterCollection>();
            mockParameterCollection.Setup(c => c.GetEnumerator()).Returns(() => parameterCollection.GetEnumerator());

            // Arragne
            tableInfo.GetResults(new FakeDatabase(), mockParameterCollection.Object);

            // Assert
            // Throws Error
        }

        [TestMethod, ExpectedException(typeof(SqlException))]
        public void StoredProcedureInfo_GetResults_ThrowsException()
        {
            // Act
            Func<FakeDatabase, IDataParameterCollection, IEnumerable<TableRow>> resolver = (d, p) =>
            {
                throw UTObjectCreator.NewSqlException(100);
            };

            var tableInfo = new StoredProcedureInfo<TableRow>(resolver, new string[] { });

            // Arragne
            tableInfo.GetResults(new FakeDatabase(), new FakeParameterCollection());

            // Assert
            // Throws Error
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(Exception), "Stored Procedure is setup as a Single Result set. You need to call GetStoredProcedureResults.")]
        public void StoredProcedureInfo_GetMultiResulsts_ListOfObjectsResult()
        {
            // Arrange
            var spInfo = new StoredProcedureInfo<TableRow>(new TableRow(1), new string[] { });

            // Act
            var result = spInfo.GetMultiResulsts(new FakeDatabase(), new FakeParameterCollection());

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(Exception), "Stored Procedure is setup as a Single Result set. You need to call GetStoredProcedureResults.")]
        public void StoredProcedureInfo_GetMultiResulsts_ListOfObjectsResults()
        {
            // Arrange
            var spInfo = new StoredProcedureInfo<TableRow>(new TableRow[] { }, new string[] { });

            // Act
            var result = spInfo.GetMultiResulsts(new FakeDatabase(), new FakeParameterCollection());

            // Assert
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(Exception), "Stored Procedure is setup as a Single Result set. You need to call GetStoredProcedureResults.")]
        public void StoredProcedureInfo_GetMultiResulsts_ListOfObjectsFunction()
        {
            // Arrange
            var spInfo = new StoredProcedureInfo<TableRow>((d, p) => new TableRow[] { }, new string[] { });

            // Act
            var result = spInfo.GetMultiResulsts(new FakeDatabase(), new FakeParameterCollection());

            // Assert
        }

        [TestMethod]
        public void StoredProcedureInfo_GetMultiResulsts_MulitResultSets()
        {
            // Arrange
            var multiResultSet = new MultiResultSet();
            multiResultSet.Add(new TableRow[] { });
            multiResultSet.Add(ScalarResultSet.CreateResults(new int[] { }));

            var spInfo = new StoredProcedureInfoMulti((d, p) => multiResultSet, new string[] { });

            // Act
            var results = spInfo.GetMultiResulsts(new FakeDatabase(), new FakeParameterCollection());

            // Assert
            Assert.AreEqual(2, results.Count());
            Assert.IsInstanceOfType(results.First(), typeof(IEnumerable<TableRow>), "Not Correct type!");
            Assert.IsInstanceOfType(results.Last(), typeof(IEnumerable<ScalarValue<int>>), "Not Correct type!");
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(InvalidOperationException), "Stored Procedure is missing a required Parameter with the name \"Missing\".")]
        public void StoredProcedureInfo_GetMultiResulsts_ThrowsExceptionWhenMissAParameter()
        {
            // Act
            var multiResultSet = new MultiResultSet();
            multiResultSet.Add(new TableRow[] { });
            multiResultSet.Add(ScalarResultSet.CreateResults(new int[] { }));

            var tableInfo = new StoredProcedureInfoMulti((d, p) => multiResultSet, new string[] { "Missing" });

            // Arragne
            tableInfo.GetMultiResulsts(new FakeDatabase(), new FakeParameterCollection());

            // Assert
            // Throws Error
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(InvalidOperationException), "Stored Procedure was passed an extra Parameter with the name \"Extra\".")]
        public void StoredProcedureInfo_GetMultiResulsts_ThrowsExceptionWhenHasAnExtraParameter()
        {
            // Act
            var multiResultSet = new MultiResultSet();
            multiResultSet.Add(new TableRow[] { });
            multiResultSet.Add(ScalarResultSet.CreateResults(new int[] { }));

            var tableInfo = new StoredProcedureInfoMulti((d, p) => multiResultSet, new string[] { });
            var fakeParameterCollection = new FakeParameterCollection();
            fakeParameterCollection.Add(new FakeParameter("Extra", "Value"));

            // Arragne
            tableInfo.GetMultiResulsts(new FakeDatabase(), fakeParameterCollection);

            // Assert
            // Throws Error
        }

        [TestMethod, ExpectedException(typeof(InvalidCastException))]
        public void StoredProcedureInfo_GetMultiResulsts_ThrowsExceptionWhenUnableToCastToIDataParameter()
        {
            // Act
            var parameterCollection = new List<TableRow>() { new TableRow(1) };

            var tableInfo = new StoredProcedureInfoMulti((d, p) => new MultiResultSet(), new string[] { });
            Mock<IDataParameterCollection> mockParameterCollection = new Mock<IDataParameterCollection>();
            mockParameterCollection.Setup(c => c.GetEnumerator()).Returns(() => parameterCollection.GetEnumerator());

            // Arragne
            tableInfo.GetMultiResulsts(new FakeDatabase(), mockParameterCollection.Object);

            // Assert
            // Throws Error
        }

        [TestMethod, ExpectedException(typeof(SqlException))]
        public void StoredProcedureInfo_GetMultiResulsts_ThrowsException()
        {
            // Act
            Func<FakeDatabase, IDataParameterCollection, MultiResultSet> resolver = (d, p) =>
            {
                throw UTObjectCreator.NewSqlException(100);
            };

            var tableInfo = new StoredProcedureInfoMulti(resolver, new string[] { });

            // Arragne
            tableInfo.GetMultiResulsts(new FakeDatabase(), new FakeParameterCollection());

            // Assert
            // Throws Error
        }

        [TestMethod]
        public void StoredProcedureInfo_GetScalar_ReturnsObject()
        {
            // Arrange
            var spInfo = new StoredProcedureInfo<TableRow>(new TableRow(1), new string[] { });

            // Act
            var result = spInfo.GetScalar(new FakeDatabase(), new FakeParameterCollection());

            // Assert
            Assert.IsInstanceOfType(result, typeof(DateTime), "Not Correct type!");
        }

        [TestMethod]
        public void StoredProcedureInfo_GetScalar_ReturnsObjectFromList()
        {
            // Arrange
            var spInfo = new StoredProcedureInfo<TableRow>(new TableRow[] { new TableRow(1) }, new string[] { });

            // Act
            var result = spInfo.GetScalar(new FakeDatabase(), new FakeParameterCollection());

            // Assert
            Assert.IsInstanceOfType(result, typeof(DateTime), "Not Correct type!");
        }

        [TestMethod]
        public void StoredProcedureInfo_GetScalar_ReturnsNegOneFromEmptyList()
        {
            // Arrange
            var spInfo = new StoredProcedureInfo<TableRow>(new TableRow[] { }, new string[] { });

            // Act
            var result = spInfo.GetScalar(new FakeDatabase(), new FakeParameterCollection());

            // Assert
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void StoredProcedureInfo_GetScalar_ReturnsObjectFromFunction()
        {
            // Arrange
            var spInfo = new StoredProcedureInfo<TableRow>((d, p) => new TableRow[] { new TableRow(1) }, new string[] { });

            // Act
            var result = spInfo.GetScalar(new FakeDatabase(), new FakeParameterCollection());

            // Assert
            Assert.IsInstanceOfType(result, typeof(DateTime), "Not Correct type!");
        }

        [TestMethod]
        public void StoredProcedureInfo_GetScalar_ReturnsObjectFromMulti()
        {
            // Arrange
            var multiResultSet = new MultiResultSet();
            multiResultSet.Add(new TableRow[] { new TableRow(1) });
            multiResultSet.Add(new TableRow[] { new TableRow(1) });

            var spInfo = new StoredProcedureInfoMulti((d, p) => multiResultSet, new string[] { });

            // Act
            var result = spInfo.GetScalar(new FakeDatabase(), new FakeParameterCollection());

            // Assert
            Assert.IsInstanceOfType(result, typeof(DateTime), "Not Correct type!");
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(InvalidOperationException), "Stored Procedure is missing a required Parameter with the name \"Missing\".")]
        public void StoredProcedureInfo_GetScalar_ThrowsExceptionWhenMissAParameter()
        {
            // Act

            var tableInfo = new StoredProcedureInfo<TableRow>(new TableRow(1), new string[] { "Missing" });

            // Arragne
            tableInfo.GetScalar(new FakeDatabase(), new FakeParameterCollection());

            // Assert
            // Throws Error
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(InvalidOperationException), "Stored Procedure was passed an extra Parameter with the name \"Extra\".")]
        public void StoredProcedureInfo_GetScalar_ThrowsExceptionWhenHasAnExtraParameter()
        {
            // Act
            var tableInfo = new StoredProcedureInfo<TableRow>(new TableRow(1), new string[] { });
            var fakeParameterCollection = new FakeParameterCollection();
            fakeParameterCollection.Add(new FakeParameter("Extra", "Value"));

            // Arragne
            tableInfo.GetScalar(new FakeDatabase(), fakeParameterCollection);

            // Assert
            // Throws Error
        }

        [TestMethod, ExpectedException(typeof(InvalidCastException))]
        public void StoredProcedureInfo_GetScalar_ThrowsExceptionWhenUnableToCastToIDataParameter()
        {
            // Act
            var parameterCollection = new List<TableRow>() { new TableRow(1) };

            var tableInfo = new StoredProcedureInfo<TableRow>(new TableRow(1), new string[] { });
            Mock<IDataParameterCollection> mockParameterCollection = new Mock<IDataParameterCollection>();
            mockParameterCollection.Setup(c => c.GetEnumerator()).Returns(() => parameterCollection.GetEnumerator());

            // Arragne
            tableInfo.GetScalar(new FakeDatabase(), mockParameterCollection.Object);

            // Assert
            // Throws Error
        }

        [TestMethod, ExpectedException(typeof(SqlException))]
        public void StoredProcedureInfo_GetScalar_ThrowsException()
        {
            // Act
            Func<FakeDatabase, IDataParameterCollection, IEnumerable<TableRow>> resolver = (d, p) =>
            {
                throw UTObjectCreator.NewSqlException(100);
            };

            var tableInfo = new StoredProcedureInfo<TableRow>(resolver, new string[] { });

            // Arragne
            tableInfo.GetScalar(new FakeDatabase(), new FakeParameterCollection());

            // Assert
            // Throws Error
        }

    }

}
