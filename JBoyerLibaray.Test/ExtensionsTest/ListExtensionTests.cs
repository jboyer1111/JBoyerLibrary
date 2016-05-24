using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JBoyerLibaray.Extensions;

namespace JBoyerLibaray.Test.ExtensionsTest
{
    [TestClass]
    public class ListExtensionTests
    {
        private string[] _items = new string[] { "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten"};

        [TestMethod]
        public void ShuffleTest()
        {
            string[] result = _items.Shuffle().ToArray();

            Assert.IsFalse(result.SequenceEqual(_items));
        }

        [TestMethod]
        public void ListBoxItemsTest()
        {
            object[] result = _items.ToListBoxItems();

            Assert.IsTrue(result.SequenceEqual(_items));
        }


    }
}
