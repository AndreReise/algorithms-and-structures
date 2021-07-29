using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Lemma.AADS.Structures;
using NUnit.Framework;

namespace Lemma.AADS.StructuresTest
{
    [TestFixture]
    public class VectorOperationOnInstanceTest
    {
        private readonly int[] _iniValues = new int[] { 1, 2, 3, 4, 5, 6 };
        private MyVector<int> _vector;

        [SetUp]
        public void Setup()
        {
            _vector = new MyVector<int>(6);

            _vector.GetType()
                .GetField("_count", BindingFlags.Instance | BindingFlags.NonPublic)?.SetValue(_vector, 6);
            _vector.GetType()
                .GetField("_vector", BindingFlags.Instance | BindingFlags.NonPublic)?.SetValue(_vector, _iniValues);
        }

        [TearDown]
        public void Reset()
        {
            _vector.Clear();
        }

        [Test]
        public void CheckCollectionSizeProperty()
        {
            Assert.AreEqual(6, _vector.Size());
        }

        [Test]
        [TestCase(-1)]
        [TestCase(-100)]
        [TestCase(6)]
        [TestCase(10)]
        public void Update_ThrowsArgumentException(int index)
        {
            Assert.Throws<ArgumentException>(() => _vector.Update(-1, index));
        }

        [Test]
        [TestCase(5, 0)]
        [TestCase(100, 5)]
        public void Update_CheckValueReplacement(int value, int index)
        {
            _vector.Update(value, index);

            Assert.AreEqual(value, _vector[index]);
        }

        [Test]
        [TestCase(100, 1)]
        [TestCase(100, 0)]
        [TestCase(100, 4)]
        [TestCase(100, 5)]
        public void InsertionTest(int value, int index)
        {
            _vector.Insert(value, index);

            var expectedArray = new List<int>();
            expectedArray.AddRange(_iniValues);
            expectedArray.Insert(index + 1, value);


            var innerCollection = _vector.GetType()
                .GetField("_vector", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(_vector) as int[];
            var elementsToCompare = innerCollection?.Take(_vector.Size()).ToArray();

            Assert.That(innerCollection != null, "innerCollection is null");
            Assert.AreEqual(expectedArray.ToArray(), elementsToCompare);
        }
    }
}
