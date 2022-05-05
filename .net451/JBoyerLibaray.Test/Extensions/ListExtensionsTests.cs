using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JBoyerLibaray.Extensions;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;
using JBoyerLibaray.DeckOfCards;
using System.Diagnostics;

namespace JBoyerLibaray.Extensions
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ListExtensionsTests
    {
        private int[] intItems = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        private string[] strItems = new string[] { "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten" };

        [TestMethod]
        public void ListExtension_Shuffle_ReOrdersListIEnum()
        {
            // Arrange

            // Act
            string[] result = strItems.Shuffle().ToArray();
            string[] result2 = strItems.Shuffle().ToArray();

            // Assert
            // This will cover most but it may shuffle to what it was before
            Assert.IsFalse(strItems.SequenceEqual(result));
            Assert.IsFalse(strItems.SequenceEqual(result2));
        }

        [TestMethod]
        public void ListExtension_Shuffle_ReOrdersListList()
        {
            // Arrange
            var result = strItems.ToList();
            var result2 = strItems.ToList();

            // Act
            result.Shuffle();
            result2.Shuffle();

            // Assert
            // This will cover most but it may shuffle to what it was before
            Assert.IsFalse(strItems.SequenceEqual(result));
            Assert.IsFalse(strItems.SequenceEqual(result2));
        }

        [TestMethod]
        public void ListExtension_Shuffle_DoesNotAddOrRemoveItems()
        {
            // Arrange

            // Act
            string[] result = strItems.Shuffle().ToArray();

            var strOrdered = strItems.ToList();
            var resultOrdered = result.ToList();

            strOrdered.Sort();
            resultOrdered.Sort();

            var refTest = true;
            for (int i = 0; i < strOrdered.Count(); i++)
            {
                if (!Object.ReferenceEquals(strOrdered[i], resultOrdered[i]))
                {
                    refTest = false;
                }
            }

            // Assert
            Assert.AreEqual(strItems.Count(), result.Count());
            Assert.IsTrue(refTest);
        }

        [TestMethod]
        public void ListExtension_ToListBoxItems_TurnsListIntoObjectArray()
        {
            // Arrange


            // Act
            var result = strItems.ToListBoxItems();

            // Assert
            Assert.IsTrue(result.SequenceEqual(strItems));
            Assert.IsInstanceOfType(result, typeof(object[]));
        }

        [TestMethod]
        public void ListExtension_ToListBoxItems_DoesNotAddOrRemoveItems()
        {
            // Arrange

            // Act
            var result = strItems.ToListBoxItems();

            var strOrdered = strItems.ToList();
            var resultOrdered = result.ToList();

            strOrdered.Sort();
            resultOrdered.Sort();

            var refTest = true;
            for (int i = 0; i < strOrdered.Count(); i++)
            {
                if (!Object.ReferenceEquals(strOrdered[i], resultOrdered[i]))
                {
                    refTest = false;
                }
            }

            // Assert
            Assert.AreEqual(strItems.Count(), result.Count());
            Assert.IsTrue(refTest);
        }

        [TestMethod]
        public void ListExtension_Random_ReturnsARandomItemFromList()
        {
            // Arrange

            // Act
            var result = strItems.Random();

            var refTest = false;
            for (int i = 0; i < strItems.Length; i++)
            {
                if (Object.ReferenceEquals(strItems[i], result))
                {
                    refTest = true;
                }
            }

            // Assert
            Assert.IsTrue(strItems.Contains(result));
            Assert.IsTrue(refTest);
        }

        [TestMethod]
        public void ListExtension_Median_ReturnsMedian()
        {
            // Arrange

            // Act
            var result = intItems.Median();

            // Assert
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(5.5, result.Sum() / 2.0);
        }

        [TestMethod]
        public void ListExtension_Median_ReturnsMedianUsingCompererPassedToSort()
        {
            // Arrange
            Card[] cards = new Card[]
            {
                new Card("A", "2"),
                new Card("B", "2"),
                new Card("B", "5"),
                new Card("C", "2"),
                new Card("D", "2")
            };

            // Act
            var result = cards.Median(CardComparer.ValueThenSuit);

            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("2 of C", result.First().ToString());
        }

        [TestMethod]
        public void ListExtension_Mode_ReturnsEmptyWhenNoMode()
        {
            // Arrange

            // Act
            var results = intItems.Mode();

            // Assert
            Assert.AreEqual(0, results.Count());
        }

        [TestMethod]
        public void ListExtension_Mode_ReturnsMode()
        {
            // Arrange
            var items = new int[]
            {
                1,
                2,
                2,
                3,
                4,
                5,
                6
            };

            // Act
            var results = items.Mode();

            // Assert
            Assert.AreEqual(1, results.Count());
            Assert.AreEqual(2, results.First());
        }

        [TestMethod]
        public void ListExtension_Mode_ReturnsModes()
        {
            // Arrange
            var items = new int[]
            {
                1,
                2,
                2,
                3,
                4,
                5,
                5,
                6
            };

            // Act
            var results = items.Mode();

            // Assert
            Assert.AreEqual(2, results.Count());
            Assert.AreEqual(2, results.First());
            Assert.AreEqual(5, results.Last());
        }

        [TestMethod]
        public void ListExtension_ForAll_DoesPredecateToAllItemsList()
        {
            // Arrange
            var items = new List<MutableKeyValuePair<string, string>>();
            items.Add(new MutableKeyValuePair<string, string>("Key", "Value"));

            // Act
            items.ForAll(i => { i.Value = "Value2"; });

            // Assert
            Assert.AreEqual("Value2", items.First().Value);
        }

        [TestMethod]
        public void ListExtension_ForAll_DoesPredecateToAllItemsIEumn()
        {
            // Arrange
            IEnumerable<MutableKeyValuePair<string, string>> items = new List<MutableKeyValuePair<string, string>>()
            {
                new MutableKeyValuePair<string, string>("Key", "Value")
            };

            // Act
            items.ForAll(i => { i.Value = "Value2"; });

            // Assert
            Assert.AreEqual("Value2", items.First().Value);
        }

        [TestMethod]
        public void ListExtension_ToBatches_BatchSize500()
        {
            // Arrange
            int batchSize = 60;
            int[] items = new int[1000000];
            for (int i = 0; i < items.Length; i++)
            {
                items[i] = i;
            }

            // Act
            int[][] result = items.ToBatches(batchSize).ToArray();

            // Assert
            Assert.AreEqual(40, result.Last().Length);
        }





























    }
}
