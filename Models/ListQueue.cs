namespace Models
{
    public class ListQueue<T>
    {
        private DoublyLinkedList<T> _list;

        public ListQueue()
        {
            _list = new DoublyLinkedList<T>();
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

    }
}
