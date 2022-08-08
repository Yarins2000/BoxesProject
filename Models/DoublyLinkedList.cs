using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class DoublyLinkedList<T> : IEnumerable<QNode<T>>
    {
        public QNode<T> Head { get; set; }
        public QNode<T> Tail { get; set; }

        public int Length { get; set; } = 0;

        public DoublyLinkedList()
        {
            Head = Tail = null;
        }

        public void AddToStart(T val)
        {
            var newNode = new QNode<T>(val);
            newNode.Next = Head;
            if(Head == null)
                Tail = newNode;
            else
                Head.Previous = newNode;
            Head = newNode;
            Length++;
        }
        public void AddToEnd(T val)
        {
            var newNode = new QNode<T>(val);
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

        public IEnumerator<QNode<T>> GetEnumerator()
        {
            var current = Head;
            while(current != null)
            {
                yield return current;
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
                res += item.Data + ", ";
            return res;
        }
    }
}
