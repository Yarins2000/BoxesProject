using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public QNode<T> Head { get; set; }
        /// <summary>
        /// Represents the tail (end) of the list.
        /// </summary>
        public QNode<T> Tail { get; set; }

        public int Length { get; set; } = 0;

        public DoublyLinkedList()
        {
            Head = Tail = null;
        }

        /// <summary>
        /// Adds new value to the head of the list.
        /// </summary>
        public void AddToStart(T value)
        {
            var newNode = new QNode<T>(value);
            newNode.Next = Head;
            if(Head == null)
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

        //public override string ToString()
        //{
        //    string res = "";
        //    foreach (var item in this)
        //        res += item.Data + ", ";
        //    return res;
        //}
    }
}
