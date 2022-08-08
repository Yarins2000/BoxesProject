namespace Models
{
    public class QNode<T>
    {
        public T Data { get; set; }
        public QNode<T> Next { get; set; }
        public QNode<T> Previous { get; set; }

        public QNode(T data, QNode<T> next = null, QNode<T> previous = null)
        {
            Data = data;
            Next = next;
            Previous = previous;
        }

        public override string ToString()
        {
            return Data.ToString();
        }
    }
}