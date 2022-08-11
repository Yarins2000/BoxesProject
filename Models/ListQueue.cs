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
            if (IsEmpty())
                _list.AddToStart(value);
            else
                _list.AddToEnd(value);
        }

        /// <summary>
        /// Removes the first value from the start of the queue.
        /// </summary>
        public void Dequeue()
        {
            if (IsEmpty())
                return;
            _list.RemoveFromStart();
        }

        public bool IsEmpty() => _list.IsEmpty();

        public IEnumerator<T> GetEnumerator() => _list.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
