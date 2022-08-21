using System.Collections;
namespace Models
{
    /// <summary>
    /// Represents a list that has two possible directions of movement.
    /// </summary>
    public class DoublyLinkedList<T> : IEnumerable<T>
    {
        /// <summary>
        /// Represents the head (start) of the list.
        /// </summary>
        internal QNode<T> Head { get; private set; }
        /// <summary>
        /// Represents the tail (end) of the list.
        /// </summary>
        internal QNode<T> Tail { get; private set; }
        /// <summary>
        /// Represents the list length.
        /// </summary>
        public int Length { get; private set; }

        public DoublyLinkedList()
        {
            Head = Tail = null;
        }

        /// <summary>
        /// Adds new value to the head of the list.
        /// </summary>
        public void AddToStart(T value)
        {
            var newNode = new QNode<T>(value)
            {
                Next = Head
            };
            if (Head == null)
                Tail = newNode;
            else
                Head.Previous = newNode;
            Head = newNode;
            Length++;
        }
        /// <summary>
        /// Adds new value to the tail of the list.
        /// </summary>
        public void AddToEnd(T value)
        {
            var newNode = new QNode<T>(value);
            if (Tail == null)
                Head = newNode;
            else
            {
                newNode.Previous = Tail;
                Tail.Next = newNode;
            }
            Tail = newNode;
            Length++;
        }

        /// <summary>
        /// Removes the first value from the list.
        /// </summary>
        public void RemoveFromStart()
        {
            if(Head != null)
            {
                Head = Head.Next;
                if (Head == null)
                    Tail = null;
                Length--;
            }
        }
        /// <summary>
        /// Removes the last value from the list.
        /// </summary>
        public void RemoveFromEnd()
        {
            if(Tail != null)
            {
                Tail = Tail.Previous;
                Tail.Next = null;
                if (Tail == null)
                    Head = null;
                Length--;
            }
        }
        /// <summary>
        /// Removes a specific value from the list.
        /// </summary>
        /// <param name="value">The wanted value to remove</param>
        /// <returns>the removed value</returns>
        public T Remove(T value)
        {
            QNode<T> current = Head;
            while(current is not null) 
            {
                if(current.Data.Equals(value))
                {
                    if (current.Next is null)
                        Tail = current.Previous;
                    else
                        current.Next.Previous = current.Previous;

                    if (current.Previous is null)
                        Head = current.Next;
                    else
                        current.Previous.Next = current.Next;
                    Length--;
                    break;
                }
                current = current.Next;
            }
            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>True if the list is empty, otherwise false.</returns>
        public bool IsEmpty() => Length == 0;

        public IEnumerator<T> GetEnumerator()
        {
            var current = Head;
            while(current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            string res = "";
            foreach (var item in this)
                res += item + "\n";
            return res;
        }
    }
}
