using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Lemma.AADS.Structures;
using NUnit.Framework;

namespace Lemma.AADS.StructuresTest
{
    [TestFixture]
    public class VectorCollectionTest
    {
        #region OperationTests

        [Test]
        [TestCase(new int[] {1, 2, -3, -4, 5})]
        public void AddElements_CompareToInnerArray(int[] elements)
        {
            var collection = new MyVector<int>();

            foreach (var e in elements)
                collection.Add(e);

            var innerCollection = collection.GetType()
                .GetField("_vector", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(collection) as int[];
            var elementsToCompare = innerCollection?.Take(collection.Count()).ToArray();

            Assert.That(innerCollection != null, "innerCollection is null");

            Assert.AreEqual(elements, elementsToCompare);
        }


        [Test]
        [TestCase(new int[] { 1, 2, -3, -4, 5 }, new int[]{1, 2, 5})]
        [TestCase(new int[] { 1, 2, -3, -4, 5 }, new int[] { 1 })]
        [TestCase(new int[] { 1, 2, -3, -4, 5 }, new int[] { 0})]
        [TestCase(new int[] { 1, 2, -3, -4, 5 }, new int[] { 5, 6 })]
        public void MultiOperations_CompareToInnerArray(int[] iniElements, int[] elementsToDelete)
        {
            var collection = new MyVector<int>();

            foreach (var e in iniElements)
                collection.Add(e);

            foreach (var e in elementsToDelete)
                collection.Delete(e);

            var innerCollection = collection.GetType()
                .GetField("_vector", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(collection) as int[];
            var elementsToCompare = innerCollection?.Take(collection.Count()).ToArray();

            Assert.That(innerCollection != null, "innerCollection is null");

            Assert.AreEqual(iniElements.Except(elementsToDelete), elementsToCompare);
        }
        #endregion

        #region SortTests

        public class ClassToFill
        {
            public ClassToFill(int c, string t)
            {
                C = c;
                T = t;
            }

            public int C { get; set; }
            public string T { get; set; }
        }

        [Test]
        [TestCase(new int[] {1, 2, 3, 4, 5})]
        [TestCase(new int[] {9, 5, 0, -2, 4, 5})]
        public void SortWithoutComparer_CompareWithInnerArray(int[] iniElements)
        {
            var collection = new MyVector<int>();

            foreach (var e in iniElements)
                collection.Add(e);

            collection.Sort();

            var innerCollection = collection.GetType()
                .GetField("_vector", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(collection) as int[];
            var elementsToCompare = innerCollection?.Take(collection.Count()).ToArray();

            Assert.That(innerCollection != null, "innerCollection is null");

            Assert.AreEqual(iniElements.OrderBy(x => x).ToArray(), elementsToCompare);
        }
        #endregion
    }
}
