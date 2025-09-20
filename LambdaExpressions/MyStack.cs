using System;
using System.Collections.Generic;

namespace LambdaExpressions
{
    /// <summary>
    /// A generic stack data structure that supports push/pop operations,
    /// along with extended methods required for SIT232 Practical Task 8.1C.
    /// </summary>
    /// <typeparam name="T">The element type stored in the stack.</typeparam>
    /// <remarks>
    /// Initializes a new instance of the <see cref="MyStack{T}"/> class
    /// with a specified capacity.
    /// </remarks>
    /// <param name="capacity">Maximum number of elements the stack can hold.</param>
    public class MyStack<T>(int capacity)
    {
        private readonly T[] array = new T[capacity];
        public int Count { get; private set; } = 0;

        /// <summary>
        /// Pushes a value onto the top of the stack.
        /// </summary>
        /// <param name="val">The value to add.</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the stack has reached its capacity.
        /// </exception>
        public void Push(T val)
        {
            if (Count < array.Length)
                array[Count++] = val;
            else
                throw new InvalidOperationException("The stack is out of capacity.");
        }

        /// <summary>
        /// Pops (removes and returns) the value at the top of the stack.
        /// </summary>
        /// <returns>The most recently pushed value.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the stack is empty.
        /// </exception>
        public T Pop()
        {
            if (Count > 0)
                return array[--Count];
            else
                throw new InvalidOperationException("The stack is empty.");
        }

        /// <summary>
        /// Searches for the first element (starting from the top)
        /// that matches the given criteria.
        /// </summary>
        /// <param name="criteria">A delegate (lambda) defining the match condition.</param>
        /// <returns>
        /// The first matching element, or <c>default(T)</c> if no match exists.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="criteria"/> is null.
        /// </exception>
        public T? Find(Func<T, bool> criteria)
        {
            ArgumentNullException.ThrowIfNull(criteria);

            for (int i = Count - 1; i >= 0; i--)
            {
                if (criteria(array[i]))
                    return array[i];
            }
            return default;
        }

        /// <summary>
        /// Retrieves all elements that match the given criteria.
        /// </summary>
        /// <param name="criteria">A delegate (lambda) defining the match condition.</param>
        /// <returns>
        /// An array of matching elements, or <c>null</c> if none are found.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="criteria"/> is null.
        /// </exception>
        public T[]? FindAll(Func<T, bool> criteria)
        {
            ArgumentNullException.ThrowIfNull(criteria);

            List<T> results = [];
            for (int i = Count - 1; i >= 0; i--)
            {
                if (criteria(array[i]))
                    results.Add(array[i]);
            }
            return results.Count > 0 ? [.. results] : null;
        }

        /// <summary>
        /// Removes all elements that match the given criteria.
        /// </summary>
        /// <param name="criteria">A delegate (lambda) defining the match condition.</param>
        /// <returns>
        /// The number of elements removed.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="criteria"/> is null.
        /// </exception>
        public int RemoveAll(Func<T, bool> criteria)
        {
            ArgumentNullException.ThrowIfNull(criteria);

            int removed = 0;
            int newIndex = 0;

            for (int i = 0; i < Count; i++)
            {
                if (criteria(array[i]))
                {
                    removed++;
                }
                else
                {
                    array[newIndex++] = array[i];
                }
            }

            Count = newIndex;
            return removed;
        }

        /// <summary>
        /// Finds the maximum element in the stack (using Comparer&lt;T&gt;.Default).
        /// </summary>
        /// <returns>
        /// The maximum element, or <c>default(T)</c> if the stack is empty.
        /// </returns>
        public T? Max()
        {
            if (Count == 0)
                return default;

            Comparer<T> comparer = Comparer<T>.Default;
            T maxVal = array[0];

            for (int i = 1; i < Count; i++)
            {
                if (comparer.Compare(array[i], maxVal) > 0)
                    maxVal = array[i];
            }
            return maxVal;
        }

        /// <summary>
        /// Finds the minimum element in the stack (using Comparer&lt;T&gt;.Default).
        /// </summary>
        /// <returns>
        /// The minimum element, or <c>default(T)</c> if the stack is empty.
        /// </returns>
        public T? Min()
        {
            if (Count == 0)
                return default;

            Comparer<T> comparer = Comparer<T>.Default;
            T minVal = array[0];

            for (int i = 1; i < Count; i++)
            {
                if (comparer.Compare(array[i], minVal) < 0)
                    minVal = array[i];
            }
            return minVal;
        }
    }
}
