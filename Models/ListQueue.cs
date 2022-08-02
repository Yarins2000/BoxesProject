namespace Models
{
    public class ListQueue<T>
    {
        public QNode<T>? Front { get; set; }
        public QNode<T>? Back { get; set; }
        public int Count { get; set; } = 0;

        public ListQueue()
        {
            Front = Back = null;
        }

        public void Enqueue(T data)
        {
            var temp = new QNode<T>(data);
            if(IsEmpty())
            {
                Front = Back = temp;
            }
        }

        public bool IsEmpty() => Count == 0;
    }
}
