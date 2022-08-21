using System.Collections;

namespace Models
{
    /// <summary>
    /// Represents a queue that is based on a doubly linked list.
    /// </summary>
    public class ListQueue<T> : IEnumerable<T>
    {
        private DoublyLinkedList<T> _list;

        public ListQueue()
        {
            _list = new();
        }

        /// <summary>
        /// Adds a new value to the end of the queue.
        /// </summary>
        public void Enqueue(T value)
        {
            _list.AddToEnd(value);
        }

        /// <summary>
        /// Removes the first value from the start of the queue.
        /// </summary>
        public T Dequeue()
        {
            if (IsEmpty())
                return default;
            var deleted = _list.Head.Data;
            _list.RemoveFromStart();
            return deleted;
        }

        /// <summary>
        /// Removes the value from the queue.
        /// </summary>
        /// <param name="value">the wanted value to remove</param>
        /// <returns>the removed value</returns>
        public T Dequeue(T value)
        {
            if (IsEmpty())
                return default;
            return _list.Remove(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>True if the list is empty, otherwise false.</returns>
        public bool IsEmpty() => _list.IsEmpty();

        public IEnumerator<T> GetEnumerator() => _list.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
