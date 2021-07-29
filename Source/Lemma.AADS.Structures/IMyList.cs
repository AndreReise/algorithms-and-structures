using System;
using System.Collections.Generic;

namespace Structures
{
    /// <summary>
    /// Represents a collection of objects
    /// </summary>
    /// <typeparam name="T">The type of elements in the list</typeparam>
    public interface IMyList<T> : IEnumerable<T>
    {
        int Size();
        void Add(T entity);
        void Delete(T entity);
        void Insert(T entity, int index);
        T Get(int index);
        void Update(T entity, int index);
        void Clear();
        void Sort<TResult>(Func<T, TResult> comparer) where TResult : IComparable;
    }
}
