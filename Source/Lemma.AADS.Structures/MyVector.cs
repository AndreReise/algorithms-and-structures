using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Structures;

namespace Lemma.AADS.Structures
{
    public class MyVector<T> : IMyList<T> where T : IComparable
    {
        private T[] _vector;
        private int _capacity;
        private int _count;

        /// <summary>
        /// Private filed to track all collection modifications
        /// </summary>
        private int _version;

        public MyVector()
        {
            _vector = new T[8];
            _capacity = 8;
            _version = 0;
            _count = -1;
        }

        public MyVector(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentException();

            _vector = new T[capacity];
            _capacity = capacity;
            _version = 0;
            _count = -1;
        }

        public T this[int index]
        {
            get => _vector[index];

            set => this.Update(value, index);
        }

        public int Size() => _count;

        public void Add(T entity)
        {
            _version++;

            if (_count + 1 == _capacity)
            {
                Resize();
            }

            _vector[_count + 1] = entity;
            _count++;
        }

        public void Delete(T entity)
        {
            _version++;

            if (_count == 0)
                throw new IndexOutOfRangeException();

            var index = FindIndex(entity);

            if (index == -1)
                return;

            if (index == _count)
            {
                _vector[index] = default;
                _count--;
                return;
            }

            for (int i = index; i <= _count; i++)
            {
                _vector[i] = _vector[i + 1];
            }

            _count--;
        }

        public void Insert(T entity, int index)
        {
            if (index < -1 || index >= _count)
                throw new ArgumentException("Index must be included in {-1; ... ; count - 1;}");

            _version++;

            if (_count + 1 >= _capacity)
            {
                Resize();
            }

            for (int i = _count; i > index; i--)
            {
                _vector[i + 1] = _vector[i];
            }

            _vector[index + 1] = entity;
            _count++;
        }

        public T Get(int index)
        {
            if (index < -1 || index > _count)
                throw new ArgumentException();

            return _vector[index];
        }

        public void Update(T entity, int index)
        {

            if (index < 0 || index >= _count)
                throw new ArgumentException();

            _vector[index] = entity;
        }

        public void Clear()
        {
            _vector = new T[8];
            _capacity = 8;
            _count = -1;
        }

        public void Sort<TResult>(Func<T, TResult> comparer = default) where TResult : IComparable
        {
            if (comparer == null)
            {
                Sort();
                return;
            }

            if (typeof(TResult).GetInterfaces().All(x => x != typeof(IComparable<T>)))
            {
                throw new ArgumentException("Not supported type");
            }

            _version++;

            SortWithComparer(comparer);
        }

        public void Sort()
        {
            for (int i = 0; i <= _count; i++)
                for (int j = i; j <= _count; j++)
                {
                    if (_vector[i].CompareTo(_vector[j]) != 1) continue;

                    var tmp = _vector[i];
                    _vector[i] = _vector[j];
                    _vector[j] = tmp;
                }
        }

        private void SortWithComparer<TResult>(Func<T, TResult> comparer) where TResult : IComparable
        {
            for (int i = 0; i <= _count; i++)
                for (int j = i; j <= _count; j++)
                {
                    var fVal = comparer(_vector[i]);
                    var sVal = comparer(_vector[j]);

                    if (fVal.CompareTo(sVal) != 1) continue;

                    var tmp = _vector[i];
                    _vector[i] = _vector[j];
                    _vector[j] = tmp;
                }
        }

        private void Resize()
        {
            var newCapacity = _vector.Length * 2;

            //Allow list to grow up to Int32 max value
            if ((uint) newCapacity > int.MaxValue) newCapacity = int.MaxValue;
            var newVector = new T[newCapacity];

            _vector.CopyTo(newVector,0);

            _capacity = newCapacity;
            _vector = newVector;
        }

        private int FindIndex(T entity)
        {
            for (int i = 0; i <= _count; i++)
            {
                if (_vector[i].Equals(entity))
                {
                    return i;
                }
            }

            return -1;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private struct Enumerator : IEnumerator<T>
        {
            private readonly MyVector<T> _vector;
            private int _index;
            private readonly int _maxIndex;
            private readonly int _version;

            public Enumerator(MyVector<T> vector)
            {
                _vector = vector;
                _version = vector._version;
                _index = -1;
                _maxIndex = vector._count;
            }

            public bool MoveNext()
            {
                VerifyVersionOrThrow();
                _index++;

                return _index <= _maxIndex;
            }

            public void Reset()
            {
                VerifyVersionOrThrow();
                _index = -1;
            }

            public T Current => _vector[_index];

            object? IEnumerator.Current => Current;

            private void VerifyVersionOrThrow()
            {
                if (_vector._version != _version)
                    throw new ModificationException(_version.ToString());
            }

            public void Dispose()
            {
            }
        }
    }
}
