using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Lemma.AADS.Structures;
using NUnit.Framework;

namespace Lemma.AADS.StructuresTest
{
    [TestFixture]
    public class VectorConstructorTest
    {
        #region ConstructorTests
        [Test]
        [TestCase(-1)]
        [TestCase(-5)]
        public void ConstructorWithParameters_ThrowsArgumentException(int capacity)
        {
            Action constructor = () => new MyVector<int>(capacity);

            Assert.Throws(typeof(ArgumentException), new TestDelegate(constructor));
        }

        [Test]
        [TestCase(0)]
        [TestCase(4)]
        [TestCase(1024)]
        public void ConstructorWithParameters_CompareCapacity(int capacity)
        {
            var collection = new MyVector<int>(capacity);
            var innerCollection = collection.GetType()
                .GetField("_vector", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(collection) as int[];

            Assert.That(innerCollection != null, "innerCollection is null");

            Assert.AreEqual(capacity, innerCollection.Length);
        }
        #endregion
    }
}
