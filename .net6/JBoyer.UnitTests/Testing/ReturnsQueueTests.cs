using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;

namespace JBoyer.Testing
{

    [TestClass, ExcludeFromCodeCoverage]
    public class ReturnsQueueTests
    {

        [TestMethod]
        public void ReturnsQueue_Constructor_DefaultItem_ReturnsDefaultTimeWhenQueueEmpty()
        {
            // Arrange
            ReturnsQueue<int> que = new ReturnsQueue<int>(123);
            que.Enqueue(0);

            // Act
            int[] result = new int[10];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = que.GetNext();
            }

            // Assert
            CollectionAssert.AreEqual(new int[] { 0, 123, 123, 123, 123, 123, 123, 123, 123, 123 }, result);
        }

        [TestMethod, ExpectedExceptionWithMessage(typeof(InvalidOperationException), "Invalid arguments passed in for type")]
        public void ReturnsQueue_Constructor_ArgList_ThrowsExceptionWhenCannotConstructTypeWithArgs()
        {
            // Arrange

            // Act
            new ReturnsQueue<string>(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);

            // Assert
        }

        [TestMethod]
        public void ReturnsQueue_Constructor_ArgList_ReturnsContructedObjectsThroughTheConstructedObject()
        {
            // Arrange
            ReturnsQueue<string> que = new ReturnsQueue<string>('c', 3);
            que.Enqueue(new string[] { "Billy", "Vigil" });

            // Act
            string?[] result = new string[10];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = que.GetNext();
            }

            // Assert
            CollectionAssert.AreEqual(new string[] { "Billy", "Vigil", "ccc", "ccc", "ccc", "ccc", "ccc", "ccc", "ccc", "ccc" }, result);
        }

    }

}
