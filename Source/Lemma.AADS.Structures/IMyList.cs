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
        /// <summary>
        /// Method to get current number of collection elements
        /// </summary>
        int Size();

        /// <summary>
        /// Adds a new entity to the end of the collection
        /// </summary>
        /// <param name="entity">Entity to add</param>
        void Add(T entity);

        /// <summary>
        /// Deletes the first counter entity.
        /// </summary>
        /// <param name="entity">Entity to delete</param>
        void Delete(T entity);

        /// <summary>
        /// Inserts a new entity in specified index
        /// </summary>
        /// <param name="entity">Entity to insert</param>
        /// <param name="index">Insertion index</param>
        void Insert(T entity, int index);

        /// <summary>
        /// Get entity value by specified index
        /// </summary>
        /// <param name="index">Entity index</param>
        /// <returns>Entity value</returns>
        T Get(int index);

        /// <summary>
        /// Update entity`s value by specified index
        /// </summary>
        /// <param name="entity">New entity value</param>
        /// <param name="index">Entity index</param>
        void Update(T entity, int index);
        void Clear();
        void Sort<TResult>(Func<T, TResult> comparer) where TResult : IComparable;
    }
}
