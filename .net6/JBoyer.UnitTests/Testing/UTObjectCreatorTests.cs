using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;

namespace JBoyer.Testing
{

    [TestClass, ExcludeFromCodeCoverage]
    public class UTObjectCreatorTests
    {

        [TestMethod]
        public void UTObjectCreator_NewSqlException_NoMessage_BuildsPropertly()
        {
            try
            {
                // Arrange

                // Act
                throw UTObjectCreator.NewSqlException(100);

                // Assert
            }
            catch (SqlException e) when (e.ErrorCode == 100)
            {
                return;
            }
        }

        [TestMethod]
        public void UTObjectCreator_NewSqlException_Message_BuildsPropertly()
        {
            try
            {
                // Arrange

                // Act
                throw UTObjectCreator.NewSqlException(100, "Message");

                // Assert
            }
            catch (SqlException e) when (e.ErrorCode == 100 && string.Equals(e.Message, "Message"))
            {
                return;
            }
        }

    }

}
