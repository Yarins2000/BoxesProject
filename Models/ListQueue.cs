using System.Collections;

namespace Models
{
    public class ListQueue<T> : IEnumerable<QNode<T>>
    {
        private DoublyLinkedList<T> _list;

        public ListQueue()
        {
            _list = new();
        }

        public void Enqueue(T value)
        {
            if (IsEmpty())
                _list.AddToStart(value);
            else
                _list.AddToEnd(value);
        }

        public void Dequeue()
        {
            if (IsEmpty())
                return;
            _list.RemoveFromStart();
        }

        public bool IsEmpty() => _list.IsEmpty();

        public IEnumerator<QNode<T>> GetEnumerator() => _list.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
